using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace UniversalEditor.Dialogs.Multimedia.Audio
{
	partial class NoteExpressionProperty
	{
		private IContainer components = null;
		private Label lblTemplate;
		private ComboBox cboTemplate;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Label label2;
		private TextBox textBox1;
		private TrackBar trackBar2;
		private GroupBox fraBendDepth;
		private TextBox txtBendDepth;
		private Label label1;
		private TrackBar trackBar1;
		private GroupBox groupBox3;
		private CheckBox checkBox1;
		private CheckBox checkBox2;
		private GroupBox groupBox4;
		private GroupBox groupBox6;
		private Label label3;
		private TextBox textBox2;
		private TrackBar trackBar3;
		private GroupBox groupBox7;
		private TextBox textBox3;
		private Label label4;
		private TrackBar trackBar4;
		private Button cmdCancel;
		private Button cmdOK;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		
		private void InitializeComponent()
		{
			this.lblTemplate = new System.Windows.Forms.Label();
			this.cboTemplate = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.trackBar2 = new System.Windows.Forms.TrackBar();
			this.fraBendDepth = new System.Windows.Forms.GroupBox();
			this.txtBendDepth = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.trackBar3 = new System.Windows.Forms.TrackBar();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.trackBar4 = new System.Windows.Forms.TrackBar();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
			this.fraBendDepth.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
			this.groupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTemplate
			// 
			this.lblTemplate.AutoSize = true;
			this.lblTemplate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTemplate.Location = new System.Drawing.Point(12, 15);
			this.lblTemplate.Name = "lblTemplate";
			this.lblTemplate.Size = new System.Drawing.Size(54, 13);
			this.lblTemplate.TabIndex = 0;
			this.lblTemplate.Text = "&Template:";
			// 
			// cboTemplate
			// 
			this.cboTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cboTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTemplate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboTemplate.FormattingEnabled = true;
			this.cboTemplate.Location = new System.Drawing.Point(72, 12);
			this.cboTemplate.Name = "cboTemplate";
			this.cboTemplate.Size = new System.Drawing.Size(330, 21);
			this.cboTemplate.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.fraBendDepth);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(12, 39);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(192, 261);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Pitch control";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkBox2);
			this.groupBox3.Controls.Add(this.checkBox1);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(6, 186);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(180, 69);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Add portamento";
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox2.Location = new System.Drawing.Point(18, 44);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(145, 18);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "During &falling movement";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox1.Location = new System.Drawing.Point(18, 20);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(142, 18);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "During &rising movement";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Controls.Add(this.trackBar2);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(99, 19);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(87, 161);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Bend &length:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(71, 132);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(10, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "%";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(14, 129);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(56, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "0";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// trackBar2
			// 
			this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.trackBar2.Location = new System.Drawing.Point(21, 19);
			this.trackBar2.Maximum = 100;
			this.trackBar2.Name = "trackBar2";
			this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar2.Size = new System.Drawing.Size(45, 104);
			this.trackBar2.TabIndex = 0;
			this.trackBar2.TickFrequency = 10;
			this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.Both;
			// 
			// fraBendDepth
			// 
			this.fraBendDepth.Controls.Add(this.txtBendDepth);
			this.fraBendDepth.Controls.Add(this.label1);
			this.fraBendDepth.Controls.Add(this.trackBar1);
			this.fraBendDepth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraBendDepth.Location = new System.Drawing.Point(6, 19);
			this.fraBendDepth.Name = "fraBendDepth";
			this.fraBendDepth.Size = new System.Drawing.Size(87, 161);
			this.fraBendDepth.TabIndex = 0;
			this.fraBendDepth.TabStop = false;
			this.fraBendDepth.Text = "&Bend depth:";
			// 
			// txtBendDepth
			// 
			this.txtBendDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBendDepth.Location = new System.Drawing.Point(14, 129);
			this.txtBendDepth.Name = "txtBendDepth";
			this.txtBendDepth.Size = new System.Drawing.Size(56, 20);
			this.txtBendDepth.TabIndex = 1;
			this.txtBendDepth.Text = "8";
			this.txtBendDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(71, 132);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(10, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "%";
			// 
			// trackBar1
			// 
			this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.trackBar1.Location = new System.Drawing.Point(21, 19);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar1.Size = new System.Drawing.Size(45, 104);
			this.trackBar1.TabIndex = 0;
			this.trackBar1.TickFrequency = 10;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar1.Value = 8;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.groupBox6);
			this.groupBox4.Controls.Add(this.groupBox7);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(210, 39);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(192, 261);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Dynamics control";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label3);
			this.groupBox6.Controls.Add(this.textBox2);
			this.groupBox6.Controls.Add(this.trackBar3);
			this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox6.Location = new System.Drawing.Point(99, 19);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(87, 161);
			this.groupBox6.TabIndex = 1;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "&Accent:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(71, 132);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(10, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "%";
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(14, 129);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(56, 20);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "50";
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// trackBar3
			// 
			this.trackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.trackBar3.Location = new System.Drawing.Point(21, 19);
			this.trackBar3.Maximum = 100;
			this.trackBar3.Name = "trackBar3";
			this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar3.Size = new System.Drawing.Size(45, 104);
			this.trackBar3.TabIndex = 0;
			this.trackBar3.TickFrequency = 10;
			this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar3.Value = 50;
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.textBox3);
			this.groupBox7.Controls.Add(this.label4);
			this.groupBox7.Controls.Add(this.trackBar4);
			this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox7.Location = new System.Drawing.Point(6, 19);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(87, 161);
			this.groupBox7.TabIndex = 0;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "&Decay:";
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox3.Location = new System.Drawing.Point(14, 129);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(56, 20);
			this.textBox3.TabIndex = 1;
			this.textBox3.Text = "50";
			this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(71, 132);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(10, 17);
			this.label4.TabIndex = 2;
			this.label4.Text = "%";
			// 
			// trackBar4
			// 
			this.trackBar4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.trackBar4.Location = new System.Drawing.Point(21, 19);
			this.trackBar4.Maximum = 100;
			this.trackBar4.Name = "trackBar4";
			this.trackBar4.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar4.Size = new System.Drawing.Size(45, 104);
			this.trackBar4.TabIndex = 0;
			this.trackBar4.TickFrequency = 10;
			this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar4.Value = 50;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(327, 310);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(246, 310);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			// 
			// NoteExpressionProperty
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(414, 345);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cboTemplate);
			this.Controls.Add(this.lblTemplate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NoteExpressionProperty";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Note Expression Properties";
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
			this.fraBendDepth.ResumeLayout(false);
			this.fraBendDepth.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
