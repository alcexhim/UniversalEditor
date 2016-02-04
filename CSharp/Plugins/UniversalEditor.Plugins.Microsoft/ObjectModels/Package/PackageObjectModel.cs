using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Package.ContentTypes;
using UniversalEditor.ObjectModels.Package.Relationships;

namespace UniversalEditor.ObjectModels.Package
{
	public class PackageObjectModel : ObjectModel
	{

		private ContentType.ContentTypeCollection mvarContentTypes = new ContentType.ContentTypeCollection();
		public ContentType.ContentTypeCollection ContentTypes { get { return mvarContentTypes; } }

		private Relationship.RelationshipCollection mvarRelationships = new Relationship.RelationshipCollection();
		public Relationship.RelationshipCollection Relationships { get { return mvarRelationships; } }

		public override void Clear()
		{
			mvarContentTypes.Clear();
			mvarRelationships.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			PackageObjectModel clone = (where as PackageObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (ContentType item in mvarContentTypes)
			{
				clone.ContentTypes.Add(item.Clone() as ContentType);
			}
			foreach (Relationship item in mvarRelationships)
			{
				clone.Relationships.Add(item.Clone() as Relationship);
			}
		}
	}
}
