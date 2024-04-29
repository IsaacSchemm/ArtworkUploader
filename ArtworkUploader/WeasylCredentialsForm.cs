using System.Windows.Forms;

namespace ArtworkUploader {
	public partial class WeasylCredentialsForm : Form {
		public string ApiKey => textBox1.Text;
		public string CrowmaskHostname => textBox2.Text;

		public WeasylCredentialsForm() {
			InitializeComponent();
		}
	}
}
