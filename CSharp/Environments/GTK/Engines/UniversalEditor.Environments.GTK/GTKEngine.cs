using System;
using Gtk;

namespace UniversalEditor.Environments.GTK
{
	public class GTKEngine : UniversalEditor.UserInterface.Engine
	{
		protected override void BeforeInitialization ()
		{
			base.BeforeInitialization ();
			Application.Init ();
		}
		
		protected override void MainLoop ()
		{
			Application.Run ();
		}
		
		protected override UniversalEditor.UserInterface.IHostApplicationWindow OpenWindowInternal (params string[] FileNames)
		{
			MainWindow mw = new MainWindow();
			mw.Show ();
			return mw;
		}
	}
}

