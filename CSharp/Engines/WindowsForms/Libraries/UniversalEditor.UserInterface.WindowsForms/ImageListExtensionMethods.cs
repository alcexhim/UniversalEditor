using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public static class ImageListExtensionMethods
	{
		public static void PopulateSystemIcons(this System.Windows.Forms.TreeView tv)
		{
			System.Windows.Forms.ImageList iml = new System.Windows.Forms.ImageList();
			iml.ImageSize = new Size(16, 16);
			iml.PopulateSystemIcons();

			tv.ImageList = iml;
		}
		public static void PopulateSystemIcons(this System.Windows.Forms.ListView lv)
		{
			System.Windows.Forms.ImageList imlSmallIcons = new System.Windows.Forms.ImageList();
			imlSmallIcons.ImageSize = new Size(16, 16);
			System.Windows.Forms.ImageList imlLargeIcons = new System.Windows.Forms.ImageList();
			imlLargeIcons.ImageSize = new Size(32, 32);

			lv.LargeImageList = imlLargeIcons;
			lv.SmallImageList = imlSmallIcons;
		}
		public static void PopulateSystemIcons(this System.Windows.Forms.ImageList iml)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					break;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					string strSize = "32x32";
					IconMethods.IconSize icnSize = IconMethods.IconSize.Large;
					
					if (iml.ImageSize.Width == 16 && iml.ImageSize.Height == 16)
					{
						strSize = "16x16";
						icnSize = IconMethods.IconSize.Small;
					}

					Image imageFile = AwesomeControls.Theming.Theme.CurrentTheme.GetImage("ImageList/" + strSize + "/generic-file.png");
					if (imageFile != null)
					{
						iml.Images.Add("generic-file", imageFile);
					}
					else
					{
						Icon iconFile = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 0, icnSize);
						iml.Images.Add("generic-file", iconFile);
					}

					Image imageDocument = AwesomeControls.Theming.Theme.CurrentTheme.GetImage("ImageList/" + strSize + "/generic-document.png");
					if (imageDocument != null)
					{
						iml.Images.Add("generic-document", imageDocument);
					}
					else
					{
						Icon iconDocument = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 1, icnSize);
						iml.Images.Add("generic-document", iconDocument);
					}
						
					Image imageApplication = AwesomeControls.Theming.Theme.CurrentTheme.GetImage("ImageList/" + strSize + "/generic-application.png");
					if (imageApplication != null)
					{
						iml.Images.Add("generic-application", imageApplication);
					}
					else
					{
						Icon iconApplication = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 2, icnSize);
						iml.Images.Add("generic-application", iconApplication);
					}
						
					Image imageFolderClosed = AwesomeControls.Theming.Theme.CurrentTheme.GetImage("ImageList/" + strSize + "/generic-folder-closed.png");
					if (imageFolderClosed != null)
					{
						iml.Images.Add("generic-folder-closed", imageFolderClosed);
					}
					else
					{
						Icon iconFolderClosed = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 3, icnSize);
						iml.Images.Add("generic-folder-closed", iconFolderClosed);
					}
						
					Image imageFolderOpen = AwesomeControls.Theming.Theme.CurrentTheme.GetImage("ImageList/" + strSize + "/generic-folder-open.png");
					if (imageFolderOpen != null)
					{
						iml.Images.Add("generic-folder-open", imageFolderOpen);
					}
					else
					{
						Icon iconFolderOpen = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 4, icnSize);
						iml.Images.Add("generic-folder-open", iconFolderOpen);
					}
					return;
				}
			}
			// throw new PlatformNotSupportedException();
		}
	}
}
