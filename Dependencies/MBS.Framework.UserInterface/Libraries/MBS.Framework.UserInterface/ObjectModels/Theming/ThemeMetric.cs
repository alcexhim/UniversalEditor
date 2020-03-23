using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public abstract class ThemeMetric : ICloneable
	{
		public class ThemeMetricCollection
			: System.Collections.ObjectModel.Collection<ThemeMetric>
		{
			public ThemeMetric this[string name]
			{
				get
				{
					foreach (ThemeMetric item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public abstract object Clone();
	}
}
