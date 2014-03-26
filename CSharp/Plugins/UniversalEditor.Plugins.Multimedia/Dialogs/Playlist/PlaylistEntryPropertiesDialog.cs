using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace UniversalEditor.Plugins.Multimedia.Dialogs.Playlist
{
	public class PlaylistEntryPropertiesDialog : Form
	{
		private IContainer components = null;
		private Label label1;
		private TextBox txtFileName;
		private Button cmdBrowseFileName;
		private Button cmdCancel;
		private Button cmdOK;
		private Label label2;
		private TextBox textBox1;
		private Label label3;
		private TextBox textBox2;
		private GroupBox fraBookmarks;
		private Button button4;
		private Button button3;
		private Button button2;
		private Button button1;
		private ListView listView1;
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
			this.txtFileName = new TextBox();
			this.cmdBrowseFileName = new Button();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.label2 = new Label();
			this.textBox1 = new TextBox();
			this.label3 = new Label();
			this.textBox2 = new TextBox();
			this.fraBookmarks = new GroupBox();
			this.listView1 = new ListView();
			this.button1 = new Button();
			this.button2 = new Button();
			this.button3 = new Button();
			this.button4 = new Button();
			this.fraBookmarks.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.FlatStyle = FlatStyle.System;
			this.label1.Location = new Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&File name:";
			this.txtFileName.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.txtFileName.Location = new Point(79, 12);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new Size(382, 20);
			this.txtFileName.TabIndex = 1;
			this.cmdBrowseFileName.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.cmdBrowseFileName.FlatStyle = FlatStyle.System;
			this.cmdBrowseFileName.Location = new Point(386, 38);
			this.cmdBrowseFileName.Name = "cmdBrowseFileName";
			this.cmdBrowseFileName.Size = new Size(75, 23);
			this.cmdBrowseFileName.TabIndex = 2;
			this.cmdBrowseFileName.Text = "B&rowse...";
			this.cmdBrowseFileName.UseVisualStyleBackColor = true;
			this.cmdCancel.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.cmdCancel.DialogResult = DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.System;
			this.cmdCancel.Location = new Point(386, 230);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdOK.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.cmdOK.FlatStyle = FlatStyle.System;
			this.cmdOK.Location = new Point(305, 230);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.label2.AutoSize = true;
			this.label2.FlatStyle = FlatStyle.System;
			this.label2.Location = new Point(12, 70);
			this.label2.Name = "label2";
			this.label2.Size = new Size(61, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Track &start:";
			this.textBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.textBox1.Location = new Point(79, 67);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(107, 20);
			this.textBox1.TabIndex = 1;
			this.label3.AutoSize = true;
			this.label3.FlatStyle = FlatStyle.System;
			this.label3.Location = new Point(192, 70);
			this.label3.Name = "label3";
			this.label3.Size = new Size(70, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Track &length:";
			this.textBox2.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.textBox2.Location = new Point(268, 67);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new Size(107, 20);
			this.textBox2.TabIndex = 1;
			this.fraBookmarks.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.fraBookmarks.Controls.Add(this.button4);
			this.fraBookmarks.Controls.Add(this.button3);
			this.fraBookmarks.Controls.Add(this.button2);
			this.fraBookmarks.Controls.Add(this.button1);
			this.fraBookmarks.Controls.Add(this.listView1);
			this.fraBookmarks.Location = new Point(12, 93);
			this.fraBookmarks.Name = "fraBookmarks";
			this.fraBookmarks.Size = new Size(449, 131);
			this.fraBookmarks.TabIndex = 3;
			this.fraBookmarks.TabStop = false;
			this.fraBookmarks.Text = "Bookmarks";
			this.listView1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listView1.Location = new Point(6, 48);
			this.listView1.Name = "listView1";
			this.listView1.Size = new Size(437, 77);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.button1.Location = new Point(6, 19);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button2.Location = new Point(87, 19);
			this.button2.Name = "button2";
			this.button2.Size = new Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "button1";
			this.button2.UseVisualStyleBackColor = true;
			this.button3.Location = new Point(168, 19);
			this.button3.Name = "button3";
			this.button3.Size = new Size(75, 23);
			this.button3.TabIndex = 1;
			this.button3.Text = "button1";
			this.button3.UseVisualStyleBackColor = true;
			this.button4.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.button4.Location = new Point(368, 19);
			this.button4.Name = "button4";
			this.button4.Size = new Size(75, 23);
			this.button4.TabIndex = 1;
			this.button4.Text = "button1";
			this.button4.UseVisualStyleBackColor = true;
			base.AcceptButton = this.cmdOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new Size(473, 265);
			base.Controls.Add(this.fraBookmarks);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdBrowseFileName);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtFileName);
			base.Controls.Add(this.label1);
			this.MinimumSize = new Size(489, 303);
			base.Name = "PlaylistEntryPropertiesDialog";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Entry Properties";
			this.fraBookmarks.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public PlaylistEntryPropertiesDialog()
		{
			this.InitializeComponent();
		}
	}
}
