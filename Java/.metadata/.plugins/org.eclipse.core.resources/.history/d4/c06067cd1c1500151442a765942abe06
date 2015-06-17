package net.alcetech.UserInterface.Theming;

import java.awt.Image;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

import javax.imageio.ImageIO;
import javax.swing.Icon;
import javax.swing.ImageIcon;

public class ThemeManager
{

	private static String mvarCurrentThemeName = "Default";
	
	public static String[] GetThemedIconFileNames(String iconName)
	{
		return new String[]
		{
			"Themes/" + mvarCurrentThemeName + "/Icons/32x32/" + iconName + ".png",
			"Themes/" + mvarCurrentThemeName + "/Icons/16x16/" + iconName + ".png"
		};
	}
	public static ImageIcon GetThemedIcon(String iconName, String size)
	{
		ImageIcon image = new ImageIcon("Themes/" + mvarCurrentThemeName + "/Icons/" + size + "/" + iconName + ".png");
		return image;
	}
	
	public static ArrayList<Image> GetThemedIconImages(String iconName)
	{
		ArrayList<Image> images = new ArrayList<Image>();

		String lastFileName = "";
		String[] files = GetThemedIconFileNames(iconName);
		try
		{
			for (int i = 0; i < files.length; i++)
			{
				File file = new File(files[i]);
				lastFileName = file.getAbsolutePath();
				
				images.add(ImageIO.read(file));
			}
		}
		catch (IOException e)
		{
			// TODO Auto-generated catch block
			System.out.println("Could not load icon '" + iconName + "' for theme '" + mvarCurrentThemeName + "' at '" + lastFileName + "'");
		}
		
		return images;
	}
}
