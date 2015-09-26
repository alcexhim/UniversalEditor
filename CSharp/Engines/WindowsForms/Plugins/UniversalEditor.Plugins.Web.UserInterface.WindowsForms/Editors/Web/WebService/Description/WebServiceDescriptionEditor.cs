using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Web.WebService.Description;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Web.WebService.Description
{
	public partial class WebServiceDescriptionEditor : Editor
	{
		public WebServiceDescriptionEditor()
		{
			InitializeComponent();
			
			tv.ImageList = SmallImageList;
			Font = SystemFonts.MenuFont;
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(WebServiceDescriptionObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tv.Nodes.Clear();

			WebServiceDescriptionObjectModel wsdl = (ObjectModel as WebServiceDescriptionObjectModel);
			if (wsdl == null) return;

			TreeNode tnToplevel = new TreeNode();
			tnToplevel.Text = wsdl.Name;
			tnToplevel.ImageKey = "Service";
			tnToplevel.SelectedImageKey = "Service";

			TreeNode tnMessages = new TreeNode();
			tnMessages.Text = "Messages";
			tnMessages.ImageKey = "generic-folder-closed";
			tnMessages.SelectedImageKey = "generic-folder-closed";

			foreach (UniversalEditor.ObjectModels.Web.WebService.Description.Message message in wsdl.Messages)
			{
				TreeNode tnMessage = new TreeNode();
				tnMessage.Tag = message;
				tnMessage.Text = message.Name;
				tnMessage.ImageKey = "Message";
				tnMessage.SelectedImageKey = "Message";
				tnMessages.Nodes.Add(tnMessage);
			}
			tnToplevel.Nodes.Add(tnMessages);

			tv.Nodes.Add(tnToplevel);
		}
	}
}
