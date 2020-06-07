//
//  MBXFileSource.cs - provides a FileSource that allows accessing data from an MBX file associated with a DAT file
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

using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HostileWaters
{
	/// <summary>
	/// Provides a <see cref="FileSource" /> that allows accessing data from an MBX file associated with a DAT file.
	/// </summary>
	public class MBXFileSource : FileSource
	{
		public string MBXFileName { get; set; } = null;
		public uint Offset { get; set; } = 0;
		public uint Length { get; set; } = 0;

		private Reader mbxReader = null;

		public override byte[] GetDataInternal(long offset, long length)
		{
			if (mbxReader == null)
			{
				mbxReader = new Reader(new FileAccessor(MBXFileName, false, false));
			}

			mbxReader.Seek(Offset, SeekOrigin.Begin);
			byte[] data = mbxReader.ReadBytes(Length);
			return data;
		}
		public override long GetLength()
		{
			return Length;
		}

		public MBXFileSource(string MBXFileName, uint offset, uint length)
		{
			this.MBXFileName = MBXFileName;
			Offset = offset;
			Length = length;
		}
	}
}
