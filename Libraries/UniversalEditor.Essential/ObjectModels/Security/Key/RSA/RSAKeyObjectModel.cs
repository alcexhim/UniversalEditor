//
//  RSAKeyObjectModel.cs - provides an ObjectModel for manipulating an RSA cryptographic key
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

namespace UniversalEditor.ObjectModels.Security.Key.RSA
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating an RSA cryptographic key.
	/// </summary>
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

		public byte[] P { get; set; } = new byte[0];
		public byte[] Q { get; set; } = new byte[0];
		public byte[] DP { get; set; } = new byte[0];
		public byte[] DQ { get; set; } = new byte[0];
		public byte[] IQ { get; set; } = new byte[0];
		public byte[] D { get; set; } = new byte[0];
		/// <summary>
		/// The bit length of the private key components.
		/// </summary>
		public int BitLength { get; set; } = 0;
		/// <summary>
		/// The exponent of the public key.
		/// </summary>
		public int PublicExponent { get; set; } = 0;
		/// <summary>
		/// The RSA modulus.
		/// </summary>
		public byte[] Modulus { get; set; } = new byte[0];

		public override void Clear()
		{
			BitLength = 0;
			D = new byte[0];
			DP = new byte[0];
			DQ = new byte[0];
			IQ = new byte[0];
			Modulus = new byte[0];
			P = new byte[0];
			PublicExponent = 0;
			Q = new byte[0];
		}

		public override void CopyTo(ObjectModel where)
		{
			RSAKeyObjectModel clone = (where as RSAKeyObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			clone.BitLength = BitLength;
			clone.D = D;
			clone.DP = DP;
			clone.DQ = DQ;
			clone.IQ = IQ;
			clone.Modulus = Modulus;
			clone.P = P;
			clone.PublicExponent = PublicExponent;
			clone.Q = Q;
		}
	}
}
