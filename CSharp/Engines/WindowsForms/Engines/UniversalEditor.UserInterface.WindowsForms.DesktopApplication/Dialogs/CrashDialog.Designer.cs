namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	partial class CrashDialog
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
			this.cmdIgnore = new System.Windows.Forms.Button();
			this.cmdExit = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtType = new System.Windows.Forms.TextBox();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.txtStackTrace = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cmdIgnore
			// 
			this.cmdIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdIgnore.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdIgnore.Location = new System.Drawing.Point(240, 267);
			this.cmdIgnore.Name = "cmdIgnore";
			this.cmdIgnore.Size = new System.Drawing.Size(75, 23);
			this.cmdIgnore.TabIndex = 0;
			this.cmdIgnore.Text = "&Ignore";
			this.cmdIgnore.UseVisualStyleBackColor = true;
			// 
			// cmdExit
			// 
			this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdExit.Location = new System.Drawing.Point(321, 267);
			this.cmdExit.Name = "cmdExit";
			this.cmdExit.Size = new System.Drawing.Size(75, 23);
			this.cmdExit.TabIndex = 0;
			this.cmdExit.Text = "E&xit";
			this.cmdExit.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(384, 36);
			this.label1.TabIndex = 1;
			this.label1.Text = "The application has encountered a problem and may not be able to continue functio" +
    "ning properly.";
			// 
			// txtType
			// 
			this.txtType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtType.Location = new System.Drawing.Point(93, 59);
			this.txtType.Name = "txtType";
			this.txtType.ReadOnly = true;
			this.txtType.Size = new System.Drawing.Size(303, 20);
			this.txtType.TabIndex = 2;
			// 
			// txtSource
			// 
			this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSource.Location = new System.Drawing.Point(93, 85);
			this.txtSource.Name = "txtSource";
			this.txtSource.ReadOnly = true;
			this.txtSource.Size = new System.Drawing.Size(303, 20);
			this.txtSource.TabIndex = 2;
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.Location = new System.Drawing.Point(93, 111);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMessage.Size = new System.Drawing.Size(303, 57);
			this.txtMessage.TabIndex = 2;
			// 
			// txtStackTrace
			// 
			this.txtStackTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStackTrace.Location = new System.Drawing.Point(93, 174);
			this.txtStackTrace.Multiline = true;
			this.txtStackTrace.Name = "txtStackTrace";
			this.txtStackTrace.ReadOnly = true;
			this.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtStackTrace.Size = new System.Drawing.Size(303, 87);
			this.txtStackTrace.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(22, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Type:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(22, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Source:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(22, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "&Message:";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(22, 177);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "&Stack trace:";
			// 
			// CrashDialog
			// 
			this.AcceptButton = this.cmdIgnore;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(408, 302);
			this.Controls.Add(this.txtStackTrace);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.txtSource);
			this.Controls.Add(this.txtType);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdExit);
			this.Controls.Add(this.cmdIgnore);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CrashDialog";
			this.Text = "Error";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdIgnore;
		private System.Windows.Forms.Button cmdExit;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtType;
		private System.Windows.Forms.TextBox txtSource;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.TextBox txtStackTrace;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
	}
}