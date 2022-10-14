//
//  ObjectModelFileSource.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using UniversalEditor.Accessors;

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	public class ObjectModelFileSource : FileSource
	{
		public ObjectModel ObjectModel { get; set; }
		public DataFormat DataFormat { get; set; }

		public ObjectModelFileSource(ObjectModel objectModel, DataFormat dataFormat)
		{
			ObjectModel = objectModel;
			DataFormat = dataFormat;
		}

		private byte[] _data = null;
		public override byte[] GetDataInternal(long offset, long length)
		{
			__getData();
			if (_data != null && (offset >= 0 && (offset + length) <= _data.Length))
			{
				byte[] slice = new byte[length];
				Array.Copy(_data, offset, slice, 0, length);
				return slice;
			}
			return null;
		}

		public override long GetLength()
		{
			__getData();
			if (_data != null)
			{
				return _data.Length;
			}
			return -1;
		}

		private void __getData()
		{
			if (_data != null)
			{
				return;
			}
			MemoryAccessor ma = new MemoryAccessor();

			Document.Save(ObjectModel, DataFormat, ma);

			_data = ma.ToArray();
		}
	}
}
