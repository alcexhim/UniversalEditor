using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.ObjectModels.Solution;
using UniversalEditor.UserInterface;

namespace UniversalEditor.DataFormats.Solution.Microsoft.VisualStudio
{
	public class VisualStudioSolutionDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(SolutionObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Microsoft Visual Studio solution", new byte?[][]
				{
					new byte?[] { (byte)0xEF, (byte)0xBB, (byte)0xBF, (byte)0x0D, (byte)0x0A, (byte)'M', (byte)'i', (byte)'c', (byte)'r', (byte)'o', (byte)'s', (byte)'o', (byte)'f', (byte)'t', (byte)' ', (byte)'V', (byte)'i', (byte)'s', (byte)'u', (byte)'a', (byte)'l', (byte)' ', (byte)'S', (byte)'t', (byte)'u', (byte)'d', (byte)'i', (byte)'o', (byte)' ', (byte)'S', (byte)'o', (byte)'l', (byte)'u', (byte)'t', (byte)'i', (byte)'o', (byte)'n', (byte)' ', (byte)'F', (byte)'i', (byte)'l', (byte)'e', (byte)',', (byte)' ', (byte)'F', (byte)'o', (byte)'r', (byte)'m', (byte)'a', (byte)'t', (byte)' ', (byte)'V', (byte)'e', (byte)'r', (byte)'s', (byte)'i', (byte)'o', (byte)'n', (byte)' ' }
				}, new string[] { "*.sln" }); ;
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SolutionObjectModel sol = (objectModel as SolutionObjectModel);
			if (sol == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			byte[] signature1 = reader.ReadBytes(3);
			if (!signature1.Match(new byte[] { 0xEF, 0xBB, 0xBF }))
			{
				// this is a Unicode Byte-Order-Mark... do we have to rely on it?
				// if we don't find it let's just back up since we may have an
				// ASCII file even though VS is known to put the BOM in its sln
				// files
				reader.Accessor.Seek(-3, SeekOrigin.Current);
			}

			string signature2a = reader.ReadLine();
			if (!String.IsNullOrEmpty(signature2a)) throw new InvalidDataFormatException("Empty line should be present at beginning of file");
			
			string signature2Verify = "Microsoft Visual Studio Solution File, Format Version ";
			string signature2 = reader.ReadLine();
			if (!signature2.StartsWith(signature2Verify)) throw new InvalidDataFormatException("File does not begin with \"" + signature2Verify + "\"");

			Project lastProject = null;

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine().Trim();
				if (line.StartsWith("#") || String.IsNullOrEmpty(line)) continue;

				if (line.StartsWith("Project("))
				{
					if (!line.Contains("="))
					{
						HostApplication.Messages.Add(HostApplicationMessageSeverity.Warning, "Invalid Project declaration in solution file");
						continue;
					}

					lastProject = new Project();

					string strProjectTypeID = line.Substring(9, line.IndexOf(')') - 10);
					Guid projectTypeID = new Guid(strProjectTypeID);

					string restOfDeclaration = line.Substring(line.IndexOf('=') + 1).Trim();
					string[] paramz = restOfDeclaration.Split(new string[] { "," }, "\"");

					lastProject.Title = paramz[0].Trim();
					lastProject.RelativeFileName = paramz[1].Trim();
					lastProject.ID = new Guid(paramz[2].Trim());
					sol.Projects.Add(lastProject);
				}
				else if (line == "EndProject")
				{
					lastProject = null;
				}
				else if (line.StartsWith("GlobalSection("))
				{

				}
				else if (line == "EndGlobalSection")
				{

				}
				else
				{
					HostApplication.Messages.Add(HostApplicationMessageSeverity.Warning, "Ignoring unknown solution directive \"" + line + "\"");
					continue;
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			SolutionObjectModel sol = (objectModel as SolutionObjectModel);
			if (sol == null) throw new ObjectModelNotSupportedException();

			string soldir = String.Empty;
			if (sol.Accessor is FileAccessor)
			{
				soldir = System.IO.Path.GetDirectoryName((sol.Accessor as FileAccessor).FileName);
			}

			Writer writer = base.Accessor.Writer;
			writer.WriteBytes(new byte[] { 0xEF, 0xBB, 0xBF });
			writer.WriteLine();
			writer.WriteLine("Microsoft Visual Studio Solution File, Format Version 12.00");
			writer.WriteLine("# Visual Studio 2012");

			foreach (Project project in sol.Projects)
			{
				string projdir = soldir + "/" + project.Title;
				project.RelativeFileName = project.Title + "\\" + project.Title + ".ueproj";

				/*
				if (project is SolutionFolder)
				{
					writer.WriteLine("Project(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\") = \"" + project.Title + "\", \"" + project.Title + "\", \"" + project.ID.ToString("B") + "\"");
					writer.WriteLine("EndProject");
				}
				else
				{
				*/
				Guid projectTypeGuid = Guid.Empty;
				if (project.ProjectType != null) projectTypeGuid = project.ProjectType.ID;
				writer.WriteLine("Project(\"" + projectTypeGuid.ToString("B") + "\") = \"" + project.Title + "\", \"" + project.RelativeFileName + "\", \"" + project.ID.ToString("B") + "\"");
				writer.WriteLine("EndProject");
				/*
				}
				*/

				SolutionObjectModel solproj = new SolutionObjectModel();
				solproj.Projects.Add(project);

				if (!System.IO.Directory.Exists(projdir))
				{
					System.IO.Directory.CreateDirectory(projdir);
				}

				Document.Save(solproj, new VisualStudioProjectDataFormat(), new FileAccessor(projdir + "/" + project.Title + ".ueproj", true, true), true);
			}

			writer.WriteLine("Global");
			writer.WriteLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
			writer.WriteLine("\t\tDebug|Any CPU = Debug|Any CPU");
			writer.WriteLine("\t\tRelease|Any CPU = Release|Any CPU");
			writer.WriteLine("\tEndGlobalSection");
			writer.WriteLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
			foreach (Project project in sol.Projects)
			{
				writer.WriteLine("\t\t" + project.ID.ToString("B") + ".Debug|Any CPU.ActiveCfg = Debug|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B") + ".Debug|Any CPU.Build.0 = Debug|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B") + ".Debug|x86.ActiveCfg = Debug|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B") + ".Release|Any CPU.ActiveCfg = Release|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B") + ".Release|Any CPU.Build.0 = Release|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B") + ".Release|x86.ActiveCfg = Release|Any CPU");
			}
			writer.WriteLine("\tEndGlobalSection");
			writer.WriteLine("\tGlobalSection(SolutionProperties) = preSolution");
			foreach (Property prop in sol.Configuration.Properties)
			{
				writer.WriteLine("\t\t" + prop.Name + " = " + prop.Value);
			}
			writer.WriteLine("\tEndGlobalSection");
			writer.WriteLine("\tGlobalSection(NestedProjects) = preSolution");
			writer.WriteLine("\tEndGlobalSection");
			writer.WriteLine("EndGlobal");
		}
	}
}
