namespace ArtworkUploader {
	partial class WeasylCredentialsForm {
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
			label1 = new System.Windows.Forms.Label();
			textBox1 = new System.Windows.Forms.TextBox();
			button1 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(46, 15);
			label1.TabIndex = 0;
			label1.Text = "API key";
			// 
			// textBox1
			// 
			textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBox1.Location = new System.Drawing.Point(12, 33);
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(310, 23);
			textBox1.TabIndex = 1;
			// 
			// button1
			// 
			button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			button1.Location = new System.Drawing.Point(166, 126);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 4;
			button1.Text = "Cancel";
			button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			button2.DialogResult = System.Windows.Forms.DialogResult.OK;
			button2.Location = new System.Drawing.Point(247, 126);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(75, 23);
			button2.TabIndex = 5;
			button2.Text = "OK";
			button2.UseVisualStyleBackColor = true;
			// 
			// WeasylCredentialsForm
			// 
			AcceptButton = button2;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = button1;
			ClientSize = new System.Drawing.Size(334, 161);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(textBox1);
			Controls.Add(label1);
			Name = "WeasylCredentialsForm";
			Text = "Add Weasyl account";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}