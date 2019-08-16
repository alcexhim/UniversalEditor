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

		protected override void BeforeSaveInternal (Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal (objectModels);

			RelationshipsObjectModel rels = (objectModels.Pop () as RelationshipsObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel ();

			MarkupTagElement tagRelationships = new MarkupTagElement ();
			tagRelationships.Attributes.Add ("xmlns", "http://schemas.openxmlformats.org/package/2006/relationships");
			tagRelationships.FullName = "Relationships";
			foreach (Relationship rel in rels.Relationships)
			{
				MarkupTagElement tagRelationship = new MarkupTagElement ();
				tagRelationship.FullName = "Relationship";
				tagRelationship.Attributes.Add ("Target", rel.Target);
				tagRelationship.Attributes.Add ("Id", rel.ID);
				tagRelationship.Attributes.Add ("Type", rel.Schema);
				tagRelationships.Elements.Add (tagRelationship);
			}

			mom.Elements.Add (tagRelationships);

			objectModels.Push (mom);
		}
	}
}
