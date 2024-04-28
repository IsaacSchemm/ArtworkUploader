﻿using ArtworkUploader.FurryNetwork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtworkUploader {
	public partial class MainForm {
		private async void furryNetworkToolStripMenuItem_Click(object sender, EventArgs e) {
			toolsToolStripMenuItem.Enabled = false;

			async IAsyncEnumerable<Settings.FurryNetworkSettings> promptForCredentials() {
				using var f1 = new FurryNetworkLoginForm();
				if (f1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					yield break;

				var client = new FurryNetworkClient(f1.RefreshToken);
				var user = await client.GetUserAsync();
				using var f2 = new FurryNetworkCharacterSelectionForm(user.characters);
				if (f2.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					foreach (var character in f2.SelectedItems) {
						yield return new Settings.FurryNetworkSettings {
							refreshToken = f1.RefreshToken,
							characterName = character.Name
						};
					}
				}
			}

			Settings s = Settings.Load();
			using (var acctSelForm = new AccountSelectionForm<Settings.FurryNetworkSettings>(s.FurryNetwork, () => promptForCredentials())) {
				acctSelForm.ShowDialog(this);
				s.FurryNetwork = acctSelForm.CurrentList.ToList();
				s.Save();
				await ReloadWrapperList();
			}

			toolsToolStripMenuItem.Enabled = true;
		}
	}
}
