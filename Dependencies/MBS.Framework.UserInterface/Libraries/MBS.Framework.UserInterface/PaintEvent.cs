using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface
{
	public delegate void PaintEventHandler(object sender, PaintEventArgs e);
	public class PaintEventArgs
	{
		public bool Handled { get; set; } = false;

		private Graphics mvarGraphics = null;
		public Graphics Graphics { get { return mvarGraphics; } }

		public PaintEventArgs(Graphics graphics)
		{
			mvarGraphics = graphics;
		}
	}
}
