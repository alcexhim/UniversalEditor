//
//  PartitionedFileSystemEditor.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.ObjectModels.PartitionedFileSystem;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.PartitionedFileSystem
{
	[ContainerLayout("~/Editors/PartitionedFileSystem/PartitionedFileSystemEditor.glade")]
	public class PartitionedFileSystemEditor : Editor
	{
		private CustomControl cnvPartitions;
		private ListViewControl tvPartitions;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(PartitionedFileSystemObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			OnObjectModelChanged(e);

			Context.AttachCommandEventHandler("PartitionedFileSystemEditor_tvPartitions_ContextMenu_CopyTo", PartitionedFileSystemEditor_tvPartitions_ContextMenu_CopyTo);
		}

		private void PartitionedFileSystemEditor_tvPartitions_ContextMenu_CopyTo(object sender, EventArgs e)
		{
			PartitionedFileSystemObjectModel disk = ObjectModel as PartitionedFileSystemObjectModel;
			if (disk == null)
				return;

			if (tvPartitions.SelectedRows.Count == 1)
			{
				FileDialog dlg = new FileDialog();
				dlg.Mode = FileDialogMode.Save;

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					Partition part = tvPartitions.SelectedRows[0].GetExtraData<Partition>("part");
					byte[] data = disk.GetPartitionData(part);

					System.IO.File.WriteAllBytes(dlg.SelectedFileName, data);
				}
			}
			else if (tvPartitions.SelectedRows.Count > 1)
			{
				FileDialog dlg = new FileDialog();
				dlg.Mode = FileDialogMode.SelectFolder;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					string dirname = dlg.SelectedPath;
					if (!System.IO.Directory.Exists(dirname))
					{
						System.IO.Directory.CreateDirectory(dirname);
					}

					Partition part = tvPartitions.SelectedRows[0].GetExtraData<Partition>("part");
					byte[] data = disk.GetPartitionData(part);

					System.IO.File.WriteAllBytes(dlg.SelectedFileName, data);
				}
			}
		}

		[EventHandler(nameof(tvPartitions), nameof(Control.BeforeContextMenu))]
		private void tvPartitions_BeforeContextMenu(object sender, EventArgs e)
		{
			tvPartitions.ContextMenuCommandID = "PartitionedFileSystemEditor_tvPartitions_ContextMenu_Unselected";
		}
		[EventHandler(nameof(tvPartitions), nameof(ListViewControl.SelectionChanged))]
		private void tvPartitions_SelectionChanged(object sender, EventArgs e)
		{
			Context.Commands["PartitionedFileSystemEditor_tvPartitions_ContextMenu_CopyTo"].Enabled = (tvPartitions.SelectedRows.Count == 1);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			if (!IsCreated)
			{
				return;
			}

			PartitionedFileSystemObjectModel disk = ObjectModel as PartitionedFileSystemObjectModel;
			if (disk == null)
				return;

			for (int i = 0; i < disk.Partitions.Count; i++)
			{
				TreeModelRow rowPartition = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvPartitions.Model.Columns[0], String.Format("@dev@{0}", i)),
					new TreeModelRowColumn(tvPartitions.Model.Columns[1], String.Empty /* Name */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[2], String.Empty /* File System */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[3], String.Empty /* Mount Point */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[4], String.Empty /* Label */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[5], UserInterface.Common.FileInfo.FormatSize(disk.CalculatePartitionSize(disk.Partitions[i]))),
					new TreeModelRowColumn(tvPartitions.Model.Columns[6], UserInterface.Common.FileInfo.FormatSize(0) /* Used */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[7], UserInterface.Common.FileInfo.FormatSize(0) /* Unused */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[8], Flagstr(disk.Partitions[i]))

				});
				rowPartition.SetExtraData<Partition>("part", disk.Partitions[i]);
				tvPartitions.Model.Rows.Add(rowPartition);
			}


			base.OnObjectModelChanged(e);
		}

		private string Flagstr(Partition partition)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (partition.IsBootable)
			{
				sb.Append("boot,");
			}

			string flagstr = sb.ToString();
			if (flagstr.EndsWith(","))
				flagstr = flagstr.Substring(0, flagstr.Length - 1);
			return flagstr;
		}
	}
}
