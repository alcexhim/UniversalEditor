namespace UniversalEditor.Editors
{
	partial class FormattedTextEditor
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
			this.txt = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// txt
			// 
			this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt.HideSelection = false;
			this.txt.Location = new System.Drawing.Point(0, 0);
			this.txt.Name = "txt";
			this.txt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.txt.ShowSelectionMargin = true;
			this.txt.Size = new System.Drawing.Size(368, 137);
			this.txt.TabIndex = 0;
			this.txt.Text = "";
			this.txt.SelectionChanged += new System.EventHandler(this.txt_SelectionChanged);
			this.txt.TextChanged += new System.EventHandler(this.txt_TextChanged);
			// 
			// FormattedTextEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txt);
			this.Name = "FormattedTextEditor";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox txt;
	}
}
