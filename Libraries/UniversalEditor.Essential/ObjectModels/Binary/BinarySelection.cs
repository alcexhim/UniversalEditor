//
//  BinarySelection.cs
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
using System.Text;

namespace UniversalEditor.ObjectModels.Binary
{
	public class BinarySelection : Selection
	{
		public BinarySelection()
		{
		}
		public BinarySelection(byte[] content, int start = 0, int length = 0)
		{
			Content = content;
			Start = start;
			Length = length;
		}

		public int Start { get; set; } = 0;
		public int Length { get; set; } = 0;

		private object _Content = null;
		public override object Content { get => _Content; set => _Content = value; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Content is byte[])
			{
				byte[] data = (byte[])Content;
				for (int i = 0; i < data.Length; i++)
				{
					sb.Append(data[i].ToString("x").PadRight(2, '0'));
					if (i < data.Length - 1)
					{
						sb.Append(' ');
					}
				}
			}
			return sb.ToString();
		}

		protected override void DeleteInternal()
		{
			_Content = null;
		}
	}
}
