//
//  PaletteEditor.cs - provides a UWT-based Editor for a PaletteObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using System.Collections.Specialized;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Keyboard;

using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette.Dialogs;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.Panels;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for a <see cref="PaletteObjectModel" />.
	/// </summary>
	[ContainerLayout(typeof(PaletteEditor), "UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette.PaletteEditor.glade")]
	public class PaletteEditor : Editor
	{
		private Toolbar tb;
		private Container pnlNoColors;
		private Controls.ColorPalette.ColorPaletteControl cc;
		private Container pnlColorInfo;
		private TextBox txtColorName;
		private Button cmdAddColor;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Context.AttachCommandEventHandler("PaletteEditor_ContextMenu_Add", PaletteEditor_ContextMenu_Add_Click);
			Context.AttachCommandEventHandler("PaletteEditor_ContextMenu_Change", PaletteEditor_ContextMenu_Change_Click);
			Context.AttachCommandEventHandler("PaletteEditor_ContextMenu_Delete", PaletteEditor_ContextMenu_Delete_Click);
			Context.AttachCommandEventHandler("PaletteEditor_CalculateNeighboringColors", PaletteEditor_CalculateNeighboringColors_Click);

			cc.Visible = false;
			pnlNoColors.Visible = true;
			pnlColorInfo.Visible = false;

			(tb.Items["tsbColorAdd"] as ToolbarItemButton).Click += tsbColorAdd_Click;
			(tb.Items["tsbColorEdit"] as ToolbarItemButton).Click += tsbColorEdit_Click;
			(tb.Items["tsbColorRemove"] as ToolbarItemButton).Click += tsbColorRemove_Click;

			ContextMenuCommandID = "PaletteEditor_ContextMenu_Unselected";

			OnObjectModelChanged(e);
		}

		private void PaletteEditor_CalculateNeighboringColors_Click(object sender, EventArgs e)
		{
			if (SelectedEntry == null)
			{
				MessageDialog.ShowDialog("Please select a palette entry.", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return;

			CalculateNeighboringColorsDialog dlg = new CalculateNeighboringColorsDialog();
			dlg.SelectedColor = SelectedEntry.Color;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				int index = palette.Entries.IndexOf(SelectedEntry);

				for (int i = 0; i < dlg.Colors.Length; i++)
				{
					PaletteEntry entry = new PaletteEntry();
					entry.Color = dlg.Colors[i];

					palette.Entries.Insert(index, entry);
					cc.Entries.Insert(index, entry);
				}
			}
		}

		private void tsbColorAdd_Click(object sender, EventArgs e)
		{
			PaletteEditor_ContextMenu_Add_Click(sender, e);
		}
		private void tsbColorEdit_Click(object sender, EventArgs e)
		{
			PaletteEditor_ContextMenu_Change_Click(sender, e);
		}
		private void tsbColorRemove_Click(object sender, EventArgs e)
		{
			PaletteEditor_ContextMenu_Delete_Click(sender, e);
		}

		protected override Selection CreateSelectionInternal(object content)
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

		private PropertyPanelClass ppclasColor = new PropertyPanelClass("Color", new PropertyPanelProperty[] { new PropertyPanelProperty("Color", typeof(Color)) });

		private void PaletteEditor_ContextMenu_Add_Click(object sender, EventArgs e)
		{
			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return;

			ColorDialog dlg = new ColorDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();

				PaletteEntry entry = new PaletteEntry(dlg.SelectedColor);
				palette.Entries.Add(entry);
				cc.Entries.Add(entry);

				PropertyPanelObject ppobj = new PropertyPanelObject(entry.Name, ppclasColor);
				ppobj.SetExtraData<PaletteEntry>("content", entry);
				ppobj.Properties.Add(new PropertyPanelProperty("Color", typeof(Color), entry.Color));
				PropertiesPanel.Objects.Add(ppobj);


				EndEdit();

				pnlNoColors.Visible = false;
				pnlColorInfo.Visible = true;
				cc.Visible = true;

				cc.Refresh();
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
				cc.Entries.Remove(SelectedEntry);

				EndEdit();

				SelectedEntry = null;

				cc.Refresh();

				if (palette.Entries.Count == 0)
				{
					cc.Visible = false;
					pnlNoColors.Visible = true;
					pnlColorInfo.Visible = false;
				}
				else
				{
					cc.Visible = true;
					pnlNoColors.Visible = false;
					pnlColorInfo.Visible = true;
				}
			}
		}

		private int paletteEntryWidth = 64;
		private int paletteEntryHeight = 24;

		public double ZoomFactor
		{
			get { return cc.ZoomFactor; }
			set { cc.ZoomFactor = value; }
		}

		void txtColorName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == KeyboardKey.Enter)
			{
				if (SelectedEntry != null)
				{
					BeginEdit();
					SelectedEntry.Name = txtColorName.Text;
					EndEdit();
				}
			}
		}

		[EventHandler(nameof(cc), nameof(UserInterface.Controls.ColorPalette.ColorPaletteControl.SelectionChanged))]
		private void cc_SelectionChanged(object sender, EventArgs e)
		{
			SelectedEntry = cc.SelectedEntry;

			Selections.Clear();
			if (cc.SelectedEntry != null)
			{
				Selections.Add(new PaletteEntrySelection(cc.SelectedEntry));
			}
		}

		/*
		[EventHandler(nameof(cc), nameof(Control.KeyDown))]
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
		*/

		[EventHandler(nameof(cc), nameof(Control.MouseDoubleClick))]
		private void cc_MouseDoubleClick(object sender, MBS.Framework.UserInterface.Input.Mouse.MouseEventArgs e)
		{
			SelectedEntry = cc.HitTest(e.Location);
			if (SelectedEntry != null)
			{
				DisplayEntryProperties(SelectedEntry);
			}
		}

		[EventHandler(nameof(cmdAddColor), nameof(Control.Click))]
		private void cmdAddColor_Click(object sender, EventArgs e)
		{
			PaletteEditor_ContextMenu_Add_Click(sender, e);
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

				if (value != null)
				{
					ContextMenuCommandID = "PaletteEditor_ContextMenu_Selected";
				}
				else
				{
					ContextMenuCommandID = "PaletteEditor_ContextMenu_Unselected";
				}

				Selections.Clear();

				if (value != null)
				{
					Selections.Add(new PaletteEntrySelection(value));
				}

				System.ComponentModel.CancelEventArgs ce = new System.ComponentModel.CancelEventArgs();
				OnSelectionChanging(ce);
				if (ce.Cancel) return;

				_SelectedEntry = value;
				cc.SelectedEntry = _SelectedEntry;
				OnSelectionChanged(EventArgs.Empty);
				cc.Refresh();
			}
		}


		protected override void OnObjectModelChanged(EventArgs e)
		{
			if (!IsCreated) return;

			cc.Entries.Clear();

			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return;

			if (palette.Entries.Count > 0)
			{
				pnlNoColors.Visible = false;
				pnlColorInfo.Visible = true;
				cc.Visible = true;
			}
			else
			{
				pnlNoColors.Visible = true;
				pnlColorInfo.Visible = false;
				cc.Visible = false;
			}

			foreach (PaletteEntry entry in palette.Entries)
			{
				cc.Entries.Add(entry);
			}

			cc.Refresh();
		}


	}
}
