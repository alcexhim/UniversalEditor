using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class ThemeStockImage : ICloneable
	{
		public class ThemeStockImageCollection
			: System.Collections.ObjectModel.Collection<ThemeStockImage>
		{
			public ThemeStockImage this[string name]
			{
				get
				{
					foreach (ThemeStockImage item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarImageFileName = null;
		public string ImageFileName { get { return mvarImageFileName; } set { mvarImageFileName = value; } }

		public object Clone()
		{
			ThemeStockImage clone = new ThemeStockImage();
			if (mvarImageFileName != null) clone.ImageFileName = (mvarImageFileName.Clone() as string);
			if (mvarName != null) clone.Name = (mvarName.Clone() as string);
			return clone;
		}
	}
}
