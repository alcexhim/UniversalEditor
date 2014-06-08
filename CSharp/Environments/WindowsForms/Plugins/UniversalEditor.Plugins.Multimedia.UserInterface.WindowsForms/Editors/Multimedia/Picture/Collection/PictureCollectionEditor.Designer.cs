namespace UniversalEditor.Editors.Multimedia.Picture.Collection
{
    partial class PictureCollectionEditor
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
            this.lv = new AwesomeControls.ListView.ListViewControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pic = new System.Windows.Forms.PictureBox();
            this.pnlTileView = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fraExport = new System.Windows.Forms.GroupBox();
            this.cmdExportAll = new System.Windows.Forms.Button();
            this.cmdExportCurrent = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cmdAnimatePlay = new System.Windows.Forms.Button();
            this.txtFrameDelay = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndFrame = new System.Windows.Forms.NumericUpDown();
            this.txtStartFrame = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStartFrame = new System.Windows.Forms.Label();
            this.fraActions = new System.Windows.Forms.GroupBox();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdReplace = new System.Windows.Forms.Button();
            this.fraView = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.optViewStretch = new System.Windows.Forms.RadioButton();
            this.optViewCenter = new System.Windows.Forms.RadioButton();
            this.optViewTile = new System.Windows.Forms.RadioButton();
            this.optViewZoom = new System.Windows.Forms.RadioButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.panel1.SuspendLayout();
            this.fraExport.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrameDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartFrame)).BeginInit();
            this.fraActions.SuspendLayout();
            this.fraView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.AllowSorting = true;
            this.lv.BackColor = System.Drawing.SystemColors.Window;
            this.lv.DefaultItemHeight = 24;
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lv.LargeImageList = null;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.Mode = AwesomeControls.ListView.ListViewMode.LargeIcons;
            this.lv.Name = "lv";
            this.lv.ShadeColor = System.Drawing.Color.WhiteSmoke;
            this.lv.Size = new System.Drawing.Size(620, 167);
            this.lv.SmallImageList = null;
            this.lv.SortColumn = null;
            this.lv.TabIndex = 0;
            this.lv.ItemActivate += new System.EventHandler(this.lv_ItemActivate);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pic);
            this.splitContainer1.Panel1.Controls.Add(this.pnlTileView);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lv);
            this.splitContainer1.Size = new System.Drawing.Size(620, 473);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 1;
            // 
            // pic
            // 
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 161);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(620, 141);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // pnlTileView
            // 
            this.pnlTileView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTileView.Location = new System.Drawing.Point(0, 161);
            this.pnlTileView.Name = "pnlTileView";
            this.pnlTileView.Size = new System.Drawing.Size(620, 141);
            this.pnlTileView.TabIndex = 5;
            this.pnlTileView.Visible = false;
            this.pnlTileView.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTileView_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fraExport);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.fraActions);
            this.panel1.Controls.Add(this.fraView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 161);
            this.panel1.TabIndex = 6;
            // 
            // fraExport
            // 
            this.fraExport.Controls.Add(this.cmdExportAll);
            this.fraExport.Controls.Add(this.cmdExportCurrent);
            this.fraExport.Location = new System.Drawing.Point(300, 3);
            this.fraExport.Name = "fraExport";
            this.fraExport.Size = new System.Drawing.Size(317, 48);
            this.fraExport.TabIndex = 6;
            this.fraExport.TabStop = false;
            this.fraExport.Text = "Export";
            // 
            // cmdExportAll
            // 
            this.cmdExportAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExportAll.Location = new System.Drawing.Point(161, 19);
            this.cmdExportAll.Name = "cmdExportAll";
            this.cmdExportAll.Size = new System.Drawing.Size(100, 23);
            this.cmdExportAll.TabIndex = 3;
            this.cmdExportAll.Text = "&All Frames";
            this.cmdExportAll.UseVisualStyleBackColor = true;
            this.cmdExportAll.Click += new System.EventHandler(this.cmdExportAll_Click);
            // 
            // cmdExportCurrent
            // 
            this.cmdExportCurrent.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExportCurrent.Location = new System.Drawing.Point(55, 19);
            this.cmdExportCurrent.Name = "cmdExportCurrent";
            this.cmdExportCurrent.Size = new System.Drawing.Size(100, 23);
            this.cmdExportCurrent.TabIndex = 3;
            this.cmdExportCurrent.Text = "&Current Frame";
            this.cmdExportCurrent.UseVisualStyleBackColor = true;
            this.cmdExportCurrent.Click += new System.EventHandler(this.cmdExportCurrent_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.cmdAnimatePlay);
            this.groupBox1.Controls.Add(this.txtFrameDelay);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtEndFrame);
            this.groupBox1.Controls.Add(this.txtStartFrame);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblStartFrame);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(300, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 96);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animate";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioButton2.Location = new System.Drawing.Point(6, 71);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(91, 18);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Frame delay:";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioButton1.Location = new System.Drawing.Point(6, 45);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 18);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Text = "Frame rate:";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // cmdAnimatePlay
            // 
            this.cmdAnimatePlay.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdAnimatePlay.Location = new System.Drawing.Point(230, 68);
            this.cmdAnimatePlay.Name = "cmdAnimatePlay";
            this.cmdAnimatePlay.Size = new System.Drawing.Size(75, 23);
            this.cmdAnimatePlay.TabIndex = 2;
            this.cmdAnimatePlay.Text = "&Play";
            this.cmdAnimatePlay.UseVisualStyleBackColor = true;
            this.cmdAnimatePlay.Click += new System.EventHandler(this.cmdAnimatePlay_Click);
            // 
            // txtFrameDelay
            // 
            this.txtFrameDelay.Location = new System.Drawing.Point(97, 71);
            this.txtFrameDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtFrameDelay.Name = "txtFrameDelay";
            this.txtFrameDelay.Size = new System.Drawing.Size(55, 20);
            this.txtFrameDelay.TabIndex = 1;
            this.txtFrameDelay.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Location = new System.Drawing.Point(158, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "ms";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(97, 45);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(55, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(158, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "fps";
            // 
            // txtEndFrame
            // 
            this.txtEndFrame.Location = new System.Drawing.Point(222, 19);
            this.txtEndFrame.Name = "txtEndFrame";
            this.txtEndFrame.Size = new System.Drawing.Size(55, 20);
            this.txtEndFrame.TabIndex = 1;
            // 
            // txtStartFrame
            // 
            this.txtStartFrame.Location = new System.Drawing.Point(97, 19);
            this.txtStartFrame.Name = "txtStartFrame";
            this.txtStartFrame.Size = new System.Drawing.Size(55, 20);
            this.txtStartFrame.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(158, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "End frame:";
            // 
            // lblStartFrame
            // 
            this.lblStartFrame.AutoSize = true;
            this.lblStartFrame.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblStartFrame.Location = new System.Drawing.Point(30, 21);
            this.lblStartFrame.Name = "lblStartFrame";
            this.lblStartFrame.Size = new System.Drawing.Size(61, 13);
            this.lblStartFrame.TabIndex = 0;
            this.lblStartFrame.Text = "Start frame:";
            // 
            // fraActions
            // 
            this.fraActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraActions.Controls.Add(this.cmdAdd);
            this.fraActions.Controls.Add(this.cmdRemove);
            this.fraActions.Controls.Add(this.cmdReplace);
            this.fraActions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraActions.Location = new System.Drawing.Point(3, 3);
            this.fraActions.Name = "fraActions";
            this.fraActions.Size = new System.Drawing.Size(291, 48);
            this.fraActions.TabIndex = 4;
            this.fraActions.TabStop = false;
            this.fraActions.Text = "Actions";
            // 
            // cmdAdd
            // 
            this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdAdd.Location = new System.Drawing.Point(27, 19);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 2;
            this.cmdAdd.Text = "A&dd...";
            this.cmdAdd.UseVisualStyleBackColor = true;
            // 
            // cmdRemove
            // 
            this.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdRemove.Location = new System.Drawing.Point(108, 19);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(75, 23);
            this.cmdRemove.TabIndex = 2;
            this.cmdRemove.Text = "&Remove...";
            this.cmdRemove.UseVisualStyleBackColor = true;
            // 
            // cmdReplace
            // 
            this.cmdReplace.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdReplace.Location = new System.Drawing.Point(189, 19);
            this.cmdReplace.Name = "cmdReplace";
            this.cmdReplace.Size = new System.Drawing.Size(75, 23);
            this.cmdReplace.TabIndex = 2;
            this.cmdReplace.Text = "Re&place...";
            this.cmdReplace.UseVisualStyleBackColor = true;
            // 
            // fraView
            // 
            this.fraView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fraView.Controls.Add(this.checkBox1);
            this.fraView.Controls.Add(this.numericUpDown3);
            this.fraView.Controls.Add(this.label3);
            this.fraView.Controls.Add(this.label2);
            this.fraView.Controls.Add(this.numericUpDown2);
            this.fraView.Controls.Add(this.optViewStretch);
            this.fraView.Controls.Add(this.optViewCenter);
            this.fraView.Controls.Add(this.optViewTile);
            this.fraView.Controls.Add(this.optViewZoom);
            this.fraView.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraView.Location = new System.Drawing.Point(3, 57);
            this.fraView.Name = "fraView";
            this.fraView.Size = new System.Drawing.Size(291, 96);
            this.fraView.TabIndex = 3;
            this.fraView.TabStop = false;
            this.fraView.Text = "View";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBox1.Location = new System.Drawing.Point(159, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(115, 18);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Keep aspect &ratio";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(88, 48);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(55, 20);
            this.numericUpDown3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(95, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "&Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(34, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "&Width";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(20, 48);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(55, 20);
            this.numericUpDown2.TabIndex = 1;
            // 
            // optViewStretch
            // 
            this.optViewStretch.AutoSize = true;
            this.optViewStretch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optViewStretch.Location = new System.Drawing.Point(20, 19);
            this.optViewStretch.Name = "optViewStretch";
            this.optViewStretch.Size = new System.Drawing.Size(65, 18);
            this.optViewStretch.TabIndex = 1;
            this.optViewStretch.Text = "&Stretch";
            this.optViewStretch.UseVisualStyleBackColor = true;
            this.optViewStretch.CheckedChanged += new System.EventHandler(this.optView_CheckedChanged);
            // 
            // optViewCenter
            // 
            this.optViewCenter.AutoSize = true;
            this.optViewCenter.Checked = true;
            this.optViewCenter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optViewCenter.Location = new System.Drawing.Point(91, 19);
            this.optViewCenter.Name = "optViewCenter";
            this.optViewCenter.Size = new System.Drawing.Size(62, 18);
            this.optViewCenter.TabIndex = 1;
            this.optViewCenter.TabStop = true;
            this.optViewCenter.Text = "&Center";
            this.optViewCenter.UseVisualStyleBackColor = true;
            this.optViewCenter.CheckedChanged += new System.EventHandler(this.optView_CheckedChanged);
            // 
            // optViewTile
            // 
            this.optViewTile.AutoSize = true;
            this.optViewTile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optViewTile.Location = new System.Drawing.Point(223, 19);
            this.optViewTile.Name = "optViewTile";
            this.optViewTile.Size = new System.Drawing.Size(48, 18);
            this.optViewTile.TabIndex = 1;
            this.optViewTile.Text = "&Tile";
            this.optViewTile.UseVisualStyleBackColor = true;
            this.optViewTile.CheckedChanged += new System.EventHandler(this.optView_CheckedChanged);
            // 
            // optViewZoom
            // 
            this.optViewZoom.AutoSize = true;
            this.optViewZoom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optViewZoom.Location = new System.Drawing.Point(159, 19);
            this.optViewZoom.Name = "optViewZoom";
            this.optViewZoom.Size = new System.Drawing.Size(58, 18);
            this.optViewZoom.TabIndex = 1;
            this.optViewZoom.Text = "&Zoom";
            this.optViewZoom.UseVisualStyleBackColor = true;
            this.optViewZoom.CheckedChanged += new System.EventHandler(this.optView_CheckedChanged);
            // 
            // PictureCollectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PictureCollectionEditor";
            this.Size = new System.Drawing.Size(620, 473);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.panel1.ResumeLayout(false);
            this.fraExport.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrameDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartFrame)).EndInit();
            this.fraActions.ResumeLayout(false);
            this.fraView.ResumeLayout(false);
            this.fraView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AwesomeControls.ListView.ListViewControl lv;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Panel pnlTileView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox fraActions;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdReplace;
        private System.Windows.Forms.GroupBox fraView;
        private System.Windows.Forms.RadioButton optViewStretch;
        private System.Windows.Forms.RadioButton optViewCenter;
        private System.Windows.Forms.RadioButton optViewTile;
        private System.Windows.Forms.RadioButton optViewZoom;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown txtEndFrame;
        private System.Windows.Forms.NumericUpDown txtStartFrame;
        private System.Windows.Forms.Label lblStartFrame;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button cmdAnimatePlay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.NumericUpDown txtFrameDelay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdExportCurrent;
        private System.Windows.Forms.GroupBox fraExport;
        private System.Windows.Forms.Button cmdExportAll;
    }
}
