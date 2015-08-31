using System;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.FLAC.Internal
{
	public class FLACMetadataBlockHeader
	{
		private bool mvarIsLastMetadataBlock = false;
		private FLACMetadataBlockType mvarBlockType = FLACMetadataBlockType.Unknown;
		private int mvarContentLength = 0;
		public bool IsLastMetadataBlock
		{
			get
			{
				return this.mvarIsLastMetadataBlock;
			}
			set
			{
				this.mvarIsLastMetadataBlock = value;
			}
		}
		public FLACMetadataBlockType BlockType
		{
			get
			{
				return this.mvarBlockType;
			}
			set
			{
				this.mvarBlockType = value;
			}
		}
		public int ContentLength
		{
			get
			{
				return this.mvarContentLength;
			}
			set
			{
				this.mvarContentLength = value;
			}
		}
	}
}
