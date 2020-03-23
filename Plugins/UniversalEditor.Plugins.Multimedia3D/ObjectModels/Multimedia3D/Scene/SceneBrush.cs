using System;
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia3D.Scene
{
	public class SceneBrush
	{
		public class SceneBrushCollection
			: System.Collections.ObjectModel.Collection<SceneBrush>
		{
		}

		public PositionVector3 Position { get; set; } = PositionVector3.Empty;
		public Dimension3D Size { get; set; } = Dimension3D.Empty;
	}
}
