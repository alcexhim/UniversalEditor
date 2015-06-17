package net.alcetech.UniversalEditor;

import javax.swing.UIManager;

import net.alcetech.UniversalEditor.Windows.*;

public class Program
{
	private static void setTheme(String className)
	{
		try
		{
			UIManager.setLookAndFeel(className);
		}
		catch (Exception ex)
		{
			System.out.println("unable to initialize look and feel '" + className + "'");
		}
	}
	public static void main(String[] args)
	{
		setTheme(UIManager.getSystemLookAndFeelClassName());

		MainWindow mw = new MainWindow();
		mw.setVisible(true);
	}
}
