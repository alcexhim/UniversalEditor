//
//  ChecksumModule.cs - provides the minimal functionality required to create a checksum calculation module
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Checksum
{
    /// <summary>
    /// Provides the minimal functionality required to create a checksum calculation module.
    /// </summary>
	public abstract class ChecksumModule
	{
        /// <summary>
        /// The name of this checksum calculation module.
        /// </summary>
		public abstract string Name { get; }

		private long mvarValue = 0;
        /// <summary>
        /// The current value of the checksum being calculated.
        /// </summary>
		public virtual long Value { get { return mvarValue; } protected set { mvarValue = value; } }

        /// <summary>
        /// Calculates the checksum based on the given input.
        /// </summary>
        /// <param name="input">The array of bytes used as input to the checksum calculation routine.</param>
        /// <returns>A <see cref="Int64" /> that represents the checksum of the given input.</returns>
		public long Calculate(byte[] input)
		{
			Reset();
			Update(input);
			return Value;
		}

		/// <summary>
		/// Resets the checksum as if no update was ever called.
		/// </summary>
		public virtual void Reset()
		{
			mvarValue = 0;
		}

		/// <summary>
		/// Updates the checksum with the bytes taken from the array.
		/// </summary>
		/// <param name="buffer">The array of bytes used as input to the checksum calculation routine.</param>
		public void Update(byte[] buffer)
		{
			if (buffer == null) throw new ArgumentNullException("buffer");
			Update(buffer, 0, buffer.Length);
		}

        /// <summary>
        /// Updates the checksum with a count of <see cref="count" /> bytes taken from the array beginning at offset
        /// <see cref="offset" />.
        /// </summary>
        /// <param name="buffer">The array of bytes used as input to the checksum calculation routine.</param>
        /// <param name="offset">The offset into <see cref="buffer" /> from which calculation starts.</param>
        /// <param name="count">The number of bytes to use in the checksum calculation.</param>
        public abstract void Update(byte[] buffer, int offset, int count);

        /// <summary>
        /// Calculates the checksum based on the given input.
        /// </summary>
        /// <param name="input">The <see cref="Int32" /> value used as input to the checksum calculation routine.</param>
        public abstract void Update(int input);
	}
}
