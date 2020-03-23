using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class ThemeFont
	{
		public class ThemeFontCollection
			: System.Collections.ObjectModel.Collection<ThemeFont>
		{
			public ThemeFont this[string name]
			{
				get
				{
					foreach (ThemeFont item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
			public bool Contains(string name)
			{
				return (this[name] != null);
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private String mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			ThemeFont clone = new ThemeFont();
			clone.Name = (mvarName.Clone() as string);
			clone.Value = (mvarValue.Clone() as string);
			return clone;
		}
	}
}
