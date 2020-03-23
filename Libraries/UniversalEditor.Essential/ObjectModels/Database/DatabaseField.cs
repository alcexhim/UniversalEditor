using System;
namespace UniversalEditor.ObjectModels.Database
{
	public class DatabaseField : ICloneable
	{

		public class DatabaseFieldCollection
			: System.Collections.ObjectModel.Collection<DatabaseField>
		{
			public DatabaseField Add(string Name, object Value = null, Type dataType = null)
			{
				DatabaseField df = new DatabaseField();
				df.Name = Name;
				df.Value = Value;
				df.DataType = dataType;

				base.Add(df);
				return df;
			}

			public DatabaseField this[string Name]
			{
				get
				{
					for (int i = 0; i < Count; i++)
					{
						if (this[i].Name.Equals(Name)) return this[i];
					}
					return null;
				}
			}
		}

		public DatabaseField(string name = "", object value = null)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; } = String.Empty;
		public object Value { get; set; } = null;
		public Type DataType { get; set; } = null;

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

		public override string ToString()
		{
			return String.Format("{0} = {1}", Name, Value);
		}
	}
}

