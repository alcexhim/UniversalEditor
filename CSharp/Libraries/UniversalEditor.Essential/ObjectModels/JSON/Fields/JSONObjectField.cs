using System;

namespace UniversalEditor.ObjectModels.JSON.Fields
{
	/// <summary>
	/// Description of ObjectField.
	/// </summary>
	public class JSONObjectField : JSONField
	{
		private JSONObject mvarValue = null;
		public JSONObject Value { get { return mvarValue; } set { mvarValue = value; } }

		public override object Clone()
		{
			JSONObjectField clone = new JSONObjectField();
			clone.Value = (Value.Clone() as JSONObject);
			return clone;
		}
	}
}
