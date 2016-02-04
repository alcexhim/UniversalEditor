using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Package.ContentTypes
{
	public class ContentTypesObjectModel : ObjectModel
	{
		private ContentType.ContentTypeCollection mvarContentTypes = new ContentType.ContentTypeCollection();
		public ContentType.ContentTypeCollection ContentTypes { get { return mvarContentTypes; } }

		public override void Clear()
		{
			mvarContentTypes.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ContentTypesObjectModel clone = (where as ContentTypesObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (ContentType item in mvarContentTypes)
			{
				clone.ContentTypes.Add(item.Clone() as ContentType);
			}
		}
	}
}
