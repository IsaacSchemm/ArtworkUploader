using System.Windows.Forms;

namespace ArtworkUploader {
	public partial class AboutForm : Form {
		public AboutForm() {
			InitializeComponent();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start(linkLabel1.Text);
		}
	}
}
