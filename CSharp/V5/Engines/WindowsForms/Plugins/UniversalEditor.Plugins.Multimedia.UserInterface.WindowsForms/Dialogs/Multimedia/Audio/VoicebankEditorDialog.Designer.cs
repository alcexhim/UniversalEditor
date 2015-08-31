using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.Multimedia.Audio
{
	partial class VoicebankEditorDialog : Form
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
			this.fraVoiceConfigurations = new System.Windows.Forms.GroupBox();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdCopy = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fraDefaultParameters = new System.Windows.Forms.GroupBox();
			this.cboSourceEngine = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.fraVOCALOIDParameters = new System.Windows.Forms.GroupBox();
			this.cboOriginalVoicebank = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.fraSynthaloidParameters = new System.Windows.Forms.GroupBox();
			this.lblWaveType = new System.Windows.Forms.Label();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.fraVoiceConfigurations.SuspendLayout();
			this.fraDefaultParameters.SuspendLayout();
			this.fraVOCALOIDParameters.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.fraSynthaloidParameters.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraVoiceConfigurations
			// 
			this.fraVoiceConfigurations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fraVoiceConfigurations.Controls.Add(this.txtSearch);
			this.fraVoiceConfigurations.Controls.Add(this.cmdDelete);
			this.fraVoiceConfigurations.Controls.Add(this.cmdCopy);
			this.fraVoiceConfigurations.Controls.Add(this.cmdAdd);
			this.fraVoiceConfigurations.Controls.Add(this.listView1);
			this.fraVoiceConfigurations.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraVoiceConfigurations.Location = new System.Drawing.Point(12, 12);
			this.fraVoiceConfigurations.Name = "fraVoiceConfigurations";
			this.fraVoiceConfigurations.Size = new System.Drawing.Size(448, 162);
			this.fraVoiceConfigurations.TabIndex = 0;
			this.fraVoiceConfigurations.TabStop = false;
			this.fraVoiceConfigurations.Text = "Voice configurations";
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.Location = new System.Drawing.Point(6, 21);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(193, 20);
			this.txtSearch.TabIndex = 4;
			// 
			// cmdDelete
			// 
			this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDelete.Location = new System.Drawing.Point(367, 19);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(75, 23);
			this.cmdDelete.TabIndex = 2;
			this.cmdDelete.Text = "&Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			// 
			// cmdCopy
			// 
			this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCopy.Location = new System.Drawing.Point(286, 19);
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Size = new System.Drawing.Size(75, 23);
			this.cmdCopy.TabIndex = 1;
			this.cmdCopy.Text = "&Copy";
			this.cmdCopy.UseVisualStyleBackColor = true;
			// 
			// cmdAdd
			// 
			this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAdd.Location = new System.Drawing.Point(205, 19);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdAdd.TabIndex = 0;
			this.cmdAdd.Text = "&Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(6, 48);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(436, 108);
			this.listView1.TabIndex = 3;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// chName
			// 
			this.chName.Text = "Name";
			this.chName.Width = 495;
			// 
			// fraDefaultParameters
			// 
			this.fraDefaultParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fraDefaultParameters.Controls.Add(this.cboSourceEngine);
			this.fraDefaultParameters.Controls.Add(this.label1);
			this.fraDefaultParameters.Controls.Add(this.fraVOCALOIDParameters);
			this.fraDefaultParameters.Controls.Add(this.fraSynthaloidParameters);
			this.fraDefaultParameters.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraDefaultParameters.Location = new System.Drawing.Point(12, 180);
			this.fraDefaultParameters.Name = "fraDefaultParameters";
			this.fraDefaultParameters.Size = new System.Drawing.Size(448, 203);
			this.fraDefaultParameters.TabIndex = 1;
			this.fraDefaultParameters.TabStop = false;
			this.fraDefaultParameters.Text = "Default parameters";
			// 
			// cboSourceEngine
			// 
			this.cboSourceEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cboSourceEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSourceEngine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboSourceEngine.FormattingEnabled = true;
			this.cboSourceEngine.Items.AddRange(new object[] {
            "General MIDI",
            "VOCALOID",
            "VOCALOID2",
            "Synthaloid",
            "Voxroid"});
			this.cboSourceEngine.Location = new System.Drawing.Point(108, 19);
			this.cboSourceEngine.Name = "cboSourceEngine";
			this.cboSourceEngine.Size = new System.Drawing.Size(334, 21);
			this.cboSourceEngine.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Source &engine:";
			// 
			// fraVOCALOIDParameters
			// 
			this.fraVOCALOIDParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
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
			this.fraVOCALOIDParameters.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraVOCALOIDParameters.Location = new System.Drawing.Point(6, 46);
			this.fraVOCALOIDParameters.Name = "fraVOCALOIDParameters";
			this.fraVOCALOIDParameters.Size = new System.Drawing.Size(436, 151);
			this.fraVOCALOIDParameters.TabIndex = 6;
			this.fraVOCALOIDParameters.TabStop = false;
			this.fraVOCALOIDParameters.Text = "VOCALOID engine";
			// 
			// cboOriginalVoicebank
			// 
			this.cboOriginalVoicebank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cboOriginalVoicebank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOriginalVoicebank.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboOriginalVoicebank.FormattingEnabled = true;
			this.cboOriginalVoicebank.Location = new System.Drawing.Point(124, 19);
			this.cboOriginalVoicebank.Name = "cboOriginalVoicebank";
			this.cboOriginalVoicebank.Size = new System.Drawing.Size(306, 21);
			this.cboOriginalVoicebank.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(20, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "&Breathiness (BRE):";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(20, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Original &voicebank:";
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.Location = new System.Drawing.Point(133, 125);
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(54, 20);
			this.numericUpDown3.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(251, 75);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(107, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "&Gender factor (GEN):";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(20, 127);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(85, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "C&learness (CLE):";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(133, 73);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(54, 20);
			this.numericUpDown1.TabIndex = 3;
			// 
			// numericUpDown5
			// 
			this.numericUpDown5.Location = new System.Drawing.Point(364, 99);
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new System.Drawing.Size(54, 20);
			this.numericUpDown5.TabIndex = 3;
			// 
			// numericUpDown4
			// 
			this.numericUpDown4.Location = new System.Drawing.Point(364, 73);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(54, 20);
			this.numericUpDown4.TabIndex = 3;
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(133, 99);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(54, 20);
			this.numericUpDown2.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(20, 101);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Brigh&tness (BRI):";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label7.Location = new System.Drawing.Point(251, 101);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(81, 13);
			this.label7.TabIndex = 2;
			this.label7.Text = "&Opening (OPE):";
			// 
			// fraSynthaloidParameters
			// 
			this.fraSynthaloidParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fraSynthaloidParameters.Controls.Add(this.lblWaveType);
			this.fraSynthaloidParameters.Enabled = false;
			this.fraSynthaloidParameters.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraSynthaloidParameters.Location = new System.Drawing.Point(6, 46);
			this.fraSynthaloidParameters.Name = "fraSynthaloidParameters";
			this.fraSynthaloidParameters.Size = new System.Drawing.Size(436, 151);
			this.fraSynthaloidParameters.TabIndex = 5;
			this.fraSynthaloidParameters.TabStop = false;
			this.fraSynthaloidParameters.Text = "Synthaloid engine";
			this.fraSynthaloidParameters.Visible = false;
			// 
			// lblWaveType
			// 
			this.lblWaveType.AutoSize = true;
			this.lblWaveType.Location = new System.Drawing.Point(20, 25);
			this.lblWaveType.Name = "lblWaveType";
			this.lblWaveType.Size = new System.Drawing.Size(62, 13);
			this.lblWaveType.TabIndex = 0;
			this.lblWaveType.Text = "&Wave type:";
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(385, 389);
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
			this.cmdOK.Location = new System.Drawing.Point(304, 389);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			// 
			// VoicebankEditorDialog
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(472, 424);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.fraDefaultParameters);
			this.Controls.Add(this.fraVoiceConfigurations);
			this.Name = "VoicebankEditorDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Voicebank Editor";
			this.fraVoiceConfigurations.ResumeLayout(false);
			this.fraVoiceConfigurations.PerformLayout();
			this.fraDefaultParameters.ResumeLayout(false);
			this.fraDefaultParameters.PerformLayout();
			this.fraVOCALOIDParameters.ResumeLayout(false);
			this.fraVOCALOIDParameters.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.fraSynthaloidParameters.ResumeLayout(false);
			this.fraSynthaloidParameters.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
