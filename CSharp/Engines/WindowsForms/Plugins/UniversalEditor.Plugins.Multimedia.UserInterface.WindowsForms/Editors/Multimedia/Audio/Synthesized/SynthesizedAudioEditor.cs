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
		public override string Title { get { return "Synthesized Audio"; } }

		public SynthesizedAudioEditor()
		{
			InitializeComponent();
			base.SupportedObjectModels.Add(typeof(SynthesizedAudioObjectModel));

			#region Menu Bar
			#region View
			ActionMenuItem mnuView = base.MenuBar.Items.Add("mnuView", "&View");
			mnuView.Items.Add("mnuViewTrackOverlay", "Track &Overlay", mnuViewTrackOverlay_Click, 3);
			mnuView.Items.AddSeparator(4);
			#endregion
			#region Job
			ActionMenuItem mnuJob = base.MenuBar.Items.Add("mnuJob", "&Job", 4);
			mnuJob.Items.Add("mnuJobNormalizeNotes", "&Normalize Overlapping Notes...", mnuJobNormalizeNotes_Click);
			mnuJob.Items.AddSeparator();
			mnuJob.Items.Add("mnuJobInsertBars", "&Insert Measures...", mnuJobInsertBars_Click);
			mnuJob.Items.Add("mnuJobDeleteBars", "&Delete Measures...", mnuJobDeleteBars_Click);
			mnuJob.Items.AddSeparator();
			mnuJob.Items.Add("mnuJobRandomize", "Insert &Random Notes...", mnuJobRandomize_Click);
			mnuJob.Items.Add("mnuJobConnect", "&Connect Notes...", mnuJobConnect_Click);
			mnuJob.Items.AddSeparator();
			mnuJob.Items.Add("mnuJobInsertLyrics", "Insert &Lyrics...", mnuJobInsertLyrics_Click);
			#endregion
			#region Track
			ActionMenuItem mnuTrack = base.MenuBar.Items.Add("mnuTrack", "Trac&k", 5);
			mnuTrack.Items.Add("mnuTrackEnable", "Enable Trac&k", mnuTrackEnable_Click);
			mnuTrack.Items.AddSeparator();
			mnuTrack.Items.Add("mnuTrackAdd", "&Add Track", mnuTrackAdd_Click);
			mnuTrack.Items.Add("mnuTrackDuplicate", "Dupli&cate Track", mnuTrackDuplicate_Click);
			mnuTrack.Items.Add("mnuTrackRename", "&Rename Track", mnuTrackRename_Click);
			mnuTrack.Items.Add("mnuTrackDelete", "&Delete Track", mnuTrackDelete_Click);
			mnuTrack.Items.AddSeparator();
			mnuTrack.Items.Add("mnuTrackRenderCurrent", "Render Current &Track", mnuTrackRenderCurrent_Click);
			mnuTrack.Items.Add("mnuTrackRenderAll", "Render All Track&s", mnuTrackRenderAll_Click);
			#endregion
			ActionMenuItem mnuPart = base.MenuBar.Items.Add("mnuPart", "Pa&rt", 6);
			ActionMenuItem mnuLyrics = base.MenuBar.Items.Add("mnuLyrics", "&Lyrics", 7);
			ActionMenuItem mnuTransport = base.MenuBar.Items.Add("mnuTransport", "Tra&nsport", 8);
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
		#endregion

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
		}
	}
}
