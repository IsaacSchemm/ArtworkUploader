﻿namespace CrosspostSharp {
	partial class SettingsDialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.groupWeasyl = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWeasylAPIKey = new System.Windows.Forms.TextBox();
            this.groupTumblr = new System.Windows.Forms.GroupBox();
            this.chkSidePadding = new System.Windows.Forms.CheckBox();
            this.btnTumblrSignIn = new System.Windows.Forms.Button();
            this.lblTokenStatus = new System.Windows.Forms.Label();
            this.lblToken = new System.Windows.Forms.Label();
            this.groupDefaults = new System.Windows.Forms.GroupBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.lblTags = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.lblBlogName = new System.Windows.Forms.Label();
            this.txtBlogName = new System.Windows.Forms.TextBox();
            this.lblTokenInfo = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupInkbunnyDest = new System.Windows.Forms.GroupBox();
            this.btnInkbunnySignIn = new System.Windows.Forms.Button();
            this.lblInkbunnyToken = new System.Windows.Forms.Label();
            this.lblInkbunnyTokenStatus = new System.Windows.Forms.Label();
            this.groupTwitter = new System.Windows.Forms.GroupBox();
            this.btnTwitterSignIn = new System.Windows.Forms.Button();
            this.lblTwitterTokenStatus = new System.Windows.Forms.Label();
            this.lblTwitterToken = new System.Windows.Forms.Label();
            this.groupDeviantArt = new System.Windows.Forms.GroupBox();
            this.btnDeviantArtSignIn = new System.Windows.Forms.Button();
            this.lblDeviantArtTokenStatus = new System.Windows.Forms.Label();
            this.lblDeviantArtToken = new System.Windows.Forms.Label();
            this.groupFurAffinity = new System.Windows.Forms.GroupBox();
            this.lblfurAffinityUsername2 = new System.Windows.Forms.Label();
            this.lblfurAffinityUsername1 = new System.Windows.Forms.Label();
            this.btnFurAffinitySignIn = new System.Windows.Forms.Button();
            this.groupFlickr = new System.Windows.Forms.GroupBox();
            this.lblFlickrTokenStatus = new System.Windows.Forms.Label();
            this.lblFlickrToken = new System.Windows.Forms.Label();
            this.btnFlickrSignIn = new System.Windows.Forms.Button();
            this.groupPixiv = new System.Windows.Forms.GroupBox();
            this.lblPixivUsername2 = new System.Windows.Forms.Label();
            this.lvlPixivUsername1 = new System.Windows.Forms.Label();
            this.btnPixivSignIn = new System.Windows.Forms.Button();
            this.groupFurryNetwork = new System.Windows.Forms.GroupBox();
            this.lblFurryNetworkTokenStatus = new System.Windows.Forms.Label();
            this.lblFurryNetworkToken = new System.Windows.Forms.Label();
            this.btnFurryNetworkSignIn = new System.Windows.Forms.Button();
            this.groupWeasyl.SuspendLayout();
            this.groupTumblr.SuspendLayout();
            this.groupDefaults.SuspendLayout();
            this.groupInkbunnyDest.SuspendLayout();
            this.groupTwitter.SuspendLayout();
            this.groupDeviantArt.SuspendLayout();
            this.groupFurAffinity.SuspendLayout();
            this.groupFlickr.SuspendLayout();
            this.groupPixiv.SuspendLayout();
            this.groupFurryNetwork.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupWeasyl
            // 
            this.groupWeasyl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupWeasyl.Controls.Add(this.label1);
            this.groupWeasyl.Controls.Add(this.txtWeasylAPIKey);
            this.groupWeasyl.Location = new System.Drawing.Point(318, 267);
            this.groupWeasyl.Name = "groupWeasyl";
            this.groupWeasyl.Size = new System.Drawing.Size(300, 45);
            this.groupWeasyl.TabIndex = 8;
            this.groupWeasyl.TabStop = false;
            this.groupWeasyl.Text = "Weasyl (read-only)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "API Key:";
            // 
            // txtWeasylAPIKey
            // 
            this.txtWeasylAPIKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWeasylAPIKey.Location = new System.Drawing.Point(83, 19);
            this.txtWeasylAPIKey.Name = "txtWeasylAPIKey";
            this.txtWeasylAPIKey.Size = new System.Drawing.Size(211, 20);
            this.txtWeasylAPIKey.TabIndex = 2;
            // 
            // groupTumblr
            // 
            this.groupTumblr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupTumblr.Controls.Add(this.chkSidePadding);
            this.groupTumblr.Controls.Add(this.btnTumblrSignIn);
            this.groupTumblr.Controls.Add(this.lblTokenStatus);
            this.groupTumblr.Controls.Add(this.lblToken);
            this.groupTumblr.Controls.Add(this.groupDefaults);
            this.groupTumblr.Controls.Add(this.lblBlogName);
            this.groupTumblr.Controls.Add(this.txtBlogName);
            this.groupTumblr.Location = new System.Drawing.Point(12, 12);
            this.groupTumblr.Name = "groupTumblr";
            this.groupTumblr.Size = new System.Drawing.Size(300, 197);
            this.groupTumblr.TabIndex = 0;
            this.groupTumblr.TabStop = false;
            this.groupTumblr.Text = "Tumblr";
            // 
            // chkSidePadding
            // 
            this.chkSidePadding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSidePadding.AutoEllipsis = true;
            this.chkSidePadding.Location = new System.Drawing.Point(9, 71);
            this.chkSidePadding.Name = "chkSidePadding";
            this.chkSidePadding.Size = new System.Drawing.Size(285, 17);
            this.chkSidePadding.TabIndex = 5;
            this.chkSidePadding.Text = "Add padding to make tall images square";
            this.chkSidePadding.UseVisualStyleBackColor = true;
            // 
            // btnTumblrSignIn
            // 
            this.btnTumblrSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTumblrSignIn.Location = new System.Drawing.Point(219, 19);
            this.btnTumblrSignIn.Name = "btnTumblrSignIn";
            this.btnTumblrSignIn.Size = new System.Drawing.Size(75, 20);
            this.btnTumblrSignIn.TabIndex = 2;
            this.btnTumblrSignIn.Text = "Sign in";
            this.btnTumblrSignIn.UseVisualStyleBackColor = true;
            this.btnTumblrSignIn.Click += new System.EventHandler(this.btnTumblrSignIn_Click);
            // 
            // lblTokenStatus
            // 
            this.lblTokenStatus.AutoEllipsis = true;
            this.lblTokenStatus.Location = new System.Drawing.Point(80, 23);
            this.lblTokenStatus.Name = "lblTokenStatus";
            this.lblTokenStatus.Size = new System.Drawing.Size(133, 13);
            this.lblTokenStatus.TabIndex = 1;
            this.lblTokenStatus.Text = "Not signed in";
            // 
            // lblToken
            // 
            this.lblToken.AutoSize = true;
            this.lblToken.Location = new System.Drawing.Point(6, 23);
            this.lblToken.Name = "lblToken";
            this.lblToken.Size = new System.Drawing.Size(41, 13);
            this.lblToken.TabIndex = 0;
            this.lblToken.Text = "Token:";
            // 
            // groupDefaults
            // 
            this.groupDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDefaults.Controls.Add(this.txtHeader);
            this.groupDefaults.Controls.Add(this.txtTags);
            this.groupDefaults.Controls.Add(this.lblTags);
            this.groupDefaults.Controls.Add(this.lblFooter);
            this.groupDefaults.Controls.Add(this.lblHeader);
            this.groupDefaults.Controls.Add(this.txtFooter);
            this.groupDefaults.Location = new System.Drawing.Point(6, 94);
            this.groupDefaults.Name = "groupDefaults";
            this.groupDefaults.Size = new System.Drawing.Size(288, 97);
            this.groupDefaults.TabIndex = 0;
            this.groupDefaults.TabStop = false;
            this.groupDefaults.Text = "Defaults";
            // 
            // txtHeader
            // 
            this.txtHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeader.Location = new System.Drawing.Point(83, 19);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(199, 20);
            this.txtHeader.TabIndex = 1;
            // 
            // txtTags
            // 
            this.txtTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTags.Location = new System.Drawing.Point(83, 71);
            this.txtTags.Name = "txtTags";
            this.txtTags.Size = new System.Drawing.Size(199, 20);
            this.txtTags.TabIndex = 5;
            // 
            // lblTags
            // 
            this.lblTags.AutoSize = true;
            this.lblTags.Location = new System.Drawing.Point(6, 74);
            this.lblTags.Name = "lblTags";
            this.lblTags.Size = new System.Drawing.Size(71, 13);
            this.lblTags.TabIndex = 4;
            this.lblTags.Text = "Default Tags:";
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new System.Drawing.Point(6, 48);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(40, 13);
            this.lblFooter.TabIndex = 2;
            this.lblFooter.Text = "Footer:";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(6, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(45, 13);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Header:";
            // 
            // txtFooter
            // 
            this.txtFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFooter.Location = new System.Drawing.Point(83, 45);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(199, 20);
            this.txtFooter.TabIndex = 3;
            // 
            // lblBlogName
            // 
            this.lblBlogName.AutoSize = true;
            this.lblBlogName.Location = new System.Drawing.Point(6, 48);
            this.lblBlogName.Name = "lblBlogName";
            this.lblBlogName.Size = new System.Drawing.Size(60, 13);
            this.lblBlogName.TabIndex = 3;
            this.lblBlogName.Text = "Blog name:";
            // 
            // txtBlogName
            // 
            this.txtBlogName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBlogName.Location = new System.Drawing.Point(83, 45);
            this.txtBlogName.Name = "txtBlogName";
            this.txtBlogName.Size = new System.Drawing.Size(208, 20);
            this.txtBlogName.TabIndex = 4;
            // 
            // lblTokenInfo
            // 
            this.lblTokenInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTokenInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTokenInfo.Location = new System.Drawing.Point(12, 315);
            this.lblTokenInfo.Name = "lblTokenInfo";
            this.lblTokenInfo.Size = new System.Drawing.Size(606, 58);
            this.lblTokenInfo.TabIndex = 9;
            this.lblTokenInfo.Text = resources.GetString("lblTokenInfo.Text");
            this.lblTokenInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(462, 376);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(543, 376);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupInkbunnyDest
            // 
            this.groupInkbunnyDest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupInkbunnyDest.Controls.Add(this.btnInkbunnySignIn);
            this.groupInkbunnyDest.Controls.Add(this.lblInkbunnyToken);
            this.groupInkbunnyDest.Controls.Add(this.lblInkbunnyTokenStatus);
            this.groupInkbunnyDest.Location = new System.Drawing.Point(318, 165);
            this.groupInkbunnyDest.Name = "groupInkbunnyDest";
            this.groupInkbunnyDest.Size = new System.Drawing.Size(300, 45);
            this.groupInkbunnyDest.TabIndex = 6;
            this.groupInkbunnyDest.TabStop = false;
            this.groupInkbunnyDest.Text = "Inkbunny";
            // 
            // btnInkbunnySignIn
            // 
            this.btnInkbunnySignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInkbunnySignIn.Location = new System.Drawing.Point(219, 19);
            this.btnInkbunnySignIn.Name = "btnInkbunnySignIn";
            this.btnInkbunnySignIn.Size = new System.Drawing.Size(75, 20);
            this.btnInkbunnySignIn.TabIndex = 8;
            this.btnInkbunnySignIn.Text = "Sign in";
            this.btnInkbunnySignIn.UseVisualStyleBackColor = true;
            this.btnInkbunnySignIn.Click += new System.EventHandler(this.btnInkbunnySignIn_Click);
            // 
            // lblInkbunnyToken
            // 
            this.lblInkbunnyToken.AutoSize = true;
            this.lblInkbunnyToken.Location = new System.Drawing.Point(6, 23);
            this.lblInkbunnyToken.Name = "lblInkbunnyToken";
            this.lblInkbunnyToken.Size = new System.Drawing.Size(41, 13);
            this.lblInkbunnyToken.TabIndex = 6;
            this.lblInkbunnyToken.Text = "Token:";
            // 
            // lblInkbunnyTokenStatus
            // 
            this.lblInkbunnyTokenStatus.AutoEllipsis = true;
            this.lblInkbunnyTokenStatus.Location = new System.Drawing.Point(80, 23);
            this.lblInkbunnyTokenStatus.Name = "lblInkbunnyTokenStatus";
            this.lblInkbunnyTokenStatus.Size = new System.Drawing.Size(133, 13);
            this.lblInkbunnyTokenStatus.TabIndex = 7;
            this.lblInkbunnyTokenStatus.Text = "Not signed in";
            // 
            // groupTwitter
            // 
            this.groupTwitter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupTwitter.Controls.Add(this.btnTwitterSignIn);
            this.groupTwitter.Controls.Add(this.lblTwitterTokenStatus);
            this.groupTwitter.Controls.Add(this.lblTwitterToken);
            this.groupTwitter.Location = new System.Drawing.Point(12, 215);
            this.groupTwitter.Name = "groupTwitter";
            this.groupTwitter.Size = new System.Drawing.Size(300, 45);
            this.groupTwitter.TabIndex = 1;
            this.groupTwitter.TabStop = false;
            this.groupTwitter.Text = "Twitter";
            // 
            // btnTwitterSignIn
            // 
            this.btnTwitterSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTwitterSignIn.Location = new System.Drawing.Point(219, 19);
            this.btnTwitterSignIn.Name = "btnTwitterSignIn";
            this.btnTwitterSignIn.Size = new System.Drawing.Size(75, 20);
            this.btnTwitterSignIn.TabIndex = 2;
            this.btnTwitterSignIn.Text = "Sign in";
            this.btnTwitterSignIn.UseVisualStyleBackColor = true;
            this.btnTwitterSignIn.Click += new System.EventHandler(this.btnTwitterSignIn_Click);
            // 
            // lblTwitterTokenStatus
            // 
            this.lblTwitterTokenStatus.AutoEllipsis = true;
            this.lblTwitterTokenStatus.Location = new System.Drawing.Point(80, 23);
            this.lblTwitterTokenStatus.Name = "lblTwitterTokenStatus";
            this.lblTwitterTokenStatus.Size = new System.Drawing.Size(133, 13);
            this.lblTwitterTokenStatus.TabIndex = 1;
            this.lblTwitterTokenStatus.Text = "Not signed in";
            // 
            // lblTwitterToken
            // 
            this.lblTwitterToken.AutoSize = true;
            this.lblTwitterToken.Location = new System.Drawing.Point(6, 23);
            this.lblTwitterToken.Name = "lblTwitterToken";
            this.lblTwitterToken.Size = new System.Drawing.Size(41, 13);
            this.lblTwitterToken.TabIndex = 0;
            this.lblTwitterToken.Text = "Token:";
            // 
            // groupDeviantArt
            // 
            this.groupDeviantArt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDeviantArt.Controls.Add(this.btnDeviantArtSignIn);
            this.groupDeviantArt.Controls.Add(this.lblDeviantArtTokenStatus);
            this.groupDeviantArt.Controls.Add(this.lblDeviantArtToken);
            this.groupDeviantArt.Location = new System.Drawing.Point(318, 12);
            this.groupDeviantArt.Name = "groupDeviantArt";
            this.groupDeviantArt.Size = new System.Drawing.Size(300, 45);
            this.groupDeviantArt.TabIndex = 3;
            this.groupDeviantArt.TabStop = false;
            this.groupDeviantArt.Text = "DeviantArt";
            // 
            // btnDeviantArtSignIn
            // 
            this.btnDeviantArtSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeviantArtSignIn.Location = new System.Drawing.Point(219, 19);
            this.btnDeviantArtSignIn.Name = "btnDeviantArtSignIn";
            this.btnDeviantArtSignIn.Size = new System.Drawing.Size(75, 20);
            this.btnDeviantArtSignIn.TabIndex = 6;
            this.btnDeviantArtSignIn.Text = "Sign in";
            this.btnDeviantArtSignIn.UseVisualStyleBackColor = true;
            this.btnDeviantArtSignIn.Click += new System.EventHandler(this.btnDeviantArtSignIn_Click);
            // 
            // lblDeviantArtTokenStatus
            // 
            this.lblDeviantArtTokenStatus.AutoEllipsis = true;
            this.lblDeviantArtTokenStatus.Location = new System.Drawing.Point(80, 23);
            this.lblDeviantArtTokenStatus.Name = "lblDeviantArtTokenStatus";
            this.lblDeviantArtTokenStatus.Size = new System.Drawing.Size(133, 13);
            this.lblDeviantArtTokenStatus.TabIndex = 5;
            this.lblDeviantArtTokenStatus.Text = "Not signed in";
            // 
            // lblDeviantArtToken
            // 
            this.lblDeviantArtToken.AutoSize = true;
            this.lblDeviantArtToken.Location = new System.Drawing.Point(6, 23);
            this.lblDeviantArtToken.Name = "lblDeviantArtToken";
            this.lblDeviantArtToken.Size = new System.Drawing.Size(41, 13);
            this.lblDeviantArtToken.TabIndex = 4;
            this.lblDeviantArtToken.Text = "Token:";
            // 
            // groupFurAffinity
            // 
            this.groupFurAffinity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupFurAffinity.Controls.Add(this.lblfurAffinityUsername2);
            this.groupFurAffinity.Controls.Add(this.lblfurAffinityUsername1);
            this.groupFurAffinity.Controls.Add(this.btnFurAffinitySignIn);
            this.groupFurAffinity.Location = new System.Drawing.Point(318, 63);
            this.groupFurAffinity.Name = "groupFurAffinity";
            this.groupFurAffinity.Size = new System.Drawing.Size(300, 45);
            this.groupFurAffinity.TabIndex = 4;
            this.groupFurAffinity.TabStop = false;
            this.groupFurAffinity.Text = "FurAffinity (read-only)";
            // 
            // lblfurAffinityUsername2
            // 
            this.lblfurAffinityUsername2.AutoEllipsis = true;
            this.lblfurAffinityUsername2.Location = new System.Drawing.Point(80, 23);
            this.lblfurAffinityUsername2.Name = "lblfurAffinityUsername2";
            this.lblfurAffinityUsername2.Size = new System.Drawing.Size(133, 13);
            this.lblfurAffinityUsername2.TabIndex = 8;
            // 
            // lblfurAffinityUsername1
            // 
            this.lblfurAffinityUsername1.AutoSize = true;
            this.lblfurAffinityUsername1.Location = new System.Drawing.Point(6, 23);
            this.lblfurAffinityUsername1.Name = "lblfurAffinityUsername1";
            this.lblfurAffinityUsername1.Size = new System.Drawing.Size(58, 13);
            this.lblfurAffinityUsername1.TabIndex = 7;
            this.lblfurAffinityUsername1.Text = "Username:";
            // 
            // btnFurAffinitySignIn
            // 
            this.btnFurAffinitySignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFurAffinitySignIn.Location = new System.Drawing.Point(219, 19);
            this.btnFurAffinitySignIn.Name = "btnFurAffinitySignIn";
            this.btnFurAffinitySignIn.Size = new System.Drawing.Size(75, 20);
            this.btnFurAffinitySignIn.TabIndex = 6;
            this.btnFurAffinitySignIn.Text = "Sign in";
            this.btnFurAffinitySignIn.UseVisualStyleBackColor = true;
            this.btnFurAffinitySignIn.Click += new System.EventHandler(this.btnFurAffinitySignIn_Click);
            // 
            // groupFlickr
            // 
            this.groupFlickr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupFlickr.Controls.Add(this.lblFlickrTokenStatus);
            this.groupFlickr.Controls.Add(this.lblFlickrToken);
            this.groupFlickr.Controls.Add(this.btnFlickrSignIn);
            this.groupFlickr.Location = new System.Drawing.Point(12, 266);
            this.groupFlickr.Name = "groupFlickr";
            this.groupFlickr.Size = new System.Drawing.Size(300, 45);
            this.groupFlickr.TabIndex = 2;
            this.groupFlickr.TabStop = false;
            this.groupFlickr.Text = "Flickr";
            // 
            // lblFlickrTokenStatus
            // 
            this.lblFlickrTokenStatus.AutoEllipsis = true;
            this.lblFlickrTokenStatus.Location = new System.Drawing.Point(80, 23);
            this.lblFlickrTokenStatus.Name = "lblFlickrTokenStatus";
            this.lblFlickrTokenStatus.Size = new System.Drawing.Size(133, 13);
            this.lblFlickrTokenStatus.TabIndex = 8;
            // 
            // lblFlickrToken
            // 
            this.lblFlickrToken.AutoSize = true;
            this.lblFlickrToken.Location = new System.Drawing.Point(6, 23);
            this.lblFlickrToken.Name = "lblFlickrToken";
            this.lblFlickrToken.Size = new System.Drawing.Size(41, 13);
            this.lblFlickrToken.TabIndex = 7;
            this.lblFlickrToken.Text = "Token:";
            // 
            // btnFlickrSignIn
            // 
            this.btnFlickrSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlickrSignIn.Location = new System.Drawing.Point(219, 19);
            this.btnFlickrSignIn.Name = "btnFlickrSignIn";
            this.btnFlickrSignIn.Size = new System.Drawing.Size(75, 20);
            this.btnFlickrSignIn.TabIndex = 6;
            this.btnFlickrSignIn.Text = "Sign in";
            this.btnFlickrSignIn.UseVisualStyleBackColor = true;
            this.btnFlickrSignIn.Click += new System.EventHandler(this.btnFlickrSignIn_Click);
            // 
            // groupPixiv
            // 
            this.groupPixiv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPixiv.Controls.Add(this.lblPixivUsername2);
            this.groupPixiv.Controls.Add(this.lvlPixivUsername1);
            this.groupPixiv.Controls.Add(this.btnPixivSignIn);
            this.groupPixiv.Location = new System.Drawing.Point(318, 216);
            this.groupPixiv.Name = "groupPixiv";
            this.groupPixiv.Size = new System.Drawing.Size(300, 45);
            this.groupPixiv.TabIndex = 7;
            this.groupPixiv.TabStop = false;
            this.groupPixiv.Text = "Pixiv (read-only)";
            // 
            // lblPixivUsername2
            // 
            this.lblPixivUsername2.AutoEllipsis = true;
            this.lblPixivUsername2.Location = new System.Drawing.Point(80, 23);
            this.lblPixivUsername2.Name = "lblPixivUsername2";
            this.lblPixivUsername2.Size = new System.Drawing.Size(133, 13);
            this.lblPixivUsername2.TabIndex = 8;
            // 
            // lvlPixivUsername1
            // 
            this.lvlPixivUsername1.AutoSize = true;
            this.lvlPixivUsername1.Location = new System.Drawing.Point(6, 23);
            this.lvlPixivUsername1.Name = "lvlPixivUsername1";
            this.lvlPixivUsername1.Size = new System.Drawing.Size(58, 13);
            this.lvlPixivUsername1.TabIndex = 7;
            this.lvlPixivUsername1.Text = "Username:";
            // 
            // btnPixivSignIn
            // 
            this.btnPixivSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPixivSignIn.Location = new System.Drawing.Point(219, 19);
            this.btnPixivSignIn.Name = "btnPixivSignIn";
            this.btnPixivSignIn.Size = new System.Drawing.Size(75, 20);
            this.btnPixivSignIn.TabIndex = 6;
            this.btnPixivSignIn.Text = "Sign in";
            this.btnPixivSignIn.UseVisualStyleBackColor = true;
            this.btnPixivSignIn.Click += new System.EventHandler(this.btnPixivSignIn_Click);
            // 
            // groupFurryNetwork
            // 
            this.groupFurryNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupFurryNetwork.Controls.Add(this.lblFurryNetworkTokenStatus);
            this.groupFurryNetwork.Controls.Add(this.lblFurryNetworkToken);
            this.groupFurryNetwork.Controls.Add(this.btnFurryNetworkSignIn);
            this.groupFurryNetwork.Location = new System.Drawing.Point(318, 114);
            this.groupFurryNetwork.Name = "groupFurryNetwork";
            this.groupFurryNetwork.Size = new System.Drawing.Size(300, 45);
            this.groupFurryNetwork.TabIndex = 5;
            this.groupFurryNetwork.TabStop = false;
            this.groupFurryNetwork.Text = "Furry Network";
            // 
            // lblFurryNetworkTokenStatus
            // 
            this.lblFurryNetworkTokenStatus.AutoEllipsis = true;
            this.lblFurryNetworkTokenStatus.Location = new System.Drawing.Point(80, 23);
            this.lblFurryNetworkTokenStatus.Name = "lblFurryNetworkTokenStatus";
            this.lblFurryNetworkTokenStatus.Size = new System.Drawing.Size(133, 13);
            this.lblFurryNetworkTokenStatus.TabIndex = 8;
            // 
            // lblFurryNetworkToken
            // 
            this.lblFurryNetworkToken.AutoSize = true;
            this.lblFurryNetworkToken.Location = new System.Drawing.Point(6, 23);
            this.lblFurryNetworkToken.Name = "lblFurryNetworkToken";
            this.lblFurryNetworkToken.Size = new System.Drawing.Size(41, 13);
            this.lblFurryNetworkToken.TabIndex = 7;
            this.lblFurryNetworkToken.Text = "Token:";
            // 
            // btnFurryNetworkSignIn
            // 
            this.btnFurryNetworkSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFurryNetworkSignIn.Location = new System.Drawing.Point(219, 19);
            this.btnFurryNetworkSignIn.Name = "btnFurryNetworkSignIn";
            this.btnFurryNetworkSignIn.Size = new System.Drawing.Size(75, 20);
            this.btnFurryNetworkSignIn.TabIndex = 6;
            this.btnFurryNetworkSignIn.Text = "Sign in";
            this.btnFurryNetworkSignIn.UseVisualStyleBackColor = true;
            this.btnFurryNetworkSignIn.Click += new System.EventHandler(this.btnFurryNetworkSignIn_Click);
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(630, 411);
            this.Controls.Add(this.groupFurryNetwork);
            this.Controls.Add(this.groupPixiv);
            this.Controls.Add(this.groupFlickr);
            this.Controls.Add(this.groupFurAffinity);
            this.Controls.Add(this.groupDeviantArt);
            this.Controls.Add(this.groupTwitter);
            this.Controls.Add(this.lblTokenInfo);
            this.Controls.Add(this.groupInkbunnyDest);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupTumblr);
            this.Controls.Add(this.groupWeasyl);
            this.Name = "SettingsDialog";
            this.Text = "Settings";
            this.groupWeasyl.ResumeLayout(false);
            this.groupWeasyl.PerformLayout();
            this.groupTumblr.ResumeLayout(false);
            this.groupTumblr.PerformLayout();
            this.groupDefaults.ResumeLayout(false);
            this.groupDefaults.PerformLayout();
            this.groupInkbunnyDest.ResumeLayout(false);
            this.groupInkbunnyDest.PerformLayout();
            this.groupTwitter.ResumeLayout(false);
            this.groupTwitter.PerformLayout();
            this.groupDeviantArt.ResumeLayout(false);
            this.groupDeviantArt.PerformLayout();
            this.groupFurAffinity.ResumeLayout(false);
            this.groupFurAffinity.PerformLayout();
            this.groupFlickr.ResumeLayout(false);
            this.groupFlickr.PerformLayout();
            this.groupPixiv.ResumeLayout(false);
            this.groupPixiv.PerformLayout();
            this.groupFurryNetwork.ResumeLayout(false);
            this.groupFurryNetwork.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupWeasyl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtWeasylAPIKey;
		private System.Windows.Forms.GroupBox groupTumblr;
		private System.Windows.Forms.Label lblBlogName;
		private System.Windows.Forms.TextBox txtBlogName;
		private System.Windows.Forms.Label lblToken;
		private System.Windows.Forms.Label lblTokenStatus;
		private System.Windows.Forms.Button btnTumblrSignIn;
		private System.Windows.Forms.Label lblTokenInfo;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkSidePadding;
		private System.Windows.Forms.GroupBox groupInkbunnyDest;
		private System.Windows.Forms.GroupBox groupTwitter;
		private System.Windows.Forms.Button btnTwitterSignIn;
		private System.Windows.Forms.Label lblTwitterTokenStatus;
		private System.Windows.Forms.Label lblTwitterToken;
        private System.Windows.Forms.GroupBox groupDeviantArt;
        private System.Windows.Forms.Button btnDeviantArtSignIn;
        private System.Windows.Forms.Label lblDeviantArtTokenStatus;
        private System.Windows.Forms.Label lblDeviantArtToken;
        private System.Windows.Forms.Button btnInkbunnySignIn;
        private System.Windows.Forms.Label lblInkbunnyToken;
        private System.Windows.Forms.Label lblInkbunnyTokenStatus;
        private System.Windows.Forms.GroupBox groupDefaults;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtTags;
        private System.Windows.Forms.Label lblTags;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.GroupBox groupFurAffinity;
        private System.Windows.Forms.Button btnFurAffinitySignIn;
        private System.Windows.Forms.Label lblfurAffinityUsername2;
        private System.Windows.Forms.Label lblfurAffinityUsername1;
        private System.Windows.Forms.GroupBox groupFlickr;
        private System.Windows.Forms.Label lblFlickrTokenStatus;
        private System.Windows.Forms.Label lblFlickrToken;
        private System.Windows.Forms.Button btnFlickrSignIn;
		private System.Windows.Forms.GroupBox groupPixiv;
		private System.Windows.Forms.Label lblPixivUsername2;
		private System.Windows.Forms.Label lvlPixivUsername1;
		private System.Windows.Forms.Button btnPixivSignIn;
		private System.Windows.Forms.GroupBox groupFurryNetwork;
		private System.Windows.Forms.Label lblFurryNetworkTokenStatus;
		private System.Windows.Forms.Label lblFurryNetworkToken;
		private System.Windows.Forms.Button btnFurryNetworkSignIn;
	}
}