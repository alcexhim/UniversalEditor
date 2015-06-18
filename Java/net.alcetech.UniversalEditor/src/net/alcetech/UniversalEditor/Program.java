package net.alcetech.UniversalEditor;

import net.alcetech.Core.*;
import net.alcetech.UniversalEditor.Windows.*;
import net.alcetech.UserInterface.Theming.*;
import net.alcetech.UniversalEditor.Core.Accessors.*;
import net.alcetech.UniversalEditor.Core.IO.*;
import net.alcetech.UniversalEditor.ObjectModels.Markup.*;
import net.alcetech.UniversalEditor.ObjectModels.Markup.Elements.*;

public class Program
{
	public static void main(String[] args)
	{
		ThemeManager.Initialize();
		
		FileAccessor fa = new FileAccessor("/var/tmp/test.xml");

		/*
		Writer writer = new Writer(fa);
		try
		{
			fa.open();
			writer.setEndianness(Endianness.BigEndian);
			writer.writeSingle(3.141578f);
			
			fa.close();
		}
		catch (IOException e)
		{
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		*/
		
		Command mnuFile = new Command("mnuFile", "_File");
		Command mnuEdit = new Command("mnuEdit", "_Edit");
		Command mnuView = new Command("mnuView", "_View");
		Command mnuProject = new Command("mnuProject", "_Project");
		Command mnuBuild = new Command("mnuBuild", "_Build");
		Command mnuDebug = new Command("mnuDebug", "_Debug");
		Command mnuTools = new Command("mnuTools", "_Tools");
		Command mnuWindow = new Command("mnuWindow", "_Window");
		Command mnuHelp = new Command("mnuHelp", "_Help");
		
		mnuFile.getCommandCollection().add(new Command("mnuFileExit", "Exit"));
		
		Application.getCommandCollection().add(mnuFile);
		Application.getCommandCollection().add(mnuEdit);
		Application.getCommandCollection().add(mnuView);
		Application.getCommandCollection().add(mnuProject);
		Application.getCommandCollection().add(mnuBuild);
		Application.getCommandCollection().add(mnuDebug);
		Application.getCommandCollection().add(mnuTools);
		Application.getCommandCollection().add(mnuWindow);
		Application.getCommandCollection().add(mnuHelp);
		
		Application.addCommandListener(new ICommandListener()
		{
			@Override
			public void onCommandExecuted(CommandEventArgs e)
			{
				// TODO Auto-generated method stub
				System.out.println("Command '" + e.getCommand().getName() + "' executed!");
			}
		});
		
		MarkupObjectModel mom = new MarkupObjectModel();
		mom.getElementCollection().add(new MarkupTagElement("test", "honduras"));
		
		MainWindow mw = new MainWindow();
		mw.setVisible(true);
	}
}
