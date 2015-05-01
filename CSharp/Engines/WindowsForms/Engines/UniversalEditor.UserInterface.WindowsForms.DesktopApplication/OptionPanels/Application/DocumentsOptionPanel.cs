using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.Settings;

namespace UniversalEditor.UserInterface.WindowsForms.OptionPanels.Application
{
    public partial class DocumentsOptionPanel : OptionPanel
    {
        public DocumentsOptionPanel()
        {
            InitializeComponent();
        }

        private string[] mvarOptionGroups = new string[] { "Application", "Documents" };
        public override string[] OptionGroups { get { return mvarOptionGroups; } }

		public override void LoadSettings()
		{
			base.LoadSettings();

			string defaultUserLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + System.IO.Path.DirectorySeparatorChar.ToString() + "Universal Editor";
			string defaultUserDocumentsLocation = defaultUserLocation + System.IO.Path.DirectorySeparatorChar.ToString() + "Documents";
			string defaultUserProjectsLocation = defaultUserLocation + System.IO.Path.DirectorySeparatorChar.ToString() + "Projects";
			txtDefaultUserDocumentsLocation.Text = SettingsManager.GetPropertyValue<string>(new string[] { "Application", "Documents", "DefaultUserDocumentsLocation" }, defaultUserDocumentsLocation);
			txtDefaultUserProjectsLocation.Text = SettingsManager.GetPropertyValue<string>(new string[] { "Application", "Documents", "DefaultUserProjectsLocation" }, defaultUserProjectsLocation);

			chkAllowEditingReadonlyFiles.Checked = SettingsManager.GetPropertyValue<bool>(new string[] { "Application", "Documents", "AllowEditingReadonlyFiles" }, false);
		}
		public override void SaveSettings()
		{
			base.SaveSettings();
		}
    }
}
