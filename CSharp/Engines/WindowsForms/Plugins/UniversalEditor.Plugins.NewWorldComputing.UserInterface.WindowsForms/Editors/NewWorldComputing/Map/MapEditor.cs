using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.NewWorldComputing.Map
{
	public partial class MapEditor : Editor
	{
		public MapEditor()
		{
			InitializeComponent();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);


		}
	}
}
