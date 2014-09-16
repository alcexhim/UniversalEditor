using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Multimedia3D.Motion;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.Editors.Multimedia3D.Motion
{
	public partial class MotionEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(MotionObjectModel));
			}
			return _er;
		}
		public MotionEditor()
		{
			InitializeComponent();
		}
	}
}
