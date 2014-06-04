using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface.WindowsForms
{
	public static class PerspectiveManager
	{
		private static Perspective.PerspectiveCollection mvarPerspectives = new Perspective.PerspectiveCollection();
		public static Perspective.PerspectiveCollection Perspectives { get { return mvarPerspectives; } }
	}
}
