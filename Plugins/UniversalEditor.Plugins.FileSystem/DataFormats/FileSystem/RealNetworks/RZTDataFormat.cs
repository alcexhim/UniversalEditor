//
//  RZTDataFormat.cs - provides a DataFormat for manipulating archives in RealNetworks RZT format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2019 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.FileSystem.RealNetworks
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in RealNetworks RZT format.
	/// </summary>
	public class RZTDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
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
			string rzt = br.ReadFixedLengthString(4);
			br.Endianness = Endianness.BigEndian;
			int uUnknown_1957 = br.ReadInt32();
			int uFileCount = br.ReadInt32();
			for (int u = 0; u < uFileCount; u++)
			{
				byte szlen = br.ReadByte();
				string sz = br.ReadFixedLengthString(szlen);
				short uz0 = br.ReadInt16();
				short uz1 = br.ReadInt16();
				fsom.Files.Add(sz, new byte[0]);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString(".rzt");
			throw new NotImplementedException();
		}
	}
}
