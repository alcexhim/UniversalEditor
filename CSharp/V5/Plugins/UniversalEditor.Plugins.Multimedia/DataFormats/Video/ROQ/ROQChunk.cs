using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Video.ROQ
{
	public class ROQChunk
	{
		public class ROQChunkCollection : Collection<ROQChunk>
		{
		}
		private short mvarID = 0;
		private short mvarArgument = 0;
		private byte[] mvarData = new byte[0];
		public short ID
		{
			get
			{
				return this.mvarID;
			}
			set
			{
				this.mvarID = value;
			}
		}
		public short Argument
		{
			get
			{
				return this.mvarArgument;
			}
			set
			{
				this.mvarArgument = value;
			}
		}
		public byte[] Data
		{
			get
			{
				return this.mvarData;
			}
			set
			{
				this.mvarData = value;
			}
		}
	}
}
