//
//  HexEditorPosition.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
namespace MBS.Framework.UserInterface.Controls.HexEditor
{
	public struct HexEditorPosition
	{
		public int ByteIndex;
		public int NybbleIndex;

		public HexEditorPosition(int byteIndex, int nybbleIndex)
		{
			ByteIndex = byteIndex;
			NybbleIndex = nybbleIndex;
		}

		public static HexEditorPosition operator ++(HexEditorPosition value)
		{
			if (value.NybbleIndex == 1)
			{
				return new HexEditorPosition(value.ByteIndex + 1, 0);
			}
			else if (value.NybbleIndex == 0)
			{
				return new HexEditorPosition(value.ByteIndex, 1);
			}
			return value;
		}
		public static HexEditorPosition operator --(HexEditorPosition value)
		{
			if (value.NybbleIndex == 1)
			{
				return new HexEditorPosition(value.ByteIndex, 0);
			}
			else if (value.NybbleIndex == 0)
			{
				return new HexEditorPosition(value.ByteIndex - 1, 1);
			}
			return value;
		}

		public static HexEditorPosition operator +(HexEditorPosition value, int p)
		{
			return new HexEditorPosition(value.ByteIndex + p, 0);
		}
		public static HexEditorPosition operator -(HexEditorPosition value, int p)
		{
			return new HexEditorPosition(value.ByteIndex - p, 0);
		}

		public static implicit operator int(HexEditorPosition value)
		{
			return value.ByteIndex;
		}
		public static implicit operator HexEditorPosition(int value)
		{
			return new HexEditorPosition(value, 0);
		}

		public static implicit operator double(HexEditorPosition value)
		{
			return value.ByteIndex + (0.5 * value.NybbleIndex);
		}
		public static implicit operator HexEditorPosition(double value)
		{
			return new HexEditorPosition((int)value, (value - (int)value > 0 ? 1 : 0));
		}
	}
}
