//
//  AccessorFileSource.cs - provides a FileSource for retrieving file source data using an Accessor
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

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	/// <summary>
	/// Provides a <see cref="FileSource" /> for retrieving file source data using an Accessor.
	/// </summary>
	public class AccessorFileSource : FileSource
	{
		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		public AccessorFileSource(Accessor accessor)
		{
			mvarAccessor = accessor;
		}

		public override byte[] GetDataInternal(long offset, long length)
		{
			mvarAccessor.Seek(offset, IO.SeekOrigin.Begin);
			byte[] data = mvarAccessor.Reader.ReadBytes(length);
			return data;
		}

		public override long GetLength()
		{
			return mvarAccessor.Length;
		}
	}
}
