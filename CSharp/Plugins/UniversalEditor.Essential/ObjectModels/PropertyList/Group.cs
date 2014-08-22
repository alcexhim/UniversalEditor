using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.PropertyList
{
	public class Group : ICloneable
	{
		public class GroupCollection : System.Collections.ObjectModel.Collection<Group>
		{
			private Group mvarParent = null;
			public Group this[string Name]
			{
				get
				{
					Group result;
					foreach (Group g in this)
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
			public GroupCollection()
			{
				this.mvarParent = null;
			}
			public GroupCollection(Group parent)
			{
				this.mvarParent = parent;
			}
			public Group Add(string Name)
			{
				Group g = new Group();
				g.Name = Name;
				g.mvarParent = this.mvarParent;
				base.Add(g);
				return g;
			}
			public bool Contains(string Name)
			{
				return this[Name] != null;
			}
			public bool Remove(string Name)
			{
				Group g = this[Name];
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
			public void Append(Group grp)
			{
				Group parent = this[grp.Name];
				if (parent == null)
				{
					base.Add(grp);
				}
				else
				{
					foreach (Group grp2 in grp.Groups)
					{
						if (parent.Groups.Contains(grp2.Name))
						{
							parent.Groups.Append(grp2);
						}
						else
						{
							parent.Groups.Add(grp2);
						}
					}
					foreach (Property prp in grp.Properties)
					{
						if (parent.Properties.Contains(prp.Name))
						{
							parent.Properties[prp.Name].Value = prp.Value;
						}
						else
						{
							parent.Properties.Add(prp.Name, prp.Value);
						}
					}
				}
			}
			public Group AddOrRetrieve(string Name)
			{
				Group result;
				if (this.Contains(Name))
				{
					result = this[Name];
				}
				else
				{
					result = this.Add(Name);
				}
				return result;
			}
		}
		private string mvarName = string.Empty;
		private Group mvarParent = null;
		private Group.GroupCollection mvarGroups = new Group.GroupCollection();
		private Property.PropertyCollection mvarProperties = null;
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
		public Group Parent
		{
			get
			{
				return this.mvarParent;
			}
		}
		public Group.GroupCollection Groups
		{
			get
			{
				return this.mvarGroups;
			}
		}
		public Property.PropertyCollection Properties
		{
			get
			{
				return this.mvarProperties;
			}
		}
		public Group()
		{
			this.mvarName = string.Empty;
			this.mvarParent = null;
			this.mvarProperties = new Property.PropertyCollection(this);
			this.mvarGroups = new Group.GroupCollection(this);
		}
		public Group(Group parent)
		{
			this.mvarName = string.Empty;
			this.mvarParent = parent;
			this.mvarProperties = new Property.PropertyCollection(this);
			this.mvarGroups = new Group.GroupCollection(this);
		}
		public Group(string Name)
		{
			this.mvarName = Name;
			this.mvarParent = null;
			this.mvarProperties = new Property.PropertyCollection(this);
			this.mvarGroups = new Group.GroupCollection(this);
		}
		public Group(Group parent, string Name)
		{
			this.mvarName = Name;
			this.mvarParent = parent;
			this.mvarProperties = new Property.PropertyCollection(this);
			this.mvarGroups = new Group.GroupCollection(this);
		}
		public object Clone()
		{
			Group clone = new Group();
			foreach (Group g in this.mvarGroups)
			{
				clone.Groups.Add(g.Clone() as Group);
			}
			foreach (Property p in this.mvarProperties)
			{
				clone.Properties.Add(p.Clone() as Property);
			}
			clone.IsDefined = mvarIsDefined;
			clone.CommentBefore = (mvarCommentBefore.Clone() as string);
			clone.CommentAfter = (mvarCommentAfter.Clone() as string);
			clone.Name = (mvarName.Clone() as string);
			return clone;
		}

		private bool? mvarIsEmpty = null;
		public bool IsEmpty
		{
			get
			{
				if (mvarIsEmpty != null) return mvarIsEmpty.Value;
				return (!(mvarGroups.Count > 0 || mvarProperties.Count > 0));
			}
			set
			{
				mvarIsEmpty = value;
			}
		}
		public void Clear()
		{
			mvarGroups.Clear();
			mvarProperties.Clear();
			mvarIsDefined = true;
			mvarCommentBefore = String.Empty;
			mvarCommentAfter = String.Empty;
			mvarName = String.Empty;
			ResetEmpty();
		}
		public void ResetEmpty()
		{
			mvarIsEmpty = null;
		}

		private bool mvarIsDefined = true;
		public bool IsDefined { get { return mvarIsDefined; } set { mvarIsDefined = value; } }

		private string mvarCommentBefore = String.Empty;
		public string CommentBefore { get { return mvarCommentBefore; } set { mvarCommentBefore = value; } }

		private string mvarCommentAfter = String.Empty;
		public string CommentAfter { get { return mvarCommentAfter; } set { mvarCommentAfter = value; } }

		public override string ToString()
		{
			return mvarName + " [" + mvarGroups.Count.ToString() + " groups, " + mvarProperties.Count.ToString() + " properties]";
		}
	}
}
