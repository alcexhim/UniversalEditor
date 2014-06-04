using System.Drawing;
namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	partial class NewDialogBase
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
			this.sc = new System.Windows.Forms.SplitContainer();
			this.fraDocumentType = new System.Windows.Forms.GroupBox();
			this.dts = new UniversalEditor.UserInterface.WindowsForms.Controls.DocumentTypeSelector();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lvFileTemplates = new AwesomeControls.ListView.ListViewControl();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.pnlNewFile = new System.Windows.Forms.Panel();
			this.pnlNewProject = new System.Windows.Forms.Panel();
			this.scNewProject = new System.Windows.Forms.SplitContainer();
			this.tvProject = new System.Windows.Forms.TreeView();
			this.lvProjectTemplates = new AwesomeControls.ListView.ListViewControl();
			this.optSeparate = new System.Windows.Forms.RadioButton();
			this.optCombine = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.txtProjectTitle = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSolutionTitle = new System.Windows.Forms.TextBox();
			this.sc.Panel1.SuspendLayout();
			this.sc.Panel2.SuspendLayout();
			this.sc.SuspendLayout();
			this.fraDocumentType.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.pnlNewFile.SuspendLayout();
			this.pnlNewProject.SuspendLayout();
			this.scNewProject.Panel1.SuspendLayout();
			this.scNewProject.Panel2.SuspendLayout();
			this.scNewProject.SuspendLayout();
			this.SuspendLayout();
			// 
			// sc
			// 
			this.sc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sc.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.sc.Location = new System.Drawing.Point(0, 0);
			this.sc.Name = "sc";
			// 
			// sc.Panel1
			// 
			this.sc.Panel1.Controls.Add(this.fraDocumentType);
			// 
			// sc.Panel2
			// 
			this.sc.Panel2.Controls.Add(this.groupBox1);
			this.sc.Size = new System.Drawing.Size(482, 160);
			this.sc.SplitterDistance = 188;
			this.sc.TabIndex = 0;
			// 
			// fraDocumentType
			// 
			this.fraDocumentType.Controls.Add(this.dts);
			this.fraDocumentType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fraDocumentType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraDocumentType.Location = new System.Drawing.Point(0, 0);
			this.fraDocumentType.Name = "fraDocumentType";
			this.fraDocumentType.Size = new System.Drawing.Size(188, 160);
			this.fraDocumentType.TabIndex = 0;
			this.fraDocumentType.TabStop = false;
			this.fraDocumentType.Text = "Document type";
			// 
			// dts
			// 
			this.dts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dts.Location = new System.Drawing.Point(3, 16);
			this.dts.Name = "dts";
			this.dts.ObjectType = UniversalEditor.UserInterface.WindowsForms.Controls.DocumentTypeSelectorObjectTypes.ObjectModel;
			this.dts.Padding = new System.Windows.Forms.Padding(6);
			this.dts.Size = new System.Drawing.Size(182, 141);
			this.dts.TabIndex = 0;
			this.dts.SelectionChanged += new System.EventHandler(this.dts_SelectionChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lvFileTemplates);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(290, 160);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Available templates";
			// 
			// lvFileTemplates
			// 
			this.lvFileTemplates.AllowSorting = true;
			this.lvFileTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvFileTemplates.BackColor = System.Drawing.SystemColors.Window;
			this.lvFileTemplates.DefaultItemHeight = 24;
			this.lvFileTemplates.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lvFileTemplates.FullRowSelect = true;
			this.lvFileTemplates.HideSelection = false;
			this.lvFileTemplates.LargeImageList = null;
			this.lvFileTemplates.Location = new System.Drawing.Point(6, 19);
			this.lvFileTemplates.Mode = AwesomeControls.ListView.ListViewMode.LargeIcons;
			this.lvFileTemplates.Name = "lvFileTemplates";
			this.lvFileTemplates.ShadeColor = System.Drawing.Color.WhiteSmoke;
			this.lvFileTemplates.Size = new System.Drawing.Size(278, 135);
			this.lvFileTemplates.SmallImageList = null;
			this.lvFileTemplates.SortColumn = null;
			this.lvFileTemplates.TabIndex = 0;
			this.lvFileTemplates.SelectionChanged += new System.EventHandler(this.lvFileTemplates_SelectionChanged);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(419, 278);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(338, 278);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// pnlNewFile
			// 
			this.pnlNewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlNewFile.Controls.Add(this.sc);
			this.pnlNewFile.Location = new System.Drawing.Point(12, 12);
			this.pnlNewFile.Name = "pnlNewFile";
			this.pnlNewFile.Size = new System.Drawing.Size(482, 160);
			this.pnlNewFile.TabIndex = 3;
			// 
			// pnlNewProject
			// 
			this.pnlNewProject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlNewProject.Controls.Add(this.scNewProject);
			this.pnlNewProject.Location = new System.Drawing.Point(12, 12);
			this.pnlNewProject.Name = "pnlNewProject";
			this.pnlNewProject.Size = new System.Drawing.Size(482, 160);
			this.pnlNewProject.TabIndex = 4;
			// 
			// scNewProject
			// 
			this.scNewProject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scNewProject.Location = new System.Drawing.Point(0, 0);
			this.scNewProject.Name = "scNewProject";
			// 
			// scNewProject.Panel1
			// 
			this.scNewProject.Panel1.Controls.Add(this.tvProject);
			// 
			// scNewProject.Panel2
			// 
			this.scNewProject.Panel2.Controls.Add(this.lvProjectTemplates);
			this.scNewProject.Size = new System.Drawing.Size(482, 160);
			this.scNewProject.SplitterDistance = 160;
			this.scNewProject.TabIndex = 0;
			// 
			// tvProject
			// 
			this.tvProject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvProject.HideSelection = false;
			this.tvProject.Location = new System.Drawing.Point(0, 0);
			this.tvProject.Name = "tvProject";
			this.tvProject.Size = new System.Drawing.Size(160, 160);
			this.tvProject.TabIndex = 0;
			this.tvProject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProject_AfterSelect);
			// 
			// lvProjectTemplates
			// 
			this.lvProjectTemplates.AllowSorting = true;
			this.lvProjectTemplates.BackColor = System.Drawing.SystemColors.Window;
			this.lvProjectTemplates.DefaultItemHeight = 24;
			this.lvProjectTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvProjectTemplates.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lvProjectTemplates.LargeImageList = null;
			this.lvProjectTemplates.Location = new System.Drawing.Point(0, 0);
			this.lvProjectTemplates.Mode = AwesomeControls.ListView.ListViewMode.Tiles;
			this.lvProjectTemplates.Name = "lvProjectTemplates";
			this.lvProjectTemplates.ShadeColor = System.Drawing.Color.WhiteSmoke;
			this.lvProjectTemplates.Size = new System.Drawing.Size(318, 160);
			this.lvProjectTemplates.SmallImageList = null;
			this.lvProjectTemplates.SortColumn = null;
			this.lvProjectTemplates.TabIndex = 0;
			this.lvProjectTemplates.SelectionChanged += new System.EventHandler(this.lvProjectTemplates_SelectionChanged);
			// 
			// optSeparate
			// 
			this.optSeparate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.optSeparate.Checked = true;
			this.optSeparate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optSeparate.Location = new System.Drawing.Point(12, 178);
			this.optSeparate.Name = "optSeparate";
			this.optSeparate.Size = new System.Drawing.Size(482, 18);
			this.optSeparate.TabIndex = 0;
			this.optSeparate.TabStop = true;
			this.optSeparate.Text = "&Create a new solution for this project";
			// 
			// optCombine
			// 
			this.optCombine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.optCombine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optCombine.Location = new System.Drawing.Point(12, 202);
			this.optCombine.Name = "optCombine";
			this.optCombine.Size = new System.Drawing.Size(482, 18);
			this.optCombine.TabIndex = 1;
			this.optCombine.Text = "&Add this project to an existing solution";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 229);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "&Project title:";
			// 
			// txtProjectTitle
			// 
			this.txtProjectTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtProjectTitle.Location = new System.Drawing.Point(115, 226);
			this.txtProjectTitle.Name = "txtProjectTitle";
			this.txtProjectTitle.Size = new System.Drawing.Size(379, 20);
			this.txtProjectTitle.TabIndex = 6;
			this.txtProjectTitle.TextChanged += new System.EventHandler(this.txtProjectTitle_TextChanged);
			this.txtProjectTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProjectTitle_KeyPress);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(42, 255);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "&Solution title:";
			// 
			// txtSolutionTitle
			// 
			this.txtSolutionTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSolutionTitle.Location = new System.Drawing.Point(115, 252);
			this.txtSolutionTitle.Name = "txtSolutionTitle";
			this.txtSolutionTitle.Size = new System.Drawing.Size(379, 20);
			this.txtSolutionTitle.TabIndex = 6;
			this.txtSolutionTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSolutionTitle_KeyPress);
			// 
			// NewDialogBase
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 313);
			this.Controls.Add(this.txtSolutionTitle);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtProjectTitle);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.optCombine);
			this.Controls.Add(this.optSeparate);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.pnlNewFile);
			this.Controls.Add(this.pnlNewProject);
			this.Name = "NewDialogBase";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create New";
			this.UseThemeBackground = false;
			this.sc.Panel1.ResumeLayout(false);
			this.sc.Panel2.ResumeLayout(false);
			this.sc.ResumeLayout(false);
			this.fraDocumentType.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.pnlNewFile.ResumeLayout(false);
			this.pnlNewProject.ResumeLayout(false);
			this.scNewProject.Panel1.ResumeLayout(false);
			this.scNewProject.Panel2.ResumeLayout(false);
			this.scNewProject.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer sc;
		private Controls.DocumentTypeSelector dts;
		private System.Windows.Forms.GroupBox fraDocumentType;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private AwesomeControls.ListView.ListViewControl lvFileTemplates;
		private System.Windows.Forms.Panel pnlNewFile;
		private System.Windows.Forms.Panel pnlNewProject;
		private System.Windows.Forms.SplitContainer scNewProject;
		private System.Windows.Forms.TreeView tvProject;
		private AwesomeControls.ListView.ListViewControl lvProjectTemplates;
		internal System.Windows.Forms.RadioButton optSeparate;
		internal System.Windows.Forms.RadioButton optCombine;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox txtProjectTitle;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox txtSolutionTitle;
	}
}