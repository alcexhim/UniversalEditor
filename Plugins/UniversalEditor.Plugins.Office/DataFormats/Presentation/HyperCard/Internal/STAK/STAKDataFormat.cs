//
//  STAKDataFormat.cs
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

namespace UniversalEditor.Plugins.Office.DataFormats.Presentation.HyperCard.Internal.STAK
{
	public class STAKDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			STAKObjectModel stak = (objectModel as STAKObjectModel);

			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			int blockId = reader.ReadInt32();
			byte[] unknown1 = reader.ReadBytes(32);

			stak.CardCount = reader.ReadUInt32();
			uint cardIDOne = reader.ReadUInt32(); // starting card?
			uint listBlockID = reader.ReadUInt32();

			byte[] unknown2 = reader.ReadBytes(16);

			stak.UserLevel = (HyperCardUserLevel)reader.ReadUInt16();
			ushort unknown3 = reader.ReadUInt16();
			ushort flags = reader.ReadUInt16(); // Bit 10 is cantPeek, 11 is cantAbort, 13 is privateAccess, 14 is cantDelete, 15 is cantModify

			byte[] unknown4 = reader.ReadBytes(18);

			// Three 4-byte NumVersion entries containing the HyperCard version numbers that created, last edited or compacted
			// this stack. (A/N: I know the "documentation" says "three 4-byte", but it also says "16 bytes", so idk...)
			uint[] numVersions = reader.ReadUInt32Array(4);

			byte[] unknown5 = reader.ReadBytes(328); // ???
			stak.Height = reader.ReadUInt16(); // the height, in pixels, of cards in this stack
			stak.Width = reader.ReadUInt16(); // the width, in pixels, of cards in this stack

			byte[] unknown6 = reader.ReadBytes(260);

			uint patternCount = 40;
			for (uint i = 0; i < patternCount; i++)
			{
				byte[] patternData = reader.ReadBytes(8); // raw data for an 8x8 bitmap, with one byte representing one row
			}

			byte[] unknown7 = reader.ReadBytes(512);

			stak.Script = HyperCardScript.Parse(reader.ReadNullTerminatedString());
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			STAKObjectModel stak = (objectModel as STAKObjectModel);

			Writer writer = Accessor.Writer;
			writer.Endianness = Endianness.BigEndian;

			writer.WriteInt32(-1); // block id
			writer.WriteBytes(new byte[32]); // unknown1

			writer.WriteUInt32(stak.CardCount);
			writer.WriteUInt32(0); // cardIDOne - starting card?
			writer.WriteUInt32(0); // listBlockID

			writer.WriteBytes(new byte[16]);

			writer.WriteUInt16((ushort)stak.UserLevel);

			writer.WriteUInt16(0);  // unknown3
			ushort flags = 0; 
			writer.WriteUInt16(flags); // Bit 10 is cantPeek, 11 is cantAbort, 13 is privateAccess, 14 is cantDelete, 15 is cantModify

			writer.WriteBytes(new byte[18]); // unknown4
			writer.WriteUInt32Array(new uint[4]); //versions
			writer.WriteBytes(new byte[328]); // unknown5
			writer.WriteUInt16(stak.Height);
			writer.WriteUInt16(stak.Width);
			writer.WriteBytes(new byte[260]); // unknown6
			writer.WriteBytes(new byte[40 * 8]); // pattern data
			writer.WriteBytes(new byte[512]);
			writer.WriteNullTerminatedString(stak.Script.Text);
		}
	}
}
