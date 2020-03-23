using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Layouts;

namespace MBS.Framework.UserInterface.TestProject
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Initialize ();

			Application.Engine.SetProperty("Windowless", true);

			Theming.ThemeManager.CurrentTheme = Theming.ThemeManager.GetByID(new Guid("{4D86F538-E277-4E6F-9CAC-60F82D49A19D}"));

            Application.ConfigurationFileNameFilter = "*.uwtxml";
			Application.Activated += Application_Activated;
			int nExitCode = Application.Start();
		}

		static void Application_Activated(object sender, ApplicationActivatedEventArgs e)
		{
			MainWindow window = new MainWindow();
			window.Show();
		}

	}
}
