using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Solution;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.Solution.Microsoft.VisualStudio
{
	public class VisualStudioProjectDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(SolutionObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Microsoft Visual Studio project", new string[] { "*.vbproj", "*.csproj", "*.jsproj", "*.wixproj", "*.fsproj", "*.pyproj", "*.sqlproj", "*.phpproj" });
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
			SolutionObjectModel sln = (objectModels.Pop() as SolutionObjectModel);
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			SolutionObjectModel sln = (objectModels.Pop() as SolutionObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			Project proj = sln.Projects[0];

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
				tagProjectGUID.Value = proj.ID.ToString("D");
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
				tagProjectTypeGUIDs.Value = proj.ProjectType.ID.ToString("B");
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
					foreach (Property p in file.Configuration.Properties)
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
