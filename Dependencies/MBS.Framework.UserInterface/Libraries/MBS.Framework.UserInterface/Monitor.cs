using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface
{
	public class Monitor
	{
		private string mvarDeviceName = String.Empty;
		public string DeviceName { get { return mvarDeviceName; } }
		public Rectangle Bounds { get; } = Rectangle.Empty;
		public Rectangle WorkingArea { get; } = Rectangle.Empty;

		public Monitor(string deviceName, Rectangle bounds, Rectangle workingArea)
		{
			mvarDeviceName = deviceName;
			Bounds = bounds;
			WorkingArea = workingArea;
		}

		public static Monitor[] Get()
		{
			return Application.Engine.GetMonitors();
		}
	}
}
