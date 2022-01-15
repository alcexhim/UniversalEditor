//
//  BMLOpcode.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Markup.BML
{
	public enum BMLOpcode
	{
		None = 0,
		DocumentBegin = 0x01,
		Preprocessor = 0x02,
		TagBegin = 0x10,
		TagEnd = 0x1F,
		Attribute = 0x20,
		Literal = 0x21,
		Comment = 0x22,
		CDATA = 0x23,
		DocumentEnd = 0x0F
	}
}
