//
//  Program.cs - the main entry point for the Universal Editor Extension Compiler
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Collections.Generic;
using UniversalEditor;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Package.OpenDocument;
using UniversalEditor.DataFormats.UEPackage;
using UniversalEditor.DataFormats.UEPackage.Binary;
using UniversalEditor.ObjectModels.Package;
using UniversalEditor.ObjectModels.UEPackage;

namespace UniversalEditor.Compiler
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.Error.WriteLine("uex started...");
			List<string> listFileNames = new List<string>();

			string outputFileName = "output.uex";
			bool foundFileName = false;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].StartsWith("/") && !foundFileName)
				{
					if (args[i].StartsWith("/out:"))
					{
						outputFileName = args[i].Substring(5);
					}
				}
				else
				{
					// is file name
					foundFileName = true;

					listFileNames.Add(args[i]);
				}
			}

			PackageObjectModel ue = new PackageObjectModel();
			OpenDocumentDataFormat odf = new OpenDocumentDataFormat();

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			int totalInstances = 0;

			string exefilename = System.Environment.GetCommandLineArgs()[0];
			string workingdir = System.IO.Path.GetDirectoryName(exefilename);

			for (int i = 0; i < listFileNames.Count; i++)
			{
				string relpath = listFileNames[i];
				if (relpath.StartsWith(workingdir))
				{
					relpath = relpath.Substring(workingdir.Length);
				}
				relpath = "Content/" + relpath;

				byte[] filedata = System.IO.File.ReadAllBytes(listFileNames[i]);
				ue.FileSystem.AddFile(relpath, filedata);
			}

			FileAccessor faout = new FileAccessor(outputFileName, true, true);
			Document.Save(ue, odf, faout);
			Console.Error.WriteLine("uex written to {0}!", outputFileName);
		}
	}
}
