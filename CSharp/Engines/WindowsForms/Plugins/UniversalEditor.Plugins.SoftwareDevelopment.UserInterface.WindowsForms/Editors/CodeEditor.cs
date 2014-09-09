using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;
using UniversalEditor.Accessors;

namespace UniversalEditor.Editors
{
	public partial class CodeEditor : Editor
	{
		public CodeEditor()
		{
			InitializeComponent();
			base.SupportedObjectModels.Add(typeof(CodeObjectModel));

			#region Initializing Menu Items
			ActionMenuItem mnuProject = base.MenuBar.Items.Add("mnuProject", "&Project");
			mnuProject.Items.Add("mnuProjectAddWindow", "Add &Window...", mnuProjectAddWindow_Click, 0);
			mnuProject.Items.Add("mnuProjectAddControl", "Add Co&ntrol...", mnuProjectAddControl_Click, 1);
			mnuProject.Items.Add("mnuProjectAddClass", "Add &Class...", mnuProjectAddClass_Click, 2);
			mnuProject.Items.AddSeparator(3);

			#endregion
			#region Initializing Image Lists
			tvExplorer.ImageList = base.SmallImageList;

			lvEnum.LargeImageList = base.LargeImageList;
			lvEnum.SmallImageList = base.SmallImageList;

			lvExplorer.LargeImageList = base.LargeImageList;
			lvExplorer.SmallImageList = base.SmallImageList;
			#endregion
		}

