using System;

using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.UWT
{
	public class UWTEngine : Engine
	{
		#region implemented abstract members of Engine
		protected override void ShowCrashDialog (Exception ex)
		{
			throw new NotImplementedException ();
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
			mw.Show ();
			return mw;
		}
		public override void ShowAboutDialog (DataFormatReference dfr)
		{
			throw new NotImplementedException ();
		}
		public override bool ShowCustomOptionDialog (ref CustomOption.CustomOptionCollection customOptions, string title = null, EventHandler aboutButtonClicked = null)
		{
			throw new NotImplementedException ();
		}
		#endregion
		


	}
}