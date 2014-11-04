﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeasylSync {
	static class Program {
		[DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
		public static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

		private static void SupressCookiePersistence() {
			// http://msdn.microsoft.com/en-us/library/windows/desktop/aa385328%28v=vs.85%29.aspx
			// http://stackoverflow.com/questions/18195844/make-net-webbrowser-not-to-share-cookies-with-ie-or-other-instances

			int INTERNET_SUPPRESS_COOKIE_PERSIST = 3;
			int INTERNET_OPTION_SUPPRESS_BEHAVIOR = 81;

			IntPtr option = Marshal.AllocHGlobal(sizeof(int));
			Marshal.WriteInt32(option, INTERNET_SUPPRESS_COOKIE_PERSIST);

			bool success = InternetSetOption(0, INTERNET_OPTION_SUPPRESS_BEHAVIOR, option, sizeof(int));

			Marshal.FreeHGlobal(option);

			if (!success) Console.Error.WriteLine("Could not set INTERNET_SUPPRESS_COOKIE_PERSIST on WebBrowser. Make sure you log out if the Tumblr account is incorrect.");
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			SupressCookiePersistence();
			Application.Run(new WeasylForm());
		}
	}
}
