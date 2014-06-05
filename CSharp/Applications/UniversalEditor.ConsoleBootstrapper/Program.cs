using System;
using System.Collections.Generic;
using System.Reflection;
using UniversalEditor.UserInterface;

namespace UniversalEditor.ConsoleBootstrapper
{
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			string directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string[] libraries = System.IO.Directory.GetFiles(directory, "*.dll");
			List<Engine> engines = new List<Engine>();
			foreach (string library in libraries)
			{
				try
				{
					Assembly assembly = Assembly.LoadFile(library);
					Type[] types = null;
					try
					{
						types = assembly.GetTypes();
					}
					catch (ReflectionTypeLoadException ex)
					{
						types = ex.Types;
					}
					if (types == null)
					{
						continue;
					}

					foreach (Type type in types)
					{
						if (type.IsSubclassOf(typeof(Engine)))
						{
							Engine engine = (Engine)type.Assembly.CreateInstance(type.FullName);
							engines.Add(engine);
						}
					}
				}
				catch
				{

				}
			}

			if (engines.Count < 1)
			{
				ConsoleColor oldcolor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("ERROR: No engines are available to launch this application.");
				Console.ForegroundColor = oldcolor;

				Pause();
				return;
			}
			else if (engines.Count == 1)
			{
				engines[0].StartApplication();
			}
			else
			{
				engines[0].StartApplication();
			}
		}

		private static void Pause()
		{
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}
