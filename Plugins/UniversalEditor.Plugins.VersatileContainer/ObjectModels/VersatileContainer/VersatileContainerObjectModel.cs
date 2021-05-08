using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.VersatileContainer
{
	public class VersatileContainerObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Versatile Container" };
			}
			return _omr;
		}

		private VersatileContainerSection.VersatileContainerSectionCollection mvarSections = new VersatileContainerSection.VersatileContainerSectionCollection();
		public VersatileContainerSection.VersatileContainerSectionCollection Sections { get { return mvarSections; } }

		private VersatileContainerProperty.VersatileContainerPropertyCollection mvarProperties = new VersatileContainerProperty.VersatileContainerPropertyCollection();
		public VersatileContainerProperty.VersatileContainerPropertyCollection Properties { get { return mvarProperties; } }

		public override void Clear()
		{
			mvarSections.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			VersatileContainerObjectModel clone = (where as VersatileContainerObjectModel);
			if (clone == null) return;

			foreach (VersatileContainerSection section in mvarSections)
			{
				clone.Sections.Add(section.Clone() as VersatileContainerSection);
			}
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
	}
}
