using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface.Settings
{
	public static class SettingsManager
	{
		public static T GetPropertyValue<T>(string[] path, T defaultValue = default(T))
		{
			return defaultValue;
		}
		public static void SetPropertyValue<T>(string[] path, T value)
		{
		}
	}
}
