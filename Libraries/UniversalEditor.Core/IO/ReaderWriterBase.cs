//
//  ReaderWriterBase.cs - common methods for implementing Reader and Writer
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.IO
{
    public abstract class ReaderWriterBase
    {
        private Endianness mvarEndianness = Endianness.LittleEndian;
        public Endianness Endianness { get { return mvarEndianness; } set { mvarEndianness = value; } }

        public void SwapEndianness()
        {
            if (mvarEndianness == Endianness.LittleEndian)
            {
                mvarEndianness = Endianness.BigEndian;
            }
            else
            {
                mvarEndianness = Endianness.LittleEndian;
            }
        }

        private bool mvarReverse = false;
        public bool Reverse { get { return mvarReverse; } }

        private Accessor mvarAccessor = null;
        public Accessor Accessor { get { return mvarAccessor; } }

        private NewLineSequence mvarNewLineSequence = NewLineSequence.Default;
        public NewLineSequence NewLineSequence { get { return mvarNewLineSequence; } set { mvarNewLineSequence = value; } }
        public string GetNewLineSequence()
        {
            string newline = System.Environment.NewLine;
            switch (mvarNewLineSequence)
            {
                case IO.NewLineSequence.CarriageReturn:
                {
                    newline = "\r";
                    break;
                }
                case IO.NewLineSequence.LineFeed:
                {
                    newline = "\n";
                    break;
                }
                case IO.NewLineSequence.CarriageReturnLineFeed:
                {
                    newline = "\r\n";
                    break;
                }
                case IO.NewLineSequence.LineFeedCarriageReturn:
                {
                    newline = "\n\r";
                    break;
                }
            }
            return newline;
        }

        public ReaderWriterBase(Accessor accessor)
        {
            this.mvarAccessor = accessor;
            this.mvarEndianness = Endianness.LittleEndian;
            this.mvarReverse = false;
		}

		/// <summary>
		/// Aligns the <see cref="Reader" /> to the specified number of bytes. If the current
		/// position of the <see cref="Reader" /> is not a multiple of the specified number of bytes,
		/// the position will be increased by the amount of bytes necessary to bring it to the
		/// aligned position.
		/// </summary>
		/// <param name="alignTo">The number of bytes on which to align the <see cref="Reader"/>.</param>
		/// <param name="extraPadding">Any additional padding bytes that should be included after aligning to the specified boundary.</param>
		public void Align(int alignTo, int extraPadding = 0)
		{
			long paddingCount = ((alignTo - (Accessor.Position % alignTo)) % alignTo);
			paddingCount += extraPadding;

			if (Accessor.Position == Accessor.Length)
			{
				Accessor.Writer.WriteBytes(new byte[paddingCount]);
			}
			else
			{
				Accessor.Position += paddingCount;
			}
		}

		public long CalculateAlignment(long currentPosition, long alignTo, long extraPadding = 0)
		{
			long paddingCount = ((alignTo - (currentPosition % alignTo)) % alignTo);
			paddingCount += extraPadding;
			return paddingCount;
		}
	}
}
