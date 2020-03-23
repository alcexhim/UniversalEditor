using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public abstract class MarkupElement : ICloneable
	{
		public class MarkupElementCollection
			: System.Collections.ObjectModel.Collection<MarkupElement>
		{
			private MarkupObjectModel _parentObjectModel = null;
			public MarkupObjectModel ParentObjectModel { get { return _parentObjectModel; } internal set { _parentObjectModel = value; } }

			private MarkupContainerElement _parent = null;
			public MarkupElement this[string nameSpace, string name]
			{
				get
				{
					return this[nameSpace + ":" + name];
				}
			}
			public MarkupElement this[string fullName]
			{
				get
				{
					MarkupElement result;
					foreach (MarkupElement e in this)
					{
						if (e.FullName == fullName)
						{
							result = e;
							return result;
						}
					}
					result = null;
					return result;
				}
			}
			public MarkupElement this[string fullName, int index]
			{
				get
				{
					int i = 0;
					MarkupElement result;
					foreach (MarkupElement e in this)
					{
						if (e.FullName == fullName)
						{
							if (i == index)
							{
								result = e;
								return result;
							}
							i++;
						}
					}
					result = null;
					return result;
				}
			}
			public MarkupElementCollection()
				: this(null, null)
			{
			}
			public MarkupElementCollection(MarkupObjectModel parentObjectModel)
				: this(null, parentObjectModel)
			{
			}
			public MarkupElementCollection(MarkupContainerElement parent)
				: this(parent, null)
			{
			}
			public MarkupElementCollection(MarkupContainerElement parent, MarkupObjectModel parentObjectModel)
			{
				this._parent = parent;
				this._parentObjectModel = parentObjectModel;
			}
			public new void Add(MarkupElement item)
			{
				item.mvarParent = this._parent;
				item.ParentObjectModel = this._parentObjectModel;
				base.Add(item);
			}
			public bool Contains(string fullName, string id = null, MarkupAttribute.MarkupAttributeCollection attributes = null)
			{
				MarkupElement el = this[fullName];
				MarkupTagElement tag = (el as MarkupTagElement);
				bool retval = el != null;
				if (id != null && tag != null)
				{
					MarkupAttribute attID = tag.Attributes["ID"];
					if (attID != null) retval &= (id == attID.Value);
				}

				if (tag != null && attributes != null)
				{
					for (int i = 0; i < attributes.Count; i++)
					{
						MarkupAttribute att = tag.Attributes[attributes[i].Name];
						if (att != null && att.Value != attributes[i].Value)
						{
							return false;
						}
					}
				}
				return retval;
			}
			public bool Contains(string fullName, Type elementType)
			{
				if ((elementType == typeof(MarkupElement)) || (elementType.IsSubclassOf(typeof(MarkupElement))))
				{
					return ((this[fullName] != null) && (this[fullName].GetType() == elementType));
				}
				return false;
			}
		}

		private MarkupObjectModel mvarParentObjectModel = null;
		public MarkupObjectModel ParentObjectModel { get { return mvarParentObjectModel; } internal set { mvarParentObjectModel = value; UpdateParentObjectModel (); } }

		protected virtual void UpdateParentObjectModel()
		{
		}

		private string mvarName = string.Empty;
		private string mvarValue = string.Empty;
		private string mvarNamespace = string.Empty;
		private MarkupContainerElement mvarParent = null;
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
		public string FullName
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				if (!string.IsNullOrEmpty(this.mvarNamespace))
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
		public MarkupContainerElement Parent
		{
			get
			{
				return this.mvarParent;
			}
		}

		public override string ToString()
		{
			string result;
			if (string.IsNullOrEmpty(this.mvarValue))
			{
				result = "<" + this.FullName + " />";
			}
			else
			{
				result = string.Concat(new string[]
				{
					"<", 
					this.FullName, 
					">", 
					this.mvarValue, 
					"</", 
					this.FullName, 
					">"
				});
			}
			return result;
		}

		public abstract object Clone();

		public virtual void Combine(MarkupElement el)
		{
		}
	}
}
