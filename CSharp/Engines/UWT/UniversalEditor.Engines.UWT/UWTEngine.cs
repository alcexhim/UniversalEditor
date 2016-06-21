using System;

using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.UWT
{
	public class UWTEngine : Engine
	{
		#region implemented abstract members of Engine
		protected override void ShowCrashDialog (Exception ex)
		{
			Console.WriteLine (ex.ToString ());
		}

		protected override void BeforeInitialization ()
		{
			UniversalWidgetToolkit.Application.Initialize();
		}
		
		protected override void MainLoop ()
		{
			UniversalWidgetToolkit.Application.Start ();
		}
		protected override IHostApplicationWindow OpenWindowInternal (params Document[] documents)
		{
			MainWindow mw = new MainWindow ();
			LastWindow = mw;
			mw.Show ();
			return mw;
		}
		public override void ShowAboutDialog (DataFormatReference dfr)
		{
			UniversalWidgetToolkit.Dialogs.AboutDialog dlg = new UniversalWidgetToolkit.Dialogs.AboutDialog ();
			dlg.ProgramName = "Universal Editor";
			dlg.Version = System.Reflection.Assembly.GetEntryAssembly ().GetName ().Version;
			dlg.Copyright = "(c) 1997-2016 Michael Becker";
			dlg.Comments = "A modular, extensible document editor";
			dlg.LicenseType = UniversalWidgetToolkit.LicenseType.GPL30;
			dlg.ShowDialog ();
		}
		public override bool ShowCustomOptionDialog (ref CustomOption.CustomOptionCollection customOptions, string title = null, EventHandler aboutButtonClicked = null)
		{
			throw new NotImplementedException ();
		}
		#endregion

		protected override void StopApplicationInternal ()
		{
			base.StopApplicationInternal ();

			UniversalWidgetToolkit.Application.Stop ();
		}


	}
}