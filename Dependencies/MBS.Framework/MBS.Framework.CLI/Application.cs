using System;
using System.Text;

namespace MBS.Framework.CLI
{
	public static class Application
	{
		private static CommandLineSwitch.CommandLineSwitchCollection _switchs = new CommandLineSwitch.CommandLineSwitchCollection();
		public static CommandLineSwitch.CommandLineSwitchCollection Switches
		{
			get
			{
				_switchs.Update();
				return _switchs;
			}
		}

		public static string ExecutableFileName
		{
			get { return System.Reflection.Assembly.GetEntryAssembly().Location; }
		}
		public static string ExecutableFileTitle
		{
			get { return System.IO.Path.GetFileName(ExecutableFileName); }
		}

		public static void Start()
		{
		}

		public static void PrintUsage()
		{
			StringBuilder sbSwitches = new StringBuilder();
			foreach (CommandLineSwitch sw in Switches)
			{
				if (sw.IsOptional)
					sbSwitches.Append('[');

				sbSwitches.Append(String.Format("/{0}", sw.Name));

				if (sw.CanHaveValue)
				{
					sbSwitches.Append(':');
					sbSwitches.Append(sw.ExampleValue == null ? "value": sw.ExampleValue);
				}

				if (sw.IsOptional)
					sbSwitches.Append(']');

				if (Switches.IndexOf(sw) < Switches.Count - 1)
				{
					sbSwitches.Append(' ');
				}
			}
			Console.WriteLine(String.Format("usage: {0} {1}", System.IO.Path.GetFileNameWithoutExtension(ExecutableFileTitle), sbSwitches.ToString()));
		}
	}
}