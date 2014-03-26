using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private Color mvarColor = Color.Empty;
        public Color Color { get { return mvarColor; } set { mvarColor = value; } }

        public object Clone()
        {
            PaletteEntry clone = new PaletteEntry();
            clone.Name = mvarName;
            clone.Color = mvarColor;
            return clone;
        }

        private bool mvarSelected = false;
        public bool Selected { get { return mvarSelected; } set { mvarSelected = value; } }
    }
}
