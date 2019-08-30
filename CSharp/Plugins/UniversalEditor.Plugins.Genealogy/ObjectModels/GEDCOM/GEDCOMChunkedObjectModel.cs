using System;
namespace UniversalEditor.Plugins.Genealogy.ObjectModels.GEDCOM
{
	public class GEDCOMChunkedObjectModel : ObjectModel
	{
		public GEDCOMChunk.GEDCOMChunkCollection Chunks { get; } = new GEDCOMChunk.GEDCOMChunkCollection();

		public override void Clear()
		{
		}

		public override void CopyTo(ObjectModel where)
		{
		}
	}
}
