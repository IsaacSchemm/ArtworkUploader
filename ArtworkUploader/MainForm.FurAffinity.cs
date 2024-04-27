using ArtworkUploader.FurAffinity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ArtworkUploader {
	public partial class MainForm {
		private async void furAffinityToolStripMenuItem_Click(object sender, EventArgs e) {
			toolsToolStripMenuItem.Enabled = false;

			async IAsyncEnumerable<Settings.FurAffinitySettings> promptForCredentials() {
				using var f = new FurAffinityLoginForm();
				f.Text = "Log In - FurAffinity";
				if (f.ShowDialog() == DialogResult.OK) {
					var newSettings = new Settings.FurAffinitySettings {
						a = f.ACookie,
						b = f.BCookie
					};
					newSettings.username = await FurAffinityFs.FurAffinity.WhoamiAsync(newSettings);
					yield return newSettings;
				}
			}

			Settings s = Settings.Load();
			using (var acctSelForm = new AccountSelectionForm<Settings.FurAffinitySettings>(s.FurAffinity, () => promptForCredentials())) {
				acctSelForm.ShowDialog(this);
				s.FurAffinity = acctSelForm.CurrentList.ToList();
				s.Save();
				await ReloadWrapperList();
			}

			toolsToolStripMenuItem.Enabled = true;
		}
	}
}
