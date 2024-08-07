﻿using ArtworkUploader.DeviantArt;
using ArtworkUploader.FurAffinity;
using ArtworkUploader.Weasyl;
using DeviantArtFs.WinForms;
using DeviantArtFs;
using Microsoft.FSharp.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ArtworkUploader {
	public partial class ArtworkForm : Form {
		private PostImage _loadedFile = null;

		private record DestinationOption(string Name, Action Click) {
			public override string ToString() {
				return Name;
			}
		}

		public PostMetadata ExportAsText() {
			return new PostMetadata {
				Title = txtTitle.Text,
				HTMLDescription = wbrDescription.Document.Body.InnerHtml,
				Tags = ListModule.OfSeq(txtTags.Text.Split(' ').Where(s => s != "")),
				Mature = chkMature.Checked,
				Adult = chkAdult.Checked
			};
		}

		public ArtworkForm(string filename = null) {
			InitializeComponent();

			Shown += (o, e) => {
				if (DeviantArtAppCredentials.AppCredentials == null) {
					deviantArtToolStripMenuItem.Visible = false;
				}

				ResetList();

				wbrDescription.Navigate("about:blank");
				wbrDescription.Document.Write($"<html><head></head><body></body></html>");
				wbrDescription.Document.Body.SetAttribute("contenteditable", "true");

				if (filename is string f) {
					LoadImage(f);
				}
			};
		}

		public void LoadImage(string filename) {
			_loadedFile = new PostImage(filename);

			var image = Image.FromFile(filename);
			pictureBox1.Image = image;
		}

		private void ResetList() {
			listBox1.Items.Clear();

			Settings settings = Settings.Load();

			foreach (var da in settings.DeviantArtTokens) {
				listBox1.Items.Add(new DestinationOption($"DeviantArt ({da.Username})", () => {
					if (_loadedFile == null)
						return;

					DeviantArtFs.Api.Stash.IFormFile file = _loadedFile;

					if (file.ContentType == "image/gif") {
						MessageBox.Show(this, "GIF images on DeviantArt require a separate preview image, which isn't possible via the API.", Text);
						return;
					}

					using var f = new Form();

					f.Width = 600;
					f.Height = 375;
					var d = new DeviantArtUploadControl(da) {
						Dock = DockStyle.Fill
					};
					f.Controls.Add(d);
					d.Uploaded += url => f.Close();

					d.SetSubmission(ExportAsText(), file);

					f.ShowDialog(this);
				}));
			}
			foreach (var fa in settings.FurAffinity) {
				listBox1.Items.Add(new DestinationOption($"Fur Affinity ({fa.username})", () => {
					if (_loadedFile == null)
						return;

					using var f = new FurAffinityPostForm(fa, ExportAsText(), _loadedFile);
					f.ShowDialog(this);
				}));
			}
			foreach (var w in settings.WeasylApi) {
				listBox1.Items.Add(new DestinationOption($"Weasyl ({w.username})", () => {
					if (_loadedFile == null)
						return;

					using var f = new WeasylPostForm(w, ExportAsText(), _loadedFile);
					f.ShowDialog(this);
				}));
			}
		}

		private void btnPost_Click(object sender, EventArgs ea) {
			var o = listBox1.SelectedItem as DestinationOption;
			o?.Click?.Invoke();
		}

		public const string OpenFilter = "All supported formats|*.png;*.jpg;*.jpeg;*.gif|Image files|*.png;*.jpg;*.jpeg;*.gif|All files|*.*";

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			using var openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = OpenFilter;
			openFileDialog.Multiselect = false;
			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				LoadImage(openFileDialog.FileName);
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
			Close();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void listBox1_DoubleClick(object sender, EventArgs e) {
			btnPost.PerformClick();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			using var f = new AboutForm();
			f.ShowDialog(this);
		}

		private void deviantArtToolStripMenuItem_Click(object sender, EventArgs e) {
			toolsToolStripMenuItem.Enabled = false;

			var app = DeviantArtAppCredentials.AppCredentials;
			if (app == null) {
				MessageBox.Show(this, "DeviantArt is not supported.");
				return;
			}

			async IAsyncEnumerable<Settings.DeviantArtAccountSettings> promptForCredentials() {
				using var f = new DeviantArtAuthorizationCodeForm(
					int.Parse(app.client_id),
					new Uri("https://www.example.com"),
					["browse", "user", "stash", "publish", "user.manage"]);
				f.Width = 525;
				f.Height = 800;
				if (f.ShowDialog(this) == DialogResult.OK) {
					var token = await DeviantArtAuth.GetTokenAsync(app, f.Code, new Uri("https://www.example.com"));
					var u = await DeviantArtFs.Api.User.WhoamiAsync(token);
					yield return new Settings.DeviantArtAccountSettings {
						AccessToken = token.access_token,
						RefreshToken = token.refresh_token,
						Username = u.username
					};
				}
			}

			Settings s = Settings.Load();
			using (var acctSelForm = new AccountSelectionForm<Settings.DeviantArtAccountSettings>(s.DeviantArtAccounts, () => promptForCredentials())) {
				acctSelForm.ShowDialog(this);
				s.DeviantArtAccounts = acctSelForm.CurrentList.ToList();
				s.Save();
				ResetList();
			}

			toolsToolStripMenuItem.Enabled = true;
		}

		private void furAffinityToolStripMenuItem_Click(object sender, EventArgs e) {
			toolsToolStripMenuItem.Enabled = false;

			static async IAsyncEnumerable<Settings.FurAffinitySettings> promptForCredentials() {
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
				ResetList();
			}

			toolsToolStripMenuItem.Enabled = true;
		}

		private void weasylToolStripMenuItem_Click(object sender, EventArgs e) {
			toolsToolStripMenuItem.Enabled = false;

			static async IAsyncEnumerable<Settings.WeasylSettings> promptForCredentials() {
				using var f = new WeasylCredentialsForm();
				if (f.ShowDialog() == DialogResult.OK) {
					var settings = new Settings.WeasylSettings {
						apiKey = f.ApiKey
					};
					var client = new WeasylClient(settings);
					var user = await client.WhoamiAsync();
					settings.username = user.login;
					yield return settings;
				}
			}

			Settings s = Settings.Load();
			using (var acctSelForm = new AccountSelectionForm<Settings.WeasylSettings>(s.WeasylApi, () => promptForCredentials())) {
				acctSelForm.ShowDialog(this);
				s.WeasylApi = acctSelForm.CurrentList.ToList();
				s.Save();
				ResetList();
			}

			toolsToolStripMenuItem.Enabled = true;
		}

		private void postJournalToolStripMenuItem_Click(object sender, EventArgs e) {
			using var form = new JournalForm();
			form.ShowDialog(this);
		}
	}
}
