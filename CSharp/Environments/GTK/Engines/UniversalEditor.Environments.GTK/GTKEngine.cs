using System;
using Gtk;

namespace UniversalEditor.Environments.GTK
{
	public class GTKEngine : UniversalEditor.UserInterface.Engine
	{
		protected override void MainLoop ()
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}

