using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Package.ContentTypes
{
	public class ContentTypesObjectModel : ObjectModel
	{
		public DefaultDefinition.DefaultDefinitionCollection DefaultDefinitions { get; } = new DefaultDefinition.DefaultDefinitionCollection ();
		public OverrideDefinition.OverrideDefinitionCollection OverrideDefinitions { get; } = new OverrideDefinition.OverrideDefinitionCollection ();

		public ContentTypesObjectModel()
		{
			DefaultDefinitions.Add ("xml", "application/xml");
			DefaultDefinitions.Add ("rels", "application/vnd.openxmlformats-package.relationships+xml");
		}

		public override void Clear()
		{
			DefaultDefinitions.Clear();
			OverrideDefinitions.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ContentTypesObjectModel clone = (where as ContentTypesObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (DefaultDefinition item in DefaultDefinitions)
			{
				clone.DefaultDefinitions.Add(item.Clone() as DefaultDefinition);
			}
			foreach (OverrideDefinition item in OverrideDefinitions)
			{
				clone.OverrideDefinitions.Add(item.Clone() as OverrideDefinition);
			}
		}
	}
}
