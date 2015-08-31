using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia.Video
{
	public class VideoTrack : ICloneable
	{
		public class VideoTrackCollection : Collection<VideoTrack>
		{
		}
		private string mvarName = string.Empty;
		private VideoFrame.VideoFrameCollection mvarFrames = new VideoFrame.VideoFrameCollection();
		private int mvarFrameRate = 24;
		private int mvarBlockDimension = 8;
		private int mvarSubBlockDimension = 4;
		private int mvarWidth = 320;
		private int mvarHeight = 240;
		public string Name
		{
			get
			{
				return this.mvarName;
			}
			set
			{
				this.mvarName = value;
			}
		}
		public VideoFrame.VideoFrameCollection Frames
		{
			get
			{
				return this.mvarFrames;
			}
		}
		public int FrameRate
		{
			get
			{
				return this.mvarFrameRate;
			}
			set
			{
				this.mvarFrameRate = value;
			}
		}
		public int BlockDimension
		{
			get
			{
				return this.mvarBlockDimension;
			}
			set
			{
				this.mvarBlockDimension = value;
			}
		}
		public int SubBlockDimension
		{
			get
			{
				return this.mvarSubBlockDimension;
			}
			set
			{
				this.mvarSubBlockDimension = value;
			}
		}
		public int Width
		{
			get
			{
				return this.mvarWidth;
			}
			set
			{
				this.mvarWidth = value;
			}
		}
		public int Height
		{
			get
			{
				return this.mvarHeight;
			}
			set
			{
				this.mvarHeight = value;
			}
		}
		public object Clone()
		{
			return new VideoTrack
			{
				Name = this.mvarName, 
				Height = this.mvarHeight, 
				Width = this.mvarWidth, 
				BlockDimension = this.mvarBlockDimension, 
				FrameRate = this.mvarFrameRate, 
				SubBlockDimension = this.mvarSubBlockDimension
			};
		}
	}
}
