﻿using System.Linq;
using System.Windows.Forms;

namespace ArtworkUploader.FurAffinity {
	public partial class FurAffinityLoginForm : Form {
		public string ACookie { get; private set; } = null;
		public string BCookie { get; private set; } = null;

		public FurAffinityLoginForm() {
			Width = 600;
			Height = 600;

			var webBrowser1 = new WebBrowser {
				Dock = DockStyle.Fill
			};
			Controls.Add(webBrowser1);

			Shown += (sender, e) => {
				webBrowser1.Navigate("https://www.furaffinity.net/msg/others/");
			};

			webBrowser1.Navigated += (sender, e) => {
				Text = (sender as WebBrowser)?.Document?.Title ?? "";

				var cks = CppCookieTools.Cookies.GetCookies("https://www.furaffinity.net");
				var dict = cks.ToDictionary(x => x.Name, x => x.Value);
				if (dict.TryGetValue("a", out string a) && dict.TryGetValue("b", out string b)) {
					ACookie = a;
					BCookie = b;
					DialogResult = DialogResult.OK;
				}
			};
		}
	}
}
