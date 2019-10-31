using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public class MarkupAttribute
	{
		public class MarkupAttributeCollection
			: System.Collections.ObjectModel.Collection<MarkupAttribute>
		{
			private Dictionary<string, MarkupAttribute> _itemsByName = new Dictionary<string, MarkupAttribute>();

			public MarkupAttribute this[string fullName]
			{
				get
				{
					if (_itemsByName.ContainsKey(fullName))
						return _itemsByName[fullName];

					return null;
				}
			}
			public MarkupAttribute Add(string fullName)
			{
				return Add(fullName, string.Empty);
			}
			public MarkupAttribute Add(string fullName, string value)
			{
				MarkupAttribute att = new MarkupAttribute();
				att.FullName = fullName;
				att.Value = value;
				base.Add(att);
				return att;
			}
			public MarkupAttribute Add(string nameSpace, string name, string value)
			{
				MarkupAttribute att = new MarkupAttribute();
				att.Namespace = nameSpace;
				att.Name = name;
				att.Value = value;
				base.Add(att);
				return att;
			}
			public bool Contains(string fullName)
			{
				return this[fullName] != null;
			}
			public bool Remove(string fullName)
			{
				MarkupAttribute att = this[fullName];
				bool result;
				if (att != null)
				{
					base.Remove(att);
					result = true;
				}
				else
				{
					result = false;
				}
				return result;
			}

			protected override void ClearItems()
			{
				base.ClearItems();
				_itemsByName.Clear();
			}
			protected override void InsertItem(int index, MarkupAttribute item)
			{
				base.InsertItem(index, item);
				_itemsByName[item.FullName] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (_itemsByName.ContainsKey(this[index].FullName))
					_itemsByName.Remove(this[index].FullName);
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, MarkupAttribute item)
			{
				if (_itemsByName.ContainsKey(this[index].FullName))
					_itemsByName.Remove(this[index].FullName);
				base.SetItem(index, item);
				_itemsByName[item.Name] = item;
			}
		}
		private string mvarNamespace = null;
		private string mvarName = string.Empty;
		private string mvarValue = string.Empty;
		public string Namespace
		{
			get
			{
				return this.mvarNamespace;
			}
			set
			{
				this.mvarNamespace = value;
			}
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
		public string Value
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
		public string FullName
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				if (this.mvarNamespace != null)
				{
					sb.Append(this.mvarNamespace);
					sb.Append(':');
				}
				sb.Append(this.mvarName);
				return sb.ToString();
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					string[] nameParts = value.Split(new char[]
					{
						':'
					}, 2);
					if (nameParts.Length == 1)
					{
						this.mvarName = nameParts[0];
					}
					else
					{
						if (nameParts.Length == 2)
						{
							this.mvarNamespace = nameParts[0];
							this.mvarName = nameParts[1];
						}
					}
				}
			}
		}
		public object Clone()
		{
			return new MarkupAttribute
			{
				Name = this.mvarName,
				Namespace = this.mvarNamespace,
				Value = this.mvarValue
			};
		}
		public override string ToString()
		{
			return this.FullName + "=\"" + this.Value + "\"";
		}
	}
}
