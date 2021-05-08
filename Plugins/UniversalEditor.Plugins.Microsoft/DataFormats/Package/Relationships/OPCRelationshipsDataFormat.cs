//
//  OPCRelationshipsDataFormat.cs - provides a DataFormat to manipulate Microsoft Open Packaging Convention package relationship definition files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Package.Relationships;

namespace UniversalEditor.DataFormats.Package.Relationships
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Microsoft Open Packaging Convention package relationship definition files.
	/// </summary>
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
