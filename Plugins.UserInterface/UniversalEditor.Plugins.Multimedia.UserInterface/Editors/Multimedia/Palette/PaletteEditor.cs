//
//  PaletteEditor.cs
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

using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette
{
	public partial class PaletteEditor : Editor
	{
		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
		public override void UpdateSelections()
		{
			if (Selections.Count == 1)
			{
				_SelectedEntry = (Selections[0] as PaletteEntrySelection).Value;
			}
			else
			{
				_SelectedEntry = null;
			}
			Refresh();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(PaletteObjectModel));
			}
			return _er;
		}

		public PaletteEditor()
		{
			InitializeComponent();
		}

		private void PaletteEditor_ContextMenu_Add_Click(object sender, EventArgs e)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return;

			ColorDialog dlg = new ColorDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();
				palette.Entries.Add(new PaletteEntry(dlg.SelectedColor));
				EndEdit();
			}
		}
		private void PaletteEditor_ContextMenu_Change_Click(object sender, EventArgs e)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null || SelectedEntry == null) return;

			ColorDialog dlg = new ColorDialog();
			dlg.SelectedColor = SelectedEntry.Color;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();
				SelectedEntry.Color = dlg.SelectedColor;
				EndEdit();

				cc.Refresh();
			}
		}
		private void PaletteEditor_ContextMenu_Delete_Click(object sender, EventArgs e)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null || SelectedEntry == null) return;

			if (MessageDialog.ShowDialog("Do you wish to delete the selected color?", "Delete Selected Color", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) == DialogResult.Yes)
			{
				BeginEdit();
				palette.Entries.Remove(SelectedEntry);
				EndEdit();

				SelectedEntry = null;

				cc.Refresh();
			}
		}

		private int paletteEntryWidth = 64;
		private int paletteEntryHeight = 24;

		private double _ZoomFactor = 1.0;
		public double ZoomFactor
		{
			get
			{
				return _ZoomFactor;
			}
			set
			{
				_ZoomFactor = value;
				cc.Refresh();
			}
		}

		public PaletteEntry HitTest(Vector2D location)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return null;

			int x = 0, y = 0, w = (int)(paletteEntryWidth * ZoomFactor), h = (int)(paletteEntryHeight * ZoomFactor);
			y = (int)cc.VerticalAdjustment.Value;


			int startIndex = (int)(cc.VerticalAdjustment.Value / h) * GetLineWidth(), endIndex = palette.Entries.Count - 1;
			int startLine = startIndex / w;
			y = (int)cc.VerticalAdjustment.Value;

			int zcount = 0;
			for (int i = startIndex; i <= endIndex; i++)
			{
				if (x > (int)(cc.Size.Width - w))
				{
					x = 0;
					y += h;
				}

				Rectangle rect = new Rectangle(x + paletteSpacingX, y + paletteSpacingY, w - paletteSpacingX - paletteSpacingX, h - paletteSpacingY - paletteSpacingY);

				if (rect.Contains(location))
					return palette.Entries[i];

				x += w;
			}
			return null;
		}
		public PaletteEntry HitTest(double x, double y)
		{
			return HitTest(new Vector2D(x, y));
		}

		void txtColorName_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
		{
			if (e.Key == MBS.Framework.UserInterface.Input.Keyboard.KeyboardKey.Enter)
			{
				if (SelectedEntry != null)
				{
					BeginEdit();
					SelectedEntry.Name = txtColorName.Text;
					EndEdit();
				}
			}
		}

		private void cc_MouseDown(object sender, MBS.Framework.UserInterface.Input.Mouse.MouseEventArgs e)
		{
			cc.Focus();

			PaletteEntry entry = HitTest(e.Location);
			if (entry != null)
			{
				SelectedEntry = entry;
			}
		}

		private void cc_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case KeyboardKey.Delete:
				{
					PaletteEditor_ContextMenu_Delete_Click(sender, e);
					e.Cancel = true;
					break;
				}
				case KeyboardKey.Enter:
				{
					DisplayEntryProperties(SelectedEntry);
					e.Cancel = true;
					break;
				}
				case KeyboardKey.Plus:
				case KeyboardKey.Add:
				{
					if ((e.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Control)
					{
						ZoomFactor += 0.05;
						e.Cancel = true;
					}
					break;
				}
				case KeyboardKey.Minus:
				case KeyboardKey.Subtract:
				{
					if ((e.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Control)
					{
						ZoomFactor -= 0.05;
						e.Cancel = true;
					}
					break;
				}
				case KeyboardKey.D0:
				case KeyboardKey.NumPad0:
				{
					if ((e.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Control)
					{
						ZoomFactor = 1.0;
						e.Cancel = true;
					}
					break;
				}
				case KeyboardKey.ArrowUp:
				case KeyboardKey.ArrowLeft:
				case KeyboardKey.ArrowDown:
				case KeyboardKey.ArrowRight:
				{
					PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
					if (palette.Entries.Count <= 0) return;

					if (SelectedEntry == null)
					{
						SelectedEntry = palette.Entries[0];
						return;
					}

					int lineWidth = GetLineWidth();

					int index = palette.Entries.IndexOf(SelectedEntry);

					if (e.Key == KeyboardKey.ArrowLeft)
					{
						index--;

						if (index < 0)
							index = 0;
					}
					else if (e.Key == KeyboardKey.ArrowRight)
					{
						index++;

						if (index >= palette.Entries.Count)
							index = palette.Entries.Count - 1;
					}
					if (e.Key == KeyboardKey.ArrowDown)
					{
						index += lineWidth;

						if (index >= palette.Entries.Count)
							index = palette.Entries.Count - 1;
					}
					else if (e.Key == KeyboardKey.ArrowUp)
					{
						index -= lineWidth;

						if (index < 0)
							index = 0;
					}

					SelectedEntry = palette.Entries[index];
					e.Cancel = true;
					break;
				}
			}
		}

		private int GetLineWidth()
		{
			return (int)((double)cc.Size.Width / (paletteEntryWidth * ZoomFactor));
		}

		private void cc_MouseDoubleClick(object sender, MBS.Framework.UserInterface.Input.Mouse.MouseEventArgs e)
		{
			SelectedEntry = HitTest(e.Location);
			if (SelectedEntry != null)
			{
				DisplayEntryProperties(SelectedEntry);
			}
		}

		public void DisplayEntryProperties(PaletteEntry entry)
		{
			if (entry == null)
				return;

			ColorDialog dlg = new ColorDialog();
			dlg.SelectedColor = entry.Color;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();
				entry.Color = dlg.SelectedColor;
				EndEdit();
			}
		}

		public event System.ComponentModel.CancelEventHandler SelectionChanging;
		protected virtual void OnSelectionChanging(System.ComponentModel.CancelEventArgs e)
		{
			if (SelectedEntry != null)
			{
				SelectedEntry.Name = txtColorName.Text;
			}
			SelectionChanging?.Invoke(this, e);
		}

		public event EventHandler SelectionChanged;
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (SelectedEntry != null)
			{
				txtColorName.Text = SelectedEntry.Name;
			}
			SelectionChanged?.Invoke(this, e);
		}

		private PaletteEntry _SelectedEntry = null;
		public PaletteEntry SelectedEntry
		{
			get { return _SelectedEntry; }
			set
			{
				bool changed = (_SelectedEntry != value);
				if (!changed) return;

				if (SelectedEntry != null)
				{
					ContextMenuCommandID = "PaletteEditor_ContextMenu_Selected";
				}
				else
				{
					ContextMenuCommandID = "PaletteEditor_ContextMenu_Unselected";
				}

				Selections.Clear();
				Selections.Add(new PaletteEntrySelection(this, value));

				System.ComponentModel.CancelEventArgs ce = new System.ComponentModel.CancelEventArgs();
				OnSelectionChanging(ce);
				if (ce.Cancel) return;

				_SelectedEntry = value;
				OnSelectionChanged(EventArgs.Empty);
				cc.Refresh();
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

		private void cc_Paint(object sender, PaintEventArgs e)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return;

			int x = 0, y = 0, w = (int)(paletteEntryWidth * ZoomFactor), h = (int)(paletteEntryHeight * ZoomFactor);

			double shscrh = palette.Entries.Count / GetLineWidth();
			cc.ScrollBounds = new Dimension2D(0, (shscrh * h) + h);

			int startIndex = (int)(cc.VerticalAdjustment.Value / h) * GetLineWidth(), endIndex = palette.Entries.Count - 1;
			int startLine = startIndex / w;
			y = (int)cc.VerticalAdjustment.Value;

			int zcount = 0;
			for (int i = startIndex; i <= endIndex;  i++)
			{
				if (x > (int)(cc.Size.Width - w))
				{
					x = 0;
					y += h;
				}

				if (y > (int)(cc.VerticalAdjustment.Value + cc.Size.Height + h + h))
				{
					// we're done for now
					return;
				}

				Rectangle rect = new Rectangle(x + paletteSpacingX, y + paletteSpacingY, w - paletteSpacingX - paletteSpacingX, h - paletteSpacingY - paletteSpacingY);

				PaletteEntry entry = palette.Entries[i];
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

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
			Refresh();
		}

		
	}
}
