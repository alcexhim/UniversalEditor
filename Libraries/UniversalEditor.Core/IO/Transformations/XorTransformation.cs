//
//  XorTransform.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker
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
namespace UniversalEditor.IO.Transformations
{
	public class XorTransformation : Transformation
	{
		/// <summary>
		/// Gets or sets a key used to encrypt or decrypt the stream.
		/// </summary>
		/// <remarks>Pronounced "zorkie", like Sonny Eclipse ;)</remarks>
		/// <value>The key used to encrypt or decrypt this stream, or <see langword="null"/> if no key is needed.</value>
		public byte[] XorKey { get; set; } = null;

		public int XorKeyIndex { get; set; } = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UniversalEditor.IO.Transformations.XorTransformation"/> class.
		/// </summary>
		/// <param name="xorkey">The key used to encrypt or decrypt this stream, or <see langword="null"/> if no key is needed.</param>
		public XorTransformation(byte[] xorkey = null)
		{
			XorKey = xorkey;
			XorKeyIndex = 0;
		}

		protected override byte[] TransformInternal(byte[] input)
		{
			if (XorKey == null) return input;
			for (int i = 0; i < input.Length; i++)
			{
				if (XorKeyIndex >= XorKey.Length)
					XorKeyIndex = 0;

				input[i] ^= XorKey[XorKeyIndex];
			}
			return input;
		}
	}
}
