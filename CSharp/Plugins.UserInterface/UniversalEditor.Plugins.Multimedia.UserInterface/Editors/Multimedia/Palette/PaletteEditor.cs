﻿//
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
			throw new NotImplementedException();
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
					SelectedEntry.Name = txtColorName.Text;
			}
		}

		private void cc_MouseDown(object sender, MBS.Framework.UserInterface.Input.Mouse.MouseEventArgs e)
		{
			SelectedEntry = HitTest(e.Location);
			Refresh();
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
				System.ComponentModel.CancelEventArgs ce = new System.ComponentModel.CancelEventArgs();
				OnSelectionChanging(ce);
				if (ce.Cancel) return;
				_SelectedEntry = value;
				OnSelectionChanged(EventArgs.Empty);
			}
		}

		private void cc_Paint(object sender, PaintEventArgs e)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return;

			int x = 0, y = 0, w = 64, h = 24;

			foreach (PaletteEntry entry in palette.Entries)
			{
				e.Graphics.FillRectangle(new SolidBrush(entry.Color), new Rectangle(x + 2, y + 2, w - 4, h - 4));
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
