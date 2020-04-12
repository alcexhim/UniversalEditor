//
//  DatabaseField.cs - represents a field (column) in a DatabaseObjectModel
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

namespace UniversalEditor.ObjectModels.Database
{
	/// <summary>
	/// Represents a field (column) in a <see cref="DatabaseObjectModel" />.
	/// </summary>
	public class DatabaseField : ICloneable
	{

		public class DatabaseFieldCollection
			: System.Collections.ObjectModel.Collection<DatabaseField>
		{
			public DatabaseField Add(string Name, object Value = null, Type dataType = null)
			{
				DatabaseField df = new DatabaseField();
				df.Name = Name;
				df.Value = Value;
				df.DataType = dataType;

				base.Add(df);
				return df;
			}

			public DatabaseField this[string Name]
			{
				get
				{
					for (int i = 0; i < Count; i++)
					{
						if (this[i].Name.Equals(Name)) return this[i];
					}
					return null;
				}
			}
		}

		public DatabaseField(string name = "", object value = null)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; } = String.Empty;
		public object Value { get; set; } = null;
		public Type DataType { get; set; } = null;

		public object Clone()
		{
			DatabaseField clone = new DatabaseField();
			clone.Name = (Name.Clone() as string);
			if (Value is ICloneable)
			{
				clone.Value = (Value as ICloneable).Clone();
			}
			else
			{
				clone.Value = Value;
			}
			return clone;
		}

		public override string ToString()
		{
			return String.Format("{0} = {1}", Name, Value);
		}
	}
}

