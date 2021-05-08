namespace UniversalEditor.Plugins.Designer.UserInterface.WindowsForms.Editors.Designer
{
	partial class DesignerEditor
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
			this.components = new System.ComponentModel.Container();
			this.designerControl1 = new AwesomeControls.Designer.DesignerControl();
			this.testComponent = new AwesomeControls.SmartTag.SmartTagComponent(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			//
			// designerControl1
			//
			this.designerControl1.BackColor = System.Drawing.SystemColors.Window;
			this.designerControl1.DefaultObjectClass = null;
			this.designerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.designerControl1.EnableCreation = false;
			this.designerControl1.Location = new System.Drawing.Point(0, 0);
			this.designerControl1.Name = "designerControl1";
			this.designerControl1.Size = new System.Drawing.Size(523, 249);
			this.designerControl1.TabIndex = 0;
			//
			// splitContainer1
			//
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			// splitContainer1.Panel1
			//
			this.splitContainer1.Panel1.Controls.Add(this.designerControl1);
			this.splitContainer1.Size = new System.Drawing.Size(523, 372);
			this.splitContainer1.SplitterDistance = 249;
			this.splitContainer1.TabIndex = 1;
			//
			// DesignerEditor
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "DesignerEditor";
			this.Size = new System.Drawing.Size(523, 372);
			this.splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private AwesomeControls.Designer.DesignerControl designerControl1;
		private AwesomeControls.SmartTag.SmartTagComponent testComponent;
		private System.Windows.Forms.SplitContainer splitContainer1;
	}
}
