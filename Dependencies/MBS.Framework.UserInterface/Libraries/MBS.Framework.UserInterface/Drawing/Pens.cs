using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;

namespace MBS.Framework.UserInterface.Drawing
{
	public static class Pens
	{
		public static Pen Black { get; } = new Pen(Colors.Black);
		public static Pen Green { get; } = new Pen(Colors.Green);
		public static Pen Red { get; } = new Pen(Colors.Red);
		public static Pen White { get; } = new Pen(Colors.White);
	}
}
