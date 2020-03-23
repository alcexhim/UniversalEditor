using System;
namespace UniversalEditor.ObjectModels.Package.ContentTypes
{
	public class OverrideDefinition : ICloneable
	{
		public class OverrideDefinitionCollection
			: System.Collections.ObjectModel.Collection<OverrideDefinition>
		{
		}

		public string PartName { get; set; } = String.Empty;
		public string ContentType { get; set; } = String.Empty;

		public object Clone ()
		{
			OverrideDefinition clone = new OverrideDefinition ();
			clone.PartName = (PartName.Clone() as string);
			clone.ContentType = (ContentType.Clone() as string);
			return clone;
		}
	}
}
