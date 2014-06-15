namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.Dialogs
{
	partial class TexturePropertiesDialog
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.txtTextureFileName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkFlagAddMap = new System.Windows.Forms.CheckBox();
            this.chkFlagMap = new System.Windows.Forms.CheckBox();
            this.chkFlagTexture = new System.Windows.Forms.CheckBox();
            this.fraTextureFileName = new System.Windows.Forms.GroupBox();
            this.cmdClearTexture = new System.Windows.Forms.Button();
            this.cmdSelectTexture = new System.Windows.Forms.Button();
            this.fraMapFileName = new System.Windows.Forms.GroupBox();
            this.cmdClearMap = new System.Windows.Forms.Button();
            this.cmdSelectMap = new System.Windows.Forms.Button();
            this.txtMapFileName = new System.Windows.Forms.TextBox();
            this.fraDuration = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.fraTextureFileName.SuspendLayout();
            this.fraMapFileName.SuspendLayout();
            this.fraDuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancel.Location = new System.Drawing.Point(308, 194);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdOK.Location = new System.Drawing.Point(227, 194);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // txtTextureFileName
            // 
            this.txtTextureFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextureFileName.Location = new System.Drawing.Point(6, 21);
            this.txtTextureFileName.Name = "txtTextureFileName";
            this.txtTextureFileName.ReadOnly = true;
            this.txtTextureFileName.Size = new System.Drawing.Size(247, 20);
            this.txtTextureFileName.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkFlagAddMap);
            this.groupBox1.Controls.Add(this.chkFlagMap);
            this.groupBox1.Controls.Add(this.chkFlagTexture);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 55);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Flags";
            // 
            // chkFlagAddMap
            // 
            this.chkFlagAddMap.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkFlagAddMap.AutoSize = true;
            this.chkFlagAddMap.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkFlagAddMap.Location = new System.Drawing.Point(140, 19);
            this.chkFlagAddMap.Name = "chkFlagAddMap";
            this.chkFlagAddMap.Size = new System.Drawing.Size(72, 18);
            this.chkFlagAddMap.TabIndex = 2;
            this.chkFlagAddMap.Text = "&AddMap";
            this.chkFlagAddMap.UseVisualStyleBackColor = true;
            // 
            // chkFlagMap
            // 
            this.chkFlagMap.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkFlagMap.AutoSize = true;
            this.chkFlagMap.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkFlagMap.Location = new System.Drawing.Point(87, 19);
            this.chkFlagMap.Name = "chkFlagMap";
            this.chkFlagMap.Size = new System.Drawing.Size(53, 18);
            this.chkFlagMap.TabIndex = 1;
            this.chkFlagMap.Text = "&Map";
            this.chkFlagMap.UseVisualStyleBackColor = true;
            // 
            // chkFlagTexture
            // 
            this.chkFlagTexture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkFlagTexture.AutoSize = true;
            this.chkFlagTexture.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkFlagTexture.Location = new System.Drawing.Point(19, 19);
            this.chkFlagTexture.Name = "chkFlagTexture";
            this.chkFlagTexture.Size = new System.Drawing.Size(68, 18);
            this.chkFlagTexture.TabIndex = 0;
            this.chkFlagTexture.Text = "&Texture";
            this.chkFlagTexture.UseVisualStyleBackColor = true;
            // 
            // fraTextureFileName
            // 
            this.fraTextureFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraTextureFileName.Controls.Add(this.cmdClearTexture);
            this.fraTextureFileName.Controls.Add(this.cmdSelectTexture);
            this.fraTextureFileName.Controls.Add(this.txtTextureFileName);
            this.fraTextureFileName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraTextureFileName.Location = new System.Drawing.Point(12, 12);
            this.fraTextureFileName.Name = "fraTextureFileName";
            this.fraTextureFileName.Size = new System.Drawing.Size(371, 52);
            this.fraTextureFileName.TabIndex = 0;
            this.fraTextureFileName.TabStop = false;
            this.fraTextureFileName.Text = "Texture filename";
            // 
            // cmdClearTexture
            // 
            this.cmdClearTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClearTexture.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdClearTexture.Location = new System.Drawing.Point(315, 19);
            this.cmdClearTexture.Name = "cmdClearTexture";
            this.cmdClearTexture.Size = new System.Drawing.Size(50, 23);
            this.cmdClearTexture.TabIndex = 2;
            this.cmdClearTexture.Text = "&Clear";
            this.cmdClearTexture.UseVisualStyleBackColor = true;
            this.cmdClearTexture.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdSelectTexture
            // 
            this.cmdSelectTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectTexture.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdSelectTexture.Location = new System.Drawing.Point(259, 19);
            this.cmdSelectTexture.Name = "cmdSelectTexture";
            this.cmdSelectTexture.Size = new System.Drawing.Size(50, 23);
            this.cmdSelectTexture.TabIndex = 1;
            this.cmdSelectTexture.Text = "&Select";
            this.cmdSelectTexture.UseVisualStyleBackColor = true;
            this.cmdSelectTexture.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // fraMapFileName
            // 
            this.fraMapFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraMapFileName.Controls.Add(this.cmdClearMap);
            this.fraMapFileName.Controls.Add(this.cmdSelectMap);
            this.fraMapFileName.Controls.Add(this.txtMapFileName);
            this.fraMapFileName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraMapFileName.Location = new System.Drawing.Point(12, 70);
            this.fraMapFileName.Name = "fraMapFileName";
            this.fraMapFileName.Size = new System.Drawing.Size(371, 52);
            this.fraMapFileName.TabIndex = 1;
            this.fraMapFileName.TabStop = false;
            this.fraMapFileName.Text = "Map filename";
            // 
            // cmdClearMap
            // 
            this.cmdClearMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClearMap.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdClearMap.Location = new System.Drawing.Point(315, 19);
            this.cmdClearMap.Name = "cmdClearMap";
            this.cmdClearMap.Size = new System.Drawing.Size(50, 23);
            this.cmdClearMap.TabIndex = 2;
            this.cmdClearMap.Text = "&Clear";
            this.cmdClearMap.UseVisualStyleBackColor = true;
            this.cmdClearMap.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdSelectMap
            // 
            this.cmdSelectMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectMap.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdSelectMap.Location = new System.Drawing.Point(259, 19);
            this.cmdSelectMap.Name = "cmdSelectMap";
            this.cmdSelectMap.Size = new System.Drawing.Size(50, 23);
            this.cmdSelectMap.TabIndex = 1;
            this.cmdSelectMap.Text = "&Select";
            this.cmdSelectMap.UseVisualStyleBackColor = true;
            this.cmdSelectMap.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // txtMapFileName
            // 
            this.txtMapFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMapFileName.Location = new System.Drawing.Point(6, 21);
            this.txtMapFileName.Name = "txtMapFileName";
            this.txtMapFileName.ReadOnly = true;
            this.txtMapFileName.Size = new System.Drawing.Size(247, 20);
            this.txtMapFileName.TabIndex = 0;
            // 
            // fraDuration
            // 
            this.fraDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fraDuration.Controls.Add(this.label1);
            this.fraDuration.Controls.Add(this.txtDuration);
            this.fraDuration.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraDuration.Location = new System.Drawing.Point(242, 128);
            this.fraDuration.Name = "fraDuration";
            this.fraDuration.Size = new System.Drawing.Size(141, 55);
            this.fraDuration.TabIndex = 2;
            this.fraDuration.TabStop = false;
            this.fraDuration.Text = "Duration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(115, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ms";
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(6, 19);
            this.txtDuration.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(103, 20);
            this.txtDuration.TabIndex = 0;
            this.txtDuration.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // TexturePropertiesDialog
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(395, 229);
            this.Controls.Add(this.fraMapFileName);
            this.Controls.Add(this.fraTextureFileName);
            this.Controls.Add(this.fraDuration);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TexturePropertiesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Texture Properties";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.fraTextureFileName.ResumeLayout(false);
            this.fraTextureFileName.PerformLayout();
            this.fraMapFileName.ResumeLayout(false);
            this.fraMapFileName.PerformLayout();
            this.fraDuration.ResumeLayout(false);
            this.fraDuration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuration)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.TextBox txtTextureFileName;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.CheckBox chkFlagAddMap;
		internal System.Windows.Forms.CheckBox chkFlagMap;
		internal System.Windows.Forms.CheckBox chkFlagTexture;
		private System.Windows.Forms.GroupBox fraTextureFileName;
		private System.Windows.Forms.Button cmdClearTexture;
		private System.Windows.Forms.Button cmdSelectTexture;
		private System.Windows.Forms.GroupBox fraMapFileName;
		private System.Windows.Forms.Button cmdClearMap;
		private System.Windows.Forms.Button cmdSelectMap;
		internal System.Windows.Forms.TextBox txtMapFileName;
		private System.Windows.Forms.GroupBox fraDuration;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.NumericUpDown txtDuration;
	}
}