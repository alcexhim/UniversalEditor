using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public static class HostApplication
	{
		private static IHostApplicationWindow mvarCurrentWindow = null;
		public static IHostApplicationWindow CurrentWindow { get { return mvarCurrentWindow; } set { mvarCurrentWindow = value; } }

		private static HostApplicationOutputWindow mvarOutputWindow = new HostApplicationOutputWindow();
		public static HostApplicationOutputWindow OutputWindow { get { return mvarOutputWindow; } set { mvarOutputWindow = value; } }

		private static HostApplicationMessage.HostApplicationMessageCollection mvarMessages = new HostApplicationMessage.HostApplicationMessageCollection();
		public static HostApplicationMessage.HostApplicationMessageCollection Messages { get { return mvarMessages; } }
	}
}
