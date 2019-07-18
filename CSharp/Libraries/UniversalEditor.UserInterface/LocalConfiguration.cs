using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;

using MBS.Framework.Drawing;

namespace UniversalEditor.UserInterface
{
	public static class LocalConfiguration
	{
		public static string ApplicationName { get; set; } = "Universal Editor";
		public static string ApplicationShortName { get; set; } = "mbs-editor";
		public static string CompanyName { get; set; } = "Mike Becker's Software";

		public static SplashScreenSettings SplashScreen { get; } = new SplashScreenSettings();
		public static StartPageSettings StartPage { get; } = new StartPageSettings();
		public static ColorSchemeSettings ColorScheme { get; } = new ColorSchemeSettings();

		public static bool ConfirmExit { get; set; }

		// public static Icon MainIcon { get; set; }
	}

	public class StartPageSettings
	{
		private bool mvarShowOnStartup = true;
		public bool ShowOnStartup { get { return mvarShowOnStartup; } set { mvarShowOnStartup = value; } }

		private string mvarHeaderImageFileName = @"Images/SplashScreen/header2.png"; // null;
		public string HeaderImageFileName { get { return mvarHeaderImageFileName; } set { mvarHeaderImageFileName = value; } }
	}

	public class ColorSchemeSettings
	{
		public Color DarkColor { get; set; } = Color.FromRGBAByte(30, 30, 30);
		public Color LightColor { get; set; } = Color.FromRGBAByte(51, 51, 51);
		public Color BackgroundColor { get; set; } = Color.FromRGBAByte(77, 77, 77);
	}
}
