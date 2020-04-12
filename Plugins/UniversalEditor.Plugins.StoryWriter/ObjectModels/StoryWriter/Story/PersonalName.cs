//
//  PersonalName.cs - represents a name separated into a given name, multiple middle names, a family name, and a nickname
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

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class PersonalName : ICloneable
	{
		/// <summary>
		/// Gets or sets the given (typically first) name.
		/// </summary>
		/// <value>The given name.</value>
		public string GivenName { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="string" /> instances representing the middle names.
		/// </summary>
		/// <value>The middle names.</value>
		public System.Collections.Specialized.StringCollection MiddleNames { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// Gets or sets the family (typically last) name.
		/// </summary>
		/// <value>The family name.</value>
		public string FamilyName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the nickname.
		/// </summary>
		/// <value>The nickname.</value>
		public string Nickname { get; set; } = String.Empty;

		public PersonalName()
		{
		}
		public PersonalName(string fullName)
		{
			Name = fullName;
		}
		public PersonalName(string givenName, string familyName) : this(givenName, String.Empty, familyName)
		{
		}
		public PersonalName(string givenName, string middleName, string familyName)
		{
			GivenName = givenName;
			string[] mn = middleName.Split(new char[] { ' ' });
			foreach (string m in mn)
			{
				if (!String.IsNullOrEmpty(m)) MiddleNames.Add(m);
			}
			FamilyName = familyName;
		}

		public string Name
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(GivenName);
				foreach (string s in MiddleNames)
				{
					sb.Append(" ");
					sb.Append(s);
				}
				if (!String.IsNullOrEmpty(FamilyName))
				{
					sb.Append(" ");
					sb.Append(FamilyName);
				}
				return sb.ToString();
			}
			set
			{
				string[] values = value.Split(new char[] { ' ' });
				StringBuilder sb = new StringBuilder();
				if (values.Length > 0)
				{
					sb.Append(values[0]);
					if (values.Length > 2)
					{
						for (int i = 1; i < values.Length - 1; i++)
						{
							MiddleNames.Add(values[i]);
						}
					}
					if (values.Length > 1)
					{
						sb.Append(" ");
						sb.Append(values[values.Length - 1]);
					}
				}
			}
		}

		public object Clone()
		{
			PersonalName clone = new PersonalName();
			clone.GivenName = (GivenName.Clone() as string);
			foreach (string s in MiddleNames)
			{
				clone.MiddleNames.Add(s.Clone() as string);
			}
			clone.FamilyName = (FamilyName.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
