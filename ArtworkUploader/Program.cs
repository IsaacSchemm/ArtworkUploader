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
				Application.Run(new ArtworkForm(args[0]));
			} else {
				Application.Run(new ArtworkForm());
			}
		}
	}
}
