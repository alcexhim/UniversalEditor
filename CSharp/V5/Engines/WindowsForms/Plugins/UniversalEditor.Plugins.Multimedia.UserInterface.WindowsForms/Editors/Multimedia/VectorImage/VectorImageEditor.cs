using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Multimedia.VectorImage
{
	public partial class VectorImageEditor : Editor
	{
		public VectorImageEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Vector Image";
				_er.SupportedObjectModels.Add(typeof(VectorImageObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			VectorImageObjectModel vector = (ObjectModel as VectorImageObjectModel);
			if (vector != null)
			{
				
			}
		}
	}
}
