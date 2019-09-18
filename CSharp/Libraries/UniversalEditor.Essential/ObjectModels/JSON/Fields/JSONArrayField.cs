using System;

namespace UniversalEditor.ObjectModels.JSON.Fields
{
	/// <summary>
	/// Description of ArrayField.
	/// </summary>
	public class JSONArrayField : JSONField
	{
		private System.Collections.Specialized.StringCollection mvarValues = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection Values { get { return mvarValues; } }

		public override object Clone()
		{
			JSONArrayField clone = new JSONArrayField();
			foreach (string value in Values)
			{
				clone.Values.Add(value.Clone() as string);
			}
			return clone;
		}
	}
}
