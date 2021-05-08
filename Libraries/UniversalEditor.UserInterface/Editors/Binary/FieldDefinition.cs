//
//  FieldDefinition.cs
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
using MBS.Framework.Drawing;

namespace UniversalEditor.Editors.Binary
{
	public class FieldDefinition
	{
		public string Name;
		public int Offset;
		public int Length;
		public Type DataType;
		public Color Color;

		public int DataTypeSize
		{
			get
			{
				int len = -1;
				if (DataType == typeof(sbyte) || DataType == typeof(byte))
				{
					len = 1;
				}
				else if (DataType == typeof(short) || DataType == typeof(ushort))
				{
					len = 2;
				}
				else if (DataType == typeof(int) || DataType == typeof(uint) || DataType == typeof(float))
				{
					len = 4;
				}
				else if (DataType == typeof(long) || DataType == typeof(ulong) || DataType == typeof(double) || DataType == typeof(Guid))
				{
					len = 8;
				}
				else if (DataType == typeof(string))
				{
					len = Length;
				}
				return len;
			}
		}
		public string DataTypeSizeString
		{
			get
			{
				if (DataTypeSize > -1)
					return DataTypeSize.ToString();
				return "*";
			}
		}
	}
}
