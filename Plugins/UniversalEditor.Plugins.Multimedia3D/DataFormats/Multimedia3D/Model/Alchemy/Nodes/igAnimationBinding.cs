using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igAnimationBinding : igBase
	{
		private igSkeleton mvarSkeleton = null;
		public igSkeleton Skeleton { get { return mvarSkeleton; } set { mvarSkeleton = value; } }

		private uint mvarBindCount = 0;
		public uint BindCount { get { return mvarBindCount; } set { mvarBindCount = value; } }

		private uint mvarChainSwapList = 0;
		public uint ChainSwapList { get { return mvarChainSwapList; } set { mvarChainSwapList = value; } }

		private uint mvarReflectTrack = 0;
		public uint ReflectTrack { get { return mvarReflectTrack; } set { mvarReflectTrack = value; } }
	}
}
