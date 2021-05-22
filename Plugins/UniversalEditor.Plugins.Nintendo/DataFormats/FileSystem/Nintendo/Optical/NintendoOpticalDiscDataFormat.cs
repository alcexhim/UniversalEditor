//
//  NintendoOpticalDiscDataFormat.cs - provides a DataFormat for manipulating Nintendo optical disc images
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
using System.Collections.Generic;
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.Nintendo.Optical
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Nintendo optical disc images.
	/// </summary>
	public class NintendoOpticalDiscDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(FormatCode), "_Format code", NintendoOpticalDiscFormatCodes.Wii, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("Revolution", "Revolution", NintendoOpticalDiscFormatCodes.Revolution),
					new ChoiceSetting.ChoiceSettingValue("Wii", "Wii", NintendoOpticalDiscFormatCodes.Wii),
					new ChoiceSetting.ChoiceSettingValue("GameCube", "GameCube", NintendoOpticalDiscFormatCodes.GameCube),
					new ChoiceSetting.ChoiceSettingValue("Utility", "Utility", NintendoOpticalDiscFormatCodes.Utility),
					new ChoiceSetting.ChoiceSettingValue("GameCubeDemo", "GameCube (Demo)", NintendoOpticalDiscFormatCodes.GameCubeDemo),
					new ChoiceSetting.ChoiceSettingValue("GameCubePromotional", "GameCube (Promotional)", NintendoOpticalDiscFormatCodes.GameCubePromotional),
					new ChoiceSetting.ChoiceSettingValue("Diagnostic", "Diagnostic", NintendoOpticalDiscFormatCodes.Diagnostic),
					new ChoiceSetting.ChoiceSettingValue("Diagnostic1", "Diagnostic1", NintendoOpticalDiscFormatCodes.Diagnostic1),
					new ChoiceSetting.ChoiceSettingValue("WiiBackup", "Wii (Backup)", NintendoOpticalDiscFormatCodes.WiiBackup),
					new ChoiceSetting.ChoiceSettingValue("WiiFitChanInstaller", "Wii Fit -chan Installer", NintendoOpticalDiscFormatCodes.WiiFitChanInstaller)
				}));
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(GameCode), "_Game code", String.Empty, 2));
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(RegionCode), "_Region code", NintendoOpticalDiscRegionCodes.UnitedStates, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("German", "Germany", NintendoOpticalDiscRegionCodes.German),
					new ChoiceSetting.ChoiceSettingValue("UnitedStates", "United States", NintendoOpticalDiscRegionCodes.UnitedStates),
					new ChoiceSetting.ChoiceSettingValue("France", "France", NintendoOpticalDiscRegionCodes.France),
					new ChoiceSetting.ChoiceSettingValue("Italy", "Italy", NintendoOpticalDiscRegionCodes.Italy),
					new ChoiceSetting.ChoiceSettingValue("Japan", "Japan", NintendoOpticalDiscRegionCodes.Japan),
					new ChoiceSetting.ChoiceSettingValue("Korea", "Korea", NintendoOpticalDiscRegionCodes.Korea),
					new ChoiceSetting.ChoiceSettingValue("PAL", "Other PAL", NintendoOpticalDiscRegionCodes.PAL),
					new ChoiceSetting.ChoiceSettingValue("Russia", "Russia", NintendoOpticalDiscRegionCodes.Russia),
					new ChoiceSetting.ChoiceSettingValue("Spanish", "Spain", NintendoOpticalDiscRegionCodes.Spanish),
					new ChoiceSetting.ChoiceSettingValue("Taiwan", "Taiwan", NintendoOpticalDiscRegionCodes.Taiwan),
					new ChoiceSetting.ChoiceSettingValue("Australia", "Australia", NintendoOpticalDiscRegionCodes.Australia),
				}));

				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(SystemType), "_System type", NintendoOpticalDiscSystemType.Wii, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("Unknown", "Unknown", NintendoOpticalDiscSystemType.Unknown),
					new ChoiceSetting.ChoiceSettingValue("GameCube", "GameCube", NintendoOpticalDiscSystemType.GameCube),
					new ChoiceSetting.ChoiceSettingValue("Wii", "Wii", NintendoOpticalDiscSystemType.Wii)
				}));

				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(GameTitle), "Game _title", String.Empty, 64));

				_dfr.Sources.Add("http://wiibrew.org/wiki/Wii_Disc");
				_dfr.Sources.Add("http://www.emutalk.net/threads/21512-GCM-file-extension!/page3");
				_dfr.Sources.Add("http://hitmen.c02.at/files/yagcd/yagcd/chap13.html");
			}
			return _dfr;
		}

		private NintendoOpticalDiscFormatCode mvarFormatCode = NintendoOpticalDiscFormatCodes.Wii;
		public NintendoOpticalDiscFormatCode FormatCode { get { return mvarFormatCode; } set { mvarFormatCode = value; } }

		private string mvarGameCode = String.Empty;
		public string GameCode { get { return mvarGameCode; } set { mvarGameCode = value; } }

		private NintendoOpticalDiscRegionCode mvarRegionCode = NintendoOpticalDiscRegionCodes.UnitedStates;
		public NintendoOpticalDiscRegionCode RegionCode { get { return mvarRegionCode; } set { mvarRegionCode = value; } }

		private string mvarMakerCode = String.Empty;
		public string MakerCode { get { return mvarMakerCode; } set { mvarMakerCode = value; } }

		private byte mvarDiscNumber = 0;
		public byte DiscNumber { get { return mvarDiscNumber; } set { mvarDiscNumber = value; } }

		private byte mvarDiscVersion = 0;
		public byte DiscVersion { get { return mvarDiscVersion; } set { mvarDiscVersion = value; } }

		private NintendoOpticalDiscSystemType mvarSystemType = NintendoOpticalDiscSystemType.Wii;
		public NintendoOpticalDiscSystemType SystemType { get { return mvarSystemType; } set { mvarSystemType = value; } }

		private string mvarGameTitle = String.Empty;
		public string GameTitle { get { return mvarGameTitle; } set { mvarGameTitle = value; } }

		private bool mvarDisableHashVerification = false;
		/// <summary>
		/// Disable hash verification and make all disc reads fail even before they reach the DVD drive. Neither this nor
		/// <see cref="DisableDiscEncryption"/> will allow unsigned code.
		/// </summary>
		public bool DisableHashVerification { get { return mvarDisableHashVerification; } set { mvarDisableHashVerification = value; } }

		private bool mvarDisableDiscEncryption = false;
		/// <summary>
		/// Disable disc encryption and h3 hash table loading and verification (which effectively also makes all disc reads fail because
		/// the h2 hashes won't be able to verify against "something" that will be in the memory of the h3 hash table. Neither this nor
		/// <see cref="DisableHashVerification"/> will allow unsigned code.
		/// </summary>
		public bool DisableDiscEncryption { get { return mvarDisableDiscEncryption; } set { mvarDisableDiscEncryption = value; } }

		private static readonly byte[] WII_MAGIC_WORD = new byte[] { 0x5D, 0x1C, 0x9E, 0xA3 };
		private static readonly byte[] GAMECUBE_MAGIC_WORD = new byte[] { 0xC2, 0x33, 0x9F, 0x3D };

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			string gameMagic = reader.ReadFixedLengthString(4);
			// gameMagic: 4 chars, [1:Disc ID][2:Game Code][1:Region Code]
			mvarFormatCode = NintendoOpticalDiscFormatCode.FromCode(gameMagic[0]);
			mvarGameCode = gameMagic.Substring(1, 2);
			mvarRegionCode = NintendoOpticalDiscRegionCode.FromCode(gameMagic[3]);

			mvarMakerCode = reader.ReadFixedLengthString(2);
			mvarDiscNumber = reader.ReadByte();
			mvarDiscVersion = reader.ReadByte();

			byte audioStreaming = reader.ReadByte();
			byte streamingBufferSize = reader.ReadByte();

			byte[] unused = reader.ReadBytes(14);

			byte[] wiiMagicWord = reader.ReadBytes(4);
			byte[] gameCubeMagicWord = reader.ReadBytes(4);

			bool isWii = wiiMagicWord.Match(WII_MAGIC_WORD);
			bool isGameCube = gameCubeMagicWord.Match(GAMECUBE_MAGIC_WORD);

			if (isWii && isGameCube)
			{
				Console.WriteLine("Unexpected twist: disc image is BOTH a GameCube and Wii??");
				mvarSystemType = NintendoOpticalDiscSystemType.GameCube | NintendoOpticalDiscSystemType.Wii;
			}
			else if (isWii)
			{
				mvarSystemType = NintendoOpticalDiscSystemType.Wii;
			}
			else if (isGameCube)
			{
				mvarSystemType = NintendoOpticalDiscSystemType.GameCube;
			}
			else
			{
				mvarSystemType = NintendoOpticalDiscSystemType.Unknown;
			}

			// from http://wiibrew.org/wiki/Wii_Disc - what does this mean?
			// "though most docs claim it to be 0x400 the Wii only reads 0x44 which will be padded by the DI driver to 0x60"
			mvarGameTitle = reader.ReadFixedLengthString(64).TrimNull();

			mvarDisableHashVerification = reader.ReadBoolean();
			mvarDisableDiscEncryption = reader.ReadBoolean();

			// from emutalk.net
			reader.Seek(0x0420, SeekOrigin.Begin);

			int offsetToMainExecutableDOL = reader.ReadInt32();				// offset of main executable DOL
			m_OffsetToFST = reader.ReadInt32();							// offset of the FST

			// Apploader at offset 0x00002440
			reader.Seek(0x00002440, SeekOrigin.Begin);

			string apploaderDate = reader.ReadFixedLengthString(16).TrimNull();
			int apploaderEntryPoint = reader.ReadInt32();
			int apploaderCodeSize = reader.ReadInt32();
			int unknown1 = reader.ReadInt32();
			int unknown2 = reader.ReadInt32();
			byte[] apploaderCode = reader.ReadBytes(apploaderCodeSize);

			// Main executable DOL
			reader.Seek(offsetToMainExecutableDOL, SeekOrigin.Begin);

			int[] textFileOffsets = reader.ReadInt32Array(7);
			int[] dataFileOffsets = reader.ReadInt32Array(7);
			int[] textMemoryOffsets = reader.ReadInt32Array(7);
			int[] dataMemoryOffsets = reader.ReadInt32Array(7);
			int[] textLengths = reader.ReadInt32Array(7);
			int[] dataLengths = reader.ReadInt32Array(7);
			int bssMemoryAddress = reader.ReadInt32();
			int bssLength = reader.ReadInt32();
			int entryPoint = reader.ReadInt32();

			reader.Seek(m_OffsetToFST, SeekOrigin.Begin);
			int unknownA1 = reader.ReadInt32();
			int unknownA2 = reader.ReadInt32();

			int fileCount = reader.ReadInt32();

			m_NameTableOffset = (fileCount * 12);

			reader.Accessor.SavePosition();

			reader.Seek(m_OffsetToFST + m_NameTableOffset, SeekOrigin.Begin);
			m_NameTableEntries.Clear();
			for (int i = 0; i < fileCount; i++)
			{
				int pos = (int)(reader.Accessor.Position - m_NameTableOffset - m_OffsetToFST);
				string value = reader.ReadNullTerminatedString();
				m_NameTableEntries.Add(pos, value);
			}
			reader.Accessor.LoadPosition();

			// this doesn't seem right... 16777216???
			do
			{
				int filesRead = LoadFileSystemObject(reader, fsom);
				fileCount -= filesRead;
			}
			while (fileCount > 1);
		}

		private Dictionary<int, string> m_NameTableEntries = new Dictionary<int, string>();
		private int m_OffsetToFST = 0;
		private int m_NameTableOffset = 0;

		private int LoadFileSystemObject(Reader reader, IFileSystemContainer parent, int parentOffset = 0, int nextOffset = 0)
		{
			int relativeNameOffsetAndFlag = reader.ReadInt32();
			int relativeNameOffset = relativeNameOffsetAndFlag;

			bool isDirectory = false;
			if ((relativeNameOffsetAndFlag & 0x01000000) == 0x01000000)
			{
				relativeNameOffset = (int)(relativeNameOffsetAndFlag & ~0x01000000);
				isDirectory = true;
			}

			string fileName = m_NameTableEntries[relativeNameOffset];
			int diskAddress = reader.ReadInt32();						// file_offset or parent_offset (dir)
			int fileSize = reader.ReadInt32();							// file_length or num_entries (root) or next_offset (dir)

			if (isDirectory)
			{
				int filesRead = 0;
				Folder folder = parent.Folders.Add(fileName);
				for (int i = 0; i < fileSize; i++)
				{
					filesRead += LoadFileSystemObject(reader, folder, diskAddress, fileSize);
				}
				return filesRead + 1;
			}
			else
			{
				File file = parent.Files.Add(fileName);
				file.Size = fileSize;
				file.Source = new EmbeddedFileSource(reader, diskAddress, fileSize);
				return 1;
			}
			return 0;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
