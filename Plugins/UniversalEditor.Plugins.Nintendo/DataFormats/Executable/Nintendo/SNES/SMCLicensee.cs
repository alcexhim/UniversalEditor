//
//  SMCLicensee.cs - provides metadata information for companies that have licensed the use of the Nintendo SNES game console for development
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
using System.Reflection;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	/// <summary>
	/// A company that has licensed the use of the Nintendo SNES game console for development.
	/// </summary>
	/// <completionlist cref="SMCLicensees" />
	public class SMCLicensee
	{
		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the company or individual holding the development license.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private byte mvarValue = 0;
		/// <summary>
		/// The single-byte code that represents this licensee in the Nintendo SNES game console.
		/// </summary>
		public byte Value { get { return mvarValue; } set { mvarValue = value; } }

		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> with the given licensee code if valid.
		/// </summary>
		/// <param name="value">The licensee code to search on.</param>
		/// <returns>If the licensee code is known, returns an instance of the associated <see cref="SMCLicensee" />. Otherwise, returns null.</returns>
		public static SMCLicensee FromCode(byte value)
		{
			Type t = typeof(SMCLicensees);
			
			MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;
			PropertyInfo[] properties = t.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.PropertyType == typeof(SMCLicensee))
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (getMethod != null && (getMethod.Attributes & methodAttributes) == methodAttributes)
					{
						object[] index = null;
						SMCLicensee val = (SMCLicensee)propertyInfo.GetValue(null, index);

						if (val.Value == value) return val;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Creates a new instance of <see cref="SMCLicensee" /> with the given title and value.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="value"></param>
		public SMCLicensee(string title, byte value)
		{
			mvarTitle = title;
			mvarValue = value;
		}

		/// <summary>
		/// Translates this <see cref="SMCLicensee" /> into a human-readable string, including the
		/// title of the region and the country code.
		/// </summary>
		/// <returns>A human-readable string representing this <see cref="SMCLicensee" />.</returns>
		public override string ToString()
		{
			return mvarTitle + " (0x" + mvarValue.ToString("X").PadLeft(2, '0') + ")";
		}
	}
	/// <summary>
	/// Licensees that have been defined by the SNES SMC data format. This class cannot be inherited.
	/// </summary>
	public sealed class SMCLicensees
	{
		private static SMCLicensee mvarNone = new SMCLicensee("(none)", 0x00);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing no currently-supported licensee.
		/// </summary>
		public static SMCLicensee None { get { return mvarNone; } }

		private static SMCLicensee mvarNintendo0x01 = new SMCLicensee("Nintendo", 0x01);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Nintendo" with a code of 0x01.
		/// </summary>
		public static SMCLicensee Nintendo0x01 { get { return mvarNintendo0x01; } }

		private static SMCLicensee mvarAjinomoto = new SMCLicensee("Ajinomoto", 0x02);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Ajinomoto".
		/// </summary>
		public static SMCLicensee Ajinomoto { get { return mvarAjinomoto; } }

		private static SMCLicensee mvarImagineerZoom = new SMCLicensee("Imagineer-Zoom", 0x03);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Imagineer-Zoom".
		/// </summary>
		public static SMCLicensee ImagineerZoom { get { return mvarImagineerZoom; } }

		private static SMCLicensee mvarChrisGrayEnterprises = new SMCLicensee("Chris Gray Enterprises Inc.", 0x04);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Chris Gray Enterprises, Inc.".
		/// </summary>
		public static SMCLicensee ChrisGrayEnterprises { get { return mvarChrisGrayEnterprises; } }

		private static SMCLicensee mvarZamuse = new SMCLicensee("Zamuse", 0x05);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Zamuse".
		/// </summary>
		public static SMCLicensee Zamuse { get { return mvarZamuse; } }

		private static SMCLicensee mvarFalcom = new SMCLicensee("Falcom", 0x06);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Falcom".
		/// </summary>
		public static SMCLicensee Falcom { get { return mvarFalcom; } }
		
		
		// UNK, 0x07

		private static SMCLicensee mvarCapcom = new SMCLicensee("Capcom", 0x08);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Capcom".
		/// </summary>
		public static SMCLicensee Capcom { get { return mvarCapcom; } }

		private static SMCLicensee mvarHotB = new SMCLicensee("HOT-B", 0x09);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "HOT-B".
		/// </summary>
		public static SMCLicensee HotB { get { return mvarHotB; } }

		private static SMCLicensee mvarJaleco0x0A = new SMCLicensee("Jaleco", 0x0A);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Jaleco" with a code of 0x0A.
		/// </summary>
		public static SMCLicensee Jaleco0x0A { get { return mvarJaleco0x0A; } }

		private static SMCLicensee mvarCoconuts = new SMCLicensee("Coconuts", 0x0B);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Coconuts".
		/// </summary>
		public static SMCLicensee Coconuts { get { return mvarCoconuts; } }

		private static SMCLicensee mvarRageSoftware = new SMCLicensee("Rage Software", 0x0C);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Rage Software".
		/// </summary>
		public static SMCLicensee RageSoftware { get { return mvarRageSoftware; } }

		private static SMCLicensee mvarMicronet = new SMCLicensee("Micronet", 0x0D);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Micronet".
		/// </summary>
		public static SMCLicensee Micronet { get { return mvarMicronet; } }

		private static SMCLicensee mvarTechnos = new SMCLicensee("Technos", 0x0E);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Technos".
		/// </summary>
		public static SMCLicensee Technos { get { return mvarTechnos; } }

		private static SMCLicensee mvarMebioSoftware = new SMCLicensee("Mebio Software", 0x0F);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Mebio Software".
		/// </summary>
		public static SMCLicensee MebioSoftware { get { return mvarMebioSoftware; } }

		private static SMCLicensee mvarSHOUEiSystem = new SMCLicensee("SHOUEi System", 0x10);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "SHOUEi System".
		/// </summary>
		public static SMCLicensee SHOUEiSystem { get { return mvarSHOUEiSystem; } }

		private static SMCLicensee mvarStarfish = new SMCLicensee("Starfish", 0x11);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Starfish".
		/// </summary>
		public static SMCLicensee Starfish { get { return mvarStarfish; } }

		private static SMCLicensee mvarGremlinGraphics0x12 = new SMCLicensee("Gremlin Graphics", 0x12);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Gremlin Graphics" with a code of 0x12.
		/// </summary>
		public static SMCLicensee GremlinGraphics0x12 { get { return mvarGremlinGraphics0x12; } }

		private static SMCLicensee mvarElectronicArts0x13 = new SMCLicensee("Electronic Arts", 0x13);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Electronic Arts" with a code of 0x13.
		/// </summary>
		public static SMCLicensee ElectronicArts0x13 { get { return mvarElectronicArts0x13; } }

		private static SMCLicensee mvarNCSMasaya = new SMCLicensee("NCS / Masaya", 0x14);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "NCS / Masaya".
		/// </summary>
		public static SMCLicensee NCSMasaya { get { return mvarNCSMasaya; } }

		private static SMCLicensee mvarCOBRATeam = new SMCLicensee("COBRA Team", 0x15);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "COBRA Team".
		/// </summary>
		public static SMCLicensee COBRATeam { get { return mvarCOBRATeam; } }

		private static SMCLicensee mvarHumanField = new SMCLicensee("Human/Field", 0x16);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Human/Field".
		/// </summary>
		public static SMCLicensee HumanField { get { return mvarHumanField; } }

		private static SMCLicensee mvarKoei0x17 = new SMCLicensee("KOEI", 0x17);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "KOEI" with a code of 0x17.
		/// </summary>
		public static SMCLicensee Koei0x17 { get { return mvarKoei0x17; } }

		private static SMCLicensee mvarHudsonSoft = new SMCLicensee("Hudson Soft", 0x18);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Hudson Soft".
		/// </summary>
		public static SMCLicensee HudsonSoft { get { return mvarHudsonSoft; } }

		private static SMCLicensee mvarGameVillage = new SMCLicensee("Game Village", 0x19);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Game Village".
		/// </summary>
		public static SMCLicensee GameVillage { get { return mvarGameVillage; } }

		private static SMCLicensee mvarYanoman = new SMCLicensee("Yanoman", 0x1A);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Yanoman".
		/// </summary>
		public static SMCLicensee Yanoman { get { return mvarYanoman; } }

		// UNK 0x1B

		private static SMCLicensee mvarTecmo = new SMCLicensee("Tecmo", 0x1C);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Tecmo".
		/// </summary>
		public static SMCLicensee Tecmo { get { return mvarTecmo; } }

		// UNK 0x1D

		private static SMCLicensee mvarOpenSystem = new SMCLicensee("Open System", 0x1E);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Open System".
		/// </summary>
		public static SMCLicensee OpenSystem { get { return mvarOpenSystem; } }

		private static SMCLicensee mvarVirginGames = new SMCLicensee("Virgin Games", 0x1F);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Virgin Games".
		/// </summary>
		public static SMCLicensee VirginGames { get { return mvarVirginGames; } }

		private static SMCLicensee mvarKSS = new SMCLicensee("KSS", 0x20);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "KSS".
		/// </summary>
		public static SMCLicensee KSS { get { return mvarKSS; } }

		private static SMCLicensee mvarSunsoft0x21 = new SMCLicensee("Sunsoft", 0x21);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Sunsoft" with a code of 0x21.
		/// </summary>
		public static SMCLicensee Sunsoft0x21 { get { return mvarSunsoft0x21; } }

		private static SMCLicensee mvarPOW = new SMCLicensee("POW", 0x22);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "POW".
		/// </summary>
		public static SMCLicensee POW { get { return mvarPOW; } }

		private static SMCLicensee mvarMicroWorld = new SMCLicensee("Micro World", 0x23);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Micro World".
		/// </summary>
		public static SMCLicensee MicroWorld { get { return mvarMicroWorld; } }

		// UNK 0x24
		// UNK 0x25

		private static SMCLicensee mvarEnix = new SMCLicensee("Enix", 0x26);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Enix".
		/// </summary>
		public static SMCLicensee Enix { get { return mvarEnix; } }

		private static SMCLicensee mvarLoricielElectroBrain = new SMCLicensee("Loriciel/Electro Brain", 0x27);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Loriciel/Electro Brain".
		/// </summary>
		public static SMCLicensee LoricielElectroBrain { get { return mvarLoricielElectroBrain; } }

		private static SMCLicensee mvarKemco0x28 = new SMCLicensee("Kemco", 0x28);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Kemco" with a code of 0x28.
		/// </summary>
		public static SMCLicensee Kemco0x28 { get { return mvarKemco0x28; } }

		private static SMCLicensee mvarSetaCoLtd = new SMCLicensee("Seta Co.,Ltd.", 0x29);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Seta Co.,Ltd.".
		/// </summary>
		public static SMCLicensee SetaCoLtd { get { return mvarSetaCoLtd; } }

		private static SMCLicensee mvarCultureBrain0x2A = new SMCLicensee("Culture Brain", 0x2A);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Culture Brain" with a code of 0x2A.
		/// </summary>
		public static SMCLicensee CultureBrain0x2A { get { return mvarCultureBrain0x2A; } }

		private static SMCLicensee mvarIremJapan = new SMCLicensee("Irem Japan", 0x2B);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Irem Japan".
		/// </summary>
		public static SMCLicensee IremJapan { get { return mvarIremJapan; } }

		private static SMCLicensee mvarPalSoft = new SMCLicensee("Pal Soft", 0x2C);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Pal Soft".
		/// </summary>
		public static SMCLicensee PalSoft { get { return mvarPalSoft; } }

		private static SMCLicensee mvarVisitCoLtd = new SMCLicensee("Visit Co.,Ltd.", 0x2D);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Visit Co.,Ltd.".
		/// </summary>
		public static SMCLicensee VisitCoLtd { get { return mvarVisitCoLtd; } }

		private static SMCLicensee mvarINTEC = new SMCLicensee("INTEC Inc.", 0x2E);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "INTEC Inc.".
		/// </summary>
		public static SMCLicensee INTEC { get { return mvarINTEC; } }

		private static SMCLicensee mvarSystemSacomCorp = new SMCLicensee("System Sacom Corp.", 0x2F);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "System Sacom Corp.".
		/// </summary>
		public static SMCLicensee SystemSacomCorp { get { return mvarSystemSacomCorp; } }

		private static SMCLicensee mvarViacomNewMedia = new SMCLicensee("Viacom New Media", 0x30);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Viacom New Media".
		/// </summary>
		public static SMCLicensee ViacomNewMedia { get { return mvarViacomNewMedia; } }

		private static SMCLicensee mvarCarrozzeria = new SMCLicensee("Carrozzeria", 0x31);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Carrozzeria".
		/// </summary>
		public static SMCLicensee Carrozzeria { get { return mvarCarrozzeria; } }

		private static SMCLicensee mvarDynamic = new SMCLicensee("Dynamic", 0x32);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Dynamic".
		/// </summary>
		public static SMCLicensee Dynamic { get { return mvarDynamic; } }

		private static SMCLicensee mvarNintendo0x33 = new SMCLicensee("Nintendo", 0x33);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Nintendo".
		/// </summary>
		public static SMCLicensee Nintendo0x33 { get { return mvarNintendo0x33; } }

		private static SMCLicensee mvarMagifact = new SMCLicensee("Magifact", 0x34);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Magifact".
		/// </summary>
		public static SMCLicensee Magifact { get { return mvarMagifact; } }

		private static SMCLicensee mvarHect = new SMCLicensee("Hect", 0x35);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Hect".
		/// </summary>
		public static SMCLicensee Hect { get { return mvarHect; } }

		// UNK 0x36
		// UNK 0x37

		private static SMCLicensee mvarCapcomEurope = new SMCLicensee("Capcom Europe", 0x38);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Capcom Europe".
		/// </summary>
		public static SMCLicensee CapcomEurope { get { return mvarCapcomEurope; } }

		private static SMCLicensee mvarAccoladeEurope = new SMCLicensee("Accolade Europe", 0x39);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Accolade Europe".
		/// </summary>
		public static SMCLicensee AccoladeEurope { get { return mvarAccoladeEurope; } }

		// UNK 0x3A

		private static SMCLicensee mvarArcadeZone = new SMCLicensee("Arcade Zone", 0x3B);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Arcade Zone".
		/// </summary>
		public static SMCLicensee ArcadeZone { get { return mvarArcadeZone; } }

		private static SMCLicensee mvarEmpireSoftware = new SMCLicensee("Empire Software", 0x3C);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Empire Software".
		/// </summary>
		public static SMCLicensee EmpireSoftware { get { return mvarEmpireSoftware; } }

		private static SMCLicensee mvarLoriciel = new SMCLicensee("Loriciel", 0x3D);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Loriciel".
		/// </summary>
		public static SMCLicensee Loriciel { get { return mvarLoriciel; } }

		private static SMCLicensee mvarGremlinGraphics0x3E = new SMCLicensee("Gremlin Graphics", 0x3E);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Gremlin Graphics" with a code of 0x3E.
		/// </summary>
		public static SMCLicensee GremlinGraphics0x3E { get { return mvarGremlinGraphics0x3E; } }

		// UNK 0x3F

		private static SMCLicensee mvarSeikaCorp = new SMCLicensee("Seika Corp.", 0x40);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Seika Corp.".
		/// </summary>
		public static SMCLicensee SeikaCorp { get { return mvarSeikaCorp; } }

		private static SMCLicensee mvarUBISoft = new SMCLicensee("UBI Soft", 0x41);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "UBI Soft".
		/// </summary>
		public static SMCLicensee UBISoft { get { return mvarUBISoft; } }

		// UNK 0x42
		// UNK 0x43

		private static SMCLicensee mvarLifeFitnessExertainment = new SMCLicensee("LifeFitness Exertainment", 0x44);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "LifeFitness Exertainment".
		/// </summary>
		public static SMCLicensee LifeFitnessExertainment { get { return mvarLifeFitnessExertainment; } }

		// UNK 0x45

		private static SMCLicensee mvarSystem3 = new SMCLicensee("System 3", 0x46);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "System 3".
		/// </summary>
		public static SMCLicensee System3 { get { return mvarSystem3; } }

		private static SMCLicensee mvarSpectrumHolobyte = new SMCLicensee("Spectrum Holobyte", 0x47);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Spectrum Holobyte".
		/// </summary>
		public static SMCLicensee SpectrumHolobyte { get { return mvarSpectrumHolobyte; } }

		// UNK 0x48

		private static SMCLicensee mvarIrem = new SMCLicensee("Irem", 0x49);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Irem".
		/// </summary>
		public static SMCLicensee Irem { get { return mvarIrem; } }

		// UNK 0x4A

		private static SMCLicensee mvarRayaSystemsSculpturedSoftware = new SMCLicensee("Raya Systems/Sculptured Software", 0x4B);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Raya Systems/Sculptured Software".
		/// </summary>
		public static SMCLicensee RayaSystemsSculpturedSoftware { get { return mvarRayaSystemsSculpturedSoftware; } }

		private static SMCLicensee mvarRenovationProducts = new SMCLicensee("Renovation Products", 0x4C);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Irem".
		/// </summary>
		public static SMCLicensee RenovationProducts { get { return mvarRenovationProducts; } }

		private static SMCLicensee mvarMalibuGamesBlackPearl = new SMCLicensee("Malibu Games/Black Pearl", 0x4D);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Malibu Games/Black Pearl".
		/// </summary>
		public static SMCLicensee MalibuGamesBlackPearl { get { return mvarMalibuGamesBlackPearl; } }

		// UNK 0x4E

		private static SMCLicensee mvarUSGold = new SMCLicensee("U.S. Gold", 0x4F);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "U.S. Gold".
		/// </summary>
		public static SMCLicensee USGold { get { return mvarUSGold; } }

		private static SMCLicensee mvarAbsoluteEntertainment = new SMCLicensee("Absolute Entertainment", 0x50);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Absolute Entertainment".
		/// </summary>
		public static SMCLicensee AbsoluteEntertainment { get { return mvarAbsoluteEntertainment; } }

		private static SMCLicensee mvarAcclaim0x51 = new SMCLicensee("Acclaim", 0x51);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Acclaim" with a code of 0x51.
		/// </summary>
		public static SMCLicensee Acclaim0x51 { get { return mvarAcclaim0x51; } }

		private static SMCLicensee mvarActivision = new SMCLicensee("Activision", 0x52);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Activision".
		/// </summary>
		public static SMCLicensee Activision { get { return mvarActivision; } }

		private static SMCLicensee mvarAmericanSammy = new SMCLicensee("American Sammy", 0x53);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "American Sammy".
		/// </summary>
		public static SMCLicensee AmericanSammy { get { return mvarAmericanSammy; } }

		private static SMCLicensee mvarGameTek = new SMCLicensee("GameTek", 0x54);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "GameTek".
		/// </summary>
		public static SMCLicensee GameTek { get { return mvarGameTek; } }

		private static SMCLicensee mvarHiTechExpressions = new SMCLicensee("Hi Tech Expressions", 0x55);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Hi Tech Expressions".
		/// </summary>
		public static SMCLicensee HiTechExpressions { get { return mvarHiTechExpressions; } }

		private static SMCLicensee mvarLJNToys = new SMCLicensee("LJN Toys", 0x56);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "LJN Toys".
		/// </summary>
		public static SMCLicensee LJNToys { get { return mvarLJNToys; } }

		// UNK 0x57
		// UNK 0x58
		// UNK 0x59

		private static SMCLicensee mvarMindscape = new SMCLicensee("Mindscape", 0x5A);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Mindscape".
		/// </summary>
		public static SMCLicensee Mindscape { get { return mvarMindscape; } }

		private static SMCLicensee mvarRomstarInc = new SMCLicensee("Romstar, Inc.", 0x5B);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Romstar, Inc.".
		/// </summary>
		public static SMCLicensee RomstarInc { get { return mvarRomstarInc; } }

		// UNK 0x5C

		private static SMCLicensee mvarTradewest = new SMCLicensee("Tradewest", 0x5D);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Tradewest".
		/// </summary>
		public static SMCLicensee Tradewest { get { return mvarTradewest; } }

		// UNK 0x5E

		private static SMCLicensee mvarAmericanSoftworksCorp = new SMCLicensee("American Softworks Corp.", 0x5F);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "American Softworks Corp.".
		/// </summary>
		public static SMCLicensee AmericanSoftworksCorp { get { return mvarAmericanSoftworksCorp; } }

		private static SMCLicensee mvarTitus = new SMCLicensee("Titus", 0x60);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Titus".
		/// </summary>
		public static SMCLicensee Titus { get { return mvarTitus; } }

		private static SMCLicensee mvarVirginInteractiveEntertainment = new SMCLicensee("Virgin Interactive Entertainment", 0x61);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Virgin Interactive Entertainment".
		/// </summary>
		public static SMCLicensee VirginInteractiveEntertainment { get { return mvarVirginInteractiveEntertainment; } }

		private static SMCLicensee mvarMaxis = new SMCLicensee("Maxis", 0x62);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Maxis".
		/// </summary>
		public static SMCLicensee Maxis { get { return mvarMaxis; } }

		private static SMCLicensee mvarOriginFCIPonyCanyon = new SMCLicensee("Origin/FCI/Pony Canyon", 0x63);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Origin/FCI/Pony Canyon".
		/// </summary>
		public static SMCLicensee OriginFCIPonyCanyon { get { return mvarOriginFCIPonyCanyon; } }

		// UNK 0x64
		// UNK 0x65
		// UNK 0x66

		private static SMCLicensee mvarOcean = new SMCLicensee("Ocean", 0x67);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Maxis".
		/// </summary>
		public static SMCLicensee Ocean { get { return mvarOcean; } }

		// UNK 0x68

		private static SMCLicensee mvarElectronicArts0x69 = new SMCLicensee("Electronic Arts", 0x69);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Electronic Arts" with a code of 0x69.
		/// </summary>
		public static SMCLicensee ElectronicArts0x69 { get { return mvarElectronicArts0x69; } }

		// UNK 0x6A

		private static SMCLicensee mvarLaserBeam = new SMCLicensee("Laser Beam", 0x6B);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Laser Beam".
		/// </summary>
		public static SMCLicensee LaserBeam { get { return mvarLaserBeam; } }

		// UNK 0x6C
		// UNK 0x6D

		private static SMCLicensee mvarElite = new SMCLicensee("Elite", 0x6E);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Elite".
		/// </summary>
		public static SMCLicensee Elite { get { return mvarElite; } }

		private static SMCLicensee mvarElectroBrain = new SMCLicensee("Electro Brain", 0x6F);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Electro Brain".
		/// </summary>
		public static SMCLicensee ElectroBrain { get { return mvarElectroBrain; } }

		private static SMCLicensee mvarInfogrames = new SMCLicensee("Infogrames", 0x70);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Infogrames".
		/// </summary>
		public static SMCLicensee Infogrames { get { return mvarInfogrames; } }

		private static SMCLicensee mvarInterplay = new SMCLicensee("Interplay", 0x71);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Interplay".
		/// </summary>
		public static SMCLicensee Interplay { get { return mvarInterplay; } }

		private static SMCLicensee mvarLucasArts = new SMCLicensee("LucasArts", 0x72);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "LucasArts".
		/// </summary>
		public static SMCLicensee LucasArts { get { return mvarLucasArts; } }

		private static SMCLicensee mvarParkerBrothers = new SMCLicensee("Parker Brothers", 0x73);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Parker Brothers".
		/// </summary>
		public static SMCLicensee ParkerBrothers { get { return mvarParkerBrothers; } }

		private static SMCLicensee mvarKonami0x74 = new SMCLicensee("Konami", 0x74);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Konami" with a code of 0x74.
		/// </summary>
		public static SMCLicensee Konami0x74 { get { return mvarKonami0x74; } }

		private static SMCLicensee mvarSTORM = new SMCLicensee("STORM", 0x75);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "STORM".
		/// </summary>
		public static SMCLicensee STORM { get { return mvarSTORM; } }

		// UNK 0x76
		// UNK 0x77

		private static SMCLicensee mvarTHQSoftware = new SMCLicensee("THQ Software", 0x78);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "THQ Software".
		/// </summary>
		public static SMCLicensee THQSoftware { get { return mvarTHQSoftware; } }

		private static SMCLicensee mvarAccoladeInc = new SMCLicensee("Accolade Inc.", 0x79);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Accolade Inc.".
		/// </summary>
		public static SMCLicensee AccoladeInc { get { return mvarAccoladeInc; } }

		private static SMCLicensee mvarTriffixEntertainment = new SMCLicensee("Triffix Entertainment", 0x7A);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Triffix Entertainment".
		/// </summary>
		public static SMCLicensee TriffixEntertainment { get { return mvarTriffixEntertainment; } }

		// UNK 0x7B

		private static SMCLicensee mvarMicroprose = new SMCLicensee("Microprose", 0x7C);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Microprose".
		/// </summary>
		public static SMCLicensee Microprose { get { return mvarMicroprose; } }

		// UNK 0x7D
		// UNK 0x7E

		private static SMCLicensee mvarKemco0x7F = new SMCLicensee("Kemco", 0x7F);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Kemco" with a code of 0x7F.
		/// </summary>
		public static SMCLicensee Kemco0x7F { get { return mvarKemco0x7F; } }

		private static SMCLicensee mvarMisawa = new SMCLicensee("Misawa", 0x80);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Misawa".
		/// </summary>
		public static SMCLicensee Misawa { get { return mvarMisawa; } }

		private static SMCLicensee mvarTeichio = new SMCLicensee("Teichio", 0x81);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Teichio".
		/// </summary>
		public static SMCLicensee Teichio { get { return mvarTeichio; } }

		private static SMCLicensee mvarNamcoLtd0x82 = new SMCLicensee("Namco Ltd.", 0x82);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Namco Ltd." with a code of 0x82.
		/// </summary>
		public static SMCLicensee NamcoLtd0x82 { get { return mvarNamcoLtd0x82; } }

		private static SMCLicensee mvarLozc = new SMCLicensee("Lozc", 0x83);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Lozc".
		/// </summary>
		public static SMCLicensee Lozc { get { return mvarLozc; } }

		private static SMCLicensee mvarKoei0x84 = new SMCLicensee("Koei", 0x84);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Koei" with a code of 0x84.
		/// </summary>
		public static SMCLicensee Koei0x84 { get { return mvarKoei0x84; } }

		// UNK 0x85

		private static SMCLicensee mvarTokumaShotenIntermedia = new SMCLicensee("Tokuma Shoten Intermedia", 0x86);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Tokuma Shoten Intermedia".
		/// </summary>
		public static SMCLicensee TokumaShotenIntermedia { get { return mvarTokumaShotenIntermedia; } }

		private static SMCLicensee mvarTsukudaOriginal = new SMCLicensee("Tsukuda Original", 0x87);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Tsukuda Original".
		/// </summary>
		public static SMCLicensee TsukudaOriginal { get { return mvarTsukudaOriginal; } }

		private static SMCLicensee mvarDATAMPolystar = new SMCLicensee("DATAM-Polystar", 0x88);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "DATAM-Polystar".
		/// </summary>
		public static SMCLicensee DATAMPolystar { get { return mvarDATAMPolystar; } }

		// UNK 0x89
		// UNK 0x8A

		private static SMCLicensee mvarBulletProofSoftware = new SMCLicensee("Bullet-Proof Software", 0x8B);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Bullet-Proof Software".
		/// </summary>
		public static SMCLicensee BulletProofSoftware { get { return mvarBulletProofSoftware; } }

		private static SMCLicensee mvarVicTokai = new SMCLicensee("Vic Tokai", 0x8C);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Vic Tokai".
		/// </summary>
		public static SMCLicensee VicTokai { get { return mvarVicTokai; } }

		// UNK 0x8D

		private static SMCLicensee mvarCharacterSoft = new SMCLicensee("Character Soft", 0x8E);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Character Soft".
		/// </summary>
		public static SMCLicensee CharacterSoft { get { return mvarCharacterSoft; } }

		private static SMCLicensee mvarIMax = new SMCLicensee("I\'\'Max", 0x8F);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "I\'\'Max".
		/// </summary>
		public static SMCLicensee IMax { get { return mvarIMax; } }

		private static SMCLicensee mvarTakara0x90 = new SMCLicensee("Takara", 0x90);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Takara" with a code of 0x90.
		/// </summary>
		public static SMCLicensee Takara0x90 { get { return mvarTakara0x90; } }

		private static SMCLicensee mvarCHUNSoft = new SMCLicensee("CHUN Soft", 0x91);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "CHUN Soft".
		/// </summary>
		public static SMCLicensee CHUNSoft { get { return mvarCHUNSoft; } }

		private static SMCLicensee mvarVideoSystemCoLtd = new SMCLicensee("Video System Co., Ltd.", 0x92);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Video System Co., Ltd.".
		/// </summary>
		public static SMCLicensee VideoSystemCoLtd { get { return mvarVideoSystemCoLtd; } }

		private static SMCLicensee mvarBEC = new SMCLicensee("BEC", 0x93);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "BEC".
		/// </summary>
		public static SMCLicensee BEC { get { return mvarBEC; } }

		// UNK 0x94

		private static SMCLicensee mvarVarie = new SMCLicensee("Varie", 0x95);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Varie".
		/// </summary>
		public static SMCLicensee Varie { get { return mvarVarie; } }

		private static SMCLicensee mvarYonezawaSPalCorp = new SMCLicensee("Yonezawa / S'Pal Corp.", 0x96);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Yonezawa / S'Pal Corp.".
		/// </summary>
		public static SMCLicensee YonezawaSPalCorp { get { return mvarYonezawaSPalCorp; } }

		private static SMCLicensee mvarKaneco = new SMCLicensee("Kaneco", 0x97);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Kaneco".
		/// </summary>
		public static SMCLicensee Kaneco { get { return mvarKaneco; } }

		// UNK 0x98

		private static SMCLicensee mvarPackInVideo = new SMCLicensee("Pack in Video", 0x99);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Pack in Video".
		/// </summary>
		public static SMCLicensee PackInVideo { get { return mvarPackInVideo; } }

		private static SMCLicensee mvarNichibutsu = new SMCLicensee("Nichibutsu", 0x9A);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Nichibutsu".
		/// </summary>
		public static SMCLicensee Nichibutsu { get { return mvarNichibutsu; } }

		private static SMCLicensee mvarTECMO = new SMCLicensee("TECMO", 0x9B);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "TECMO".
		/// </summary>
		public static SMCLicensee TECMO { get { return mvarTECMO; } }

		private static SMCLicensee mvarImagineerCo = new SMCLicensee("Imagineer Co.", 0x9C);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Imagineer Co.".
		/// </summary>
		public static SMCLicensee ImagineerCo { get { return mvarImagineerCo; } }

		// UNK 0x9D
		// UNK 0x9E
		// UNK 0x9F

		private static SMCLicensee mvarTelenet = new SMCLicensee("Telenet", 0xA0);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Telenet".
		/// </summary>
		public static SMCLicensee Telenet { get { return mvarTelenet; } }

		private static SMCLicensee mvarHori = new SMCLicensee("Hori", 0xA1);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Hori".
		/// </summary>
		public static SMCLicensee Hori { get { return mvarHori; } }

		// UNK 0xA2
		// UNK 0xA3

		private static SMCLicensee mvarKonami0xA4 = new SMCLicensee("Konami", 0xA4);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Konami" with a code of 0xA4.
		/// </summary>
		public static SMCLicensee Konami0xA4 { get { return mvarKonami0xA4; } }

		private static SMCLicensee mvarKAmusementLeasingCo = new SMCLicensee("K.Amusement Leasing Co.", 0xA5);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "K.Amusement Leasing Co.".
		/// </summary>
		public static SMCLicensee KAmusementLeasingCo { get { return mvarKAmusementLeasingCo; } }
		
		// UNK 0xA6

		private static SMCLicensee mvarTakara0xA7 = new SMCLicensee("Takara", 0xA7);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Takara" with a code of 0xA7.
		/// </summary>
		public static SMCLicensee Takara0xA7 { get { return mvarTakara0xA7; } }

		// UNK 0xA8

		private static SMCLicensee mvarTechnosJap = new SMCLicensee("Technos Jap.", 0xA9);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Technos Jap.".
		/// </summary>
		public static SMCLicensee TechnosJap { get { return mvarTechnosJap; } }

		private static SMCLicensee mvarJVC = new SMCLicensee("JVC", 0xAA);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "JVC".
		/// </summary>
		public static SMCLicensee JVC { get { return mvarJVC; } }

		// UNK 0xAB

		private static SMCLicensee mvarToeiAnimation = new SMCLicensee("Toei Animation", 0xAC);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Toei Animation".
		/// </summary>
		public static SMCLicensee ToeiAnimation { get { return mvarToeiAnimation; } }

		private static SMCLicensee mvarToho = new SMCLicensee("Toho", 0xAD);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Toho".
		/// </summary>
		public static SMCLicensee Toho { get { return mvarToho; } }

		// UNK 0xAE

		private static SMCLicensee mvarNamcoLtd0xAF = new SMCLicensee("Namco Ltd.", 0xAF);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Namco Ltd." with a code of 0xAF.
		/// </summary>
		public static SMCLicensee NamcoLtd0xAF { get { return mvarNamcoLtd0xAF; } }

		private static SMCLicensee mvarMediaRingsCorp = new SMCLicensee("Media Rings Corp.", 0xB0);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Media Rings Corp.".
		/// </summary>
		public static SMCLicensee MediaRingsCorp { get { return mvarMediaRingsCorp; } }

		private static SMCLicensee mvarASCIICoActivision = new SMCLicensee("ASCII Co. Activison", 0xB1);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "ASCII Co. Activison".
		/// </summary>
		public static SMCLicensee ASCIICoActivision { get { return mvarASCIICoActivision; } }

		private static SMCLicensee mvarBandai = new SMCLicensee("Bandai", 0xB2);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Bandai".
		/// </summary>
		public static SMCLicensee Bandai { get { return mvarBandai; } }

		// UNK 0xB3

		private static SMCLicensee mvarEnixAmerica = new SMCLicensee("Enix America", 0xB4);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Enix America".
		/// </summary>
		public static SMCLicensee EnixAmerica { get { return mvarEnixAmerica; } }

		// UNK 0xB5

		private static SMCLicensee mvarHalken = new SMCLicensee("Halken", 0xB6);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Halken".
		/// </summary>
		public static SMCLicensee Halken { get { return mvarHalken; } }

		// UNK 0xB7
		// UNK 0xB8
		// UNK 0xB9

		private static SMCLicensee mvarCultureBrain0xBA = new SMCLicensee("Culture Brain", 0xBA);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Culture Brain" with a code of 0xBA.
		/// </summary>
		public static SMCLicensee CultureBrain0xBA { get { return mvarCultureBrain0xBA; } }

		private static SMCLicensee mvarSunsoft0xBB = new SMCLicensee("Sunsoft", 0xBB);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Sunsoft" with a code of 0xBB.
		/// </summary>
		public static SMCLicensee Sunsoft0xBB { get { return mvarSunsoft0xBB; } }

		private static SMCLicensee mvarToshibaEMI = new SMCLicensee("Toshiba EMI", 0xBC);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Toshiba EMI".
		/// </summary>
		public static SMCLicensee ToshibaEMI { get { return mvarToshibaEMI; } }

		private static SMCLicensee mvarSonyImagesoft = new SMCLicensee("Sony Imagesoft", 0xBD);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Sony Imagesoft".
		/// </summary>
		public static SMCLicensee SonyImagesoft { get { return mvarSonyImagesoft; } }

		// UNK 0xBE

		private static SMCLicensee mvarSammy = new SMCLicensee("Sammy", 0xBF);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Sammy".
		/// </summary>
		public static SMCLicensee Sammy { get { return mvarSammy; } }

		private static SMCLicensee mvarTaito = new SMCLicensee("Taito", 0xC0);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Taito".
		/// </summary>
		public static SMCLicensee Taito { get { return mvarTaito; } }

		// UNK 0xC1

		private static SMCLicensee mvarKemco0xC2 = new SMCLicensee("Kemco", 0xC2);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Kemco" with a code of 0xC2.
		/// </summary>
		public static SMCLicensee Kemco0xC2 { get { return mvarKemco0xC2; } }

		private static SMCLicensee mvarSquare = new SMCLicensee("Square", 0xC3);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Square".
		/// </summary>
		public static SMCLicensee Square { get { return mvarSquare; } }

		private static SMCLicensee mvarTokumaSoft = new SMCLicensee("Tokuma Soft", 0xC4);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Tokuma Soft".
		/// </summary>
		public static SMCLicensee TokumaSoft { get { return mvarTokumaSoft; } }

		private static SMCLicensee mvarDataEast = new SMCLicensee("Data East", 0xC5);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Data East".
		/// </summary>
		public static SMCLicensee DataEast { get { return mvarDataEast; } }

		private static SMCLicensee mvarTonkinHouse = new SMCLicensee("Tonkin House", 0xC6);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Tonkin House".
		/// </summary>
		public static SMCLicensee TonkinHouse { get { return mvarTonkinHouse; } }

		// UNK 0xC7

		private static SMCLicensee mvarKoei0xC8 = new SMCLicensee("KOEI", 0xC8);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "KOEI" with a code of 0xC8.
		/// </summary>
		public static SMCLicensee Koei0xC8 { get { return mvarKoei0xC8; } }

		// UNK 0xC9

		private static SMCLicensee mvarKonamiUSA = new SMCLicensee("Konami USA", 0xCA);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Konami USA".
		/// </summary>
		public static SMCLicensee KonamiUSA { get { return mvarKonamiUSA; } }

		private static SMCLicensee mvarNTVIC = new SMCLicensee("NTVIC", 0xCB);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "NTVIC".
		/// </summary>
		public static SMCLicensee NTVIC { get { return mvarNTVIC; } }

		// UNK 0xCC

		private static SMCLicensee mvarMeldac = new SMCLicensee("Meldac", 0xCD);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Meldac".
		/// </summary>
		public static SMCLicensee Meldac { get { return mvarMeldac; } }

		private static SMCLicensee mvarPonyCanyon = new SMCLicensee("Pony Canyon", 0xCE);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Pony Canyon".
		/// </summary>
		public static SMCLicensee PonyCanyon { get { return mvarPonyCanyon; } }

		private static SMCLicensee mvarSotsuAgencySunrise = new SMCLicensee("Sotsu Agency/Sunrise", 0xCF);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Sotsu Agency/Sunrise".
		/// </summary>
		public static SMCLicensee SotsuAgencySunrise { get { return mvarSotsuAgencySunrise; } }

		private static SMCLicensee mvarDiscoTaito = new SMCLicensee("Disco/Taito", 0xD0);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Disco/Taito".
		/// </summary>
		public static SMCLicensee DiscoTaito { get { return mvarDiscoTaito; } }

		private static SMCLicensee mvarSofel = new SMCLicensee("Sofel", 0xD1);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Sofel".
		/// </summary>
		public static SMCLicensee Sofel { get { return mvarSofel; } }

		private static SMCLicensee mvarQuestCorp = new SMCLicensee("Quest Corp.", 0xD2);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Quest Corp.".
		/// </summary>
		public static SMCLicensee QuestCorp { get { return mvarQuestCorp; } }

		private static SMCLicensee mvarSigma = new SMCLicensee("Sigma", 0xD3);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Sigma".
		/// </summary>
		public static SMCLicensee Sigma { get { return mvarSigma; } }

		private static SMCLicensee mvarAskKodanshaCoLtd = new SMCLicensee("Ask Kodansha Co., Ltd.", 0xD4);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Ask Kodansha Co., Ltd.".
		/// </summary>
		public static SMCLicensee AskKodanshaCoLtd { get { return mvarAskKodanshaCoLtd; } }

		// UNK 0xD5

		private static SMCLicensee mvarNaxat = new SMCLicensee("Naxat", 0xD6);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Naxat".
		/// </summary>
		public static SMCLicensee Naxat { get { return mvarNaxat; } }

		// UNK 0xD7

		private static SMCLicensee mvarCapcomCoLtd = new SMCLicensee("Capcom Co., Ltd.", 0xD8);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Capcom Co., Ltd.".
		/// </summary>
		public static SMCLicensee CapcomCoLtd { get { return mvarCapcomCoLtd; } }

		private static SMCLicensee mvarBanpresto = new SMCLicensee("Banpresto", 0xD9);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Banpresto".
		/// </summary>
		public static SMCLicensee Banpresto { get { return mvarBanpresto; } }

		private static SMCLicensee mvarTomy = new SMCLicensee("Tomy", 0xDA);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Tomy".
		/// </summary>
		public static SMCLicensee Tomy { get { return mvarTomy; } }

		private static SMCLicensee mvarAcclaim0xDB = new SMCLicensee("Acclaim", 0xDB);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Acclaim" with a code of 0xDB.
		/// </summary>
		public static SMCLicensee Acclaim0xDB { get { return mvarAcclaim0xDB; } }
		
		// UNK 0xDC

		private static SMCLicensee mvarNCS = new SMCLicensee("NCS", 0xDD);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "NCS".
		/// </summary>
		public static SMCLicensee NCS { get { return mvarNCS; } }

		private static SMCLicensee mvarHumanEntertainment = new SMCLicensee("Human Entertainment", 0xDE);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Human Entertainment".
		/// </summary>
		public static SMCLicensee HumanEntertainment { get { return mvarHumanEntertainment; } }

		private static SMCLicensee mvarAltron = new SMCLicensee("Altron", 0xDF);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Altron".
		/// </summary>
		public static SMCLicensee Altron { get { return mvarAltron; } }

		private static SMCLicensee mvarJaleco0xE0 = new SMCLicensee("Jaleco", 0xE0);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Jaleco" with a code of 0xE0.
		/// </summary>
		public static SMCLicensee Jaleco0xE0 { get { return mvarJaleco0xE0; } }

		// UNK 0xE1

		private static SMCLicensee mvarYutaka = new SMCLicensee("Yutaka", 0xE2);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Yutaka".
		/// </summary>
		public static SMCLicensee Yutaka { get { return mvarYutaka; } }

		// UNK 0xE3

		private static SMCLicensee mvarTAndESoft = new SMCLicensee("T&ESoft", 0xE4);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "T&ESoft".
		/// </summary>
		public static SMCLicensee TAndESoft { get { return mvarTAndESoft; } }

		private static SMCLicensee mvarEPOCHCoLtd = new SMCLicensee("EPOCH Co.,Ltd.", 0xE5);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "EPOCH Co.,Ltd.".
		/// </summary>
		public static SMCLicensee EPOCHCoLtd { get { return mvarEPOCHCoLtd; } }

		// UNK 0xE6

		private static SMCLicensee mvarAthena = new SMCLicensee("Athena", 0xE7);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Athena".
		/// </summary>
		public static SMCLicensee Athena { get { return mvarAthena; } }

		private static SMCLicensee mvarAsmik = new SMCLicensee("Asmik", 0xE8);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Asmik".
		/// </summary>
		public static SMCLicensee Asmik { get { return mvarAsmik; } }

		private static SMCLicensee mvarNatsume = new SMCLicensee("Natsume", 0xE9);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Natsume".
		/// </summary>
		public static SMCLicensee Natsume { get { return mvarNatsume; } }

		private static SMCLicensee mvarKingRecords = new SMCLicensee("King Records", 0xEA);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "King Records".
		/// </summary>
		public static SMCLicensee KingRecords { get { return mvarKingRecords; } }

		private static SMCLicensee mvarAtlus = new SMCLicensee("Atlus", 0xEB);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Atlus".
		/// </summary>
		public static SMCLicensee Atlus { get { return mvarAtlus; } }

		private static SMCLicensee mvarSonyMusicEntertainment = new SMCLicensee("Sony Music Entertainment", 0xEC);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Sony Music Entertainment".
		/// </summary>
		public static SMCLicensee SonyMusicEntertainment { get { return mvarSonyMusicEntertainment; } }

		// UNK 0xED

		private static SMCLicensee mvarIGS = new SMCLicensee("IGS", 0xEE);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "IGS".
		/// </summary>
		public static SMCLicensee IGS { get { return mvarIGS; } }

		// UNK 0xEF
		// UNK 0xF0

		private static SMCLicensee mvarMotownSoftware = new SMCLicensee("Motown Software", 0xF1);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Motown Software".
		/// </summary>
		public static SMCLicensee MotownSoftware { get { return mvarMotownSoftware; } }

		private static SMCLicensee mvarLeftFieldEntertainment = new SMCLicensee("Left Field Entertainment", 0xF2);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Left Field Entertainment".
		/// </summary>
		public static SMCLicensee LeftFieldEntertainment { get { return mvarLeftFieldEntertainment; } }

		private static SMCLicensee mvarBeamSoftware = new SMCLicensee("Beam Software", 0xF3);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Beam Software".
		/// </summary>
		public static SMCLicensee BeamSoftware { get { return mvarBeamSoftware; } }

		private static SMCLicensee mvarTecMagik = new SMCLicensee("Tec Magik", 0xF4);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Tec Magik".
		/// </summary>
		public static SMCLicensee TecMagik { get { return mvarTecMagik; } }

		// UNK 0xF5
		// UNK 0xF6
		// UNK 0xF7
		// UNK 0xF8

		private static SMCLicensee mvarCybersoft = new SMCLicensee("Cybersoft", 0xF9);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Cybersoft".
		/// </summary>
		public static SMCLicensee Cybersoft { get { return mvarCybersoft; } }

		// UNK 0xFA

		private static SMCLicensee mvarPsygnosis = new SMCLicensee("Psygnosis", 0xFB);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Psygnosis".
		/// </summary>
		public static SMCLicensee Psygnosis { get { return mvarPsygnosis; } }

		// UNK 0xFC
		// UNK 0xFD

		private static SMCLicensee mvarDavidson = new SMCLicensee("Davidson", 0xFE);
		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> representing "Davidson".
		/// </summary>
		public static SMCLicensee Davidson { get { return mvarDavidson; } }

		// UNK 0xFF

	}
}
