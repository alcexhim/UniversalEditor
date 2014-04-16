using System.Drawing;
namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
    partial class UnsavedDocumentsDialog
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
            this.lv = new AwesomeControls.ListView.ListViewControl();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdDiscardAll = new System.Windows.Forms.Button();
            this.cmdContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.AllowSorting = true;
            this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv.BackColor = System.Drawing.SystemColors.Window;
            this.lv.DefaultItemHeight = 24;
            this.lv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lv.FullRowSelect = true;
            this.lv.LargeImageList = null;
            this.lv.Location = new System.Drawing.Point(12, 45);
            this.lv.Mode = AwesomeControls.ListView.ListViewMode.Details;
            this.lv.Name = "lv";
            this.lv.ShadeColor = System.Drawing.Color.WhiteSmoke;
            this.lv.Size = new System.Drawing.Size(387, 130);
            this.lv.SmallImageList = null;
            this.lv.SortColumn = null;
            this.lv.TabIndex = 1;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancel.Location = new System.Drawing.Point(324, 194);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdDiscardAll
            // 
            this.cmdDiscardAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDiscardAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdDiscardAll.Location = new System.Drawing.Point(243, 194);
            this.cmdDiscardAll.Name = "cmdDiscardAll";
            this.cmdDiscardAll.Size = new System.Drawing.Size(75, 23);
            this.cmdDiscardAll.TabIndex = 3;
            this.cmdDiscardAll.Text = "Discard All";
            this.cmdDiscardAll.UseVisualStyleBackColor = true;
            this.cmdDiscardAll.Click += new System.EventHandler(this.cmdDiscard_Click);
            // 
            // cmdContinue
            // 
            this.cmdContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdContinue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdContinue.Location = new System.Drawing.Point(162, 194);
            this.cmdContinue.Name = "cmdContinue";
            this.cmdContinue.Size = new System.Drawing.Size(75, 23);
            this.cmdContinue.TabIndex = 2;
            this.cmdContinue.Text = "Continue";
            this.cmdContinue.UseVisualStyleBackColor = true;
            this.cmdContinue.Click += new System.EventHandler(this.cmdContinue_Click);
            // 
            // UnsavedDocumentsDialog
            // 
            this.AcceptButton = this.cmdContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(411, 229);
            this.Controls.Add(this.cmdContinue);
            this.Controls.Add(this.cmdDiscardAll);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lv);
            this.Name = "UnsavedDocumentsDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Unsaved Documents";
            this.ResumeLayout(false);

        }

        #endregion

        internal AwesomeControls.ListView.ListViewControl lv;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdDiscardAll;
        private System.Windows.Forms.Button cmdContinue;
    }
}