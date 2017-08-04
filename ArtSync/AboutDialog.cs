﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtSync {
	public partial class AboutDialog : Form {
		public AboutDialog() {
			InitializeComponent();
		}

		private void lnkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(lnkWebsite.Text);
		}

		private void lnkTumblrSharp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start("http://tumblrsharp.codeplex.com/");
		}

		private void lnkJsonNET_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start("http://james.newtonking.com/json");
		}

		private void lnkHtmlAgilityPack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start("http://htmlagilitypack.codeplex.com/");
		}

		private void lnkHtmlAgilityPackLicense2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start("http://htmlagilitypack.codeplex.com/license");
		}
	}
}