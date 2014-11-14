using System;
using System.Collections.Generic;

namespace UniversalEditor.Collections.Generic
{
	public class KeyValuePairList<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
	{
		private Dictionary<TKey, KeyValuePair<TKey, TValue>> valuesByKey = new Dictionary<TKey, KeyValuePair<TKey, TValue>>();
		
		public KeyValuePair<TKey, TValue> Add(TKey key, TValue value)
		{
			KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(key, value);
			base.Add(item);
			valuesByKey.Add(key, item);
			return item;
		}
		public KeyValuePair<TKey, TValue> this[TKey key]
		{
			get { return valuesByKey[key]; }
		}
		public bool Contains(TKey key)
		{
			return valuesByKey.ContainsKey(key);
		}
		public bool Remove(TKey key)
		{
			if (!Contains(key))
			{
				return false;
			}
			Remove(this[key]);
			return true;
		}
		public new void Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			valuesByKey.Remove(item.Key);
		}
	}
}