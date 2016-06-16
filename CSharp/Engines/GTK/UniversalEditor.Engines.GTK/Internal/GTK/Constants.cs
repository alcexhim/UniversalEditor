using System;

namespace UniversalEditor.Engines.GTK.Internal.GTK
{
	internal static class Constants
	{
		public enum GApplicationFlags
		{
			None = 0,
			IsService  =          (1 << 0),
			IsLauncher =          (1 << 1),

			HandlesOpen =         (1 << 2),
			HandlesCommandLine = (1 << 3),
			SendEnvironment    =  (1 << 4),

			NonUnique =           (1 << 5)
		}

		public enum GtkWindowType
		{
			TopLevel = 0,
			Popup = 1
		}
		public enum GtkBoxOrientation
		{
			Horizontal = 0,
			Vertical = 1
		}
	}
}

