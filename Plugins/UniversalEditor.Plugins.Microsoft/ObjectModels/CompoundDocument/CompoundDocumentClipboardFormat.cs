//
//  CompoundDocumentClipboardFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
namespace UniversalEditor.ObjectModels.CompoundDocument
{
	public struct CompoundDocumentClipboardFormat
	{
		private string _Str;
		private uint _Int;

		public static readonly CompoundDocumentClipboardFormat Empty;

		private CompoundDocumentClipboardFormat(uint _int)
		{
			_Int = _int;
			_Str = null;
		}
		private CompoundDocumentClipboardFormat(string _str)
		{
			_Str = _str;
			_Int = 0;
		}

		public bool IsStandard { get { return _Str == null && _Int != 0; } }
		public bool IsEmpty { get { return _Str == null && _Int == 0; } }

		public override string ToString()
		{
			if (_Str != null)
			{
				return _Str;
			}
			return _Int.ToString();
		}

		public static CompoundDocumentClipboardFormat FromStandard(uint index)
		{
			return new CompoundDocumentClipboardFormat(index);
		}
		public static CompoundDocumentClipboardFormat FromString(string format)
		{
			return new CompoundDocumentClipboardFormat(format);
		}
	}
}
