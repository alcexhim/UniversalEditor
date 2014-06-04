using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Bootstrapper
{
	static class Program
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
				MessageBox.Show("No engines are available to launch this application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
	}
}
