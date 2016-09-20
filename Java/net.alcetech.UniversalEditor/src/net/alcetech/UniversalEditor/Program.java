package net.alcetech.UniversalEditor;

import net.alcetech.ApplicationFramework.*;
import net.alcetech.ApplicationFramework.CommandItems.*;
import net.alcetech.ApplicationFramework.Theming.*;

import net.alcetech.UniversalEditor.Windows.*;
import net.alcetech.UniversalEditor.Core.Accessors.*;
import net.alcetech.UniversalEditor.Core.IO.*;
import net.alcetech.UniversalEditor.ObjectModels.Markup.*;
import net.alcetech.UniversalEditor.ObjectModels.Markup.Elements.*;

public class Program
{
	public static void main(String[] args)
	{
		ThemeManager.initialize();
		
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
		
		Command mnuFile = new Command("File", "_File");
		Command mnuFileExit = new Command("FileExit", "E_xit");

		mnuFile.addItem(new CommandReferenceCommandItem("FileExit"));
		
		Command mnuEdit = new Command("Edit", "_Edit");
		Command mnuEditCut = new Command("EditCut", "Cu_t");
		Command mnuEditCopy = new Command("EditCopy", "_Copy");
		Command mnuEditPaste = new Command("EditPaste", "_Paste");
		
		mnuEdit.addItem(new CommandReferenceCommandItem("EditCut"));
		mnuEdit.addItem(new CommandReferenceCommandItem("EditCopy"));
		mnuEdit.addItem(new CommandReferenceCommandItem("EditPaste"));
		
		Command mnuView = new Command("View", "_View");
		Command mnuProject = new Command("Project", "_Project");
		Command mnuBuild = new Command("Build", "_Build");
		Command mnuDebug = new Command("Debug", "_Debug");
		Command mnuTools = new Command("Tools", "_Tools");
		Command mnuWindow = new Command("Window", "_Window");
		Command mnuHelp = new Command("Help", "_Help");
		
		Application.addCommand(mnuFile);
		Application.addCommand(mnuFileExit);
		
		Application.addCommand(mnuEdit);
		Application.addCommand(mnuEditCut);
		Application.addCommand(mnuEditCopy);
		Application.addCommand(mnuEditPaste);
		
		Application.addCommand(mnuView);
		Application.addCommand(mnuProject);
		Application.addCommand(mnuBuild);
		Application.addCommand(mnuDebug);
		Application.addCommand(mnuTools);
		Application.addCommand(mnuWindow);
		Application.addCommand(mnuHelp);
		
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("File"));
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("Edit"));
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("View"));
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("Project"));
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("Build"));
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("Debug"));
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("Tools"));
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("Window"));
		Application.addMainMenuCommandItem(new CommandReferenceCommandItem("Help"));
		
		Application.addCommandListener(new CommandListener()
		{
			@Override
			public void commandExecuted(Command item) {

				// TODO Auto-generated method stub
				System.out.println("Command '" + item.getID() + "' executed!");
				
				if (item.getID() == "FileExit")
				{
					Application.stop();
				}
			}
		});
		
		MarkupObjectModel mom = new MarkupObjectModel();
		mom.addElement(new MarkupTagElement("test", "honduras"));
		
		MainWindow mw = new MainWindow();
		mw.setVisible(true);
	}
}
