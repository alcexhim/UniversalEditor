namespace UniversalEditor.UserInterface.WindowsForms.Pages
{
    partial class EmbeddedEditorPage
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
            this.errorMessage1 = new UniversalEditor.UserInterface.WindowsForms.Controls.ErrorMessage();
            this.SuspendLayout();
            // 
            // errorMessage1
            // 
            this.errorMessage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorMessage1.Enabled = false;
            this.errorMessage1.Location = new System.Drawing.Point(0, 0);
            this.errorMessage1.Name = "errorMessage1";
            this.errorMessage1.Size = new System.Drawing.Size(480, 332);
            this.errorMessage1.TabIndex = 0;
            this.errorMessage1.Visible = false;
            // 
            // EditorPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.errorMessage1);
            this.Name = "EditorPage";
            this.Size = new System.Drawing.Size(480, 332);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ErrorMessage errorMessage1;

    }
}
