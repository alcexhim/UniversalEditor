using System.Drawing;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Pages
{
    partial class StartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblApplicationTitle = new System.Windows.Forms.Label();
            this.pnlSide = new System.Windows.Forms.Panel();
            this.lvRecent = new AwesomeControls.ListView.ListViewControl();
            this.chkShowStartPageAtStartup = new System.Windows.Forms.CheckBox();
            this.lblOpenProject = new AwesomeControls.Label.LabelControl();
            this.lblNewProject = new AwesomeControls.Label.LabelControl();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.pnlSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlTop.Controls.Add(this.picIcon);
            this.pnlTop.Controls.Add(this.lblApplicationTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(813, 74);
            this.pnlTop.TabIndex = 0;
            // 
            // picIcon
            // 
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(3, 5);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(64, 64);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 1;
            this.picIcon.TabStop = false;
            // 
            // lblApplicationTitle
            // 
            this.lblApplicationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApplicationTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblApplicationTitle.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblApplicationTitle.Location = new System.Drawing.Point(73, 15);
            this.lblApplicationTitle.Name = "lblApplicationTitle";
            this.lblApplicationTitle.Size = new System.Drawing.Size(725, 37);
            this.lblApplicationTitle.TabIndex = 0;
            this.lblApplicationTitle.Text = "Universal Editor";
            // 
            // pnlSide
            // 
            this.pnlSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.pnlSide.Controls.Add(this.lvRecent);
            this.pnlSide.Controls.Add(this.chkShowStartPageAtStartup);
            this.pnlSide.Controls.Add(this.lblOpenProject);
            this.pnlSide.Controls.Add(this.lblNewProject);
            this.pnlSide.Controls.Add(this.label5);
            this.pnlSide.Controls.Add(this.label2);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSide.Location = new System.Drawing.Point(0, 74);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(264, 371);
            this.pnlSide.TabIndex = 1;
            // 
            // lvRecent
            // 
            this.lvRecent.AllowSorting = true;
            this.lvRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRecent.BackColor = System.Drawing.SystemColors.Window;
            this.lvRecent.DefaultItemHeight = 24;
            this.lvRecent.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lvRecent.FullRowSelect = true;
            this.lvRecent.HideSelection = false;
            this.lvRecent.LargeImageList = null;
            this.lvRecent.Location = new System.Drawing.Point(24, 130);
            this.lvRecent.Mode = AwesomeControls.ListView.ListViewMode.List;
            this.lvRecent.Name = "lvRecent";
            this.lvRecent.ShadeColor = System.Drawing.Color.WhiteSmoke;
            this.lvRecent.Size = new System.Drawing.Size(218, 199);
            this.lvRecent.SmallImageList = null;
            this.lvRecent.SortColumn = null;
            this.lvRecent.TabIndex = 3;
            this.lvRecent.ItemActivate += new System.EventHandler(this.lvRecent_ItemActivate);
            // 
            // chkShowStartPageAtStartup
            // 
            this.chkShowStartPageAtStartup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowStartPageAtStartup.Checked = true;
            this.chkShowStartPageAtStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowStartPageAtStartup.Location = new System.Drawing.Point(24, 337);
            this.chkShowStartPageAtStartup.Name = "chkShowStartPageAtStartup";
            this.chkShowStartPageAtStartup.Size = new System.Drawing.Size(219, 21);
            this.chkShowStartPageAtStartup.TabIndex = 1;
            this.chkShowStartPageAtStartup.Text = "Show this page at &startup";
            this.chkShowStartPageAtStartup.UseVisualStyleBackColor = true;
            // 
            // lblOpenProject
            // 
            this.lblOpenProject.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblOpenProject.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOpenProject.HotTrack = true;
            this.lblOpenProject.HoverColor = System.Drawing.Color.Red;
            this.lblOpenProject.Location = new System.Drawing.Point(21, 70);
            this.lblOpenProject.Name = "lblOpenProject";
            this.lblOpenProject.Size = new System.Drawing.Size(222, 20);
            this.lblOpenProject.TabIndex = 0;
            this.lblOpenProject.Text = "Open Existing Project...";
            this.lblOpenProject.Click += new System.EventHandler(this.lblOpenProject_Click);
            // 
            // lblNewProject
            // 
            this.lblNewProject.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblNewProject.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNewProject.HotTrack = true;
            this.lblNewProject.HoverColor = System.Drawing.Color.Red;
            this.lblNewProject.Location = new System.Drawing.Point(21, 50);
            this.lblNewProject.Name = "lblNewProject";
            this.lblNewProject.Size = new System.Drawing.Size(221, 20);
            this.lblNewProject.TabIndex = 0;
            this.lblNewProject.Text = "Create New Project...";
            this.lblNewProject.Click += new System.EventHandler(this.lblNewProject_Click);
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.label5.Location = new System.Drawing.Point(20, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(222, 26);
            this.label5.TabIndex = 0;
            this.label5.Text = "Recent items";
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.label2.Location = new System.Drawing.Point(20, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "Get started";
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.Controls.Add(this.pnlSide);
            this.Controls.Add(this.pnlTop);
            this.Name = "StartPage";
            this.Size = new System.Drawing.Size(813, 445);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.pnlSide.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblApplicationTitle;
        private System.Windows.Forms.Panel pnlSide;
        private AwesomeControls.Label.LabelControl lblNewProject;
        private System.Windows.Forms.Label label2;
        private AwesomeControls.Label.LabelControl lblOpenProject;
        private System.Windows.Forms.CheckBox chkShowStartPageAtStartup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picIcon;
        private AwesomeControls.ListView.ListViewControl lvRecent;
    }
}
