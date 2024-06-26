﻿namespace ArtworkUploader {
	public static class HtmlConversion {
		public static string ConvertHtmlToText(string html) {
			if (html == null)
				return null;

			html = html.Replace("<div><br></div>", "");
			html = html.Replace("<br></div>", "</div>");

			using var w = new System.Windows.Forms.WebBrowser();
			w.Navigate("about:blank");
			w.Document.Write(html);

			var coll = w.Document.GetElementsByTagName("a");
			for (int i = 0; i < coll.Count; i++) {
				var a = coll[i];
				string href = a.GetAttribute("href");
				if (href.Contains(a.InnerText.Replace("…", ""))) {
					a.InnerText = href;
				}
			}

			return w.Document.Body.InnerText?.Trim() ?? "";
		}
	}
}
