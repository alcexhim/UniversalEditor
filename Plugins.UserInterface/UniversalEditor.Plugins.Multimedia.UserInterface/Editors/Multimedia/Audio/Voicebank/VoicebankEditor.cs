//
//  VoicebankEditor.cs - provides a UWT-based Editor for a VoicebankObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Voicebank
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for a <see cref="VoicebankObjectModel" />.
	/// </summary>
	public class VoicebankEditor : Editor
	{
		private DefaultTreeModel tmSamples = null;
		private ListView lvSamples = null;

		public VoicebankEditor()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			tmSamples = new DefaultTreeModel(new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) });

			lvSamples = new ListView();
			lvSamples.SelectionMode = SelectionMode.Multiple;
			lvSamples.BeforeContextMenu += lvSamples_BeforeContextMenu;
			lvSamples.Columns.Add(new ListViewColumnText(tmSamples.Columns[0], "Name"));
			lvSamples.Columns.Add(new ListViewColumnText(tmSamples.Columns[1], "Frequency"));
			lvSamples.Columns.Add(new ListViewColumnText(tmSamples.Columns[2], "Channels"));
			lvSamples.Columns.Add(new ListViewColumnText(tmSamples.Columns[3], "Size"));
			lvSamples.Model = tmSamples;

			Controls.Add(lvSamples, new BoxLayout.Constraints(true, true));

			Context.AttachCommandEventHandler("VoicebankEditor_ContextMenu_Samples_Selected_CopyTo", VoicebankEditor_ContextMenu_Samples_Selected_CopyTo_Click);
		}

		void VoicebankEditor_ContextMenu_Samples_Selected_CopyTo_Click(object sender, EventArgs e)
		{
			FileDialog dlg = new FileDialog();
			dlg.Text = "Extract Sample Files";
			dlg.FileNameFilters.Add("Audio files", "*.wav");

			if (lvSamples.SelectedRows.Count == 1)
			{
				dlg.Mode = FileDialogMode.Save;
			}
			else if (lvSamples.SelectedRows.Count > 1)
			{
				dlg.Mode = FileDialogMode.SelectFolder;
			}

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (lvSamples.SelectedRows.Count == 1)
				{
					VoicebankSample vs = lvSamples.SelectedRows[0].GetExtraData<VoicebankSample>("vs");
					System.IO.File.WriteAllBytes(dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1], vs.Data);
				}
				else if (lvSamples.SelectedRows.Count > 1)
				{
					for (int i = 0; i < lvSamples.SelectedRows.Count; i++)
					{
						VoicebankSample vs = lvSamples.SelectedRows[i].GetExtraData<VoicebankSample>("vs");
						System.IO.File.WriteAllBytes(dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1] + System.IO.Path.DirectorySeparatorChar + i.ToString().PadLeft(8, '0') + ".raw", vs.Data);
					}
				}
			}
		}

		void lvSamples_BeforeContextMenu(object sender, EventArgs e)
		{
			if (lvSamples.SelectedRows.Count == 0)
			{
				lvSamples.ContextMenuCommandID = "VoicebankEditor_ContextMenu_Samples_Unselected";
			}
			else
			{
				lvSamples.ContextMenuCommandID = "VoicebankEditor_ContextMenu_Samples_Selected";
			}
		}


		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(VoicebankObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			VoicebankObjectModel vo = (ObjectModel as VoicebankObjectModel);
			if (vo == null)
				return;

			for (int i = 0; i < vo.Samples.Count; i++)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmSamples.Columns[0], vo.Samples[i].Name),
					new TreeModelRowColumn(tmSamples.Columns[1], vo.Samples[i].Frequency.ToString()),
					new TreeModelRowColumn(tmSamples.Columns[2], vo.Samples[i].ChannelCount.ToString()),
					new TreeModelRowColumn(tmSamples.Columns[3], UniversalEditor.UserInterface.Common.FileInfo.FormatSize(vo.Samples[i].Data.Length))
				});
				row.SetExtraData<VoicebankSample>("vs", vo.Samples[i]);
				tmSamples.Rows.Add(row);
			}
		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
	}
}
