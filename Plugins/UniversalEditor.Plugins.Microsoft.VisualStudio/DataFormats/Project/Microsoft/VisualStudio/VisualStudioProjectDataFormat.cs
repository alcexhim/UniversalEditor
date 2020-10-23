//
//  VisualStudioProjectDataFormat.cs - provides a DataFormat for manipulating Microsoft Visual Studio / MSBuild project files
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
using System.Collections.Generic;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.ObjectModels.Solution;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Project.Microsoft.VisualStudio
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft Visual Studio / MSBuild project files.
	/// </summary>
	public class VisualStudioProjectDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(ProjectObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public VisualStudioProjectDataFormat()
		{
			// Do not encode the apostrophe (') as &apos;
			base.Settings.Entities.RemoveByValue1("apos");
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			ObjectModel omtarget = objectModels.Pop();

			ProjectObjectModel proj = null;
			if (omtarget is SolutionObjectModel)
			{
				proj = new ProjectObjectModel();
				(omtarget as SolutionObjectModel).Projects.Add(proj);
			}
			else if (omtarget is ProjectObjectModel)
			{
				proj = (omtarget as ProjectObjectModel);
			}
			else
			{
				throw new ObjectModelNotSupportedException();
			}

			string basePath = System.IO.Path.GetDirectoryName(Accessor.GetFileName());

			MarkupTagElement tagProject = (mom.Elements["Project"] as MarkupTagElement);
			if (tagProject == null) throw new InvalidDataFormatException();

			Dictionary<string, List<string>> dependents = new Dictionary<string, List<string>>();
			for (int i = 0; i < tagProject.Elements.Count; i++)
			{
				MarkupTagElement tag = (tagProject.Elements[i] as MarkupTagElement);
				if (tag == null) continue;

				if (tag.FullName.Equals("PropertyGroup"))
				{
					MarkupTagElement tagAssemblyName = (tag.Elements["AssemblyName"] as MarkupTagElement);
					if (tagAssemblyName != null)
					{
						proj.Title = tagAssemblyName.Value;
					}
				}
				else if (tag.FullName.Equals("ItemGroup"))
				{
					for (int j = 0; j < tag.Elements.Count; j++)
					{
						MarkupTagElement tag1 = (tag.Elements[j] as MarkupTagElement);
						if (tag1.FullName.Equals("Compile") || tag1.FullName.Equals("Content") || tag1.FullName.Equals("EmbeddedResource") || tag1.FullName.Equals("None"))
						{
							MarkupAttribute attInclude = tag1.Attributes["Include"];
							string relativePath = attInclude.Value.Replace('\\', System.IO.Path.DirectorySeparatorChar);
							proj.FileSystem.AddFile(basePath + System.IO.Path.DirectorySeparatorChar.ToString() + relativePath, attInclude.Value, '\\');

							MarkupTagElement tagDependentUpon = (tag1.Elements["DependentUpon"] as MarkupTagElement);
							if (tagDependentUpon != null)
							{
								string[] pathParts = attInclude.Value.Split(new char[] { '\\' });
								string dependentParentPath = String.Join("\\", pathParts, 0, pathParts.Length - 1);
								string dependentFullPath = String.Format("{0}\\{1}", dependentParentPath, tagDependentUpon.Value);
								if (!dependents.ContainsKey(dependentFullPath))
								{
									dependents[dependentFullPath] = new List<string>();
								}
								dependents[dependentFullPath].Add(relativePath);
							}
						}
						else if (tag1.FullName.Equals("Reference"))
						{
							MarkupAttribute attInclude = tag1.Attributes["Include"];
							Reference reff = new Reference();
							reff.Title = attInclude.Value;
							proj.References.Add(reff);
						}
					}
				}
			}

			if (omtarget is SolutionObjectModel && String.IsNullOrEmpty((omtarget as SolutionObjectModel)?.Title))
			{
				(omtarget as SolutionObjectModel).Title = proj.Title;
			}

			foreach (KeyValuePair<string, List<string>> kvp in dependents)
			{
				ProjectFile pfDependent = proj.FileSystem.FindFile(kvp.Key);
				foreach (string val in kvp.Value)
				{
					ProjectFile pf2 = proj.FileSystem.FindFile(val);
					if (pf2 == null)
					{
						Console.WriteLine("prj::fs file {0} not found", val);
						continue;
					}
					pfDependent.Files.Add(pf2);
					pf2.Dependents.Add(pfDependent);
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			ProjectObjectModel proj = (objectModels.Pop() as ProjectObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagProject = new MarkupTagElement();
			tagProject.FullName = "Project";
			// remove for compatibility with older VS versions
			tagProject.Attributes.Add("ToolsVersion", "4.0");
			tagProject.Attributes.Add("DefaultTargets", "Build");
			tagProject.Attributes.Add("xmlns", "http://schemas.microsoft.com/developer/msbuild/2003");

			#region PropertyGroup
			{
				MarkupTagElement tagPropertyGroup = new MarkupTagElement();
				tagPropertyGroup.FullName = "PropertyGroup";

				MarkupTagElement tagConfiguration = new MarkupTagElement();
				tagConfiguration.FullName = "Configuration";
				tagConfiguration.Attributes.Add("Condition", " '$(Configuration)' == '' ");
				tagConfiguration.Value = "Debug";
				tagPropertyGroup.Elements.Add(tagConfiguration);

				MarkupTagElement tagName = new MarkupTagElement();
				tagName.FullName = "Name";
				tagName.Value = proj.Title;
				tagPropertyGroup.Elements.Add(tagName);

				MarkupTagElement tagProjectGUID = new MarkupTagElement();
				tagProjectGUID.FullName = "ProjectGuid";
				tagProjectGUID.Value = proj.ID.ToString("B").ToUpper();
				tagPropertyGroup.Elements.Add(tagProjectGUID);

				MarkupTagElement tagOutputType = new MarkupTagElement();
				tagOutputType.FullName = "OutputType";
				tagOutputType.Value = "Library";
				tagPropertyGroup.Elements.Add(tagOutputType);

				MarkupTagElement tagRootNamespace = new MarkupTagElement();
				tagRootNamespace.FullName = "RootNamespace";
				tagRootNamespace.Value = proj.Title;
				tagPropertyGroup.Elements.Add(tagRootNamespace);

				MarkupTagElement tagProjectTypeGUIDs = new MarkupTagElement();
				tagProjectTypeGUIDs.FullName = "ProjectTypeGuids";

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < proj.ProjectTypes.Count; i++)
				{
					sb.Append(proj.ProjectTypes[i].ID.ToString("B").ToUpper());
					if (i < proj.ProjectTypes.Count - 1)
					{
						sb.Append(";");
					}
				}
				tagProjectTypeGUIDs.Value = sb.ToString();
				tagPropertyGroup.Elements.Add(tagProjectTypeGUIDs);

				MarkupTagElement tagAssemblyName = new MarkupTagElement();
				tagAssemblyName.FullName = "AssemblyName";
				tagAssemblyName.Value = proj.Title;
				tagPropertyGroup.Elements.Add(tagAssemblyName);

				tagProject.Elements.Add(tagPropertyGroup);
			}
			#endregion
			#region PropertyGroup
			{
				MarkupTagElement tagPropertyGroup = new MarkupTagElement();
				tagPropertyGroup.FullName = "PropertyGroup";
				tagPropertyGroup.Attributes.Add("Condition", " '$(Configuration)' == 'Debug' ");
				
				MarkupTagElement tagIncludeDebugInformation = new MarkupTagElement();
				tagIncludeDebugInformation.FullName = "IncludeDebugInformation";
				tagIncludeDebugInformation.Value = "true";
				tagPropertyGroup.Elements.Add(tagIncludeDebugInformation);

				tagProject.Elements.Add(tagPropertyGroup);
			}
			#endregion
			#region PropertyGroup
			{
				MarkupTagElement tagPropertyGroup = new MarkupTagElement();
				tagPropertyGroup.FullName = "PropertyGroup";
				tagPropertyGroup.Attributes.Add("Condition", " '$(Configuration)' == 'Release' ");

				MarkupTagElement tagIncludeDebugInformation = new MarkupTagElement();
				tagIncludeDebugInformation.FullName = "IncludeDebugInformation";
				tagIncludeDebugInformation.Value = "false";
				tagPropertyGroup.Elements.Add(tagIncludeDebugInformation);

				tagProject.Elements.Add(tagPropertyGroup);
			}
			#endregion
			#region ItemGroup
			{
				MarkupTagElement tagItemGroup = new MarkupTagElement();
				tagItemGroup.FullName = "ItemGroup";
				tagItemGroup.Attributes.Add("Condition", " '$(Configuration)' == 'Debug' ");

				foreach (ProjectFile file in proj.FileSystem.Files)
				{
					MarkupTagElement tagCompile = new MarkupTagElement();
					tagCompile.FullName = "Compile";
					tagCompile.Attributes.Add("Include", file.DestinationFileName);
					foreach (Property p in file.Configuration.Items.OfType<Property>())
					{
						MarkupTagElement tagProperty = new MarkupTagElement();
						tagProperty.Name = p.Name;
						tagProperty.Value = p.Value.ToString();
						tagCompile.Elements.Add(tagProperty);
					}
					tagItemGroup.Elements.Add(tagCompile);
				}

				tagProject.Elements.Add(tagItemGroup);
			}
			#endregion

			mom.Elements.Add(tagProject);
			objectModels.Push(mom);
		}
	}
}
