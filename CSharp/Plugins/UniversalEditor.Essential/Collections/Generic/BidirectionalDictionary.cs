using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Collections.Generic
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

		public void Add(T1 value1, T2 value2)
		{
			this.mvarForwardDictionary.Add(value1, value2);
			this.mvarBackwardDictionary.Add(value2, value1);
		}
		public void RemoveByValue1(T1 value1)
		{
			T2 value2 = this.mvarForwardDictionary[value1];
			this.mvarForwardDictionary.Remove(value1);
			this.mvarBackwardDictionary.Remove(value2);
		}
		public void RemoveByValue2(T2 value2)
		{
			T1 value3 = this.mvarBackwardDictionary[value2];
			this.mvarForwardDictionary.Remove(value3);
			this.mvarBackwardDictionary.Remove(value2);
		}
		public T1 GetValue1(T2 value2)
		{
			return this.mvarBackwardDictionary[value2];
		}
		public T2 GetValue2(T1 value1)
		{
			return this.mvarForwardDictionary[value1];
		}
		public bool HasValue1(T2 value2)
		{
			return this.mvarBackwardDictionary.ContainsKey(value2);
		}
		public bool HasValue2(T1 value1)
		{
			return this.mvarForwardDictionary.ContainsKey(value1);
		}
		public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
		{
			return this.mvarForwardDictionary.GetEnumerator();
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.mvarForwardDictionary.GetEnumerator();
		}

		public bool ContainsValue1(T1 value)
		{
			return this.mvarForwardDictionary.ContainsKey(value);
		}
		public bool ContainsValue2(T2 value)
		{
			return this.mvarBackwardDictionary.ContainsKey(value);
		}

		public int Count
		{
			get
			{
				if (mvarForwardDictionary.Count != mvarBackwardDictionary.Count) throw new InvalidOperationException("Count mismatch");
				return mvarBackwardDictionary.Count;
			}
		}
	}
}
