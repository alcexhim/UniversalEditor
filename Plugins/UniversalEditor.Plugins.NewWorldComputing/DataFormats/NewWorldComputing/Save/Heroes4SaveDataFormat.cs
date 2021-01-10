//
//  Heroes4SaveDataFormat.cs - provides a DataFormat for manipulating Heroes of Might and Magic IV game save data files
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
using UniversalEditor.ObjectModels.NewWorldComputing.Save;

namespace UniversalEditor.DataFormats.NewWorldComputing.Save
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Heroes of Might and Magic IV game save data files.
	/// </summary>
	public class Heroes4SaveDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SaveObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			byte[] dataCompressed = br.ReadToEnd();
			byte[] dataUncompressed = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(dataCompressed);

			br = new IO.Reader(new MemoryAccessor(dataUncompressed));

			string H4_SAVE_GAME = br.ReadFixedLengthString(12);
			if (H4_SAVE_GAME != "H4_SAVE_GAME") throw new InvalidDataFormatException();

			short unknown1 = br.ReadInt16();
			short unknown2 = br.ReadInt16();
			short unknown3 = br.ReadInt16();
			short unknown4 = br.ReadInt16();
			short unknown5 = br.ReadInt16();
			short unknown6 = br.ReadInt16();
			short unknown7 = br.ReadInt16();

			string originalFileName = br.ReadInt16String();

			byte unknown8 = br.ReadByte();
			byte unknown9 = br.ReadByte();
			byte unknown10 = br.ReadByte();

			string title = br.ReadInt16String();

			short unknown11 = br.ReadInt16();
			short unknown12 = br.ReadInt16();
			short unknown13 = br.ReadInt16();
			short unknown14 = br.ReadInt16();

			string description = br.ReadInt16String();

			byte[] unknown = br.ReadBytes(77);

			string loseCondition = br.ReadInt16String();
			string winCondition = br.ReadInt16String();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
	}
}
