using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Engines.WindowsForms.OptionPanels.Application
{
	public partial class GeneralOptionPanel : OptionPanel
	{
		public GeneralOptionPanel()
		{
			InitializeComponent();
		}

		private string[] mvarOptionGroups = new string[] { "Application", "General" };
		public override string[] OptionGroups { get { return mvarOptionGroups; } }

		public override void LoadSettings()
		{
			base.LoadSettings();

			string value = Engine.CurrentEngine.ConfigurationManager.GetValue<string>(new string[]
			{
				"Application",
				"TitleBarBehavior"
			});
			switch (value)
			{
				case "CurrentFileName":
				{
					optTitleBarBehaviorCurrentFileName.Checked = true;
					break;
				}
				case "CurrentFilePath":
				{
					optTitleBarBehaviorCurrentFilePath.Checked = true;
					break;
				}
				default:
				{
					optTitleBarBehaviorNone.Checked = true;
					break;
				}
			}
		}
		public override void SaveSettings()
		{
			base.SaveSettings();

			#region Title Bar Behavior
			{
				string value = "None";
				if (optTitleBarBehaviorCurrentFileName.Checked)
				{
					value = "CurrentFileName";
				}
				if (optTitleBarBehaviorCurrentFilePath.Checked)
				{
					value = "CurrentFilePath";
				}

				Engine.CurrentEngine.ConfigurationManager.SetValue<string>(new string[]
				{
					"Application",
					"TitleBarBehavior"
				}, value);
			}
			#endregion
		}
		public override void ResetSettings()
		{
			base.ResetSettings();

			optTitleBarBehaviorNone.Checked = true;
		}
	}
}
