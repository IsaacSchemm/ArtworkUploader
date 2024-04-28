namespace ArtworkUploader {
	partial class JournalForm {
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
			btnPost = new System.Windows.Forms.Button();
			lblTitle = new System.Windows.Forms.Label();
			txtTitle = new System.Windows.Forms.TextBox();
			lblDescription = new System.Windows.Forms.Label();
			lblTags = new System.Windows.Forms.Label();
			txtTags = new System.Windows.Forms.TextBox();
			wbrDescription = new System.Windows.Forms.WebBrowser();
			checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			SuspendLayout();
			// 
			// btnPost
			// 
			btnPost.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnPost.Location = new System.Drawing.Point(497, 276);
			btnPost.Name = "btnPost";
			btnPost.Size = new System.Drawing.Size(75, 23);
			btnPost.TabIndex = 19;
			btnPost.Text = "Post";
			btnPost.UseVisualStyleBackColor = true;
			btnPost.Click += btnPost_Click;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new System.Drawing.Point(51, 15);
			lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(29, 15);
			lblTitle.TabIndex = 13;
			lblTitle.Text = "Title";
			// 
			// txtTitle
			// 
			txtTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			txtTitle.Location = new System.Drawing.Point(88, 12);
			txtTitle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new System.Drawing.Size(317, 23);
			txtTitle.TabIndex = 14;
			// 
			// lblDescription
			// 
			lblDescription.AutoSize = true;
			lblDescription.Location = new System.Drawing.Point(13, 41);
			lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new System.Drawing.Size(67, 30);
			lblDescription.TabIndex = 15;
			lblDescription.Text = "Description\r\n(HTML)";
			lblDescription.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblTags
			// 
			lblTags.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			lblTags.AutoSize = true;
			lblTags.Location = new System.Drawing.Point(50, 250);
			lblTags.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblTags.Name = "lblTags";
			lblTags.Size = new System.Drawing.Size(30, 15);
			lblTags.TabIndex = 16;
			lblTags.Text = "Tags";
			// 
			// txtTags
			// 
			txtTags.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			txtTags.Location = new System.Drawing.Point(88, 247);
			txtTags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtTags.Name = "txtTags";
			txtTags.Size = new System.Drawing.Size(317, 23);
			txtTags.TabIndex = 17;
			// 
			// wbrDescription
			// 
			wbrDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			wbrDescription.Location = new System.Drawing.Point(88, 41);
			wbrDescription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			wbrDescription.MinimumSize = new System.Drawing.Size(23, 23);
			wbrDescription.Name = "wbrDescription";
			wbrDescription.Size = new System.Drawing.Size(317, 200);
			wbrDescription.TabIndex = 18;
			// 
			// checkedListBox1
			// 
			checkedListBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			checkedListBox1.FormattingEnabled = true;
			checkedListBox1.IntegralHeight = false;
			checkedListBox1.Location = new System.Drawing.Point(412, 12);
			checkedListBox1.Name = "checkedListBox1";
			checkedListBox1.Size = new System.Drawing.Size(160, 258);
			checkedListBox1.TabIndex = 20;
			// 
			// JournalForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(584, 311);
			Controls.Add(checkedListBox1);
			Controls.Add(btnPost);
			Controls.Add(wbrDescription);
			Controls.Add(txtTags);
			Controls.Add(lblTags);
			Controls.Add(lblDescription);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Name = "JournalForm";
			Text = "Post Journal";
			Shown += JournalForm_Shown;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Button btnPost;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblTags;
		private System.Windows.Forms.TextBox txtTags;
		private System.Windows.Forms.WebBrowser wbrDescription;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
	}
}