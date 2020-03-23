using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.Accessors;

using MBS.Framework.UserInterface.ObjectModels.Theming;
using MBS.Framework.UserInterface.DataFormats.Theming;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface.Theming
{
	public static class ThemeManager
	{
		private static Theme mvarCurrentTheme = null;
		public static Theme CurrentTheme { get { return mvarCurrentTheme; } set { mvarCurrentTheme = value; } }

		private static Theme[] mvarAvailableThemes = null;
		public static Theme[] Get()
		{
			if (mvarAvailableThemes == null)
			{
				List<Theme> list = new List<Theme>();
				string themeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + System.IO.Path.DirectorySeparatorChar.ToString() + "Themes";
				if (System.IO.Directory.Exists(themeDir))
				{
					string[] themeFileNames = System.IO.Directory.GetFiles(themeDir, "*.xml", System.IO.SearchOption.AllDirectories);
					foreach (string themeFileName in themeFileNames)
					{
						if (System.IO.File.Exists(themeFileName))
						{
							ThemeObjectModel theme = new ThemeObjectModel();
							UniversalEditor.Document.Load(theme, new ThemeXMLDataFormat(), new FileAccessor(themeFileName));
							foreach (Theme themeDefinition in theme.Themes)
							{
								list.Add(themeDefinition);
							}
						}
					}
				}
				mvarAvailableThemes = list.ToArray();

				foreach (Theme theme in mvarAvailableThemes)
				{
					if (theme.InheritsThemeID != Guid.Empty)
					{
						Theme ct1 = GetByID(theme.InheritsThemeID);
						if (ct1 == null)
						{
							Console.WriteLine("ac-theme: custom theme with ID '" + theme.InheritsThemeID.ToString("B").ToUpper() + "' not found");
						}
						else
						{
							theme.InheritsTheme = ct1;
						}
					}
					foreach (ThemeComponent tc in theme.Components)
					{
						if (tc.InheritsComponentID != Guid.Empty)
						{
							tc.InheritsComponent = theme.Components[tc.InheritsComponentID];
						}
					}
				}
			}
			return mvarAvailableThemes;
		}

		public static Theme GetByID(Guid id)
		{
			Theme[] themes = Get();
			foreach (Theme theme in themes)
			{
				if (theme.ID == id) return theme;
			}
			return null;
		}
	}
}
