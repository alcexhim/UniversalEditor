using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface.WindowsForms
{
	public static class HostApplication
	{
		private static IHostApplicationWindow mvarCurrentWindow = null;
		public static IHostApplicationWindow CurrentWindow { get { return mvarCurrentWindow; } set { mvarCurrentWindow = value; } }

        private static HostApplicationOutputWindow mvarOutputWindow = new HostApplicationOutputWindow();
        public static HostApplicationOutputWindow OutputWindow { get { return mvarOutputWindow; } set { mvarOutputWindow = value; } }
	}
}
