//
//  VisualStudioSolutionDataFormat.cs - provides a DataFormat for manipulating Microsoft Visual Studio solution files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.UserInterface;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.ObjectModels.Solution;

using UniversalEditor.ObjectModels.Project;
using UniversalEditor.DataFormats.Project.Microsoft.VisualStudio;
using System.Linq;
using System.Collections.Generic;

namespace UniversalEditor.DataFormats.Solution.Microsoft.VisualStudio
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft Visual Studio solution files.
	/// </summary>
	public class VisualStudioSolutionDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SolutionObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SolutionObjectModel sol = (objectModel as SolutionObjectModel);
			if (sol == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			if (base.Accessor.Remaining < 3) throw new InvalidDataFormatException("File must be at least three bytes");

			byte[] signature1 = reader.ReadBytes(3);
			if (!signature1.Match(new byte[] { 0xEF, 0xBB, 0xBF }))
			{
				// this is a Unicode Byte-Order-Mark... do we have to rely on it?
				// if we don't find it let's just back up since we may have an
				// ASCII file even though VS is known to put the BOM in its sln
				// files
				reader.Accessor.Seek(-3, SeekOrigin.Current);
			}

			string solutionFileName = String.Empty;
			string solutionPath = String.Empty;
			if (base.Accessor is FileAccessor)
			{
				solutionFileName = (base.Accessor as FileAccessor).FileName;
			}
			sol.Title = System.IO.Path.GetFileNameWithoutExtension(solutionFileName);
			solutionPath = System.IO.Path.GetDirectoryName(solutionFileName);

			string signature2a = reader.ReadLine();
			signature2a = signature2a.Trim();
			if (!String.IsNullOrEmpty(signature2a)) throw new InvalidDataFormatException("Empty line should be present at beginning of file");
			
			string signature2Verify = "Microsoft Visual Studio Solution File, Format Version ";
			string signature2 = reader.ReadLine();
			if (!signature2.StartsWith(signature2Verify)) throw new InvalidDataFormatException("File does not begin with \"" + signature2Verify + "\"");

			ProjectObjectModel lastProject = null;

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

					string strProjectTypeID = line.Substring(9, line.IndexOf(')') - 10);
					Guid projectTypeID = new Guid(strProjectTypeID);

					string restOfDeclaration = line.Substring(line.IndexOf('=') + 1).Trim();
					string[] paramz = restOfDeclaration.Split(new string[] { "," }, "\"");

					string projectTitle = paramz[0].Trim();
					string projectRelativeFileName = paramz[1].Trim();
					string projectFileName = solutionPath + System.IO.Path.DirectorySeparatorChar.ToString() + projectRelativeFileName;
					string projectDirectory = System.IO.Path.GetDirectoryName(projectFileName);

					Guid projectID = new Guid(paramz[2].Trim());

					if (projectTypeID == KnownProjectTypeIDs.SolutionFolder)
					{
						ProjectFolder pf = new ProjectFolder();
						pf.Name = projectTitle;
					}
					else
					{
						projectFileName = projectFileName.Replace('\\', System.IO.Path.DirectorySeparatorChar);
						if (System.IO.File.Exists(projectFileName))
						{
							if (UniversalEditor.Common.Reflection.GetAvailableObjectModel<ProjectObjectModel>(projectFileName, out ProjectObjectModel project))
							{
								project.BasePath = projectDirectory;
								project.Title = projectTitle;
								sol.Projects.Add(project);
								lastProject = project;
							}
						}
						else
						{
							Console.WriteLine("skipping nonexistent project file {0}", projectFileName);
						}
					}
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

			foreach (ProjectObjectModel project in sol.Projects)
			{
				string projdir = soldir + "/" + project.Title;
				project.RelativeFileName = project.Title + "\\" + project.Title + GetFileExtensionForProjectType(project.ProjectTypes);

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
				if (project.ProjectTypes.Count > 0)
				{
					projectTypeGuid = project.ProjectTypes[0].ID;
				}

				writer.WriteLine("Project(\"" + projectTypeGuid.ToString("B").ToUpper() + "\") = \"" + project.Title + "\", \"" + project.RelativeFileName + "\", \"" + project.ID.ToString("B").ToUpper() + "\"");
				writer.WriteLine("EndProject");
				/*
				}
				*/

				if (!System.IO.Directory.Exists(projdir))
				{
					System.IO.Directory.CreateDirectory(projdir);
				}

				foreach (ProjectFile pf in project.FileSystem.Files)
				{
					pf.SourceFileAccessor.Open();
					pf.SourceFileAccessor.Seek(0, SeekOrigin.Begin);
					byte[] data = pf.SourceFileAccessor.Reader.ReadToEnd();
					pf.SourceFileAccessor.Close();

					string filename = System.IO.Path.Combine(new string[] { projdir, pf.DestinationFileName });
					string filedir = System.IO.Path.GetDirectoryName(filename);
					if (!System.IO.Directory.Exists(filedir))
					{
						System.IO.Directory.CreateDirectory(filedir);
					}

					System.IO.File.WriteAllBytes(System.IO.Path.Combine(new string[] { projdir, pf.DestinationFileName }), data);
				}

				Document.Save(project, new VisualStudioProjectDataFormat(), new FileAccessor(projdir + "/" + project.Title + ".ueproj", true, true), true);
				project.BasePath = projdir;
			}

			writer.WriteLine("Global");
			writer.WriteLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
			writer.WriteLine("\t\tDebug|Any CPU = Debug|Any CPU");
			writer.WriteLine("\t\tRelease|Any CPU = Release|Any CPU");
			writer.WriteLine("\tEndGlobalSection");
			writer.WriteLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
			foreach (ProjectObjectModel project in sol.Projects)
			{
				writer.WriteLine("\t\t" + project.ID.ToString("B").ToUpper() + ".Debug|Any CPU.ActiveCfg = Debug|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B").ToUpper() + ".Debug|Any CPU.Build.0 = Debug|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B").ToUpper() + ".Debug|x86.ActiveCfg = Debug|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B").ToUpper() + ".Release|Any CPU.ActiveCfg = Release|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B").ToUpper() + ".Release|Any CPU.Build.0 = Release|Any CPU");
				writer.WriteLine("\t\t" + project.ID.ToString("B").ToUpper() + ".Release|x86.ActiveCfg = Release|Any CPU");
			}
			writer.WriteLine("\tEndGlobalSection");
			writer.WriteLine("\tGlobalSection(SolutionProperties) = preSolution");
			foreach (Property prop in sol.Configuration.Items.OfType<Property>())
			{
				writer.WriteLine("\t\t" + prop.Name + " = " + prop.Value);
			}
			writer.WriteLine("\tEndGlobalSection");
			writer.WriteLine("\tGlobalSection(NestedProjects) = preSolution");
			writer.WriteLine("\tEndGlobalSection");
			writer.WriteLine("EndGlobal");
		}

		private string GetFileExtensionForProjectType(ProjectType.ProjectTypeCollection projectTypes)
		{
			ProjectType projectType = null;
			if (projectTypes.Count > 0)
				projectType = projectTypes[0];

			if (projectType?.ProjectFileExtension != null)
			{
				return projectType.ProjectFileExtension;
			}
			return ".ueproj";
		}
	}
}
