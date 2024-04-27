using DeviantArtFs;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ArtworkUploader.DeviantArt {
	public partial class DeviantArtStatusUpdateForm : Form {
		private readonly LocalFile _downloaded;

		private readonly IDeviantArtAccessToken _token;

		public DeviantArtStatusUpdateForm(IDeviantArtAccessToken token, TextPost post, LocalFile downloaded = null) {
			InitializeComponent();
			_token = token;
			_downloaded = downloaded;

			textBox1.Text = post.HTMLDescription;
		}

		private async void DeviantArtStatusUpdateForm_Shown(object sender, EventArgs e) {
			try {
				if (_downloaded != null) {
					picImageToPost.Image = Image.FromFile(_downloaded.Filename);
				} else {
					picImageToPost.Visible = false;
				}

				var u = await DeviantArtFs.Api.User.WhoamiAsync(_token);
				lblUsername1.Text = u.username;
				picUserIcon.ImageLocation = u.usericon;
			} catch (Exception) { }
		}

		private async void btnPost_Click(object sender, EventArgs e) {
			btnPost.Enabled = false;

			try {
				long? itemId = null;

				if (_downloaded != null) {
					var resp = await DeviantArtFs.Api.Stash.SubmitAsync(
						_token,
						DeviantArtFs.Api.Stash.SubmissionDestination.Default,
						DeviantArtFs.Api.Stash.SubmissionParameters.Default,
						_downloaded);
					itemId = resp.itemid;
				}

				await DeviantArtFs.Api.User.PostStatusAsync(
					_token,
					new DeviantArtFs.Api.User.EmbeddableStatusContent(
						DeviantArtFs.Api.User.EmbeddableObject.Nothing,
						DeviantArtFs.Api.User.EmbeddableObjectParent.NoParent,
						itemId is long x
							? DeviantArtFs.Api.User.EmbeddableStashItem.NewStashItem(x)
							: DeviantArtFs.Api.User.EmbeddableStashItem.NoStashItem),
					textBox1.Text);

				Close();
			} catch (Exception ex) {
				btnPost.Enabled = true;
				MessageBox.Show(this, ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
