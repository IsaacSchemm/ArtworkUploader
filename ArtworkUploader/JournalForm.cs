using ArtworkUploader.DeviantArt;
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

using DeviantArtJournal = DeviantArtFs.Api.Deviation.Journal;

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
				checkedListBox1.Items.Add(new DeviantArtTokenWrapper(s, x));
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
				if (item is DeviantArtTokenWrapper deviantArtTokenWrapper) {
					await DeviantArtJournal.CreateAsync(
						deviantArtTokenWrapper,
						[DeviantArtJournal.ImmutableField.NewBody(html)],
						[
							DeviantArtJournal.MutableField.NewTitle(title),
							DeviantArtJournal.MutableField.NewIsMature(false)
						]);
				} else if (item is Settings.FurAffinitySettings furAffinitySettings) {
					await FurAffinityFs.FurAffinity.PostJournalAsync(
						furAffinitySettings,
						new FurAffinityFs.FurAffinity.Journal(
							subject: title,
							message: text,
							disable_comments: false,
							make_featured: false));
				} else if (item is Settings.WeasylSettings weasylSettings) {
					var client = new WeasylClient(weasylSettings);
					int? journalid = await client.UploadJournalAsync(
						title: title,
						rating: WeasylClient.Rating.General,
						content: html,
						tags: tags);
				} else {
					throw new NotImplementedException();
				}
			}

			DialogResult = DialogResult.OK;
		}
	}
}
