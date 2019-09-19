//
//  D00DataFormat.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.EdLib
{
	public class D00DataFormat : DataFormat
	{
		private AdlibBlockType mvarBlockType = AdlibBlockType.MusicData;
		public AdlibBlockType BlockType { get { return mvarBlockType; } set { mvarBlockType = value; } }

		private byte mvarPlayerVersion = 0x04;
		public byte PlayerVersion { get { return mvarPlayerVersion; } set { mvarPlayerVersion = value; } }

		private byte mvarTimerSpeed = 0x46;
		public byte TimerSpeed { get { return mvarTimerSpeed; } set { mvarTimerSpeed = value; } }

		private AdlibSoundcard mvarSoundcard = AdlibSoundcard.Adlib;
		public AdlibSoundcard Soundcard { get { return mvarSoundcard; } set { mvarSoundcard = value; } }

		private string mvarSongTitle = "";
		public string SongTitle { get { return mvarSongTitle; } set { mvarSongTitle = value; } }

		private string mvarSongArtist = "";
		public string SongArtist { get { return mvarSongArtist; } set { mvarSongArtist = value; } }

		private byte[] mvarReserved = new byte[32];
		public byte[] Reserved { get { return mvarReserved; } set { mvarReserved = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			byte[] magic = br.ReadBytes(6); // 'J', 'C', 'H', 0x26, 0x02, 0x66
			if (!magic.Match(new byte[] { (byte)'J', (byte)'C', (byte)'H', 0x26, 0x02, 0x66 }))
				throw new InvalidDataFormatException();

			byte blockType = br.ReadByte(); // 0x00 for music data
			if (blockType == 0)
			{
				mvarBlockType = ((AdlibBlockType)blockType);
			}
			else
			{
				mvarBlockType = AdlibBlockType.Unknown;
			}

			mvarPlayerVersion = br.ReadByte(); // Required player version; usually 0x04
			mvarTimerSpeed = br.ReadByte(); // Timer speed for the block; usually 0x46
			byte numberOfMusicAndSFX = br.ReadByte(); // Number of music and SFX; usually 0x01

			byte soundCard = br.ReadByte(); // Soundcard; usually 0x00 - Adlib
			if (soundCard == 0)
			{
				mvarSoundcard = ((AdlibSoundcard)soundCard);
			}
			else
			{
				mvarSoundcard = AdlibSoundcard.Unknown;
			}

			mvarSongTitle = br.ReadFixedLengthString(32);       // 32 bytes name of the music
			mvarSongArtist = br.ReadFixedLengthString(32);      // 32 bytes name of composer
			mvarReserved = br.ReadBytes(32);                        // 32 bytes reserved for future expansion

			short ptrTpoin = br.ReadInt16();                        // Pointer to "Tpoin" tables
			short ptrSeqPointer = br.ReadInt16();                   // Pointer to "SeqPointer" tables
			short ptrInstrument = br.ReadInt16();                   // Pointer to "Instrument" tables
			short ptrDataInfo = br.ReadInt16();                 // Pointer to "DataInfo" tables
			short ptrSpecial = br.ReadInt16();                  // Pointer to "Special" tables (SpFX)
			short endMark = br.ReadInt16();                     // Endmark (0xFFFF)

			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			bw.WriteBytes(new byte[] { 0x4A, 0x43, 0x48, 0x26, 0x02, 0x66 });
			switch (mvarBlockType)
			{
				case AdlibBlockType.MusicData:
				{
					bw.WriteByte((byte)0);
					break;
				}
				default:
				{
					bw.WriteByte((byte)0);
					break;
				}
			}

			bw.WriteByte(mvarPlayerVersion);
			bw.WriteByte(mvarTimerSpeed);

			bw.WriteByte(1); // numberOfMusicAndSFX: Number of music and SFX; usually 0x01

			switch (mvarSoundcard)
			{
				case AdlibSoundcard.Adlib:
				{
					bw.WriteByte((byte)0);
					break;
				}
				default:
				{
					bw.WriteByte((byte)0);
					break;
				}
			}

			bw.WriteFixedLengthString(mvarSongTitle, 32); // 32 bytes name of the music
			bw.WriteFixedLengthString(mvarSongArtist, 32); // 32 bytes name of composer
			bw.WriteFixedLengthBytes(mvarReserved, 32); // 32 bytes reserved for future expansion

			short ptrTpoin = 0;
			bw.WriteInt16(ptrTpoin);                     // Pointer to "Tpoin" tables

			short ptrSeqPointer = 0;                    // Pointer to "SeqPointer" tables
			bw.WriteInt16(ptrSeqPointer);

			short ptrInstrument = 0;                    // Pointer to "Instrument" tables
			bw.WriteInt16(ptrInstrument);

			short ptrDataInfo = 0;                  // Pointer to "DataInfo" tables
			bw.WriteInt16(ptrDataInfo);

			short ptrSpecial = 0;                   // Pointer to "Special" tables (SpFX)
			bw.WriteInt16(ptrSpecial);

			short endMark = 0;                      // Endmark (0xFFFF)
			bw.WriteInt16(endMark);

			throw new NotImplementedException();
		}
	}
}
