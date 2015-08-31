namespace UniversalEditor.Editors
{
    partial class PlainTextEditor
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
            this.txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.BackColor = System.Drawing.SystemColors.Window;
            this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt.HideSelection = false;
            this.txt.Location = new System.Drawing.Point(0, 0);
            this.txt.Multiline = true;
            this.txt.Name = "txt";
            this.txt.SelectionStart = 0;
            this.txt.Size = new System.Drawing.Size(533, 322);
            this.txt.TabIndex = 0;
            this.txt.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // PlainTextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt);
            this.Name = "PlainTextEditor";
            this.Size = new System.Drawing.Size(533, 322);
            this.ResumeLayout(false);

        }

        #endregion

        // private AwesomeControls.TextBox.TextBoxControl txt;
        private System.Windows.Forms.TextBox txt;
    }
}
