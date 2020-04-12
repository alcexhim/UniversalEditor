//
//  SMCRegion.cs - provides metadata information about regions for Nintendo SNES game dump files in SMC format
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
	/// Provides metadata information about regions for Nintendo SNES game dump files in SMC format.
	/// </summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:UniversalEditor.DataFormats.Executable.Nintendo.SNES.SMCRegions" />
	public class SMCRegion
	{
		private string mvarTitle = String.Empty;
		/// <summary>
		/// The human-readable title of this <see cref="SMCRegion" />.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private byte mvarValue = 0;
		/// <summary>
		/// The single-byte code that corresponds to this <see cref="SMCRegion" /> in the SMC data
		/// format.
		/// </summary>
		public byte Value { get { return mvarValue; } set { mvarValue = value; } }

		/// <summary>
		/// Initializes a new <see cref="SMCRegion" /> with the specified title and value.
		/// </summary>
		/// <param name="title">The human-readable title of this <see cref="SMCRegion" />.</param>
		/// <param name="value">The single-byte code that corresponds to this <see cref="SMCRegion" /> in the SMC data format.</param>
		public SMCRegion(string title, byte value)
		{
			mvarTitle = title;
			mvarValue = value;
		}

		/// <summary>
		/// Gets the <see cref="SMCRegion" /> with the given region code if valid.
		/// </summary>
		/// <param name="value">The region code to search on.</param>
		/// <returns>If the region code is known, returns an instance of the associated <see cref="SMCRegion" />. Otherwise, returns null.</returns>
		public static SMCRegion FromCode(byte value)
		{
			Type t = typeof(SMCRegions);

			MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;
			PropertyInfo[] properties = t.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.PropertyType == typeof(SMCRegion))
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (getMethod != null && (getMethod.Attributes & methodAttributes) == methodAttributes)
					{
						object[] index = null;
						SMCRegion val = (SMCRegion)propertyInfo.GetValue(null, index);

						if (val.Value == value) return val;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Translates this <see cref="SMCRegion" /> into a human-readable string, including the
		/// title of the region and the country code.
		/// </summary>
		/// <returns>A human-readable string representing this <see cref="SMCRegion" />.</returns>
		public override string ToString()
		{
			return mvarTitle + " (0x" + mvarValue.ToString("X").PadLeft(2, '0') + ")";
		}
	}
	/// <summary>
	/// Regions that have been defined by the SNES SMC data format. This class cannot be inherited.
	/// </summary>
	public sealed class SMCRegions
	{
		private static SMCRegion mvarJapan = new SMCRegion("Japan (NTSC)", 0x00);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Japan (NTSC)".
		/// </summary>
		public static SMCRegion Japan { get { return mvarJapan; } }

		private static SMCRegion mvarNorthAmerica = new SMCRegion("USA and Canada (NTSC)", 0x01);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "USA and Canada (NTSC)".
		/// </summary>
		public static SMCRegion NorthAmerica { get { return mvarNorthAmerica; } }

		private static SMCRegion mvarEurasia = new SMCRegion("Europe, Oceania, and Asia (PAL)", 0x02);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Europe, Oceania, and Asia (PAL)".
		/// </summary>
		public static SMCRegion Eurasia { get { return mvarEurasia; } }

		private static SMCRegion mvarSweden = new SMCRegion("Sweden (PAL)", 0x03);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Sweden (PAL)".
		/// </summary>
		public static SMCRegion Sweden { get { return mvarSweden; } }

		private static SMCRegion mvarFinland = new SMCRegion("Finland (PAL)", 0x04);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Finland (PAL)".
		/// </summary>
		public static SMCRegion Finland { get { return mvarFinland; } }

		private static SMCRegion mvarDenmark = new SMCRegion("Denmark (PAL)", 0x05);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Denmark (PAL)".
		/// </summary>
		public static SMCRegion Denmark { get { return mvarDenmark; } }

		private static SMCRegion mvarFrance = new SMCRegion("France (PAL)", 0x06);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "France (PAL)".
		/// </summary>
		public static SMCRegion France { get { return mvarFrance; } }

		private static SMCRegion mvarHolland = new SMCRegion("Holland (PAL)", 0x07);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Holland (PAL)".
		/// </summary>
		public static SMCRegion Holland { get { return mvarHolland; } }

		private static SMCRegion mvarSpain = new SMCRegion("Spain (PAL)", 0x08);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Spain (PAL)".
		/// </summary>
		public static SMCRegion Spain { get { return mvarSpain; } }

		private static SMCRegion mvarGermanyAustriaSwitzerland = new SMCRegion("Germany, Austria, and Switzerland (PAL)", 0x09);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Germany, Austria, and Switzerland (PAL)".
		/// </summary>
		public static SMCRegion GermanyAustriaSwitzerland { get { return mvarGermanyAustriaSwitzerland; } }

		private static SMCRegion mvarItaly = new SMCRegion("Italy (PAL)", 0x0A);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Italy (PAL)".
		/// </summary>
		public static SMCRegion Italy { get { return mvarItaly; } }

		private static SMCRegion mvarHongKongAndChina = new SMCRegion("Hong Kong and China (PAL)", 0x0B);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Hong Kong and China (PAL)".
		/// </summary>
		public static SMCRegion HongKongAndChina { get { return mvarHongKongAndChina; } }

		private static SMCRegion mvarIndonesia = new SMCRegion("Indonesia (PAL)", 0x0C);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "Indonesia (PAL)".
		/// </summary>
		public static SMCRegion Indonesia { get { return mvarIndonesia; } }

		private static SMCRegion mvarSouthKorea = new SMCRegion("South Korea (NTSC)", 0x0D);
		/// <summary>
		/// Gets the <see cref="SMCRegion" /> representing "South Korea (NTSC)".
		/// </summary>
		public static SMCRegion SouthKorea { get { return mvarSouthKorea; } }

		// UNK 0x0E
		// UNK 0x0F
	}
}
