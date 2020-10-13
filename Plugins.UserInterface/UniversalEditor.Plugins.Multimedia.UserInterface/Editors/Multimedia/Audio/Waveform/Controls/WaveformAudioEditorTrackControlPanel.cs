//
//  WaveformAudioEditorTrackControlPanel.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Waveform.Controls
{
	[ContainerLayout(typeof(WaveformAudioEditorTrackControlPanel), "UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Waveform.Controls.WaveformAudioEditorTrackControlPanel.glade")]
	public class WaveformAudioEditorTrackControlPanel : Container
	{
		private Button cmdDelete;
		private TextBox txtTrackName;
		private Button cmdMenu;
		private Container ctButtons;
		private Button cmdMute;
		private Button cmdSolo;
		private Container ctSliders;
		private Label lblBitrate;
		private Label lblFormat;
		private Button cmdExpandCollapse;
		private Button cmdSelect;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
		}

		private void cmdExpandCollapse_Click(object sender, EventArgs e)
		{
			if (ctButtons.Visible)
			{
				ctButtons.Visible = false;
				ctSliders.Visible = false;
				lblBitrate.Visible = false;
				lblFormat.Visible = false;
			}
			else
			{
				ctButtons.Visible = true;
				ctSliders.Visible = true;
				lblBitrate.Visible = true;
				lblFormat.Visible = true;
			}
		}
	}
}
