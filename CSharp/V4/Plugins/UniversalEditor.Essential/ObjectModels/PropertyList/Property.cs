using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.PropertyList
{
	public class Property : ICloneable
	{
		public class PropertyCollection : System.Collections.ObjectModel.Collection<Property>
		{
			private Group mvarParent = null;
			public Property this[string Name]
			{
				get
				{
					Property result;
					foreach (Property g in this)
					{
						if (g.Name == Name)
						{
							result = g;
							return result;
						}
					}
					result = null;
					return result;
				}
			}
			public PropertyCollection()
			{
				this.mvarParent = null;
			}
			public PropertyCollection(Group parent)
			{
				this.mvarParent = parent;
			}
			public Property Add(string name)
			{
				return this.Add(name, null);
			}
			public Property Add(string name, object value)
			{
				return Add(name, value, PropertyValueType.Unknown);
			}
			public Property Add(string name, object value, PropertyValueType type)
			{
				Property p = new Property();
				p.Name = name;
				p.Value = value;
				p.mvarParent = this.mvarParent;
				p.Type = type;
				base.Add(p);
				return p;
			}
			public bool Contains(string Name)
			{
				return this[Name] != null;
			}
			public bool Remove(string Name)
			{
				Property g = this[Name];
				bool result;
				if (g != null)
				{
					base.Remove(g);
					result = true;
				}
				else
				{
					result = false;
				}
				return result;
			}
			public void AddOrReplace(string Name, object Value)
			{
				if (this.Contains(Name))
				{
					this[Name].Value = Value;
				}
				else
				{
					this.Add(Name, Value);
				}
			}
		}
		private string mvarName = "";
		private object mvarValue = null;
		private Group mvarParent = null;
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
		public object Value
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
		public Group Parent
		{
			get
			{
				return this.mvarParent;
			}
		}
		public object Clone()
		{
			return new Property
			{
				Name = this.mvarName,
				Value = this.mvarValue
			};
		}
		public override string ToString()
		{
			return mvarName + " = \"" + mvarValue.ToString() + "\"";
		}

		public Property()
		{
		}
		public Property(string name, object value = null)
		{
			mvarName = name;
			mvarValue = value;
		}

		private PropertyValueType mvarType = PropertyValueType.Unknown;
		public PropertyValueType Type { get { return mvarType; } set { mvarType = value; } }
	}
}
