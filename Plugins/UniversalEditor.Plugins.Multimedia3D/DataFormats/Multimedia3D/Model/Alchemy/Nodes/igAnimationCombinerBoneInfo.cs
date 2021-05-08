using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igAnimationCombinerBoneInfo : igBase
	{
		private igAnimationState mvarAnimationState = null;
		public igAnimationState AnimationState { get { return mvarAnimationState; } set { mvarAnimationState = value; } }

		// transform source?

		private PositionVector4 mvarConstantQuaternion = new PositionVector4();
		public PositionVector4 ConstantQuaternion { get { return mvarConstantQuaternion; } set { mvarConstantQuaternion = value; } }

		private PositionVector3 mvarConstantTranslation = new PositionVector3();
		public PositionVector3 ConstantTranslation { get { return mvarConstantTranslation; } set { mvarConstantTranslation = value; } }

		private uint mvarPriority = 0;
		public uint Priority { get { return mvarPriority; } set { mvarPriority = value; } }

		private uint mvarAnimationDrivenChannels = 0;
		public uint AnimationDrivenChannels { get { return mvarAnimationDrivenChannels; } set { mvarAnimationDrivenChannels = value; } }

		private uint mvarReflect = 0;
		public uint Reflect { get { return mvarReflect; } set { mvarReflect = value; } }
	}
}
