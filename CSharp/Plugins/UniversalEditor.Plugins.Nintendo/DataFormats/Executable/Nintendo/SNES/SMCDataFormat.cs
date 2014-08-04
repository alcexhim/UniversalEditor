using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	/// <summary>
	/// The Nintendo SNES SMC ROM data format.
	/// </summary>
	public class SMCDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);

				_dfr.ExportOptions.Add(new CustomOptionChoice("Region", "&Region:", false, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice(SMCRegions.Japan),
					new CustomOptionFieldChoice(SMCRegions.NorthAmerica),
					new CustomOptionFieldChoice(SMCRegions.Eurasia),
					new CustomOptionFieldChoice(SMCRegions.Sweden),
					new CustomOptionFieldChoice(SMCRegions.Finland),
					new CustomOptionFieldChoice(SMCRegions.Denmark),
					new CustomOptionFieldChoice(SMCRegions.France),
					new CustomOptionFieldChoice(SMCRegions.Holland),
					new CustomOptionFieldChoice(SMCRegions.Spain),
					new CustomOptionFieldChoice(SMCRegions.GermanyAustriaSwitzerland),
					new CustomOptionFieldChoice(SMCRegions.Italy),
					new CustomOptionFieldChoice(SMCRegions.HongKongAndChina),
					new CustomOptionFieldChoice(SMCRegions.Indonesia),
					new CustomOptionFieldChoice(SMCRegions.SouthKorea)
				}));

				_dfr.ExportOptions.Add(new CustomOptionChoice("Licensee", "&Licensee:", false, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice(SMCLicensees.None, true),
					new CustomOptionFieldChoice(SMCLicensees.Nintendo0x01),
					new CustomOptionFieldChoice(SMCLicensees.Ajinomoto),
					new CustomOptionFieldChoice(SMCLicensees.ImagineerZoom),
					new CustomOptionFieldChoice(SMCLicensees.ChrisGrayEnterprises),
					new CustomOptionFieldChoice(SMCLicensees.Zamuse),
					new CustomOptionFieldChoice(SMCLicensees.Falcom),
					new CustomOptionFieldChoice(SMCLicensees.Capcom),
					new CustomOptionFieldChoice(SMCLicensees.HotB),
					new CustomOptionFieldChoice(SMCLicensees.Jaleco0x0A),
					new CustomOptionFieldChoice(SMCLicensees.Coconuts),
					new CustomOptionFieldChoice(SMCLicensees.RageSoftware),
					new CustomOptionFieldChoice(SMCLicensees.Micronet),
					new CustomOptionFieldChoice(SMCLicensees.Technos),
					new CustomOptionFieldChoice(SMCLicensees.MebioSoftware),
					new CustomOptionFieldChoice(SMCLicensees.SHOUEiSystem),
					new CustomOptionFieldChoice(SMCLicensees.Starfish),
					new CustomOptionFieldChoice(SMCLicensees.GremlinGraphics0x12),
					new CustomOptionFieldChoice(SMCLicensees.ElectronicArts0x13),
					new CustomOptionFieldChoice(SMCLicensees.NCSMasaya),
					new CustomOptionFieldChoice(SMCLicensees.COBRATeam),
					new CustomOptionFieldChoice(SMCLicensees.HumanField),
					new CustomOptionFieldChoice(SMCLicensees.Koei0x17),
					new CustomOptionFieldChoice(SMCLicensees.HudsonSoft),
					new CustomOptionFieldChoice(SMCLicensees.GameVillage),
					new CustomOptionFieldChoice(SMCLicensees.Yanoman),
					new CustomOptionFieldChoice(SMCLicensees.Tecmo),
					new CustomOptionFieldChoice(SMCLicensees.OpenSystem),
					new CustomOptionFieldChoice(SMCLicensees.VirginGames),
					new CustomOptionFieldChoice(SMCLicensees.KSS),
					new CustomOptionFieldChoice(SMCLicensees.Sunsoft0x21),
					new CustomOptionFieldChoice(SMCLicensees.POW),
					new CustomOptionFieldChoice(SMCLicensees.MicroWorld),
					new CustomOptionFieldChoice(SMCLicensees.Enix),
					new CustomOptionFieldChoice(SMCLicensees.LoricielElectroBrain),
					new CustomOptionFieldChoice(SMCLicensees.Kemco0x28),
					new CustomOptionFieldChoice(SMCLicensees.SetaCoLtd),
					new CustomOptionFieldChoice(SMCLicensees.CultureBrain0x2A),
					new CustomOptionFieldChoice(SMCLicensees.IremJapan),
					new CustomOptionFieldChoice(SMCLicensees.PalSoft),
					new CustomOptionFieldChoice(SMCLicensees.VisitCoLtd),
					new CustomOptionFieldChoice(SMCLicensees.INTEC),
					new CustomOptionFieldChoice(SMCLicensees.SystemSacomCorp),
					new CustomOptionFieldChoice(SMCLicensees.ViacomNewMedia),
					new CustomOptionFieldChoice(SMCLicensees.Carrozzeria),
					new CustomOptionFieldChoice(SMCLicensees.Dynamic),
					new CustomOptionFieldChoice(SMCLicensees.Nintendo0x33),
					new CustomOptionFieldChoice(SMCLicensees.Magifact),
					new CustomOptionFieldChoice(SMCLicensees.Hect),
					new CustomOptionFieldChoice(SMCLicensees.CapcomEurope),
					new CustomOptionFieldChoice(SMCLicensees.AccoladeEurope),
					new CustomOptionFieldChoice(SMCLicensees.ArcadeZone),
					new CustomOptionFieldChoice(SMCLicensees.EmpireSoftware),
					new CustomOptionFieldChoice(SMCLicensees.Loriciel),
					new CustomOptionFieldChoice(SMCLicensees.GremlinGraphics0x3E),
					new CustomOptionFieldChoice(SMCLicensees.SeikaCorp),
					new CustomOptionFieldChoice(SMCLicensees.UBISoft),
					new CustomOptionFieldChoice(SMCLicensees.LifeFitnessExertainment),
					new CustomOptionFieldChoice(SMCLicensees.System3),
					new CustomOptionFieldChoice(SMCLicensees.SpectrumHolobyte),
					new CustomOptionFieldChoice(SMCLicensees.Irem),
					new CustomOptionFieldChoice(SMCLicensees.RayaSystemsSculpturedSoftware),
					new CustomOptionFieldChoice(SMCLicensees.RenovationProducts),
					new CustomOptionFieldChoice(SMCLicensees.MalibuGamesBlackPearl),
					new CustomOptionFieldChoice(SMCLicensees.USGold),
					new CustomOptionFieldChoice(SMCLicensees.AbsoluteEntertainment),
					new CustomOptionFieldChoice(SMCLicensees.Acclaim0x51),
					new CustomOptionFieldChoice(SMCLicensees.Activision),
					new CustomOptionFieldChoice(SMCLicensees.AmericanSammy),
					new CustomOptionFieldChoice(SMCLicensees.GameTek),
					new CustomOptionFieldChoice(SMCLicensees.HiTechExpressions),
					new CustomOptionFieldChoice(SMCLicensees.LJNToys),
					new CustomOptionFieldChoice(SMCLicensees.Mindscape),
					new CustomOptionFieldChoice(SMCLicensees.RomstarInc),
					new CustomOptionFieldChoice(SMCLicensees.Tradewest),
					new CustomOptionFieldChoice(SMCLicensees.AmericanSoftworksCorp),
					new CustomOptionFieldChoice(SMCLicensees.Titus),
					new CustomOptionFieldChoice(SMCLicensees.VirginInteractiveEntertainment),
					new CustomOptionFieldChoice(SMCLicensees.Maxis),
					new CustomOptionFieldChoice(SMCLicensees.OriginFCIPonyCanyon),
					new CustomOptionFieldChoice(SMCLicensees.Ocean),
					new CustomOptionFieldChoice(SMCLicensees.ElectronicArts0x69),
					new CustomOptionFieldChoice(SMCLicensees.LaserBeam),
					new CustomOptionFieldChoice(SMCLicensees.Elite),
					new CustomOptionFieldChoice(SMCLicensees.ElectroBrain),
					new CustomOptionFieldChoice(SMCLicensees.Infogrames),
					new CustomOptionFieldChoice(SMCLicensees.Interplay),
					new CustomOptionFieldChoice(SMCLicensees.LucasArts),
					new CustomOptionFieldChoice(SMCLicensees.ParkerBrothers),
					new CustomOptionFieldChoice(SMCLicensees.Konami0x74),
					new CustomOptionFieldChoice(SMCLicensees.STORM),
					new CustomOptionFieldChoice(SMCLicensees.THQSoftware),
					new CustomOptionFieldChoice(SMCLicensees.AccoladeInc),
					new CustomOptionFieldChoice(SMCLicensees.TriffixEntertainment),
					new CustomOptionFieldChoice(SMCLicensees.Microprose),
					new CustomOptionFieldChoice(SMCLicensees.Kemco0x7F),
					new CustomOptionFieldChoice(SMCLicensees.Misawa),
					new CustomOptionFieldChoice(SMCLicensees.Teichio),
					new CustomOptionFieldChoice(SMCLicensees.NamcoLtd0x82),
					new CustomOptionFieldChoice(SMCLicensees.Lozc),
					new CustomOptionFieldChoice(SMCLicensees.Koei0x84),
					new CustomOptionFieldChoice(SMCLicensees.TokumaShotenIntermedia),
					new CustomOptionFieldChoice(SMCLicensees.TsukudaOriginal),
					new CustomOptionFieldChoice(SMCLicensees.DATAMPolystar),
					new CustomOptionFieldChoice(SMCLicensees.BulletProofSoftware),
					new CustomOptionFieldChoice(SMCLicensees.VicTokai),
					new CustomOptionFieldChoice(SMCLicensees.CharacterSoft),
					new CustomOptionFieldChoice(SMCLicensees.IMax),
					new CustomOptionFieldChoice(SMCLicensees.Takara0x90),
					new CustomOptionFieldChoice(SMCLicensees.CHUNSoft),
					new CustomOptionFieldChoice(SMCLicensees.VideoSystemCoLtd),
					new CustomOptionFieldChoice(SMCLicensees.BEC),
					new CustomOptionFieldChoice(SMCLicensees.Varie),
					new CustomOptionFieldChoice(SMCLicensees.YonezawaSPalCorp),
					new CustomOptionFieldChoice(SMCLicensees.Kaneco),
					new CustomOptionFieldChoice(SMCLicensees.PackInVideo),
					new CustomOptionFieldChoice(SMCLicensees.Nichibutsu),
					new CustomOptionFieldChoice(SMCLicensees.TECMO),
					new CustomOptionFieldChoice(SMCLicensees.ImagineerCo),
					new CustomOptionFieldChoice(SMCLicensees.Telenet),
					new CustomOptionFieldChoice(SMCLicensees.Hori),
					new CustomOptionFieldChoice(SMCLicensees.Konami0xA4),
					new CustomOptionFieldChoice(SMCLicensees.KAmusementLeasingCo),
					new CustomOptionFieldChoice(SMCLicensees.Takara0xA7),
					new CustomOptionFieldChoice(SMCLicensees.TechnosJap),
					new CustomOptionFieldChoice(SMCLicensees.JVC),
					new CustomOptionFieldChoice(SMCLicensees.ToeiAnimation),
					new CustomOptionFieldChoice(SMCLicensees.Toho),
					new CustomOptionFieldChoice(SMCLicensees.NamcoLtd0xAF),
					new CustomOptionFieldChoice(SMCLicensees.MediaRingsCorp),
					new CustomOptionFieldChoice(SMCLicensees.ASCIICoActivision),
					new CustomOptionFieldChoice(SMCLicensees.Bandai),
					new CustomOptionFieldChoice(SMCLicensees.EnixAmerica),
					new CustomOptionFieldChoice(SMCLicensees.Halken),
					new CustomOptionFieldChoice(SMCLicensees.CultureBrain0xBA),
					new CustomOptionFieldChoice(SMCLicensees.Sunsoft0xBB),
					new CustomOptionFieldChoice(SMCLicensees.ToshibaEMI),
					new CustomOptionFieldChoice(SMCLicensees.SonyImagesoft),
					new CustomOptionFieldChoice(SMCLicensees.Sammy),
					new CustomOptionFieldChoice(SMCLicensees.Taito),
					new CustomOptionFieldChoice(SMCLicensees.Kemco0xC2),
					new CustomOptionFieldChoice(SMCLicensees.Square),
					new CustomOptionFieldChoice(SMCLicensees.TokumaSoft),
					new CustomOptionFieldChoice(SMCLicensees.DataEast),
					new CustomOptionFieldChoice(SMCLicensees.TonkinHouse),
					new CustomOptionFieldChoice(SMCLicensees.Koei0xC8),
					new CustomOptionFieldChoice(SMCLicensees.KonamiUSA),
					new CustomOptionFieldChoice(SMCLicensees.NTVIC),
					new CustomOptionFieldChoice(SMCLicensees.Meldac),
					new CustomOptionFieldChoice(SMCLicensees.PonyCanyon),
					new CustomOptionFieldChoice(SMCLicensees.SotsuAgencySunrise),
					new CustomOptionFieldChoice(SMCLicensees.DiscoTaito),
					new CustomOptionFieldChoice(SMCLicensees.Sofel),
					new CustomOptionFieldChoice(SMCLicensees.QuestCorp),
					new CustomOptionFieldChoice(SMCLicensees.Sigma),
					new CustomOptionFieldChoice(SMCLicensees.AskKodanshaCoLtd),
					new CustomOptionFieldChoice(SMCLicensees.Naxat),
					new CustomOptionFieldChoice(SMCLicensees.CapcomCoLtd),
					new CustomOptionFieldChoice(SMCLicensees.Banpresto),
					new CustomOptionFieldChoice(SMCLicensees.Tomy),
					new CustomOptionFieldChoice(SMCLicensees.Acclaim0xDB),
					new CustomOptionFieldChoice(SMCLicensees.NCS),
					new CustomOptionFieldChoice(SMCLicensees.HumanEntertainment),
					new CustomOptionFieldChoice(SMCLicensees.Altron),
					new CustomOptionFieldChoice(SMCLicensees.Jaleco0xE0),
					new CustomOptionFieldChoice(SMCLicensees.Yutaka),
					new CustomOptionFieldChoice(SMCLicensees.TAndESoft),
					new CustomOptionFieldChoice(SMCLicensees.EPOCHCoLtd),
					new CustomOptionFieldChoice(SMCLicensees.Athena),
					new CustomOptionFieldChoice(SMCLicensees.Asmik),
					new CustomOptionFieldChoice(SMCLicensees.Natsume),
					new CustomOptionFieldChoice(SMCLicensees.KingRecords),
					new CustomOptionFieldChoice(SMCLicensees.Atlus),
					new CustomOptionFieldChoice(SMCLicensees.SonyMusicEntertainment),
					new CustomOptionFieldChoice(SMCLicensees.IGS),
					new CustomOptionFieldChoice(SMCLicensees.MotownSoftware),
					new CustomOptionFieldChoice(SMCLicensees.LeftFieldEntertainment),
					new CustomOptionFieldChoice(SMCLicensees.BeamSoftware),
					new CustomOptionFieldChoice(SMCLicensees.TecMagik),
					new CustomOptionFieldChoice(SMCLicensees.Cybersoft),
					new CustomOptionFieldChoice(SMCLicensees.Psygnosis),
					new CustomOptionFieldChoice(SMCLicensees.Davidson),
				}));

				_dfr.Filters.Add("Nintendo SNES executable", new string[] { "*.smc", "*.sfc", "*.swc", "*.fig", "*.ufo", "*.?gm" });
				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SNES_ROM_layout");
				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SNES_header");
				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SMC_header");
			}
			return _dfr;
		}

		private SMCExtendedHeader mvarExtendedHeader = new SMCExtendedHeader();
		public SMCExtendedHeader ExtendedHeader { get { return mvarExtendedHeader; } }

		private SMCRegion mvarRegion = SMCRegions.Japan;
		/// <summary>
		/// The region in which the game is licensed.
		/// </summary>
		public SMCRegion Region { get { return mvarRegion; } set { mvarRegion = value; } }

		private SMCLicensee mvarLicensee = SMCLicensees.None;
		/// <summary>
		/// The licensed organization that developed the game.
		/// </summary>
		public SMCLicensee Licensee { get { return mvarLicensee; } set { mvarLicensee = value; } }

		/// <summary>
		/// Loads game data into the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> into which to load the data.</param>
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
			mvarRegion = SMCRegion.FromCode(countryCode);

			// Licensee code. If this value is $33, then the ROM has an extended header with ID at
			// $ffb2..$ffb5. 
			byte licenseeCode = reader.ReadByte();
			mvarLicensee = SMCLicensee.FromCode(licenseeCode);

			if (licenseeCode == 0x33)
			{

			}

			byte versionNumber = reader.ReadByte();

			byte checksumComplement = reader.ReadByte();
			byte checksum = reader.ReadByte();

			int checksumCheck = ((int)checksum + (int)checksumComplement);
			if (checksumCheck != 0xFFFF) throw new InvalidDataFormatException("Invalid checksum/checksum complement pair");

			int unknown2 = reader.ReadInt32();

			short nativeInterruptVectorCOP = reader.ReadInt16();
			short nativeInterruptVectorBRK = reader.ReadInt16();
			short nativeInterruptVectorABORT = reader.ReadInt16();
			short nativeInterruptVectorNMI = reader.ReadInt16(); // vertical blank
			short nativeInterruptVectorUnused = reader.ReadInt16();
			short nativeInterruptVectorIRQ = reader.ReadInt16();

			int unknown3 = reader.ReadInt32();

			short emulationInterruptVectorCOP = reader.ReadInt16();
			short emulationInterruptVectorUnused = reader.ReadInt16();
			short emulationInterruptVectorABORT = reader.ReadInt16();
			short emulationInterruptVectorNMI = reader.ReadInt16(); // vertical blank
			short emulationInterruptVectorRESET = reader.ReadInt16();
			short emulationInterruptVectorIRQorBRK = reader.ReadInt16();
			#endregion
		}

		/// <summary>
		/// Saves game data from the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> from which to save the data.</param>
		protected override void SaveInternal(ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (exe == null) throw new ObjectModelNotSupportedException();

			throw new NotImplementedException();
		}
	}
}
