using System;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette
{
	public class PaletteEntrySelection : EditorSelection
	{
		public PaletteEntrySelection(PaletteEntry value)
		{
			Value = value;
		}
		public PaletteEntry Value { get; set; } = null;

		public override object Content { get => Value; set => Value = (value as PaletteEntry); }
	}
}
