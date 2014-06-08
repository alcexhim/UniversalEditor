namespace UniversalEditor.Editors.Multimedia.Palette
{
    partial class PaletteEditor
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
			this.fraColorEditor = new System.Windows.Forms.GroupBox();
			this.cmdColor = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.txtHue = new System.Windows.Forms.NumericUpDown();
			this.lblHue = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.trackBar3 = new System.Windows.Forms.TrackBar();
			this.txtSaturation = new System.Windows.Forms.NumericUpDown();
			this.trackBar2 = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.txtValue = new System.Windows.Forms.NumericUpDown();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.sldBlue = new System.Windows.Forms.TrackBar();
			this.txtRed = new System.Windows.Forms.NumericUpDown();
			this.sldGreen = new System.Windows.Forms.TrackBar();
			this.label3 = new System.Windows.Forms.Label();
			this.sldRed = new System.Windows.Forms.TrackBar();
			this.label4 = new System.Windows.Forms.Label();
			this.txtBlue = new System.Windows.Forms.NumericUpDown();
			this.txtGreen = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.trackBar10 = new System.Windows.Forms.TrackBar();
			this.trackBar7 = new System.Windows.Forms.TrackBar();
			this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
			this.trackBar8 = new System.Windows.Forms.TrackBar();
			this.label6 = new System.Windows.Forms.Label();
			this.trackBar9 = new System.Windows.Forms.TrackBar();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.fraAvailableColors = new System.Windows.Forms.GroupBox();
			this.cmdClear = new System.Windows.Forms.Button();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdUpdate = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.pal = new UniversalEditor.Controls.Multimedia.Palette.ColorListControl();
			this.lblColorName = new System.Windows.Forms.Label();
			this.txtColorName = new System.Windows.Forms.TextBox();
			this.fraColorEditor.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtHue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSaturation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtValue)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldBlue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sldGreen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sldRed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtBlue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGreen)).BeginInit();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
			this.fraAvailableColors.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraColorEditor
			// 
			this.fraColorEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraColorEditor.Controls.Add(this.txtColorName);
			this.fraColorEditor.Controls.Add(this.lblColorName);
			this.fraColorEditor.Controls.Add(this.cmdColor);
			this.fraColorEditor.Controls.Add(this.tabControl1);
			this.fraColorEditor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraColorEditor.Location = new System.Drawing.Point(3, 3);
			this.fraColorEditor.Name = "fraColorEditor";
			this.fraColorEditor.Size = new System.Drawing.Size(506, 168);
			this.fraColorEditor.TabIndex = 0;
			this.fraColorEditor.TabStop = false;
			this.fraColorEditor.Text = "Color Editor";
			// 
			// cmdColor
			// 
			this.cmdColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmdColor.BackColor = System.Drawing.Color.Black;
			this.cmdColor.Location = new System.Drawing.Point(6, 47);
			this.cmdColor.Name = "cmdColor";
			this.cmdColor.Size = new System.Drawing.Size(75, 75);
			this.cmdColor.TabIndex = 2;
			this.cmdColor.UseVisualStyleBackColor = false;
			this.cmdColor.Click += new System.EventHandler(this.cmdColor_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(87, 47);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(413, 115);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.txtHue);
			this.tabPage1.Controls.Add(this.lblHue);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.trackBar3);
			this.tabPage1.Controls.Add(this.txtSaturation);
			this.tabPage1.Controls.Add(this.trackBar2);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.trackBar1);
			this.tabPage1.Controls.Add(this.txtValue);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(405, 89);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "HSV";
			// 
			// txtHue
			// 
			this.txtHue.Location = new System.Drawing.Point(70, 6);
			this.txtHue.Maximum = new decimal(new int[] {
            239,
            0,
            0,
            0});
			this.txtHue.Name = "txtHue";
			this.txtHue.Size = new System.Drawing.Size(61, 20);
			this.txtHue.TabIndex = 1;
			this.txtHue.Validated += new System.EventHandler(this.txtHSV_Validated);
			// 
			// lblHue
			// 
			this.lblHue.AutoSize = true;
			this.lblHue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblHue.Location = new System.Drawing.Point(6, 8);
			this.lblHue.Name = "lblHue";
			this.lblHue.Size = new System.Drawing.Size(30, 13);
			this.lblHue.TabIndex = 0;
			this.lblHue.Text = "&Hue:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(6, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Saturation:";
			// 
			// trackBar3
			// 
			this.trackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar3.AutoSize = false;
			this.trackBar3.Location = new System.Drawing.Point(137, 58);
			this.trackBar3.Maximum = 255;
			this.trackBar3.Name = "trackBar3";
			this.trackBar3.Size = new System.Drawing.Size(262, 20);
			this.trackBar3.TabIndex = 2;
			this.trackBar3.TickFrequency = 10;
			this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// txtSaturation
			// 
			this.txtSaturation.Location = new System.Drawing.Point(70, 32);
			this.txtSaturation.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
			this.txtSaturation.Name = "txtSaturation";
			this.txtSaturation.Size = new System.Drawing.Size(61, 20);
			this.txtSaturation.TabIndex = 1;
			this.txtSaturation.Validated += new System.EventHandler(this.txtHSV_Validated);
			// 
			// trackBar2
			// 
			this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar2.AutoSize = false;
			this.trackBar2.Location = new System.Drawing.Point(137, 32);
			this.trackBar2.Maximum = 255;
			this.trackBar2.Name = "trackBar2";
			this.trackBar2.Size = new System.Drawing.Size(262, 20);
			this.trackBar2.TabIndex = 2;
			this.trackBar2.TickFrequency = 10;
			this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(6, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "&Value:";
			// 
			// trackBar1
			// 
			this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar1.AutoSize = false;
			this.trackBar1.Location = new System.Drawing.Point(137, 6);
			this.trackBar1.Maximum = 255;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(262, 20);
			this.trackBar1.TabIndex = 2;
			this.trackBar1.TickFrequency = 10;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(70, 58);
			this.txtValue.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(61, 20);
			this.txtValue.TabIndex = 1;
			this.txtValue.Validated += new System.EventHandler(this.txtHSV_Validated);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.sldBlue);
			this.tabPage2.Controls.Add(this.txtRed);
			this.tabPage2.Controls.Add(this.sldGreen);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.sldRed);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.txtBlue);
			this.tabPage2.Controls.Add(this.txtGreen);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(405, 117);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "RGB";
			// 
			// sldBlue
			// 
			this.sldBlue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sldBlue.AutoSize = false;
			this.sldBlue.Location = new System.Drawing.Point(137, 58);
			this.sldBlue.Maximum = 255;
			this.sldBlue.Name = "sldBlue";
			this.sldBlue.Size = new System.Drawing.Size(262, 20);
			this.sldBlue.TabIndex = 2;
			this.sldBlue.TickFrequency = 10;
			this.sldBlue.TickStyle = System.Windows.Forms.TickStyle.None;
			this.sldBlue.Scroll += new System.EventHandler(this.sldRGB_Scroll);
			// 
			// txtRed
			// 
			this.txtRed.Location = new System.Drawing.Point(70, 6);
			this.txtRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.txtRed.Name = "txtRed";
			this.txtRed.Size = new System.Drawing.Size(61, 20);
			this.txtRed.TabIndex = 1;
			this.txtRed.Validated += new System.EventHandler(this.txtRGB_Validated);
			// 
			// sldGreen
			// 
			this.sldGreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sldGreen.AutoSize = false;
			this.sldGreen.Location = new System.Drawing.Point(137, 32);
			this.sldGreen.Maximum = 255;
			this.sldGreen.Name = "sldGreen";
			this.sldGreen.Size = new System.Drawing.Size(262, 20);
			this.sldGreen.TabIndex = 2;
			this.sldGreen.TickFrequency = 10;
			this.sldGreen.TickStyle = System.Windows.Forms.TickStyle.None;
			this.sldGreen.Scroll += new System.EventHandler(this.sldRGB_Scroll);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(6, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "&Red:";
			// 
			// sldRed
			// 
			this.sldRed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sldRed.AutoSize = false;
			this.sldRed.Location = new System.Drawing.Point(137, 6);
			this.sldRed.Maximum = 255;
			this.sldRed.Name = "sldRed";
			this.sldRed.Size = new System.Drawing.Size(262, 20);
			this.sldRed.TabIndex = 2;
			this.sldRed.TickFrequency = 10;
			this.sldRed.TickStyle = System.Windows.Forms.TickStyle.None;
			this.sldRed.Scroll += new System.EventHandler(this.sldRGB_Scroll);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(6, 34);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "&Green:";
			// 
			// txtBlue
			// 
			this.txtBlue.Location = new System.Drawing.Point(70, 58);
			this.txtBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.txtBlue.Name = "txtBlue";
			this.txtBlue.Size = new System.Drawing.Size(61, 20);
			this.txtBlue.TabIndex = 1;
			this.txtBlue.Validated += new System.EventHandler(this.txtRGB_Validated);
			// 
			// txtGreen
			// 
			this.txtGreen.Location = new System.Drawing.Point(70, 32);
			this.txtGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.txtGreen.Name = "txtGreen";
			this.txtGreen.Size = new System.Drawing.Size(61, 20);
			this.txtGreen.TabIndex = 1;
			this.txtGreen.Validated += new System.EventHandler(this.txtRGB_Validated);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(6, 60);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "&Blue:";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.trackBar10);
			this.tabPage3.Controls.Add(this.trackBar7);
			this.tabPage3.Controls.Add(this.numericUpDown6);
			this.tabPage3.Controls.Add(this.trackBar8);
			this.tabPage3.Controls.Add(this.label6);
			this.tabPage3.Controls.Add(this.trackBar9);
			this.tabPage3.Controls.Add(this.label7);
			this.tabPage3.Controls.Add(this.numericUpDown9);
			this.tabPage3.Controls.Add(this.numericUpDown7);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Controls.Add(this.numericUpDown8);
			this.tabPage3.Controls.Add(this.label8);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(405, 117);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "CMYK";
			// 
			// trackBar10
			// 
			this.trackBar10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar10.AutoSize = false;
			this.trackBar10.Location = new System.Drawing.Point(137, 84);
			this.trackBar10.Maximum = 255;
			this.trackBar10.Name = "trackBar10";
			this.trackBar10.Size = new System.Drawing.Size(262, 20);
			this.trackBar10.TabIndex = 9;
			this.trackBar10.TickFrequency = 10;
			this.trackBar10.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// trackBar7
			// 
			this.trackBar7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar7.AutoSize = false;
			this.trackBar7.Location = new System.Drawing.Point(137, 58);
			this.trackBar7.Maximum = 255;
			this.trackBar7.Name = "trackBar7";
			this.trackBar7.Size = new System.Drawing.Size(262, 20);
			this.trackBar7.TabIndex = 9;
			this.trackBar7.TickFrequency = 10;
			this.trackBar7.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// numericUpDown6
			// 
			this.numericUpDown6.Location = new System.Drawing.Point(70, 6);
			this.numericUpDown6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new System.Drawing.Size(61, 20);
			this.numericUpDown6.TabIndex = 6;
			// 
			// trackBar8
			// 
			this.trackBar8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar8.AutoSize = false;
			this.trackBar8.Location = new System.Drawing.Point(137, 32);
			this.trackBar8.Maximum = 255;
			this.trackBar8.Name = "trackBar8";
			this.trackBar8.Size = new System.Drawing.Size(262, 20);
			this.trackBar8.TabIndex = 10;
			this.trackBar8.TickFrequency = 10;
			this.trackBar8.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(6, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(34, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "&Cyan:";
			// 
			// trackBar9
			// 
			this.trackBar9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar9.AutoSize = false;
			this.trackBar9.Location = new System.Drawing.Point(137, 6);
			this.trackBar9.Maximum = 255;
			this.trackBar9.Name = "trackBar9";
			this.trackBar9.Size = new System.Drawing.Size(262, 20);
			this.trackBar9.TabIndex = 11;
			this.trackBar9.TickFrequency = 10;
			this.trackBar9.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label7.Location = new System.Drawing.Point(6, 34);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(52, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "&Magenta:";
			// 
			// numericUpDown9
			// 
			this.numericUpDown9.Location = new System.Drawing.Point(70, 84);
			this.numericUpDown9.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown9.Name = "numericUpDown9";
			this.numericUpDown9.Size = new System.Drawing.Size(61, 20);
			this.numericUpDown9.TabIndex = 7;
			// 
			// numericUpDown7
			// 
			this.numericUpDown7.Location = new System.Drawing.Point(70, 58);
			this.numericUpDown7.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.Size = new System.Drawing.Size(61, 20);
			this.numericUpDown7.TabIndex = 7;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label9.Location = new System.Drawing.Point(6, 86);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(37, 13);
			this.label9.TabIndex = 5;
			this.label9.Text = "Blac&k:";
			// 
			// numericUpDown8
			// 
			this.numericUpDown8.Location = new System.Drawing.Point(70, 32);
			this.numericUpDown8.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.Size = new System.Drawing.Size(61, 20);
			this.numericUpDown8.TabIndex = 8;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label8.Location = new System.Drawing.Point(6, 60);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 13);
			this.label8.TabIndex = 5;
			this.label8.Text = "&Yellow:";
			// 
			// fraAvailableColors
			// 
			this.fraAvailableColors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraAvailableColors.Controls.Add(this.cmdClear);
			this.fraAvailableColors.Controls.Add(this.cmdRemove);
			this.fraAvailableColors.Controls.Add(this.cmdUpdate);
			this.fraAvailableColors.Controls.Add(this.cmdAdd);
			this.fraAvailableColors.Controls.Add(this.pal);
			this.fraAvailableColors.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraAvailableColors.Location = new System.Drawing.Point(3, 177);
			this.fraAvailableColors.Name = "fraAvailableColors";
			this.fraAvailableColors.Size = new System.Drawing.Size(506, 203);
			this.fraAvailableColors.TabIndex = 1;
			this.fraAvailableColors.TabStop = false;
			this.fraAvailableColors.Text = "Available colors";
			// 
			// cmdClear
			// 
			this.cmdClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClear.Location = new System.Drawing.Point(425, 19);
			this.cmdClear.Name = "cmdClear";
			this.cmdClear.Size = new System.Drawing.Size(75, 23);
			this.cmdClear.TabIndex = 1;
			this.cmdClear.Text = "&Clear";
			this.cmdClear.UseVisualStyleBackColor = true;
			// 
			// cmdRemove
			// 
			this.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdRemove.Location = new System.Drawing.Point(168, 19);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdRemove.TabIndex = 1;
			this.cmdRemove.Text = "&Remove";
			this.cmdRemove.UseVisualStyleBackColor = true;
			// 
			// cmdUpdate
			// 
			this.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdUpdate.Location = new System.Drawing.Point(87, 19);
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Size = new System.Drawing.Size(75, 23);
			this.cmdUpdate.TabIndex = 1;
			this.cmdUpdate.Text = "&Update";
			this.cmdUpdate.UseVisualStyleBackColor = true;
			// 
			// cmdAdd
			// 
			this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAdd.Location = new System.Drawing.Point(6, 19);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdAdd.TabIndex = 1;
			this.cmdAdd.Text = "&Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			// 
			// pal
			// 
			this.pal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pal.BackColor = System.Drawing.SystemColors.Window;
			this.pal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pal.Location = new System.Drawing.Point(6, 48);
			this.pal.Name = "pal";
			this.pal.SelectedColor = null;
			this.pal.SelectedIndex = 0;
			this.pal.Size = new System.Drawing.Size(494, 149);
			this.pal.TabIndex = 0;
			this.pal.TileHeight = 32;
			this.pal.TileWidth = 32;
			this.pal.SelectionChanged += new System.EventHandler(this.pal_SelectionChanged);
			// 
			// lblColorName
			// 
			this.lblColorName.AutoSize = true;
			this.lblColorName.Location = new System.Drawing.Point(16, 22);
			this.lblColorName.Name = "lblColorName";
			this.lblColorName.Size = new System.Drawing.Size(38, 13);
			this.lblColorName.TabIndex = 3;
			this.lblColorName.Text = "&Name:";
			// 
			// txtColorName
			// 
			this.txtColorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtColorName.Location = new System.Drawing.Point(87, 19);
			this.txtColorName.Name = "txtColorName";
			this.txtColorName.Size = new System.Drawing.Size(413, 20);
			this.txtColorName.TabIndex = 4;
			// 
			// PaletteEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fraAvailableColors);
			this.Controls.Add(this.fraColorEditor);
			this.Name = "PaletteEditor";
			this.Size = new System.Drawing.Size(512, 383);
			this.fraColorEditor.ResumeLayout(false);
			this.fraColorEditor.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtHue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSaturation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtValue)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldBlue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sldGreen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sldRed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtBlue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGreen)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
			this.fraAvailableColors.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fraColorEditor;
        private System.Windows.Forms.NumericUpDown txtHue;
        private System.Windows.Forms.Label lblHue;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.NumericUpDown txtBlue;
        private System.Windows.Forms.NumericUpDown txtValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtGreen;
        private System.Windows.Forms.NumericUpDown txtSaturation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtRed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar sldBlue;
        private System.Windows.Forms.TrackBar sldGreen;
        private System.Windows.Forms.TrackBar sldRed;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TrackBar trackBar10;
        private System.Windows.Forms.TrackBar trackBar7;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.TrackBar trackBar8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBar9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown9;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdColor;
        private System.Windows.Forms.GroupBox fraAvailableColors;
        private Controls.Multimedia.Palette.ColorListControl pal;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.TextBox txtColorName;
		private System.Windows.Forms.Label lblColorName;
    }
}
