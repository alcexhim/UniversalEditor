//
//  VersatileContainerV3DataFormat.cs
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
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.VersatileContainer
{
	public class VersatileContainerV3DataFormat : DataFormat
	{
		public VersatileContainerV3DataFormat()
		{
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader reader = Accessor.Reader;
			bool done = false;
			while (!reader.EndOfStream && !done)
			{
				VSCTChunkType chunkType = (VSCTChunkType) reader.ReadByte();
				int chunkLength = reader.ReadCompactInt32();

				switch (chunkType)
				{
					case VSCTChunkType.Header:
					{
						break;
					}
					case VSCTChunkType.Schema:
					{
						break;
					}
					case VSCTChunkType.End:
					{
						done = true;
						break;
					}
					default:
					{
						Console.WriteLine("vsct: ignoring unknown chunk type {0}", chunkType);
						reader.Seek(chunkLength, SeekOrigin.Current);
						break;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
