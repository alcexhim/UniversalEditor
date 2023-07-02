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
using System.Collections.Generic;
using MBS.Framework;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Drawing.Drawing2D;
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.ObjectModels.PartitionedFileSystem;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.PartitionedFileSystem
{
	[ContainerLayout("~/Editors/PartitionedFileSystem/PartitionedFileSystemEditor.glade")]
	public class PartitionedFileSystemEditor : Editor
	{
		private CustomControl cnvPartitions;
		private ListViewControl tvPartitions;

		private Partition _SelectedPartition = null;
		public Partition SelectedPartition
		{
			get { return _SelectedPartition; }
			set
			{
				_SelectedPartition = value;

				_InhibitSelectedRowsChanged = true;
				tvPartitions.SelectedRows.Clear();
				foreach (TreeModelRow row in tvPartitions.Model.Rows)
				{
					if (row.GetExtraData<Partition>("part") == value)
					{
						tvPartitions.SelectedRows.Add(row);
					}
				}
				_InhibitSelectedRowsChanged = false;

				Context.Commands["PartitionedFileSystemEditor_tvPartitions_ContextMenu_CopyTo"].Enabled = _SelectedPartition != null;

				cnvPartitions.Refresh();
			}
		}

		private bool _InhibitSelectedRowsChanged = false;

		private Partition PartitionHitTest(Vector2D loc)
		{
			PartitionedFileSystemObjectModel disk = ObjectModel as PartitionedFileSystemObjectModel;
			if (disk == null)
				return null;

			double totalSize = 0;
			double x = 0, y = 0;
			foreach (Partition p in disk.Partitions)
			{
				totalSize += (p.SectorCount * disk.SectorSize);
			}

			foreach (Partition p in disk.Partitions)
			{
				double width = 0;
				width = ((p.SectorCount * disk.SectorSize) / totalSize) * this.Size.Width;

				if (loc.X >= x && loc.X <= x + width)
				{
					return p;
				}

				x += width;
			}
			return null;
		}

		[EventHandler(nameof(cnvPartitions), nameof(Control.MouseDown))]
		private void cnvPartitions_MouseDown(object sender, MouseEventArgs e)
		{
			Partition p = PartitionHitTest(e.Location);
			if (p != null)
			{
				SelectedPartition = p;
			}
		}
		[EventHandler(nameof(cnvPartitions), nameof(Control.BeforeContextMenu))]
		private void cnvPartitions_BeforeContextMenu(object sender, EventArgs e)
		{
			if (tvPartitions.SelectedRows.Count > 0)
			{
				cnvPartitions.ContextMenuCommandID = "PartitionedFileSystemEditor_tvPartitions_ContextMenu_Selected";
			}
			else
			{
				cnvPartitions.ContextMenuCommandID = "PartitionedFileSystemEditor_tvPartitions_ContextMenu_Unselected";
			}
		}

		private DashStyle dsLongDash = new DashStyle(new double[] { 4, 4 });
		private Dictionary<PartitionType, PartitionGuiInfo> _partitionGuiInfo = new Dictionary<PartitionType, PartitionGuiInfo>();

		[EventHandler(nameof(cnvPartitions), nameof(Control.Paint))]
		private void cnvPartitions_Paint(object sender, PaintEventArgs e)
		{
			PartitionedFileSystemObjectModel disk = ObjectModel as PartitionedFileSystemObjectModel;
			if (disk == null)
				return;

			double totalSize = 0;
			foreach (Partition p in disk.Partitions)
			{
				totalSize += (p.SectorCount * disk.SectorSize);
			}

			double x = 0;
			bool f = false;
			foreach (Partition p in disk.Partitions)
			{
				double partitionSize = (p.SectorCount * disk.SectorSize);
				double pctWidth = partitionSize / totalSize;

				double width = 0;
				width = (pctWidth * cnvPartitions.Size.Width) - 8;

				double usedSize = 0.3 * totalSize;
				double usedWidth = ((usedSize / partitionSize) * width) - 8;

				Rectangle partRect = new Rectangle(x + 4, 4, width - 4, cnvPartitions.Size.Height - 8);
				e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBADouble(1.0, 1.0, 1.0, 0.1)), partRect);

				e.Graphics.FillRectangle(new SolidBrush(SystemColors.HighlightBackground.Alpha(0.4)), new Rectangle(partRect.X, partRect.Y, usedWidth, partRect.Height));

				Pen pen = new Pen(Colors.DarkGray, new Measurement(4, MeasurementUnit.Pixel));
				if (_partitionGuiInfo.ContainsKey(p.PartitionType))
				{
					PartitionGuiInfo info = _partitionGuiInfo[p.PartitionType];
					pen.Color = info.Color;
				}

				e.Graphics.DrawRectangle(pen, partRect);

				e.Graphics.DrawText(String.Format("{0}\n{1}", String.Format("@dev@{0}", disk.Partitions.IndexOf(p)), UserInterface.Common.FileInfo.FormatSize(disk.CalculatePartitionSize(p))), Font.FromFont(SystemFonts.MenuFont, new Measurement(12, MeasurementUnit.Pixel)), partRect, new SolidBrush(SystemColors.WindowForeground), HorizontalAlignment.Center, VerticalAlignment.Middle);

				if (p == SelectedPartition)
				{
					Pen pen2 = new Pen(SystemColors.HighlightBackground, new Measurement(4, MeasurementUnit.Pixel), dsLongDash, LineCapStyles.Flat);
					e.Graphics.DrawRectangle(pen2, partRect);
				}

				x += width + 8;
				f = true;
			}
		}

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

			InitializePartitionGuiInfo();

			OnObjectModelChanged(e);

			Context.AttachCommandEventHandler("PartitionedFileSystemEditor_tvPartitions_ContextMenu_CopyTo", PartitionedFileSystemEditor_tvPartitions_ContextMenu_CopyTo);
		}

		private void InitializePartitionGuiInfo(PartitionGuiInfo info)
		{
			_partitionGuiInfo.Add(info.PartitionType, info);
		}
		private void InitializePartitionGuiInfo()
		{
			InitializePartitionGuiInfo(new PartitionGuiInfo(PartitionType.Ext4, "ext4", Color.Parse("#314e6c")));
			InitializePartitionGuiInfo(new PartitionGuiInfo(PartitionType.FAT12, "fat12", Color.Parse("#00ff00")));
			InitializePartitionGuiInfo(new PartitionGuiInfo(PartitionType.FAT32LBA, "fat32", Color.Parse("#46a046")));
			InitializePartitionGuiInfo(new PartitionGuiInfo(PartitionType.IFS_HPFS_NTFS_exFAT_QNX, "ntfs", Color.Parse("#70d2b1")));
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
			if (tvPartitions.SelectedRows.Count > 0)
			{
				cnvPartitions.ContextMenuCommandID = "PartitionedFileSystemEditor_tvPartitions_ContextMenu_Selected";
			}
			else
			{
				cnvPartitions.ContextMenuCommandID = "PartitionedFileSystemEditor_tvPartitions_ContextMenu_Unselected";
			}
		}
		[EventHandler(nameof(tvPartitions), nameof(ListViewControl.SelectionChanged))]
		private void tvPartitions_SelectionChanged(object sender, EventArgs e)
		{
			if (tvPartitions.SelectedRows.Count == 1)
			{
				SelectedPartition = tvPartitions.SelectedRows[0].GetExtraData<Partition>("part");
			}
			else if (tvPartitions.SelectedRows.Count > 1)
			{

			}
			else
			{
				SelectedPartition = null;
			}
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
					new TreeModelRowColumn(tvPartitions.Model.Columns[2], _partitionGuiInfo.ContainsKey(disk.Partitions[i].PartitionType) ? _partitionGuiInfo[disk.Partitions[i].PartitionType].Name : disk.Partitions[i].PartitionType.ToString() /* File System */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[3], String.Empty /* Mount Point */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[4], String.Empty /* Label */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[5], UserInterface.Common.FileInfo.FormatSize(disk.CalculatePartitionSize(disk.Partitions[i]))),
					new TreeModelRowColumn(tvPartitions.Model.Columns[6], UserInterface.Common.FileInfo.FormatSize(0) /* Used */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[7], UserInterface.Common.FileInfo.FormatSize(0) /* Unused */),
					new TreeModelRowColumn(tvPartitions.Model.Columns[8], Flagstr(disk.Partitions[i])),
					new TreeModelRowColumn(tvPartitions.Model.Columns[9], _partitionGuiInfo.ContainsKey(disk.Partitions[i].PartitionType) ? _partitionGuiInfo[disk.Partitions[i].PartitionType].ColorImage : null)
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
