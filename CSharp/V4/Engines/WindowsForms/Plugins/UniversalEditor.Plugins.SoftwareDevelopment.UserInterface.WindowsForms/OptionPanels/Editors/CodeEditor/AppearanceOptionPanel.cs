using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.OptionPanels.Editors.CodeEditor
{
	public partial class AppearanceOptionPanel : OptionPanel
	{
		public AppearanceOptionPanel()
		{
			InitializeComponent();
		}

		private string[] mvarOptionGroups = new string[] 
		{
			"Editors",
			"Code Editor",
			"Appearance"
		};

		public override string[] OptionGroups
		{
			get { return mvarOptionGroups; }
		}
	}
}
