﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	[DebuggerNonUserCode()]
	public struct Ptr<T>
	{
		private T[] value;
		private int index;
		private int length;

		/// <summary>
		/// Gets the capacity of the underlying array.
		/// </summary>
		public int Capacity
		{
			get { return value.Length; }
		}
		/// <summary>
		/// Gets the size of the <see cref="Ptr" />.
		/// </summary>
		public int Size
		{
			get { return length; }
		}

		public Ptr(T[] value, int index = -1)
		{
			this.value = value;
			this.length = value.Length;
			this.mvarAutoResize = false;
			if (index < -1 || index > value.Length - 1) throw new IndexOutOfRangeException();

			this.index = index;
		}

		public static Ptr<T> operator + (Ptr<T> value, int addend)
		{
			return new Ptr<T>(value.value, value.index + 1);
		}
		public static Ptr<T> operator - (Ptr<T> value, int addend)
		{
			return new Ptr<T>(value.value, value.index - 1);
		}

		/// <summary>
		/// Increments the index, then sets the value stored at that position to the given value.
		/// </summary>
		/// <param name="value">The value to set.</param>
		public void IncrementThenSetValue(T value)
		{
			this.Increment();
			SetValue(value);
		}
		/// <summary>
		/// Decrements the index, then sets the value stored at that position to the given value.
		/// </summary>
		/// <param name="value">The value to set.</param>
		public void DecrementThenSetValue(T value)
		{
			Decrement();
			SetValue(value);
		}

		/// <summary>
		/// Sets the value at the current position to the given value, then increments the index.
		/// </summary>
		/// <param name="value">The value to set.</param>
		public void SetValueThenIncrement(T value)
		{
			SetValue(value);
			Increment();
		}
		/// <summary>
		/// Sets the value at the current position to the given value, then decrements the index.
		/// </summary>
		/// <param name="value">The value to set.</param>
		public void SetValueThenDecrement(T value)
		{
			SetValue(value);
			Decrement();
		}

		public void Increment()
		{
			this.index++;
			AutoResizeIfNeeded(this.index);
		}
		public void Decrement()
		{
			if (this.index - 1 < 0) throw new IndexOutOfRangeException();
			this.index--;
		}

		/// <summary>
		/// Increments the index, then gets the value stored at that position.
		/// </summary>
		/// <returns>The value stored at the position after the index has been incremented.</returns>
		public T IncrementThenGetValue()
		{
			this.index++;
			T value = this.value[this.index];
			return value;
		}
		/// <summary>
		/// Decrements the index, then gets the value stored at that position.
		/// </summary>
		/// <returns>The value stored at the position after the index has been decremented.</returns>
		public T DecrementThenGetValue()
		{
			this.index--;
			T value = this.value[this.index];
			return value;
		}

		/// <summary>
		/// Gets the value stored at the current position, then increments the index.
		/// </summary>
		/// <returns>The value stored at the position before the index has been incremented.</returns>
		public T GetValueThenIncrement()
		{
			T value = this.value[this.index];
			this.index++;
			return value;
		}
		/// <summary>
		/// Gets the value stored at the current position, then decrements the index.
		/// </summary>
		/// <returns>The value stored at the position before the index has been decremented.</returns>
		public T GetValueThenDecrement()
		{
			T value = this.value[this.index];
			this.index--;
			return value;
		}

		/// <summary>
		/// Retrieves all elements of this <see cref="Ptr" /> as an array.
		/// </summary>
		/// <returns>An array of all items in this <see cref="Ptr" />.</returns>
		public T[] ToArray()
		{
			return (T[])this.value.Clone();
		}

		/// <summary>
		/// Sets the value at the specified index to the given value.
		/// </summary>
		/// <param name="value">The value to set.</param>
		/// <param name="index">The index in the underlying array of the value to set, or -1 to set the current value.</param>
		public void SetValue(T value, int index = -1)
		{
			if (index == -1) index = this.index;
			
			AutoResizeIfNeeded(index);

			if (index < 0 || index > this.value.Length - 1) throw new IndexOutOfRangeException();
			this.value[index] = value;
		}

		/// <summary>
		/// Determines whether an auto-resize is needed.
		/// </summary>
		/// <param name="index"></param>
		/// <returns>-1 if auto-resize is disabled, 0 if no resize was needed, or the number of elements added to the underlying array.</returns>
		private int AutoResizeIfNeeded(int index)
		{
			if (mvarAutoResize) return -1;
			if (index > this.length - 1 && mvarAutoResize)
			{
				if (this.length + 1 > this.value.Length - 1)
				{
					Array.Resize<T>(ref this.value, this.value.Length * 2);
					this.length++;
				}
			}
			return 0;
		}

		/// <summary>
		/// Retrieves the value at the specified index.
		/// </summary>
		/// <param name="index">The index in the underlying array of the value to retrieve, or -1 to retrieve the current value.</param>
		/// <returns></returns>
		public T GetValue(int index = -1)
		{
			if (index == -1) index = this.index;
			return this.value[index];
		}

		public T this[int index]
		{
			get
			{
				return this.value[index];
			}
			set
			{
				this.value[index] = value;
			}
		}

		private bool mvarAutoResize;
		/// <summary>
		/// Determines if the underlying array will automatically resize when an attempt is made to address memory outside of the array boundaries.
		/// </summary>
		public bool AutoResize { get { return mvarAutoResize; } set { mvarAutoResize = value; } }
	}
}