//
//  ExtensionMethods.cs - extension methods for Unreal Engine functionality
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

using UniversalEditor.IO;

namespace UniversalEditor.Plugins.UnrealEngine
{
	/// <summary>
	/// Extension methods for Unreal Engine functionality.
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		/// The Name type is a simple string type. The format does, although, differ between the
		/// package versions.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static string ReadNAME(this Reader br, int packageVersion)
		{
			string value = String.Empty;

			// Older package versions (<64, original Unreal engine) store the Name type as a
			// zero-terminated ASCII string; "UT2k3", for example would be stored as:
			//      "U" "T" "2" "k" "3" 0x00
			if (packageVersion < 64)
			{
				value = br.ReadNullTerminatedString();
			}
			else if (packageVersion >= 512)
			{
				uint length = br.ReadUInt32();
				value = br.ReadNullTerminatedString();
			}
			else
			{
				// Newer packages (>=64, UT engine) prepend the length of the string plus the trailing
				// zero. Again, "UT2k3" would be now stored as: 0x06 "U" "T" "2" "k" "3" 0x00
				value = br.ReadLengthPrefixedString();
				if (value.EndsWith("\0"))
				{
					value = value.TrimNull();
				}
				else
				{
					// do we need this sanity check?
					throw new InvalidOperationException();
				}
			}
			return value;
		}
		public static void WriteNAME(this Writer bw, string value, int packageVersion)
		{
			// Older package versions (<64, original Unreal engine) store the Name type as a
			// zero-terminated ASCII string; "UT2k3", for example would be stored as:
			//      "U" "T" "2" "k" "3" 0x00
			if (packageVersion < 64)
			{
				bw.WriteNullTerminatedString(value);
			}
			else if (packageVersion >= 512)
			{
				bw.WriteUInt32((uint)value.Length);
				bw.WriteNullTerminatedString(value);
			}
			else
			{
				// Newer packages (>=64, UT engine) prepend the length of the string plus the trailing
				// zero. Again, "UT2k3" would be now stored as: 0x06 "U" "T" "2" "k" "3" 0x00
				bw.Write7BitEncodedInt32(value.Length + 1);
				bw.WriteFixedLengthString(value);
				bw.WriteByte((byte)0);
			}
		}

		/// <summary>
		/// Reads a compact integer from the FileReader. Bytes read differs, so do not make
		/// assumptions about physical data being read from the stream. (If you have to, get the
		/// difference of FileReader.BaseStream.Position before and after this is executed.)
		/// </summary>
		/// <returns>An "uncompacted" signed integer.</returns>
		/// <remarks>There may be better ways to implement this, but this is fast, and it works.</remarks>
		public static int ReadINDEX(this Reader br)
		{
			int output = 0;
			bool signed = false;
			for (int i = 0; i < 5; i++)
			{
				byte x = br.ReadByte();
				// First byte
				if (i == 0)
				{
					// Bit: X0000000
					if ((x & 0x80) > 0)
					{
						signed = true;
					}
					// Bits: 00XXXXXX
					output |= (x & 0x3F);
					// Bit: 0X000000
					if ((x & 0x40) == 0)
					{
						break;
					}
				}
				// Last byte
				else if (i == 4)
				{
					// Bits: 000XXXXX -- the 0 bits are ignored
					// (hits the 32 bit boundary)
					output |= (x & 0x1F) << (6 + (3 * 7));
				}
				// Middle bytes
				else
				{
					// Bits: 0XXXXXXX
					output |= (x & 0x7F) << (6 + ((i - 1) * 7));
					// Bit: X0000000
					if ((x & 0x80) == 0)
					{
						break;
					}
				}
			}
			// multiply by negative one here, since the first 6+ bits could be 0
			if (signed)
			{
				output *= -1;
			}
			return (output);
		}
		public static void WriteINDEX(this Writer bw, int value)
		{
			bw.Write7BitEncodedInt32(value);
		}
	}
}
