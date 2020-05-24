//
//  BINComponentType.cs
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
namespace UniversalEditor.DataFormats.NWCSceneLayout.NewWorldComputing.BIN
{
	public enum BINComponentType
	{
		None = 0x00,
		Border = 0x01,
		Button = 0x02,
		Label = 0x08,
		Image = 0x10,

		TextBox = 0x0201,
		DropDownList = 0x0203,
		TextBox2 = 0x0204,
		ListBox = 0x0205
	}
}
