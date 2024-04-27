using DeviantArtFs;
using DeviantArtFs.ParameterTypes;
using DeviantArtFs.ResponseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ArtworkUploader.DeviantArt {
	public partial class DeviantArtFolderSelectionForm : Form {
		public IEnumerable<GalleryFolder> InitialFolders { get; set; }

		private readonly IDeviantArtAccessToken _token;
		private readonly List<GalleryFolder> _selectedFolders;
		public IEnumerable<GalleryFolder> SelectedFolders => _selectedFolders;

		public DeviantArtFolderSelectionForm(IDeviantArtAccessToken token) {
			InitializeComponent();
			_token = token;
			_selectedFolders = [];
		}

		private async void DeviantArtFolderSelectionForm_Load(object sender, EventArgs e) {
			try {
				this.Enabled = false;

				var enumerable = DeviantArtFs.Api.Gallery.GetFoldersAsync(_token,
					CalculateSize.NewCalculateSize(false),
					FolderPreload.NewFolderPreload(false),
					FilterEmptyFolder.NewFilterEmptyFolder(false),
					UserScope.ForCurrentUser,
					PagingLimit.MaximumPagingLimit,
					PagingOffset.StartingOffset);

				await foreach (var f in enumerable) {
					var chk = new CheckBox {
						AutoSize = true,
						Text = f.name,
						Checked = InitialFolders?.Any(f2 => f.folderid == f2.folderid) == true
					};
					chk.CheckedChanged += (o, ea) => {
						if (chk.Checked) {
							_selectedFolders.Add(f);
						} else {
							_selectedFolders.Remove(f);
						}
					};
					flowLayoutPanel1.Controls.Add(chk);
				}

				this.Enabled = true;
			} catch (Exception ex) {
				MessageBox.Show(this.ParentForm, ex.Message, $"{this.GetType().Name}: {ex.GetType().Name}");
				this.Close();
			}
		}
	}
}
