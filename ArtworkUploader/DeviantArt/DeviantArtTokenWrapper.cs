using DeviantArtFs;
using System.Threading.Tasks;

namespace ArtworkUploader.DeviantArt {
	public class DeviantArtTokenWrapper(Settings parent, Settings.DeviantArtAccountSettings current) : IDeviantArtRefreshableAccessToken {
		public static DeviantArtApp App => new(
			OAuthConsumer.DeviantArt.CLIENT_ID.ToString(),
			OAuthConsumer.DeviantArt.CLIENT_SECRET);

		public string RefreshToken => current.RefreshToken;
		public string AccessToken => current.AccessToken;
		public string Username => current.Username;

		private record AccessTokenOnly(string AccessToken) : IDeviantArtAccessToken;

		async Task IDeviantArtRefreshableAccessToken.RefreshAccessTokenAsync() {
			var resp = await DeviantArtAuth.RefreshAsync(App, RefreshToken);
			current.AccessToken = resp.access_token;
			current.RefreshToken = resp.refresh_token;
			parent.Save();
		}

		public override string ToString() {
			return $"DeviantArt ({Username})";
		}
	}
}
