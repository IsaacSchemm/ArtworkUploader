﻿using ArtworkUploader.DeviantArt;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

		public List<DeviantArtAccountSettings> DeviantArtAccounts = [];

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

		public List<FurAffinitySettings> FurAffinity = [];

		public struct WeasylSettings : IAccountCredentials {
			public string username;
			public string apiKey;

			readonly string IAccountCredentials.Username => username;
		}

		public List<WeasylSettings> WeasylApi = [];

		public static Settings Load(string filename = "ArtworkUploader.json") {
			Settings s = new();
			if (filename != null && File.Exists(filename)) {
				s = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filename));
			}
			return s;
		}

		public void Save() {
			File.WriteAllText("ArtworkUploader.json", JsonConvert.SerializeObject(this, Formatting.Indented));
		}
	}
}
