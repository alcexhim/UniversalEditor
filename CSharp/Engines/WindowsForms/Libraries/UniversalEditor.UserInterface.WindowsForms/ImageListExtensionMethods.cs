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
					if (iml.ImageSize.Width == 16 && iml.ImageSize.Height == 16)
					{
						Icon iconFile = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 0, IconMethods.IconSize.Small);
						iml.Images.Add("generic-file", iconFile);
						Icon iconDocument = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 1, IconMethods.IconSize.Small);
						iml.Images.Add("generic-document", iconDocument);
						Icon iconApplication = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 2, IconMethods.IconSize.Small);
						iml.Images.Add("generic-application", iconApplication);
						Icon iconFolderClosed = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 3, IconMethods.IconSize.Small);
						iml.Images.Add("generic-folder-closed", iconFolderClosed);
						Icon iconFolderOpen = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 4, IconMethods.IconSize.Small);
						iml.Images.Add("generic-folder-open", iconFolderOpen);
					}
					else
					{
						Icon iconFile = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 0, IconMethods.IconSize.Large);
						iml.Images.Add("generic-file", iconFile);
						Icon iconDocument = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 1, IconMethods.IconSize.Large);
						iml.Images.Add("generic-document", iconDocument);
						Icon iconApplication = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 2, IconMethods.IconSize.Large);
						iml.Images.Add("generic-application", iconApplication);
						Icon iconFolderClosed = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 3, IconMethods.IconSize.Large);
						iml.Images.Add("generic-folder-closed", iconFolderClosed);
						Icon iconFolderOpen = IconMethods.ExtractAssociatedIcon(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "shell32.dll", 4, IconMethods.IconSize.Large);
						iml.Images.Add("generic-folder-open", iconFolderOpen);
					}
					return;
				}
			}
			// throw new PlatformNotSupportedException();
		}
	}
}
