using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Collections.Generic
{
	/// <summary>
	/// Provides a <see cref="T:Dictionary`2" /> that automatically adds or updates a key or value when
	/// it is requested.
	/// </summary>
	/// <typeparam name="TKey">The type of the key part of the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the value part of the dictionary.</typeparam>
	public class AutoDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		/// <summary>
		/// Retrieves or updates an item with the specified key. If the item does not exist on update,
		/// it will be created. If the item does not exist on retrieval, the value specified for
		/// <paramref name="defaultValue" /> will be returned.
		/// </summary>
		/// <param name="key">The key of the item to retrieve or update.</param>
		/// <param name="defaultValue">The value returned on retrieval when the item with the specified key does not exist.</param>
		/// <returns>The item with the specified key if it exists in this collection; otherwise, defaultValue.</returns>
		public TValue this[TKey key, TValue defaultValue = default(TValue)]
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
			set
			{
				// the .NET Framework Dictionary`2 implementation handles the "add or update" case
				// automatically if a non-existent key is specified
				base[key] = value;
			}
		}
	}
}
