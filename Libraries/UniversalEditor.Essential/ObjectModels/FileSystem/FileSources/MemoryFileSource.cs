//
//  MemoryFileSource.cs - provides a FileSource for retrieving file data from a byte array
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

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	/// <summary>
	/// Provides a <see cref="FileSource" /> for retrieving file data from a <see cref="byte" /> array.
	/// </summary>
	public class MemoryFileSource : FileSource
	{
		private byte[] mvarData = null;
		public byte[] Data { get { return mvarData; } set { mvarData = value; } }

		public MemoryFileSource(byte[] data)
		{
			mvarData = data;
		}

		public override byte[] GetDataInternal(long offset, long length)
		{
			long realLength = Math.Min(length, mvarData.Length);
			byte[] data = new byte[realLength];
			Array.Copy(mvarData, offset, data, 0, realLength);
			return data;
		}

		public override long GetLength()
		{
			return mvarData.Length;
		}
	}
}
