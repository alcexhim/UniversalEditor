namespace UniversalEditor.UserInterface.WindowsForms.Controls
{
    partial class DocumentTypeSelector
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
            this.components = new System.ComponentModel.Container();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lvDataFormats = new System.Windows.Forms.ListView();
            this.chDataFormatTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDataFormatDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.tvObjectModels = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(461, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lvDataFormats
            // 
            this.lvDataFormats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDataFormatTitle,
            this.chDataFormatDescription});
            this.lvDataFormats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDataFormats.FullRowSelect = true;
            this.lvDataFormats.GridLines = true;
            this.lvDataFormats.HideSelection = false;
            this.lvDataFormats.LargeImageList = this.imlLargeIcons;
            this.lvDataFormats.Location = new System.Drawing.Point(0, 20);
            this.lvDataFormats.MultiSelect = false;
            this.lvDataFormats.Name = "lvDataFormats";
            this.lvDataFormats.Size = new System.Drawing.Size(461, 232);
            this.lvDataFormats.SmallImageList = this.imlSmallIcons;
            this.lvDataFormats.TabIndex = 1;
            this.lvDataFormats.UseCompatibleStateImageBehavior = false;
            this.lvDataFormats.View = System.Windows.Forms.View.Details;
            this.lvDataFormats.SelectedIndexChanged += new System.EventHandler(this.lvDataFormats_SelectedIndexChanged);
            // 
            // chDataFormatTitle
            // 
            this.chDataFormatTitle.Text = "Title";
            this.chDataFormatTitle.Width = 278;
            // 
            // chDataFormatDescription
            // 
            this.chDataFormatDescription.Text = "Description";
            this.chDataFormatDescription.Width = 172;
            // 
            // imlLargeIcons
            // 
            this.imlLargeIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlLargeIcons.ImageSize = new System.Drawing.Size(32, 32);
            this.imlLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imlSmallIcons
            // 
            this.imlSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tvObjectModels
            // 
            this.tvObjectModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvObjectModels.HideSelection = false;
            this.tvObjectModels.ImageIndex = 0;
            this.tvObjectModels.ImageList = this.imlSmallIcons;
            this.tvObjectModels.Location = new System.Drawing.Point(0, 20);
            this.tvObjectModels.Name = "tvObjectModels";
            this.tvObjectModels.SelectedImageIndex = 0;
            this.tvObjectModels.Size = new System.Drawing.Size(461, 232);
            this.tvObjectModels.TabIndex = 2;
            this.tvObjectModels.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvObjectModels_AfterCollapse);
            this.tvObjectModels.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvObjectModels_AfterExpand);
            this.tvObjectModels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObjectModels_AfterSelect);
            this.tvObjectModels.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvObjectModels_MouseDoubleClick);
            // 
            // DocumentTypeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvObjectModels);
            this.Controls.Add(this.lvDataFormats);
            this.Controls.Add(this.txtSearch);
            this.Name = "DocumentTypeSelector";
            this.Size = new System.Drawing.Size(461, 252);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListView lvDataFormats;
        private System.Windows.Forms.ColumnHeader chDataFormatTitle;
        private System.Windows.Forms.ColumnHeader chDataFormatDescription;
        private System.Windows.Forms.TreeView tvObjectModels;
        private System.Windows.Forms.ImageList imlLargeIcons;
        private System.Windows.Forms.ImageList imlSmallIcons;
    }
}
