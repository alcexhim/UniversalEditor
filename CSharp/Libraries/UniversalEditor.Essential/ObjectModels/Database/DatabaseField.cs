using System;
namespace UniversalEditor.ObjectModels.Database
{
	public class DatabaseField : ICloneable
	{

		public class DatabaseFieldCollection
			: System.Collections.ObjectModel.Collection<DatabaseField>
		{
			private System.Collections.Generic.Dictionary<string, DatabaseField> fieldsByName = new System.Collections.Generic.Dictionary<string, DatabaseField>();
			public DatabaseField Add(string Name)
			{
				return Add(Name, String.Empty);
			}
			public DatabaseField Add(string Name, object Value)
			{
				DatabaseField df = new DatabaseField();
				df.Name = Name;
				df.Value = Value;

				base.Add(df);
				return df;
			}

			public DatabaseField this[string Name]
			{
				get
				{
					return fieldsByName[Name];
				}
			}
		}

		public string Name { get; set; } = String.Empty;
		public object Value { get; set; } = null;

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
	}
}

