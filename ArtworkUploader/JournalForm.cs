using ArtworkUploader.Weasyl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtworkUploader {
	public partial class JournalForm : Form {
		public JournalForm() {
			InitializeComponent();
		}

		private void JournalForm_Shown(object sender, EventArgs e) {
			wbrDescription.Navigate("about:blank");
			wbrDescription.Document.Write($"<html><head></head><body></body></html>");
			wbrDescription.Document.Body.SetAttribute("contenteditable", "true");

			Settings s = Settings.Load();

			foreach (var x in s.DeviantArtAccounts) {
				checkedListBox1.Items.Add(x);
			}
			foreach (var x in s.FurAffinity) {
				checkedListBox1.Items.Add(x);
			}
			foreach (var x in s.WeasylApi) {
				checkedListBox1.Items.Add(x);
			}
		}

		private async void btnPost_Click(object sender, EventArgs e) {
			string title = txtTitle.Text;
			string html = wbrDescription.Document.Body.InnerHtml;
			string text = wbrDescription.Document.Body.InnerText;
			string[] tags = txtTags.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			foreach (var item in checkedListBox1.CheckedItems) {
				if (item is Settings.DeviantArtAccountSettings deviantArtSettings) {
					throw new NotImplementedException();
				} else if (item is Settings.FurAffinitySettings furAffinitySettings) {
					throw new NotImplementedException();
				} else if (item is Settings.WeasylSettings weasylSettings) {
					var client = new WeasylClient(weasylSettings.apiKey);
					int? journalid = await client.UploadJournalAsync(
						title: title,
						rating: WeasylClient.Rating.General,
						content: html,
						tags: tags);
					if (journalid is int j) {
						MessageBox.Show(this, $"{j}");
					}
				} else {
					throw new NotImplementedException();
				}
			}

			DialogResult = DialogResult.OK;
		}
	}
}
