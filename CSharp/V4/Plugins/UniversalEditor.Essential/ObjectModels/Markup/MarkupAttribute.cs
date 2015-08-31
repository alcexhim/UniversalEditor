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
			public MarkupAttribute this[string fullName]
			{
				get
				{
					MarkupAttribute result;
					foreach (MarkupAttribute att in this)
					{
						if (att.FullName == fullName)
						{
							result = att;
							return result;
						}
					}
					result = null;
					return result;
				}
			}
			public MarkupAttribute Add(string fullName)
			{
				return this.Add(fullName, string.Empty);
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
