using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Fsfs = FurAffinityFs.FurAffinity;

namespace ArtworkUploader.FurAffinity {
	public partial class FurAffinityPostForm : Form {
		private readonly Fsfs.ICredentials _credentials;
		private readonly PostMetadata _post;
		private readonly PostImage _downloaded;

		public FurAffinityPostForm(Fsfs.ICredentials s, PostMetadata post, PostImage downloaded) {
			InitializeComponent();
			_credentials = s;
			_post = post;
			_downloaded = downloaded;

			txtTitle.Text = post.Title;
			txtDescription.Enabled = false;
			txtTags.Text = string.Join(" ", post.Tags.Where(t => t.Length >= 3));

			if (post.Adult) {
				radRating2.Checked = true;
			} else if (post.Mature) {
				radRating1.Checked = true;
			} else {
				radRating0.Checked = true;
			}
		}

		private static bool HasAlpha(Image image) {
			return image.PixelFormat.HasFlag(PixelFormat.Alpha);
		}

		private async void Form_Shown(object sender, EventArgs e) {
			PopulateDescription();

			chkRemoveTransparency.Enabled = HasAlpha(_downloaded.Image);

			try {
				var options = await Fsfs.ListPostOptionsAsync(_credentials);

				foreach (var x in options.Categories)
					ddlCategory.Items.Add(x);
				foreach (var x in options.Types)
					ddlTheme.Items.Add(x);
				foreach (var x in options.Species)
					ddlSpecies.Items.Add(x);
				foreach (var x in options.Genders)
					ddlGender.Items.Add(x);

				lblUsername1.Text = await Fsfs.WhoamiAsync(_credentials);

				foreach (var galleryFolder in await Fsfs.ListGalleryFoldersAsync(_credentials)) {
					listBox1.Items.Add(galleryFolder);
				}
			} catch (Exception ex) {
				Console.Error.WriteLine(ex);
			}
		}

		private void PopulateDescription() {
			try {
				txtDescription.Text = HtmlConversion.ConvertHtmlToText(_post.HTMLDescription);
			} catch (Exception) { }
			txtDescription.Enabled = true;
		}

		private async void btnPost_Click(object sender, EventArgs e) {
			btnPost.Enabled = false;
			try {
				byte[] data = _downloaded.Data.ToArray();
				string filename = _downloaded.Filename;

				var image = _downloaded.Image;

				if (chkRemoveTransparency.Checked) {
					using var newImage = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);

					using (var g = Graphics.FromImage(newImage)) {
						g.FillRectangle(new SolidBrush(Color.White), 0, 0, image.Width, image.Height);
						g.DrawImage(image, 0, 0, image.Width, image.Height);
					}

					using var msout = new MemoryStream();

					newImage.Save(msout, ImageFormat.Png);

					data = msout.ToArray();
					filename = "image.png";
				}

				IEnumerable<long> folderIds() {
					foreach (var item in listBox1.SelectedItems) {
						if (item is Fsfs.ExistingGalleryFolderInformation f) {
							yield return f.FolderId;
						}
					}
				}

				await Fsfs.PostArtworkAsync(
					_credentials,
					new Fsfs.File(filename, data),
					new Fsfs.ArtworkMetadata(
						title: txtTitle.Text,
						message: txtDescription.Text,
						keywords: Fsfs.Keywords(txtTags.Text.Split(' ').Select(s => s.Trim()).Where(s => s != "").ToArray()),
						cat: ddlCategory.SelectedItem is Fsfs.PostOption<Fsfs.Category> x1
							? x1.Value
							: Fsfs.Category.All,
						scrap: chkScraps.Checked,
						atype: ddlTheme.SelectedItem is Fsfs.PostOption<Fsfs.Type> x2
							? x2.Value
							: Fsfs.Type.All,
						species: ddlSpecies.SelectedItem is Fsfs.PostOption<Fsfs.Species> x3
							? x3.Value
							: Fsfs.Species.Unspecified_Any,
						gender: ddlGender.SelectedItem is Fsfs.PostOption<Fsfs.Gender> x4
							? x4.Value
							: Fsfs.Gender.Any,
						rating: radRating0.Checked ? Fsfs.Rating.General
							: radRating1.Checked ? Fsfs.Rating.Mature
							: radRating2.Checked ? Fsfs.Rating.Adult
							: throw new ApplicationException("Must select a rating"),
						lock_comments: chkLockComments.Checked,
						folder_ids: Fsfs.FolderIds(folderIds().ToArray()),
						create_folder_name: Fsfs.NoNewFolder
					));

				Close();
			} catch (Exception ex) {
				btnPost.Enabled = true;
				MessageBox.Show(this, ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
