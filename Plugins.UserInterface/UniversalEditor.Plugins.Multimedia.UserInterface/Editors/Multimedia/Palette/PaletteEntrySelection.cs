//
//  PaletteEntrySelection.cs - provides an EditorSelection for manipulating the current selection in a PaletteObjectModel
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

using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette
{
	/// <summary>
	/// Provides an <see cref="EditorSelection" /> for manipulating the current selection in a <see cref="PaletteObjectModel" />.
	/// </summary>
	public class PaletteEntrySelection : EditorSelection
	{
		public PaletteEntrySelection(PaletteEditor editor, PaletteEntry value)
			: base(editor)
		{
			Value = value;
		}
		public PaletteEntry Value { get; set; } = null;

		public override object Content { get => Value; set => Value = (value as PaletteEntry); }

		protected override void DeleteInternal()
		{
			PaletteObjectModel palette = (Editor.ObjectModel as PaletteObjectModel);
			if (Value != null)
			{
				palette.Entries.Remove(Value);
			}
		}
	}
}
