using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class Theme : ICloneable
	{
		public class ThemeCollection
			: System.Collections.ObjectModel.Collection<Theme>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarAuthor = String.Empty;
		public string Author { get { return mvarAuthor; } set { mvarAuthor = value; } }

		public string GetBasePath()
		{
			System.Reflection.Assembly entryAsm = System.Reflection.Assembly.GetEntryAssembly();
			if (entryAsm == null) return String.Empty;

			string pathName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
			{
				System.IO.Path.GetDirectoryName(entryAsm.Location),
				"Themes",
				this.ID.ToString("B").ToUpper()
			});
			return pathName;
		}

		private Dictionary<string, string> mvarThemeColorOverrides = new Dictionary<string, string>();
		public void SetColor(string name, string value)
		{
			mvarThemeColorOverrides[name] = value;
		}

		private Dictionary<string, string> mvarThemeFontOverrides = new Dictionary<string, string>();
		public void SetFont(string name, string value)
		{
			mvarThemeFontOverrides[name] = value;
		}

		private ThemeColor.ThemeColorCollection mvarColors = new ThemeColor.ThemeColorCollection();
		public ThemeColor.ThemeColorCollection Colors { get { return mvarColors; } }

		private ThemeFont.ThemeFontCollection mvarFonts = new ThemeFont.ThemeFontCollection();
		public ThemeFont.ThemeFontCollection Fonts { get { return mvarFonts; } }

		private ThemeMetric.ThemeMetricCollection mvarMetrics = new ThemeMetric.ThemeMetricCollection();
		public ThemeMetric.ThemeMetricCollection Metrics { get { return mvarMetrics; } }

		private ThemeComponent.ThemeComponentCollection mvarComponents = new ThemeComponent.ThemeComponentCollection();
		public ThemeComponent.ThemeComponentCollection Components { get { return mvarComponents; } }

		private ThemeStockImage.ThemeStockImageCollection mvarStockImages = new ThemeStockImage.ThemeStockImageCollection();
		public ThemeStockImage.ThemeStockImageCollection StockImages { get { return mvarStockImages; } }

		private ThemeProperty.ThemePropertyCollection mvarProperties = new ThemeProperty.ThemePropertyCollection();
		public ThemeProperty.ThemePropertyCollection Properties { get { return mvarProperties; } }

		public object Clone()
		{
			Theme clone = new Theme();
			clone.Author = (mvarAuthor.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
			clone.ID = mvarID;
			foreach (ThemeColor item in mvarColors)
			{
				clone.Colors.Add(item.Clone() as ThemeColor);
			}
			foreach (ThemeFont item in mvarFonts)
			{
				clone.Fonts.Add(item.Clone() as ThemeFont);
			}
			foreach (ThemeMetric item in mvarMetrics)
			{
				clone.Metrics.Add(item.Clone() as ThemeMetric);
			}
			foreach (ThemeComponent item in mvarComponents)
			{
				clone.Components.Add(item.Clone() as ThemeComponent);
			}
			foreach (ThemeStockImage item in mvarStockImages)
			{
				clone.StockImages.Add(item.Clone() as ThemeStockImage);
			}
			foreach (ThemeProperty item in mvarProperties)
			{
				clone.Properties.Add(item.Clone() as ThemeProperty);
			}
			return clone;
		}

		private Guid mvarInheritsThemeID = Guid.Empty;
		public Guid InheritsThemeID { get { return mvarInheritsThemeID; } set { mvarInheritsThemeID = value; } }

		private Theme mvarInheritsTheme = null;
		public Theme InheritsTheme { get { return mvarInheritsTheme; } set { mvarInheritsTheme = value; } }

		private string mvarBasePath = null;
		public string BasePath { get { return mvarBasePath; } set { mvarBasePath = value; } }

		public ThemeComponent GetComponent(Guid id, Theme theme = null)
		{
			if (theme == null) theme = this;

			ThemeComponent tc = theme.Components[id];
			if (tc == null && theme.InheritsTheme != null) return GetComponent(id, theme.InheritsTheme);

			return tc;
		}

		public Color GetColorFromString(string value, Theme theme = null)
		{
			if (theme == null) theme = this;

			if (value.StartsWith("@"))
			{
                string name = value.Substring(1);
				if (mvarThemeColorOverrides.ContainsKey(name))
				{
					if (mvarThemeColorOverrides[name] == name) return Color.Empty;
					return GetColorFromString(mvarThemeColorOverrides[name]);
				}

                if (!theme.Colors.Contains(name))
                {
					if (theme.InheritsTheme != null) return GetColorFromString(value, theme.InheritsTheme);

                    Console.WriteLine("ac-theme: theme definition does not contain color '" + name + "'");
                    return Color.Empty;
                }
				string color = theme.Colors[name].Value;
				return GetColorFromString(color);
			}
			else if (value.StartsWith("#") && value.Length == 7)
			{
				string RRGGBB = value.Substring(1);
				byte RR = Byte.Parse(RRGGBB.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
				byte GG = Byte.Parse(RRGGBB.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
				byte BB = Byte.Parse(RRGGBB.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
				return Color.FromRGBAByte(RR, GG, BB);
			}
			else if (value.StartsWith("rgb(") && value.EndsWith(")"))
			{
				string r_g_b = value.Substring(3, value.Length - 4);
				string[] rgb = r_g_b.Split(new char[] { ',' });
				if (rgb.Length == 3)
				{
					byte r = Byte.Parse(rgb[0].Trim());
					byte g = Byte.Parse(rgb[1].Trim());
					byte b = Byte.Parse(rgb[2].Trim());
				}
			}
			else if (value.StartsWith("rgba(") && value.EndsWith(")"))
			{

			}
			else
			{
				try
				{
					Color color = Color.FromString(value);
					return color;
				}
				catch
				{

				}
			}
			return Color.Empty;
		}
	}
}
