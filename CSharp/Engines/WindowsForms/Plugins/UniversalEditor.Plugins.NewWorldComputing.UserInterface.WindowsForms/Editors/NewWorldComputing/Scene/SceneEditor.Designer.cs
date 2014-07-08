namespace UniversalEditor.Editors.NewWorldComputing.Scene
{
    partial class SceneEditor
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
			this.dsnr = new AwesomeControls.Designer.DesignerControl();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkVisibilityLabel = new System.Windows.Forms.CheckBox();
			this.chkVisibilityImage = new System.Windows.Forms.CheckBox();
			this.chkVisibilityButton = new System.Windows.Forms.CheckBox();
			this.chkVisibilityScreen = new System.Windows.Forms.CheckBox();
			this.fraObjectProperties = new System.Windows.Forms.GroupBox();
			this.pnlObjectSpecificProperties = new System.Windows.Forms.Panel();
			this.pnlObjectPropertiesLabel = new System.Windows.Forms.Panel();
			this.txtFontFilename = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtLabelText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlObjectPropertiesImage = new System.Windows.Forms.Panel();
			this.pnlObjectPropertiesButton = new System.Windows.Forms.Panel();
			this.pnlCommonProperties = new System.Windows.Forms.Panel();
			this.txtHeight = new System.Windows.Forms.NumericUpDown();
			this.txtTop = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtWidth = new System.Windows.Forms.NumericUpDown();
			this.txtLeft = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pnlOptions = new System.Windows.Forms.Panel();
			this.groupBox2.SuspendLayout();
			this.fraObjectProperties.SuspendLayout();
			this.pnlObjectSpecificProperties.SuspendLayout();
			this.pnlObjectPropertiesLabel.SuspendLayout();
			this.pnlCommonProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTop)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLeft)).BeginInit();
			this.pnlOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// dsnr
			// 
			this.dsnr.DefaultObjectClass = null;
			this.dsnr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dsnr.EnableCreation = false;
			this.dsnr.Location = new System.Drawing.Point(0, 169);
			this.dsnr.Name = "dsnr";
			this.dsnr.Size = new System.Drawing.Size(510, 227);
			this.dsnr.TabIndex = 0;
			this.dsnr.DesignerObjectSelected += new AwesomeControls.Designer.DesignerObjectSelectedEventHandler(this.dsnr_DesignerObjectSelected);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkVisibilityLabel);
			this.groupBox2.Controls.Add(this.chkVisibilityImage);
			this.groupBox2.Controls.Add(this.chkVisibilityButton);
			this.groupBox2.Controls.Add(this.chkVisibilityScreen);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(135, 160);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filter Objects by Type";
			// 
			// chkVisibilityLabel
			// 
			this.chkVisibilityLabel.AutoSize = true;
			this.chkVisibilityLabel.Checked = true;
			this.chkVisibilityLabel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkVisibilityLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkVisibilityLabel.Location = new System.Drawing.Point(15, 92);
			this.chkVisibilityLabel.Name = "chkVisibilityLabel";
			this.chkVisibilityLabel.Size = new System.Drawing.Size(58, 18);
			this.chkVisibilityLabel.TabIndex = 0;
			this.chkVisibilityLabel.Text = "&Label";
			this.chkVisibilityLabel.UseVisualStyleBackColor = true;
			this.chkVisibilityLabel.CheckedChanged += new System.EventHandler(this.chkVisibility_CheckedChanged);
			// 
			// chkVisibilityImage
			// 
			this.chkVisibilityImage.AutoSize = true;
			this.chkVisibilityImage.Checked = true;
			this.chkVisibilityImage.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkVisibilityImage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkVisibilityImage.Location = new System.Drawing.Point(15, 69);
			this.chkVisibilityImage.Name = "chkVisibilityImage";
			this.chkVisibilityImage.Size = new System.Drawing.Size(61, 18);
			this.chkVisibilityImage.TabIndex = 0;
			this.chkVisibilityImage.Text = "&Image";
			this.chkVisibilityImage.UseVisualStyleBackColor = true;
			this.chkVisibilityImage.CheckedChanged += new System.EventHandler(this.chkVisibility_CheckedChanged);
			// 
			// chkVisibilityButton
			// 
			this.chkVisibilityButton.AutoSize = true;
			this.chkVisibilityButton.Checked = true;
			this.chkVisibilityButton.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkVisibilityButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkVisibilityButton.Location = new System.Drawing.Point(15, 46);
			this.chkVisibilityButton.Name = "chkVisibilityButton";
			this.chkVisibilityButton.Size = new System.Drawing.Size(63, 18);
			this.chkVisibilityButton.TabIndex = 0;
			this.chkVisibilityButton.Text = "&Button";
			this.chkVisibilityButton.UseVisualStyleBackColor = true;
			this.chkVisibilityButton.CheckedChanged += new System.EventHandler(this.chkVisibility_CheckedChanged);
			// 
			// chkVisibilityScreen
			// 
			this.chkVisibilityScreen.AutoSize = true;
			this.chkVisibilityScreen.Checked = true;
			this.chkVisibilityScreen.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkVisibilityScreen.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkVisibilityScreen.Location = new System.Drawing.Point(15, 23);
			this.chkVisibilityScreen.Name = "chkVisibilityScreen";
			this.chkVisibilityScreen.Size = new System.Drawing.Size(66, 18);
			this.chkVisibilityScreen.TabIndex = 0;
			this.chkVisibilityScreen.Text = "&Screen";
			this.chkVisibilityScreen.UseVisualStyleBackColor = true;
			this.chkVisibilityScreen.CheckedChanged += new System.EventHandler(this.chkVisibility_CheckedChanged);
			// 
			// fraObjectProperties
			// 
			this.fraObjectProperties.Controls.Add(this.pnlObjectSpecificProperties);
			this.fraObjectProperties.Controls.Add(this.pnlCommonProperties);
			this.fraObjectProperties.Location = new System.Drawing.Point(144, 3);
			this.fraObjectProperties.Name = "fraObjectProperties";
			this.fraObjectProperties.Size = new System.Drawing.Size(363, 160);
			this.fraObjectProperties.TabIndex = 1;
			this.fraObjectProperties.TabStop = false;
			this.fraObjectProperties.Text = "Object Properties";
			// 
			// pnlObjectSpecificProperties
			// 
			this.pnlObjectSpecificProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlObjectSpecificProperties.Controls.Add(this.pnlObjectPropertiesLabel);
			this.pnlObjectSpecificProperties.Controls.Add(this.pnlObjectPropertiesImage);
			this.pnlObjectSpecificProperties.Controls.Add(this.pnlObjectPropertiesButton);
			this.pnlObjectSpecificProperties.Location = new System.Drawing.Point(6, 92);
			this.pnlObjectSpecificProperties.Name = "pnlObjectSpecificProperties";
			this.pnlObjectSpecificProperties.Size = new System.Drawing.Size(351, 62);
			this.pnlObjectSpecificProperties.TabIndex = 3;
			// 
			// pnlObjectPropertiesLabel
			// 
			this.pnlObjectPropertiesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlObjectPropertiesLabel.Controls.Add(this.txtFontFilename);
			this.pnlObjectPropertiesLabel.Controls.Add(this.label2);
			this.pnlObjectPropertiesLabel.Controls.Add(this.txtLabelText);
			this.pnlObjectPropertiesLabel.Controls.Add(this.label1);
			this.pnlObjectPropertiesLabel.Location = new System.Drawing.Point(0, 0);
			this.pnlObjectPropertiesLabel.Name = "pnlObjectPropertiesLabel";
			this.pnlObjectPropertiesLabel.Size = new System.Drawing.Size(351, 62);
			this.pnlObjectPropertiesLabel.TabIndex = 1;
			// 
			// txtFontFilename
			// 
			this.txtFontFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFontFilename.Location = new System.Drawing.Point(82, 29);
			this.txtFontFilename.Name = "txtFontFilename";
			this.txtFontFilename.Size = new System.Drawing.Size(266, 20);
			this.txtFontFilename.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(3, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Font file&name:";
			// 
			// txtLabelText
			// 
			this.txtLabelText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLabelText.Location = new System.Drawing.Point(82, 3);
			this.txtLabelText.Name = "txtLabelText";
			this.txtLabelText.Size = new System.Drawing.Size(266, 20);
			this.txtLabelText.TabIndex = 1;
			this.txtLabelText.Validated += new System.EventHandler(this.txtLabelText_Validated);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Text:";
			// 
			// pnlObjectPropertiesImage
			// 
			this.pnlObjectPropertiesImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlObjectPropertiesImage.Location = new System.Drawing.Point(0, 0);
			this.pnlObjectPropertiesImage.Name = "pnlObjectPropertiesImage";
			this.pnlObjectPropertiesImage.Size = new System.Drawing.Size(351, 62);
			this.pnlObjectPropertiesImage.TabIndex = 2;
			// 
			// pnlObjectPropertiesButton
			// 
			this.pnlObjectPropertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlObjectPropertiesButton.Location = new System.Drawing.Point(0, 0);
			this.pnlObjectPropertiesButton.Name = "pnlObjectPropertiesButton";
			this.pnlObjectPropertiesButton.Size = new System.Drawing.Size(351, 62);
			this.pnlObjectPropertiesButton.TabIndex = 0;
			// 
			// pnlCommonProperties
			// 
			this.pnlCommonProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlCommonProperties.Controls.Add(this.txtHeight);
			this.pnlCommonProperties.Controls.Add(this.txtTop);
			this.pnlCommonProperties.Controls.Add(this.label6);
			this.pnlCommonProperties.Controls.Add(this.label4);
			this.pnlCommonProperties.Controls.Add(this.txtWidth);
			this.pnlCommonProperties.Controls.Add(this.txtLeft);
			this.pnlCommonProperties.Controls.Add(this.label5);
			this.pnlCommonProperties.Controls.Add(this.label3);
			this.pnlCommonProperties.Location = new System.Drawing.Point(6, 19);
			this.pnlCommonProperties.Name = "pnlCommonProperties";
			this.pnlCommonProperties.Size = new System.Drawing.Size(351, 67);
			this.pnlCommonProperties.TabIndex = 3;
			// 
			// txtHeight
			// 
			this.txtHeight.Location = new System.Drawing.Point(186, 29);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(87, 20);
			this.txtHeight.TabIndex = 1;
			this.txtHeight.Validated += new System.EventHandler(this.txtBounds_Validated);
			// 
			// txtTop
			// 
			this.txtTop.Location = new System.Drawing.Point(38, 29);
			this.txtTop.Name = "txtTop";
			this.txtTop.Size = new System.Drawing.Size(87, 20);
			this.txtTop.TabIndex = 1;
			this.txtTop.Validated += new System.EventHandler(this.txtBounds_Validated);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(139, 31);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "&Height:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(3, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "&Top:";
			// 
			// txtWidth
			// 
			this.txtWidth.Location = new System.Drawing.Point(186, 3);
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.Size = new System.Drawing.Size(87, 20);
			this.txtWidth.TabIndex = 1;
			this.txtWidth.Validated += new System.EventHandler(this.txtBounds_Validated);
			// 
			// txtLeft
			// 
			this.txtLeft.Location = new System.Drawing.Point(38, 3);
			this.txtLeft.Name = "txtLeft";
			this.txtLeft.Size = new System.Drawing.Size(87, 20);
			this.txtLeft.TabIndex = 1;
			this.txtLeft.Validated += new System.EventHandler(this.txtBounds_Validated);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(139, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "&Width:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(3, 5);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(28, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "&Left:";
			// 
			// pnlOptions
			// 
			this.pnlOptions.Controls.Add(this.fraObjectProperties);
			this.pnlOptions.Controls.Add(this.groupBox2);
			this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlOptions.Location = new System.Drawing.Point(0, 0);
			this.pnlOptions.Name = "pnlOptions";
			this.pnlOptions.Size = new System.Drawing.Size(510, 169);
			this.pnlOptions.TabIndex = 2;
			// 
			// SceneEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dsnr);
			this.Controls.Add(this.pnlOptions);
			this.Name = "SceneEditor";
			this.Size = new System.Drawing.Size(510, 396);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.fraObjectProperties.ResumeLayout(false);
			this.pnlObjectSpecificProperties.ResumeLayout(false);
			this.pnlObjectPropertiesLabel.ResumeLayout(false);
			this.pnlObjectPropertiesLabel.PerformLayout();
			this.pnlCommonProperties.ResumeLayout(false);
			this.pnlCommonProperties.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTop)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLeft)).EndInit();
			this.pnlOptions.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private AwesomeControls.Designer.DesignerControl dsnr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkVisibilityLabel;
        private System.Windows.Forms.CheckBox chkVisibilityImage;
        private System.Windows.Forms.CheckBox chkVisibilityButton;
        private System.Windows.Forms.CheckBox chkVisibilityScreen;
        private System.Windows.Forms.GroupBox fraObjectProperties;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Panel pnlObjectPropertiesButton;
        private System.Windows.Forms.Panel pnlObjectPropertiesImage;
        private System.Windows.Forms.Panel pnlObjectPropertiesLabel;
        private System.Windows.Forms.TextBox txtLabelText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFontFilename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlCommonProperties;
        private System.Windows.Forms.Panel pnlObjectSpecificProperties;
        private System.Windows.Forms.NumericUpDown txtHeight;
        private System.Windows.Forms.NumericUpDown txtTop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtWidth;
        private System.Windows.Forms.NumericUpDown txtLeft;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
    }
}
