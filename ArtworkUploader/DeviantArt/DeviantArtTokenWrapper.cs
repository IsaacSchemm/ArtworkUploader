using DeviantArtFs;
using System.Threading.Tasks;

namespace ArtworkUploader.DeviantArt {
	public class DeviantArtTokenWrapper(Settings parent, Settings.DeviantArtAccountSettings current) : IDeviantArtRefreshableAccessToken {
		public string RefreshToken => current.RefreshToken;
		public string AccessToken => current.AccessToken;
		public string Username => current.Username;

		private record AccessTokenOnly(string AccessToken) : IDeviantArtAccessToken;

		async Task IDeviantArtRefreshableAccessToken.RefreshAccessTokenAsync() {
			var resp = await DeviantArtAuth.RefreshAsync(
				DeviantArtAppCredentials.AppCredentials,
				RefreshToken);
			current.AccessToken = resp.access_token;
			current.RefreshToken = resp.refresh_token;
			parent.Save();
		}

		public override string ToString() {
			return $"DeviantArt ({Username})";
		}
	}
}
