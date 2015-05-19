using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.CoreObject
{
	public class CoreObjectAttribute : ICloneable
	{
		public class CoreObjectAttributeCollection
			: System.Collections.ObjectModel.Collection<CoreObjectAttribute>
		{
			public CoreObjectAttribute Add(string name, string value = null)
			{
				CoreObjectAttribute item = new CoreObjectAttribute(name, value);
				Add(item);
				return item;
			}
		}

		public CoreObjectAttribute()
		{
			mvarName = String.Empty;
		}
		public CoreObjectAttribute(string name, params string[] values)
		{
			mvarName = name;
			foreach (string value in values)
			{
				mvarValues.Add(value);
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private System.Collections.Specialized.StringCollection mvarValues = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection Values { get { return mvarValues; } }

		public object Clone()
		{
			CoreObjectAttribute clone = new CoreObjectAttribute();
			clone.Name = (mvarName.Clone() as string);
			foreach (string value in mvarValues)
			{
				clone.Values.Add(value.Clone() as string);
			}
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarName);
			if (mvarValues.Count > 0)
			{
				sb.Append("=");
				for (int i = 0; i < mvarValues.Count; i++)
				{
					sb.Append(mvarValues[i]);
					if (i < mvarValues.Count - 1) sb.Append(",");
				}
			}
			return sb.ToString();
		}
	}
}
