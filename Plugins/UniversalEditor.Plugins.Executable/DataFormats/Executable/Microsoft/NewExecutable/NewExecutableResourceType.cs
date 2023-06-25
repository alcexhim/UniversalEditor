//
//  NewExecutableResourceType.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
namespace UniversalEditor.DataFormats.Executable.Microsoft.NewExecutable
{
	public enum NewExecutableResourceType : ushort
	{
		Accelerator = 9,
		AnimatedCursor = 21,
		AnimatedIcon = 22,
		Bitmap = 2,
		Cursor = 1,
		Dialog = 5,
		DialogInclude = 17,
		DialogInit = 240,
		Font = 8,
		FontDir = 7,
		GroupCursor =12,
		GroupIcon = 14,
		HTML = 23,
		Icon = 3,
		Manifest = 24,
		Menu = 4,
		MessageTable = 11,
		PlugAndPlay = 19,
		RCData = 10,
		String = 6,
		Version = 16,
		VxD = 20
	}
}
