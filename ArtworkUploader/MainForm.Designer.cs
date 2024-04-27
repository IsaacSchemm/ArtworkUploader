namespace ArtworkUploader {
	partial class MainForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			accountSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deviantArtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			furAffinityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			weasylToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			btnOpen = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			button1 = new System.Windows.Forms.Button();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			btnAbout = new System.Windows.Forms.Button();
			menuStrip1.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			SuspendLayout();
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			menuStrip1.Size = new System.Drawing.Size(284, 24);
			menuStrip1.TabIndex = 0;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, exitToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			openToolStripMenuItem.Name = "openToolStripMenuItem";
			openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			openToolStripMenuItem.Text = "&Open...";
			openToolStripMenuItem.Click += openToolStripMenuItem_Click;
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			exitToolStripMenuItem.Text = "E&xit";
			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
			// 
			// toolsToolStripMenuItem
			// 
			toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { accountSetupToolStripMenuItem });
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			toolsToolStripMenuItem.Text = "&Tools";
			// 
			// accountSetupToolStripMenuItem
			// 
			accountSetupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { deviantArtToolStripMenuItem, furAffinityToolStripMenuItem, weasylToolStripMenuItem });
			accountSetupToolStripMenuItem.Name = "accountSetupToolStripMenuItem";
			accountSetupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			accountSetupToolStripMenuItem.Text = "&Account Setup";
			// 
			// deviantArtToolStripMenuItem
			// 
			deviantArtToolStripMenuItem.Name = "deviantArtToolStripMenuItem";
			deviantArtToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			deviantArtToolStripMenuItem.Text = "&DeviantArt";
			deviantArtToolStripMenuItem.Click += deviantArtToolStripMenuItem_Click;
			// 
			// furAffinityToolStripMenuItem
			// 
			furAffinityToolStripMenuItem.Name = "furAffinityToolStripMenuItem";
			furAffinityToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			furAffinityToolStripMenuItem.Text = "Fur &Affinity";
			furAffinityToolStripMenuItem.Click += furAffinityToolStripMenuItem_Click;
			// 
			// weasylToolStripMenuItem
			// 
			weasylToolStripMenuItem.Name = "weasylToolStripMenuItem";
			weasylToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			weasylToolStripMenuItem.Text = "&Weasyl";
			weasylToolStripMenuItem.Click += weasylToolStripMenuItem_Click;
			// 
			// helpToolStripMenuItem
			// 
			helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutToolStripMenuItem });
			helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			aboutToolStripMenuItem.Text = "&About";
			aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
			// 
			// btnOpen
			// 
			btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
			btnOpen.Location = new System.Drawing.Point(53, 3);
			btnOpen.Name = "btnOpen";
			btnOpen.Size = new System.Drawing.Size(154, 24);
			btnOpen.TabIndex = 1;
			btnOpen.Text = "Open file...";
			btnOpen.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel1.Controls.Add(btnOpen, 1, 0);
			tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new System.Drawing.Size(260, 30);
			tableLayoutPanel1.TabIndex = 3;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 3;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
			tableLayoutPanel2.TabIndex = 0;
			// 
			// button1
			// 
			button1.Dock = System.Windows.Forms.DockStyle.Fill;
			button1.Location = new System.Drawing.Point(23, 3);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(154, 94);
			button1.TabIndex = 1;
			button1.Text = "Open file...";
			button1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel3.ColumnCount = 3;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel3.Controls.Add(btnAbout, 1, 0);
			tableLayoutPanel3.Location = new System.Drawing.Point(12, 63);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 1;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel3.Size = new System.Drawing.Size(260, 30);
			tableLayoutPanel3.TabIndex = 4;
			// 
			// btnAbout
			// 
			btnAbout.Dock = System.Windows.Forms.DockStyle.Fill;
			btnAbout.Location = new System.Drawing.Point(73, 3);
			btnAbout.Name = "btnAbout";
			btnAbout.Size = new System.Drawing.Size(114, 24);
			btnAbout.TabIndex = 1;
			btnAbout.Text = "About";
			btnAbout.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(284, 161);
			Controls.Add(tableLayoutPanel3);
			Controls.Add(tableLayoutPanel1);
			Controls.Add(menuStrip1);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MainMenuStrip = menuStrip1;
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "MainForm";
			Text = "Artwork Uploader 6.0";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel3.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem accountSetupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem furAffinityToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deviantArtToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem weasylToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button btnAbout;
	}
}

