using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Collections.Generic
{
	/// <summary>
	/// Provides a <see cref="Dictionary" /> that automatically adds or updates a key or value when it is
	/// requested.
	/// </summary>
	/// <typeparam name="TKey">The type of the key part of the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the value part of the dictionary.</typeparam>
	public class AutoDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		public new TValue this[TKey key]
		{
			get { return this[key, default(TValue)]; }
			set
			{
				if (ContainsKey(key))
				{
					// we already contain an item with this key, so update the value accordingly
					base[key] = value;
				}
				else
				{
					// we do not already contain an item with this key, so create a new value
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
					// we already contain an item with this key, so return the value accordingly
					return base[key];
				}
				else
				{
					// we do not already contain an item with this key, so create a new value set to the
					// specified default value and return that
					base.Add(key, defaultValue);
					return defaultValue;
				}
			}
			set { this[key] = value; }
		}
	}
}
