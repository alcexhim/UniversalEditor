using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Collections.Generic
{
	public class AutoDictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
	{
		public new TValue this[TKey key]
		{
			get
			{
				return this[key, default(TValue)];
			}
			set
			{
				if (ContainsKey(key))
				{
					base[key] = value;
				}
				else
				{
					base.Add(key, value);
				}
			}
		}
		public TValue this[TKey key, TValue defaultValue]
		{
			get
			{
				if (ContainsKey(key))
				{
					return base[key];
				}
				else
				{
					base.Add(key, defaultValue);
					return defaultValue;
				}
			}
			set
			{
				this[key] = value;
			}
		}
	}
}
