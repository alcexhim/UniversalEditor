//
//  EmbeddedFileSource.cs - provides a FileSource for retrieving uncompressed embedded file data
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

using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	/// <summary>
	/// Provides a <see cref="FileSource" /> for retrieving ucompressed embedded file data.
	/// </summary>
	public class EmbeddedFileSource : FileSource
	{
		private Reader mvarReader = null;
		public Reader Reader { get { return mvarReader; } set { mvarReader = value; } }

		private long mvarOffset = 0;
		public long Offset { get { return mvarOffset; } set { mvarOffset = value; } }

		private long mvarLength = 0;
		public long Length { get { return mvarLength; } set { mvarLength = value; } }

		public override byte[] GetData(long offset, long length)
		{
			mvarReader.Seek(mvarOffset, SeekOrigin.Begin);
			byte[] sourceData = mvarReader.ReadBytes(mvarLength);

			long realLength = Math.Min(length, sourceData.Length);
			byte[] data = new byte[realLength];
			Array.Copy(sourceData, 0, data, 0, realLength);
			return data;
		}

		public override long GetLength()
		{
			return mvarLength;
		}

		public EmbeddedFileSource(Reader reader, long offset, long length, FileSourceTransformation[] transformations = null)
		{
			mvarReader = reader;
			mvarOffset = offset;
			mvarLength = length;

			if (transformations != null)
			{
				foreach (FileSourceTransformation transformation in transformations)
				{
					base.Transformations.Add(transformation);
				}
			}
		}
	}
}
