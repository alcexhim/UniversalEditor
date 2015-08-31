namespace UniversalEditor.Engines.WindowsForms.OptionPanels.Application
{
    partial class GeneralOptionPanel
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
			this.fraWindowLayout = new System.Windows.Forms.GroupBox();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.fraExtensions = new System.Windows.Forms.GroupBox();
			this.txtOnlineGalleryURL = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.chkEnableGalleryAutoUpdate = new System.Windows.Forms.CheckBox();
			this.chkEnableExtensionGallery = new System.Windows.Forms.CheckBox();
			this.fraTitleBarBehavior = new System.Windows.Forms.GroupBox();
			this.optTitleBarBehaviorNone = new System.Windows.Forms.RadioButton();
			this.optTitleBarBehaviorCurrentFileName = new System.Windows.Forms.RadioButton();
			this.optTitleBarBehaviorCurrentFilePath = new System.Windows.Forms.RadioButton();
			this.fraWindowLayout.SuspendLayout();
			this.fraExtensions.SuspendLayout();
			this.fraTitleBarBehavior.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraWindowLayout
			// 
			this.fraWindowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraWindowLayout.Controls.Add(this.radioButton3);
			this.fraWindowLayout.Controls.Add(this.radioButton2);
			this.fraWindowLayout.Controls.Add(this.radioButton1);
			this.fraWindowLayout.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraWindowLayout.Location = new System.Drawing.Point(3, 3);
			this.fraWindowLayout.Name = "fraWindowLayout";
			this.fraWindowLayout.Size = new System.Drawing.Size(505, 52);
			this.fraWindowLayout.TabIndex = 0;
			this.fraWindowLayout.TabStop = false;
			this.fraWindowLayout.Text = "Window layout";
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioButton3.Location = new System.Drawing.Point(273, 19);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(118, 18);
			this.radioButton3.TabIndex = 0;
			this.radioButton3.Text = "&Separate windows";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Checked = true;
			this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioButton2.Location = new System.Drawing.Point(151, 19);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(122, 18);
			this.radioButton2.TabIndex = 0;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "&Multiple documents";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Enabled = false;
			this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioButton1.Location = new System.Drawing.Point(28, 19);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(123, 18);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.Text = "&Tabbed documents";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// fraExtensions
			// 
			this.fraExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraExtensions.Controls.Add(this.txtOnlineGalleryURL);
			this.fraExtensions.Controls.Add(this.label1);
			this.fraExtensions.Controls.Add(this.checkBox1);
			this.fraExtensions.Controls.Add(this.chkEnableGalleryAutoUpdate);
			this.fraExtensions.Controls.Add(this.chkEnableExtensionGallery);
			this.fraExtensions.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraExtensions.Location = new System.Drawing.Point(3, 162);
			this.fraExtensions.Name = "fraExtensions";
			this.fraExtensions.Size = new System.Drawing.Size(505, 117);
			this.fraExtensions.TabIndex = 1;
			this.fraExtensions.TabStop = false;
			this.fraExtensions.Text = "Extensions";
			// 
			// txtOnlineGalleryURL
			// 
			this.txtOnlineGalleryURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOnlineGalleryURL.Location = new System.Drawing.Point(151, 42);
			this.txtOnlineGalleryURL.Name = "txtOnlineGalleryURL";
			this.txtOnlineGalleryURL.Size = new System.Drawing.Size(348, 20);
			this.txtOnlineGalleryURL.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(47, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Online gallery &URL:";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(28, 91);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(288, 17);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Load &per-user extensions when running as Administrator";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// chkEnableGalleryAutoUpdate
			// 
			this.chkEnableGalleryAutoUpdate.AutoSize = true;
			this.chkEnableGalleryAutoUpdate.Location = new System.Drawing.Point(28, 68);
			this.chkEnableGalleryAutoUpdate.Name = "chkEnableGalleryAutoUpdate";
			this.chkEnableGalleryAutoUpdate.Size = new System.Drawing.Size(283, 17);
			this.chkEnableGalleryAutoUpdate.TabIndex = 0;
			this.chkEnableGalleryAutoUpdate.Text = "Automatically check for up&dates to installed extensions";
			this.chkEnableGalleryAutoUpdate.UseVisualStyleBackColor = true;
			// 
			// chkEnableExtensionGallery
			// 
			this.chkEnableExtensionGallery.AutoSize = true;
			this.chkEnableExtensionGallery.Location = new System.Drawing.Point(28, 19);
			this.chkEnableExtensionGallery.Name = "chkEnableExtensionGallery";
			this.chkEnableExtensionGallery.Size = new System.Drawing.Size(258, 17);
			this.chkEnableExtensionGallery.TabIndex = 0;
			this.chkEnableExtensionGallery.Text = "Enable access to extensions in the Online &Gallery";
			this.chkEnableExtensionGallery.UseVisualStyleBackColor = true;
			// 
			// fraTitleBarBehavior
			// 
			this.fraTitleBarBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraTitleBarBehavior.Controls.Add(this.optTitleBarBehaviorCurrentFilePath);
			this.fraTitleBarBehavior.Controls.Add(this.optTitleBarBehaviorCurrentFileName);
			this.fraTitleBarBehavior.Controls.Add(this.optTitleBarBehaviorNone);
			this.fraTitleBarBehavior.Location = new System.Drawing.Point(3, 61);
			this.fraTitleBarBehavior.Name = "fraTitleBarBehavior";
			this.fraTitleBarBehavior.Size = new System.Drawing.Size(505, 95);
			this.fraTitleBarBehavior.TabIndex = 2;
			this.fraTitleBarBehavior.TabStop = false;
			this.fraTitleBarBehavior.Text = "Title bar behavior";
			// 
			// optTitleBarBehaviorNone
			// 
			this.optTitleBarBehaviorNone.AutoSize = true;
			this.optTitleBarBehaviorNone.Checked = true;
			this.optTitleBarBehaviorNone.Location = new System.Drawing.Point(28, 19);
			this.optTitleBarBehaviorNone.Name = "optTitleBarBehaviorNone";
			this.optTitleBarBehaviorNone.Size = new System.Drawing.Size(301, 17);
			this.optTitleBarBehaviorNone.TabIndex = 0;
			this.optTitleBarBehaviorNone.TabStop = true;
			this.optTitleBarBehaviorNone.Text = "Do not display document-specific information in the title bar";
			this.optTitleBarBehaviorNone.UseVisualStyleBackColor = true;
			// 
			// optTitleBarBehaviorCurrentFileName
			// 
			this.optTitleBarBehaviorCurrentFileName.AutoSize = true;
			this.optTitleBarBehaviorCurrentFileName.Location = new System.Drawing.Point(28, 42);
			this.optTitleBarBehaviorCurrentFileName.Name = "optTitleBarBehaviorCurrentFileName";
			this.optTitleBarBehaviorCurrentFileName.Size = new System.Drawing.Size(304, 17);
			this.optTitleBarBehaviorCurrentFileName.TabIndex = 0;
			this.optTitleBarBehaviorCurrentFileName.Text = "Display the file &name of the current document in the title bar";
			this.optTitleBarBehaviorCurrentFileName.UseVisualStyleBackColor = true;
			// 
			// optTitleBarBehaviorCurrentFilePath
			// 
			this.optTitleBarBehaviorCurrentFilePath.AutoSize = true;
			this.optTitleBarBehaviorCurrentFilePath.Location = new System.Drawing.Point(28, 65);
			this.optTitleBarBehaviorCurrentFilePath.Name = "optTitleBarBehaviorCurrentFilePath";
			this.optTitleBarBehaviorCurrentFilePath.Size = new System.Drawing.Size(299, 17);
			this.optTitleBarBehaviorCurrentFilePath.TabIndex = 0;
			this.optTitleBarBehaviorCurrentFilePath.Text = "Display the full &path to the current document in the title bar";
			this.optTitleBarBehaviorCurrentFilePath.UseVisualStyleBackColor = true;
			// 
			// ApplicationOptionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fraTitleBarBehavior);
			this.Controls.Add(this.fraExtensions);
			this.Controls.Add(this.fraWindowLayout);
			this.Name = "ApplicationOptionPanel";
			this.Size = new System.Drawing.Size(511, 405);
			this.fraWindowLayout.ResumeLayout(false);
			this.fraWindowLayout.PerformLayout();
			this.fraExtensions.ResumeLayout(false);
			this.fraExtensions.PerformLayout();
			this.fraTitleBarBehavior.ResumeLayout(false);
			this.fraTitleBarBehavior.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fraWindowLayout;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox fraExtensions;
        private System.Windows.Forms.TextBox txtOnlineGalleryURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkEnableExtensionGallery;
        private System.Windows.Forms.CheckBox chkEnableGalleryAutoUpdate;
        private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.GroupBox fraTitleBarBehavior;
		private System.Windows.Forms.RadioButton optTitleBarBehaviorCurrentFilePath;
		private System.Windows.Forms.RadioButton optTitleBarBehaviorCurrentFileName;
		private System.Windows.Forms.RadioButton optTitleBarBehaviorNone;
    }
}
