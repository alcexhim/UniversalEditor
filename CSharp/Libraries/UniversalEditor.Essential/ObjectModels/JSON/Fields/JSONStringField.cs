using System;

namespace UniversalEditor.ObjectModels.JSON.Fields
{
	/// <summary>
	/// Description of StringField.
	/// </summary>
	public class JSONStringField
		: JSONField
	{
		private string mvarValue = "";
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public override object Clone()
		{
			JSONStringField clone = new JSONStringField();
			clone.Value = (Value.Clone() as string);
			return clone;
		}
	}
}
