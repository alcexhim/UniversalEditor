using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igActor : igNamedObject
	{
		[Flags]
		public enum igActorFlags : uint
		{
			None = 0x00000000
		}

		public string Name { get; set; } = String.Empty;
		public igBase Bound { get; set; }
		public igAppearance Appearance { get; set; }
		public igAnimationDatabase AnimationDatabase { get; set; }
		public igAnimationSystem AnimationSystem { get; set; }
		public igList ChildList { get; set; }
		public igBase BoneMatrixCacheArray { get; set; }
		public igBase BlendMatrixCacheArray { get; set; }
		public igAnimationModifierList ModifierList { get; internal set; }

		public igActorFlags Flags { get; set; } = 0;
		public float[] Transform { get; set; } = new float[16];
	}
}
