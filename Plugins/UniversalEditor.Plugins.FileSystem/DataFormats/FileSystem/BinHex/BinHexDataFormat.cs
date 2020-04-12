//
//  BinHexDataFormat.cs - provides a DataFormat for manipulating archives in BinHex format
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
using System.Linq;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.BinHex
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in BinHex format.
	/// </summary>
	public class BinHexDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Determines if the "This file must be converted with BinHex" message is written to the
		/// output file. This message is required by RFC-1741.
		/// </summary>
		public bool RequireWarningComment { get; set; } = true;
		/// <summary>
		/// Gets or sets the format version of the BinHex file.
		/// </summary>
		/// <value>The format version of the BinHex file.</value>
		public Version FormatVersion { get; set; } = new Version(4, 0);

		private string mvarCharacterTable = "!\"#$%&'()*+,-012345689@ABCDEFGHIJKLMNPQRSTUVXYZ[`abcdefhijklmpqr";

		private byte CharacterToIndex(char value)
		{
			if (!mvarCharacterTable.Contains(value)) throw new ArgumentOutOfRangeException("Character '" + value.ToString() + "' does not exist in valid character table");
			return (byte)(mvarCharacterTable.IndexOf(value));
		}
		private char IndexToCharacter(byte value)
		{
			if (value < 0 || value > mvarCharacterTable.Length - 1) throw new ArgumentOutOfRangeException("Byte '" + value.ToString() + "' does not have an entry in valid character table");
			return mvarCharacterTable[(int)value];
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			base.Accessor.SavePosition();

			string line = reader.ReadLine();
			if (!(line.StartsWith("(This file must be converted with BinHex ") && line.EndsWith(")")))
			{
				if (RequireWarningComment) throw new InvalidDataFormatException("File does not begin with BinHex warning comment");
				base.Accessor.LoadPosition();
			}
			else
			{
				line = line.Substring("(This file must be converted with BinHex ".Length);
				line = line.Substring(0, line.Length - 1);
				FormatVersion = new Version(line);
			}
			base.Accessor.ClearLastPosition();

			string inputStr = reader.ReadStringToEnd();

			inputStr = inputStr.Replace("\r", String.Empty);
			inputStr = inputStr.Replace("\n", String.Empty);

			if (!inputStr.StartsWith(":"))
			{

			}
			else
			{
				inputStr = inputStr.Substring(1);
			}

			if (!inputStr.EndsWith(":"))
			{

			}
			else
			{
				inputStr = inputStr.Substring(0, inputStr.Length - 1);
			}

			char[] input = inputStr.ToCharArray();


			byte[] data = null;
			#region Magic inside
			{
				int[] value = new int[128];
				for (int i = 0; i < value.Length; ++i)
					value[i] = -1;
				for (int i = 0; i < mvarCharacterTable.Length; ++i)
					value[mvarCharacterTable[i]] = i;

				int accum = 0;
				int alen = 0;

				Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor();
				Writer bw = new Writer(ma);

				for (int i = 0; i < input.Length; i++)
				{
					if (i == input.Length - 1) break;

					int chr = input[i];

					int v = -1, c = chr & 0x7f;
					if (chr == 0x90)
					{

					}
					if (c == chr)
					{
						v = value[c];
						if (v != -1)
						{
							accum = (accum << 6) | v;
							alen += 6;
							if (alen > 8)
							{
								alen -= 8;
								bw.WriteByte((byte)(accum >> alen));
							}
						}
						else
						{

						}
					}
					else
					{

					}
				}

				bw.Flush();
				bw.Close();

				data = ma.ToArray();
			}
			#endregion

			Accessors.MemoryAccessor ma1 = new Accessors.MemoryAccessor(data);
			Reader rdr1 = new Reader(ma1);
			byte fileNameLength = rdr1.ReadByte();
			string fileName = rdr1.ReadFixedLengthString(fileNameLength);
			byte nul1 = rdr1.ReadByte();

			rdr1.Endianness = Endianness.BigEndian;

			string FOURCC_type = rdr1.ReadFixedLengthString(4);
			string FOURCC_author = rdr1.ReadFixedLengthString(4);
			short flag = rdr1.ReadInt16();
			int dlen = rdr1.ReadInt32();
			int rlen = rdr1.ReadInt32();
			short headerChecksum = rdr1.ReadInt16();

			if (dlen > rdr1.Remaining)
			{
				throw new InvalidDataFormatException("Insanity check: data fork length goes past the end of file");
			}
			byte[] dataFork = rdr1.ReadBytes(dlen);
			short dataChecksum = rdr1.ReadInt16();

			if (rlen > rdr1.Remaining)
			{
				throw new InvalidDataFormatException("Insanity check: resource fork length goes past the end of file");
			}
			byte[] resourceFork = rdr1.ReadBytes(rlen);
			short resourceChecksum = rdr1.ReadInt16();

			fsom.Files.Add(fileName, dataFork);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.NewLineSequence = NewLineSequence.CarriageReturn;
			if (RequireWarningComment)
			{
				writer.WriteLine("(This file must be converted with BinHex " + FormatVersion.ToString() + ")");
				writer.WriteLine();
			}

			throw new NotImplementedException();

			if (fsom.Files.Count > 0)
			{
				File file = fsom.Files[0];
				byte[] data = file.GetData();

			}
		}
	}
}
