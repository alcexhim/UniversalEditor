using UniversalEditor.UserInterface.WindowsForms.Controls;
namespace UniversalEditor.Engines.WindowsForms.Pages
{
    partial class GenericErrorPage
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
            this.err = new ErrorMessage();
            this.SuspendLayout();
            // 
            // err
            // 
            this.err.Details = "";
            this.err.Dock = System.Windows.Forms.DockStyle.Fill;
            this.err.Location = new System.Drawing.Point(0, 0);
            this.err.Name = "err";
            this.err.Size = new System.Drawing.Size(502, 346);
            this.err.TabIndex = 1;
            // 
            // GenericErrorPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.err);
            this.Name = "GenericErrorPage";
            this.Size = new System.Drawing.Size(502, 346);
            this.ResumeLayout(false);

        }

        #endregion

        private ErrorMessage err;

    }
}