		#region Menu Item Handlers
		private void mnuProjectAddWindow_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Going to add a new Window to the project", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			HostApplication.CurrentWindow.NewFile();
		}
		private void mnuProjectAddControl_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Going to add a new Control to the project", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			HostApplication.CurrentWindow.NewFile();
		}
		private void mnuProjectAddClass_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Going to add a new Class to the project", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			HostApplication.CurrentWindow.NewFile();
		}
		#endregion

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
			
			tvExplorer.Nodes.Clear();
			
			CodeObjectModel code = (base.ObjectModel as CodeObjectModel);
			if (code == null) return;

			TreeNode tnRoot = new TreeNode();

			string FileName = String.Empty;
			if (code.Accessor is FileAccessor) FileName = (code.Accessor as FileAccessor).FileName;
			switch (System.IO.Path.GetExtension(FileName))
			{
				case ".cs":
				{
					tnRoot.ImageKey = "project-csharp";
					tnRoot.SelectedImageKey = "project-csharp";
					break;
				}
				case ".vb":
				{
					tnRoot.ImageKey = "project-vbnet";
					tnRoot.SelectedImageKey = "project-vbnet";
					break;
				}
				case ".c":
				case ".cpp":
				{
					tnRoot.ImageKey = "project-cpp";
					tnRoot.SelectedImageKey = "project-cpp";
					break;
				}
				case ".js":
				case ".jsf":
				{
					tnRoot.ImageKey = "project-jsharp";
					tnRoot.SelectedImageKey = "project-jsharp";
					break;
				}
				case ".fs":
				{
					tnRoot.ImageKey = "project-fsharp";
					tnRoot.SelectedImageKey = "project-fsharp";
					break;
				}
			}
			tnRoot.Text = System.IO.Path.GetFileName(FileName);

			foreach (CodeElement element in code.Elements)
			{
				RecursiveAddTreeNode(element, tnRoot);
			}
			tvExplorer.Nodes.Add(tnRoot);
		}

		private void RecursiveAddTreeNode(CodeElement element, TreeNode parent)
		{
			if (element == null) return;

			TreeNode tn = new TreeNode();
			if (element is INamedCodeElement)
			{
				tn.Text = (element as INamedCodeElement).Name;
			}
			tn.Tag = element;

			if (element is CodeClassElement)
			{
				CodeClassElement clss = (element as CodeClassElement);
				switch (clss.AccessModifiers)
				{
					case CodeAccessModifiers.Assembly:
					case CodeAccessModifiers.FamilyANDAssembly:
					case CodeAccessModifiers.FamilyORAssembly:
					{
						tn.ImageKey = "class-internal";
						tn.SelectedImageKey = "class-internal";
						break;
					}
					case CodeAccessModifiers.Private:
					{
						tn.ImageKey = "class-private";
						tn.SelectedImageKey = "class-private";
						break;
					}
					case CodeAccessModifiers.None:
					case CodeAccessModifiers.Family:
					{
						tn.ImageKey = "class-protected";
						tn.SelectedImageKey = "class-protected";
						break;
					}
					default:
					{
						tn.ImageKey = "class";
						tn.SelectedImageKey = "class";
						break;
					}
				}
			}
			else if (element is CodeEnumerationElement)
			{
				CodeEnumerationElement enumm = (element as CodeEnumerationElement);
				switch (enumm.AccessModifiers)
				{
					case CodeAccessModifiers.Assembly:
					case CodeAccessModifiers.FamilyANDAssembly:
					case CodeAccessModifiers.FamilyORAssembly:
					{
						tn.ImageKey = "enum-internal";
						tn.SelectedImageKey = "enum-internal";
						break;
					}
					case CodeAccessModifiers.Private:
					{
						tn.ImageKey = "enum-private";
						tn.SelectedImageKey = "enum-private";
						break;
					}
					case CodeAccessModifiers.None:
					case CodeAccessModifiers.Family:
					{
						tn.ImageKey = "enum-protected";
						tn.SelectedImageKey = "enum-protected";
						break;
					}
					default:
					{
						tn.ImageKey = "enum";
						tn.SelectedImageKey = "enum";
						break;
					}
				}

				foreach (CodeEnumerationValue cev in enumm.Values)
				{
					TreeNode tn1 = new TreeNode();
					tn1.Text = cev.Name + " = " + cev.Value.ToString();
					tn1.ImageKey = "enumvalue";
					tn1.SelectedImageKey = "enumvalue";
					tn1.Tag = cev;
					tn.Nodes.Add(tn1);
				}
			}
			else if (element is CodeMethodElement)
			{
				CodeMethodElement meth = (element as CodeMethodElement);
				
				switch (meth.AccessModifiers)
				{
					case CodeAccessModifiers.Assembly:
					case CodeAccessModifiers.FamilyANDAssembly:
					case CodeAccessModifiers.FamilyORAssembly:
					{
						tn.ImageKey = "method-internal";
						tn.SelectedImageKey = "method-internal";
						break;
					}
					case CodeAccessModifiers.Private:
					{
						tn.ImageKey = "method-private";
						tn.SelectedImageKey = "method-private";
						break;
					}
					case CodeAccessModifiers.None:
					case CodeAccessModifiers.Family:
					{
						tn.ImageKey = "method-protected";
						tn.SelectedImageKey = "method-protected";
						break;
					}
					default:
					{
						tn.ImageKey = "method";
						tn.SelectedImageKey = "method";
						break;
					}
				}
			}
			else if (element is CodeNamespaceElement)
			{
				tn.ImageKey = "namespace";
				tn.SelectedImageKey = "namespace";
			}
			else if (element is CodePropertyElement)
			{
				CodePropertyElement prop = (element as CodePropertyElement);
				switch (prop.AccessModifiers)
				{
					case CodeAccessModifiers.Assembly:
					case CodeAccessModifiers.FamilyANDAssembly:
					case CodeAccessModifiers.FamilyORAssembly:
					{
						tn.ImageKey = "property-internal";
						tn.SelectedImageKey = "property-internal";
						break;
					}
					case CodeAccessModifiers.Private:
					{
						tn.ImageKey = "property-private";
						tn.SelectedImageKey = "property-private";
						break;
					}
					case CodeAccessModifiers.None:
					case CodeAccessModifiers.Family:
					{
						tn.ImageKey = "property-protected";
						tn.SelectedImageKey = "property-protected";
						break;
					}
					default:
					{
						tn.ImageKey = "property";
						tn.SelectedImageKey = "property";
						break;
					}
				}
			}
			else if (element is CodeVariableElement)
			{
				CodeVariableElement field = (element as CodeVariableElement);
				switch (field.AccessModifiers)
				{
					case CodeAccessModifiers.Assembly:
					case CodeAccessModifiers.FamilyANDAssembly:
					case CodeAccessModifiers.FamilyORAssembly:
					{
						tn.ImageKey = "field-internal";
						tn.SelectedImageKey = "field-internal";
						break;
					}
					case CodeAccessModifiers.Private:
					{
						tn.ImageKey = "field-private";
						tn.SelectedImageKey = "field-private";
						break;
					}
					case CodeAccessModifiers.None:
					case CodeAccessModifiers.Family:
					{
						tn.ImageKey = "field-protected";
						tn.SelectedImageKey = "field-protected";
						break;
					}
					default:
					{
						tn.ImageKey = "field";
						tn.SelectedImageKey = "field";
						break;
					}
				}
			}

			if (element is CodeElementContainerElement)
			{
				CodeElementContainerElement container = (element as CodeElementContainerElement);
				foreach (CodeElement el in container.Elements)
				{
					RecursiveAddTreeNode(el, tn);
				}
			}

			if (parent != null)
			{
				parent.Nodes.Add(tn);
			}
			else
			{
				tvExplorer.Nodes.Add(tn);
			}
		}

		private void tvExplorer_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag is CodeElementContainerElement)
			{
				CodeElementContainerElement container = (e.Node.Tag as CodeElementContainerElement);

				lvExplorer.Items.Clear();
				foreach (CodeElement element in container.Elements)
				{
					ListViewItem lvi = new ListViewItem();

					#region Class
					if (element is CodeClassElement)
					{
						CodeClassElement el = (element as CodeClassElement);
						switch (el.AccessModifiers)
						{
							case CodeAccessModifiers.Assembly:
							case CodeAccessModifiers.FamilyANDAssembly:
							case CodeAccessModifiers.FamilyORAssembly:
							{
								lvi.ImageKey = "class-internal";
								break;
							}
							case CodeAccessModifiers.Private:
							{
								lvi.ImageKey = "class-private";
								break;
							}
							case CodeAccessModifiers.None:
							case CodeAccessModifiers.Family:
							{
								lvi.ImageKey = "class-protected";
								break;
							}
							default:
							{
								lvi.ImageKey = "class";
								break;
							}
						}
					}
					#endregion
					#region Enum
					else if (element is CodeEnumerationElement)
					{
						CodeEnumerationElement el = (element as CodeEnumerationElement);
						switch (el.AccessModifiers)
						{
							case CodeAccessModifiers.Assembly:
							case CodeAccessModifiers.FamilyANDAssembly:
							case CodeAccessModifiers.FamilyORAssembly:
							{
								lvi.ImageKey = "enum-internal";
								break;
							}
							case CodeAccessModifiers.Private:
							{
								lvi.ImageKey = "enum-private";
								break;
							}
							case CodeAccessModifiers.None:
							case CodeAccessModifiers.Family:
							{
								lvi.ImageKey = "enum-protected";
								break;
							}
							default:
							{
								lvi.ImageKey = "enum";
								break;
							}
						}
					}
					#endregion
					#region Method
					else if (element is CodeMethodElement)
					{
						CodeMethodElement el = (element as CodeMethodElement);
						switch (el.AccessModifiers)
						{
							case CodeAccessModifiers.Assembly:
							case CodeAccessModifiers.FamilyANDAssembly:
							case CodeAccessModifiers.FamilyORAssembly:
								{
									lvi.ImageKey = "method-internal";
									break;
								}
							case CodeAccessModifiers.Private:
								{
									lvi.ImageKey = "method-private";
									break;
								}
							case CodeAccessModifiers.None:
							case CodeAccessModifiers.Family:
								{
									lvi.ImageKey = "method-protected";
									break;
								}
							default:
								{
									lvi.ImageKey = "method";
									break;
								}
						}
					}
					#endregion
					#region Variable
					else if (element is CodeVariableElement)
					{
						CodeVariableElement el = (element as CodeVariableElement);
						switch (el.AccessModifiers)
						{
							case CodeAccessModifiers.Assembly:
							case CodeAccessModifiers.FamilyANDAssembly:
							case CodeAccessModifiers.FamilyORAssembly:
							{
								lvi.ImageKey = "field-internal";
								break;
							}
							case CodeAccessModifiers.Private:
							{
								lvi.ImageKey = "field-private";
								break;
							}
							case CodeAccessModifiers.None:
							case CodeAccessModifiers.Family:
							{
								lvi.ImageKey = "field-protected";
								break;
							}
							default:
							{
								lvi.ImageKey = "field";
								break;
							}
						}
					}
					#endregion

					lvi.Tag = element;
					if (element is INamedCodeElement)
					{
						lvi.Text = (element as INamedCodeElement).Name;
					}
					lvExplorer.Items.Add(lvi);
				}

				lvExplorer.Visible = true;
				lvEnum.Visible = false;
				lvExplorer.BringToFront();
			}
			else if (e.Node.Tag is CodeEnumerationElement)
			{
				CodeEnumerationElement enumm = (e.Node.Tag as CodeEnumerationElement);

				lvEnum.Items.Clear();
				foreach (CodeEnumerationValue value in enumm.Values)
				{
					ListViewItem lvi = new ListViewItem();
					lvi.Text = value.Name;
					lvi.SubItems.Add(value.Value.ToString());
					lvi.Tag = value;
					lvi.ImageKey = "enumvalue";
					lvEnum.Items.Add(lvi);
				}
				lvEnum.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

				lvExplorer.Visible = false;
				lvEnum.Visible = true;
				lvEnum.BringToFront();
			}
		}

		private void mnuContextTreeView_Opening(object sender, CancelEventArgs e)
		{
			if (tvExplorer.SelectedNode == null || tvExplorer.SelectedNode.Tag == null)
			{
				mnuContextTreeViewNewMethodCall.Visible = false;
				mnuContextTreeViewNewVariable.Visible = false;
			}

			if (tvExplorer.SelectedNode != null)
			{
				mnuContextTreeViewNewNamespace.Visible = (!(tvExplorer.SelectedNode.Tag is CodeClassElement));
				mnuContextTreeViewNewMethodCall.Visible = (tvExplorer.SelectedNode.Tag is CodeMethodElement);
			}
		}

		private void mnuContextTreeViewNew_Click(object sender, EventArgs e)
		{
			CodeObjectModel code = (ObjectModel as CodeObjectModel);
			if (code == null) return;

			string objectType = (sender as ToolStripMenuItem).Name.Substring("mnuContextTreeViewNew".Length).ToLower();
			switch (objectType)
			{
				case "namespace":
				{
					Dialogs.ObjectPropertiesDialog dlg = new Dialogs.ObjectPropertiesDialog();
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						TreeNode prev = tvExplorer.SelectedNode;

						TreeNode tn = new TreeNode();
						tn.Text = dlg.Name;
						CodeNamespaceElement ce = new CodeNamespaceElement(dlg.Name);
						tn.Tag = ce;
						ce.Name = dlg.Name.Split(new char[] { '.' });

						TreeNodeCollection tnc = (tvExplorer.SelectedNode != null ? tvExplorer.SelectedNode.Nodes : tvExplorer.Nodes);
						tnc.Add(tn);
						tvExplorer.SelectedNode = tn;

						if (prev != null && prev.Tag != null)
						{
							CodeElementContainerElement ct = (prev.Tag as CodeElementContainerElement);
							if (ct != null)
							{
								ct.Elements.Add(ce);
							}
						}
						else
						{
							code.Elements.Add(ce);
						}
					}
					break;
				}
				case "class":
				{
					break;
				}
			}
		}
	}
}
