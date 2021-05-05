//
//  NameValuePair.cs - represents a name-value pair of String expressions (not sure why we did this)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor
{
	using System;

	/// <summary>
	/// Represents a name-value pair of String expressions (not sure why we did this).
	/// </summary>
	public class NameValuePair : NameValuePair<string>
	{
		public NameValuePair(string Name) : base(Name)
		{
		}

		public NameValuePair(string Name, string Value) : base(Name, Value)
		{
		}
	}
	public class NameValuePair<T> : ICloneable
	{
		private string mvarName;
		private T mvarValue;

		public NameValuePair(string Name)
		{
			this.mvarName = "";
			this.mvarValue = default(T);
			this.mvarName = Name;
			this.mvarValue = default(T);
		}

		public NameValuePair(string Name, T Value)
		{
			this.mvarName = "";
			this.mvarValue = default(T);
			this.mvarName = Name;
			this.mvarValue = Value;
		}

		public string Name
		{
			get
			{
				return this.mvarName;
			}
			set
			{
				this.mvarName = value;
			}
		}

		public T Value
		{
			get
			{
				return this.mvarValue;
			}
			set
			{
				this.mvarValue = value;
			}
		}

		public class NameValuePairCollection : System.Collections.ObjectModel.Collection<NameValuePair<T>>
		{
			public NameValuePair<T> Add(string Name)
			{
				return this.Add(Name, default(T));
			}

			public NameValuePair<T> Add(string Name, T Value)
			{
				NameValuePair<T> p = new NameValuePair<T>(Name, Value);
				base.Add(p);
				return p;
			}

			public bool Contains(string Name)
			{
				return (this[Name] != null);
			}

			public bool Remove(string Name)
			{
				NameValuePair<T> p = this[Name];
				if (p != null)
				{
					base.Remove(p);
					return true;
				}
				return false;
			}

			public NameValuePair<T> this[string Name]
			{
				get
				{
					foreach (NameValuePair<T> p in this)
					{
						if (p.Name == Name)
						{
							return p;
						}
					}
					return null;
				}
			}
		}

		public object Clone()
		{
			NameValuePair<T> clone = new NameValuePair<T>(mvarName.Clone() as string);
			if (mvarValue is ICloneable)
			{
				clone.Value = (T)((mvarValue as ICloneable).Clone());
			}
			else
			{
				clone.Value = mvarValue;
			}
			return clone;
		}
	}
}
