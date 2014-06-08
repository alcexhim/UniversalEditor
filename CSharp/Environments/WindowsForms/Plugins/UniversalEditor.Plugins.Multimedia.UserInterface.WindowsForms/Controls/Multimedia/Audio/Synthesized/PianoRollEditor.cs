using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.Controls.Multimedia.Audio.Synthesized
{
    public partial class PianoRollEditor : Editor
    {
        public PianoRollEditor()
        {
            InitializeComponent();

            base.SupportedObjectModels.Add(typeof(SynthesizedAudioObjectModel));
            base.DoubleBuffered = true;
        }

        private int mvarGridSpacingX = 4;
        public int GridSpacingX { get { return mvarGridSpacingX; } set { mvarGridSpacingX = value; } }
        private int mvarGridSpacingY = 4;
        public int GridSpacingY { get { return mvarGridSpacingY; } set { mvarGridSpacingY = value; } }

        private PianoRollEditorTool mvarCurrentTool = PianoRollEditorTool.Draw;
        public PianoRollEditorTool CurrentTool
        {
            get { return mvarCurrentTool; }
            set
            {
                mvarCurrentTool = value;
                switch (mvarCurrentTool)
                {
                    case PianoRollEditorTool.Draw:
                    {
                        Cursor = MyCursors.Pen;
                        break;
                    }
                    case PianoRollEditorTool.Select:
                    {
                        Cursor = Cursors.IBeam;
                        break;
                    }
                    case PianoRollEditorTool.Erase:
                    {
                        Cursor = MyCursors.Eraser;
                        break;
                    }
                }
            }
        }

        private SynthesizedAudioTrack mvarCurrentTrack = null;
        public SynthesizedAudioTrack CurrentTrack { get { return mvarCurrentTrack; } set { mvarCurrentTrack = value; Refresh(); } }

        private SynthesizedAudioCommand mvarSelectedCommand = null;
        public SynthesizedAudioCommand SelectedCommand { get { return mvarSelectedCommand; } set { mvarSelectedCommand = value; Refresh(); } }

        protected override void OnObjectModelChanged(EventArgs e)
        {
            base.OnObjectModelChanged(e);

            SynthesizedAudioObjectModel audio = (ObjectModel as SynthesizedAudioObjectModel);
            if (audio == null) return;
            if (audio.Tracks.Count < 1) return;

            mvarCurrentTrack = audio.Tracks[0];
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void mnuContext_Opening(object sender, CancelEventArgs e)
        {
            mnuContextProperties.Enabled = (mvarSelectedCommand != null);
        }

        private void mnuContextSelect_Click(object sender, EventArgs e)
        {
            CurrentTool = PianoRollEditorTool.Select;
        }

        private void mnuContextDraw_Click(object sender, EventArgs e)
        {
            CurrentTool = PianoRollEditorTool.Draw;
        }

        private void mnuContextErase_Click(object sender, EventArgs e)
        {
            CurrentTool = PianoRollEditorTool.Erase;
        }
    }
}
