using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Palette
{
	public class PaletteObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
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
			clone.Name = (Name.Clone() as string);
			for (int i = 0; i < mvarEntries.Count; i++)
			{
				clone.Entries.Add(mvarEntries[i].Clone() as PaletteEntry);
			}
		}

		public string Name { get; set; } = String.Empty;

		private PaletteEntry.PaletteEntryCollection mvarEntries = new PaletteEntry.PaletteEntryCollection();
		public PaletteEntry.PaletteEntryCollection Entries { get { return mvarEntries; } }
	}
}
