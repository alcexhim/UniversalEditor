//
//  NameValuePairGroup.cs - represents a grouped collection of NameValuePair<T> items
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
	/// Represents a grouped collection of <see cref="NameValuePair{T}" /> items.
	/// </summary>
	public class NameValuePairGroup<T>
	{
		private NameValuePairGroupCollection mvarGroups;
		private NameValuePair<T>.NameValuePairCollection mvarItems;
		private string mvarName;
		private string mvarPathSeparator;

		public NameValuePairGroup(string Name)
		{
			mvarGroups = new NameValuePairGroupCollection();
			mvarItems = new NameValuePair<T>.NameValuePairCollection();
			mvarPathSeparator = "";
			mvarName = Name;
		}

		public NameValuePairGroup<T> GetGroup(string GroupName)
		{
			string[] path = GroupName.Split(new string[] { this.mvarPathSeparator }, StringSplitOptions.None);
			if (path.Length == 1)
			{
				return this.mvarGroups[path[0]];
			}
			NameValuePairGroup<T> pgParent = this.mvarGroups[path[0]];
			int i = 1;
			while (pgParent != null)
			{
				if (pgParent.Groups[path[i]] != null)
				{
					pgParent = pgParent.Groups[path[i]];
					i++;
				}
				else
				{
					break;
				}
			}
			return pgParent.Groups[path[path.Length - 1]];
		}

		public NameValuePair<T> GetItem(string PropertyName)
		{
			string[] path = PropertyName.Split(new string[] { this.mvarPathSeparator }, StringSplitOptions.None);
			if (path.Length == 1)
			{
				return mvarItems[path[0]];
			}
			NameValuePairGroup<T> pgParent = mvarGroups[path[0]];
			int i = 1;
			while (pgParent != null)
			{
				if (pgParent.Groups[path[i]] != null)
				{
					pgParent = pgParent.Groups[path[i]];
					i++;
				}
				else
				{
					break;
				}
			}
			return pgParent.Items[path[path.Length - 1]];
		}

		public bool GetItemValueAsBoolean(string ValueName)
		{
			return this.GetItemValueAsBoolean(ValueName, false);
		}

		public bool GetItemValueAsBoolean(string ValueName, bool DefaultValue)
		{
			string val = this.GetItemValueAsString(ValueName);
			if ((((val.ToLower() == "1") || (val.ToLower() == "true")) || (val.ToLower() == "yes")) || (val.ToLower() == "on"))
			{
				return true;
			}
			if ((((val.ToLower() == "0") || (val.ToLower() == "false")) || (val.ToLower() == "no")) || (val.ToLower() == "off"))
			{
				return false;
			}
			return DefaultValue;
		}

		public int GetItemValueAsInteger(string ValueName)
		{
			return this.GetItemValueAsInteger(ValueName, -1);
		}

		public int GetItemValueAsInteger(string ValueName, int DefaultValue)
		{
			try
			{
				return int.Parse(this.GetItemValueAsString(ValueName, DefaultValue.ToString()));
			}
			catch
			{
				return DefaultValue;
			}
		}

		public string GetItemValueAsString(string ValueName)
		{
			return this.GetItemValueAsString(ValueName, "");
		}

		public string GetItemValueAsString(string ValueName, string DefaultValue)
		{
			try
			{
				return this.GetItem(ValueName).Value.ToString();
			}
			catch
			{
				return DefaultValue;
			}
		}

		public NameValuePairGroupCollection Groups
		{
			get
			{
				return this.mvarGroups;
			}
		}

		public NameValuePair<T>.NameValuePairCollection Items
		{
			get
			{
				return this.mvarItems;
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

		public string PathSeparator
		{
			get
			{
				return this.mvarPathSeparator;
			}
			set
			{
				this.mvarPathSeparator = value;
			}
		}

		public class NameValuePairGroupCollection : System.Collections.ObjectModel.Collection<NameValuePairGroup<T>>
		{
			public NameValuePairGroup<T> Add(string Name)
			{
				NameValuePairGroup<T> pg = new NameValuePairGroup<T>(Name);
				base.Add(pg);
				return pg;
			}

			public bool Contains(string Name)
			{
				return (this[Name] != null);
			}

			public bool Remove(string Name)
			{
				NameValuePairGroup<T> pg = this[Name];
				if (pg != null)
				{
					base.Remove(pg);
					return true;
				}
				return false;
			}

			public NameValuePairGroup<T> this[string Name]
			{
				get
				{
					foreach (NameValuePairGroup<T> pg in this)
					{
						if (pg.Name == Name)
						{
							return pg;
						}
					}
					return null;
				}
			}
		}
	}
}
