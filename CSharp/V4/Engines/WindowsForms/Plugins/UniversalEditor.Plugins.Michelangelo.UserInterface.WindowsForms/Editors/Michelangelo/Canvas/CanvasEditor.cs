using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Michelangelo.Canvas
{
	public partial class CanvasEditor : Editor
	{
		public CanvasEditor()
		{
			InitializeComponent();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

		}
	}
}
