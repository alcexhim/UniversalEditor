//
//  Person.cs
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
	public class Person : DatabaseObject
	{
		public class PersonCollection
			: System.Collections.ObjectModel.Collection<Person>
		{

		}

		public Person()
		{
		}
		public Person(PersonName name)
		{
			Names.Add(name);
			DefaultName = name;
		}
		public Person(PersonName[] names, int defaultIndex = 0)
		{
			for (int i = 0; i < names.Length; i++)
			{
				Names.Add(names[i]);
			}
			if (Names.Count > 0)
			{
				DefaultName = Names[defaultIndex];
			}
		}

		public PersonName.PersonNameCollection Names { get; } = new PersonName.PersonNameCollection();
		public PersonName DefaultName { get; set; } = null;
		public Gender Gender { get; set; } = null;

		public override string ToString()
		{
			if (DefaultName != null)
			{
				return DefaultName.ToString();
			}
			return String.Empty;
		}

		protected override DatabaseObject CloneInternal()
		{
			Person clone = new Person();
			CloneTo(clone);

			for (int i = 0; i < Names.Count; i++)
			{
				clone.Names.Add(Names[i].Clone() as PersonName);
				if (Names[i] == DefaultName)
				{
					clone.DefaultName = clone.Names[clone.Names.Count - 1];
				}
			}
			return clone;
		}
	}
}
