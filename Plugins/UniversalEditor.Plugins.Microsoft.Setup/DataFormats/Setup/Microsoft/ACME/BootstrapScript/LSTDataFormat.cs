//
//  LSTDataFormat.cs - provides a DataFormat for manipulating Microsoft ACME setup bootstrapper list (LST) files
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
using System.Linq;
using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript;

namespace UniversalEditor.DataFormats.Setup.Microsoft.ACME.BootstrapScript
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft ACME setup bootstrapper list (LST) files.
	/// </summary>
	public class LSTDataFormat : WindowsConfigurationDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(BootstrapScriptObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public LSTDataFormat()
		{
			base.PropertyValuePrefix = String.Empty;
			base.PropertyValueSuffix = String.Empty;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);

			BootstrapScriptObjectModel script = (objectModels.Pop() as BootstrapScriptObjectModel);
			if (script == null) throw new ObjectModelNotSupportedException();

			foreach (Group grp in plom.Items.OfType<Group>())
			{
				BootstrapOperatingSystem operatingSystem = null;
				
				if (grp.Name == "Params" || grp.Name.EndsWith(" Params"))
				{
					if (grp.Name == "Params")
					{
						operatingSystem = BootstrapOperatingSystem.PlatformIndependent;
					}
					else
					{
						string operatingSystemName = grp.Name.Substring(0, grp.Name.Length - " Params".Length);
						operatingSystem = script.OperatingSystems.AddOrRetrieve(operatingSystemName);
					}

					operatingSystem.WindowTitle = grp.GetPropertyValue<string>("WndTitle");
					operatingSystem.WindowMessage = grp.GetPropertyValue<string>("WndMess");
					operatingSystem.TemporaryDirectorySize = grp.GetPropertyValue<int>("TmpDirSize");
					operatingSystem.TemporaryDirectoryName = grp.GetPropertyValue<string>("TmpDirName");
					operatingSystem.CommandLine = grp.GetPropertyValue<string>("CmdLine");
					operatingSystem.WindowClassName = grp.GetPropertyValue<string>("DrvWinClass");
					if (grp.Items.Contains<Property>("Require31"))
					{
						operatingSystem.Require31Enabled = true;
						operatingSystem.Require31Message = grp.GetPropertyValue<string>("Require31");
					}
					if (grp.Items.Contains<Property>("DrvModName"))
					{
						operatingSystem.DriverModuleName = grp.GetPropertyValue<string>("DrvModName");
					}
				}
				else if (grp.Name == "Files" || grp.Name.EndsWith(" Files"))
				{
					if (grp.Name == "Files")
					{
						operatingSystem = BootstrapOperatingSystem.PlatformIndependent;
					}
					else
					{
						string operatingSystemName = grp.Name.Substring(0, grp.Name.Length - " Files".Length);
						operatingSystem = script.OperatingSystems.AddOrRetrieve(operatingSystemName);
					}
					foreach (Property p in grp.Items.OfType<Property>())
					{
						operatingSystem.Files.Add(p.Name, p.Value.ToString());
					}
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			BootstrapScriptObjectModel script = (objectModels.Pop() as BootstrapScriptObjectModel);
			PropertyListObjectModel plom = new PropertyListObjectModel();

			foreach (BootstrapOperatingSystem item in script.OperatingSystems)
			{
				string paramsGroupName = "Params";
				string filesGroupName = "Files";

				if (item != BootstrapOperatingSystem.PlatformIndependent)
				{
					paramsGroupName = item.Name + " Params";
					filesGroupName = item.Name = " Files";
				}

				Group grpParams = new Group(paramsGroupName, new PropertyListItem[]
				{
					new Property("WndTitle", item.WindowTitle),
					new Property("WndMess", item.WindowMessage),
					new Property("TmpDirSize", item.TemporaryDirectorySize.ToString()),
					new Property("TmpDirName", item.TemporaryDirectoryName),
					new Property("CmdLine", item.CommandLine),
					new Property("DrvWinClass", item.WindowClassName)
				});
				if (item.Require31Enabled)
				{
					grpParams.Items.AddProperty("Require31", item.Require31Message);
				}
				plom.Items.Add(grpParams);

				Group grpFiles = new Group(filesGroupName);
				foreach (BootstrapFile file in item.Files)
				{
					grpFiles.Items.AddProperty(file.SourceFileName, file.DestinationFileName);
				}
				plom.Items.Add(grpFiles);
			}

			objectModels.Push(plom);
		}
	}
}
