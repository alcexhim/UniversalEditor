using System;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Waveform.FLAC.Internal
{
	public enum FLACMetadataBlockType
	{
		Unknown = -1,
		StreamInfo,
		Padding,
		Application,
		SeekTable,
		VorbisComment,
		Cuesheet,
		Picture,
		Invalid = 127
	}
}
