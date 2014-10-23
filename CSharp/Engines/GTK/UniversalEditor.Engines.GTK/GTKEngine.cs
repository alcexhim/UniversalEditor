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
		public override bool ShowDataFormatOptionsDialog(ref DataFormat df, UniversalEditor.UserInterface.DataFormatOptionsDialogType type)
		{
			return Dialogs.DataFormatOptionsDialog.ShowDialog(ref df, type);
		}
		protected override void ShowCrashDialog(Exception ex)
		{
			Dialogs.CrashDialog dlg = new Dialogs.CrashDialog();
			dlg.Exception = ex;
			switch ((ResponseType)dlg.Run())
			{
				case ResponseType.Ok:
				{
					break;
				}
			}
			dlg.Destroy();
		}
	}
}

