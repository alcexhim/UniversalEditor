using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.UserInterface.WindowsForms.Editors
{
	public partial class PropertyListEditor : Editor
	{
		public PropertyListEditor()
		{
			InitializeComponent();

			base.SupportedObjectModels.Add(typeof(PropertyListObjectModel));

			lv.SmallImageList = base.SmallImageList;
			lv.LargeImageList = base.LargeImageList;
			tv.ImageList = base.SmallImageList;
			// IconMethods.PopulateSystemIcons(base.SmallImageList);
			// IconMethods.PopulateSystemIcons(base.LargeImageList);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			PropertyListObjectModel plom = (base.ObjectModel as PropertyListObjectModel);
			
			tv.Nodes.Clear();
			
			TreeNode tnRoot = tv.Nodes.Add("<ROOT>");
			foreach (Group group in plom.Groups)
			{
				RecursiveAddTreeNode(group, tnRoot);
			}

			RefreshListView();
		}

		private void RefreshListView()
		{
			lv.Items.Clear();

			Property.PropertyCollection coll = null;
			if (mvarSelectedGroup == null)
			{
				PropertyListObjectModel plom = (base.ObjectModel as PropertyListObjectModel);
				coll = plom.Properties;
			}
			else
			{
				coll = mvarSelectedGroup.Properties;
			}

			foreach (Property property in coll)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Tag = property;
				lvi.Text = property.Name;
				if (property.Value == null)
				{
					lvi.SubItems.Add(String.Empty);
				}
				else
				{
					lvi.SubItems.Add(property.Value.ToString());
				}

				switch (property.Type)
				{
					case PropertyValueType.Binary:
					{
						lvi.ImageKey = "binary";
						break;
					}
					case PropertyValueType.DoubleWord:
					{
						lvi.ImageKey = "dword";
						break;
					}
					case PropertyValueType.ExpandedString:
					{
						lvi.ImageKey = "string-expanded";
						break;
					}
					case PropertyValueType.Link:
					{
						lvi.ImageKey = "link";
						break;
					}
					case PropertyValueType.None:
					{
						lvi.ImageKey = "none";
						break;
					}
					case PropertyValueType.QuadWord:
					{
						lvi.ImageKey = "qword";
						break;
					}
					case PropertyValueType.String:
					{
						lvi.ImageKey = "string";
						break;
					}
					case PropertyValueType.StringList:
					{
						lvi.ImageKey = "string-list";
						break;
					}
					case PropertyValueType.Unknown:
					{
						lvi.ImageKey = "unknown";
						break;
					}
				}

				lv.Items.Add(lvi);
			}
			lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private Group mvarSelectedGroup = null;

		private void RecursiveAddTreeNode(Group group, TreeNode parent)
		{
			TreeNode tn = new TreeNode();
			tn.Tag = group;
			tn.Text = group.Name;
			tn.ImageKey = "generic-folder-closed";
			tn.SelectedImageKey = "generic-folder-closed";

			foreach (Group group1 in group.Groups)
			{
				RecursiveAddTreeNode(group1, tn);
			}

			if (parent != null)
			{
				parent.Nodes.Add(tn);
			}
			else
			{
				tv.Nodes.Add(tn);
			}
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				mvarSelectedGroup = (e.Node.Tag as Group);
				RefreshListView();
			}
		}

		private void tv_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				if (e.Node.ImageKey == "generic-folder-closed")
				{
					e.Node.ImageKey = "generic-folder-open";
					e.Node.SelectedImageKey = "generic-folder-open";
				}
			}
		}

		private void tv_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				if (e.Node.ImageKey == "generic-folder-open")
				{
					e.Node.ImageKey = "generic-folder-closed";
					e.Node.SelectedImageKey = "generic-folder-closed";
				}
			}
		}

		private void lv_ItemActivate(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count == 1)
			{
				ListViewItem lvi = lv.SelectedItems[0];
				Property prop = (lvi.Tag as Property);
				if (prop == null) return;
				
				Dialogs.PropertyList.PropertyDetailsDialog dlg = new Dialogs.PropertyList.PropertyDetailsDialog();
				dlg.txtPropertyName.Text = prop.Name;
				if (prop.Value is byte[])
				{
					StringBuilder sb = new StringBuilder();
					byte[] array = (byte[])prop.Value;
					for (int i = 0; i < array.Length; i++)
					{
						sb.Append(array[i].ToString("X").PadLeft(2, '0'));
					}
					dlg.txtPropertyValue.Text = sb.ToString();
				}
				else
				{
					dlg.txtPropertyValue.Text = prop.Value.ToString();
				}

				switch (prop.Type)
				{
					case PropertyValueType.Binary:
					{
						dlg.cboPropertyType.SelectedIndex = 2;
						break;
					}
					case PropertyValueType.DoubleWord:
					{
						dlg.cboPropertyType.SelectedIndex = 3;
						break;
					}
					case PropertyValueType.ExpandedString:
					{
						dlg.cboPropertyType.SelectedIndex = 4;
						break;
					}
					case PropertyValueType.Link:
					{
						dlg.cboPropertyType.SelectedIndex = 5;
						break;
					}
					case PropertyValueType.None:
					{
						dlg.cboPropertyType.SelectedIndex = 7;
						break;
					}
					case PropertyValueType.QuadWord:
					{
						dlg.cboPropertyType.SelectedIndex = 8;
						break;
					}
					case PropertyValueType.String:
					{
						dlg.cboPropertyType.SelectedIndex = 1;
						break;
					}
					case PropertyValueType.StringList:
					{
						dlg.cboPropertyType.SelectedIndex = 6;
						break;
					}
					case PropertyValueType.Unknown:
					{
						dlg.cboPropertyType.SelectedIndex = 9;
						break;
					}
				}
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					lvi.Text = dlg.txtPropertyName.Text;

					prop.Name = dlg.txtPropertyName.Text;
					if (dlg.cboPropertyType.SelectedIndex == 0)
					{
						// auto-detect
						int dummy32 = 0;
						long dummy64 = 0;
						string value = dlg.txtPropertyValue.Text;
						if (Int32.TryParse(value, out dummy32))
						{
							prop.Value = dummy32;
							prop.Type = PropertyValueType.DoubleWord;
						}
						else if (Int64.TryParse(value, out dummy64))
						{
							prop.Value = dummy64;
							prop.Type = PropertyValueType.QuadWord;
						}
						else
						{
							prop.Value = dlg.txtPropertyValue.Text;
							prop.Type = PropertyValueType.String;
						}
					}
					else
					{
						switch (dlg.cboPropertyType.SelectedIndex)
						{
							case 1: // String
							{
								prop.Value = dlg.txtPropertyValue.Text;
								prop.Type = PropertyValueType.String;
								lvi.ImageKey = "string";
								break;
							}
							case 4: // Expanded String
							{
								prop.Value = dlg.txtPropertyValue.Text;
								prop.Type = PropertyValueType.ExpandedString;
								lvi.SubItems[1].Text = Environment.ExpandEnvironmentVariables(dlg.txtPropertyValue.Text);
								lvi.ImageKey = "string-expanded";
								break;
							}
							case 2: // Binary
							{
								List<byte> data = new List<byte>();
								for (int i = 0; i < dlg.txtPropertyValue.Text.Length; i += 2)
								{
									try
									{
										byte next = Byte.Parse(dlg.txtPropertyValue.Text.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
										data.Add(next);
									}
									catch (Exception)
									{

									}
								}
								prop.Value = data.ToArray();
								prop.Type = PropertyValueType.Binary;
								lvi.ImageKey = "binary";
								break;
							}
							case 3: // DWORD
							{
								prop.Value = Int32.Parse(dlg.txtPropertyValue.Text);
								prop.Type = PropertyValueType.DoubleWord;
								lvi.ImageKey = "dword";
								break;
							}
							case 8: // QWORD
							{
								prop.Value = Int64.Parse(dlg.txtPropertyValue.Text);
								prop.Type = PropertyValueType.QuadWord;
								lvi.ImageKey = "qword";
								break;
							}
							case 6: // String List
							{
								prop.Value = dlg.txtPropertyValue.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
								prop.Type = PropertyValueType.StringList;
								lvi.ImageKey = "string-list";
								break;
							}
							case 7: // None
							{
								prop.Value = null;
								prop.Type = PropertyValueType.None;
								lvi.ImageKey = "none";
								break;
							}
							case 9: // Unknown
							{
								prop.Value = null;
								prop.Type = PropertyValueType.Unknown;
								lvi.ImageKey = "unknown";
								break;
							}
						}
					}
					if (dlg.cboPropertyType.SelectedIndex != 4)
					{
						lvi.SubItems[1].Text = dlg.txtPropertyValue.Text;
					}
				}
			}
		}


		private void mnuContextListViewNewGroup_Click(object sender, EventArgs e)
		{
			PropertyListObjectModel plom = (base.ObjectModel as PropertyListObjectModel);
			if (plom == null) return;

			Group grp = new Group();


			Group parent = (tv.SelectedNode == null ? null : (tv.SelectedNode.Tag as Group));
			if (parent != null)
			{
				parent.Groups.Add(grp);

				TreeNode tn = new TreeNode();
				tn.Tag = grp;
				tv.SelectedNode.Nodes.Add(tn);
				tn.EnsureVisible();
				tn.BeginEdit();
			}
			else
			{
				plom.Groups.Add(grp);

				TreeNode tn = new TreeNode();
				tn.Tag = grp;
				tv.Nodes[0].Nodes.Add(tn);
				tn.EnsureVisible();
				tn.BeginEdit();
			}
		}
		private void mnuContextListViewNewProperty_Click(object sender, EventArgs e)
		{
			PropertyListObjectModel plom = (base.ObjectModel as PropertyListObjectModel);
			if (plom == null) return;

			Dialogs.PropertyList.PropertyDetailsDialog dlg = new Dialogs.PropertyList.PropertyDetailsDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				Property prop = new Property();
				prop.Name = dlg.txtPropertyName.Text;

				if (dlg.cboPropertyType.SelectedIndex == 0)
				{
					// auto-detect
					int dummy32 = 0;
					long dummy64 = 0;
					string value = dlg.txtPropertyValue.Text;
					if (Int32.TryParse(value, out dummy32))
					{
						prop.Value = dummy32;
						prop.Type = PropertyValueType.DoubleWord;
					}
					else if (Int64.TryParse(value, out dummy64))
					{
						prop.Value = dummy64;
						prop.Type = PropertyValueType.QuadWord;
					}
					else
					{
						prop.Value = dlg.txtPropertyValue.Text;
						prop.Type = PropertyValueType.String;
					}
				}
				else
				{
					switch (dlg.cboPropertyType.SelectedIndex)
					{
						case 1: // String
						{
							prop.Value = dlg.txtPropertyValue.Text;
							prop.Type = PropertyValueType.String;
							break;
						}
						case 4: // Expanded String
						{
							prop.Value = dlg.txtPropertyValue.Text;
							prop.Type = PropertyValueType.ExpandedString;
							break;
						}
						case 2: // Binary
						{
							List<byte> data = new List<byte>();
							for (int i = 0; i < dlg.txtPropertyValue.Text.Length; i += 2)
							{
								try
								{
									byte next = Byte.Parse(dlg.txtPropertyValue.Text.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
									data.Add(next);
								}
								catch (Exception)
								{

								}
							}
							prop.Value = data.ToArray();
							prop.Type = PropertyValueType.Binary;
							break;
						}
						case 3: // DWORD
						{
							prop.Value = Int32.Parse(dlg.txtPropertyValue.Text);
							prop.Type = PropertyValueType.DoubleWord;
							break;
						}
						case 8: // QWORD
						{
							prop.Value = Int64.Parse(dlg.txtPropertyValue.Text);
							prop.Type = PropertyValueType.QuadWord;
							break;
						}
						case 6: // String List
						{
							prop.Value = dlg.txtPropertyValue.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
							prop.Type = PropertyValueType.StringList;
							break;
						}
						case 7: // None
						{
							prop.Value = null;
							prop.Type = PropertyValueType.None;
							break;
						}
						case 9: // Unknown
						{
							prop.Value = null;
							prop.Type = PropertyValueType.Unknown;
							break;
						}
					}
				}

				if (tv.SelectedNode != null)
				{
					Group group = (tv.SelectedNode.Tag as Group);
					if (group != null)
					{
						group.Properties.Add(prop);
						return;
					}
				}

				plom.Properties.Add(prop);

				ListViewItem lvi = new ListViewItem();
				lvi.Text = prop.Name;
				lvi.Tag = prop;
				if (prop.Type == PropertyValueType.ExpandedString)
				{
					lvi.SubItems.Add(Environment.ExpandEnvironmentVariables(prop.Value.ToString()));
				}
				else
				{
					lvi.SubItems.Add(prop.Value.ToString());
				}

				switch (prop.Type)
				{
					case PropertyValueType.Binary:
					{
						lvi.ImageKey = "binary";
						break;
					}
					case PropertyValueType.DoubleWord:
					{
						lvi.ImageKey = "dword";
						break;
					}
					case PropertyValueType.ExpandedString:
					{
						lvi.ImageKey = "string-expanded";
						break;
					}
					case PropertyValueType.Link:
					{
						lvi.ImageKey = "link";
						break;
					}
					case PropertyValueType.None:
					{
						lvi.ImageKey = "none";
						break;
					}
					case PropertyValueType.QuadWord:
					{
						lvi.ImageKey = "qword";
						break;
					}
					case PropertyValueType.String:
					{
						lvi.ImageKey = "string";
						break;
					}
					case PropertyValueType.StringList:
					{
						lvi.ImageKey = "string-list";
						break;
					}
					case PropertyValueType.Unknown:
					{
						lvi.ImageKey = "unknown";
						break;
					}
				}
				lv.Items.Add(lvi);
			}
		}

		private void mnuContextListViewProperties_Click(object sender, EventArgs e)
		{
			lv_ItemActivate(sender, e);
		}

		public override void Delete()
		{
			base.Delete();
		}

		private void tv_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Node == null) return;
			Group g = (e.Node.Tag as Group);
			if (!String.IsNullOrEmpty(e.Label))
			{
				BeginEdit();

				g.Name = e.Label;
				e.Node.Text = e.Label;

				EndEdit();
			}
			else if (e.Label == null && String.IsNullOrEmpty(g.Name))
			{
				if (g.Parent == null)
				{
					(ObjectModel as PropertyListObjectModel).Groups.Remove(g);
				}
				else
				{
					g.Parent.Groups.Remove(g);
				}
				e.Node.Remove();
			}
			else
			{
				e.Node.Text = g.Name;
			}
		}

		private void tv_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Node == null) return;
			if (e.Node == tv.Nodes[0]) e.CancelEdit = true;
		}

		private void tv_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TreeNode tn = tv.HitTest(e.Location).Node;
				if (tn != null) tv.SelectedNode = tn;
			}
		}
	}
}
