using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public static class HostApplication
	{
		private static IHostApplicationWindow mvarCurrentWindow = null;
		/// <summary>
		/// Gets or sets the current window of the host application.
		/// </summary>
		public static IHostApplicationWindow CurrentWindow { get { return mvarCurrentWindow; } set { mvarCurrentWindow = value; } }

		private static HostApplicationOutputWindow mvarOutputWindow = new HostApplicationOutputWindow();
		/// <summary>
		/// Gets or sets the output window of the host application, where other plugins can read from and write to.
		/// </summary>
		public static HostApplicationOutputWindow OutputWindow { get { return mvarOutputWindow; } set { mvarOutputWindow = value; } }

		private static HostApplicationMessage.HostApplicationMessageCollection mvarMessages = new HostApplicationMessage.HostApplicationMessageCollection();
		/// <summary>
		/// A collection of messages to display in the Error List panel.
		/// </summary>
		public static HostApplicationMessage.HostApplicationMessageCollection Messages { get { return mvarMessages; } }
	}
}
