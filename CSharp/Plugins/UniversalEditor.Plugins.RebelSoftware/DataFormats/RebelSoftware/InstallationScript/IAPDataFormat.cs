using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration;

using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript;
using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs;
using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Actions;

namespace UniversalEditor.DataFormats.RebelSoftware.InstallationScript
{
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
			this.Settings.PropertySeparator = "\r\n";

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

			foreach (Group group in plom.Groups)
			{
				switch (group.Name)
				{
					case "IA_Globals":
					{
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
			IA_Globals.Properties.Add("name", script.ProductName);
			IA_Globals.Properties.Add("version", script.ProductVersion.ToString());
			IA_Globals.Properties.Add("diskspace", "60"); // not sure what this is
			if (!String.IsNullOrEmpty(script.BackgroundImageFileName))
			{
				IA_Globals.Properties.Add("bgbitmap", script.BackgroundImageFileName);
			}
			plom.Groups.Add(IA_Globals);

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
					IA_Dialog.Properties.Add("file", dlg.LicenseFileName);
				}
				else if (dialog is DestinationDialog)
				{
					DestinationDialog dlg = (dialog as DestinationDialog);
					IA_Dialog.Name = "IA_DestinationDialog";
					IA_Dialog.Properties.Add("defaultdir", dlg.DefaultDirectory);
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
							IA_Dialog.Properties.Add("unzip", act.FileName);
						}
					}
				}
				else if (dialog is FinishDialog)
				{
					FinishDialog dlg = (dialog as FinishDialog);
					IA_Dialog.Name = "IA_FinishDialog";
					if (!String.IsNullOrEmpty(dlg.ReadmeFileName))
					{
						IA_Dialog.Properties.Add("readme", dlg.ReadmeFileName);
					}
					if (dlg.RequireReboot)
					{
						IA_Dialog.Properties.Add("reboot", "yes");
					}
					if (!String.IsNullOrEmpty(dlg.ExecutableFileName))
					{
						IA_Dialog.Properties.Add("exe", dlg.ExecutableFileName);
					}
					if (!String.IsNullOrEmpty(dlg.ExecutableWorkingDirectory))
					{
						IA_Dialog.Properties.Add("exeworkdir", dlg.ExecutableWorkingDirectory);
					}
				}

				plom.Groups.Add(IA_Dialog);
			}

			Group IA_StartMenu = new Group("IA_StartMenu");
			IA_StartMenu.Properties.Add("name", script.StartMenuDirectoryName);
			plom.Groups.Add(IA_StartMenu);

			objectModels.Push(plom);
		}
	}
}
