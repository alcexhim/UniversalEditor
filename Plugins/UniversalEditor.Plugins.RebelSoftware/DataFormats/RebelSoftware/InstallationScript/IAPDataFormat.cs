//
//  IAPDataFormat.cs - provides a DataFormat to manipulate Rebel Software installation scripts in IAP format
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

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration;

using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript;
using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs;
using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Actions;
using System.Linq;

namespace UniversalEditor.DataFormats.RebelSoftware.InstallationScript
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Rebel Software installation scripts in IAP format.
	/// </summary>
	public class IAPDataFormat : ExtensibleConfigurationDataFormat
	{
		private DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(InstallationScriptObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public IAPDataFormat()
		{
			// quote characters are literally part of the property value
			this.Settings.PropertyValuePrefix = String.Empty;
			this.Settings.PropertyValueSuffix = String.Empty;

			// IAP setup PUKES if the open brace is on the next line :(
			this.Settings.InlineGroupStart = true;

			// No separator except space between name and value
			this.Settings.PropertyNameValueSeparator = " ";

			// Property separator is the newline character
			this.Settings.PropertySeparator = "\n";

			// Do not allow top-level properties as property name/value separator (' ') conflicts with group definitions with spaces in them
			this.Settings.AllowTopLevelProperties = false;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			InstallationScriptObjectModel script = (objectModels.Pop() as InstallationScriptObjectModel);

			foreach (Group group in plom.Items.OfType<Group>())
			{
				switch (group.Name)
				{
					case "IA_Globals":
					{
						if (group.Items.OfType<Property>("version") != null) script.ProductVersion = new Version(group.Items.OfType<Property>("version").Value.ToString());
						break;
					}
					case "IA_WelcomeDialog":
					{
						script.Dialogs.Add(new WelcomeDialog());
						break;
					}
					case "IA_FinishDialog":
					{
						script.Dialogs.Add(new FinishDialog());
						break;
					}
					case "IA_LicenseDialog":
					{
						script.Dialogs.Add(new LicenseDialog());
						break;
					}
					case "IA_DestinationDialog":
					{
						script.Dialogs.Add(new DestinationDialog());
						break;
					}
					case "IA_StartCopyingDialog":
					{
						script.Dialogs.Add(new StartCopyingDialog());
						break;
					}
					case "IA_CopyFilesDialog":
					{
						script.Dialogs.Add(new CopyFilesDialog());
						break;
					}
					case "IA_StartMenu":
					{
						break;
					}
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			InstallationScriptObjectModel script = (objectModels.Pop() as InstallationScriptObjectModel);
			
			PropertyListObjectModel plom = new PropertyListObjectModel();

			Group IA_Globals = new Group("IA_Globals");
			IA_Globals.Items.AddProperty("name", script.ProductName);
			IA_Globals.Items.AddProperty("version", script.ProductVersion.ToString());
			IA_Globals.Items.AddProperty("diskspace", "60"); // not sure what this is
			if (!String.IsNullOrEmpty(script.BackgroundImageFileName))
			{
				IA_Globals.Items.AddProperty("bgbitmap", script.BackgroundImageFileName);
			}
			plom.Items.Add(IA_Globals);

			foreach (Dialog dialog in script.Dialogs)
			{
				// TODO: you can actually specify multiple IA_*Dialog definitions for the same dialog, but
				// the program will only use the properties of the last-defined one

				Group IA_Dialog = new Group();

				if (dialog is WelcomeDialog)
				{
					IA_Dialog.Name = "IA_WelcomeDialog";
				}
				else if (dialog is LicenseDialog)
				{
					LicenseDialog dlg = (dialog as LicenseDialog);
					IA_Dialog.Name = "IA_LicenseDialog";
					IA_Dialog.Items.AddProperty("file", dlg.LicenseFileName);
				}
				else if (dialog is DestinationDialog)
				{
					DestinationDialog dlg = (dialog as DestinationDialog);
					IA_Dialog.Name = "IA_DestinationDialog";
					IA_Dialog.Items.AddProperty("defaultdir", dlg.DefaultDirectory);
				}
				else if (dialog is StartCopyingDialog)
				{
					IA_Dialog.Name = "IA_StartCopyingDialog";
				}
				else if (dialog is CopyFilesDialog)
				{
					CopyFilesDialog dlg = (dialog as CopyFilesDialog);
					IA_Dialog.Name = "IA_CopyFilesDialog";
					foreach (var action in dlg.Actions)
					{
						if (action is UnzipAction)
						{
							UnzipAction act = (action as UnzipAction);
							IA_Dialog.Items.AddProperty("unzip", act.FileName);
						}
					}
				}
				else if (dialog is FinishDialog)
				{
					FinishDialog dlg = (dialog as FinishDialog);
					IA_Dialog.Name = "IA_FinishDialog";
					if (!String.IsNullOrEmpty(dlg.ReadmeFileName))
					{
						IA_Dialog.Items.AddProperty("readme", dlg.ReadmeFileName);
					}
					if (dlg.RequireReboot)
					{
						IA_Dialog.Items.AddProperty("reboot", "yes");
					}
					if (!String.IsNullOrEmpty(dlg.ExecutableFileName))
					{
						IA_Dialog.Items.AddProperty("exe", dlg.ExecutableFileName);
					}
					if (!String.IsNullOrEmpty(dlg.ExecutableWorkingDirectory))
					{
						IA_Dialog.Items.AddProperty("exeworkdir", dlg.ExecutableWorkingDirectory);
					}
				}

				plom.Items.Add(IA_Dialog);
			}

			Group IA_StartMenu = new Group("IA_StartMenu");
			IA_StartMenu.Items.AddProperty("name", script.StartMenuDirectoryName);
			plom.Items.Add(IA_StartMenu);

			objectModels.Push(plom);
		}
	}
}
