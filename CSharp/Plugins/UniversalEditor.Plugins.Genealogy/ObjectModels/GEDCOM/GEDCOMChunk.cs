using System;
namespace UniversalEditor.Plugins.Genealogy.ObjectModels.GEDCOM
{
	public class GEDCOMChunk
	{
		public class GEDCOMChunkCollection
			: System.Collections.ObjectModel.Collection<GEDCOMChunk>
		{
		}

		public int Index { get; set; } = 0;
		public string Name { get; set; } = null;
		public string Value { get; set; } = null;

		public GEDCOMChunk.GEDCOMChunkCollection Chunks { get; } = new GEDCOMChunk.GEDCOMChunkCollection();
	}
}
