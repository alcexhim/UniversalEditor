package net.alcetech.UniversalEditor;

import java.io.FileNotFoundException;

import net.alcetech.ApplicationFramework.*;
import net.alcetech.ApplicationFramework.CommandItems.*;
import net.alcetech.ApplicationFramework.Configuration.ConfigurationManager;
import net.alcetech.ApplicationFramework.Theming.*;

import net.alcetech.UniversalEditor.Windows.*;
import net.alcetech.UniversalEditor.Core.Accessors.*;
import net.alcetech.UniversalEditor.Core.IO.*;
import net.alcetech.UniversalEditor.ObjectModels.Markup.*;

public class Program
{
	public static void main(String[] args)
	{
		Application.setShortName("universal-editor");
		Application.setTitle("Universal Editor");
		// Application.setConsoleMessageVisibility(ConsoleMessageVisibility.DEBUG);
		
		ConfigurationManager.setConfigurationFileNameFilter("*.uexml");
		ConfigurationManager.initialize();
		ThemeManager.initialize();
		
		try {
			FileAccessor fa = FileAccessor.fromFile("/var/tmp/test.xml");
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

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
