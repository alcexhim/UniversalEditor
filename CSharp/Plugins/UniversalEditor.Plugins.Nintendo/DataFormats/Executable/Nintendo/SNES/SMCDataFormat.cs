using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	public class SMCDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Nintendo SNES executable", new string[] { "*.smc", "*.sfc", "*.swc", "*.fig", "*.ufo", "*.?gm" });
				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SNES_ROM_layout");
				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SNES_header");
				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SMC_header");
			}
			return _dfr;
		}

		private SMCExtendedHeader mvarExtendedHeader = new SMCExtendedHeader();
		public SMCExtendedHeader ExtendedHeader { get { return mvarExtendedHeader; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (exe == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			mvarExtendedHeader.Enabled = (base.Accessor.Length % 1024 == 512);
			if (mvarExtendedHeader.Enabled)
			{
				#region 00-02 The size of the ROM dump, in units of 8 kilobytes, as a little-endian integer.
				{
					mvarExtendedHeader.FileSize = reader.ReadInt16();
					mvarExtendedHeader.FileSize *= 8000;
				}
				#endregion
				#region 02-03 Flags
				byte flags = reader.ReadByte();
				mvarExtendedHeader.SplitFile = ((flags & 0x40) == 0x40);
				mvarExtendedHeader.HiRomEnabled = ((flags & 0x30) == 0x30);

				if ((flags & 0x04) == 0x04)
				{
					mvarExtendedHeader.SaveRAMSize = SMCSaveRAMSize.SaveRAM8K;
				}
				else if ((flags & 0x08) == 0x08)
				{
					mvarExtendedHeader.SaveRAMSize = SMCSaveRAMSize.SaveRAM2K;
				}
				else if ((flags & 0x0C) == 0x0C)
				{
					mvarExtendedHeader.SaveRAMSize = SMCSaveRAMSize.SaveRAMNone;
				}

				if ((flags & 0x80) == 0x80)
				{
					mvarExtendedHeader.ResetVectorOverride = 0x8000;
				}
				#endregion
				#region 03-04 HiRom/LoRom (Pro Fighter specific)
				byte hiRomLoRom = reader.ReadByte();
				if ((flags & 0x30) != 0x30)
				{
					// only set HiRom/LoRom from this field if not set in a flag
					mvarExtendedHeader.HiRomEnabled = ((hiRomLoRom & 0x80) == 0x80);
				}
				#endregion
				#region 04-06 DSP-1 settings (Pro Fighter specific)
				mvarExtendedHeader.DSP1Settings = reader.ReadInt16();
				#endregion
				#region 06-08 Unknown
				ushort unknown1 = reader.ReadUInt16();
				#endregion
				#region 08-16 SUPERUFO
				string creator = reader.ReadFixedLengthString(8); // SUPERUFO
				mvarExtendedHeader.Creator = creator;
				#endregion
				#region 16-24 Extra data
				byte[] extradata = reader.ReadBytes(8);
				#endregion

				if (mvarExtendedHeader.HiRomEnabled)
				{
					base.Accessor.Seek(0x101c0, SeekOrigin.Begin);
				}
				else
				{
					base.Accessor.Seek(0x81c0, SeekOrigin.Begin);
				}
			}

			#region SNES header
			string gamename = reader.ReadFixedLengthString(21).Trim();
			byte romLayout = reader.ReadByte();
			if (romLayout == 0x20)
			{
				// LoROM
			}
			else if (romLayout == 0x21)
			{
				// HiROM
			}
			byte cartridgeType = reader.ReadByte();
			byte romsize = reader.ReadByte();
			byte ramsize = reader.ReadByte();

			// Country code, which selects the video in the emulator. Values $00, $01, $0d use NTSC.
			// Values in range $02..$0c use PAL. Other values are invalid.
			byte countryCode = reader.ReadByte();

			// Licensee code. If this value is $33, then the ROM has an extended header with ID at
			// $ffb2..$ffb5. 
			byte licenseeCode = reader.ReadByte();

			byte versionNumber = reader.ReadByte();
			#endregion
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (exe == null) throw new ObjectModelNotSupportedException();

			throw new NotImplementedException();
		}
	}
}
