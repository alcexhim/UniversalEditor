//
//  SMCCartridgeType.cs - provides metadata information about cartridge types for an SMC game dump
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
	/// Indicates the hardware in the cartridge. Emulators use this byte to decide which hardware to emulate. A real SNES ignores this byte and uses the real hardware in the real cartridge.
	/// </summary>
	/// <completionlist cref="SMCCartridgeTypes" />
	public class SMCCartridgeType
	{
		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the cartridge type.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private byte mvarValue = 0;
		/// <summary>
		/// The single-byte code that represents this cartridge type in the Nintendo SNES game console.
		/// </summary>
		public byte Value { get { return mvarValue; } set { mvarValue = value; } }


		/// <summary>
		/// Gets the <see cref="SMCLicensee" /> with the given licensee code if valid.
		/// </summary>
		/// <param name="value">The licensee code to search on.</param>
		/// <returns>If the licensee code is known, returns an instance of the associated <see cref="SMCLicensee" />. Otherwise, returns null.</returns>
		public static SMCCartridgeType FromCode(byte value)
		{
			Type t = typeof(SMCCartridgeTypes);

			MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;
			PropertyInfo[] properties = t.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.PropertyType == typeof(SMCCartridgeType))
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (getMethod != null && (getMethod.Attributes & methodAttributes) == methodAttributes)
					{
						object[] index = null;
						SMCCartridgeType val = (SMCCartridgeType)propertyInfo.GetValue(null, index);

						if (val.Value == value) return val;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Creates a new instance of <see cref="SMCCartridgeType" /> with the given title and value.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="value"></param>
		public SMCCartridgeType(string title, byte value)
		{
			mvarTitle = title;
			mvarValue = value;
		}

		/// <summary>
		/// Translates this <see cref="SMCCartridgeType" /> into a human-readable string, including the
		/// title of the cartridge type and the internal identifier.
		/// </summary>
		/// <returns>A human-readable string representing this <see cref="SMCCartridgeType" />.</returns>
		public override string ToString()
		{
			return mvarTitle + " (0x" + mvarValue.ToString("X").PadLeft(2, '0') + ")";
		}
	}
	public sealed class SMCCartridgeTypes
	{
		private static SMCCartridgeType mvarROMOnly = new SMCCartridgeType("ROM-only", 0x00);
		/// <summary>
		/// The cartridge contains only ROM.
		/// </summary>
		public static SMCCartridgeType ROMOnly { get { return mvarROMOnly; } }

		private static SMCCartridgeType mvarROMAndRAM = new SMCCartridgeType("ROM and RAM, no battery", 0x01);
		/// <summary>
		/// The cartridge contains ROM and RAM but no battery.
		/// </summary>
		public static SMCCartridgeType ROMAndRAM { get { return mvarROMAndRAM; } }

		private static SMCCartridgeType mvarROMAndRAMWithBattery = new SMCCartridgeType("ROM and save-RAM, with battery", 0x02);
		/// <summary>
		/// The cartridge contains ROM and save-RAM (with a battery).
		/// </summary>
		public static SMCCartridgeType ROMAndRAMWithBattery { get { return mvarROMAndRAMWithBattery; } }

		private static SMCCartridgeType mvarSuperFXNoBattery0x13 = new SMCCartridgeType("SuperFX, no battery", 0x13);
		/// <summary>
		/// SuperFX, no battery (0x13)
		/// </summary>
		public static SMCCartridgeType SuperFXNoBattery0x13 { get { return mvarSuperFXNoBattery0x13; } }

		private static SMCCartridgeType mvarSuperFXNoBattery0x14 = new SMCCartridgeType("SuperFX, no battery", 0x14);
		/// <summary>
		/// SuperFX, no battery (0x14)
		/// </summary>
		public static SMCCartridgeType SuperFXNoBattery0x14 { get { return mvarSuperFXNoBattery0x14; } }

		private static SMCCartridgeType mvarSuperFXWithBattery0x15 = new SMCCartridgeType("SuperFX, with battery", 0x15);
		/// <summary>
		/// SuperFX, with battery (0x15)
		/// </summary>
		public static SMCCartridgeType SuperFXWithBattery0x15 { get { return mvarSuperFXWithBattery0x15; } }

		private static SMCCartridgeType mvarSuperFXWithBattery0x1A = new SMCCartridgeType("SuperFX, with battery", 0x1A);
		/// <summary>
		/// SuperFX, with battery (0x1A)
		/// </summary>
		public static SMCCartridgeType SuperFXWithBattery0x1A { get { return mvarSuperFXWithBattery0x1A; } }

		private static SMCCartridgeType mvarSA10x34 = new SMCCartridgeType("SA-1", 0x34);
		/// <summary>
		/// SuperFX, with battery (0x15)
		/// </summary>
		public static SMCCartridgeType SA10x34 { get { return mvarSA10x34; } }

		private static SMCCartridgeType mvarSA10x35 = new SMCCartridgeType("SA-1", 0x35);
		/// <summary>
		/// SuperFX, with battery (0x1A)
		/// </summary>
		public static SMCCartridgeType SA10x35 { get { return mvarSA10x35; } }
	}
}
