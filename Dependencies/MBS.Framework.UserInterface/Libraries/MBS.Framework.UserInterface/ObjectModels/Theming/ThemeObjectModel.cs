using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class ThemeObjectModel : ObjectModel
	{
		private Theme.ThemeCollection mvarThemes = new Theme.ThemeCollection();
		public Theme.ThemeCollection Themes { get { return mvarThemes; } }

		public override void Clear()
		{
			mvarThemes.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			ThemeObjectModel clone = (where as ThemeObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (Theme theme in mvarThemes)
			{
				clone.Themes.Add(theme.Clone() as Theme);
			}
		}
	}
}
