using HtmlAgilityPack;

namespace ArtworkUploader {
	public static class HtmlConversion {
		public static string ConvertHtmlToText(string html) {
			var document = new HtmlDocument();

			html = html.Replace("<div><br></div>", "");
			html = html.Replace("<br></div>", "</div>");

			document.LoadHtml(html);

			return document.DocumentNode.InnerText?.Trim() ?? "";
		}
	}
}
