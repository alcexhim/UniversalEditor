using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GIO
{
	internal class Structures
	{
		public struct GActionEntry
		{
			public string name;
			public Delegates.ActivateDelegate activate;
			public string parameter_type;
			public string state;
			public Delegates.ChangeStateDelegate change_state;
			public uint padding1;
			public uint padding2;
			public uint padding3;
		}
	}
}

