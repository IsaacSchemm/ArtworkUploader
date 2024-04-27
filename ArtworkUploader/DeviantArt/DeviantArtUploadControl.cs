using DeviantArtFs;
using DeviantArtFs.ParameterTypes;
using DeviantArtFs.ResponseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static DeviantArtFs.Api.Stash;

namespace ArtworkUploader.DeviantArt {
	public partial class DeviantArtUploadControl : UserControl {
		public delegate void DeviantArtUploadedHandler(string url);
		public event DeviantArtUploadedHandler Uploaded;

		private IEnumerable<GalleryFolder> _selectedFolders;
		public IEnumerable<GalleryFolder> SelectedFolders {
			get {
				return _selectedFolders;
			}
			set {
				_selectedFolders = value;
				txtGalleryFolders.Text = value == null
					? ""
					: string.Join(", ", value.Select(f => f.name));
			}
		}

		private IFormFile _downloaded;

		public string UploadedUrl { get; private set; }

		private readonly IDeviantArtAccessToken _token;

		public DeviantArtUploadControl(IDeviantArtAccessToken token) {
			InitializeComponent();
			_token = token;

			radNone.CheckedChanged += MatureChanged;
			radModerate.CheckedChanged += MatureChanged;
			radStrict.CheckedChanged += MatureChanged;

			ddlLicense.Items.Clear();
			ddlLicense.Items.AddRange(License.All.ToArray());

			ddlLicense.SelectedIndex = 0;
			ddlSharing.SelectedIndex = 0;
		}

		public void SetSubmission(TextPost post, IFormFile downloaded) {
			_downloaded = downloaded;

			txtTitle.Text = post.Title ?? "";
			txtArtistComments.Text = post.HTMLDescription ?? "";
			txtTags.Text = string.Join(" ", post.Tags?.Select(s => $"#{s}") ?? Enumerable.Empty<string>());
			if (post.Mature) {
				radStrict.Checked = true;
			} else {
				radNone.Checked = true;
			}
		}

		private void MatureChanged(object sender, EventArgs e) {
			grpMatureClassification.Enabled = !radNone.Checked;
		}

		private void btnCategory_Click(object sender, EventArgs e) {
			MessageBox.Show(this, "This feature is no longer supported.");
		}

		private void btnGalleryFolders_Click(object sender, EventArgs e) {
			try {
				using var form = new DeviantArtFolderSelectionForm(_token);
				if (form.ShowDialog() == DialogResult.OK) {
					SelectedFolders = form.SelectedFolders;
				}
			} catch (Exception ex) {
				MessageBox.Show(this.ParentForm, ex.Message, $"{this.GetType()}, {ex.GetType()}");
			}
		}

		private async void btnPublish_Click(object sender, EventArgs e) {
			if (chkAgree.Checked == false) {
				MessageBox.Show("Before submitting to DeviantArt, you must agree to the Submission Policy and the Terms of Service.");
				return;
			}

			try {
				this.Enabled = false;

				var item = await SubmitAsync(
					_token,
					SubmissionDestination.Default,
					new SubmissionParameters(
						SubmissionTitle.NewSubmissionTitle(txtTitle.Text),
						ArtistComments.NewArtistComments(txtArtistComments.Text),
						TagList.Create(txtTags.Text.Replace("#", "").Replace(",", "").Split(' ').Where(s => s != "")),
						OriginalUrl.NoOriginalUrl,
						is_dirty: false),
					_downloaded);

				var classifications = new List<MatureClassification>();
				if (chkNudity.Checked) classifications.Add(MatureClassification.Nudity);
				if (chkSexual.Checked) classifications.Add(MatureClassification.Sexual);
				if (chkGore.Checked) classifications.Add(MatureClassification.Gore);
				if (chkLanguage.Checked) classifications.Add(MatureClassification.Language);
				if (chkIdeology.Checked) classifications.Add(MatureClassification.Ideology);

				var sharingStr = ddlSharing.SelectedItem?.ToString();
				var sharing = sharingStr == "Show share buttons" ? Sharing.AllowSharing
					: sharingStr == "Hide share buttons" ? Sharing.HideShareButtons
					: sharingStr == "Hide & require login to view" ? Sharing.HideShareButtonsAndMembersOnly
					: throw new Exception("Unrecognized ddlSharing.SelectedItem");

				IEnumerable<PublishParameter> getParams() {
					yield return PublishParameter.NewMaturity(
							radNone.Checked
								? Maturity.NotMature
								: Maturity.MatureBecause(
									radStrict.Checked ? MatureLevel.MatureStrict : MatureLevel.MatureModerate,
									classifications));
					yield return PublishParameter.NewSubmissionPolicyAgreement(chkAgree.Checked);
					yield return PublishParameter.NewTermsOfServiceAgreement(chkAgree.Checked);
					yield return PublishParameter.NewAllowComments(chkAllowComments.Checked);
					yield return PublishParameter.NewRequestCritique(chkRequestCritique.Checked);
					yield return PublishParameter.NewSharing(sharing);
					if (ddlLicense.SelectedItem is License l)
						yield return PublishParameter.NewLicense(l);
					foreach (var f in SelectedFolders)
						yield return PublishParameter.NewGalleryId(f.folderid);
					yield return PublishParameter.NewAllowFreeDownload(chkAllowFreeDownload.Checked);
				}

				var resp = await PublishAsync(_token, getParams(), Item.NewItem(item.itemid));

				Uploaded?.Invoke(resp.url);
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, $"{GetType()} {ex.GetType()}");
			}

			this.Enabled = true;
		}
	}
}
