using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArtworkUploader.Weasyl {
	public partial class WeasylClient(string apiKey) {
		private static readonly Lazy<HttpMessageHandler> _httpClientHandler =
			new(() => new SocketsHttpHandler {
				UseCookies = false,
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			});

		private readonly Lazy<HttpClient> _httpClient = new(() => {
			var client = new HttpClient(_httpClientHandler.Value, disposeHandler: false);
			client.DefaultRequestHeaders.Add(
				"X-Weasyl-API-Key",
				apiKey);
			client.DefaultRequestHeaders.UserAgent.Add(
				new ProductInfoHeaderValue(
					"ArtworkUploader",
					"6.0"));
			return client;
		});

		[GeneratedRegex(@"<option value=""(\d+)"">([^<]+)</option>")]
		private static partial Regex OptionTag();

		[GeneratedRegex(@"^/journal/([0-9]+)/")]
		private static partial Regex JournalUri();

		public async Task<WeasylUser> WhoamiAsync() {
			using var resp = await _httpClient.Value.GetAsync("https://www.weasyl.com/api/whoami");
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadFromJsonAsync<WeasylUser>();
		}

		public record Folder(
			int FolderId,
			string Name);

		public async IAsyncEnumerable<Folder> GetFoldersAsync() {
			using var resp = await _httpClient.Value.GetAsync("https://www.weasyl.com/submit/visual");
			resp.EnsureSuccessStatusCode();
			using var stream = await resp.Content.ReadAsStreamAsync();
			using var sr = new StreamReader(stream);

			string line;
			while ((line = await sr.ReadLineAsync()) != null) {
				if (line.Contains("<select name=\"folderid\"")) {
					break;
				}
			}

			while ((line = await sr.ReadLineAsync()) != null) {
				var match = OptionTag().Match(line);
				if (match.Success && int.TryParse(match.Groups[1].Value, out int id)) {
					yield return new Folder(
						id,
						match.Groups[2].Value);
				}
				if (line.Contains("</select>"))
					break;
			}
		}

		public enum SubmissionType {
			Sketch = 1010,
			Traditional = 1020,
			Digital = 1030,
			Animation = 1040,
			Photography = 1050,
			Design_Interface = 1060,
			Modeling_Sculpture = 1070,
			Crafts_Jewelry = 1075,
			Sewing_Knitting = 1078,
			Desktop_Wallpaper = 1080,
			Other = 1999,
		}

		public enum Rating {
			General = 10,
			Mature = 30,
			Explicit = 40,
		}

		public async Task UploadVisualAsync(ReadOnlyMemory<byte> data, string title, SubmissionType subtype, int? folderid, Rating rating, string content, IEnumerable<string> tags) {
			using var req = new HttpRequestMessage(HttpMethod.Post, "https://www.weasyl.com/submit/visual");

			req.Content = new MultipartFormDataContent {
				{ new ReadOnlyMemoryContent(data), "submitfile", "picture.dat" },
				{ new ByteArrayContent([]), "thumbfile", "thumb.dat" },
				{ new StringContent(title), "title" },
				{ new StringContent($"{(int)subtype}"), "subtype" },
				{ new StringContent($"{folderid}"), "folderid" },
				{ new StringContent($"{(int)rating}"), "rating" },
				{ new StringContent($"{content}"), "content" },
				{ new StringContent(string.Join(" ", tags.Select(s => s.Replace(' ', '_')))), "tags" },
			};

			using var resp = await _httpClient.Value.SendAsync(req);
			resp.EnsureSuccessStatusCode();
		}

		public async Task<int?> UploadJournalAsync(string title, Rating rating, string content, IEnumerable<string> tags) {
			using var req = new HttpRequestMessage(HttpMethod.Post, "https://www.weasyl.com/submit/journal");

			req.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
				["title"] = title,
				["rating"] = $"{(int)rating}",
				["content"] = content,
				["tags"] = string.Join(" ", tags.Select(s => s.Replace(' ', '_')))
			});

			using var resp = await _httpClient.Value.SendAsync(req);
			resp.EnsureSuccessStatusCode();

			//https://www.weasyl.com/journal/176145/test-1
			var match = JournalUri().Match(resp.RequestMessage.RequestUri.LocalPath);
			return match.Success && int.TryParse(match.Groups[1].Value, out int journalid)
				? journalid
				: null;
		}
	}
}
