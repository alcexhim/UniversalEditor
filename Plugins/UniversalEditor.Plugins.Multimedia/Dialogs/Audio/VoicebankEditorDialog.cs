using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace UniversalEditor.Plugins.Multimedia.Dialogs.Audio
{
	public class VoicebankEditorDialog : Form
	{
		private IContainer components = null;
		private GroupBox fraVoiceConfigurations;
		private GroupBox fraDefaultParameters;
		private Button cmdCancel;
		private Button cmdOK;
		private ListView listView1;
		private ColumnHeader chName;
		private Button cmdDelete;
		private Button cmdCopy;
		private Button cmdAdd;
		private ComboBox cboOriginalVoicebank;
		private Label label2;
		private ComboBox cboSourceEngine;
		private Label label1;
		private GroupBox fraSynthaloidParameters;
		private Label lblWaveType;
		private GroupBox fraVOCALOIDParameters;
		private Label label3;
		private NumericUpDown numericUpDown3;
		private Label label6;
		private Label label5;
		private NumericUpDown numericUpDown1;
		private NumericUpDown numericUpDown5;
		private NumericUpDown numericUpDown4;
		private NumericUpDown numericUpDown2;
		private Label label4;
		private Label label7;
		private TextBox txtSearch;
		public VoicebankEditorDialog()
		{
			this.InitializeComponent();
		}
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
			this.fraVoiceConfigurations = new GroupBox();
			this.fraDefaultParameters = new GroupBox();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.listView1 = new ListView();
			this.chName = new ColumnHeader();
			this.cmdAdd = new Button();
			this.cmdCopy = new Button();
			this.cmdDelete = new Button();
			this.label1 = new Label();
			this.cboSourceEngine = new ComboBox();
			this.label2 = new Label();
			this.cboOriginalVoicebank = new ComboBox();
			this.fraSynthaloidParameters = new GroupBox();
			this.lblWaveType = new Label();
			this.fraVOCALOIDParameters = new GroupBox();
			this.label3 = new Label();
			this.numericUpDown3 = new NumericUpDown();
			this.label6 = new Label();
			this.label5 = new Label();
			this.numericUpDown1 = new NumericUpDown();
			this.numericUpDown5 = new NumericUpDown();
			this.numericUpDown4 = new NumericUpDown();
			this.numericUpDown2 = new NumericUpDown();
			this.label4 = new Label();
			this.label7 = new Label();
			this.txtSearch = new TextBox();
			this.fraVoiceConfigurations.SuspendLayout();
			this.fraDefaultParameters.SuspendLayout();
			this.fraSynthaloidParameters.SuspendLayout();
			this.fraVOCALOIDParameters.SuspendLayout();
			((ISupportInitialize)this.numericUpDown3).BeginInit();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			((ISupportInitialize)this.numericUpDown5).BeginInit();
			((ISupportInitialize)this.numericUpDown4).BeginInit();
			((ISupportInitialize)this.numericUpDown2).BeginInit();
			base.SuspendLayout();
			this.fraVoiceConfigurations.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.fraVoiceConfigurations.Controls.Add(this.txtSearch);
			this.fraVoiceConfigurations.Controls.Add(this.cmdDelete);
			this.fraVoiceConfigurations.Controls.Add(this.cmdCopy);
			this.fraVoiceConfigurations.Controls.Add(this.cmdAdd);
			this.fraVoiceConfigurations.Controls.Add(this.listView1);
			this.fraVoiceConfigurations.FlatStyle = FlatStyle.System;
			this.fraVoiceConfigurations.Location = new Point(12, 12);
			this.fraVoiceConfigurations.Name = "fraVoiceConfigurations";
			this.fraVoiceConfigurations.Size = new Size(527, 175);
			this.fraVoiceConfigurations.TabIndex = 0;
			this.fraVoiceConfigurations.TabStop = false;
			this.fraVoiceConfigurations.Text = "Voice configurations";
			this.fraDefaultParameters.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.fraDefaultParameters.Controls.Add(this.cboSourceEngine);
			this.fraDefaultParameters.Controls.Add(this.label1);
			this.fraDefaultParameters.Controls.Add(this.fraVOCALOIDParameters);
			this.fraDefaultParameters.Controls.Add(this.fraSynthaloidParameters);
			this.fraDefaultParameters.FlatStyle = FlatStyle.System;
			this.fraDefaultParameters.Location = new Point(12, 193);
			this.fraDefaultParameters.Name = "fraDefaultParameters";
			this.fraDefaultParameters.Size = new Size(527, 203);
			this.fraDefaultParameters.TabIndex = 1;
			this.fraDefaultParameters.TabStop = false;
			this.fraDefaultParameters.Text = "Default parameters";
			this.cmdCancel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.cmdCancel.DialogResult = DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.System;
			this.cmdCancel.Location = new Point(464, 402);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdOK.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.cmdOK.FlatStyle = FlatStyle.System;
			this.cmdOK.Location = new Point(383, 402);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.listView1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listView1.Columns.AddRange(new ColumnHeader[]
			{
				this.chName
			});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new Point(6, 48);
			this.listView1.Name = "listView1";
			this.listView1.Size = new Size(515, 121);
			this.listView1.TabIndex = 3;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = View.Details;
			this.chName.Text = "Name";
			this.chName.Width = 495;
			this.cmdAdd.FlatStyle = FlatStyle.System;
			this.cmdAdd.Location = new Point(284, 19);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new Size(75, 23);
			this.cmdAdd.TabIndex = 0;
			this.cmdAdd.Text = "&Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			this.cmdCopy.FlatStyle = FlatStyle.System;
			this.cmdCopy.Location = new Point(365, 19);
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Size = new Size(75, 23);
			this.cmdCopy.TabIndex = 1;
			this.cmdCopy.Text = "&Copy";
			this.cmdCopy.UseVisualStyleBackColor = true;
			this.cmdDelete.FlatStyle = FlatStyle.System;
			this.cmdDelete.Location = new Point(446, 19);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new Size(75, 23);
			this.cmdDelete.TabIndex = 2;
			this.cmdDelete.Text = "&Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			this.label1.AutoSize = true;
			this.label1.FlatStyle = FlatStyle.System;
			this.label1.Location = new Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new Size(79, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Source &engine:";
			this.cboSourceEngine.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboSourceEngine.FlatStyle = FlatStyle.System;
			this.cboSourceEngine.FormattingEnabled = true;
			this.cboSourceEngine.Items.AddRange(new object[]
			{
				"General MIDI",
				"VOCALOID",
				"VOCALOID2",
				"Synthaloid",
				"Voxroid"
			});
			this.cboSourceEngine.Location = new Point(108, 19);
			this.cboSourceEngine.Name = "cboSourceEngine";
			this.cboSourceEngine.Size = new Size(413, 21);
			this.cboSourceEngine.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.FlatStyle = FlatStyle.System;
			this.label2.Location = new Point(20, 22);
			this.label2.Name = "label2";
			this.label2.Size = new Size(98, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Original &voicebank:";
			this.cboOriginalVoicebank.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.cboOriginalVoicebank.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboOriginalVoicebank.FlatStyle = FlatStyle.System;
			this.cboOriginalVoicebank.FormattingEnabled = true;
			this.cboOriginalVoicebank.Location = new Point(124, 19);
			this.cboOriginalVoicebank.Name = "cboOriginalVoicebank";
			this.cboOriginalVoicebank.Size = new Size(385, 21);
			this.cboOriginalVoicebank.TabIndex = 1;
			this.fraSynthaloidParameters.Controls.Add(this.lblWaveType);
			this.fraSynthaloidParameters.Location = new Point(6, 46);
			this.fraSynthaloidParameters.Name = "fraSynthaloidParameters";
			this.fraSynthaloidParameters.Size = new Size(515, 151);
			this.fraSynthaloidParameters.TabIndex = 5;
			this.fraSynthaloidParameters.TabStop = false;
			this.fraSynthaloidParameters.Text = "Synthaloid engine";
			this.lblWaveType.AutoSize = true;
			this.lblWaveType.Location = new Point(20, 25);
			this.lblWaveType.Name = "lblWaveType";
			this.lblWaveType.Size = new Size(62, 13);
			this.lblWaveType.TabIndex = 0;
			this.lblWaveType.Text = "&Wave type:";
			this.fraVOCALOIDParameters.Controls.Add(this.cboOriginalVoicebank);
			this.fraVOCALOIDParameters.Controls.Add(this.label3);
			this.fraVOCALOIDParameters.Controls.Add(this.label2);
			this.fraVOCALOIDParameters.Controls.Add(this.numericUpDown3);
			this.fraVOCALOIDParameters.Controls.Add(this.label6);
			this.fraVOCALOIDParameters.Controls.Add(this.label5);
			this.fraVOCALOIDParameters.Controls.Add(this.numericUpDown1);
			this.fraVOCALOIDParameters.Controls.Add(this.numericUpDown5);
			this.fraVOCALOIDParameters.Controls.Add(this.numericUpDown4);
			this.fraVOCALOIDParameters.Controls.Add(this.numericUpDown2);
			this.fraVOCALOIDParameters.Controls.Add(this.label4);
			this.fraVOCALOIDParameters.Controls.Add(this.label7);
			this.fraVOCALOIDParameters.Location = new Point(6, 46);
			this.fraVOCALOIDParameters.Name = "fraVOCALOIDParameters";
			this.fraVOCALOIDParameters.Size = new Size(515, 151);
			this.fraVOCALOIDParameters.TabIndex = 6;
			this.fraVOCALOIDParameters.TabStop = false;
			this.fraVOCALOIDParameters.Text = "VOCALOID engine";
			this.label3.AutoSize = true;
			this.label3.FlatStyle = FlatStyle.System;
			this.label3.Location = new Point(20, 75);
			this.label3.Name = "label3";
			this.label3.Size = new Size(96, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "&Breathiness (BRE):";
			this.numericUpDown3.Location = new Point(133, 125);
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new Size(54, 20);
			this.numericUpDown3.TabIndex = 3;
			this.label6.AutoSize = true;
			this.label6.FlatStyle = FlatStyle.System;
			this.label6.Location = new Point(251, 75);
			this.label6.Name = "label6";
			this.label6.Size = new Size(107, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "&Gender factor (GEN):";
			this.label5.AutoSize = true;
			this.label5.FlatStyle = FlatStyle.System;
			this.label5.Location = new Point(20, 127);
			this.label5.Name = "label5";
			this.label5.Size = new Size(85, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "C&learness (CLE):";
			this.numericUpDown1.Location = new Point(133, 73);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new Size(54, 20);
			this.numericUpDown1.TabIndex = 3;
			this.numericUpDown5.Location = new Point(364, 99);
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new Size(54, 20);
			this.numericUpDown5.TabIndex = 3;
			this.numericUpDown4.Location = new Point(364, 73);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new Size(54, 20);
			this.numericUpDown4.TabIndex = 3;
			this.numericUpDown2.Location = new Point(133, 99);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new Size(54, 20);
			this.numericUpDown2.TabIndex = 3;
			this.label4.AutoSize = true;
			this.label4.FlatStyle = FlatStyle.System;
			this.label4.Location = new Point(20, 101);
			this.label4.Name = "label4";
			this.label4.Size = new Size(86, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Brigh&tness (BRI):";
			this.label7.AutoSize = true;
			this.label7.FlatStyle = FlatStyle.System;
			this.label7.Location = new Point(251, 101);
			this.label7.Name = "label7";
			this.label7.Size = new Size(81, 13);
			this.label7.TabIndex = 2;
			this.label7.Text = "&Opening (OPE):";
			this.txtSearch.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.txtSearch.Location = new Point(6, 21);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new Size(272, 20);
			this.txtSearch.TabIndex = 4;
			base.AcceptButton = this.cmdOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new Size(551, 437);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.fraDefaultParameters);
			base.Controls.Add(this.fraVoiceConfigurations);
			base.Name = "VoicebankEditorDialog";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Voicebank Editor";
			this.fraVoiceConfigurations.ResumeLayout(false);
			this.fraVoiceConfigurations.PerformLayout();
			this.fraDefaultParameters.ResumeLayout(false);
			this.fraDefaultParameters.PerformLayout();
			this.fraSynthaloidParameters.ResumeLayout(false);
			this.fraSynthaloidParameters.PerformLayout();
			this.fraVOCALOIDParameters.ResumeLayout(false);
			this.fraVOCALOIDParameters.PerformLayout();
			((ISupportInitialize)this.numericUpDown3).EndInit();
			((ISupportInitialize)this.numericUpDown1).EndInit();
			((ISupportInitialize)this.numericUpDown5).EndInit();
			((ISupportInitialize)this.numericUpDown4).EndInit();
			((ISupportInitialize)this.numericUpDown2).EndInit();
			base.ResumeLayout(false);
		}
	}
}
