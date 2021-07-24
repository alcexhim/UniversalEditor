using System;
using System.Collections.Generic;
using System.Reflection;
using UniversalEditor.UserInterface;

namespace UniversalEditor.ConsoleBootstrapper
{
	public class Program : EditorApplication
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			int exitCode = (new Program()).Start();
			if (exitCode != 0)
			{
				ConsoleColor oldcolor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("ERROR: No engines are available to launch this application.");
				Console.ForegroundColor = oldcolor;

				Pause();
			}
		}

		private static void Pause()
		{
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}
