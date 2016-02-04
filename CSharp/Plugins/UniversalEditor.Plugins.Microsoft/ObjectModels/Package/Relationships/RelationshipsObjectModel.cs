using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Package.Relationships
{
	public class RelationshipsObjectModel : ObjectModel
	{
		private Relationship.RelationshipCollection mvarRelationships = new Relationship.RelationshipCollection();
		public Relationship.RelationshipCollection Relationships { get { return mvarRelationships; } }

		public override void Clear()
		{
			mvarRelationships.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			RelationshipsObjectModel clone = (where as RelationshipsObjectModel);
			foreach (Relationship item in mvarRelationships)
			{
				clone.Relationships.Add(item.Clone() as Relationship);
			}
		}
	}
}
