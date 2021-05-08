//
//  Surname.cs
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
	public class Surname : ICloneable
	{
		public class SurnameCollection
			: System.Collections.ObjectModel.Collection<Surname>
		{

		}

		public string Prefix { get; set; } = null;
		public string Name { get; set; } = null;
		public string Connector { get; set; } = null;
		public SurnameOrigin Origin { get; set; } = SurnameOrigin.Unknown;
		public bool Primary { get; set; } = true;

		public Surname()
		{
		}
		public Surname(string name)
			: this(null, name)
		{
		}
		public Surname(string prefix, string name)
			: this(prefix, name, null)
		{
		}
		public Surname(string prefix, string name, string connector)
			: this(prefix, name, connector, SurnameOrigin.Unknown)
		{
		}
		public Surname(string prefix, string name, string connector, SurnameOrigin origin)
			: this(prefix, name, connector, origin, true)
		{
		}
		public Surname(string prefix, string name, string connector, SurnameOrigin origin, bool primary)
		{
			Prefix = prefix;
			Name = name;
			Connector = connector;
			Origin = origin;
			Primary = primary;
		}

		public object Clone()
		{
			Surname clone = new Surname();
			clone.Prefix = (Prefix?.Clone() as string);
			clone.Name = (Name?.Clone() as string);
			clone.Connector = (Connector?.Clone() as string);
			clone.Origin = (Origin?.Clone() as SurnameOrigin);
			clone.Primary = Primary;
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Prefix != null)
			{
				sb.Append(Prefix);
				sb.Append(' ');
			}
			if (Name != null)
			{
				sb.Append(Name);
				sb.Append(' ');
			}
			if (Connector != null)
			{
				sb.Append(Connector);
				sb.Append(' ');
			}
			return sb.ToString().Trim();
		}
	}
}
