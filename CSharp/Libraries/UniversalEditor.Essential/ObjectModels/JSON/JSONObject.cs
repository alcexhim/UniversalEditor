using System;

namespace UniversalEditor.ObjectModels.JSON
{
	/// <summary>
	/// Description of Object.
	/// </summary>
	public class JSONObject : ICloneable
	{
		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		private JSONField.JSONFieldCollection mvarFields = new JSONField.JSONFieldCollection();
		public JSONField.JSONFieldCollection Fields { get { return mvarFields; } }
		
		public class JSONObjectCollection
			: System.Collections.ObjectModel.Collection<JSONObject>
		{
			public JSONObject Add()
			{
				return Add("");
			}
			public JSONObject Add(string Name)
			{
				JSONObject o = new JSONObject();
				o.Name = Name;
				base.Add(o);
				return o;
			}
			
			public JSONObject this[string Name]
			{
				get
				{
					foreach(JSONObject o in this)
					{
						if (o.Name == Name)
						{
							return o;
						}
					}
					return null;
				}
			}
			public bool Contains(string Name)
			{
				return (this[Name] != null);
			}
			public bool Remove(string Name)
			{
				JSONObject o = this[Name];
				if (o != null)
				{
					base.Remove(o);
					return true;
				}
				return false;
			}
		}

		public object Clone()
		{
			JSONObject clone = new JSONObject();
			foreach (JSONField field in Fields)
			{
				clone.Fields.Add(field.Clone() as JSONField);
			}
			clone.Name = (Name.Clone() as string);
			return clone;
		}
	}
}
