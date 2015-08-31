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

			/// <summary>
			/// If a <see cref="Group" /> with the specified name exists within this collection,
			/// returns that group. Otherwise, creates a new group with that name and returns it.
			/// </summary>
			/// <param name="Name">The name of the <see cref="Group" /> to search for in this collection.</param>
			/// <returns>The <see cref="Group" /> with the given name in this collection, or if no such group exists, an empty group with the given name.</returns>
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
		/// <summary>
		/// The name of this <see cref="Group"/>.
		/// </summary>
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private Group mvarParent = null;
		/// <summary>
		/// The <see cref="Group" /> that contains this group as a child.
		/// </summary>
		public Group Parent { get { return mvarParent; } }

		private Group.GroupCollection mvarGroups = new Group.GroupCollection();
		/// <summary>
		/// The children <see cref="Group"/>s that are contained within this group.
		/// </summary>
		public Group.GroupCollection Groups { get { return mvarGroups; } }

		private Property.PropertyCollection mvarProperties = null;
		/// <summary>
		/// The children <see cref="Property" />s that are contained within this group.
		/// </summary>
		public Property.PropertyCollection Properties { get { return mvarProperties; } }

		public Group() : this(null, String.Empty)
		{
		}
		public Group(Group parent) : this(parent, String.Empty)
		{
		}
		public Group(string Name) : this(null, Name)
		{
		}
		public Group(Group parent, string Name)
		{
			mvarName = Name;
			mvarParent = parent;
			mvarProperties = new Property.PropertyCollection(this);
			mvarGroups = new Group.GroupCollection(this);
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
			set { mvarIsEmpty = value; }
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
		/// <summary>
		/// The comment(s) to display before the group header.
		/// </summary>
		public string CommentBefore { get { return mvarCommentBefore; } set { mvarCommentBefore = value; } }

		private string mvarCommentAfter = String.Empty;
		/// <summary>
		/// The comment(s) to display after the group header but before the first property (or
		/// subgroup) within the group.
		/// </summary>
		public string CommentAfter { get { return mvarCommentAfter; } set { mvarCommentAfter = value; } }

		public override string ToString()
		{
			return mvarName + " [" + mvarGroups.Count.ToString() + " groups, " + mvarProperties.Count.ToString() + " properties]";
		}
	}
}
