//
//  WMFFont.cs
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
namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile.Internal
{
	internal class WMFFont : WMFObject
	{
		private ushort height;
		private ushort width;
		private ushort escapement;
		private ushort orientation;
		private ushort weight;
		private bool italic;
		private bool underline;
		private bool strikeout;
		private WMFCharset charset;
		private WMFOutputPrecision precision;
		private WMFClipPrecision clipPrecision;
		private WMFFontQuality fontQuality;
		private WMFFontFamily family;
		private WMFFontPitch pitch;
		private string faceName;

		public WMFFont(ushort height, ushort width, ushort escapement, ushort orientation, ushort weight, bool italic, bool underline, bool strikeout, WMFCharset charset, WMFOutputPrecision precision, WMFClipPrecision clipPrecision, WMFFontQuality fontQuality, WMFFontFamily family, WMFFontPitch pitch, string faceName)
		{
			this.height = height;
			this.width = width;
			this.escapement = escapement;
			this.orientation = orientation;
			this.weight = weight;
			this.italic = italic;
			this.underline = underline;
			this.strikeout = strikeout;
			this.charset = charset;
			this.precision = precision;
			this.clipPrecision = clipPrecision;
			this.fontQuality = fontQuality;
			this.family = family;
			this.pitch = pitch;
			this.faceName = faceName;
		}
	}
}
