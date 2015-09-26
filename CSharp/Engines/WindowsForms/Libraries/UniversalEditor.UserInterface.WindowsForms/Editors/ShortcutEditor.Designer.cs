namespace UniversalEditor.Editors
{
    partial class ShortcutEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboTargetType = new System.Windows.Forms.ComboBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdBrowseTarget = new System.Windows.Forms.Button();
            this.txtStartIn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtShortcutKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboWindowSize = new System.Windows.Forms.ComboBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fraShortcutInformation = new System.Windows.Forms.GroupBox();
            this.fraAdvancedOptions = new System.Windows.Forms.GroupBox();
            this.chkRunInSeparateMemorySpace = new System.Windows.Forms.CheckBox();
            this.chkRunWithDifferentCredentials = new System.Windows.Forms.CheckBox();
            this.fraIcon = new System.Windows.Forms.GroupBox();
            this.lvIcons = new System.Windows.Forms.ListView();
            this.txtIconFileName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.fraShortcutInformation.SuspendLayout();
            this.fraAdvancedOptions.SuspendLayout();
            this.fraIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target type:";
            // 
            // cboTargetType
            // 
            this.cboTargetType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTargetType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboTargetType.FormattingEnabled = true;
            this.cboTargetType.Items.AddRange(new object[] {
            "Local file/directory",
            "Shell namespace extension"});
            this.cboTargetType.Location = new System.Drawing.Point(94, 22);
            this.cboTargetType.Name = "cboTargetType";
            this.cboTargetType.Size = new System.Drawing.Size(383, 21);
            this.cboTargetType.TabIndex = 1;
            this.cboTargetType.SelectedIndexChanged += new System.EventHandler(this.cboTargetType_SelectedIndexChanged);
            // 
            // txtTarget
            // 
            this.txtTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTarget.Location = new System.Drawing.Point(94, 49);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(383, 20);
            this.txtTarget.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Target:";
            // 
            // cmdBrowseTarget
            // 
            this.cmdBrowseTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseTarget.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdBrowseTarget.Location = new System.Drawing.Point(402, 75);
            this.cmdBrowseTarget.Name = "cmdBrowseTarget";
            this.cmdBrowseTarget.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseTarget.TabIndex = 4;
            this.cmdBrowseTarget.Text = "&Browse...";
            this.cmdBrowseTarget.UseVisualStyleBackColor = true;
            this.cmdBrowseTarget.Click += new System.EventHandler(this.cmdBrowseTarget_Click);
            // 
            // txtStartIn
            // 
            this.txtStartIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartIn.Location = new System.Drawing.Point(94, 104);
            this.txtStartIn.Name = "txtStartIn";
            this.txtStartIn.Size = new System.Drawing.Size(383, 20);
            this.txtStartIn.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(18, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Start in:";
            // 
            // txtShortcutKey
            // 
            this.txtShortcutKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShortcutKey.Location = new System.Drawing.Point(94, 130);
            this.txtShortcutKey.Name = "txtShortcutKey";
            this.txtShortcutKey.Size = new System.Drawing.Size(383, 20);
            this.txtShortcutKey.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(18, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Shortcut key:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Location = new System.Drawing.Point(18, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Window size:";
            // 
            // cboWindowSize
            // 
            this.cboWindowSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboWindowSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboWindowSize.FormattingEnabled = true;
            this.cboWindowSize.Location = new System.Drawing.Point(94, 156);
            this.cboWindowSize.Name = "cboWindowSize";
            this.cboWindowSize.Size = new System.Drawing.Size(383, 21);
            this.cboWindowSize.TabIndex = 1;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(94, 183);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(383, 20);
            this.txtComment.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.Location = new System.Drawing.Point(18, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Comment:";
            // 
            // fraShortcutInformation
            // 
            this.fraShortcutInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fraShortcutInformation.Controls.Add(this.label1);
            this.fraShortcutInformation.Controls.Add(this.cmdBrowseTarget);
            this.fraShortcutInformation.Controls.Add(this.cboTargetType);
            this.fraShortcutInformation.Controls.Add(this.label4);
            this.fraShortcutInformation.Controls.Add(this.label5);
            this.fraShortcutInformation.Controls.Add(this.label3);
            this.fraShortcutInformation.Controls.Add(this.cboWindowSize);
            this.fraShortcutInformation.Controls.Add(this.label6);
            this.fraShortcutInformation.Controls.Add(this.txtTarget);
            this.fraShortcutInformation.Controls.Add(this.label2);
            this.fraShortcutInformation.Controls.Add(this.txtComment);
            this.fraShortcutInformation.Controls.Add(this.txtShortcutKey);
            this.fraShortcutInformation.Controls.Add(this.txtStartIn);
            this.fraShortcutInformation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraShortcutInformation.Location = new System.Drawing.Point(3, 3);
            this.fraShortcutInformation.Name = "fraShortcutInformation";
            this.fraShortcutInformation.Size = new System.Drawing.Size(483, 224);
            this.fraShortcutInformation.TabIndex = 5;
            this.fraShortcutInformation.TabStop = false;
            this.fraShortcutInformation.Text = "Shortcut information";
            // 
            // fraAdvancedOptions
            // 
            this.fraAdvancedOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fraAdvancedOptions.Controls.Add(this.chkRunInSeparateMemorySpace);
            this.fraAdvancedOptions.Controls.Add(this.chkRunWithDifferentCredentials);
            this.fraAdvancedOptions.Location = new System.Drawing.Point(3, 233);
            this.fraAdvancedOptions.Name = "fraAdvancedOptions";
            this.fraAdvancedOptions.Size = new System.Drawing.Size(483, 43);
            this.fraAdvancedOptions.TabIndex = 6;
            this.fraAdvancedOptions.TabStop = false;
            this.fraAdvancedOptions.Text = "Advanced options";
            // 
            // chkRunInSeparateMemorySpace
            // 
            this.chkRunInSeparateMemorySpace.AutoSize = true;
            this.chkRunInSeparateMemorySpace.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkRunInSeparateMemorySpace.Location = new System.Drawing.Point(196, 19);
            this.chkRunInSeparateMemorySpace.Name = "chkRunInSeparateMemorySpace";
            this.chkRunInSeparateMemorySpace.Size = new System.Drawing.Size(178, 18);
            this.chkRunInSeparateMemorySpace.TabIndex = 0;
            this.chkRunInSeparateMemorySpace.Text = "Run in separate memory space";
            this.chkRunInSeparateMemorySpace.UseVisualStyleBackColor = true;
            // 
            // chkRunWithDifferentCredentials
            // 
            this.chkRunWithDifferentCredentials.AutoSize = true;
            this.chkRunWithDifferentCredentials.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkRunWithDifferentCredentials.Location = new System.Drawing.Point(21, 19);
            this.chkRunWithDifferentCredentials.Name = "chkRunWithDifferentCredentials";
            this.chkRunWithDifferentCredentials.Size = new System.Drawing.Size(169, 18);
            this.chkRunWithDifferentCredentials.TabIndex = 0;
            this.chkRunWithDifferentCredentials.Text = "Run with different credentials";
            this.chkRunWithDifferentCredentials.UseVisualStyleBackColor = true;
            // 
            // fraIcon
            // 
            this.fraIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fraIcon.Controls.Add(this.lvIcons);
            this.fraIcon.Controls.Add(this.txtIconFileName);
            this.fraIcon.Controls.Add(this.button1);
            this.fraIcon.Controls.Add(this.label7);
            this.fraIcon.Location = new System.Drawing.Point(3, 282);
            this.fraIcon.Name = "fraIcon";
            this.fraIcon.Size = new System.Drawing.Size(483, 114);
            this.fraIcon.TabIndex = 7;
            this.fraIcon.TabStop = false;
            this.fraIcon.Text = "Icon";
            // 
            // lvIcons
            // 
            this.lvIcons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvIcons.Location = new System.Drawing.Point(94, 47);
            this.lvIcons.Name = "lvIcons";
            this.lvIcons.Size = new System.Drawing.Size(383, 61);
            this.lvIcons.TabIndex = 5;
            this.lvIcons.UseCompatibleStateImageBehavior = false;
            // 
            // txtIconFileName
            // 
            this.txtIconFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIconFileName.Location = new System.Drawing.Point(94, 21);
            this.txtIconFileName.Name = "txtIconFileName";
            this.txtIconFileName.ReadOnly = true;
            this.txtIconFileName.Size = new System.Drawing.Size(302, 20);
            this.txtIconFileName.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(402, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "&Browse...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Location = new System.Drawing.Point(18, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "File name:";
            // 
            // WindowsShortcutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fraIcon);
            this.Controls.Add(this.fraAdvancedOptions);
            this.Controls.Add(this.fraShortcutInformation);
            this.Name = "WindowsShortcutEditor";
            this.Size = new System.Drawing.Size(489, 399);
            this.fraShortcutInformation.ResumeLayout(false);
            this.fraShortcutInformation.PerformLayout();
            this.fraAdvancedOptions.ResumeLayout(false);
            this.fraAdvancedOptions.PerformLayout();
            this.fraIcon.ResumeLayout(false);
            this.fraIcon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTargetType;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdBrowseTarget;
        private System.Windows.Forms.TextBox txtStartIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtShortcutKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboWindowSize;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox fraShortcutInformation;
        private System.Windows.Forms.GroupBox fraAdvancedOptions;
        private System.Windows.Forms.CheckBox chkRunWithDifferentCredentials;
        private System.Windows.Forms.CheckBox chkRunInSeparateMemorySpace;
        private System.Windows.Forms.GroupBox fraIcon;
        private System.Windows.Forms.ListView lvIcons;
        private System.Windows.Forms.TextBox txtIconFileName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;

    }
}
