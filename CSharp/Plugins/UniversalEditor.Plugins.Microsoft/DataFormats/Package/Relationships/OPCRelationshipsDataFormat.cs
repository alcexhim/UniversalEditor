using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Package.Relationships;

namespace UniversalEditor.DataFormats.Package.Relationships
{
	public class OPCRelationshipsDataFormat : XMLDataFormat
	{
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			RelationshipsObjectModel rels = (objectModels.Pop() as RelationshipsObjectModel);

			MarkupTagElement tagRelationships = (mom.Elements["Relationships"] as MarkupTagElement);
			foreach (MarkupElement elRelationship in tagRelationships.Elements)
			{
				MarkupTagElement tagRelationship = (elRelationship as MarkupTagElement);
				if (tagRelationship == null) continue;
				if (tagRelationship.FullName != "Relationship") continue;

				Relationship rel = new Relationship();

				MarkupAttribute attTarget = tagRelationship.Attributes["Target"];
				if (attTarget != null) rel.Target = attTarget.Value;

				MarkupAttribute attID = tagRelationship.Attributes["Id"];
				if (attID != null) rel.ID = attID.Value;

				MarkupAttribute attType = tagRelationship.Attributes["Type"];
				if (attType != null) rel.Schema = attType.Value;

				rels.Relationships.Add(rel);
			}
		}
	}
}
