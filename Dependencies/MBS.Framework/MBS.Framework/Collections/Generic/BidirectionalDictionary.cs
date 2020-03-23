using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MBS.Framework.Collections.Generic
{
	/// <summary>
	/// Provides a collection that can be keyed either forward (T1=>T2) or backward (T2=>T1).
	/// </summary>
	/// <typeparam name="T1">The type of the value on which to forward-key the collection.</typeparam>
	/// <typeparam name="T2">The type of the value on which to backward-key the collection.</typeparam>
	public class BidirectionalDictionary<T1, T2> : System.Collections.IEnumerable
	{
		private Dictionary<T1, T2> mvarForwardDictionary = new Dictionary<T1, T2>();
		private Dictionary<T2, T1> mvarBackwardDictionary = new Dictionary<T2, T1>();

		/// <summary>
		/// Adds the specified first and second value to the collection.
		/// </summary>
		/// <param name="value1">The first value to add.</param>
		/// <param name="value2">The second value to add.</param>
		public void Add(T1 value1, T2 value2)
		{
			mvarForwardDictionary.Add(value1, value2);
			mvarBackwardDictionary.Add(value2, value1);
		}

		/// <summary>
		/// Removes the first and second value associated with the specified first value.
		/// </summary>
		/// <param name="value1">The first value of the first-second value pair to remove.</param>
		public void RemoveByValue1(T1 value1)
		{
			T2 value2 = mvarForwardDictionary[value1];
			mvarForwardDictionary.Remove(value1);
			mvarBackwardDictionary.Remove(value2);
		}
		/// <summary>
		/// Removes the first and second value associated with the specified second value.
		/// </summary>
		/// <param name="value1">The second value of the first-second value pair to remove.</param>
		public void RemoveByValue2(T2 value2)
		{
			T1 value1 = mvarBackwardDictionary[value2];
			mvarForwardDictionary.Remove(value1);
			mvarBackwardDictionary.Remove(value2);
		}

		/// <summary>
		/// Gets the first value associated with the specified second value.
		/// </summary>
		/// <param name="value2">The second value of the first-second value pair to search for.</param>
		/// <returns>The first value associated with the specified second value.</returns>
		public T1 GetValue1(T2 value2)
		{
			return mvarBackwardDictionary[value2];
		}
		/// <summary>
		/// Gets the second value associated with the specified first value.
		/// </summary>
		/// <param name="value1">The first value of the first-second value pair to search for.</param>
		/// <returns>The second value associated with the specified first value.</returns>
		public T2 GetValue2(T1 value1)
		{
			return mvarForwardDictionary[value1];
		}

		/// <summary>
		/// Determines if the specified first value is contained in this collection.
		/// </summary>
		/// <param name="value">The value to search for.</param>
		/// <returns>True if the value exists in the first value dictionary; otherwise, false.</returns>
		public bool ContainsValue1(T1 value)
		{
			return mvarForwardDictionary.ContainsKey(value);
		}
		/// <summary>
		/// Determines if the specified second value is contained in this collection.
		/// </summary>
		/// <param name="value">The value to search for.</param>
		/// <returns>True if the value exists in the second value dictionary; otherwise, false.</returns>
		public bool ContainsValue2(T2 value)
		{
			return mvarBackwardDictionary.ContainsKey(value);
		}

		/// <summary>
		/// Gets a <see cref="IEnumerator" /> that can be used to iterate over this collection.
		/// </summary>
		/// <returns>A <see cref="IEnumerator" /> that can be used to iterate over this collection.</returns>
		public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
		{
			return mvarForwardDictionary.GetEnumerator();
		}
		/// <summary>
		/// Gets a <see cref="IEnumerator" /> that can be used to iterate over this collection.
		/// </summary>
		/// <returns>A <see cref="IEnumerator" /> that can be used to iterate over this collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return mvarForwardDictionary.GetEnumerator();
		}

		/// <summary>
		/// Returns the number of items in this collection.
		/// </summary>
		public int Count
		{
			get
			{
				if (mvarForwardDictionary.Count != mvarBackwardDictionary.Count)
				{
					// this should never happen
					throw new InvalidOperationException("Count mismatch");
				}

				// they should be equal, so choose one at random to return and hardcode it ;)
				return mvarBackwardDictionary.Count;
			}
		}
	}
}
