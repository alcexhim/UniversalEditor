using System;
using System.Collections.Generic;

namespace MBS.Framework.UserInterface
{
	public class CommandEventArgs : EventArgs
	{
		public Command Command { get; private set; }

		private Dictionary<string, object> _NamedParameters = new Dictionary<string, object>();
		public T GetNamedParameter<T>(string key, T defaultValue = default(T))
		{
			if (_NamedParameters.ContainsKey(key))
				return (T)_NamedParameters[key];
			return defaultValue;
		}
		public object GetNamedParameter(string key, object defaultValue = null)
		{
			if (_NamedParameters.ContainsKey(key))
				return _NamedParameters[key];
			return defaultValue;
		}


		public CommandEventArgs(Command command, KeyValuePair<string, object>[] namedParameters = null)
		{
			Command = command;
			if (namedParameters != null)
			{
				for (int i = 0; i < namedParameters.Length; i++)
				{
					_NamedParameters[namedParameters[i].Key] = namedParameters[i].Value;
				}
			}
		}
	}
	public delegate void CommandEventHandler(object sender, CommandEventArgs e);
}
