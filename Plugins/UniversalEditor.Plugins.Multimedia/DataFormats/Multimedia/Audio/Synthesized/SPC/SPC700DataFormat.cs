//
//  SPC700DataFormat.cs - provides a DataFormat for manipulating synthesized audio in SPC700 format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2014-2020 Mike Becker's Software
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

using MBS.Framework.Settings;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.SPC
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio in SPC700 format.
	/// </summary>
	public class SPC700DataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);

			dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(Generator), "_Generator", SPC700Emulator.Unknown,
			new ChoiceSetting.ChoiceSettingValue[]
			{
				new ChoiceSetting.ChoiceSettingValue("Unknown", "Unknown", SPC700Emulator.Unknown),
				new ChoiceSetting.ChoiceSettingValue("ZSNES", "ZSNES", SPC700Emulator.ZSNES),
				new ChoiceSetting.ChoiceSettingValue("Snes9x", "Snes9x", SPC700Emulator.Snes9x)
			}));

			return dfr;
		}

		public byte[] ID666Reserved { get; set; } = new byte[45];
		public byte ID666DefaultChannelDisables { get; set; } = 0;
		/// <summary>
		/// The emulator that generated this file.
		/// </summary>
		public SPC700Emulator Generator { get; set; } = SPC700Emulator.Unknown;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SynthesizedAudioObjectModel au = (objectModel as SynthesizedAudioObjectModel);

			Reader br = base.Accessor.Reader;
			string fileHeader = br.ReadFixedLengthString(33);
			if (!fileHeader.StartsWith("SNES-SPC700 Sound File Data")) throw new InvalidDataFormatException("File does not begin with \"SNES-SPC700 Sound File Data\"");

			byte[] flags = br.ReadBytes(2u);
			byte hasID666Value = br.ReadByte();
			bool hasID666 = (hasID666Value == 26);
			byte versionMinor = br.ReadByte();
			short regPC = br.ReadInt16();
			byte regA = br.ReadByte();
			byte regX = br.ReadByte();
			byte regY = br.ReadByte();
			byte regPSW = br.ReadByte();
			byte regSP = br.ReadByte();
			short regReserved = br.ReadInt16();
			if (hasID666)
			{
				au.Information.SongTitle = br.ReadNullTerminatedString(32);
				au.Information.AlbumTitle = br.ReadNullTerminatedString(32);
				au.Information.Creator = br.ReadNullTerminatedString(16);
				au.Information.Comments = br.ReadNullTerminatedString(32);
				string id666DumpDate = br.ReadNullTerminatedString(11);
				au.Information.DateCreated = new DateTime(int.Parse(id666DumpDate.Substring(0, 4)), int.Parse(id666DumpDate.Substring(3, 2)), int.Parse(id666DumpDate.Substring(5, 2)));
				au.Information.FadeOutDelay = int.Parse(br.ReadNullTerminatedString(3));
				au.Information.FadeOutLength = int.Parse(br.ReadNullTerminatedString(5));
				au.Information.SongArtist = br.ReadNullTerminatedString(32);
				this.ID666DefaultChannelDisables = br.ReadByte();
				Generator = (SPC700Emulator)br.ReadByte();
				byte[] id666Reserved = br.ReadBytes(45u);
				this.ID666Reserved = id666Reserved;
			}
			byte[] regRAM = br.ReadBytes(65536u);
			byte[] regDSP = br.ReadBytes(128u);
			byte[] regUnused2 = br.ReadBytes(64u);
			byte[] regExtra = br.ReadBytes(64u);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			SynthesizedAudioObjectModel au = objectModel as SynthesizedAudioObjectModel;
			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("SNES-SPC700 Sound File Data v0.30");
			bw.WriteBytes(new byte[] { 26, 26 });
			bool hasID666 = true;
			if (hasID666)
			{
				bw.WriteByte(26);
			}
			else
			{
				bw.WriteByte(27);
			}
			byte versionMinor = 30;
			bw.WriteByte(versionMinor);
			short regPC = 0;
			bw.WriteInt16(regPC);
			byte regA = 0;
			bw.WriteByte(regA);
			byte regX = 0;
			bw.WriteByte(regX);
			byte regY = 0;
			bw.WriteByte(regY);
			byte regPSW = 0;
			bw.WriteByte(regPSW);
			byte regSP = 0;
			bw.WriteByte(regSP);
			short regReserved = 0;
			bw.WriteInt16(regReserved);
			if (hasID666)
			{
				bw.WriteFixedLengthString(au.Information.SongTitle, 32);
				bw.WriteFixedLengthString(au.Information.AlbumTitle, 32);
				bw.WriteFixedLengthString(au.Information.Creator, 16);
				bw.WriteFixedLengthString(au.Information.Comments, 32);
				string id666DumpDate = au.Information.DateCreated.ToString("YYYYMMDD");
				bw.WriteFixedLengthString(id666DumpDate, 11);
				Writer arg_15E_0 = bw;
				int num = au.Information.FadeOutDelay;
				arg_15E_0.WriteFixedLengthString(num.ToString(), 3);
				Writer arg_17A_0 = bw;
				num = au.Information.FadeOutLength;
				arg_17A_0.WriteFixedLengthString(num.ToString(), 5);
				bw.WriteFixedLengthString(au.Information.SongArtist, 32);
				bw.WriteByte(ID666DefaultChannelDisables);
				bw.WriteByte((byte)Generator);

				bw.WriteFixedLengthBytes(this.ID666Reserved, 45);
			}
			byte[] regRAM = new byte[65536];
			bw.WriteBytes(regRAM);
			byte[] regDSP = new byte[128];
			bw.WriteBytes(regDSP);
			byte[] regUnused2 = new byte[64];
			bw.WriteBytes(regUnused2);
			byte[] regExtra = new byte[64];
			bw.WriteBytes(regExtra);
			bw.Flush();
		}
	}
}
