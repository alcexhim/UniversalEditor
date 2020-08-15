//
//  AnimationPropertiesDialog.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.PictureCollection.Dialogs
{
	[ContainerLayout("~/Editors/Multimedia/PictureCollection/Dialogs/AnimationPropertiesDialog.glade", "GtkDialog")]
	public class AnimationPropertiesDialog : CustomDialog
	{
		private TextBox txtAnimationName;
		private Toolbar tbAnimationFrames;
		private ListViewControl tvAnimationFrames;
		private DefaultTreeModel tmAnimationFrames;

		private Button cmdOK;
		private Button cmdCancel;

		public string AnimationName { get; set; } = String.Empty;
		public List<byte> AnimationFrames { get; } = new List<byte>();

		private NumericTextBox txtFrameIndex;
		private PictureFrame pic;

		public PictureCollectionObjectModel PictureCollection { get; set; } = null;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			DefaultButton = cmdOK;

			txtAnimationName.Text = AnimationName;
			for (int i = 0; i < AnimationFrames.Count; i++)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmAnimationFrames.Columns[0], AnimationFrames[i].ToString())
				});
				row.SetExtraData<byte>("FrameIndex", AnimationFrames[i]);
				row.SetExtraData<int>("EditingIndex", i);
				tmAnimationFrames.Rows.Add(row);
			}

			UpdateConstraints();

			(tbAnimationFrames.Items["tsbAnimationFrameAdd"] as ToolbarItemButton).Click += tsbAnimationFrameAdd_Click;
			(tbAnimationFrames.Items["tsbAnimationFrameRemove"] as ToolbarItemButton).Click += tsbAnimationFrameRemove_Click;
		}

		private void UpdateConstraints()
		{
			if (!IsCreated) return;
			if (PictureCollection == null) return;

			txtFrameIndex.Maximum = PictureCollection.Pictures.Count - 1;
			txtFrameIndex.Minimum = 0;
		}

		private byte _OriginalFrameIndex = 0;

		[EventHandler(nameof(tvAnimationFrames), "SelectionChanging")]
		private void tvAnimationFrames_SelectionChanging(object sender, ListViewSelectionChangingEventArgs e)
		{
			if ((byte)txtFrameIndex.Value != _OriginalFrameIndex)
			{
				if (MessageDialog.ShowDialog("You have changed the image index associated with this frame. Do you want to save your change before selecting another frame?", "Save Change", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) == DialogResult.No)
				{
					e.Cancel = true;
					return;
				}

				if (e.OldSelection != null)
				{
					for (int i = 0; i < e.OldSelection.Length; i++)
					{
						e.OldSelection[i].SetExtraData<byte>("FrameIndex", (byte)txtFrameIndex.Value);
						AnimationFrames[e.OldSelection[i].GetExtraData<int>("EditingIndex")] = (byte)txtFrameIndex.Value;
						e.OldSelection[i].RowColumns[0].Value = txtFrameIndex.Value.ToString();
					}
				}
			}
		}

		[EventHandler(nameof(tvAnimationFrames), "SelectionChanged")]
		private void tvAnimationFrames_SelectionChanged(object sender, EventArgs e)
		{
			TreeModelRow row = tvAnimationFrames.SelectedRows[0];
			int editingIndex = row.GetExtraData<int>("EditingIndex");

			txtFrameIndex.Value = AnimationFrames[editingIndex];
			_OriginalFrameIndex = (byte)txtFrameIndex.Value;
		}

		[EventHandler(nameof(txtFrameIndex), "Changed")]
		private void txtFrameIndex_Changed(object sender, EventArgs e)
		{
			if (PictureCollection == null) return;
			pic.Image = PictureCollection.Pictures[(int)txtFrameIndex.Value].ToImage();
		}

		[EventHandler(nameof(cmdOK), "Click")]
		private void cmdOK_Click(object sender, EventArgs e)
		{
			AnimationName = txtAnimationName.Text;
			DialogResult = DialogResult.OK;
		}

		private void tsbAnimationFrameAdd_Click(object sender, EventArgs e)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmAnimationFrames.Columns[0], txtFrameIndex.Value.ToString())
			});
			row.SetExtraData<byte>("FrameIndex", (byte)txtFrameIndex.Value);
			row.SetExtraData<int>("EditingIndex", tmAnimationFrames.Rows.Count);
			tmAnimationFrames.Rows.Add(row);

			AnimationFrames.Add((byte)txtFrameIndex.Value);
		}
		private void tsbAnimationFrameRemove_Click(object sender, EventArgs e)
		{

		}
	}
}
