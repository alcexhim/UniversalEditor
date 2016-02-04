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

				ContentType type = new ContentType();
				MarkupAttribute attExtension = tagType.Attributes["Extension"];
				if (attExtension != null) type.Extension = attExtension.Value;

				MarkupAttribute attContentType = tagType.Attributes["ContentType"];
				if (attContentType != null) type.Value = attContentType.Value;

				switch (elType.FullName)
				{
					case "Default":
					{
						types.ContentTypes.Add(type);
						break;
					}
				}
			}
		}
	}
}
