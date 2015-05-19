using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.CoreObject
{
	public class CoreObjectObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Core Object";
			}
			return _omr;
		}

		private CoreObjectGroup.CoreObjectGroupCollection mvarGroups = new CoreObjectGroup.CoreObjectGroupCollection();
		public CoreObjectGroup.CoreObjectGroupCollection Groups { get { return mvarGroups; } }

		private CoreObjectProperty.CoreObjectPropertyCollection mvarProperties = new CoreObjectProperty.CoreObjectPropertyCollection();
		public CoreObjectProperty.CoreObjectPropertyCollection Properties { get { return mvarProperties; } }

		public override void Clear()
		{
			mvarGroups.Clear();
			mvarProperties.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			CoreObjectObjectModel clone = (where as CoreObjectObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (CoreObjectGroup item in mvarGroups)
			{
				clone.Groups.Add(item.Clone() as CoreObjectGroup);
			}
			foreach (CoreObjectProperty item in mvarProperties)
			{
				clone.Properties.Add(item.Clone() as CoreObjectProperty);
			}
		}
	}
}
