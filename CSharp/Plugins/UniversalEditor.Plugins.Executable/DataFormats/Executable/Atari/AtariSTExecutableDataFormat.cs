//
//  AtariSTExecutableDataFormat.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2019 Mike Becker
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
using System.Collections.Generic;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;
// Code adapted from reverse-engineering provided by DaFi <webmaster@freudenstadt.net>

namespace UniversalEditor.DataFormats.Executable.Atari
{
	/// <summary>
	/// Description of AtariSTDocument.
	/// </summary>
	public class AtariSTExecutableDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
				/*
				List<string> supportedExtensions = new List<string>();
				supportedExtensions.Add("*.tos");
				supportedExtensions.Add("*.prg");
				supportedExtensions.Add("*.ttp");
				supportedExtensions.Add("*.prx");
				supportedExtensions.Add("*.gtp");
				supportedExtensions.Add("*.app");
				supportedExtensions.Add("*.acc");
				supportedExtensions.Add("*.acx");
				mvarSupportedFilters = new System.Collections.ObjectModel.ReadOnlyCollection<string>(supportedExtensions);
				*/
			}
			return _dfr;
		}
		
		public bool IsGEMGuiEnabled
		{
			get
			{
				string ext = System.IO.Path.GetExtension(base.Accessor.GetFileName());
				return (ext != ".tos") && (ext != ".ttp");
			}
		}
		
		private byte[] mvarTextSegment = new byte[] { };
		public byte[] TextSegment { get { return mvarTextSegment; } set { mvarTextSegment = value; } }
		
		private byte[] mvarDataSegment = new byte[] { };
		public byte[] DataSegment { get { return mvarDataSegment; } set { mvarDataSegment = value; } }
		
		private byte[] mvarBssSegment = new byte[] { };
		public byte[] BssSegment { get { return mvarBssSegment; } set { mvarBssSegment = value; } }
		
		private byte[] mvarSymbolTableSegment = new byte[] { };
		public byte[] SymbolTableSegment { get { return mvarSymbolTableSegment; } set { mvarSymbolTableSegment = value; } }
		
		private bool mvarNeedsRelocation = false;
		public bool NeedsRelocation { get { return mvarNeedsRelocation; } set { mvarNeedsRelocation = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ExecutableObjectModel fsom = (objectModel as ExecutableObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;

			ushort PRG_magic = br.ReadUInt16(); // Should be 0x601A
			uint PRG_tsize = br.ReadUInt32();   // Size of text segment
			uint PRG_dsize = br.ReadUInt32();   // Size of data segment
			uint PRG_bsize = br.ReadUInt32();   // Size of bss segment
			uint PRG_ssize = br.ReadUInt32();   // Size of symbol table
			uint PRG_res1 = br.ReadUInt32();   // Reserved
			uint PRGFLAGS = br.ReadUInt32();    // bit vector that defines additional process characteristics, as follows:
												// 		Bit 0 PF_FASTLOAD - if set, only the BSS area is cleared, otherwise,
												//							the program´s whole memory is cleared before loading
												//		Bit 1 PF_TTRAMLOAD - if set, the program will be loaded into TT RAM
												//		Bit 2 PF_TTRAMMEM - if set, the program will be allowed to allocate
												//							memory from TT RAM
												//	Bit 4 AND 5 as a two bit value with the following meanings:
												//		0 PF_PRIVATE - the processes entire memory space is considered private
												//		1 PF_GLOBAL - the processes memory will be r/w-allowed for others
												//		2 PF_SUPER - the memory will be r/w for itself and any supervisor proc
												//		3 PF_READ - the memory will be readable by others
			ushort ABSFLAG = br.ReadUInt16();       //  Non-zero if the program does not need to be relocated; zero if it does

			byte[] SEG_text = br.ReadBytes(PRG_tsize);
			byte[] SEG_data = br.ReadBytes(PRG_dsize);
			byte[] SEG_symb = br.ReadBytes(PRG_ssize);

			bool readFixup = false;
			if (readFixup)
			{
				uint FIXUP_offset = br.ReadUInt32();
				while (true)
				{
					byte fixup = br.ReadByte();
					if (fixup == 0) break;
					if (fixup == 254)
					{
						br.Seek(254, SeekOrigin.Current);
						continue;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ExecutableObjectModel fsom = (objectModel as ExecutableObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;

			bw.WriteUInt16(0x601A);                       // Should be 0x601A
			bw.WriteInt32(mvarTextSegment.Length);               // Size of text segment
			bw.WriteInt32(mvarDataSegment.Length);               // Size of data segment
			bw.WriteInt32(mvarBssSegment.Length);                // Size of bss segment
			bw.WriteInt32(mvarSymbolTableSegment.Length);        // Size of symbol table
			bw.WriteInt32(0);                              // Reserved

			uint PRGFLAGS = 0;
			bw.WriteUInt32(PRGFLAGS);                         // bit vector that defines additional process characteristics, as follows:
														// 		Bit 0 PF_FASTLOAD - if set, only the BSS area is cleared, otherwise,
														//							the program´s whole memory is cleared before loading
														//		Bit 1 PF_TTRAMLOAD - if set, the program will be loaded into TT RAM
														//		Bit 2 PF_TTRAMMEM - if set, the program will be allowed to allocate
														//							memory from TT RAM
														//	Bit 4 AND 5 as a two bit value with the following meanings:
														//		0 PF_PRIVATE - the processes entire memory space is considered private
														//		1 PF_GLOBAL - the processes memory will be r/w-allowed for others
														//		2 PF_SUPER - the memory will be r/w for itself and any supervisor proc
														//		3 PF_READ - the memory will be readable by others

			if (mvarNeedsRelocation)                        //  Non-zero if the program does not need to be relocated; zero if it does
			{
				bw.WriteUInt16(0);
			}
			else
			{
				bw.WriteUInt16(1);
			}

			bw.WriteBytes(mvarTextSegment);
			bw.WriteBytes(mvarDataSegment);
			bw.WriteBytes(mvarSymbolTableSegment);

			bool writeFixup = false;
			if (writeFixup)
			{
				uint FIXUP_offset = 0;
				bw.WriteUInt32(FIXUP_offset);
				while (true)
				{
					byte fixup = 0;
					bw.WriteByte(fixup);
				}
			}
		}
	}
}
