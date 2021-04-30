//
//  StringAccessor.cs - provide an Accessor for reading from/writing to a string
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

namespace UniversalEditor.Accessors
{
	public class StringAccessor : MemoryAccessor
	{
		private static AccessorReference _ar = null;
		protected override AccessorReference MakeReferenceInternal()
		{
			if (_ar == null)
			{
				_ar = base.MakeReferenceInternal();
				_ar.Visible = false;
			}
			return _ar;
		}

		public StringAccessor() : this(String.Empty)
		{

		}
		public StringAccessor(string value)
			: this(value, System.Text.Encoding.UTF8)
		{

		}

		public StringAccessor(string value, IO.Encoding encoding)
			: base(encoding.GetBytes(value))
		{

		}

		public StringAccessor(string value, System.Text.Encoding encoding)
			: base(encoding.GetBytes(value))
		{

		}
	}
}
