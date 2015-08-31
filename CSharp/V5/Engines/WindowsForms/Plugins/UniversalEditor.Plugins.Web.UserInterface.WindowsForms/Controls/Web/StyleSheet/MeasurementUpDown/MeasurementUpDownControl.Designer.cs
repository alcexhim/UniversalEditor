namespace UniversalEditor.Controls.Web.StyleSheet.MeasurementUpDown
{
	partial class MeasurementUpDownControl
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
			this.cboUnit = new System.Windows.Forms.ComboBox();
			this.txt = new System.Windows.Forms.NumericUpDown();
			this.cbo = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.txt)).BeginInit();
			this.SuspendLayout();
			// 
			// cboUnit
			// 
			this.cboUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboUnit.FormattingEnabled = true;
			this.cboUnit.Items.AddRange(new object[] {
			"px",
			"pt",
			"in",
			"cm",
			"mm",
			"pc",
			"em",
			"ex",
			"%"});
			this.cboUnit.Location = new System.Drawing.Point(80, 0);
			this.cboUnit.Name = "cboUnit";
			this.cboUnit.Size = new System.Drawing.Size(40, 21);
			this.cboUnit.TabIndex = 6;
			this.cboUnit.SelectedIndexChanged += new System.EventHandler(this.cboUnit_SelectedIndexChanged);
			// 
			// txt
			// 
			this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txt.Location = new System.Drawing.Point(61, 1);
			this.txt.Maximum = new decimal(new int[] {
			2147483647,
			0,
			0,
			0});
			this.txt.Minimum = new decimal(new int[] {
			-2147483648,
			0,
			0,
			-2147483648});
			this.txt.Name = "txt";
			this.txt.Size = new System.Drawing.Size(18, 20);
			this.txt.TabIndex = 5;
			this.txt.ValueChanged += new System.EventHandler(this.txt_ValueChanged);
			// 
			// cbo
			// 
			this.cbo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.cbo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cbo.FormattingEnabled = true;
			this.cbo.Location = new System.Drawing.Point(0, 0);
			this.cbo.Name = "cbo";
			this.cbo.Size = new System.Drawing.Size(60, 21);
			this.cbo.TabIndex = 4;
			this.cbo.SelectedIndexChanged += new System.EventHandler(this.cbo_SelectedIndexChanged);
			this.cbo.TextUpdate += new System.EventHandler(this.cbo_TextUpdate);
			// 
			// MeasurementUpDown
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboUnit);
			this.Controls.Add(this.txt);
			this.Controls.Add(this.cbo);
			this.MinimumSize = new System.Drawing.Size(120, 21);
			this.Name = "MeasurementUpDown";
			this.Size = new System.Drawing.Size(120, 21);
			((System.ComponentModel.ISupportInitialize)(this.txt)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cboUnit;
		private System.Windows.Forms.NumericUpDown txt;
		private System.Windows.Forms.ComboBox cbo;
	}
}
