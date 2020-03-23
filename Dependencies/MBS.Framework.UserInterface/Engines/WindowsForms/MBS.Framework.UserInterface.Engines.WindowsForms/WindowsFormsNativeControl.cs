using System;
using System.Collections.Generic;

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public class WindowsFormsNativeControl : NativeControl
	{
		public System.Windows.Forms.Control Handle { get; private set; } = null;

		public WindowsFormsNativeControl (System.Windows.Forms.Control handle)
		{
			Handle = handle;
		}

		public System.Windows.Forms.Control[] AdditionalHandles
		{
			get
			{
				List<System.Windows.Forms.Control> list = new List<System.Windows.Forms.Control>();
				foreach (KeyValuePair<string, System.Windows.Forms.Control> kvp in _NamedHandles)
				{
					list.Add(kvp.Value);
				}
				return list.ToArray();
			}
		}

		private Dictionary<string, System.Windows.Forms.Control> _NamedHandles = new Dictionary<string, System.Windows.Forms.Control>();
		public void SetNamedHandle(string name, System.Windows.Forms.Control value)
		{
			_NamedHandles[name] = value;
		}
		public System.Windows.Forms.Control GetNamedHandle(string name, System.Windows.Forms.Control defaultValue = default(System.Windows.Forms.Control))
		{
			if (_NamedHandles.ContainsKey(name)) return _NamedHandles[name];
			return defaultValue;
		}

	}
}
