using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.BinHex
{
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

		private bool mvarRequireWarningComment = true;
		/// <summary>
		/// Determines if the "This file must be converted with BinHex" message is written to the
		/// output file. This message is required by RFC-1741.
		/// </summary>
		public bool RequireWarningComment { get { return mvarRequireWarningComment; } set { mvarRequireWarningComment = value; } }

		private Version mvarFormatVersion = new Version(4, 0);
		public Version FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

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
				if (mvarRequireWarningComment) throw new InvalidDataFormatException("File does not begin with BinHex warning comment");
				base.Accessor.LoadPosition();
			}
			else
			{
				line = line.Substring("(This file must be converted with BinHex ".Length);
				line = line.Substring(0, line.Length - 1);
				mvarFormatVersion = new Version(line);
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
			if (mvarRequireWarningComment)
			{
				writer.WriteLine("(This file must be converted with BinHex " + mvarFormatVersion.ToString() + ")");
				writer.WriteLine();
			}

			throw new NotImplementedException();

			if (fsom.Files.Count > 0)
			{
				File file = fsom.Files[0];
				byte[] data = file.GetDataAsByteArray();

			}
		}
	}
}
