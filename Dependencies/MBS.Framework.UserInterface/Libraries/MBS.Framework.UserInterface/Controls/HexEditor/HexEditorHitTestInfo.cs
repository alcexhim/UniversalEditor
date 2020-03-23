//
//  HexEditorHitTestInfo.cs
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
	public class HexEditorHitTestInfo
	{
		public int ByteIndex { get; private set; } = -1;
		public int NybbleIndex { get; private set; } = -1;
		public HexEditorHitTestSection Section { get; private set; } = HexEditorHitTestSection.Hexadecimal;

		internal HexEditorHitTestInfo(int byteIndex = -1, int nybbleIndex = -1, HexEditorHitTestSection section = HexEditorHitTestSection.Hexadecimal)
		{
			ByteIndex = byteIndex;
			NybbleIndex = nybbleIndex;
			Section = section;
		}
	}
}
