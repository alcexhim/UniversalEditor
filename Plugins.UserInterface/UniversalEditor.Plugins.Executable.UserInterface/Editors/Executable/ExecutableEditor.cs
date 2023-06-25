//
//  ExecutableEditor.cs - provides an Editor for the ExecutableObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.UserInterface;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Input.Mouse;
using System.Reflection;
using System.Text;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Controls.SyntaxTextBox;
using MBS.Framework;
using System.Collections.Generic;

namespace UniversalEditor.Plugins.Executable.UserInterface.Editors.Executable
{
	/// <summary>
	/// Provides an <see cref="Editor" /> for the <see cref="ExecutableObjectModel" />.
	/// </summary>
	[ContainerLayout("~/Editors/Executable/ExecutableEditor.glade")]
	public class ExecutableEditor : Editor
	{
		private ListViewControl tvSections = null;
		private DefaultTreeModel tmSections = null;
		private DefaultTreeModel lsManagedDisassemblyLanguage;

		private TabContainer tbs = null;

		private TextBox txtAssemblyName = null;
		private TextBox txtAssemblyVersion = null;
		private SyntaxTextBoxControl txtManagedAssemblySource = null;

		private DefaultTreeModel tmOtherInformation = null;

		// managed assembly panel
		private Button cmdManagedDisassemblySave;
		private TextBox txtManagedAssemblySearch;
		private ComboBox cboManagedDisassemblyLanguage;
		private ListViewControl tvManagedDisassemblyTypes;
		private DefaultTreeModel tmManagedDisassemblyTypes;
		private TextBox txtManagedDisassemblySource;

