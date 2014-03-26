using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Chunked
{
	public class ChunkedObjectModel : ObjectModel
	{
		private ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Title = "Resource Interchange File Format (RIFF)";
				_omr.Path = new string[] { "Software Development", "Resource Interchange File Format (RIFF)" };
			}
			return _omr;
        }

#if UE_CHUNKED_RIFF_INCLUDE_METADATA

        private RIFFMetadataItem.RIFFMetadataItemCollection mvarInformation = new RIFFMetadataItem.RIFFMetadataItemCollection();
        public RIFFMetadataItem.RIFFMetadataItemCollection Information { get { return mvarInformation; } }

#endif
		
		private RIFFChunk.RIFFChunkCollection mvarChunks = new RIFFChunk.RIFFChunkCollection();
		public RIFFChunk.RIFFChunkCollection Chunks { get { return mvarChunks; } }

		public override void Clear()
		{
			mvarChunks.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ChunkedObjectModel clone = where as ChunkedObjectModel;
			foreach (RIFFChunk chunk in mvarChunks)
			{
				clone.Chunks.Add(chunk.Clone() as RIFFChunk);
			}
		}
	}
}
