using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace ArtworkUploader.Weasyl {
	public partial class WeasylPostForm : Form {
		private readonly WeasylClient _apiClient;
		private readonly WeasylClient _frontendClient;
		private readonly PostImage _downloaded;

		public WeasylPostForm(Settings.WeasylSettings s, PostMetadata post, PostImage downloaded) {
			InitializeComponent();

			_apiClient = _frontendClient = new WeasylClient(s);
			_downloaded = downloaded;

			txtTitle.Text = post.Title;
			txtDescription.Text = post.HTMLDescription;
			txtTags.Text = string.Join(" ", post.Tags.Select(t => t.Replace(' ', '_')));

			txtAltText.Enabled = s.crowmaskHost != null;

			foreach (var o in Enum.GetValues(typeof(WeasylClient.SubmissionType))) {
				ddlCategory.Items.Add((WeasylClient.SubmissionType)o);
			}
			foreach (var o in Enum.GetValues(typeof(WeasylClient.Rating))) {
				ddlRating.Items.Add((WeasylClient.Rating)o);
			}
		}

		private async void WeasylPostForm_Shown(object sender, EventArgs e) {
			try {
				var user = await _apiClient.WhoamiAsync();
				if (user?.login == null) {
					MessageBox.Show("You are not logged in.");
					Close();
					return;
				}
				lblUsername1.Text = user.login;

				var folders = await _frontendClient.GetFoldersAsync().ToListAsync();
				ddlFolder.Items.Add("");
				foreach (var f in folders) ddlFolder.Items.Add(f);
			} catch (Exception) { }
		}

		private async void btnPost_Click(object sender, EventArgs e) {
			btnPost.Enabled = false;
			try {
				if (ddlCategory.SelectedItem is not WeasylClient.SubmissionType subtype) {
					throw new Exception("A category is required.");
				}
				if (ddlRating.SelectedItem is not WeasylClient.Rating rating) {
					throw new Exception("A rating is required.");
				}

				var folder = ddlFolder.SelectedItem as WeasylClient.Folder;

				int? result = await _frontendClient.UploadVisualAsync(
					_downloaded.Data,
					txtTitle.Text,
					subtype,
					folder?.FolderId,
					rating,
					txtDescription.Text,
					txtTags.Text.Split(' '));

				if (result is int submitid) {
					await _frontendClient.RefreshCrowmaskSubmissionAsync(submitid, alt: txtAltText.Text);
				}

				Close();
			} catch (WebException ex) when (ex.Response is HttpWebResponse r && r.StatusCode == HttpStatusCode.Forbidden && r.Server == "cloudflare") {
				btnPost.Enabled = true;
				MessageBox.Show(this, "Automated upload blocked by Cloudflare. You'll need to upload to the website manually.", ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
			} catch (Exception ex) {
				btnPost.Enabled = true;
				MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
