using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.ObjectModels.SoftwareInstallation.Microsoft.ACME.BootstrapScript;

namespace UniversalEditor.DataFormats.SoftwareInstallation.Microsoft.ACME.BootstrapScript
{
	public class LSTDataFormat : WindowsConfigurationDataFormat
	{
		private static DataFormatReference _dfr = null;
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

			Group grpParams = plom.Groups["Params"];
			if (grpParams == null) throw new InvalidDataFormatException("File does not contain a 'Params' section");

			Group grpFiles = plom.Groups["Files"];
			if (grpFiles == null) throw new InvalidDataFormatException("File does not contain a 'Files' section");

			if (grpParams.Properties.Contains("WndTitle")) script.WindowTitle = grpParams.Properties["WndTitle"].Value.ToString();
			if (grpParams.Properties.Contains("WndMess")) script.WindowMessage = grpParams.Properties["WndMess"].Value.ToString();
			if (grpParams.Properties.Contains("TmpDirSize")) script.TemporaryDirectorySize = Int32.Parse(grpParams.Properties["TmpDirSize"].Value.ToString());
			if (grpParams.Properties.Contains("TmpDirName")) script.TemporaryDirectoryName = grpParams.Properties["TmpDirName"].Value.ToString();
			if (grpParams.Properties.Contains("CmdLine")) script.CommandLine = grpParams.Properties["CmdLine"].Value.ToString();
			if (grpParams.Properties.Contains("DrvWinClass")) script.WindowClassName = grpParams.Properties["DrvWinClass"].Value.ToString();
			if (grpParams.Properties.Contains("Require31"))
			{
				script.Require31Enabled = true;
				script.Require31Message = grpParams.Properties["Require31"].Value.ToString();
			}
			foreach (Property p in grpFiles.Properties)
			{
				script.Files.Add(p.Name, p.Value.ToString());
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			BootstrapScriptObjectModel script = (objectModels.Pop() as BootstrapScriptObjectModel);
			PropertyListObjectModel plom = new PropertyListObjectModel();

			Group grpParams = new Group("Params");
			grpParams.Properties.Add("WndTitle", script.WindowTitle);
			grpParams.Properties.Add("WndMess", script.WindowMessage);
			grpParams.Properties.Add("TmpDirSize", script.TemporaryDirectorySize.ToString());
			grpParams.Properties.Add("TmpDirName", script.TemporaryDirectoryName);
			grpParams.Properties.Add("CmdLine", script.CommandLine);
			grpParams.Properties.Add("DrvWinClass", script.WindowClassName);
			if (script.Require31Enabled)
			{
				grpParams.Properties.Add("Require31", script.Require31Message);
			}
			plom.Groups.Add(grpParams);

			Group grpFiles = new Group("Files");
			foreach (BootstrapFile file in script.Files)
			{
				grpFiles.Properties.Add(file.SourceFileName, file.DestinationFileName);
			}
			plom.Groups.Add(grpFiles);

			objectModels.Push(plom);
		}
	}
}
