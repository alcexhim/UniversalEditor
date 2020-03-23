using System;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GObject
{
	internal static class Constants
	{
		public enum GConnectFlags
		{
			None = 0,

			/// <summary>
			/// Indicates that the handler should be called after rather than before the default handler of the signal.
			/// </summary>
			ConnectAfter = 1 << 0,
			/// <summary>
			/// Indicates that the instance and data should be swapped when calling the handler.
			/// </summary>
			ConnectSwapped = 1 << 1
		}
	}
}

