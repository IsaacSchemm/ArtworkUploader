using System;
using System.Windows.Forms;

namespace ArtworkUploader {
	public static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			CppCookieTools.Cookies.SetSuppressBehaviorForProcess(CppCookieTools.SuppressBehavior.CookiePersist);
			Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (args.Length == 1) {
				try {
					Application.Run(new ArtworkForm(args[0]));
				} catch (Exception ex) {
					MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} else {
				Application.Run(new MainForm());
			}
		}
	}
}
