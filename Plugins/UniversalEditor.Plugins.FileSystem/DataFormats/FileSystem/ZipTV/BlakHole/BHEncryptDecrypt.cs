//
//  BHEncryptDecrypt.cs - implements functions for encrypting and decrypting a ZipTV BlakHole archive
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

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ZipTV.BlakHole
{
	/// <summary>
	/// Implements functions for encrypting and decrypting a ZipTV BlakHole archive.
	/// </summary>
	internal class BHEncryptDecrypt
	{
		// This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
		// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
		// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
		private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

		// This constant is used to determine the keysize of the encryption algorithm.
		private const int keysize = 256;

		public static byte[] Decrypt(byte[] input, string password)
		{
			PasswordDeriveBytes passwd = new PasswordDeriveBytes(password, null);

			byte[] keyBytes = passwd.GetBytes(keysize / 8);
			using (AesManaged symmetricKey = new AesManaged())
			{
				symmetricKey.Mode = CipherMode.CBC;
				using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
				{
					using (MemoryStream memoryStream = new MemoryStream(input))
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
						{
							byte[] output = new byte[input.Length];
							int decryptedByteCount = cryptoStream.Read(output, 0, output.Length);
							return output;
						}
					}
				}
			}
		}
	}
}
