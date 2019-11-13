using System;

namespace UniversalEditor.ObjectModels.Database
{
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