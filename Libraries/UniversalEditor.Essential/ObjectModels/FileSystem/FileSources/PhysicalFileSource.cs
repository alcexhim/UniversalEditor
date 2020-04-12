//
//  PhysicalFileSource.cs - provides a FileSource for retrieving file data from a physical file on the file system
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
	/// Provides a <see cref="FileSource" /> for retrieving file data from a physical file on the file system.
	/// </summary>
	public class PhysicalFileSource : FileSource
	{
		private string mvarFileName = String.Empty;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		public override byte[] GetData(long offset, long length)
		{
			byte[] sourceData = System.IO.File.ReadAllBytes(mvarFileName);
			long realLength = Math.Min(length, sourceData.Length);
			byte[] data = new byte[realLength];
			Array.Copy(sourceData, offset, data, 0, realLength);
			return data;
		}

		public override long GetLength()
		{
			return (new System.IO.FileInfo(mvarFileName)).Length;
		}
	}
}
