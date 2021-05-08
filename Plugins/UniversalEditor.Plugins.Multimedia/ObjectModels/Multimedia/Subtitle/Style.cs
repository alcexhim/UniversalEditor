//
//  Style.cs - describes the visual appearance for a subtitle
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

namespace UniversalEditor.ObjectModels.Multimedia.Subtitle
{
	/// <summary>
	/// Describes the visual appearance for a subtitle.
	/// </summary>
	public class Style : ICloneable
	{
		public class StyleCollection
			: System.Collections.ObjectModel.Collection<Style>
		{
		}
		public string Name { get; set; } = String.Empty;
		public string FontName { get; set; } = String.Empty;
		public int FontSize { get; set; } = 32;

		public Color PrimaryColor { get; set; } = Color.Empty;
		public Color SecondaryColor { get; set; } = Color.Empty;
		public Color OutlineColor { get; set; } = Color.Empty;
		public Color BackgroundColor { get; set; } = Color.Empty;

		public bool Bold { get; set; } = false;
		public bool Italic { get; set; } = false;
		public bool Underline { get; set; } = false;
		public bool Strikethrough { get; set; } = false;
		public double ScaleX { get; set; } = 1.0;
		public double ScaleY { get; set; } = 1.0;
		public int Spacing { get; set; } = 0;
		public int Angle { get; set; } = 0;
		public int BorderStyle { get; set; } = 1;
		public int OutlineWidth { get; set; } = 2;
		public int ShadowWidth { get; set; } = 2;
		public int Alignment { get; set; } = 2;
		public int MarginLeft { get; set; } = 10;
		public int MarginRight { get; set; } = 10;
		public int MarginVertical { get; set; } = 10;
		public int Encoding { get; set; } = 1;

		public object Clone()
		{
			Style clone = new Style();
			clone.Alignment = Alignment;
			clone.Angle = Angle;
			clone.BackgroundColor = BackgroundColor;
			clone.Bold = Bold;
			clone.BorderStyle = BorderStyle;
			clone.Encoding = Encoding;
			clone.FontName = (FontName.Clone() as string);
			clone.FontSize = FontSize;
			clone.Italic = Italic;
			clone.MarginLeft = MarginLeft;
			clone.MarginRight = MarginRight;
			clone.MarginVertical = MarginVertical;
			clone.Name = (Name.Clone() as string);
			clone.OutlineColor = OutlineColor;
			clone.OutlineWidth = OutlineWidth;
			clone.PrimaryColor = PrimaryColor;
			clone.ScaleX = ScaleX;
			clone.ScaleY = ScaleY;
			clone.SecondaryColor = SecondaryColor;
			clone.ShadowWidth = ShadowWidth;
			clone.Spacing = Spacing;
			clone.Strikethrough = Strikethrough;
			clone.Underline = Underline;
			return clone;
		}
	}
}
