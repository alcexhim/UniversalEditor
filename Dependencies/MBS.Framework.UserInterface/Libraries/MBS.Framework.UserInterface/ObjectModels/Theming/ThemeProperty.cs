using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class ThemeProperty : ICloneable
	{
		public class ThemePropertyCollection
			: System.Collections.ObjectModel.Collection<ThemeProperty>
		{
			public ThemeProperty this[string name]
			{
				get
				{
					foreach (ThemeProperty item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarValue = null;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			ThemeProperty clone = new ThemeProperty();
			clone.Name = (mvarName.Clone() as string);
			clone.Value = (mvarValue.Clone() as string);
			return clone;
		}
	}
}
