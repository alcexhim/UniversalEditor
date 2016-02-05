using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Package.Relationships
{
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
