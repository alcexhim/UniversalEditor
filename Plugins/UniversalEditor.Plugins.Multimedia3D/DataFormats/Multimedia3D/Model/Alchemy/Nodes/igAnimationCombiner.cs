using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igAnimationCombiner : igNamedObject
	{
		private igSkeleton mvarSkeleton = null;
		public igSkeleton Skeleton { get { return mvarSkeleton; } set { mvarSkeleton = value; } }

		private igAnimationCombinerBoneInfoListList mvarBoneInfoListList = null;
		public igAnimationCombinerBoneInfoListList BoneInfoListList { get { return mvarBoneInfoListList; } set { mvarBoneInfoListList = value; } }

		private igAnimationCombinerBoneInfoList mvarBoneInfoListBase = null;
		public igAnimationCombinerBoneInfoList BoneInfoListBase { get { return mvarBoneInfoListBase; } set { mvarBoneInfoListBase = value; } }

		private igAnimationStateList mvarAnimationStateList = null;
		public igAnimationStateList AnimationStateList { get { return mvarAnimationStateList; } set { mvarAnimationStateList = value; } }

		private igQuaternionfList mvarResultQuaternionArray = null;
		public igQuaternionfList ResultQuaternionArray { get { return mvarResultQuaternionArray; } set { mvarResultQuaternionArray = value; } }

		private ulong mvarCacheTime = 0;
		public ulong CacheTime { get { return mvarCacheTime; } set { mvarCacheTime = value; } }

		private int mvarCacheValid = 0;
		public int CacheValid { get { return mvarCacheValid; } set { mvarCacheValid = value; } }

		private ulong mvarAnimationStateTime = 0;
		public ulong AnimationStateTime { get { return mvarAnimationStateTime; } set { mvarAnimationStateTime = value; } }

		private int mvarLastCleanStateTime = 0;
		public int LastCleanStateTime { get { return mvarLastCleanStateTime; } set { mvarLastCleanStateTime = value; } }

		private ulong mvarCleanStateTimeThreshold = 0;
		public ulong CleanStateTimeThreshold { get { return mvarCleanStateTimeThreshold; } set { mvarCleanStateTimeThreshold = value; } }

		private int mvarCleanStatesTransitionMargin = 0;
		public int CleanStatesTransitionMargin { get { return mvarCleanStatesTransitionMargin; } set { mvarCleanStatesTransitionMargin = value; } }
	}
}
