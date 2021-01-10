//
//  PersonName.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree
{
	public class PersonName : ICloneable
	{
		public class PersonNameCollection
			: System.Collections.ObjectModel.Collection<PersonName>
		{

		}

		public PersonNameType Type { get; set; } = null;

		public Surname.SurnameCollection Surnames { get; } = new Surname.SurnameCollection();
		public string CompleteSurname
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < Surnames.Count; i++)
				{
					sb.Append(Surnames[i].ToString());
					if (i < Surnames.Count - 1)
					{
						sb.Append(' ');
					}
				}
				return sb.ToString();
			}
			set
			{
				string[] values = value.Split(new char[] { ' ' });
				for (int i = 0; i < values.Length; i++)
				{
					Surnames.Add(new Surname(values[i]));
				}
			}
		}

		public System.Collections.Specialized.StringCollection GivenNames { get; } = new System.Collections.Specialized.StringCollection();
		public string CompleteGivenName
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < GivenNames.Count; i++)
				{
					sb.Append(GivenNames[i]);
					if (i < GivenNames.Count - 1)
					{
						sb.Append(' ');
					}
				}
				return sb.ToString();
			}
			set
			{
				GivenNames.Clear();

				if (value != null)
				{
					string[] names = value.Split(new char[] { ' ' });
					for (int i = 0; i < names.Length; i++)
					{
						GivenNames.Add(names[i]);
					}
				}
			}
		}

		public string CallName { get; set; } = null;
		public string Title { get; set; } = null;
		public string Suffix { get; set; } = null;
		public string Nickname { get; set; } = null;

		public object Clone()
		{
			PersonName clone = new PersonName();
			for (int i = 0; i < Surnames.Count; i++)
			{
				clone.Surnames.Add(Surnames[i].Clone() as Surname);
			}
			for (int i = 0; i < GivenNames.Count; i++)
			{
				clone.GivenNames.Add(GivenNames[i].Clone() as string);
			}
			return clone;
		}

		public string GroupSurname { get; set; } = null;
		public PersonNameFormatter SortFormat { get; set; } = null;
		public PersonNameFormatter DisplayFormat { get; set; } = null;

		public PersonName()
		{

		}
		public PersonName(string completeSurname, string completeGivenName)
		{
			CompleteSurname = completeSurname;
			CompleteGivenName = completeGivenName;
		}
		public PersonName(Surname[] surnames, string[] givenNames)
		{
			for (int i = 0; i < surnames.Length; i++)
			{
				Surnames.Add(surnames[i]);
			}
			for (int i = 0; i < givenNames.Length; i++)
			{
				GivenNames.Add(givenNames[i]);
			}
		}

		public override string ToString()
		{
			if (DisplayFormat != null)
			{
				return base.ToString();
			}
			return PersonNameFormatter.Default.Format(this);
		}

	}
}
