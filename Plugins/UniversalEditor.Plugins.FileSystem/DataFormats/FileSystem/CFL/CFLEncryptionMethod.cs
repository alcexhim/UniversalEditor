//
//  CFLEncryptionMethod.cs - indicates the encryption method of a CFL archive
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.FileSystem.CFL
{
	/// <summary>
	/// Indicates the encryption method of a CFL archive.
	/// </summary>
	public enum CFLEncryptionMethod
	{
		/// <summary>
		/// No encryption.
		/// </summary>
		None = 0x00000000,
		/// <summary>
		/// Simple XOR crypt (generally stops casual hex-editor), key is one char.
		/// </summary>
		SimpleXor = 0x01000000,
		/// <summary>
		/// XOR's every byte with data from pseudorandom generator, key is the random seed.
		/// </summary>
		PseudoRandomXor = 0x02000000,
		/// <summary>
		/// XOR's every byte with a letter from entered string. Somewhat easy to crack
		/// if string is short, but is easy way to implement password protection.
		/// </summary>
		StringXor = 0x03000000,
		PGP = 0x10000000, //!< Pretty Good Privacy
		GPG = 0x20000000, //!< GPG
		DES = 0x30000000, //!< Data Encryption Standard
		TripleDES = 0x40000000, //!< Triple-DES
		BLOWFISH = 0x50000000, //!< Blowfish
		IDEA = 0x60000000, //!< IDEA
		RC4 = 0x70000000  //!< RC4
	}
}
