using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Palette
{
    public class PaletteObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        public override ObjectModelReference MakeReference()
        {
            if (_omr == null)
            {
                _omr = base.MakeReference();
                _omr.Title = "Color palette";
                _omr.Path = new string[] { "Multimedia", "Color palette" };
            }
            return _omr;
        }

        public override void Clear()
        {
            mvarEntries.Clear();
        }

        public override void CopyTo(ObjectModel where)
        {
            PaletteObjectModel clone = (where as PaletteObjectModel);
            foreach (PaletteEntry entry in mvarEntries)
            {
                clone.Entries.Add(entry.Clone() as PaletteEntry);
            }
        }

        private PaletteEntry.PaletteEntryCollection mvarEntries = new PaletteEntry.PaletteEntryCollection();
        public PaletteEntry.PaletteEntryCollection Entries { get { return mvarEntries; } }
    }
}
