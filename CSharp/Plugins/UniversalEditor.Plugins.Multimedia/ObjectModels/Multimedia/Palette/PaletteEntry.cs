using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.Palette
{
    public class PaletteEntry : ICloneable
    {
        public class PaletteEntryCollection
            : System.Collections.ObjectModel.Collection<PaletteEntry>
        {
            public PaletteEntry Add(Color color, string colorName = "")
            {
                PaletteEntry entry = new PaletteEntry();
                entry.Name = colorName;
                entry.Color = color;

                Add(entry);
                return entry;
            }
        }

		public PaletteEntry(Color color = default(Color), string name = "")
		{
			Name = name;
			Color = color;
		}

		private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

		public Color Color { get; set; } = Color.Empty;

        public object Clone()
        {
            PaletteEntry clone = new PaletteEntry();
            clone.Name = mvarName;
            clone.Color = Color;
            return clone;
        }

        private bool mvarSelected = false;
        public bool Selected { get { return mvarSelected; } set { mvarSelected = value; } }
    }
}
