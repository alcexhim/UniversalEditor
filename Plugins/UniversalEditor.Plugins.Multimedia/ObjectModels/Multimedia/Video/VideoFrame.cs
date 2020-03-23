using System;
using System.Collections.ObjectModel;
using UniversalEditor.ObjectModels.Multimedia.Picture;
namespace UniversalEditor.ObjectModels.Multimedia.Video
{
	public class VideoFrame : ICloneable
	{
		public class VideoFrameCollection : Collection<VideoFrame>
		{
		}
		private PictureObjectModel mvarObjectModel = new PictureObjectModel();
		public PictureObjectModel ObjectModel
		{
			get
			{
				return this.mvarObjectModel;
			}
			set
			{
				this.mvarObjectModel = value;
			}
		}
		public object Clone()
		{
			return new VideoFrame
			{
				ObjectModel = this.mvarObjectModel.Clone() as PictureObjectModel
			};
		}
	}
}
