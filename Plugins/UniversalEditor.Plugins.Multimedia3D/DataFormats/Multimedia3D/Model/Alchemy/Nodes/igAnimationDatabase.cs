using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igAnimationDatabase : igInfo
	{
		public igSkeletonList SkeletonList { get; set; }
		public igAnimationList AnimationList { get; set; }
		public igSkinList SkinList { get; set; }
		public igAppearanceList AppearanceList { get; set; }
		public igAnimationCombinerList CombinerList { get; set; }
	}
}
