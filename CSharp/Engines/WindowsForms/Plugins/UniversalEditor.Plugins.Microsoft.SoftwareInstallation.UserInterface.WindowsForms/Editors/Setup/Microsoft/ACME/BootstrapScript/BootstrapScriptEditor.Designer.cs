namespace UniversalEditor.Editors.Setup.Microsoft.ACME.BootstrapScript
{
	partial class BootstrapScriptEditor
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.fraGeneral = new System.Windows.Forms.GroupBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.chkRequire31 = new System.Windows.Forms.CheckBox();
			this.txtTemporaryDirectorySize = new System.Windows.Forms.NumericUpDown();
			this.txtTemporaryDirectoryName = new System.Windows.Forms.TextBox();
			this.txtRequire31 = new System.Windows.Forms.TextBox();
			this.txtCommandLine = new System.Windows.Forms.TextBox();
			this.txtWindowClassName = new System.Windows.Forms.TextBox();
			this.txtWindowMessage = new System.Windows.Forms.TextBox();
			this.lblRequire31 = new System.Windows.Forms.Label();
			this.lblTemporaryDirectorySize = new System.Windows.Forms.Label();
			this.lblCommandLine = new System.Windows.Forms.Label();
			this.lblWindowClassName = new System.Windows.Forms.Label();
			this.lblTemporaryDirectoryName = new System.Windows.Forms.Label();
			this.lblWindowMessage = new System.Windows.Forms.Label();
			this.txtWindowTitle = new System.Windows.Forms.TextBox();
			this.lblWindowTitle = new System.Windows.Forms.Label();
			this.fraFiles = new System.Windows.Forms.GroupBox();
			this.cmdFilesClear = new System.Windows.Forms.Button();
			this.cmdFilesRemove = new System.Windows.Forms.Button();
			this.cmdFilesModify = new System.Windows.Forms.Button();
			this.cmdFilesAdd = new System.Windows.Forms.Button();
			this.lvFiles = new System.Windows.Forms.ListView();
			this.chSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chDestination = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fraGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTemporaryDirectorySize)).BeginInit();
			this.fraFiles.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraGeneral
			// 
			this.fraGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraGeneral.Controls.Add(this.pictureBox1);
			this.fraGeneral.Controls.Add(this.chkRequire31);
			this.fraGeneral.Controls.Add(this.txtTemporaryDirectorySize);
			this.fraGeneral.Controls.Add(this.txtTemporaryDirectoryName);
			this.fraGeneral.Controls.Add(this.txtRequire31);
			this.fraGeneral.Controls.Add(this.txtCommandLine);
			this.fraGeneral.Controls.Add(this.txtWindowClassName);
			this.fraGeneral.Controls.Add(this.txtWindowMessage);
			this.fraGeneral.Controls.Add(this.lblRequire31);
			this.fraGeneral.Controls.Add(this.lblTemporaryDirectorySize);
			this.fraGeneral.Controls.Add(this.lblCommandLine);
			this.fraGeneral.Controls.Add(this.lblWindowClassName);
			this.fraGeneral.Controls.Add(this.lblTemporaryDirectoryName);
			this.fraGeneral.Controls.Add(this.lblWindowMessage);
			this.fraGeneral.Controls.Add(this.txtWindowTitle);
			this.fraGeneral.Controls.Add(this.lblWindowTitle);
			this.fraGeneral.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraGeneral.Location = new System.Drawing.Point(4, 4);
			this.fraGeneral.Name = "fraGeneral";
			this.fraGeneral.Size = new System.Drawing.Size(479, 208);
			this.fraGeneral.TabIndex = 0;
			this.fraGeneral.TabStop = false;
			this.fraGeneral.Text = "General";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::UniversalEditor.Properties.Resources.setup;
			this.pictureBox1.Location = new System.Drawing.Point(13, 26);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// chkRequire31
			// 
			this.chkRequire31.AutoSize = true;
			this.chkRequire31.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkRequire31.Location = new System.Drawing.Point(19, 149);
			this.chkRequire31.Name = "chkRequire31";
			this.chkRequire31.Size = new System.Drawing.Size(134, 18);
			this.chkRequire31.TabIndex = 3;
			this.chkRequire31.Text = "&Require Windows 3.1";
			this.chkRequire31.UseVisualStyleBackColor = true;
			this.chkRequire31.CheckedChanged += new System.EventHandler(this.chkRequire31_CheckedChanged);
			// 
			// txtTemporaryDirectorySize
			// 
			this.txtTemporaryDirectorySize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTemporaryDirectorySize.Location = new System.Drawing.Point(405, 97);
			this.txtTemporaryDirectorySize.Maximum = new decimal(new int[] {
            65531,
            0,
            0,
            0});
			this.txtTemporaryDirectorySize.Name = "txtTemporaryDirectorySize";
			this.txtTemporaryDirectorySize.Size = new System.Drawing.Size(68, 20);
			this.txtTemporaryDirectorySize.TabIndex = 2;
			this.txtTemporaryDirectorySize.Value = new decimal(new int[] {
            3200,
            0,
            0,
            0});
			this.txtTemporaryDirectorySize.Validated += new System.EventHandler(this.txtTemporaryDirectorySize_Validated);
			// 
			// txtTemporaryDirectoryName
			// 
			this.txtTemporaryDirectoryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTemporaryDirectoryName.Location = new System.Drawing.Point(154, 96);
			this.txtTemporaryDirectoryName.Name = "txtTemporaryDirectoryName";
			this.txtTemporaryDirectoryName.Size = new System.Drawing.Size(209, 20);
			this.txtTemporaryDirectoryName.TabIndex = 1;
			this.txtTemporaryDirectoryName.Text = "~msstfqf.t";
			this.txtTemporaryDirectoryName.Validated += new System.EventHandler(this.txtTemporaryDirectoryName_Validated);
			// 
			// txtRequire31
			// 
			this.txtRequire31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtRequire31.Location = new System.Drawing.Point(154, 172);
			this.txtRequire31.Name = "txtRequire31";
			this.txtRequire31.ReadOnly = true;
			this.txtRequire31.Size = new System.Drawing.Size(319, 20);
			this.txtRequire31.TabIndex = 1;
			this.txtRequire31.Text = "This application requires a newer version of Microsoft Windows.";
			this.txtRequire31.Validated += new System.EventHandler(this.txtRequire31_Validated);
			// 
			// txtCommandLine
			// 
			this.txtCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCommandLine.Location = new System.Drawing.Point(154, 123);
			this.txtCommandLine.Name = "txtCommandLine";
			this.txtCommandLine.Size = new System.Drawing.Size(319, 20);
			this.txtCommandLine.TabIndex = 1;
			this.txtCommandLine.Text = "acmsetup /T setup.stf";
			this.txtCommandLine.Validated += new System.EventHandler(this.txtCommandLine_Validated);
			// 
			// txtWindowClassName
			// 
			this.txtWindowClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtWindowClassName.Location = new System.Drawing.Point(154, 71);
			this.txtWindowClassName.Name = "txtWindowClassName";
			this.txtWindowClassName.Size = new System.Drawing.Size(319, 20);
			this.txtWindowClassName.TabIndex = 1;
			this.txtWindowClassName.Text = "Stuff-Shell";
			this.txtWindowClassName.Validated += new System.EventHandler(this.txtWindowClassName_Validated);
			// 
			// txtWindowMessage
			// 
			this.txtWindowMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtWindowMessage.Location = new System.Drawing.Point(154, 45);
			this.txtWindowMessage.Name = "txtWindowMessage";
			this.txtWindowMessage.Size = new System.Drawing.Size(319, 20);
			this.txtWindowMessage.TabIndex = 1;
			this.txtWindowMessage.Text = "Initializing Setup...";
			this.txtWindowMessage.Validated += new System.EventHandler(this.txtWindowMessage_Validated);
			// 
			// lblRequire31
			// 
			this.lblRequire31.AutoSize = true;
			this.lblRequire31.Enabled = false;
			this.lblRequire31.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblRequire31.Location = new System.Drawing.Point(51, 175);
			this.lblRequire31.Name = "lblRequire31";
			this.lblRequire31.Size = new System.Drawing.Size(77, 13);
			this.lblRequire31.TabIndex = 0;
			this.lblRequire31.Text = "Error &message:";
			// 
			// lblTemporaryDirectorySize
			// 
			this.lblTemporaryDirectorySize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTemporaryDirectorySize.AutoSize = true;
			this.lblTemporaryDirectorySize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTemporaryDirectorySize.Location = new System.Drawing.Point(369, 99);
			this.lblTemporaryDirectorySize.Name = "lblTemporaryDirectorySize";
			this.lblTemporaryDirectorySize.Size = new System.Drawing.Size(30, 13);
			this.lblTemporaryDirectorySize.TabIndex = 0;
			this.lblTemporaryDirectorySize.Text = "&Size:";
			// 
			// lblCommandLine
			// 
			this.lblCommandLine.AutoSize = true;
			this.lblCommandLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCommandLine.Location = new System.Drawing.Point(16, 126);
			this.lblCommandLine.Name = "lblCommandLine";
			this.lblCommandLine.Size = new System.Drawing.Size(76, 13);
			this.lblCommandLine.TabIndex = 0;
			this.lblCommandLine.Text = "&Command line:";
			// 
			// lblWindowClassName
			// 
			this.lblWindowClassName.AutoSize = true;
			this.lblWindowClassName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblWindowClassName.Location = new System.Drawing.Point(16, 74);
			this.lblWindowClassName.Name = "lblWindowClassName";
			this.lblWindowClassName.Size = new System.Drawing.Size(105, 13);
			this.lblWindowClassName.TabIndex = 0;
			this.lblWindowClassName.Text = "Window class &name:";
			// 
			// lblTemporaryDirectoryName
			// 
			this.lblTemporaryDirectoryName.AutoSize = true;
			this.lblTemporaryDirectoryName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTemporaryDirectoryName.Location = new System.Drawing.Point(16, 99);
			this.lblTemporaryDirectoryName.Name = "lblTemporaryDirectoryName";
			this.lblTemporaryDirectoryName.Size = new System.Drawing.Size(132, 13);
			this.lblTemporaryDirectoryName.TabIndex = 0;
			this.lblTemporaryDirectoryName.Text = "&Temporary directory name:";
			// 
			// lblWindowMessage
			// 
			this.lblWindowMessage.AutoSize = true;
			this.lblWindowMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblWindowMessage.Location = new System.Drawing.Point(53, 48);
			this.lblWindowMessage.Name = "lblWindowMessage";
			this.lblWindowMessage.Size = new System.Drawing.Size(94, 13);
			this.lblWindowMessage.TabIndex = 0;
			this.lblWindowMessage.Text = "Window &message:";
			// 
			// txtWindowTitle
			// 
			this.txtWindowTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtWindowTitle.Location = new System.Drawing.Point(154, 19);
			this.txtWindowTitle.Name = "txtWindowTitle";
			this.txtWindowTitle.Size = new System.Drawing.Size(319, 20);
			this.txtWindowTitle.TabIndex = 1;
			this.txtWindowTitle.Text = "Your Application Name Setup";
			this.txtWindowTitle.Validated += new System.EventHandler(this.txtWindowTitle_Validated);
			// 
			// lblWindowTitle
			// 
			this.lblWindowTitle.AutoSize = true;
			this.lblWindowTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblWindowTitle.Location = new System.Drawing.Point(53, 22);
			this.lblWindowTitle.Name = "lblWindowTitle";
			this.lblWindowTitle.Size = new System.Drawing.Size(68, 13);
			this.lblWindowTitle.TabIndex = 0;
			this.lblWindowTitle.Text = "Window &title:";
			// 
			// fraFiles
			// 
			this.fraFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraFiles.Controls.Add(this.cmdFilesClear);
			this.fraFiles.Controls.Add(this.cmdFilesRemove);
			this.fraFiles.Controls.Add(this.cmdFilesModify);
			this.fraFiles.Controls.Add(this.cmdFilesAdd);
			this.fraFiles.Controls.Add(this.lvFiles);
			this.fraFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraFiles.Location = new System.Drawing.Point(4, 218);
			this.fraFiles.Name = "fraFiles";
			this.fraFiles.Size = new System.Drawing.Size(473, 171);
			this.fraFiles.TabIndex = 1;
			this.fraFiles.TabStop = false;
			this.fraFiles.Text = "Files";
			// 
			// cmdFilesClear
			// 
			this.cmdFilesClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdFilesClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFilesClear.Location = new System.Drawing.Point(392, 19);
			this.cmdFilesClear.Name = "cmdFilesClear";
			this.cmdFilesClear.Size = new System.Drawing.Size(75, 23);
			this.cmdFilesClear.TabIndex = 1;
			this.cmdFilesClear.Text = "Cl&ear";
			this.cmdFilesClear.UseVisualStyleBackColor = true;
			// 
			// cmdFilesRemove
			// 
			this.cmdFilesRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFilesRemove.Location = new System.Drawing.Point(168, 19);
			this.cmdFilesRemove.Name = "cmdFilesRemove";
			this.cmdFilesRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdFilesRemove.TabIndex = 1;
			this.cmdFilesRemove.Text = "&Remove";
			this.cmdFilesRemove.UseVisualStyleBackColor = true;
			// 
			// cmdFilesModify
			// 
			this.cmdFilesModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFilesModify.Location = new System.Drawing.Point(87, 19);
			this.cmdFilesModify.Name = "cmdFilesModify";
			this.cmdFilesModify.Size = new System.Drawing.Size(75, 23);
			this.cmdFilesModify.TabIndex = 1;
			this.cmdFilesModify.Text = "&Modify...";
			this.cmdFilesModify.UseVisualStyleBackColor = true;
			// 
			// cmdFilesAdd
			// 
			this.cmdFilesAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFilesAdd.Location = new System.Drawing.Point(6, 19);
			this.cmdFilesAdd.Name = "cmdFilesAdd";
			this.cmdFilesAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdFilesAdd.TabIndex = 1;
			this.cmdFilesAdd.Text = "&Add...";
			this.cmdFilesAdd.UseVisualStyleBackColor = true;
			// 
			// lvFiles
			// 
			this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSource,
            this.chDestination});
			this.lvFiles.HideSelection = false;
			this.lvFiles.Location = new System.Drawing.Point(6, 48);
			this.lvFiles.Name = "lvFiles";
			this.lvFiles.Size = new System.Drawing.Size(461, 117);
			this.lvFiles.TabIndex = 0;
			this.lvFiles.UseCompatibleStateImageBehavior = false;
			this.lvFiles.View = System.Windows.Forms.View.Details;
			// 
			// chSource
			// 
			this.chSource.Text = "Source";
			this.chSource.Width = 166;
			// 
			// chDestination
			// 
			this.chDestination.Text = "Destination";
			this.chDestination.Width = 287;
			// 
			// BootstrapScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fraFiles);
			this.Controls.Add(this.fraGeneral);
			this.MinimumSize = new System.Drawing.Size(486, 392);
			this.Name = "BootstrapScriptEditor";
			this.Size = new System.Drawing.Size(486, 392);
			this.fraGeneral.ResumeLayout(false);
			this.fraGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTemporaryDirectorySize)).EndInit();
			this.fraFiles.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox fraGeneral;
		private System.Windows.Forms.TextBox txtWindowTitle;
		private System.Windows.Forms.Label lblWindowTitle;
		private System.Windows.Forms.TextBox txtWindowMessage;
		private System.Windows.Forms.Label lblWindowMessage;
		private System.Windows.Forms.NumericUpDown txtTemporaryDirectorySize;
		private System.Windows.Forms.TextBox txtTemporaryDirectoryName;
		private System.Windows.Forms.Label lblTemporaryDirectorySize;
		private System.Windows.Forms.Label lblTemporaryDirectoryName;
		private System.Windows.Forms.TextBox txtCommandLine;
		private System.Windows.Forms.Label lblCommandLine;
		private System.Windows.Forms.TextBox txtWindowClassName;
		private System.Windows.Forms.Label lblWindowClassName;
		private System.Windows.Forms.CheckBox chkRequire31;
		private System.Windows.Forms.TextBox txtRequire31;
		private System.Windows.Forms.Label lblRequire31;
		private System.Windows.Forms.GroupBox fraFiles;
		private System.Windows.Forms.ListView lvFiles;
		private System.Windows.Forms.ColumnHeader chSource;
		private System.Windows.Forms.ColumnHeader chDestination;
		private System.Windows.Forms.Button cmdFilesClear;
		private System.Windows.Forms.Button cmdFilesRemove;
		private System.Windows.Forms.Button cmdFilesModify;
		private System.Windows.Forms.Button cmdFilesAdd;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}
