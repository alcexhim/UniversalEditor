using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Security.Key.RSA
{
	public class RSAKeyObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "RSA cryptography key";
				_omr.Path = new string[] { "Security", "Key" };
			}
			return _omr;
		}

		private byte[] mvarP = new byte[0];
		public byte[] P { get { return mvarP; } set { mvarP = value; } }
		private byte[] mvarQ = new byte[0];
		public byte[] Q { get { return mvarQ; } set { mvarQ = value; } }
		private byte[] mvarDP = new byte[0];
		public byte[] DP { get { return mvarDP; } set { mvarDP = value; } }
		private byte[] mvarDQ = new byte[0];
		public byte[] DQ { get { return mvarDQ; } set { mvarDQ = value; } }
		private byte[] mvarIQ = new byte[0];
		public byte[] IQ { get { return mvarIQ; } set { mvarIQ = value; } }
		private byte[] mvarD = new byte[0];
		public byte[] D { get { return mvarD; } set { mvarD = value; } }

		private int mvarBitLength = 0;
		/// <summary>
		/// The bit length of the private key components.
		/// </summary>
		public int BitLength { get { return mvarBitLength; } set { mvarBitLength = value; } }

		private uint mvarPublicExponent = 0;
		/// <summary>
		/// The exponent of the public key.
		/// </summary>
		public uint PublicExponent { get { return mvarPublicExponent; } set { mvarPublicExponent = value; } }

		private byte[] mvarModulus = new byte[0];
		/// <summary>
		/// The RSA modulus.
		/// </summary>
		public byte[] Modulus { get { return mvarModulus; } set { mvarModulus = value; } }

		public override void Clear()
		{
			mvarBitLength = 0;
			mvarD = new byte[0];
			mvarDP = new byte[0];
			mvarDQ = new byte[0];
			mvarIQ = new byte[0];
			mvarModulus = new byte[0];
			mvarP = new byte[0];
			mvarPublicExponent = 0;
			mvarQ = new byte[0];
		}

		public override void CopyTo(ObjectModel where)
		{
			RSAKeyObjectModel clone = (where as RSAKeyObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			clone.BitLength = mvarBitLength;
			clone.D = mvarD;
			clone.DP = mvarDP;
			clone.DQ = mvarDQ;
			clone.IQ = mvarIQ;
			clone.Modulus = mvarModulus;
			clone.P = mvarP;
			clone.PublicExponent = mvarPublicExponent;
			clone.Q = mvarQ;
		}
	}
}
