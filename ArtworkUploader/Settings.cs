using ArtworkUploader.DeviantArt;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArtworkUploader {
	public class Settings {
		public interface IAccountCredentials {
			string Username { get; }
		}

		public class DeviantArtAccountSettings : IAccountCredentials {
			public string AccessToken { get; set; }
			public string RefreshToken { get; set; }
			public string Username { get; set; }
		}

		public List<DeviantArtAccountSettings> DeviantArtAccounts { get; set; } = [];

		[JsonIgnore]
		public IEnumerable<DeviantArtTokenWrapper> DeviantArtTokens =>
			DeviantArtAccounts.Select(x => new DeviantArtTokenWrapper(this, x));

		public struct FurAffinitySettings : IAccountCredentials, FurAffinityFs.FurAffinity.ICredentials {
			public string b;
			public string a;
			public string username;

			readonly string IAccountCredentials.Username => username;

			readonly string FurAffinityFs.FurAffinity.ICredentials.A => a;
			readonly string FurAffinityFs.FurAffinity.ICredentials.B => b;
		}

		public List<FurAffinitySettings> FurAffinity { get; set; } = [];

		public struct WeasylSettings : IAccountCredentials {
			public string username;
			public string apiKey;

			readonly string IAccountCredentials.Username => username;
		}

		public List<WeasylSettings> WeasylApi { get; set; } = [];

		private static readonly JsonSerializerOptions SettingsSerializerOptions = new() {
			IncludeFields = true,
			WriteIndented = true
		};

		public static Settings Load(string filename = "ArtworkUploader.json") {
			Settings s = new();
			if (filename != null && File.Exists(filename)) {
				s = JsonSerializer.Deserialize<Settings>(File.ReadAllText(filename), SettingsSerializerOptions);
			}
			return s;
		}

		public void Save() {
			File.WriteAllText("ArtworkUploader.json", JsonSerializer.Serialize(this, SettingsSerializerOptions));
		}
	}
}
