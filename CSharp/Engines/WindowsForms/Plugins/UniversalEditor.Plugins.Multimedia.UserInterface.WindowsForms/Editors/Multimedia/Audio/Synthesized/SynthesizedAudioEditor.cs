using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

using UniversalEditor.Controls.Multimedia.Audio.Synthesized.PianoRoll;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized
{
	public partial class SynthesizedAudioEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Synthesized audio";
				_er.SupportedObjectModels.Add(typeof(SynthesizedAudioObjectModel));
			}
			return _er;
		}

		public SynthesizedAudioEditor()
		{
			InitializeComponent();

			#region Menu Bar
			#region View
			ActionMenuItem mnuView = base.MenuBar.Items.Add("View", "&View");
			mnuView.Items.Add("ViewTrackOverlay", "Track &Overlay", mnuViewTrackOverlay_Click, 3);
			mnuView.Items.AddSeparator(4);
			#endregion
			#region Job
			ActionMenuItem mnuJob = base.MenuBar.Items.Add("Job", "&Job", 4);
			mnuJob.Items.Add("JobNormalizeNotes", "&Normalize Overlapping Notes...", mnuJobNormalizeNotes_Click);
			mnuJob.Items.AddSeparator();
			mnuJob.Items.Add("JobInsertBars", "&Insert Measures...", mnuJobInsertBars_Click);
			mnuJob.Items.Add("JobDeleteBars", "&Delete Measures...", mnuJobDeleteBars_Click);
			mnuJob.Items.AddSeparator();
			mnuJob.Items.Add("JobRandomize", "Insert &Random Notes...", mnuJobRandomize_Click);
			mnuJob.Items.Add("JobConnect", "&Connect Notes...", mnuJobConnect_Click);
			mnuJob.Items.AddSeparator();
			mnuJob.Items.Add("JobInsertLyrics", "Insert &Lyrics...", mnuJobInsertLyrics_Click);
			#endregion
			#region Track
			ActionMenuItem mnuTrack = base.MenuBar.Items.Add("Track", "Trac&k", 5);
			mnuTrack.Items.Add("TrackEnable", "Enable Trac&k", mnuTrackEnable_Click);
			mnuTrack.Items.AddSeparator();
			mnuTrack.Items.Add("TrackAdd", "&Add Track", mnuTrackAdd_Click);
			mnuTrack.Items.Add("TrackDuplicate", "Dupli&cate Track", mnuTrackDuplicate_Click);
			mnuTrack.Items.Add("TrackRename", "&Rename Track", mnuTrackRename_Click);
			mnuTrack.Items.Add("TrackDelete", "&Delete Track", mnuTrackDelete_Click);
			mnuTrack.Items.AddSeparator();
			mnuTrack.Items.Add("TrackRenderCurrent", "Render Current &Track", mnuTrackRenderCurrent_Click);
			mnuTrack.Items.Add("TrackRenderAll", "Render All Track&s", mnuTrackRenderAll_Click);
			#endregion
			ActionMenuItem mnuPart = base.MenuBar.Items.Add("Part", "Pa&rt", 6);
			ActionMenuItem mnuLyrics = base.MenuBar.Items.Add("Lyrics", "&Lyrics", 7);
			ActionMenuItem mnuTransport = base.MenuBar.Items.Add("Transport", "Tra&nsport", 8);
			#endregion
		}

		#region Event Handlers
		#region Menu Bar
		#region View
		private void mnuViewTrackOverlay_Click(object sender, EventArgs e)
		{
		}
		#endregion
		#region Job
		private void mnuJobNormalizeNotes_Click(object sender, EventArgs e)
		{
		}
		private void mnuJobInsertBars_Click(object sender, EventArgs e)
		{
		}
		private void mnuJobDeleteBars_Click(object sender, EventArgs e)
		{
		}
		private void mnuJobRandomize_Click(object sender, EventArgs e)
		{
		}
		private void mnuJobConnect_Click(object sender, EventArgs e)
		{
		}
		private void mnuJobInsertLyrics_Click(object sender, EventArgs e)
		{
		}
		#endregion
		#region Track
		private void mnuTrackEnable_Click(object sender, EventArgs e)
		{
		}
		private void mnuTrackAdd_Click(object sender, EventArgs e)
		{
		}
		private void mnuTrackDuplicate_Click(object sender, EventArgs e)
		{
		}
		private void mnuTrackRename_Click(object sender, EventArgs e)
		{
		}
		private void mnuTrackDelete_Click(object sender, EventArgs e)
		{
		}
		private void mnuTrackRenderCurrent_Click(object sender, EventArgs e)
		{
		}
		private void mnuTrackRenderAll_Click(object sender, EventArgs e)
		{
		}
		#endregion
		#endregion
		#region Editor Commands
		public override void Delete()
		{
			while (pnlPianoRoll.SelectedCommands.Count > 0)
			{
				pnlPianoRoll.Commands.Remove(pnlPianoRoll.SelectedCommands[0]);
			}
		}
		#endregion
		#endregion

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
		}
	}
}
