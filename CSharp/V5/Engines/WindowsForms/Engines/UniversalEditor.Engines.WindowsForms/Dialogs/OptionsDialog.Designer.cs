using System.Drawing;
namespace UniversalEditor.Engines.WindowsForms.Dialogs
{
    partial class OptionsDialog
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
			this.tv = new System.Windows.Forms.TreeView();
			this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.pnlContainer = new System.Windows.Forms.Panel();
			this.sc = new System.Windows.Forms.SplitContainer();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.sc.Panel1.SuspendLayout();
			this.sc.Panel2.SuspendLayout();
			this.sc.SuspendLayout();
			this.SuspendLayout();
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.ImageIndex = 0;
			this.tv.ImageList = this.imlSmallIcons;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.SelectedImageIndex = 0;
			this.tv.Size = new System.Drawing.Size(150, 223);
			this.tv.TabIndex = 0;
			this.tv.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCollapse);
			this.tv.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterExpand);
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// imlSmallIcons
			// 
			this.imlSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pnlContainer
			// 
			this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContainer.Location = new System.Drawing.Point(0, 0);
			this.pnlContainer.Name = "pnlContainer";
			this.pnlContainer.Size = new System.Drawing.Size(296, 223);
			this.pnlContainer.TabIndex = 1;
			// 
			// sc
			// 
			this.sc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sc.Location = new System.Drawing.Point(12, 12);
			this.sc.Name = "sc";
			// 
			// sc.Panel1
			// 
			this.sc.Panel1.Controls.Add(this.tv);
			// 
			// sc.Panel2
			// 
			this.sc.Panel2.Controls.Add(this.pnlContainer);
			this.sc.Size = new System.Drawing.Size(450, 223);
			this.sc.SplitterDistance = 150;
			this.sc.TabIndex = 2;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(387, 253);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(306, 253);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// OptionsDialog
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(474, 288);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.sc);
			this.Name = "OptionsDialog";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.sc.Panel1.ResumeLayout(false);
			this.sc.Panel2.ResumeLayout(false);
			this.sc.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.SplitContainer sc;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.ImageList imlSmallIcons;

    }
}