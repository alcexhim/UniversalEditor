using System;

namespace UniversalEditor.ObjectModels.Database
{
	public class DatabaseTable : ICloneable
	{
		public class DatabaseTableCollection
			: System.Collections.ObjectModel.Collection<DatabaseTable>
		{
		}

		public string Name { get; set; } = String.Empty;

		private DatabaseField.DatabaseFieldCollection mvarFields = new DatabaseField.DatabaseFieldCollection();
		public DatabaseField.DatabaseFieldCollection Fields
		{
			get { return mvarFields; }
		}
		
		private DatabaseRecord.DatabaseRecordCollection mvarRecords = new DatabaseRecord.DatabaseRecordCollection();
		public DatabaseRecord.DatabaseRecordCollection Records
		{
			get { return mvarRecords; }
		}

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