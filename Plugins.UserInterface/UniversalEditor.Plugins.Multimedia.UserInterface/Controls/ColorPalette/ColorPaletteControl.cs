//
//  ColorPaletteControl.cs
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Controls.ColorPalette
{
	public class ColorPaletteControl : CustomControl
	{
		public event EventHandler SelectionChanged;
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			SelectionChanged?.Invoke(this, e);
		}

		public PaletteEntry.PaletteEntryCollection Entries { get; set; } = new PaletteEntry.PaletteEntryCollection();

		public Dimension2D PaletteEntrySize { get; set; } = new Dimension2D(64, 24);
		public double ZoomFactor { get; set; } = 1.0;

		private int GetLineWidth()
		{
			return (int)((double)Size.Width / (PaletteEntrySize.Width * ZoomFactor));
		}


		public PaletteEntry HitTest(Vector2D location)
		{
			int x = 0, y = 0, w = (int)(PaletteEntrySize.Width * ZoomFactor), h = (int)(PaletteEntrySize.Height * ZoomFactor);
			y = (int)VerticalAdjustment.Value;


			int startIndex = (int)(VerticalAdjustment.Value / h) * GetLineWidth(), endIndex = Entries.Count - 1;
			int startLine = startIndex / w;
			y = (int)VerticalAdjustment.Value;

			int zcount = 0;
			for (int i = startIndex; i <= endIndex; i++)
			{
				if (x > (int)(Size.Width - w))
				{
					x = 0;
					y += h;
				}

				Rectangle rect = new Rectangle(x + paletteSpacingX, y + paletteSpacingY, w - paletteSpacingX - paletteSpacingX, h - paletteSpacingY - paletteSpacingY);

				if (rect.Contains(location))
					return Entries[i];

				x += w;
			}
			return null;
		}
		public PaletteEntry HitTest(double x, double y)
		{
			return HitTest(new Vector2D(x, y));
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();

			PaletteEntry entry = HitTest(e.Location);
			SelectedEntry = entry;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			int x = 0, y = 0, w = (int)(PaletteEntrySize.Width * ZoomFactor), h = (int)(PaletteEntrySize.Height * ZoomFactor);

			double shscrh = Entries.Count / GetLineWidth();
			ScrollBounds = new Dimension2D(0, (shscrh * h) + h);

			int startIndex = (int)(VerticalAdjustment.Value / h) * GetLineWidth(), endIndex = Entries.Count - 1;
			int startLine = startIndex / w;
			y = (int)VerticalAdjustment.Value;

			int zcount = 0;
			for (int i = startIndex; i <= endIndex; i++)
			{
				if (x > (int)(Size.Width - w))
				{
					x = 0;
					y += h;
				}

				if (y > (int)(VerticalAdjustment.Value + Size.Height + h + h))
				{
					// we're done for now
					return;
				}

				Rectangle rect = new Rectangle(x + paletteSpacingX, y + paletteSpacingY, w - paletteSpacingX - paletteSpacingX, h - paletteSpacingY - paletteSpacingY);

				PaletteEntry entry = Entries[i];
				if (entry.Color.A < 1.0)
				{
					// only fill the alpha background if we need to
					DrawAlphaBackground(e.Graphics, rect);
				}
				e.Graphics.FillRectangle(new SolidBrush(entry.Color), rect);
				if (entry == SelectedEntry)
				{
					e.Graphics.DrawRectangle(new Pen(SystemColors.HighlightBackground, new Measurement(2, MeasurementUnit.Pixel)), new Rectangle(x, y, w, h));
				}
				zcount++;
				x += w;
			}
		}
		private TextureBrush AlphaBackgroundBrush = null;
		private void DrawAlphaBackground(Graphics g, Rectangle rect)
		{
			// this is too slow for now, do absolutely nothing
			return;

			/*
			if (AlphaBackgroundBrush == null)
			{
				Image AlphaBackgroundImage = Image.Create(24, 24);
				Graphics g = Graphics.FromImage(AlphaBackgroundImage);
				g.FillRectangle(Brushes.White, new Rectangle(0, 0, 24, 24));
				g.FillRectangle(Brushes.Black, new Rectangle(16, 16, 16, 16));

				AlphaBackgroundBrush = new TextureBrush(AlphaBackgroundImage);
			}
			*/
			int qs = 0;
			for (int patternY = 0; patternY < rect.Height; patternY += 8)
			{
				for (int patternX = qs; patternX < rect.Width - qs; patternX += 8)
				{
					g.FillRectangle(Brushes.Black, new Rectangle(rect.X + patternX, rect.Y + patternY, 4, 4));
				}
				if (qs == 0)
				{
					qs = 8;
				}
				else
				{
					qs = 0;
				}
			}
			// e.Graphics.FillRectangle(AlphaBackgroundBrush, rect);
		}

		int paletteSpacingX = 2;
		int paletteSpacingY = 2;

		private PaletteEntry _SelectedEntry = null;
		public PaletteEntry SelectedEntry { get { return _SelectedEntry; } set { bool changed = (_SelectedEntry != value); _SelectedEntry = value; if (changed) { Refresh(); OnSelectionChanged(EventArgs.Empty); } } }
	}
}
