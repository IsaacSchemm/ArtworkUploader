﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrosspostSharp3 {
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
