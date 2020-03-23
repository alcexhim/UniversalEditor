//
//  PSDBlendModeKey.cs
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
	public enum PSDBlendModeKey : int
	{
		// all values are in big-endian
		PassThrough = 1885434739, // 'pass'
		Normal = 1852797549, // 'norm'
		Dissolve = 1684632435, // 'diss'
		Darken = 1684107883, // 'dark'
		Multiply = 1836411936, // 'mul '
		ColorBurn = 1768188278, // 'idiv'
		LinearBurn = 1818391150, // 'lbrn'
		DarkerColor = 1684751212, // 'dkCl'
		Lighten = 1818850405, // 'lite'
		Screen = 1935897198, // 'scrn'
		ColorDodge = 1684633120, // 'div '
		LinearDodge = 1818518631, // 'lddg'
		LighterColor = 1818706796, // 'lgCl'
		Overlay = 1870030194, // 'over'
		SoftLight = 1934387572, // 'sLit'
		HardLight = 1749838196, // 'hLit'
		VividLight = 1984719220, // 'vLit'
		LinearLight = 1816947060, // 'lLit'
		PinLight = 1884055924, // 'pLit'
		HardMix = 1749903736, // 'hMix'
		Difference = 1684629094, // 'diff'
		Exclusion = 1936553316, // 'smud'
		Subtraction = 1718842722, // 'fsub'
		Divide = 1717856630, // 'fdiv'
		Hue = 1752524064, // 'hue '
		Saturation = 1935766560, // 'sat '
		Color = 1668246642, // 'colr'
		Luminosity = 1819634976 // 'lum '
	}
}
