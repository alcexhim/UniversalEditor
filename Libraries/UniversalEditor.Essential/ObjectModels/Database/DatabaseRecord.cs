//
//  DatabaseRecord.cs - represents a record (row) in a DatabaseObjectModel
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
	/// Represents a record (row) in a <see cref="DatabaseObjectModel" />.
	/// </summary>
	public class DatabaseRecord : ICloneable
	{

		public class DatabaseRecordCollection
			: System.Collections.ObjectModel.Collection<DatabaseRecord>
		{
			public DatabaseRecord Add(params DatabaseField[] parameters)
			{
				DatabaseRecord dr = new DatabaseRecord();
				foreach (DatabaseField df in parameters)
				{
					dr.Fields.Add(df.Name, df.Value);
				}
				return dr;
			}
		}

		public DatabaseRecord(params DatabaseField[] fields)
		{
			for (int i = 0; i < fields.Length; i++)
			{
				Fields.Add(fields[i]);
			}
		}

		private DatabaseField.DatabaseFieldCollection mvarFields = new DatabaseField.DatabaseFieldCollection ();
		public DatabaseField.DatabaseFieldCollection Fields
		{
			get { return mvarFields; }
		}

		public object Clone()
		{
			DatabaseRecord clone = new DatabaseRecord();
			for (int i = 0; i < Fields.Count; i++)
			{
				clone.Fields.Add(Fields[i].Clone() as DatabaseField);
			}
			return clone;
		}
	}
}
