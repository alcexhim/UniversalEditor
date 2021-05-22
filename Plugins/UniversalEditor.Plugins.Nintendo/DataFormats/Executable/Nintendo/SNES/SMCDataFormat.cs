//
//  SMCDataFormat.cs - provides a DataFormat to manipulate Nintendo SNES game dump files in SMC format
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
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Nintendo SNES game dump files in SMC format.
	/// </summary>
	public class SMCDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);

				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(GameName), "Game _name", String.Empty, 21));
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(CartridgeType), "Cartridge _type", SMCCartridgeTypes.ROMOnly, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.ROMOnly),
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.ROMAndRAM),
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.ROMAndRAMWithBattery),

					// Values greater than $02 indicate special add-on hardware in the cartridge. The caveat is that emulators like
					// Snes9x may ignore these values unless the add-on (at $ffd6) is compatible with the ROM layout (at $ffd5).
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.SuperFXNoBattery0x13),
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.SuperFXNoBattery0x14),
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.SuperFXWithBattery0x15),
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.SuperFXWithBattery0x1A),
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.SA10x34),
					new ChoiceSetting.ChoiceSettingValue(SMCCartridgeTypes.SA10x35)
				}));

				ChoiceSetting.ChoiceSettingValue[] _smcMemorySizes = new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K2),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K4),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K8),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K16),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K32),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K64),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K128),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K256),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.K512),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.M1),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.M2),
					new ChoiceSetting.ChoiceSettingValue(SMCMemorySizes.M4),
				};

				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(ROMSize), "RO_M size", SMCMemorySizes.K256, _smcMemorySizes));
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(RAMSize), "R_AM size", SMCMemorySizes.K256, _smcMemorySizes));

				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(Region), "_Region", false, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Japan),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.NorthAmerica),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Eurasia),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Sweden),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Finland),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Denmark),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.France),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Holland),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Spain),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.GermanyAustriaSwitzerland),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Italy),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.HongKongAndChina),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.Indonesia),
					new ChoiceSetting.ChoiceSettingValue(SMCRegions.SouthKorea)
				}));

				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(Licensee), "_Licensee", SMCLicensees.None, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.None),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Nintendo0x01),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Ajinomoto),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ImagineerZoom),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ChrisGrayEnterprises),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Zamuse),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Falcom),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Capcom),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.HotB),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Jaleco0x0A),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Coconuts),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.RageSoftware),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Micronet),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Technos),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.MebioSoftware),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.SHOUEiSystem),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Starfish),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.GremlinGraphics0x12),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ElectronicArts0x13),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.NCSMasaya),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.COBRATeam),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.HumanField),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Koei0x17),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.HudsonSoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.GameVillage),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Yanoman),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Tecmo),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.OpenSystem),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.VirginGames),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.KSS),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Sunsoft0x21),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.POW),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.MicroWorld),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Enix),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.LoricielElectroBrain),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Kemco0x28),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.SetaCoLtd),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.CultureBrain0x2A),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.IremJapan),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.PalSoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.VisitCoLtd),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.INTEC),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.SystemSacomCorp),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ViacomNewMedia),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Carrozzeria),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Dynamic),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Nintendo0x33),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Magifact),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Hect),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.CapcomEurope),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.AccoladeEurope),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ArcadeZone),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.EmpireSoftware),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Loriciel),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.GremlinGraphics0x3E),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.SeikaCorp),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.UBISoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.LifeFitnessExertainment),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.System3),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.SpectrumHolobyte),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Irem),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.RayaSystemsSculpturedSoftware),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.RenovationProducts),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.MalibuGamesBlackPearl),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.USGold),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.AbsoluteEntertainment),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Acclaim0x51),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Activision),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.AmericanSammy),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.GameTek),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.HiTechExpressions),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.LJNToys),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Mindscape),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.RomstarInc),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Tradewest),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.AmericanSoftworksCorp),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Titus),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.VirginInteractiveEntertainment),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Maxis),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.OriginFCIPonyCanyon),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Ocean),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ElectronicArts0x69),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.LaserBeam),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Elite),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ElectroBrain),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Infogrames),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Interplay),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.LucasArts),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ParkerBrothers),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Konami0x74),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.STORM),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.THQSoftware),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.AccoladeInc),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TriffixEntertainment),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Microprose),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Kemco0x7F),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Misawa),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Teichio),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.NamcoLtd0x82),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Lozc),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Koei0x84),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TokumaShotenIntermedia),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TsukudaOriginal),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.DATAMPolystar),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.BulletProofSoftware),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.VicTokai),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.CharacterSoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.IMax),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Takara0x90),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.CHUNSoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.VideoSystemCoLtd),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.BEC),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Varie),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.YonezawaSPalCorp),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Kaneco),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.PackInVideo),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Nichibutsu),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TECMO),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ImagineerCo),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Telenet),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Hori),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Konami0xA4),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.KAmusementLeasingCo),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Takara0xA7),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TechnosJap),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.JVC),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ToeiAnimation),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Toho),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.NamcoLtd0xAF),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.MediaRingsCorp),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ASCIICoActivision),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Bandai),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.EnixAmerica),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Halken),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.CultureBrain0xBA),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Sunsoft0xBB),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.ToshibaEMI),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.SonyImagesoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Sammy),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Taito),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Kemco0xC2),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Square),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TokumaSoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.DataEast),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TonkinHouse),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Koei0xC8),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.KonamiUSA),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.NTVIC),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Meldac),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.PonyCanyon),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.SotsuAgencySunrise),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.DiscoTaito),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Sofel),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.QuestCorp),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Sigma),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.AskKodanshaCoLtd),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Naxat),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.CapcomCoLtd),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Banpresto),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Tomy),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Acclaim0xDB),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.NCS),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.HumanEntertainment),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Altron),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Jaleco0xE0),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Yutaka),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TAndESoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.EPOCHCoLtd),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Athena),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Asmik),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Natsume),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.KingRecords),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Atlus),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.SonyMusicEntertainment),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.IGS),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.MotownSoftware),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.LeftFieldEntertainment),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.BeamSoftware),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.TecMagik),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Cybersoft),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Psygnosis),
					new ChoiceSetting.ChoiceSettingValue(SMCLicensees.Davidson),
				}));
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new RangeSetting(nameof(VersionNumber), "_Version number", 0, Byte.MinValue, Byte.MaxValue));

				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SNES_ROM_layout");
				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SNES_header");
				_dfr.Sources.Add("http://romhack.wikia.com/wiki/SMC_header");
			}
			return _dfr;
		}

		private SMCExtendedHeader mvarExtendedHeader = new SMCExtendedHeader();
		public SMCExtendedHeader ExtendedHeader { get { return mvarExtendedHeader; } }

		private string mvarGameName = String.Empty;
		/// <summary>
		/// Name of the ROM, typically in ASCII, using spaces to pad the name to 21 bytes.
		/// </summary>
		public string GameName { get { return mvarGameName; } set { mvarGameName = value; } }

		private SMCLayout mvarROMLayout = SMCLayout.LoROM;
		public SMCLayout ROMLayout { get { return mvarROMLayout; } set { mvarROMLayout = value; } }

		private SMCCartridgeType mvarCartridgeType = SMCCartridgeTypes.ROMOnly;
		/// <summary>
		/// indicates the hardware in the cartridge. Emulators use this byte to decide which hardware to emulate. A real SNES ignores this byte and uses the real hardware in the real cartridge.
		/// </summary>
		public SMCCartridgeType CartridgeType { get { return mvarCartridgeType; } set { mvarCartridgeType = value; } }

		private SMCMemorySize mvarROMSize = SMCMemorySizes.K256;
		/// <summary>
		/// Indicates the amount of ROM in the cartridge.
		/// </summary>
		public SMCMemorySize ROMSize { get { return mvarROMSize; } set { mvarROMSize = value; } }

		private SMCMemorySize mvarRAMSize = SMCMemorySizes.K256;
		/// <summary>
		/// Indicates the amount of RAM in the cartridge (excluding the RAM in the SNES system).
		/// </summary>
		public SMCMemorySize RAMSize { get { return mvarRAMSize; } set { mvarRAMSize = value; } }

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

		private byte mvarVersionNumber = 0;
		/// <summary>
		/// Typically contains 0x00. Most ROM hackers never touch this byte, so multiple versions of a ROM
		/// hack may share the same value of this byte. A few ROM hackers actually set this byte.
		/// </summary>
		public byte VersionNumber { get { return mvarVersionNumber; } set { mvarVersionNumber = value; } }

		/// <summary>
		/// Loads game data into the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> into which to load the data.</param>
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (exe == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			#region Extended Header
			{
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
					SMCExtendedHeaderFlags flags = (SMCExtendedHeaderFlags)reader.ReadByte();
					mvarExtendedHeader.SplitFile = ((flags & SMCExtendedHeaderFlags.SplitFile) == SMCExtendedHeaderFlags.SplitFile);
					mvarExtendedHeader.HiRomEnabled = ((flags & SMCExtendedHeaderFlags.HiRomEnabled) == SMCExtendedHeaderFlags.HiRomEnabled);

					if ((flags & SMCExtendedHeaderFlags.SaveRam8K) == SMCExtendedHeaderFlags.SaveRam8K)
					{
						mvarExtendedHeader.SaveRAMSize = SMCSaveRAMSize.SaveRAM8K;
					}
					else if ((flags & SMCExtendedHeaderFlags.SaveRam2K) == SMCExtendedHeaderFlags.SaveRam2K)
					{
						mvarExtendedHeader.SaveRAMSize = SMCSaveRAMSize.SaveRAM2K;
					}
					else if ((flags & SMCExtendedHeaderFlags.SaveRamNone) == SMCExtendedHeaderFlags.SaveRamNone)
					{
						mvarExtendedHeader.SaveRAMSize = SMCSaveRAMSize.SaveRAMNone;
					}

					if ((flags & SMCExtendedHeaderFlags.ResetVectorAddressOverride) == SMCExtendedHeaderFlags.ResetVectorAddressOverride)
					{
						mvarExtendedHeader.ResetVectorOverride = 0x8000;
					}
					#endregion
					#region 03-04 HiRom/LoRom (Pro Fighter specific)
					byte hiRomLoRom = reader.ReadByte();
					if ((flags & SMCExtendedHeaderFlags.HiRomEnabled) != SMCExtendedHeaderFlags.HiRomEnabled)
					{
						// only set HiRom/LoRom from this field if not set in a flag
						mvarExtendedHeader.HiRomEnabled = ((hiRomLoRom & 0x80) == 0x80);
					}
					if (mvarExtendedHeader.HiRomEnabled)
					{
						mvarROMLayout = SMCLayout.HiROM;
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
			}
			#endregion

			#region SNES header
			mvarGameName = reader.ReadFixedLengthString(21).Trim();

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
			mvarCartridgeType = SMCCartridgeType.FromCode(cartridgeType);

			byte romsize = reader.ReadByte();
			mvarROMSize = SMCMemorySize.FromCode(romsize);

			byte ramsize = reader.ReadByte();
			mvarRAMSize = SMCMemorySize.FromCode(ramsize);

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

			mvarVersionNumber = reader.ReadByte();

			ushort checksumComplement = reader.ReadUInt16();
			ushort checksum = reader.ReadUInt16();

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

			Writer writer = base.Accessor.Writer;

			#region SNES header
			writer.WriteFixedLengthString(mvarGameName, 21, ' ');

			switch (mvarROMLayout)
			{
				case SMCLayout.LoROM:
				{
					writer.WriteByte(0x20);
					break;
				}
				case SMCLayout.HiROM:
				{
					writer.WriteByte(0x21);
					break;
				}
			}

			writer.WriteByte((byte)(mvarCartridgeType != null ? mvarCartridgeType.Value : 0));
			writer.WriteByte((byte)(mvarROMSize != null ? mvarROMSize.Value : 0));
			writer.WriteByte((byte)(mvarRAMSize != null ? mvarRAMSize.Value : 0));
			writer.WriteByte((byte)(mvarRegion != null ? mvarRegion.Value : 0));
			writer.WriteByte((byte)(mvarLicensee != null ? mvarLicensee.Value : 0));

			if (mvarLicensee != null && mvarLicensee.Value == 0x33)
			{

			}

			writer.WriteByte(mvarVersionNumber);

			ushort checksumComplement = 0;
			ushort checksum = 0;
			writer.WriteUInt16(checksumComplement);
			writer.WriteUInt16(checksum);

			int unknown2 = 0;
			writer.WriteInt32(unknown2);

			short nativeInterruptVectorCOP = 0;
			short nativeInterruptVectorBRK = 0;
			short nativeInterruptVectorABORT = 0;
			short nativeInterruptVectorNMI = 0; // vertical blank
			short nativeInterruptVectorUnused = 0;
			short nativeInterruptVectorIRQ = 0;

			writer.WriteInt16(nativeInterruptVectorCOP);
			writer.WriteInt16(nativeInterruptVectorBRK);
			writer.WriteInt16(nativeInterruptVectorABORT);
			writer.WriteInt16(nativeInterruptVectorNMI);
			writer.WriteInt16(nativeInterruptVectorUnused);
			writer.WriteInt16(nativeInterruptVectorIRQ);

			int unknown3 = 0;
			writer.WriteInt32(unknown3);

			short emulationInterruptVectorCOP = 0;
			short emulationInterruptVectorUnused = 0;
			short emulationInterruptVectorABORT = 0;
			short emulationInterruptVectorNMI = 0; // vertical blank
			short emulationInterruptVectorRESET = 0;
			short emulationInterruptVectorIRQorBRK = 0;

			writer.WriteInt16(emulationInterruptVectorCOP);
			writer.WriteInt16(emulationInterruptVectorUnused);
			writer.WriteInt16(emulationInterruptVectorABORT);
			writer.WriteInt16(emulationInterruptVectorNMI);
			writer.WriteInt16(emulationInterruptVectorRESET);
			writer.WriteInt16(emulationInterruptVectorIRQorBRK);
			#endregion
		}
	}
}
