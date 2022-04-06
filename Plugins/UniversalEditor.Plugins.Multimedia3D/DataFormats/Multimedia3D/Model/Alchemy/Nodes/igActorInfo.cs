using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igActorInfo : igInfo
	{
		public igActor Actor { get; set; } = null;
		public igActorList ActorList { get; set; } = null;
		public igAnimationDatabase AnimationDatabase { get; set; }
		public igAppearanceList AppearanceList { get; set; }
		public igAnimationCombinerList CombinerList { get; set; }

	}
}
