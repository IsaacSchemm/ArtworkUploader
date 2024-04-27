using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtworkUploader {
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();
		}

		private Task ReloadWrapperList() => Task.CompletedTask;

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			using (var openFileDialog = new OpenFileDialog()) {
				openFileDialog.Filter = ArtworkForm.OpenFilter;
				openFileDialog.Multiselect = false;
				if (openFileDialog.ShowDialog() == DialogResult.OK) {
					using var f = new ArtworkForm(openFileDialog.FileName);
					f.ShowDialog(this);
				}
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			using var f = new AboutForm();
			f.ShowDialog(this);
		}
	}
}
