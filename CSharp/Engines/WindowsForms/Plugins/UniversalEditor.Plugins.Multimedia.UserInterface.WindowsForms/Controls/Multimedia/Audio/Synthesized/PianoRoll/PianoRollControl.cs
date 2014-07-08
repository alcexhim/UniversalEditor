using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Controls.Multimedia.Audio.Synthesized.PianoRoll
{
    public partial class PianoRollControl : UserControl
    {
        public PianoRollControl()
        {
            InitializeComponent();
        }

        private Size mvarGridSize = new Size(128, 16);
        public Size GridSize { get { return mvarGridSize; } set { mvarGridSize = value; } }

        private Size mvarQuantizationSize = new Size(16, 16);
        public Size QuantizationSize { get { return mvarQuantizationSize; } set { mvarQuantizationSize = value; } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}
