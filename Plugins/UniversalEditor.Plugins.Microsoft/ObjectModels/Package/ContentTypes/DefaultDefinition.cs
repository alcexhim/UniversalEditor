using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Package.ContentTypes
{
	public class DefaultDefinition : ICloneable
	{
		public class DefaultDefinitionCollection
			: System.Collections.ObjectModel.Collection<DefaultDefinition>
		{
			public DefaultDefinition Add (string extension, string contentType)
			{
				DefaultDefinition item = new DefaultDefinition ();
				item.Extension = extension;
				item.ContentType = contentType;
				Add (item);
				return item;
			}
		}

		public string Extension { get; set; } = String.Empty;
		public string ContentType { get; set; } = String.Empty;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("*.");
			sb.Append(Extension);
			sb.Append("; ");
			sb.Append(ContentType);
			return sb.ToString();
		}

		public object Clone()
		{
			DefaultDefinition clone = new DefaultDefinition();
			clone.ContentType = (ContentType.Clone() as string);
			clone.Extension = (Extension.Clone() as string);
			return clone;
		}
	}
}
