//
//  RIFFEditor.cs
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
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.UserInterface;
using UniversalEditor.Editors.RIFF.Dialogs;

namespace UniversalEditor.Editors.RIFF
{
	[ContainerLayout("~/Editors/RIFF/RIFFEditor.glade")]
	public class RIFFEditor : Editor
	{
		private ListView tv;
		private DefaultTreeModel tm;

		public override void UpdateSelections()
		{
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			return null;
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(ChunkedObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			tm.Rows.Clear();

			ChunkedObjectModel riff = (ObjectModel as ChunkedObjectModel);
			if (riff == null) return;

			if (riff.Chunks.Count == 0)
			{
				riff.Chunks.Add(new RIFFGroupChunk { TypeID = "RIFF" });
			}

			for (int i = 0; i < riff.Chunks.Count; i++)
			{
				AddChunkRecursive(riff.Chunks[i], null);
			}
		}

		private void AddChunkRecursive(RIFFChunk chunk, TreeModelRow parent = null)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tm.Columns[0], chunk.ID),
				new TreeModelRowColumn(tm.Columns[1], String.Empty),
				new TreeModelRowColumn(tm.Columns[2], chunk.Size)
			});
			row.SetExtraData<RIFFChunk>("chunk", chunk);

			if (chunk is RIFFGroupChunk)
			{
				RIFFGroupChunk g = (RIFFGroupChunk)chunk;
				row.RowColumns[1].Value = g.TypeID;
				for (int i = 0; i < g.Chunks.Count; i++)
				{
					AddChunkRecursive(g.Chunks[i], row);
				}
			}

			if (parent == null)
			{
				tm.Rows.Add(row);
			}
			else
			{
				parent.Rows.Add(row);
			}
		}

		protected override bool ShowDocumentPropertiesDialogInternal()
		{
			if (tv.Focused)
			{
				if (tv.SelectedRows.Count == 0)
					return true;

				RIFFChunkPropertiesDialog dlg = new RIFFChunkPropertiesDialog();
				RIFFChunk originalChunk = tv.SelectedRows[0].GetExtraData<RIFFChunk>("chunk");
				dlg.Chunk = originalChunk;

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					BeginEdit();

					int originalIndex = originalChunk.Parent.Chunks.IndexOf(originalChunk);
					originalChunk.Parent.Chunks[originalIndex] = dlg.Chunk;

					tv.SelectedRows[0].RowColumns[0].Value = dlg.Chunk.ID;
					tv.SelectedRows[0].RowColumns[1].Value = (dlg.Chunk as RIFFGroupChunk)?.TypeID ?? String.Empty;
					tv.SelectedRows[0].RowColumns[2].Value = dlg.Chunk.Size;
					tv.SelectedRows[0].SetExtraData<RIFFChunk>("chunk", dlg.Chunk);

					EndEdit();
				}
				return true;
			}
			return base.ShowDocumentPropertiesDialogInternal();
		}

		private void RIFFEditorContextMenu_Add_NewGroupChunk(object sender, EventArgs e)
		{
			if (tv.SelectedRows.Count == 0) return;
			TreeModelRow rowParent = tv.SelectedRows[0];

			RIFFGroupChunk group = (rowParent.GetExtraData<RIFFChunk>("chunk") as RIFFGroupChunk);
			if (group == null) return;

			RIFFChunkPropertiesDialog dlg2 = new RIFFChunkPropertiesDialog();

			dlg2.Chunk = new RIFFGroupChunk();
			if (dlg2.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();
				group.Chunks.Add(dlg2.Chunk);
				EndEdit();

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tm.Columns[0], dlg2.Chunk.ID),
					new TreeModelRowColumn(tm.Columns[1], (dlg2.Chunk as RIFFGroupChunk)?.TypeID),
					new TreeModelRowColumn(tm.Columns[2], dlg2.Chunk.Size)
				});
				row.SetExtraData<RIFFChunk>("chunk", dlg2.Chunk);
				rowParent.Rows.Add(row);
			}
		}

		private void RIFFEditorContextMenu_Add_NewDataChunk(object sender, EventArgs e)
		{
			if (tv.SelectedRows.Count == 0) return;
			TreeModelRow rowParent = tv.SelectedRows[0];

			RIFFGroupChunk group = (rowParent.GetExtraData<RIFFChunk>("chunk") as RIFFGroupChunk);
			if (group == null) return;

			RIFFChunkPropertiesDialog dlg2 = new RIFFChunkPropertiesDialog();

			dlg2.Chunk = new RIFFDataChunk { Data = new byte[0] };
			if (dlg2.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();
				group.Chunks.Add(dlg2.Chunk);
				EndEdit();

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tm.Columns[0], dlg2.Chunk.ID),
					new TreeModelRowColumn(tm.Columns[1], (dlg2.Chunk as RIFFGroupChunk)?.TypeID),
					new TreeModelRowColumn(tm.Columns[2], dlg2.Chunk.Size)
				});
				row.SetExtraData<RIFFChunk>("chunk", dlg2.Chunk);
				rowParent.Rows.Add(row);
			}
		}

		private void RIFFEditorContextMenu_Add_ExistingFile(object sender, EventArgs e)
		{
			if (tv.SelectedRows.Count == 0) return;
			TreeModelRow rowParent = tv.SelectedRows[0];

			RIFFGroupChunk group = (rowParent.GetExtraData<RIFFChunk>("chunk") as RIFFGroupChunk);
			if (group == null) return;

			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Open;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				RIFFChunkPropertiesDialog dlg2 = new RIFFChunkPropertiesDialog();

				dlg2.Chunk = new RIFFDataChunk { Data = System.IO.File.ReadAllBytes(dlg.SelectedFileName) };
				if (dlg2.ShowDialog() == DialogResult.OK)
				{
					BeginEdit();
					group.Chunks.Add(dlg2.Chunk);
					EndEdit();

					TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tm.Columns[0], dlg2.Chunk.ID),
						new TreeModelRowColumn(tm.Columns[1], (dlg2.Chunk as RIFFGroupChunk)?.TypeID),
						new TreeModelRowColumn(tm.Columns[2], dlg2.Chunk.Size)
					});
					row.SetExtraData<RIFFChunk>("chunk", dlg2.Chunk);
					rowParent.Rows.Add(row);
				}
			}
		}
		private void RIFFEditorContextMenu_Export(object sender, EventArgs e)
		{
			if (tv.SelectedRows.Count < 1) return;

			RIFFChunk chunk = tv.SelectedRows[0].GetExtraData<RIFFChunk>("chunk");
			if (chunk is RIFFDataChunk)
			{
				FileDialog dlg = new FileDialog();
				dlg.Mode = FileDialogMode.Save;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					System.IO.File.WriteAllBytes(dlg.SelectedFileName, (chunk as RIFFDataChunk).Data);
				}
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Context.AttachCommandEventHandler("RIFFEditorContextMenu_Add_NewGroupChunk", RIFFEditorContextMenu_Add_NewGroupChunk);
			Context.AttachCommandEventHandler("RIFFEditorContextMenu_Add_NewDataChunk", RIFFEditorContextMenu_Add_NewDataChunk);
			Context.AttachCommandEventHandler("RIFFEditorContextMenu_Add_ExistingFile", RIFFEditorContextMenu_Add_ExistingFile);
			Context.AttachCommandEventHandler("RIFFEditorContextMenu_Export", RIFFEditorContextMenu_Export);

			tv.ContextMenuCommandID = "RIFFEditorContextMenu";

			OnObjectModelChanged(EventArgs.Empty);
		}
	}
}
