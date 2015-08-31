namespace UniversalEditor.Controls.Icarus
{
    partial class IcarusExpressionTextBox
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
            this.cmd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt.Location = new System.Drawing.Point(0, 1);
            this.txt.Name = "txt";
            this.txt.ReadOnly = true;
            this.txt.Size = new System.Drawing.Size(75, 20);
            this.txt.TabIndex = 0;
            this.txt.Click += new System.EventHandler(this.cmd_Click);
            // 
            // cmd
            // 
            this.cmd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmd.Location = new System.Drawing.Point(77, 0);
            this.cmd.Name = "cmd";
            this.cmd.Size = new System.Drawing.Size(23, 23);
            this.cmd.TabIndex = 1;
            this.cmd.Text = "...";
            this.cmd.UseVisualStyleBackColor = true;
            this.cmd.Click += new System.EventHandler(this.cmd_Click);
            // 
            // IcarusExpressionTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmd);
            this.Controls.Add(this.txt);
            this.MinimumSize = new System.Drawing.Size(100, 22);
            this.Name = "IcarusExpressionTextBox";
            this.Size = new System.Drawing.Size(100, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Button cmd;
    }
}
