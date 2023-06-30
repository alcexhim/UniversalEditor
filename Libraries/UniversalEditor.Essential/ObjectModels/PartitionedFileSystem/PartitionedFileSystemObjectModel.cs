//
//  PartitionedFileSystemObjectModel.cs
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
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.ObjectModels.PartitionedFileSystem
{
	public class PartitionedFileSystemObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;

		public int SectorSize { get; set; } = 512;

		public Partition.PartitionCollection Partitions { get; } = new Partition.PartitionCollection();

		public long CalculatePartitionSize(Partition part)
		{
			return part.SectorCount * SectorSize;
		}

		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Partitioned file system" };
			}
			return _omr;
		}

		public override void Clear()
		{
			Partitions.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			PartitionedFileSystemObjectModel clone = where as PartitionedFileSystemObjectModel;
			if (clone == null)
				throw new ObjectModelNotSupportedException();

			foreach (Partition p in Partitions)
			{
				clone.Partitions.Add(p.Clone() as Partition);
			}
		}

		public event EventHandler<PartitionDataRequestEventArgs> PartitionDataRequest;
		protected virtual void OnPartitionDataRequest(PartitionDataRequestEventArgs e)
		{
			PartitionDataRequest?.Invoke(this, e);
		}

		public byte[] GetPartitionData(Partition part)
		{
			PartitionDataRequestEventArgs e = new PartitionDataRequestEventArgs(this, part);
			OnPartitionDataRequest(e);

			return e.Data;
		}

		public long CalculatePartitionOffset(Partition partition)
		{
			long firstAbsoluteSectorLBA = partition.FirstAbsoluteSectorLBA;
			return firstAbsoluteSectorLBA * SectorSize;
		}
	}
}
