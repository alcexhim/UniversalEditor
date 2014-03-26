using System;
namespace UniversalEditor.Collections.Generic
{
	public class KeyValuePairList<TKey, TValue>
		: System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<TKey, TValue>>
	{
		
		private System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.KeyValuePair<TKey, TValue>> valuesByKey = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.KeyValuePair<TKey, TValue>>();
		
		public System.Collections.Generic.KeyValuePair<TKey, TValue> Add (TKey key, TValue value)
		{
			System.Collections.Generic.KeyValuePair<TKey, TValue> item = new System.Collections.Generic.KeyValuePair<TKey, TValue> (key, value);
			base.Add (item);
			valuesByKey.Add (key, item);
			return item;
		}
		public System.Collections.Generic.KeyValuePair<TKey, TValue> this[TKey key]
		{
			get
			{
				return valuesByKey[key];
			}
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

