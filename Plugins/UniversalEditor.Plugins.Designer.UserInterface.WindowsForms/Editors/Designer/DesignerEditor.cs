using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.Designer;

namespace UniversalEditor.Plugins.Designer.UserInterface.WindowsForms.Editors.Designer
{
	public partial class DesignerEditor : Editor
	{
		public DesignerEditor()
		{
			InitializeComponent();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			DesignerObjectModel design = (ObjectModel as DesignerObjectModel);

			if (design == null) return;
		}
	}
}
