using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.ThreeDStudio
{
	public class ThreeDStudioChunk
	{
		private ushort mvarID = 0;
		public ushort ID { get { return mvarID; } set { mvarID = value; } }
		private uint mvarLength = 0;
		public uint Length { get { return mvarLength; } set { mvarLength = value; } }
	}
}
