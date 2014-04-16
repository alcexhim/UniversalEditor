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
            this.txt = new AwesomeControls.TextBox.TextBoxControl();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.AcceptReturn = true;
            this.txt.AutoSuggestFilter = true;
            this.txt.AutoSuggestMode = AwesomeControls.TextBox.TextBoxAutoSuggestMode.None;
            this.txt.BackColor = System.Drawing.SystemColors.Window;
            this.txt.CaretBlinkInterval = 530;
            this.txt.CaretColor = System.Drawing.Color.Black;
            this.txt.CaretOrientation = System.Windows.Forms.Orientation.Vertical;
            this.txt.CaretSize = 1;
            this.txt.CharacterSpacing = 0;
            this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt.EnableCaret = true;
            this.txt.EnableCaretBlink = true;
            this.txt.EnableOutlining = false;
            this.txt.EnableOverwrite = false;
            this.txt.EnableOverwriteShortcut = true;
            this.txt.EnableSelection = true;
            this.txt.EnableSyntaxHighlight = false;
            this.txt.HideSelection = false;
            this.txt.LineSeparator = AwesomeControls.TextBox.TextBoxLineSeparator.Default;
            this.txt.LineSeparatorString = "\r\n";
            this.txt.Location = new System.Drawing.Point(0, 0);
            this.txt.Multiline = true;
            this.txt.Name = "txt";
            this.txt.PlaceholderText = "";
            this.txt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.txt.SelectionStart = 0;
            this.txt.Size = new System.Drawing.Size(533, 322);
            this.txt.CaseSensitive = true;
            this.txt.TabIndex = 0;
            this.txt.WordSpacing = 0;
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

        private AwesomeControls.TextBox.TextBoxControl txt;
    }
}
