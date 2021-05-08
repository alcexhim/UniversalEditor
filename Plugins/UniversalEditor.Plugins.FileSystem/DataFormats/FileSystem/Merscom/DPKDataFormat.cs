//
//  DPKDataFormat.cs - provides a DataFormat for manipulating archives in Merscom DPK format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker
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
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Merscom
{
	/// <summary>
	/// Provides a <see cref="DataFormat" />for manipulating archives in Merscom DPK format.
	/// </summary>
	public class DPKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Title = "Merscom Data Package (DPK)";
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;

			string DPK4 = br.ReadFixedLengthString(4);
			if (DPK4 != "DPK4")
				throw new InvalidDataFormatException();

			uint u1 = br.ReadUInt32();
			uint u2 = br.ReadUInt32();
			uint u3 = br.ReadUInt32();
			uint u4 = br.ReadUInt32();
			uint uFileCount = br.ReadUInt32();
			for (int i = 0; i < uFileCount; i++)
			{
				br.SeekUntilFirstNonNull();

				uint u6 = br.ReadUInt32();
				uint u7 = br.ReadUInt32();

				string nam = br.ReadNullTerminatedString();
				File file = fsom.AddFile(nam);

				ushort u8 = br.ReadUInt16();
				uint u9 = br.ReadUInt32();
				uint u10 = br.ReadUInt32();
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
