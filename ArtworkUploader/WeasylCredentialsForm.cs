using System.Windows.Forms;

namespace ArtworkUploader {
	public partial class WeasylCredentialsForm : Form {
		public string ApiKey => textBox1.Text;

		public WeasylCredentialsForm() {
			InitializeComponent();
		}
	}
}
