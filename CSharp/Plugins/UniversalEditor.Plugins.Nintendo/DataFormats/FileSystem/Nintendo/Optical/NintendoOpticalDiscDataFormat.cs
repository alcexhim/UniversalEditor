using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Nintendo.Optical
{
	public class NintendoOpticalDiscDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice("FormatCode", "&Format code:", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.Revolution),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.Wii, true),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.GameCube),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.Utility),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.GameCubeDemo),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.GameCubePromotional),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.Diagnostic),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.Diagnostic1),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.WiiBackup),
					new CustomOptionFieldChoice(NintendoOpticalDiscFormatCodes.WiiFitChanInstaller)
				}));
				_dfr.ExportOptions.Add(new CustomOptionText("GameCode", "&Game code:", String.Empty, 2));
				_dfr.ExportOptions.Add(new CustomOptionChoice("RegionCode", "&Region code:", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.German),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.UnitedStates, true),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.France),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.Italy),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.Japan),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.Korea),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.PAL),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.Russia),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.Spanish),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.Taiwan),
					new CustomOptionFieldChoice(NintendoOpticalDiscRegionCodes.Australia),
				}));

				_dfr.ExportOptions.Add(new CustomOptionChoice("SystemType", "&System type:", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("GameCube", NintendoOpticalDiscSystemType.GameCube),
					new CustomOptionFieldChoice("Wii", NintendoOpticalDiscSystemType.Wii, true)
				}));

				_dfr.ExportOptions.Add(new CustomOptionText("GameTitle", "Game &title:", String.Empty, 64));
				
				_dfr.Sources.Add("http://wiibrew.org/wiki/Wii_Disc");
				_dfr.Sources.Add("http://www.emutalk.net/threads/21512-GCM-file-extension!/page3");
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

		private int LoadFileSystemObject(Reader reader, IFileSystemContainer parent)
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
			int diskAddress = reader.ReadInt32();
			int fileSize = reader.ReadInt32();

			if (isDirectory)
			{
				int filesRead = 0;
				Folder folder = parent.Folders.Add(fileName);
				for (int i = 0; i < fileSize; i++)
				{
					filesRead += LoadFileSystemObject(reader, folder);
				}
				return filesRead + 1;
			}
			else
			{
				File file = parent.Files.Add(fileName);
				file.Size = fileSize;
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
