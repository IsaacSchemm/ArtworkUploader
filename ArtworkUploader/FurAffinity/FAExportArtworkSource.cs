using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ArtworkUploader.FurAffinity {
	public static class FAExportArtworkSource {
		public const string FAExportHost = "faexport.spangle.org.uk";

		public record CurrentUser {
			public string profile_name;
		}

		public record Notifications {
			public CurrentUser current_user;
		}

		public static async Task<Notifications> GetNotificationsAsync(string fa_cookie, bool sfw, int from) {
			var req = WebRequest.CreateHttp($"https://{FAExportHost}/notifications/others.json?{(sfw ? "sfw=1" : "")}&from={from}");
			req.UserAgent = "CrosspostSharp/4.0 (https://github.com/libertyernie/CrosspostSharp)";
			req.Headers.Set("FA_COOKIE", fa_cookie);
			using var resp = await req.GetResponseAsync();
			using var stream = resp.GetResponseStream();
			using var sr = new StreamReader(stream);
			string json = await sr.ReadToEndAsync();
			return JsonConvert.DeserializeObject<Notifications>(json);
		}

		public static async Task<string> GetUsernameAsync(string fa_cookie, bool sfw) {
			var resp = await GetNotificationsAsync(fa_cookie, sfw, from: int.MaxValue);
			return resp.current_user.profile_name;
		}
	}
}
