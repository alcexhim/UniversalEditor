using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Video;
namespace UniversalEditor.DataFormats.Multimedia.Video.RAD.Smacker
{
	public class SmackerDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("RAD Video Tools Smacker", new byte?[][] { new byte?[] { new byte?(83), new byte?(77), new byte?(75), new byte?(50) }, new byte?[] { new byte?(83), new byte?(77), new byte?(75), new byte?(52) } }, new string[] { "*.smk" });
			dfr.Capabilities.Add(typeof(VideoObjectModel), DataFormatCapabilities.All);
			dfr.Sources.Add("http://wiki.multimedia.cx/index.php?title=Smacker");
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(4);
			int width = br.ReadInt32();
			int height = br.ReadInt32();
			int frames = br.ReadInt32();
			int frameRate = br.ReadInt32();
			if (frameRate > 0)
			{
				int framesPerSecond = 1000 / frameRate;
			}
			else
			{
				if (frameRate < 0)
				{
					int framesPerSecond = 100000 / -frameRate;
				}
			}
			int flags = br.ReadInt32();
			int[] audioSize = br.ReadInt32Array(7);
			int TreesSize = br.ReadInt32();
			int MMap_Size = br.ReadInt32();
			int MClr_Size = br.ReadInt32();
			int Full_Size = br.ReadInt32();
			int Type_Size = br.ReadInt32();
			int[] audioRate = br.ReadInt32Array(7);
			int dummy = br.ReadInt32();
			int[] frameSizes = br.ReadInt32Array(frames);
			byte[] frameTypes = br.ReadBytes(frames);
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
