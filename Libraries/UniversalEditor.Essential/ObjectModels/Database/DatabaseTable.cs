//
//  DatabaseTable.cs - represents a table in a DatabaseObjectModel
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

namespace UniversalEditor.ObjectModels.Database
{
	/// <summary>
	/// Represents a table in a <see cref="DatabaseObjectModel" />.
	/// </summary>
	public class DatabaseTable : ICloneable
	{
		public class DatabaseTableCollection
			: System.Collections.ObjectModel.Collection<DatabaseTable>
		{
		}

		/// <summary>
		/// Gets or sets the name of the <see cref="DatabaseTable" />.
		/// </summary>
		/// <value>The name of the <see cref="DatabaseTable" />.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="DatabaseField" /> instances representing the fields (columns) in the <see cref="DatabaseTable" />.
		/// </summary>
		/// <value>The fields (columns) in the <see cref="DatabaseTable" />.</value>
		public DatabaseField.DatabaseFieldCollection Fields { get; } = new DatabaseField.DatabaseFieldCollection();
		/// <summary>
		/// Gets a collection of <see cref="DatabaseRecord" /> instances representing the records (rows) in the <see cref="DatabaseTable" />.
		/// </summary>
		/// <value>The records (rows) in the <see cref="DatabaseTable" />.</value>
		public DatabaseRecord.DatabaseRecordCollection Records { get; } = new DatabaseRecord.DatabaseRecordCollection();

		public object Clone()
		{
			DatabaseTable clone = new DatabaseTable();
			clone.Name = (Name.Clone() as string);
			for (int i = 0; i < Fields.Count; i++)
			{
				clone.Fields.Add(Fields[i].Clone() as DatabaseField);
			}
			for (int i = 0; i < Records.Count; i++)
			{
				clone.Records.Add(Records[i].Clone() as DatabaseRecord);
			}
			return clone;
		}
	}
}