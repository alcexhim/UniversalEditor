using System;
using System.Collections.Generic;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Internal.Linux
{
	public class GTKNativeControl : NativeControl
	{
		public IntPtr Handle { get; private set; } = IntPtr.Zero;
		public IntPtr[] AdditionalHandles
		{
			get
			{
				List<IntPtr> list = new List<IntPtr>();
				foreach (KeyValuePair<string, IntPtr> kvp in _NamedHandles)
				{
					list.Add(kvp.Value);
				}
				return list.ToArray();
			}
		}

		private Dictionary<string, IntPtr> _NamedHandles = new Dictionary<string, IntPtr>();
		public void SetNamedHandle(string name, IntPtr value)
		{
			_NamedHandles[name] = value;
		}
		public IntPtr GetNamedHandle(string name, IntPtr defaultValue = default(IntPtr))
		{
			if (_NamedHandles.ContainsKey(name)) return _NamedHandles[name];
			return defaultValue;
		}

		public GTKNativeControl(IntPtr handle, params KeyValuePair<string, IntPtr>[] namedHandles)
		{
			this.Handle = handle;
			foreach (KeyValuePair<string, IntPtr> kvp in namedHandles)
			{
				SetNamedHandle(kvp.Key, kvp.Value);
			}
		}

		public bool ContainsHandle(IntPtr handle)
		{
			if (Handle == handle) return true;
			if (_NamedHandles.ContainsValue(handle)) return true;
			return false;
		}

		public override string ToString()
		{
			return Handle.ToString();
		}
	}
}
