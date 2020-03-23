//
//  DatabaseObjectModel.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
	public class DatabaseObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Database";
				_omr.Path = new string[] { "General", "Database" };
			}
			return _omr;
		}

		public DatabaseTable.DatabaseTableCollection Tables { get; } = new DatabaseTable.DatabaseTableCollection();

		public override void Clear()
		{
			Tables.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			DatabaseObjectModel clone = (where as DatabaseObjectModel);
			if (clone == null)
				throw new ObjectModelNotSupportedException();

			for (int i = 0; i < Tables.Count; i++)
			{
				clone.Tables.Add(Tables[i].Clone() as DatabaseTable);
			}
		}
	}
}
