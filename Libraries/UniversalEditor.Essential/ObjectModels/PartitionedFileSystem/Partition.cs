//
//  Partition.cs
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
using UniversalEditor.DataFormats.PartitionedFileSystem.MBR;

namespace UniversalEditor.ObjectModels.PartitionedFileSystem
{
	public class Partition : ICloneable
	{
		public class PartitionCollection
			: System.Collections.ObjectModel.Collection<Partition>
		{

		}


		public bool IsBootable { get; set; } = false;

		public long FirstAbsoluteSectorLBA { get; set; }
		public CHSPartitionAddress FirstAbsoluteSectorAddress { get; set; }
		public CHSPartitionAddress LastAbsoluteSectorAddress { get; set; }
		public uint SectorCount { get; set; }
		public PartitionType PartitionType { get; set; }

		public object Clone()
		{
			Partition clone = new Partition();
			clone.IsBootable = IsBootable;
			return clone;
		}
	}
}
