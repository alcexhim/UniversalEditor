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

				Refresh();
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

				Refresh();
			}
		}

		public PaletteEntry HitTest(Vector2D location)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return null;

			int x = 0, y = 0, w = 64, h = 24;

			foreach (PaletteEntry entry in palette.Entries)
			{
				Rectangle rect = new Rectangle(x, y, w, h);
				x += w;

				if (x > (int)(Size.Width - w))
				{
					x = 0;
					y += h;
				}
				if (y > (int)(Size.Height - h))
					return null;

				if (rect.Contains(location))
					return entry;
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
			SelectedEntry = HitTest(e.Location);
			Refresh();

			if (SelectedEntry != null)
			{
				ContextMenuCommandID = "PaletteEditor_ContextMenu_Selected";
			}
			else
			{
				ContextMenuCommandID = "PaletteEditor_ContextMenu_Unselected";
			}
		}

		private void cc_MouseDoubleClick(object sender, MBS.Framework.UserInterface.Input.Mouse.MouseEventArgs e)
		{
			SelectedEntry = HitTest(e.Location);
			if (SelectedEntry != null)
			{
				ColorDialog dlg = new ColorDialog();
				dlg.SelectedColor = SelectedEntry.Color;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					BeginEdit();
					SelectedEntry.Color = dlg.SelectedColor;
					EndEdit();
				}
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

				Selections.Clear();
				Selections.Add(new PaletteEntrySelection(value));

				System.ComponentModel.CancelEventArgs ce = new System.ComponentModel.CancelEventArgs();
				OnSelectionChanging(ce);
				if (ce.Cancel) return;
				_SelectedEntry = value;
				OnSelectionChanged(EventArgs.Empty);
			}
		}

		private TextureBrush AlphaBackgroundBrush = null;
		private void DrawAlphaBackground(Graphics g, Rectangle rect)
		{
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

		private void cc_Paint(object sender, PaintEventArgs e)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return;

			int x = 0, y = 0, w = 64, h = 24;
			foreach (PaletteEntry entry in palette.Entries)
			{
				Rectangle rect = new Rectangle(x + 2, y + 2, w - 4, h - 4);
				if (entry.Color.A < 1.0)
				{
					// only fill the alpha background if we need to
					DrawAlphaBackground(e.Graphics, rect);
				}
				e.Graphics.FillRectangle(new SolidBrush(entry.Color), rect);
				if (entry == SelectedEntry)
				{
					e.Graphics.DrawRectangle(new Pen(Colors.LightSteelBlue, new Measurement(2, MeasurementUnit.Pixel)), new Rectangle(x, y, w, h));
				}
				x += w;

				if (x > (int)(Size.Width - w))
				{
					x = 0;
					y += h;
				}
				if (y > (int)(Size.Height - h))
					return;
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
			Refresh();
		}

		
	}
}
