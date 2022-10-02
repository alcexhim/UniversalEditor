//
//  RIFFDataChunk.cs - represents a data chunk in a Resource Interchange File Format file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.ObjectModels.Chunked
{
	/// <summary>
	/// Represents a data chunk in a Resource Interchange File Format file.
	/// </summary>
	public class RIFFDataChunk : RIFFChunk
	{
		[Obsolete("use for backward-compatibility only; new implementations use Source")]
		public byte[] Data
		{
			get
			{
				return Source.GetData();
			}
			set
			{
				Source = new MemoryFileSource(value);
			}
		}

		public override long Size
		{
			get
			{
				return (Source?.GetLength()).GetValueOrDefault(0);
			}
		}

		public long Checksum { get; set; }

		public FileSource Source { get; set; } = null;

		public void Extract(string FileName)
		{
			System.IO.File.WriteAllBytes(FileName, Data);
		}

		public override object Clone()
		{
			RIFFDataChunk clone = new RIFFDataChunk();
			clone.Source = Source;
			clone.ID = base.ID;
			return clone;
		}

		public override string ToString()
		{
			return base.ID + " [" + Size.ToString() + "]";
		}
	}
}
