//
//  RIFFChunkPropertiesDialog.cs
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
using MBS.Framework.UserInterface.Controls.HexEditor;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.ObjectModels.Chunked;

namespace UniversalEditor.Editors.RIFF.Dialogs
{
	[ContainerLayout("~/Editors/RIFF/Dialogs/RIFFChunkPropertiesDialog.glade")]
	public class RIFFChunkPropertiesDialog : CustomDialog
	{
		private ComboBox cboChunkType;
		private Label lblChunkID;
		private TextBox txtChunkID;
		private Label lblGroupID;
		private ComboBox cboTypeID;
		private HexEditorControl hexData;
		private Disclosure dscData;
		private Button cmdImportData;
		private Button cmdExportData;

		public RIFFChunk Chunk { get; set; } = null;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Buttons[0].ResponseValue = (int)DialogResult.None;
			Buttons[0].Click += cmdOK_Click;
			DefaultButton = Buttons[0];

			if (Chunk != null)
			{
				txtChunkID.Text = Chunk.ID;
				if (Chunk is RIFFDataChunk)
				{
					cboChunkType.SelectedItem = (cboChunkType.Model as DefaultTreeModel).Rows[0];
					hexData.Data = (Chunk as RIFFDataChunk).Data;
				}
				else if (Chunk is RIFFGroupChunk)
				{
					cboChunkType.SelectedItem = (cboChunkType.Model as DefaultTreeModel).Rows[1];
					cboTypeID.Text = (Chunk as RIFFGroupChunk).TypeID;
				}
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (cboChunkType.SelectedItem == (cboChunkType.Model as DefaultTreeModel).Rows[0])
			{
				// Data
				Chunk = new RIFFDataChunk();
				(Chunk as RIFFDataChunk).Data = hexData.Data;
				(Chunk as RIFFDataChunk).ID = txtChunkID.Text;
			}
			else if (cboChunkType.SelectedItem == (cboChunkType.Model as DefaultTreeModel).Rows[1])
			{
				// Group
				Chunk = new RIFFGroupChunk();
				(Chunk as RIFFGroupChunk).TypeID = cboTypeID.Text;
				(Chunk as RIFFGroupChunk).ID = txtChunkID.Text;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		[EventHandler(nameof(cboChunkType), "Changed")]
		private void cboChunkType_Changed(object sender, EventArgs e)
		{
			if (cboChunkType.SelectedItem == (cboChunkType.Model as DefaultTreeModel).Rows[0])
			{
				// Data
				cboTypeID.Visible = false;
				lblGroupID.Visible = false;
				dscData.Visible = true;
			}
			else if (cboChunkType.SelectedItem == (cboChunkType.Model as DefaultTreeModel).Rows[1])
			{
				// Group
				cboTypeID.Visible = true;
				lblGroupID.Visible = true;
				dscData.Visible = false;
			}
		}

		[EventHandler(nameof(cmdImportData), "Click")]
		private void cmdImportData_Click(object sender, EventArgs e)
		{
			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Open;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				hexData.Data = System.IO.File.ReadAllBytes(dlg.SelectedFileName);
			}
		}

		[EventHandler(nameof(cmdExportData), "Click")]
		private void cmdExportData_Click(object sender, EventArgs e)
		{
			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Save;
			dlg.SelectedFileName = txtChunkID.Text;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				System.IO.File.WriteAllBytes(dlg.SelectedFileName, hexData.Data);
			}
		}

	}
}
