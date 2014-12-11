using System;
using UniversalEditor.IO;
using UniversalEditor.DataFormats.Multimedia.Audio.Waveform.FLAC.Internal;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.FLAC
{
	public class FLACDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("Free Lossless Audio Codec", new byte?[][] { new byte?[] { new byte?(102), new byte?(76), new byte?(97), new byte?(67) } }, new string[] { "*.flac" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private FLACMetadataBlock.FLACMetadataBlockCollection mvarMetadataBlocks = new FLACMetadataBlock.FLACMetadataBlockCollection();
		public FLACMetadataBlock.FLACMetadataBlockCollection MetadataBlocks { get { return this.mvarMetadataBlocks; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
		private void SaveMetadataBlockHeader(Writer bw, FLACMetadataBlockHeader header)
		{
			byte flagAndType = 0;
			if (header.IsLastMetadataBlock)
			{
				flagAndType |= 1;
			}
			flagAndType |= (byte)header.BlockType;
			bw.WriteByte(flagAndType);
			bw.WriteInt24(header.ContentLength);
		}
		private void SaveMetadataBlockStreamInfo(Writer bw, FLACMetadataBlockStreamInfo block)
		{
			this.SaveMetadataBlockHeader(bw, new FLACMetadataBlockHeader
			{
				IsLastMetadataBlock = this.mvarMetadataBlocks.Count == 0, 
				BlockType = FLACMetadataBlockType.StreamInfo, 
				ContentLength = 0
			});
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("fLaC");
			FLACMetadataBlockStreamInfo streamInfo = new FLACMetadataBlockStreamInfo();
			this.SaveMetadataBlockStreamInfo(bw, streamInfo);
			bw.Flush();
		}
	}
}
