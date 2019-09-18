using System;

namespace UniversalEditor.ObjectModels.JSON.Fields
{
	/// <summary>
	/// Description of NumberField.
	/// </summary>
	public class JSONNumberField
		: JSONField
	{
		private int mvarValue = 0;
		public int Value { get { return mvarValue; } set { mvarValue = value; } }

		public override object Clone()
		{
			JSONNumberField clone = new JSONNumberField();
			clone.Value = Value;
			return clone;
		}
	}
}
