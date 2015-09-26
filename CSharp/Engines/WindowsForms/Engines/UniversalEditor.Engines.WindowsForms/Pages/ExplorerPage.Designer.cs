using UniversalEditor.UserInterface.WindowsForms.Controls;
namespace UniversalEditor.Engines.WindowsForms.Pages
{
    partial class ExplorerPage
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
            this.explorer = new LocalFileSystemExplorer();
            this.SuspendLayout();
            // 
            // explorer
            // 
            this.explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorer.Location = new System.Drawing.Point(0, 0);
            this.explorer.Name = "explorer";
            this.explorer.Path = "";
            this.explorer.ShowDetails = false;
            this.explorer.ShowFavorites = false;
            this.explorer.ShowPreview = false;
            this.explorer.Size = new System.Drawing.Size(700, 372);
            this.explorer.TabIndex = 0;
            this.explorer.Navigate += new NavigateEventHandler(this.explorer_Navigate);
            // 
            // ExplorerPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.explorer);
            this.Name = "ExplorerPage";
            this.Size = new System.Drawing.Size(700, 372);
            this.ResumeLayout(false);

        }

        #endregion

        private LocalFileSystemExplorer explorer;

    }
}
