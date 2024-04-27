using System;
using System.Windows.Forms;

namespace ArtworkUploader {
	[Obsolete("Should be replaced with Weasyl-specific form")]
	public partial class UsernamePasswordDialog : Form {
		public string Username => txtUsername.Text;
		public string Password => txtPassword.Text;

		public string UsernameLabel {
			get {
				return lblUsername.Text;
			}
			set {
				lblUsername.Text = value;
			}
		}

		public bool ShowPassword {
			get {
				return lblPassword.Visible || txtPassword.Visible;
			}
			set {
				lblPassword.Visible = txtPassword.Visible = value;
			}
		}

		public UsernamePasswordDialog() {
			InitializeComponent();
		}
	}
}
