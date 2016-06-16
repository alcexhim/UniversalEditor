using System;

using UniversalEditor;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.GTK
{
	public class GTKEngine : Engine
	{
		protected override void InitializeInternal ()
		{
			base.InitializeInternal ();

			string[] argv = System.Environment.GetCommandLineArgs ();
			int argc = argv.Length;

			bool check = GtkApplication.Initialize (ref argc, ref argv);
		}
		protected override void MainLoop ()
		{
			GtkApplication.Run ();
		}
		protected override IHostApplicationWindow OpenWindowInternal (params Document[] documents)
		{
			MainWindow mw = new MainWindow ();
			mw.Show ();
			return mw;
		}
		protected override void ShowCrashDialog (Exception ex)
		{
			throw new NotImplementedException ();
		}
		public override void ShowAboutDialog (DataFormatReference dfr)
		{
			throw new NotImplementedException ();
		}
		public override bool ShowCustomOptionDialog (ref CustomOption.CustomOptionCollection customOptions, string title, EventHandler aboutButtonClicked)
		{
			throw new NotImplementedException ();
		}
	}
}

