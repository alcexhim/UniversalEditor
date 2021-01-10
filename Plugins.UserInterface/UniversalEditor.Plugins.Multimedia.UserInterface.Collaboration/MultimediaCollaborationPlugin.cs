//
//  MyClass.cs
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
using System.Collections.Generic;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.Editors.Multimedia.Audio.Synthesized;
using UniversalEditor.Editors.Multimedia.Audio.Synthesized.Views.PianoRoll;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.Plugins.Collaboration;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Collaboration
{
	public class MultimediaCollaborationPlugin : UserInterfacePlugin
	{
		public MultimediaCollaborationPlugin()
		{
			Context = new UIContext(new Guid("{262a622c-c9e3-458b-8fbb-49cf9258b05d}"), "Multimedia Collaboration Plugin");
			Context.AttachCommandEventHandler("Collaboration_Tracking_Track", delegate (object sender, EventArgs e)
			{
				Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
				Document d = editor?.Document;
				if (d != null)
				{
					if (_initted(d))
						return;

					if (d.ObjectModel is SynthesizedAudioObjectModel)
					{
						if (editor is SynthesizedAudioEditor)
						{
							SynthesizedAudioEditor ed = (editor as SynthesizedAudioEditor);
							if (ed.PianoRoll != null)
							{
								ed.PianoRoll.Paint += ed_PianoRoll_Paint;
								ed.PianoRoll.NoteRendered += ed_PianoRoll_NoteRendered;
								ed.PianoRoll.NoteInserted += ed_PianoRoll_NoteInserted;
								ed.PianoRoll.NoteDeleted += ed_PianoRoll_NoteDeleted;
							}
						}
					}

					__initted[d] = true;
				}
			});
			Context.AttachCommandEventHandler("Collaboration_Tracking_AcceptAll", delegate (object sender, EventArgs e)
			{
				if (MessageDialog.ShowDialog("Are you sure you want to accept all tracked changes in this document?", "Accept All Tracked Changes", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) == DialogResult.No)
					return;

				SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection InsertedCommands = GetChangedCommandsList();
				InsertedCommands.Clear();
			});
			Context.AttachCommandEventHandler("Collaboration_Tracking_PreviousChange", delegate (object sender, EventArgs e)
			{
				Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
				Document d = editor?.Document;
				if (d == null)
					return;

				if (!_initted(d))
					return;

				if (d.ObjectModel is SynthesizedAudioObjectModel)
				{
					SynthesizedAudioObjectModel om = (d.ObjectModel as SynthesizedAudioObjectModel);
					if (editor is SynthesizedAudioEditor)
					{
						SynthesizedAudioEditor ed = (editor as SynthesizedAudioEditor);
						if (ed.PianoRoll != null)
						{
							SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection InsertedCommands = GetChangedCommandsList();

							SelectedChangeIndex--;
							if (SelectedChangeIndex < 0)
							{
								SelectedChangeIndex = InsertedCommands.Count - 1;
							}
							else if (SelectedChangeIndex >= InsertedCommands.Count)
							{
								SelectedChangeIndex = InsertedCommands.Count - 1;
							}

							if (SelectedChangeIndex >= 0 && SelectedChangeIndex < InsertedCommands.Count)
							{
								ed.PianoRoll.SelectedCommands.Clear();
								ed.PianoRoll.SelectedCommands.Add(InsertedCommands[SelectedChangeIndex].Command);
							}
						}
					}
				}
			});
			Context.AttachCommandEventHandler("Collaboration_Tracking_NextChange", delegate (object sender, EventArgs e)
			{
				Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
				Document d = editor?.Document;
				if (d == null)
					return;

				if (!_initted(d))
					return;

				if (d.ObjectModel is SynthesizedAudioObjectModel)
				{
					SynthesizedAudioObjectModel om = (d.ObjectModel as SynthesizedAudioObjectModel);
					if (editor is SynthesizedAudioEditor)
					{
						SynthesizedAudioEditor ed = (editor as SynthesizedAudioEditor);
						if (ed.PianoRoll != null)
						{
							SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection InsertedCommands = GetChangedCommandsList();

							SelectedChangeIndex++;
							if (SelectedChangeIndex < 0)
							{
								SelectedChangeIndex = 0;
							}
							else if (SelectedChangeIndex >= InsertedCommands.Count)
							{
								SelectedChangeIndex = 0;
							}

							if (SelectedChangeIndex >= 0 && SelectedChangeIndex < InsertedCommands.Count)
							{
								ed.PianoRoll.SelectedCommands.Clear();
								ed.PianoRoll.SelectedCommands.Add(InsertedCommands[SelectedChangeIndex].Command);
							}
						}
					}
				}
			});
		}

		private SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection GetChangedCommandsList()
		{
			Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
			Document d = editor?.Document;
			if (d == null)
				return null;

			if (!_initted(d))
				return null;

			CollaborationPlugin plugin = (UserInterfacePlugin.Get(new Guid("{981d54ae-dee6-47c7-bea6-20890b3baa23}")) as CollaborationPlugin);
			CollaborationSettings collab = plugin.GetCollaborationSettings(d);

			SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection list = collab.GetExtraData<SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection>("TrackedChanges", new SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection());
			collab.SetExtraData<SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection>("TrackedChanges", list);
			return list;
		}

		private void ed_PianoRoll_NoteInserted(object sender, NoteEventArgs e)
		{
			Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
			Document d = editor?.Document;
			CollaborationPlugin plugin = (UserInterfacePlugin.Get(new Guid("{981d54ae-dee6-47c7-bea6-20890b3baa23}")) as CollaborationPlugin);
			CollaborationSettings collab = plugin.GetCollaborationSettings(d);

			if (collab.TrackChangesEnabled)
			{
				SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection ChangedCommands = GetChangedCommandsList();
				if (ChangedCommands != null)
				{
					ChangedCommands.Add(new SynthesizedAudioCommandChange(e.Note, e.Bounds, CollaborationChangeType.Insertion));
				}
			}
		}

		private void ed_PianoRoll_NoteDeleted(object sender, NoteEventArgs e)
		{
			Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
			Document d = editor?.Document;
			CollaborationPlugin plugin = (UserInterfacePlugin.Get(new Guid("{981d54ae-dee6-47c7-bea6-20890b3baa23}")) as CollaborationPlugin);
			CollaborationSettings collab = plugin.GetCollaborationSettings(d);

			if (collab.TrackChangesEnabled)
			{
				SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection ChangedCommands = GetChangedCommandsList();
				if (ChangedCommands != null)
				{
					ChangedCommands.Add(new SynthesizedAudioCommandChange(e.Note, e.Bounds, CollaborationChangeType.Deletion));
				}
			}
		}


		public Dictionary<Document, bool> __initted = new Dictionary<Document, bool>();
		private bool _initted(Document d)
		{
			return __initted.ContainsKey(d) && __initted[d];
		}

		public int SelectedChangeIndex
		{
			get
			{
				Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
				Document d = editor?.Document;
				CollaborationPlugin plugin = (UserInterfacePlugin.Get(new Guid("{981d54ae-dee6-47c7-bea6-20890b3baa23}")) as CollaborationPlugin);
				CollaborationSettings collab = plugin.GetCollaborationSettings(d);
				return collab.GetExtraData<int>("SelectedChangeIndex", -1);
			}
			set
			{
				Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
				Document d = editor?.Document;
				CollaborationPlugin plugin = (UserInterfacePlugin.Get(new Guid("{981d54ae-dee6-47c7-bea6-20890b3baa23}")) as CollaborationPlugin);
				CollaborationSettings collab = plugin.GetCollaborationSettings(d);
				collab.SetExtraData<int>("SelectedChangeIndex", value);
			}
		}

		private void ed_PianoRoll_Paint(object sender, PaintEventArgs e)
		{
			// painting is handled by PianoRoll._vp, not PianoRollView!!!
			SynthesizedAudioEditor editor = HostApplication.CurrentWindow.GetCurrentEditor() as SynthesizedAudioEditor;
			CollaborationPlugin plugin = (UserInterfacePlugin.Get(new Guid("{981d54ae-dee6-47c7-bea6-20890b3baa23}")) as CollaborationPlugin);
			CollaborationSettings collab = plugin.GetCollaborationSettings();

			if (collab.TrackChangesEnabled)
			{
				SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection ChangedCommands = GetChangedCommandsList();
				SynthesizedAudioCommandChange[] deletions = ChangedCommands.GetDeletions();
				for (int i = 0; i < deletions.Length; i++)
				{
					if (deletions[i].Command is SynthesizedAudioCommandNote)
					{
						editor.PianoRoll.DrawNote(e.Graphics, deletions[i].Bounds, deletions[i].Command as SynthesizedAudioCommandNote, false, false, false);
					}
				}
			}
		}

		private void ed_PianoRoll_NoteRendered(object sender, NoteRenderedEventArgs e)
		{
			Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
			Document d = editor?.Document;
			CollaborationPlugin plugin = (UserInterfacePlugin.Get(new Guid("{981d54ae-dee6-47c7-bea6-20890b3baa23}")) as CollaborationPlugin);
			CollaborationSettings collab = plugin.GetCollaborationSettings(d);

			if (collab.TrackChangesEnabled)
			{
				SynthesizedAudioCommandChange.SynthesizedAudioCommandChangeCollection InsertedCommands = GetChangedCommandsList();
				if (InsertedCommands.Contains(e.Note))
				{
					Color color = Colors.Gray;
					switch (InsertedCommands[e.Note].ChangeType)
					{
						case CollaborationChangeType.Insertion:
						{
							color = Colors.Green;
							break;
						}
						case CollaborationChangeType.Deletion:
						{
							color = Colors.Red;
							break;
						}
					}
					e.Graphics.DrawLine(new MBS.Framework.UserInterface.Drawing.Pen(color, new Measurement(2, MeasurementUnit.Pixel)), e.Bounds.X, e.Bounds.Bottom + 2, e.Bounds.Right, e.Bounds.Bottom + 2);
				}
			}
		}
	}
}