		private ListViewControl tvResources;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(ExecutableObjectModel));
			}
			return _er;
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		public ExecutableEditor()
		{
			Application.Instance.AttachCommandEventHandler("ExecutableEditor_ContextMenu_Sections_Selected_CopyTo", ContextMenu_CopyTo_Click);
		}

		private void ContextMenu_CopyTo_Click(object sender, EventArgs e)
		{
			FileDialog fd = new FileDialog();
			if (tvSections.SelectedRows.Count == 1)
			{
				fd.Mode = FileDialogMode.Save;

				ExecutableSection section = tvSections.SelectedRows[0].GetExtraData<ExecutableSection>("section");
				fd.SelectedFileNames.Add(section.Name);

				if (fd.ShowDialog() == DialogResult.OK)
				{
					System.IO.File.WriteAllBytes(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1], section.Data);
				}
			}
			else if (tvSections.SelectedRows.Count > 1)
			{
				// select a folder
				fd.Mode = FileDialogMode.SelectFolder;

				if (fd.ShowDialog() == DialogResult.OK)
				{
					foreach (TreeModelRow row in tvSections.SelectedRows)
					{
						ExecutableSection section = tvSections.SelectedRows[0].GetExtraData<ExecutableSection>("section");
						System.IO.File.WriteAllBytes(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1] + System.IO.Path.DirectorySeparatorChar.ToString() + section.Name, section.Data);
					}
				}
			}
		}

		[EventHandler(nameof(tvSections), "BeforeContextMenu")]
		private void tvSections_BeforeContextMenu(object sender, EventArgs e)
		{
			bool selected = tvSections.SelectedRows.Count > 0;
			if (e is MouseEventArgs)
			{
				MouseEventArgs ee = (e as MouseEventArgs);
				ListViewHitTestInfo lvih = tvSections.HitTest(ee.X, ee.Y);
				if (lvih.Row == null)
					selected = false;
			}

			if (selected)
			{
				tvSections.ContextMenuCommandID = "ExecutableEditor_ContextMenu_Sections_Selected";
			}
			else
			{
				tvSections.ContextMenuCommandID = "ExecutableEditor_ContextMenu_Sections_Unselected";
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Type[] codeProviders = MBS.Framework.Reflection.GetAvailableTypes(new Type[] { typeof(CodeProvider) });
			for (int i = 0; i < codeProviders.Length; i++)
			{
				if (codeProviders[i].IsAbstract)
					continue;

				CodeProvider codeProvider = (codeProviders[i].Assembly.CreateInstance(codeProviders[i].FullName) as CodeProvider);
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(lsManagedDisassemblyLanguage.Columns[0], codeProvider.Title)
				});
				row.SetExtraData<CodeProvider>("provider", codeProvider);
				lsManagedDisassemblyLanguage.Rows.Add(row);
			}
			cboManagedDisassemblyLanguage.ReadOnly = true;
			if (lsManagedDisassemblyLanguage.Rows.Count > 0)
			{
				cboManagedDisassemblyLanguage.SelectedItem = lsManagedDisassemblyLanguage.Rows[0];
			}

			Context.AttachCommandEventHandler("ExecutableEditor_ContextMenu_Resources_Selected_CopyTo", ExecutableEditor_ContextMenu_Resources_Selected_CopyTo);

			OnObjectModelChanged(EventArgs.Empty);
		}

		private void ExecutableEditor_ContextMenu_Resources_Selected_CopyTo(object sender, EventArgs e)
		{
			CommandEventArgs ee = (e as CommandEventArgs);

			if (tvResources.SelectedRows.Count == 1)
			{
				FileDialog dlg = new FileDialog();
				dlg.Mode = FileDialogMode.Save;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					ExecutableResource res = tvResources.SelectedRows[0].GetExtraData<ExecutableResource>("res");
					if (res.Source != null)
					{
						System.IO.File.WriteAllBytes(dlg.SelectedFileName, res.Source.GetData());
					}
				}
			}
			else if (tvResources.SelectedRows.Count > 1)
			{
				FileDialog dlg = new FileDialog();
				dlg.Mode = FileDialogMode.SelectFolder;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					foreach (TreeModelRow row in tvResources.SelectedRows)
					{
						ExecutableResource res = row.GetExtraData<ExecutableResource>("res");
						if (res.Source != null)
						{
							System.IO.File.WriteAllBytes(System.IO.Path.Combine(dlg.SelectedFileName, res.Identifier.ToString()), res.Source.GetData());
						}
					}
				}
			}
		}

		private Dictionary<ExecutableResourceType, EditorDocumentExplorerNode> _resourceNodes = new Dictionary<ExecutableResourceType, EditorDocumentExplorerNode>();

		[EventHandler(nameof(tvResources), nameof(Control.BeforeContextMenu))]
		private void tvResources_BeforeContextMenu(object sender, EventArgs e)
		{
			if (tvResources.SelectedRows.Count > 0)
			{
				tvResources.ContextMenuCommandID = "ExecutableEditor_ContextMenu_Resources_Selected";
			}
			else
			{
				tvResources.ContextMenuCommandID = "ExecutableEditor_ContextMenu_Resources_Unselected";
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			DocumentExplorer.Nodes.Clear();

			if (!IsCreated) return;

			tmSections.Rows.Clear();

			// tv.Nodes.Clear();
			// lvSections.Items.Clear();

			tbs.TabPages[0].Text = "Sections (0)";
			tbs.TabPages[1].Visible = false;

			ExecutableObjectModel executable = (ObjectModel as ExecutableObjectModel);
			if (executable == null) return;

			tbs.TabPages[0].Text = "Sections (" + executable.Sections.Count.ToString() + ")";

			foreach (ExecutableSection section in executable.Sections)
			{
				tmSections.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmSections.Columns[0], section.Name),
					new TreeModelRowColumn(tmSections.Columns[1], section.PhysicalAddress.ToString()),
					new TreeModelRowColumn(tmSections.Columns[2], section.VirtualAddress.ToString()),
					new TreeModelRowColumn(tmSections.Columns[3], section.VirtualSize.ToString())
				}));
				tmSections.Rows[tmSections.Rows.Count - 1].SetExtraData<ExecutableSection>("section", section);
			}

			EditorDocumentExplorerNode nodeResources = new EditorDocumentExplorerNode("Resources", StockType.Folder);
			foreach (ExecutableResource res in executable.Resources)
			{
				ExecutableResourceType restype = res.ResourceType;
				if (!_resourceNodes.ContainsKey(restype))
				{
					EditorDocumentExplorerNode nodeResType = new EditorDocumentExplorerNode(restype.Title, StockType.Folder);
					_resourceNodes[restype] = nodeResType;
					nodeResources.Nodes.Add(nodeResType);
				}

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvResources.Model.Columns[0], restype.Title),
					new TreeModelRowColumn(tvResources.Model.Columns[1], res.Identifier.ToString()),
					new TreeModelRowColumn(tvResources.Model.Columns[2], res.VirtualAddress),
					new TreeModelRowColumn(tvResources.Model.Columns[3], res.Length)
				});
				row.SetExtraData<ExecutableResource>("res", res);
				tvResources.Model.Rows.Add(row);

				EditorDocumentExplorerNode node;
				if (restype == ExecutableResourceTypes.Font)
				{
					node = new EditorDocumentExplorerNode(res.Identifier.ToString(), StockType.SelectFont);
				}
				else
				{
					node = new EditorDocumentExplorerNode(res.Identifier.ToString(), StockType.File);
				}
				_resourceNodes[restype].Nodes.Add(node);
			}
			DocumentExplorer.Nodes.Add(nodeResources);

			if (executable.ManagedAssembly != null)
			{
				tbs.TabPages[1].Visible = true;

				txtAssemblyName.Text = executable.ManagedAssembly.GetName().Name;
				txtAssemblyVersion.Text = executable.ManagedAssembly.GetName().Version.ToString();

				// pnlManagedAssembly.Assembly = executable.ManagedAssembly;
				UpdateTypeList();
			}
		}


		private void UpdateTypeList()
		{
			// FIXME: this doesn't crash .NET, but the app disappears when we select the Managed Assembly tab... if we call this function
			tmManagedDisassemblyTypes.Rows.Clear();

			ExecutableObjectModel executable = (ObjectModel as ExecutableObjectModel);
			if (executable == null)
				return;

			Type[] types = null;
			try
			{
				types = executable.ManagedAssembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				types = ex.Types;
			}

			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
					continue;

				string[] nameParts = types[i].FullName.Split(new char[] { '.' });
				bool nestedClass = false;
				while (nameParts[nameParts.Length - 1].Contains("+"))
				{
					// handle the case of nested classes
					nestedClass = true;
					Array.Resize<string>(ref nameParts, nameParts.Length + 1);
					string[] p = nameParts[nameParts.Length - 2].Split(new char[] { '+' });
					nameParts[nameParts.Length - 2] = p[0];
					nameParts[nameParts.Length - 1] = p[1];
				}

				TreeModelRow row = tmManagedDisassemblyTypes.RecursiveCreateTreeModelRow(tmManagedDisassemblyTypes.Columns[0], nameParts, new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[1], "Namespace")
				});

				if (nestedClass)
					row.ParentRow.RowColumns[1].Value = "Class";

				row.RowColumns.Add(new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[1], "Class"));

				SetupTypeTreeModelRow(row, types[i]);
			}
		}

		private void SetupTypeTreeModelRow(TreeModelRow row, Type type)
		{
			row.SetExtraData<Type>("item", type);

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

			TreeModelRow rowBaseTypes = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[0], "Base Types")
			});

			if (type.BaseType != null)
			{
				TreeModelRow rowBaseType = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[0], type.BaseType.FullName)
				});
				SetupTypeTreeModelRow(rowBaseType, type.BaseType);
				rowBaseTypes.Rows.Add(rowBaseType);
			}

			row.Rows.Add(rowBaseTypes);

			TreeModelRow rowDerivedTypes = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[0], "Derived Types")
			});
			row.Rows.Add(rowDerivedTypes);

			FieldInfo[] fields = type.GetFields(bindingFlags);
			for (int j = 0; j < fields.Length; j++)
			{
				TreeModelRow row2 = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[0], fields[j]),
					new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[1], "Field")
				});
				row2.SetExtraData<FieldInfo>("item", fields[j]);
				row.Rows.Add(row2);
			}

			EventInfo[] events = type.GetEvents(bindingFlags);
			for (int j = 0; j < events.Length; j++)
			{
				TreeModelRow row2 = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[0], events[j]),
					new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[1], "Event")
				});
				row2.SetExtraData<EventInfo>("item", events[j]);
				row.Rows.Add(row2);
			}

			PropertyInfo[] props = type.GetProperties(bindingFlags);
			for (int j = 0; j < props.Length; j++)
			{
				TreeModelRow row2 = new TreeModelRow(new TreeModelRowColumn[]
				{
						new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[0], props[j]),
						new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[1], "Property")
				});
				row2.SetExtraData<PropertyInfo>("item", props[j]);
				row.Rows.Add(row2);
			}

			MethodInfo[] meths = type.GetMethods(bindingFlags);
			for (int j = 0; j < meths.Length; j++)
			{
				if (meths[j].IsSpecialName && (meths[j].Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase) || meths[j].Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase)))
				{
					// we can be REASONABLY sure that this is a property setter / getter,
					// and as we've already gotten all the properties, ignore it
					continue;
				}

				TreeModelRow row2 = new TreeModelRow(new TreeModelRowColumn[]
				{
						new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[0], GetMethodTitle(meths[j])),
						new TreeModelRowColumn(tmManagedDisassemblyTypes.Columns[1], "Method")
				});
				row2.SetExtraData<MethodInfo>("item", meths[j]);
				row.Rows.Add(row2);
			}
		}

		private void tsbILSave_Click(object sender, EventArgs e)
		{
			MemberInfo t = tvManagedDisassemblyTypes.SelectedRows[0].GetExtraData<MemberInfo>("item");
			if (t == null)
				return;

			FileDialog dlg = new FileDialog();
			dlg.Text = "Save Code File";

			dlg.SelectedFileNames.Clear();

			dlg.SelectedFileNames.Add(t.Name + Language.CodeFileExtension);

			if (dlg.ShowDialog() == DialogResult.OK)
			{

			}
		}

		[EventHandler(nameof(cboManagedDisassemblyLanguage), "Changed")]
		void cboLanguage_Changed(object sender, EventArgs e)
		{
			CodeProvider provider = cboManagedDisassemblyLanguage.SelectedItem.GetExtraData<CodeProvider>("provider");
			Language = provider;
		}


		private CodeProvider _Language = CodeProvider.CSharp;
		public CodeProvider Language
		{
			get { return _Language; }
			set
			{
				_Language = value;
				tvTypes_SelectionChanged(this, EventArgs.Empty);
			}
		}

		private string GetAccessModifiersSourceCode(PropertyInfo mi)
		{
			return Language.GetAccessModifiers(mi);
		}
		private string GetAccessModifiersSourceCode(MethodInfo mi)
		{
			return Language.GetAccessModifiers(mi);
		}
		private string GetAccessModifiersSourceCode(FieldInfo mi)
		{
			return Language.GetAccessModifiers(mi);
		}
		private string GetAccessModifiersSourceCode(Type mi)
		{
			return Language.GetAccessModifiers(mi);
		}


		private string GetMethodTitle(MethodInfo mi)
		{
			StringBuilder sb = new StringBuilder();
			if (Language == null) Language = cboManagedDisassemblyLanguage.SelectedItem.GetExtraData<CodeProvider>("provider");

			string typeName = Language.GetTypeName(mi.ReturnType);
			sb.Append(mi.Name);
			sb.Append('(');
			sb.Append(')');
			sb.Append(" : ");
			sb.Append(typeName);
			return sb.ToString();
		}

		[EventHandler(nameof(tvManagedDisassemblyTypes), "SelectionChanged")]
		private void tvTypes_SelectionChanged(object sender, EventArgs e)
		{
			if (tvManagedDisassemblyTypes.SelectedRows.Count == 0)
				return;

			object item = tvManagedDisassemblyTypes.SelectedRows[0].GetExtraData("item");
			if (item is Type)
			{
				Type typ = (item as Type);
				txtManagedAssemblySource.Text = Language.GetSourceCode(typ, 0);
			}
			else if (item is MethodInfo)
			{
				txtManagedAssemblySource.Text = Language.GetSourceCode(item as MethodInfo, 0);
			}
		}
	}
}
