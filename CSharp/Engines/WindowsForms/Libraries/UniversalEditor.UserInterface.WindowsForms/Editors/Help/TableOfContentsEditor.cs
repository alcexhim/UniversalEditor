using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Help.TableOfContents;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Help
{
	public partial class TableOfContentsEditor : Editor
	{
		public TableOfContentsEditor()
		{
			InitializeComponent();
			tv.ImageList = SmallImageList;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tv.Nodes.Clear();

			TableOfContentsObjectModel toc = (ObjectModel as TableOfContentsObjectModel);
			if (toc == null) return;

			foreach (TOCNode node in toc.Nodes)
			{
				LoadTOCNode(node, null);
			}
		}

		private void LoadTOCNode(TOCNode node, TreeNode parentTreeNode)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Text = node.Title;
			treeNode.Tag = node;

			treeNode.ImageKey = "generic-book-closed";
			treeNode.SelectedImageKey = "generic-book-closed";

			foreach (TOCNode node1 in node.Nodes)
			{
				LoadTOCNode(node1, treeNode);
			}

			if (parentTreeNode == null)
			{
				tv.Nodes.Add(treeNode);
			}
			else
			{
				parentTreeNode.Nodes.Add(treeNode);
			}
		}

		private void tv_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			if (e.Node == null) return;
			if (e.Node.ImageKey == "generic-book-open")
			{
				e.Node.ImageKey = "generic-book-closed";
			}
		}

		private void tv_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (e.Node == null) return;
			if (e.Node.ImageKey == "generic-book-closed")
			{
				e.Node.ImageKey = "generic-book-open";
			}
		}
	}
}
