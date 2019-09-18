using System;

namespace UniversalEditor.ObjectModels.JSON.Fields
{
	/// <summary>
	/// Description of BooleanField.
	/// </summary>
	public class JSONBooleanField
		: JSONField
	{
		private bool mvarValue = false;
		public bool Value { get { return mvarValue; } set { mvarValue = value; } }

		public override object Clone()
		{
			JSONBooleanField clone = new JSONBooleanField();
			clone.Value = Value;
			return clone;
		}
	}
}
