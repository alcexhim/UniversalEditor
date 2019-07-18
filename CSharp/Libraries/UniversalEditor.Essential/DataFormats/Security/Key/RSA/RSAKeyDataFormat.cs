using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Security.Key.RSA;

namespace UniversalEditor.DataFormats.Security.Key.RSA
{
	public class RSAKeyDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(RSAKeyObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private byte mvarFormatVersion = 0x02;
		public byte FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			RSAKeyObjectModel key = (objectModel as RSAKeyObjectModel);
			if (key == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			
			// ---- read the BLOBHEADER struct ------
			bool dotnetkey = false;
			RSAKeyType btype = (RSAKeyType)reader.ReadByte();
			if (btype != RSAKeyType.PublicKeyBlob && btype != RSAKeyType.PrivateKeyBlob)
			{
				// possibly a .NET publickey
				reader.Accessor.Seek(11, SeekOrigin.Current); // advance past 3 int headers, minus 1 byte read
				btype = (RSAKeyType)reader.ReadByte();
				if (btype != RSAKeyType.PublicKeyBlob && btype != RSAKeyType.PrivateKeyBlob)
				{
					throw new InvalidDataFormatException("Key is neither a PublicKeyBlob nor a PrivateKeyBlob");
				}
				dotnetkey = true;
			}

			byte version = reader.ReadByte();
			if (version != mvarFormatVersion)
			{
				throw new InvalidDataFormatException("Invalid version for format " + version.ToString());
			}

			ushort reserved = reader.ReadUInt16();
			RSAKeyAlgorithm algorithm = (RSAKeyAlgorithm)reader.ReadUInt32();
			if (algorithm != RSAKeyAlgorithm.KeyX  && algorithm != RSAKeyAlgorithm.Sign)
			{
				throw new InvalidDataFormatException("Unknown algorithm " + algorithm.ToString());
			}

			//---- read the RSAPUBKEY struct --------
			string magic = reader.ReadFixedLengthString(4);
			if (magic != "RSA1" && magic != "RSA2")
			{
				throw new InvalidDataFormatException("File does not begin with 'RSA1' or 'RSA2'");
			}
			int bitlen = reader.ReadInt32();   //get RSA bit length
			key.BitLength = bitlen;

			//---- read RSA public exponent ------ 
			reader.Endianness = Endianness.BigEndian;
			uint pubexp = reader.ReadUInt32();   //get public exponent
			key.PublicExponent = (int)pubexp;

			//---- read RSA modulus -----------
			//Reverse byte array for little-endian to big-endian conversion 
			byte[] RSAmodulus = reader.ReadBytes(bitlen / 8);
			Array.Reverse(RSAmodulus);
			key.Modulus = RSAmodulus;
			
			//-- if this is a valid unencrypted PRIVATEKEYBLOB, read RSA private key properties
			if (btype == RSAKeyType.PrivateKeyBlob)
			{
				int bitlen16 = bitlen / 16;
				byte[] P = reader.ReadBytes(bitlen16);
				Array.Reverse(P);
				if (P.Length != bitlen16)
					throw new InvalidDataFormatException("Invalid bit length for P");
				key.P = P;

				byte[] Q = reader.ReadBytes(bitlen16);
				Array.Reverse(Q);
				if (Q.Length != bitlen16)
					throw new InvalidDataFormatException("Invalid bit length for Q");
				key.Q = Q;

				byte[] DP = reader.ReadBytes(bitlen16);
				Array.Reverse(DP);
				if (DP.Length != bitlen16)
					throw new InvalidDataFormatException("Invalid bit length for DP");
				key.DP = DP;

				byte[] DQ = reader.ReadBytes(bitlen16);
				Array.Reverse(DQ);
				if (DQ.Length != bitlen16)
					throw new InvalidDataFormatException("Invalid bit length for DQ");
				key.DQ = DQ;

				byte[] IQ = reader.ReadBytes(bitlen16);
				Array.Reverse(IQ);
				if (IQ.Length != bitlen16)
					throw new InvalidDataFormatException("Invalid bit length for IQ");
				key.IQ = IQ;

				byte[] D = reader.ReadBytes(bitlen / 8);
				Array.Reverse(D);
				if (D.Length != bitlen / 8)
					throw new InvalidDataFormatException("Invalid bit length for D");
				key.D = D;
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
