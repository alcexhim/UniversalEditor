using System;
using Gtk;

namespace UniversalEditor.Engines.GTK
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
		
		protected override void StopApplicationInternal ()
		{
			Application.Quit ();
		}
		
		protected override UniversalEditor.UserInterface.IHostApplicationWindow OpenWindowInternal (params string[] FileNames)
		{
			MainWindow mw = new MainWindow();
			mw.Show ();
			return mw;
		}
		
		public override void ShowAboutDialog()
		{
			Dialogs.AboutDialog dlg = new Dialogs.AboutDialog();
			dlg.Run();
			dlg.Destroy();
		}
	}
}

