using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.Controls.Multimedia.Palette
{
	[DefaultEvent("SelectionChanged")]
	public partial class ColorListControl : UserControl
	{
		public ColorListControl()
		{
			InitializeComponent();
		}

		private int mvarTileWidth = 32;
		public int TileWidth { get { return mvarTileWidth; } set { mvarTileWidth = value; } }

		private int mvarTileHeight = 32;
		public int TileHeight { get { return mvarTileHeight; } set { mvarTileHeight = value; } }

		private PaletteEntry.PaletteEntryCollection mvarEntries = new PaletteEntry.PaletteEntryCollection();
		public PaletteEntry.PaletteEntryCollection Entries { get { return mvarEntries; } }

		public Rectangle GetEntryBounds(PaletteEntry entry)
		{
			Rectangle rect = new Rectangle(0, 0, mvarTileWidth, mvarTileHeight);
			foreach (PaletteEntry color in mvarEntries)
			{
				if (color == entry) break;

				rect.X += mvarTileWidth;
				if (rect.Right >= this.Width)
				{
					rect.Y += mvarTileHeight;
					rect.X = 0;
				}
			}
			return rect;
		}

		public PaletteEntry SelectedColor
		{
			get
			{
				if (mvarSelectedIndex > -1 && mvarSelectedIndex < mvarEntries.Count)
				{
					return mvarEntries[mvarSelectedIndex];
				}
				return null;
			}
			set
			{
				if (!mvarEntries.Contains(value)) return;

				Rectangle prevRect = GetEntryBounds(mvarEntries[mvarSelectedIndex]);
				mvarSelectedIndex = mvarEntries.IndexOf(value);
				Rectangle rect = GetEntryBounds(mvarEntries[mvarSelectedIndex]);

				prevRect.X -= 2;
				prevRect.Y -= 2;
				prevRect.Width += 4;
				prevRect.Height += 4;

				Invalidate(prevRect);
				Invalidate(rect);

				OnSelectionChanged(EventArgs.Empty);
			}
		}

		public event EventHandler SelectionChanged;
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (SelectionChanged != null) SelectionChanged(this, e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle rect = new Rectangle(0, 0, mvarTileWidth, mvarTileHeight);
			foreach (PaletteEntry color in mvarEntries)
			{
				SolidBrush brush = new SolidBrush(color.Color.ToGdiColor());
				e.Graphics.FillRectangle(brush, rect);
				e.Graphics.DrawRectangle(Pens.Black, rect);

				if (mvarSelectedIndex == mvarEntries.IndexOf(color))
				{
					e.Graphics.DrawRectangle(new Pen(System.Drawing.Color.FromKnownColor(KnownColor.Highlight), 2), rect);
				}

				rect.X += mvarTileWidth;
				if (rect.Right >= this.Width)
				{
					rect.Y += mvarTileHeight;
					rect.X = 0;
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Refresh();
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				PaletteEntry color = HitTest(e.Location);
				if (color != null && color != mvarEntries[mvarSelectedIndex])
				{
					SelectedColor = color;
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			PaletteEntry color = HitTest(e.Location);
			if (color != null && mvarEntries.Contains(color) && color != mvarEntries[mvarSelectedIndex])
			{
				SelectedColor = color;
			}
		}

		private PaletteEntry HitTest(Point point)
		{
			Rectangle rect = new Rectangle(0, 0, mvarTileWidth, mvarTileHeight);
			foreach (PaletteEntry entry in mvarEntries)
			{
				if (rect.Contains(point))
				{
					return entry;
				}
				rect.X += mvarTileWidth;
				if (rect.Right >= this.Width)
				{
					rect.Y += mvarTileHeight;
					rect.X = 0;
				}
			}
			return null;
		}

		private int mvarSelectedIndex = 0;
		public int SelectedIndex { get { return mvarSelectedIndex; } set { mvarSelectedIndex = value; } }
	}
}
