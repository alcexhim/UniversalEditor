namespace UniversalEditor.UserInterface.WindowsForms.Controls
{
    partial class LocalFileSystemExplorer
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
            this.scExplorerFiles = new System.Windows.Forms.SplitContainer();
            this.scFavoritesFiles = new System.Windows.Forms.SplitContainer();
            this.tvFavorites = new AwesomeControls.ListView.ListViewControl();
            this.tv = new AwesomeControls.ListView.ListViewControl();
            this.imlLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.scFilesPreview = new System.Windows.Forms.SplitContainer();
            this.lv = new AwesomeControls.ListView.ListViewControl();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.scFilesDetails = new System.Windows.Forms.SplitContainer();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.scExplorerFiles.Panel1.SuspendLayout();
            this.scExplorerFiles.Panel2.SuspendLayout();
            this.scExplorerFiles.SuspendLayout();
            this.scFavoritesFiles.Panel1.SuspendLayout();
            this.scFavoritesFiles.Panel2.SuspendLayout();
            this.scFavoritesFiles.SuspendLayout();
            this.scFilesPreview.Panel1.SuspendLayout();
            this.scFilesPreview.Panel2.SuspendLayout();
            this.scFilesPreview.SuspendLayout();
            this.scFilesDetails.Panel1.SuspendLayout();
            this.scFilesDetails.Panel2.SuspendLayout();
            this.scFilesDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // scExplorerFiles
            // 
            this.scExplorerFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scExplorerFiles.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scExplorerFiles.Location = new System.Drawing.Point(0, 0);
            this.scExplorerFiles.Name = "scExplorerFiles";
            // 
            // scExplorerFiles.Panel1
            // 
            this.scExplorerFiles.Panel1.Controls.Add(this.scFavoritesFiles);
            // 
            // scExplorerFiles.Panel2
            // 
            this.scExplorerFiles.Panel2.Controls.Add(this.scFilesPreview);
            this.scExplorerFiles.Size = new System.Drawing.Size(634, 398);
            this.scExplorerFiles.SplitterDistance = 185;
            this.scExplorerFiles.TabIndex = 0;
            // 
            // scFavoritesFiles
            // 
            this.scFavoritesFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFavoritesFiles.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scFavoritesFiles.Location = new System.Drawing.Point(0, 0);
            this.scFavoritesFiles.Name = "scFavoritesFiles";
            this.scFavoritesFiles.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scFavoritesFiles.Panel1
            // 
            this.scFavoritesFiles.Panel1.Controls.Add(this.tvFavorites);
            this.scFavoritesFiles.Panel1Collapsed = true;
            // 
            // scFavoritesFiles.Panel2
            // 
            this.scFavoritesFiles.Panel2.Controls.Add(this.tv);
            this.scFavoritesFiles.Size = new System.Drawing.Size(185, 398);
            this.scFavoritesFiles.SplitterDistance = 118;
            this.scFavoritesFiles.TabIndex = 1;
            // 
            // tvFavorites
            // 
            this.tvFavorites.AllowSorting = true;
            this.tvFavorites.BackColor = System.Drawing.SystemColors.Window;
            this.tvFavorites.DefaultItemHeight = 24;
            this.tvFavorites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFavorites.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tvFavorites.LargeImageList = null;
            this.tvFavorites.Location = new System.Drawing.Point(0, 0);
            this.tvFavorites.Mode = AwesomeControls.ListView.ListViewMode.List;
            this.tvFavorites.Name = "tvFavorites";
            this.tvFavorites.ShadeColor = System.Drawing.Color.WhiteSmoke;
            this.tvFavorites.Size = new System.Drawing.Size(150, 118);
            this.tvFavorites.SmallImageList = null;
            this.tvFavorites.SortColumn = null;
            this.tvFavorites.TabIndex = 1;
            // 
            // tv
            // 
            this.tv.AllowSorting = true;
            this.tv.BackColor = System.Drawing.SystemColors.Window;
            this.tv.DefaultItemHeight = 24;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tv.LargeImageList = this.imlLargeIcons;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Mode = AwesomeControls.ListView.ListViewMode.List;
            this.tv.Name = "tv";
            this.tv.ShadeColor = System.Drawing.Color.WhiteSmoke;
            this.tv.Size = new System.Drawing.Size(185, 398);
            this.tv.SmallImageList = this.imlSmallIcons;
            this.tv.SortColumn = null;
            this.tv.TabIndex = 1;
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
            // scFilesPreview
            // 
            this.scFilesPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFilesPreview.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scFilesPreview.Location = new System.Drawing.Point(0, 0);
            this.scFilesPreview.Name = "scFilesPreview";
            // 
            // scFilesPreview.Panel1
            // 
            this.scFilesPreview.Panel1.Controls.Add(this.lv);
            // 
            // scFilesPreview.Panel2
            // 
            this.scFilesPreview.Panel2.Controls.Add(this.pnlPreview);
            this.scFilesPreview.Panel2Collapsed = true;
            this.scFilesPreview.Size = new System.Drawing.Size(445, 398);
            this.scFilesPreview.SplitterDistance = 326;
            this.scFilesPreview.TabIndex = 1;
            // 
            // lv
            // 
            this.lv.AllowSorting = true;
            this.lv.BackColor = System.Drawing.SystemColors.Window;
            this.lv.DefaultItemHeight = 24;
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lv.LargeImageList = this.imlLargeIcons;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.Mode = AwesomeControls.ListView.ListViewMode.Tiles;
            this.lv.Name = "lv";
            this.lv.ShadeColor = System.Drawing.Color.WhiteSmoke;
            this.lv.Size = new System.Drawing.Size(445, 398);
            this.lv.SmallImageList = this.imlSmallIcons;
            this.lv.SortColumn = null;
            this.lv.TabIndex = 1;
            this.lv.ItemActivate += new System.EventHandler(this.lv_ItemActivate);
            // 
            // pnlPreview
            // 
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(0, 0);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(96, 100);
            this.pnlPreview.TabIndex = 0;
            // 
            // scFilesDetails
            // 
            this.scFilesDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFilesDetails.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scFilesDetails.Location = new System.Drawing.Point(0, 0);
            this.scFilesDetails.Name = "scFilesDetails";
            this.scFilesDetails.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scFilesDetails.Panel1
            // 
            this.scFilesDetails.Panel1.Controls.Add(this.scExplorerFiles);
            // 
            // scFilesDetails.Panel2
            // 
            this.scFilesDetails.Panel2.Controls.Add(this.pnlDetails);
            this.scFilesDetails.Panel2Collapsed = true;
            this.scFilesDetails.Size = new System.Drawing.Size(634, 398);
            this.scFilesDetails.SplitterDistance = 294;
            this.scFilesDetails.TabIndex = 2;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(150, 46);
            this.pnlDetails.TabIndex = 0;
            // 
            // LocalFileSystemExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scFilesDetails);
            this.Name = "LocalFileSystemExplorer";
            this.Size = new System.Drawing.Size(634, 398);
            this.scExplorerFiles.Panel1.ResumeLayout(false);
            this.scExplorerFiles.Panel2.ResumeLayout(false);
            this.scExplorerFiles.ResumeLayout(false);
            this.scFavoritesFiles.Panel1.ResumeLayout(false);
            this.scFavoritesFiles.Panel2.ResumeLayout(false);
            this.scFavoritesFiles.ResumeLayout(false);
            this.scFilesPreview.Panel1.ResumeLayout(false);
            this.scFilesPreview.Panel2.ResumeLayout(false);
            this.scFilesPreview.ResumeLayout(false);
            this.scFilesDetails.Panel1.ResumeLayout(false);
            this.scFilesDetails.Panel2.ResumeLayout(false);
            this.scFilesDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scExplorerFiles;
        private System.Windows.Forms.SplitContainer scFavoritesFiles;
        private AwesomeControls.ListView.ListViewControl tvFavorites;
        private AwesomeControls.ListView.ListViewControl tv;
        private System.Windows.Forms.SplitContainer scFilesDetails;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.SplitContainer scFilesPreview;
        private AwesomeControls.ListView.ListViewControl lv;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.ImageList imlSmallIcons;
        private System.Windows.Forms.ImageList imlLargeIcons;
    }
}
