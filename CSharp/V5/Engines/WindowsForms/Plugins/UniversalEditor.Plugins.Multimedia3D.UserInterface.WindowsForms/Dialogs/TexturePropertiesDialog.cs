using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.Dialogs
{
	public partial class TexturePropertiesDialog : Form
	{
		public TexturePropertiesDialog()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		internal ModelObjectModel ParentModel = null;

		private void cmdSelect_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (sender == cmdSelectTexture)
			{
				if (!String.IsNullOrEmpty(txtTextureFileName.Text) && System.IO.File.Exists(txtTextureFileName.Text))
				{
					ofd.FileName = txtTextureFileName.Text;
				}
			}
			else if (sender == cmdSelectMap)
			{
				if (!String.IsNullOrEmpty(txtMapFileName.Text) && System.IO.File.Exists(txtMapFileName.Text))
				{
					ofd.FileName = txtMapFileName.Text;
				}
			}

			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
                string path = String.Empty;
                if (ParentModel.Accessor is FileAccessor)
                {
                    path = System.IO.Path.GetDirectoryName((ParentModel.Accessor as FileAccessor).FileName);
                }

				if (sender == cmdSelectTexture)
				{
					txtTextureFileName.Text = Common.Path.MakeRelativePath(ofd.FileName, path);
				}
				else if (sender == cmdSelectMap)
				{
					txtMapFileName.Text = Common.Path.MakeRelativePath(ofd.FileName, path);
				}
			}
		}

		private void cmdClear_Click(object sender, EventArgs e)
		{
			if (sender == cmdClearTexture)
			{
				txtTextureFileName.Text = String.Empty;
			}
			else if (sender == cmdClearMap)
			{
				txtMapFileName.Text = String.Empty;
			}
		}
	}
}
