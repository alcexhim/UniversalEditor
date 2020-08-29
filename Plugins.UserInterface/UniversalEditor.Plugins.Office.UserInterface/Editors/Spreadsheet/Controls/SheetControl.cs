//
//  SheetControl.cs
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
using System.Runtime.InteropServices;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.ObjectModels.Spreadsheet;

namespace UniversalEditor.Plugins.Office.UserInterface.Editors.Spreadsheet.Controls
{
	public class SheetControl : CustomControl
	{
		public Sheet Sheet { get; set; } = null;

		public const int MAX_ROWS = 20;
		public const int MAX_COLS = 20;

		protected override void OnPaint(PaintEventArgs e)
		{
			if (Sheet == null) return;

			SolidBrush bfg = new SolidBrush(SystemColors.WindowForeground);

			Rectangle rectColumnHeader = new Rectangle(0, 0, 0, 13);
			// draw the column headers first
			for (int c = 0; c < MAX_COLS; c++)
			{
				string title = Sheet.Columns.Contains(c) ? Sheet.Columns[c].Title : c.ToString();
				int colWidth = Sheet.Columns.Contains(c) ? Sheet.Columns[c].Width.GetValueOrDefault((Parent as SpreadsheetEditor).DefaultColumnWidth) : (Parent as SpreadsheetEditor).DefaultColumnWidth;
				rectColumnHeader.Width = colWidth;

				Rectangle rectColumnHeaderText = new Rectangle(rectColumnHeader.X, rectColumnHeader.Y, rectColumnHeader.Width, rectColumnHeader.Height);
				e.Graphics.DrawText(title, null, rectColumnHeaderText, bfg, HorizontalAlignment.Center, VerticalAlignment.Middle);

				rectColumnHeader.X += rectColumnHeader.Width;
			}

			Rectangle cellRect = new Rectangle(0, 0, (Parent as SpreadsheetEditor).DefaultColumnWidth, (Parent as SpreadsheetEditor).DefaultRowHeight);
			for (int r = 0; r < MAX_ROWS; r++)
			{
				int rowheight = (Parent as SpreadsheetEditor).DefaultRowHeight;
				if (Sheet.Rows.Contains(r))
					rowheight = Sheet.Rows[r].Height.GetValueOrDefault((Parent as SpreadsheetEditor).DefaultRowHeight);

				for (int c = 0; c < MAX_COLS; c++)
				{
					int colwidth = (Parent as SpreadsheetEditor).DefaultColumnWidth;
					if (Sheet.Columns.Contains(c))
						colwidth = Sheet.Columns[c].Width.GetValueOrDefault((Parent as SpreadsheetEditor).DefaultColumnWidth);

					e.Graphics.FillRectangle(new SolidBrush(Colors.White), cellRect);
					cellRect.X += colwidth + 1;

					if (r == 0)
					{
						e.Graphics.DrawLine(new Pen(Colors.DarkGray), cellRect.X - 1, 0, cellRect.X - 1, Size.Height);
					}
				}

				cellRect.X = 0;
				cellRect.Y += rowheight + 1;
				e.Graphics.DrawLine(new Pen(Colors.DarkGray), 0, cellRect.Y - 1, Size.Width, cellRect.Y - 1);
			}
		}
	}
}
