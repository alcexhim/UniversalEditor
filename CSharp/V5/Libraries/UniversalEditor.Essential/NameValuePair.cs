namespace UniversalEditor
{
	using System;

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