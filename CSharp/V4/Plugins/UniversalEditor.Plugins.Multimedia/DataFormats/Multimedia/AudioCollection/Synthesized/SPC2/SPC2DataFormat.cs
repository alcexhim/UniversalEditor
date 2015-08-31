// Universal Editor DataFormat for loading SPC2 synthesized audio files
// Copyright (C) 2014  Mike Becker's Software
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.SPC;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia.AudioCollection.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.AudioCollection.Synthesized.SPC2
{
	public class SPC2DataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SynthesizedAudioCollectionObjectModel), DataFormatCapabilities.All);
				_dfr.ImportOptions.Add(new CustomOptionBoolean("UseID666TagInformationIfAvailable", "Use &ID666 tag information if available"));
				_dfr.ExportOptions.Add(new CustomOptionBoolean("IncludeID666TagInformation", "Include &ID666 tag information in output file"));
				_dfr.Sources.Add("http://blog.kevtris.org/blogfiles/spc2_file_specification_v1.txt");
			}
			return _dfr;
		}

		private bool mvarUseID666TagInformationIfAvailable = false;
		public bool UseID666TagInformationIfAvailable { get { return mvarUseID666TagInformationIfAvailable; } set { mvarUseID666TagInformationIfAvailable = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SynthesizedAudioCollectionObjectModel coll = (objectModel as SynthesizedAudioCollectionObjectModel);
			if (coll == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(4);
			byte x1A = reader.ReadByte();
			if (signature != "KSPC" || x1A != 0x1A) throw new InvalidDataFormatException("File does not begin with 'KSPC', 0x1A");

			byte versionMajor = reader.ReadByte();
			byte versionMinor = reader.ReadByte();

			ushort fileCount = reader.ReadUInt16();
			byte[] expansion = reader.ReadBytes(7);

			for (ushort i = 0; i < fileCount; i++)
			{
				// Each SPC data block has this format
				for (int j = 0; j < 256; j++)
				{
					// Offsets for all 256, 256 byte blocks of data in RAM
					ushort dataBlockRAMOffset = reader.ReadUInt16();
				}

				// DSP register data (as-is from .SPC)
				byte[] dspRegisterData = reader.ReadBytes(128);

				// IPL ROM (as-is from .SPC)
				byte[] iplROM = reader.ReadBytes(64);

				// PCL, PCH, A, X, Y, PSW, SP (as-is from .SPC)
				byte registerPCL = reader.ReadByte();
				byte registerPCH = reader.ReadByte();
				byte registerA = reader.ReadByte();
				byte registerX = reader.ReadByte();
				byte registerY = reader.ReadByte();
				byte registerPSW = reader.ReadByte();
				byte registerSP = reader.ReadByte();

				// channel enable bits, 1 per chan (0 = en, 1 = dis) (as-is)
				byte channelEnableBits = reader.ReadByte();

				// stored as four bytes, in mm/dd/yyyy as BCD, like so:
				// 01h, 12h, 20h, 00h would be 01/12/2000
				// 07h, 30h, 19h, 95h would be 07/30/1995
				byte[] dateParts = reader.ReadBytes(4);
				DateTime date = new DateTime
				(
					Int32.Parse(dateParts[2].ToString("X").PadLeft(2, '0') + dateParts[3].ToString("X").PadLeft(2, '0')),
					Int32.Parse(dateParts[0].ToString("X").PadLeft(2, '0')),
					Int32.Parse(dateParts[1].ToString("X").PadLeft(2, '0'))
				);

				// 1/64000th's to play before fadeout
				uint fadeoutDelay = reader.ReadUInt32();
				// 1/64000th's to fade to silence
				uint fadeoutLength = reader.ReadUInt32();

				// amplification value (10000h == 1.00)
				uint iAmplificationValue = reader.ReadUInt32();
				double amplificationValue = (double)iAmplificationValue * 0.00001;

				SPC700Emulator emulator = (SPC700Emulator)reader.ReadByte();

				byte ostDisk = reader.ReadByte();
				byte ostTrackNumber = reader.ReadByte();

				ushort copyrightYear = reader.ReadUInt16();

				byte[] unused = reader.ReadBytes(34);

				SynthesizedAudioCollectionTrack track = new SynthesizedAudioCollectionTrack();
				track.SongTitle = reader.ReadFixedLengthString(32).TrimNull().Trim();
				track.GameTitle = reader.ReadFixedLengthString(32).TrimNull().Trim();
				track.ArtistName = reader.ReadFixedLengthString(32).TrimNull().Trim();
				track.DumperName = reader.ReadFixedLengthString(32).TrimNull().Trim();
				track.Comments = reader.ReadFixedLengthString(32).TrimNull().Trim();
				track.AlbumTitle = reader.ReadFixedLengthString(32).TrimNull().Trim();
				track.PublisherName = reader.ReadFixedLengthString(32).TrimNull().Trim();
				track.OriginalFileName = reader.ReadFixedLengthString(28).TrimNull().Trim();

				uint extendedDataBlockOffset = reader.ReadUInt32();
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			SynthesizedAudioCollectionObjectModel coll = (objectModel as SynthesizedAudioCollectionObjectModel);
			if (coll == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
		}
	}
}
