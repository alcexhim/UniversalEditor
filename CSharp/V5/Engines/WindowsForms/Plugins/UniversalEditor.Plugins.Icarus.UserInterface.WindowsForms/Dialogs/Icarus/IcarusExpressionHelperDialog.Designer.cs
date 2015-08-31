namespace UniversalEditor.Dialogs.Icarus
{
    partial class IcarusExpressionHelperDialog
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
            this.optGET = new System.Windows.Forms.RadioButton();
            this.optTag = new System.Windows.Forms.RadioButton();
            this.cboGETParameterType = new System.Windows.Forms.ComboBox();
            this.cboGETParameterName = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.lblRangeFrom = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lblRangeTo = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.optConstant = new System.Windows.Forms.RadioButton();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // optGET
            // 
            this.optGET.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optGET.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optGET.Location = new System.Drawing.Point(12, 61);
            this.optGET.Name = "optGET";
            this.optGET.Size = new System.Drawing.Size(416, 17);
            this.optGET.TabIndex = 2;
            this.optGET.TabStop = true;
            this.optGET.Text = "&GET";
            this.optGET.UseVisualStyleBackColor = true;
            // 
            // optTag
            // 
            this.optTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optTag.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optTag.Location = new System.Drawing.Point(12, 111);
            this.optTag.Name = "optTag";
            this.optTag.Size = new System.Drawing.Size(416, 17);
            this.optTag.TabIndex = 5;
            this.optTag.TabStop = true;
            this.optTag.Text = "&TAG";
            this.optTag.UseVisualStyleBackColor = true;
            // 
            // cboGETParameterType
            // 
            this.cboGETParameterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGETParameterType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboGETParameterType.FormattingEnabled = true;
            this.cboGETParameterType.Items.AddRange(new object[] {
            "FLOAT",
            "STRING",
            "VECTOR"});
            this.cboGETParameterType.Location = new System.Drawing.Point(39, 84);
            this.cboGETParameterType.Name = "cboGETParameterType";
            this.cboGETParameterType.Size = new System.Drawing.Size(113, 21);
            this.cboGETParameterType.TabIndex = 3;
            // 
            // cboGETParameterName
            // 
            this.cboGETParameterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGETParameterName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboGETParameterName.FormattingEnabled = true;
            this.cboGETParameterName.Items.AddRange(new object[] {
            "SET_PARM1",
            "SET_PARM2",
            "SET_PARM3",
            "SET_PARM4",
            "SET_PARM5",
            "SET_PARM6",
            "SET_PARM7",
            "SET_PARM8",
            "SET_PARM9",
            "SET_PARM10",
            "SET_PARM11",
            "SET_PARM12",
            "SET_PARM13",
            "SET_PARM14",
            "SET_PARM15",
            "SET_PARM16"});
            this.cboGETParameterName.Location = new System.Drawing.Point(158, 84);
            this.cboGETParameterName.Name = "cboGETParameterName";
            this.cboGETParameterName.Size = new System.Drawing.Size(251, 21);
            this.cboGETParameterName.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(158, 134);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(251, 20);
            this.textBox2.TabIndex = 7;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ORIGIN",
            "ANGLES"});
            this.comboBox1.Location = new System.Drawing.Point(39, 134);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(113, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioButton1.Location = new System.Drawing.Point(12, 161);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(416, 17);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "&Range";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // lblRangeFrom
            // 
            this.lblRangeFrom.AutoSize = true;
            this.lblRangeFrom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblRangeFrom.Location = new System.Drawing.Point(39, 186);
            this.lblRangeFrom.Name = "lblRangeFrom";
            this.lblRangeFrom.Size = new System.Drawing.Size(30, 13);
            this.lblRangeFrom.TabIndex = 9;
            this.lblRangeFrom.Text = "&From";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(75, 184);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown1.TabIndex = 10;
            // 
            // lblRangeTo
            // 
            this.lblRangeTo.AutoSize = true;
            this.lblRangeTo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblRangeTo.Location = new System.Drawing.Point(192, 186);
            this.lblRangeTo.Name = "lblRangeTo";
            this.lblRangeTo.Size = new System.Drawing.Size(16, 13);
            this.lblRangeTo.TabIndex = 11;
            this.lblRangeTo.Text = "&to";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(214, 184);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(107, 20);
            this.numericUpDown2.TabIndex = 12;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancel.Location = new System.Drawing.Point(353, 234);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 14;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdOK.Location = new System.Drawing.Point(272, 234);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 13;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // optConstant
            // 
            this.optConstant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optConstant.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optConstant.Location = new System.Drawing.Point(12, 12);
            this.optConstant.Name = "optConstant";
            this.optConstant.Size = new System.Drawing.Size(416, 17);
            this.optConstant.TabIndex = 0;
            this.optConstant.TabStop = true;
            this.optConstant.Text = "&Constant";
            this.optConstant.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(39, 35);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(370, 20);
            this.textBox3.TabIndex = 1;
            // 
            // IcarusExpressionHelperDialog
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(440, 269);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.lblRangeTo);
            this.Controls.Add(this.lblRangeFrom);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cboGETParameterName);
            this.Controls.Add(this.cboGETParameterType);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.optTag);
            this.Controls.Add(this.optConstant);
            this.Controls.Add(this.optGET);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IcarusExpressionHelperDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Expression Helper";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optGET;
        private System.Windows.Forms.RadioButton optTag;
        private System.Windows.Forms.ComboBox cboGETParameterType;
        private System.Windows.Forms.ComboBox cboGETParameterName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label lblRangeFrom;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lblRangeTo;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.RadioButton optConstant;
        private System.Windows.Forms.TextBox textBox3;
    }
}