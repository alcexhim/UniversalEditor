//
//  CoreObjectAttribute.cs - represents an attribute in a Core Object file
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

using System;
using System.Text;

namespace UniversalEditor.ObjectModels.CoreObject
{
	/// <summary>
	/// Represents an attribute in a Core Object file.
	/// </summary>
	public class CoreObjectAttribute : ICloneable
	{
		public class CoreObjectAttributeCollection
			: System.Collections.ObjectModel.Collection<CoreObjectAttribute>
		{
			public CoreObjectAttribute Add(string name, string value = null)
			{
				CoreObjectAttribute item = new CoreObjectAttribute(name, value);
				Add(item);
				return item;
			}

			public CoreObjectAttribute this[string name]
			{
				get
				{
					foreach (CoreObjectAttribute item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
		}

		public CoreObjectAttribute()
		{
			mvarName = String.Empty;
		}
		public CoreObjectAttribute(string name, params string[] values)
		{
			mvarName = name;
			foreach (string value in values)
			{
				mvarValues.Add(value);
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private System.Collections.Specialized.StringCollection mvarValues = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection Values { get { return mvarValues; } }

		public object Clone()
		{
			CoreObjectAttribute clone = new CoreObjectAttribute();
			clone.Name = (mvarName.Clone() as string);
			foreach (string value in mvarValues)
			{
				clone.Values.Add(value.Clone() as string);
			}
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarName);
			if (mvarValues.Count > 0)
			{
				sb.Append("=");
				for (int i = 0; i < mvarValues.Count; i++)
				{
					sb.Append(mvarValues[i]);
					if (i < mvarValues.Count - 1) sb.Append(",");
				}
			}
			return sb.ToString();
		}
	}
}
