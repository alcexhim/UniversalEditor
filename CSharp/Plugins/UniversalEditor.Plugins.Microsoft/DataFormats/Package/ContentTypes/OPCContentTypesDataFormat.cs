using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Package.ContentTypes;

namespace UniversalEditor.DataFormats.Package.ContentTypes
{
	public class OPCContentTypesDataFormat : XMLDataFormat
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
			ContentTypesObjectModel types = (objectModels.Pop() as ContentTypesObjectModel);

			MarkupTagElement tagTypes = (mom.Elements["Types"] as MarkupTagElement);
			foreach (MarkupElement elType in tagTypes.Elements)
			{
				MarkupTagElement tagType = (elType as MarkupTagElement);
				if (elType == null) continue;

				DefaultDefinition type = new DefaultDefinition();
				MarkupAttribute attExtension = tagType.Attributes["Extension"];
				if (attExtension != null) type.Extension = attExtension.Value;

				MarkupAttribute attContentType = tagType.Attributes["ContentType"];
				if (attContentType != null) type.ContentType = attContentType.Value;

				switch (elType.FullName)
				{
					case "Default":
					{
						types.DefaultDefinitions.Add(type);
						break;
					}
				}
			}
		}

		protected override void BeforeSaveInternal (Stack<ObjectModel> objectModels)
		{
			MarkupObjectModel mom = new MarkupObjectModel ();
			ContentTypesObjectModel types = (objectModels.Pop () as ContentTypesObjectModel);

			MarkupTagElement tagTypes = new MarkupTagElement ();
			tagTypes.FullName = "Types";
			tagTypes.Attributes.Add ("xmlns", "http://schemas.openxmlformats.org/package/2006/content-types");

			foreach (DefaultDefinition type in types.DefaultDefinitions)
			{
				MarkupTagElement tagDefault = new MarkupTagElement ();
				tagDefault.FullName = "Default";
				tagDefault.Attributes.Add ("Extension", type.Extension);
				tagDefault.Attributes.Add ("ContentType", type.ContentType);
				tagTypes.Elements.Add (tagDefault);
			}
			foreach (OverrideDefinition def in types.OverrideDefinitions)
			{
				MarkupTagElement tagOverride = new MarkupTagElement ();
				tagOverride.FullName = "Override";
				tagOverride.Attributes.Add ("PartName", def.PartName);
				tagOverride.Attributes.Add ("ContentType", def.ContentType);
				tagTypes.Elements.Add (tagOverride);
			}

			mom.Elements.Add (tagTypes);

			objectModels.Push (mom);

			base.BeforeSaveInternal (objectModels);
		}
	}
}
