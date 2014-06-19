namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
    partial class DocumentPropertiesDialogBase
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
            this.components = new System.ComponentModel.Container();
            this.txtObjectModel = new System.Windows.Forms.TextBox();
            this.cmdObjectModel = new System.Windows.Forms.Button();
            this.txtDataFormat = new System.Windows.Forms.TextBox();
            this.cmdDataFormat = new System.Windows.Forms.Button();
            this.txtAccessor = new System.Windows.Forms.TextBox();
            this.cmdAccessor = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.fraDataFormatOptions = new System.Windows.Forms.GroupBox();
            this.mnuAccessor = new AwesomeControls.CommandBars.CBContextMenu(this.components);
            this.mnuAccessorLocalFile = new System.Windows.Forms.ToolStripMenuItem();
            this.fTPServerToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.hTTPServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fTPServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccessor.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtObjectModel
            // 
            this.txtObjectModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjectModel.Location = new System.Drawing.Point(127, 14);
            this.txtObjectModel.Name = "txtObjectModel";
            this.txtObjectModel.ReadOnly = true;
            this.txtObjectModel.Size = new System.Drawing.Size(218, 20);
            this.txtObjectModel.TabIndex = 1;
            // 
            // cmdObjectModel
            // 
            this.cmdObjectModel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdObjectModel.Location = new System.Drawing.Point(12, 12);
            this.cmdObjectModel.Name = "cmdObjectModel";
            this.cmdObjectModel.Size = new System.Drawing.Size(109, 23);
            this.cmdObjectModel.TabIndex = 0;
            this.cmdObjectModel.Text = "Object &model:";
            this.cmdObjectModel.UseVisualStyleBackColor = true;
            this.cmdObjectModel.Click += new System.EventHandler(this.cmdObjectModel_Click);
            // 
            // txtDataFormat
            // 
            this.txtDataFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataFormat.Location = new System.Drawing.Point(127, 43);
            this.txtDataFormat.Name = "txtDataFormat";
            this.txtDataFormat.ReadOnly = true;
            this.txtDataFormat.Size = new System.Drawing.Size(218, 20);
            this.txtDataFormat.TabIndex = 3;
            // 
            // cmdDataFormat
            // 
            this.cmdDataFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdDataFormat.Location = new System.Drawing.Point(12, 41);
            this.cmdDataFormat.Name = "cmdDataFormat";
            this.cmdDataFormat.Size = new System.Drawing.Size(109, 23);
            this.cmdDataFormat.TabIndex = 2;
            this.cmdDataFormat.Text = "Data &format:";
            this.cmdDataFormat.UseVisualStyleBackColor = true;
            this.cmdDataFormat.Click += new System.EventHandler(this.cmdDataFormat_Click);
            // 
            // txtAccessor
            // 
            this.txtAccessor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessor.Location = new System.Drawing.Point(127, 72);
            this.txtAccessor.Name = "txtAccessor";
            this.txtAccessor.ReadOnly = true;
            this.txtAccessor.Size = new System.Drawing.Size(218, 20);
            this.txtAccessor.TabIndex = 5;
            // 
            // cmdAccessor
            // 
            this.cmdAccessor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdAccessor.Location = new System.Drawing.Point(12, 70);
            this.cmdAccessor.Name = "cmdAccessor";
            this.cmdAccessor.Size = new System.Drawing.Size(109, 23);
            this.cmdAccessor.TabIndex = 4;
            this.cmdAccessor.Text = "&Accessor:";
            this.cmdAccessor.UseVisualStyleBackColor = true;
            this.cmdAccessor.Click += new System.EventHandler(this.cmdAccessor_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancel.Location = new System.Drawing.Point(270, 191);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Enabled = false;
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdOK.Location = new System.Drawing.Point(189, 191);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 7;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // fraDataFormatOptions
            // 
            this.fraDataFormatOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraDataFormatOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraDataFormatOptions.Location = new System.Drawing.Point(12, 99);
            this.fraDataFormatOptions.Name = "fraDataFormatOptions";
            this.fraDataFormatOptions.Size = new System.Drawing.Size(333, 86);
            this.fraDataFormatOptions.TabIndex = 6;
            this.fraDataFormatOptions.TabStop = false;
            this.fraDataFormatOptions.Text = "Data format options";
            // 
            // mnuAccessor
            // 
            this.mnuAccessor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAccessorLocalFile,
            this.fTPServerToolStripMenuItem,
            this.hTTPServerToolStripMenuItem,
            this.fTPServerToolStripMenuItem1});
            this.mnuAccessor.Name = "mnuAccessor";
            this.mnuAccessor.Size = new System.Drawing.Size(135, 76);
            // 
            // mnuAccessorLocalFile
            // 
            this.mnuAccessorLocalFile.Name = "mnuAccessorLocalFile";
            this.mnuAccessorLocalFile.Size = new System.Drawing.Size(134, 22);
            this.mnuAccessorLocalFile.Text = "&Local File";
            this.mnuAccessorLocalFile.Click += new System.EventHandler(this.mnuAccessorLocalFile_Click);
            // 
            // fTPServerToolStripMenuItem
            // 
            this.fTPServerToolStripMenuItem.Name = "fTPServerToolStripMenuItem";
            this.fTPServerToolStripMenuItem.Size = new System.Drawing.Size(131, 6);
            // 
            // hTTPServerToolStripMenuItem
            // 
            this.hTTPServerToolStripMenuItem.Name = "hTTPServerToolStripMenuItem";
            this.hTTPServerToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.hTTPServerToolStripMenuItem.Text = "&HTTP Server";
            // 
            // fTPServerToolStripMenuItem1
            // 
            this.fTPServerToolStripMenuItem1.Name = "fTPServerToolStripMenuItem1";
            this.fTPServerToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.fTPServerToolStripMenuItem1.Text = "&FTP Server";
            // 
            // DocumentPropertiesDialogBase
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(357, 226);
            this.Controls.Add(this.fraDataFormatOptions);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccessor);
            this.Controls.Add(this.txtAccessor);
            this.Controls.Add(this.cmdDataFormat);
            this.Controls.Add(this.txtDataFormat);
            this.Controls.Add(this.cmdObjectModel);
            this.Controls.Add(this.txtObjectModel);
            this.Name = "DocumentPropertiesDialogBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Document Properties";
            this.mnuAccessor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtObjectModel;
        private System.Windows.Forms.Button cmdObjectModel;
        private System.Windows.Forms.TextBox txtDataFormat;
        private System.Windows.Forms.Button cmdDataFormat;
        private System.Windows.Forms.TextBox txtAccessor;
        private System.Windows.Forms.Button cmdAccessor;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.GroupBox fraDataFormatOptions;
        private AwesomeControls.CommandBars.CBContextMenu mnuAccessor;
        private System.Windows.Forms.ToolStripMenuItem mnuAccessorLocalFile;
        private System.Windows.Forms.ToolStripSeparator fTPServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTTPServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fTPServerToolStripMenuItem1;

    }
}