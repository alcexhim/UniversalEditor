using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Multimedia3D.Motion;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.Editors.Multimedia3D.Motion
{
	public partial class MotionEditor : Editor
	{
		public MotionEditor()
		{
			InitializeComponent();
			base.SupportedObjectModels.Add(typeof(MotionObjectModel));
		}
	}
}
