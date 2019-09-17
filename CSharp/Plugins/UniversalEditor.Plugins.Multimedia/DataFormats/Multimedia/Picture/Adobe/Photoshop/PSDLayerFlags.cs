//
//  PSDLayerFlags.cs
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.Adobe.Photoshop
{
	[Flags()]
	public enum PSDLayerFlags
	{
		TransparencyProtected = 0x0,
		Visible = 0x1,
		Obsolete = 0x2,
		Photoshop5OrLater = 0x4,
		PixelDataIrrelevantToAppearanceOfDocument = 0x8 // wtf
	}
}
