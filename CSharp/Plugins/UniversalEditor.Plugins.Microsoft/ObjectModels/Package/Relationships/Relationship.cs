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

		}

		private string mvarID = String.Empty;
		public string ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarSchema = String.Empty;
		public string Schema { get { return mvarSchema; } set { mvarSchema = value; } }

		private string mvarTarget = String.Empty;
		public string Target { get { return mvarTarget; } set { mvarTarget = value; } }



		public object Clone()
		{
			Relationship clone = new Relationship();
			clone.ID = (mvarID.Clone() as string);
			clone.Schema = (mvarSchema.Clone() as string);
			clone.Target = (mvarTarget.Clone() as string);
			return clone;
		}
	}
}
