package net.alcetech.UniversalEditor;

import java.io.IOException;

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
		
		MarkupObjectModel mom = new MarkupObjectModel();
		mom.getElementCollection().add(new MarkupTagElement("test", "honduras"));
		
		MainWindow mw = new MainWindow();
		mw.setVisible(true);
	}
}
