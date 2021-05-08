//
//  Relationship.cs - defines a package relationship for an Open Packaging Convention document
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
using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Package.Relationships
{
	/// <summary>
	/// Defines a package relationship for an Open Packaging Convention document.
	/// </summary>
	public class Relationship : ICloneable
	{
		public class RelationshipCollection
			: System.Collections.ObjectModel.Collection<Relationship>
		{
			/// <summary>
			/// Gets all <see cref="Relationship" />s with the specified schema.
			/// </summary>
			/// <param name="schema">The schema to search for.</param>
			/// <returns>Array of <see cref="Relationship" /> objects whose Schema property is set to the specified value.</returns>
			public Relationship[] GetBySchema(string schema, string source = null)
			{
				List<Relationship> rels = new List<Relationship>();
				foreach (Relationship rel in this)
				{
					if (rel.Schema == schema && rel.Source == source)
					{
						rels.Add(rel);
					}
				}
				return rels.ToArray();
			}
		}

		private string mvarID = String.Empty;
		public string ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarSchema = String.Empty;
		public string Schema { get { return mvarSchema; } set { mvarSchema = value; } }

		private string mvarTarget = String.Empty;
		public string Target { get { return mvarTarget; } set { mvarTarget = value; } }

		private string mvarSource = null;
		public string Source { get { return mvarSource; } set { mvarSource = value; } }

		public object Clone()
		{
			Relationship clone = new Relationship();
			clone.ID = (mvarID.Clone() as string);
			clone.Schema = (mvarSchema.Clone() as string);
			clone.Source = (mvarSource.Clone() as string);
			clone.Target = (mvarTarget.Clone() as string);
			return clone;
		}
	}
}
