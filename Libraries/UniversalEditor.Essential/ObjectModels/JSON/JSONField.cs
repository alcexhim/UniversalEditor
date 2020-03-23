using System;
using UniversalEditor.ObjectModels.JSON.Fields;

namespace UniversalEditor.ObjectModels.JSON
{
	/// <summary>
	/// Description of Field.
	/// </summary>
	public abstract class JSONField : ICloneable
	{
		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		public class JSONFieldCollection
			: System.Collections.ObjectModel.Collection<JSONField>
		{
			public JSONNumberField Add(string Name, int Value)
			{
				JSONNumberField f = new JSONNumberField();
				f.Name = Name;
				f.Value = Value;
				base.Add(f);
				return f;
			}
			public JSONStringField Add(string Name, string Value)
			{
				JSONStringField f = new JSONStringField();
				f.Name = Name;
				f.Value = Value;
				base.Add(f);
				return f;
			}
			public JSONBooleanField Add(string Name, bool Value)
			{
				JSONBooleanField f = new JSONBooleanField();
				f.Name = Name;
				f.Value = Value;
				base.Add(f);
				return f;
			}
			public JSONArrayField Add(string Name, params string[] Values)
			{
				JSONArrayField f = new JSONArrayField();
				f.Name = Name;
				foreach(string s in Values)
				{
					f.Values.Add(s);
				}
				base.Add(f);
				return f;
			}
			public JSONObjectField Add(string Name, JSONObject Value)
			{
				JSONObjectField f = new JSONObjectField();
				f.Name = Name;
				Value.Name = Name;
				f.Value = Value;
				base.Add(f);
				return f;
			}
		}

		public abstract object Clone();
	}
}
