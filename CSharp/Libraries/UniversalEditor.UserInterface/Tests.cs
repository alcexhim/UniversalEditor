//
//  Tests.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
namespace UniversalEditor.UserInterface
{
	public class Tests
	{
#if INCLUDE_TESTS

		private static void Test_TextDocument()
		{
			UniversalEditor.ObjectModels.Text.TextObjectModel text = new UniversalEditor.ObjectModels.Text.TextObjectModel();
			UniversalEditor.DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(text.MakeReference());
			UniversalEditor.DataFormatReference dfr = dfrs[0];

			DataFormat df = dfr.Create();

			df.Open(@"W:\Libraries\UniversalEditor\bin\Debug\Tests\Text\Document.xps");
			df.Load<UniversalEditor.ObjectModels.Text.TextObjectModel>(ref text);
			df.Close();

			df.EnableWrite = true;
			df.ForceOverwrite = true;
			df.Open(@"W:\Libraries\UniversalEditor\bin\Debug\Tests\Text\Document2.xps");
			df.Save(text);
			df.Close();
		}
		private static void Test_InstallShieldScript()
		{
			string InputFileName = @"W:\Libraries\UniversalEditor\bin\Debug\Tests\Installers\InstallShield Legacy\SETUP.INS";
			UniversalEditor.ObjectModels.InstallShield.InstallShieldScriptObjectModel ins = UniversalEditor.Common.Reflection.GetAvailableObjectModel<UniversalEditor.ObjectModels.InstallShield.InstallShieldScriptObjectModel>(InputFileName);


		}
		private static void Test_Executable()
		{
			string InputFileName = @"version\syscor32.dll";
			string OutputFileName = @"version\syscoree.dll";
			// InputFileName = @"Tests\IMGSTART\imgstart.exe";
			// OutputFileName = @"Tests\IMGSTART\udnova.exe";

			UniversalEditor.ObjectModels.Executable.ExecutableObjectModel exe = new ObjectModels.Executable.ExecutableObjectModel();
			UniversalEditor.DataFormats.Executable.Microsoft.MicrosoftExecutableDataFormat exedf = new DataFormats.Executable.Microsoft.MicrosoftExecutableDataFormat();

			/*
			exedf.Open(InputFileName);
			exedf.Load<UniversalEditor.ObjectModels.Executable.ExecutableObjectModel>(ref exe);
			exedf.Close();
			exe.Sections[".rsrc"].Save(@"version\syscor32.ver");
			
			exe = new UniversalEditor.ObjectModels.Executable.ExecutableObjectModel();
			exedf.Open(OutputFileName);
			exedf.Load<UniversalEditor.ObjectModels.Executable.ExecutableObjectModel>(ref exe);
			exedf.Close();
			exe.Sections[".rsrc"].Save(@"version\syscoree.ver");
			exe = new UniversalEditor.ObjectModels.Executable.ExecutableObjectModel();
			*/

			exedf.Open(InputFileName);
			exedf.Load<UniversalEditor.ObjectModels.Executable.ExecutableObjectModel>(ref exe);
			exedf.Close();

			exedf.EnableWrite = true;
			exedf.ForceOverwrite = true;

			UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceBlock version = (exe.Resources[0] as UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceBlock);
			UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceStringFileInfoBlock sfi = (version.ChildBlocks[0] as UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceStringFileInfoBlock);
			sfi.Entries[0].StringTableEntries[1].Value = "Hello";

			exedf.Open(OutputFileName);
			// (exe.Resources[0] as UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceBlock).ChildBlocks[0].Add("Website", "http://www.google.com/");
			exedf.Save(exe);
			exedf.Close();
		}
		private static void Test_RenPyArchive()
		{
			/*
			object nidl = (new UniversalEditor.Serializers.PickleSerializer()).Deserialize("cos\nsystem\n(S'ls ~'\ntR.");

			UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel fsom = new ObjectModels.FileSystem.FileSystemObjectModel();
			UniversalEditor.DataFormats.RenPy.Archive.RenPyArchiveDataFormat rpa = new DataFormats.RenPy.Archive.RenPyArchiveDataFormat();

			ObjectModel om = fsom;
			rpa.Open(@"C:\Program Files (x86)\Katawa Shoujo\game\data.rpa");
			rpa.Load(ref om);
			rpa.Close();
			*/
			return;
		}
		private static void Test_ARJ()
		{
			string FileName = @"C:\ARJ\test1.arj";
			string FileName2 = @"C:\ARJ\test1_aaaa.arj";

			UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel fsom = new ObjectModels.FileSystem.FileSystemObjectModel();
			UniversalEditor.DataFormats.FileSystem.ARJ.ARJDataFormat arj = new DataFormats.FileSystem.ARJ.ARJDataFormat();
			arj.Open(FileName);
			arj.Load<UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel>(ref fsom);
			arj.Close();

			arj.EnableWrite = true;

			arj.Open(FileName2);
			arj.Save(fsom);
			arj.Close();
		}
		private static void Test_Outsim()
		{
			ObjectModelReference omrHOM = UniversalEditor.Common.Reflection.GetObjectModelByTypeName("UniversalEditor.ObjectModels.Outsim.SynthMaker.Module.ModuleObjectModel");
			ObjectModel omHOM = omrHOM.Create();

			DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(omrHOM);
			DataFormat dfHOM = dfrs[0].Create();

			dfHOM.Open(@"C:\Program Files (x86)\Outsim\SynthMaker\modules\System\ADSR.hom");
			dfHOM.Load(ref omHOM);
			dfHOM.Close();
		}
		private static void Test_WinHelpBookCollection()
		{
			string FileName = @"W:\Libraries\UniversalEditor\bin\Debug\Tests\Book Collection\WinHelp\ACCESS.HLP";
			string FileName2 = @"W:\Libraries\UniversalEditor\bin\Debug\Tests\Book Collection\WinHelp\ACCESS_aaaa.HLP";

			BookCollectionObjectModel books = new BookCollectionObjectModel();
			UniversalEditor.DataFormats.BookCollection.WinHelp.WinHelpDataFormat winhlp = new DataFormats.BookCollection.WinHelp.WinHelpDataFormat();
			winhlp.Open(FileName);
			winhlp.Load<BookCollectionObjectModel>(ref books);
			winhlp.Close();

			winhlp.EnableWrite = true;
			winhlp.Open(FileName2);
			winhlp.Save(books);
			winhlp.Close();
		}
#endif
	}
}
