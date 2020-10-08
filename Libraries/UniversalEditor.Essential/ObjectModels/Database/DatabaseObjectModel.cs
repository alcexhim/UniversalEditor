//
//  DatabaseObjectModel.cs - provides an ObjectModel for manipulating databases
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

using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.ObjectModels.Database
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating databases.
	/// </summary>
	public class DatabaseObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Database" };
			}
			return _omr;
		}

		public string Name { get; set; } = null;
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

		public static DatabaseObjectModel FromMarkup(MarkupTagElement tag)
		{
			DatabaseObjectModel db = new DatabaseObjectModel();
			for (int i = 0; i < tag.Elements.Count; i++)
			{
				MarkupTagElement tag2 = (tag.Elements[i] as MarkupTagElement);
				if (tag2 == null) continue;
				if (tag2.FullName == "Tables")
				{
					foreach (MarkupElement elTable in tag2.Elements )
					{
						MarkupTagElement tagTable = (elTable as MarkupTagElement);
						if (tagTable == null) continue;
						if (tagTable.FullName != "Table") continue;

						MarkupAttribute attName = tag2.Attributes["Name"];
						if (attName == null) continue;

						DatabaseTable dt = new DatabaseTable();
						dt.Name = attName.Value;
						db.Tables.Add(dt);
					}
				}
			}
			return db;
		}
	}
}
