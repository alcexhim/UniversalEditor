using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ZipTV.BlakHole
{
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
