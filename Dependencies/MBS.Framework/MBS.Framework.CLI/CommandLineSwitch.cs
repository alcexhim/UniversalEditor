using System;

namespace MBS.Framework.CLI
{
	public class CommandLineSwitch
	{
		public class CommandLineSwitchCollection
			: System.Collections.ObjectModel.Collection<CommandLineSwitch>
		{
			private bool updated = false;
			public void Update()
			{
				if (updated) return;

				string[] args = Environment.GetCommandLineArgs();
				for (int i = 1; i < args.Length; i++)
				{
					string arg = args[i];


					if (arg.StartsWith("/"))
					{
						CommandLineSwitch sw = this[arg];
						if (sw == null)
							sw = new CommandLineSwitch();

						if (arg.Contains(":"))
						{
							string[] values = arg.Split(new char[] { ':' }, 2);
							sw.Name = values[0];
							sw.Value = values[1];
						}
						else
						{
							sw.Name = arg;
						}
					}
				}
				updated = true;
			}

			public CommandLineSwitch this[string name]
			{
				get
				{
					foreach (CommandLineSwitch sw in this)
					{
						if (sw.Name == name) return sw;
					}
					return null;
				}
			}

			public CommandLineSwitch Add(string name, string defaultValue = null, bool optional = false, bool canHaveValue = false, string exampleValue = null)
			{
				CommandLineSwitch sw = new CommandLineSwitch();
				sw.Name = name;
				sw.CanHaveValue = canHaveValue;
				sw.DefaultValue = defaultValue;
				sw.ExampleValue = exampleValue;
				sw.IsOptional = optional;
				Add(sw);
				return sw;
			}
		}

		public bool IsOptional { get; set; } = false;
		public bool CanHaveValue { get; set; } = false;

		public string Name { get; set; } = String.Empty;
		public string DefaultValue { get; set; } = null;
		public string ExampleValue { get; set; } = null;

		private string mvarValue = null;
		public string Value
		{
			get { return mvarValue == null ? DefaultValue : mvarValue; }
			private set { mvarValue = value; }
		}
	}
}