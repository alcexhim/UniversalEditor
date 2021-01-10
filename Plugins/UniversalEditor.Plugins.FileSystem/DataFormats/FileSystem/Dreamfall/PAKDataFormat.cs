//
//  PAKDataFormat.cs - provides a DataFormat for manipulating archives in Dreamfall PAK format
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
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Dreamfall
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Dreamfall PAK format.
	/// </summary>
	public class PAKDataFormat : DataFormat
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

		// All other (i. e. unsupported) characters are assigned to an encoded value of 32, the same as
		// the apostrophe.
		private const string NAME_LOOKUP_TABLE = "abcdefghijklmnopqrstuvwxyz/\n\r-_'.0123456789";

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;

			string signature = br.ReadFixedLengthString(12);
			if (signature != "tlj_pack0001") throw new InvalidDataFormatException();

			uint hashNodeCount = br.ReadUInt32();
			uint nameTableIndexCount = br.ReadUInt32();
			uint nameTableLength = br.ReadUInt32();

			for (uint i = 0; i < hashNodeCount; i++)
			{
				// For each hash node
				uint fileOffset = br.ReadUInt32();
				uint fileLength = br.ReadUInt32();
				uint baseNodeIndex = br.ReadUInt32();
				uint pathLength = br.ReadUInt32();
				uint nameTableIndex = br.ReadUInt32();
			}

			// Name table
			for (uint i = 0; i < nameTableLength; i++)
			{
				// zero-terminated strings
				byte[] nameTableData = br.ReadUntil(new byte[] { 0 });
				StringBuilder sb = new StringBuilder();
				for (int j = 0; j < nameTableData.Length; j++)
				{
					sb.Append(NAME_LOOKUP_TABLE[nameTableData[j] - 1]);
				}
				string nameTableValue = sb.ToString();
			}

			// Name table indexes
			for (uint i = 0; i < nameTableIndexCount; i++)
			{
				uint index = br.ReadUInt32(); // Name table index data
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
