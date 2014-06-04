namespace UniversalEditor.Editors.NewWorldComputing.Map
{
    partial class MapEditor
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
            this.fraPreview = new System.Windows.Forms.GroupBox();
            this.pnlMiniMap = new System.Windows.Forms.Panel();
            this.fraPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // fraPreview
            // 
            this.fraPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fraPreview.Controls.Add(this.pnlMiniMap);
            this.fraPreview.Location = new System.Drawing.Point(521, 3);
            this.fraPreview.Name = "fraPreview";
            this.fraPreview.Size = new System.Drawing.Size(200, 212);
            this.fraPreview.TabIndex = 0;
            this.fraPreview.TabStop = false;
            this.fraPreview.Text = "Preview";
            // 
            // pnlMiniMap
            // 
            this.pnlMiniMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMiniMap.BackColor = System.Drawing.Color.Black;
            this.pnlMiniMap.Location = new System.Drawing.Point(6, 19);
            this.pnlMiniMap.Name = "pnlMiniMap";
            this.pnlMiniMap.Size = new System.Drawing.Size(188, 187);
            this.pnlMiniMap.TabIndex = 0;
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fraPreview);
            this.Name = "MapEditor";
            this.Size = new System.Drawing.Size(724, 372);
            this.fraPreview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fraPreview;
        private System.Windows.Forms.Panel pnlMiniMap;
    }
}
