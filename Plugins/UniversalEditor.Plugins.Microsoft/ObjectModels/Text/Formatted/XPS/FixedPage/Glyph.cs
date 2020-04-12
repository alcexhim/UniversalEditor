//
//  Glyph.cs - represents a FixedPage glyph in an XML Paper Specification (XPS) document
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedPage
{
	/// <summary>
	/// Represents a FixedPage glyph in an XML Paper Specification (XPS) document.
	/// </summary>
	public class Glyph : FixedPageItem
	{
		public Color FillColor { get; set; } = Color.Empty;
		public double FontRenderingEmSize { get; set; } = 16.0006;
		public XPSStyleSimulations StyleSimulations { get; set; } = XPSStyleSimulations.None;

		public double OriginX { get; set; } = 0.0;
		public double OriginY { get; set; } = 0.0;

		public string Text { get; set; } = String.Empty;
	}
}
