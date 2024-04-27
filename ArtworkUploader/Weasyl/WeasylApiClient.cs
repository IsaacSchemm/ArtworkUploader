using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ArtworkUploader.Weasyl {
	public partial class WeasylClient {
		private readonly string _apiKey;

		public WeasylClient(string apiKey = null) {
			_apiKey = apiKey;
		}

		public async Task<WeasylUser> WhoamiAsync() {
			HttpWebRequest req = WebRequest.CreateHttp("https://www.weasyl.com/api/whoami");
			if (_apiKey != null) req.Headers["X-Weasyl-API-Key"] = _apiKey;
			using (WebResponse resp = await req.GetResponseAsync())
			using (StreamReader sr = new StreamReader(resp.GetResponseStream())) {
				string json = await sr.ReadToEndAsync();
				return JsonConvert.DeserializeObject<WeasylUser>(json);
			}
		}
	}
}
