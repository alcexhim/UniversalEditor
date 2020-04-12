//
//  Group.cs - represents a group in a PropertyListObjectModel which can contain Property instances and other Groups
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

using System;

namespace UniversalEditor.ObjectModels.PropertyList
{
	/// <summary>
	/// Represents a group in a <see cref="PropertyListObjectModel" /> which can contain <see cref="Property" /> instances and other <see cref="Group" />s.
	/// </summary>
	public class Group : ICloneable, IPropertyListContainer
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
				g.Parent = this.mvarParent;
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
		/// <summary>
		/// The name of this <see cref="Group"/>.
		/// </summary>
		public string Name { get; set; } = string.Empty;
		/// <summary>
		/// The <see cref="Group" /> that contains this group as a child.
		/// </summary>
		public Group Parent { get; private set; } = null;
		/// <summary>
		/// The children <see cref="Group"/>s that are contained within this group.
		/// </summary>
		public Group.GroupCollection Groups { get; } = new Group.GroupCollection();
		/// <summary>
		/// The children <see cref="Property" />s that are contained within this group.
		/// </summary>
		public Property.PropertyCollection Properties { get; } = null;

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
			this.Name = Name;
			Parent = parent;
			Properties = new Property.PropertyCollection(this);
			Groups = new Group.GroupCollection(this);
		}

		public object Clone()
		{
			Group clone = new Group();
			foreach (Group g in this.Groups)
			{
				clone.Groups.Add(g.Clone() as Group);
			}
			foreach (Property p in this.Properties)
			{
				clone.Properties.Add(p.Clone() as Property);
			}
			clone.IsDefined = IsDefined;
			clone.CommentBefore = (CommentBefore.Clone() as string);
			clone.CommentAfter = (CommentAfter.Clone() as string);
			clone.Name = (Name.Clone() as string);
			return clone;
		}

		private bool? mvarIsEmpty = null;
		public bool IsEmpty
		{
			get
			{
				if (mvarIsEmpty != null) return mvarIsEmpty.Value;
				return (!(Groups.Count > 0 || Properties.Count > 0));
			}
			set { mvarIsEmpty = value; }
		}

		public void Clear()
		{
			Groups.Clear();
			Properties.Clear();
			IsDefined = true;
			CommentBefore = String.Empty;
			CommentAfter = String.Empty;
			Name = String.Empty;
			ResetEmpty();
		}
		public void ResetEmpty()
		{
			mvarIsEmpty = null;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UniversalEditor.ObjectModels.PropertyList.Group"/> is defined.
		/// </summary>
		/// <value><c>true</c> if is defined; otherwise, <c>false</c>.</value>
		public bool IsDefined { get; set; } = true;
		/// <summary>
		/// The comment(s) to display before the group header.
		/// </summary>
		public string CommentBefore { get; set; } = String.Empty;
		/// <summary>
		/// The comment(s) to display after the group header but before the first property (or
		/// subgroup) within the group.
		/// </summary>
		public string CommentAfter { get; set; } = String.Empty;

		public override string ToString()
		{
			return Name + " [" + Groups.Count.ToString() + " groups, " + Properties.Count.ToString() + " properties]";
		}
	}
}
