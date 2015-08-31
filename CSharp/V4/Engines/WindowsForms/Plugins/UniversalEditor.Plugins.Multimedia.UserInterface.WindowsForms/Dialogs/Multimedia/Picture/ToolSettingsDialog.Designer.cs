using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace UniversalEditor.Dialogs.Multimedia.Picture
{
	partial class ToolSettingsDialog : Form
	{
		private IContainer components = null;
		private Label label1;
		private Label label2;
		private Button cmdCancel;
		private Button cmdOK;
		private Label label3;
		private ComboBox cboTool;
		internal Button cmdColor;
		internal TrackBar sldPencilSize;
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
			this.label1 = new Label();
			this.cmdColor = new Button();
			this.label2 = new Label();
			this.sldPencilSize = new TrackBar();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.label3 = new Label();
			this.cboTool = new ComboBox();
			((ISupportInitialize)this.sldPencilSize).BeginInit();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.FlatStyle = FlatStyle.System;
			this.label1.Location = new Point(13, 44);
			this.label1.Name = "label1";
			this.label1.Size = new Size(34, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "&Color:";
			this.cmdColor.BackColor = System.Drawing.Color.Black;
			this.cmdColor.Location = new Point(53, 39);
			this.cmdColor.Name = "cmdColor";
			this.cmdColor.Size = new Size(23, 23);
			this.cmdColor.TabIndex = 3;
			this.cmdColor.UseVisualStyleBackColor = false;
			this.cmdColor.Click += new EventHandler(this.cmdColor_Click);
			this.label2.AutoSize = true;
			this.label2.FlatStyle = FlatStyle.System;
			this.label2.Location = new Point(95, 44);
			this.label2.Name = "label2";
			this.label2.Size = new Size(30, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "&Size:";
			this.sldPencilSize.Location = new Point(131, 39);
			this.sldPencilSize.Name = "sldPencilSize";
			this.sldPencilSize.Size = new Size(194, 45);
			this.sldPencilSize.TabIndex = 5;
			this.sldPencilSize.Value = 1;
			this.cmdCancel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.System;
			this.cmdCancel.Location = new Point(250, 120);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new Size(75, 23);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdOK.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.cmdOK.FlatStyle = FlatStyle.System;
			this.cmdOK.Location = new Point(169, 120);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new Size(75, 23);
			this.cmdOK.TabIndex = 6;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label3.AutoSize = true;
			this.label3.FlatStyle = FlatStyle.System;
			this.label3.Location = new Point(12, 15);
			this.label3.Name = "label3";
			this.label3.Size = new Size(31, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "&Tool:";
			this.cboTool.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.cboTool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTool.FlatStyle = FlatStyle.System;
			this.cboTool.FormattingEnabled = true;
			this.cboTool.Location = new Point(53, 12);
			this.cboTool.Name = "cboTool";
			this.cboTool.Size = new Size(272, 21);
			this.cboTool.TabIndex = 1;
			base.AcceptButton = this.cmdOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new Size(337, 155);
			base.Controls.Add(this.cboTool);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.sldPencilSize);
			base.Controls.Add(this.cmdColor);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ToolSettingsDialog";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tool Settings";
			((ISupportInitialize)this.sldPencilSize).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
