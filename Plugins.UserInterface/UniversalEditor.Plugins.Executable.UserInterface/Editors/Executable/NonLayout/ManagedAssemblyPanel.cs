//
//  ManagedAssemblyPanel.cs - provides a UWT Container with controls to edit managed assembly information for an ExecutableObjectModel
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
using System.Reflection;
using System.Text;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.Plugins.Executable.UserInterface.Editors.Executable
{
	/// <summary>
	/// Provides a UWT Container with controls to edit managed assembly information for an <see cref="ObjectModels.Executable.ExecutableObjectModel" />.
	/// </summary>
	public class ManagedAssemblyPanel : Container
	{
		private TextBox txtSearch = null;
		private ListViewControl tvTypes = null;
		private DefaultTreeModel tmTypes = null;
		private TextBox txtSource = null;

		private ComboBox cboLanguage = null;

		private Assembly _Assembly = null;
		public Assembly Assembly
		{
			get { return _Assembly; }
			set
			{
				_Assembly = value;
				UpdateTypeList();
			}
		}

		private void UpdateTypeList()
		{
			tmTypes.Rows.Clear();

			Type[] types = null;
			try
			{
				types = _Assembly.GetTypes();
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

				TreeModelRow row = tmTypes.RecursiveCreateTreeModelRow(tmTypes.Columns[0], nameParts, new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmTypes.Columns[1], "Namespace")
				});

				if (nestedClass)
					row.ParentRow.RowColumns[1].Value = "Class";

				row.RowColumns.Add(new TreeModelRowColumn(tmTypes.Columns[1], "Class"));

				SetupTypeTreeModelRow(row, types[i]);
			}
		}

		private void SetupTypeTreeModelRow(TreeModelRow row, Type type)
		{
			row.SetExtraData<Type>("item", type);

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

			TreeModelRow rowBaseTypes = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmTypes.Columns[0], "Base Types")
			});

			if (type.BaseType != null)
			{
				TreeModelRow rowBaseType = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmTypes.Columns[0], type.BaseType.FullName)
				});
				SetupTypeTreeModelRow(rowBaseType, type.BaseType);
				rowBaseTypes.Rows.Add(rowBaseType);
			}

			row.Rows.Add(rowBaseTypes);

			TreeModelRow rowDerivedTypes = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmTypes.Columns[0], "Derived Types")
			});
			row.Rows.Add(rowDerivedTypes);

			FieldInfo[] fields = type.GetFields(bindingFlags);
			for (int j = 0; j < fields.Length; j++)
			{
				TreeModelRow row2 = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmTypes.Columns[0], fields[j]),
					new TreeModelRowColumn(tmTypes.Columns[1], "Field")
				});
				row2.SetExtraData<FieldInfo>("item", fields[j]);
				row.Rows.Add(row2);
			}

			EventInfo[] events = type.GetEvents(bindingFlags);
			for (int j = 0; j < events.Length; j++)
			{
				TreeModelRow row2 = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmTypes.Columns[0], events[j]),
					new TreeModelRowColumn(tmTypes.Columns[1], "Event")
				});
				row2.SetExtraData<EventInfo>("item", events[j]);
				row.Rows.Add(row2);
			}

			PropertyInfo[] props = type.GetProperties(bindingFlags);
			for (int j = 0; j < props.Length; j++)
			{
				TreeModelRow row2 = new TreeModelRow(new TreeModelRowColumn[]
				{
						new TreeModelRowColumn(tmTypes.Columns[0], props[j]),
						new TreeModelRowColumn(tmTypes.Columns[1], "Property")
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
						new TreeModelRowColumn(tmTypes.Columns[0], GetMethodTitle(meths[j])),
						new TreeModelRowColumn(tmTypes.Columns[1], "Method")
				});
				row2.SetExtraData<MethodInfo>("item", meths[j]);
				row.Rows.Add(row2);
			}
		}

		private void tsbILSave_Click(object sender, EventArgs e)
		{
			MemberInfo t = tvTypes.SelectedRows[0].GetExtraData<MemberInfo>("item");
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

		public ManagedAssemblyPanel()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			Container ctToolbarAndOthers = new Container();
			ctToolbarAndOthers.Layout = new BoxLayout(Orientation.Horizontal);

			Toolbar tb = new Toolbar();
			tb.Items.Add(new ToolbarItemButton("tsbILSave", StockType.Save, tsbILSave_Click));
			ctToolbarAndOthers.Controls.Add(tb, new BoxLayout.Constraints(false, true));

			cboLanguage = new ComboBox();
			cboLanguage.Changed += cboLanguage_Changed;
			Type[] codeProviders = MBS.Framework.Reflection.GetAvailableTypes(new Type[] { typeof(CodeProvider) });
			DefaultTreeModel tmLanguage = new DefaultTreeModel(new Type[] { typeof(string) });

			/*
			tmLanguage.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmLanguage.Columns[0], "Raw Bytes")
			}));
			*/

			for (int i = 0; i < codeProviders.Length; i++)
			{
				CodeProvider codeProvider = (codeProviders[i].Assembly.CreateInstance(codeProviders[i].FullName) as CodeProvider);
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmLanguage.Columns[0], codeProvider.Title)
				});
				row.SetExtraData<CodeProvider>("provider", codeProvider);
				tmLanguage.Rows.Add(row);
			}
			if (tmLanguage.Rows.Count > 0)
			{
				cboLanguage.SelectedItem = tmLanguage.Rows[0];
			}
			cboLanguage.ReadOnly = true;
			cboLanguage.Model = tmLanguage;
			ctToolbarAndOthers.Controls.Add(cboLanguage, new BoxLayout.Constraints(false, false));

			Controls.Add(ctToolbarAndOthers, new BoxLayout.Constraints(false, true));

			SplitContainer scLeftRight = new SplitContainer(Orientation.Vertical);
			scLeftRight.SplitterPosition = 250;

			txtSearch = new TextBox();

			scLeftRight.Panel1.Layout = new BoxLayout(Orientation.Vertical);
			scLeftRight.Panel1.Controls.Add(txtSearch, new BoxLayout.Constraints(false, true));

			tvTypes = new ListViewControl();
			tvTypes.SelectionChanged += tvTypes_SelectionChanged;

			tmTypes = new DefaultTreeModel(new Type[] { typeof(string), typeof(string) });
			tvTypes.Model = tmTypes;

			tvTypes.Columns.Add(new ListViewColumnText(tmTypes.Columns[0], "Name"));
			tvTypes.Columns.Add(new ListViewColumnText(tmTypes.Columns[1], "Type"));
			scLeftRight.Panel1.Controls.Add(tvTypes, new BoxLayout.Constraints(true, true));

			scLeftRight.Panel2.Layout = new BoxLayout(Orientation.Vertical);
			txtSource = new TextBox();
			txtSource.Multiline = true;
			scLeftRight.Panel2.Controls.Add(txtSource, new BoxLayout.Constraints(true, true));

			Controls.Add(scLeftRight, new BoxLayout.Constraints(true, true));
		}

		void cboLanguage_Changed(object sender, EventArgs e)
		{
			CodeProvider provider = cboLanguage.SelectedItem.GetExtraData<CodeProvider>("provider");
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
			string typeName = Language.GetTypeName(mi.ReturnType);
			sb.Append(mi.Name);
			sb.Append('(');
			sb.Append(')');
			sb.Append(" : ");
			sb.Append(typeName);
			return sb.ToString();
		}

		private void tvTypes_SelectionChanged(object sender, EventArgs e)
		{
			if (tvTypes.SelectedRows.Count == 0)
				return;

			object item = tvTypes.SelectedRows[0].GetExtraData("item");
			if (item is Type)
			{
				Type typ = (item as Type);
				txtSource.Text = Language.GetSourceCode(typ, 0);
			}
			else if (item is MethodInfo)
			{
				txtSource.Text = Language.GetSourceCode(item as MethodInfo, 0);
			}
		}

	}
}
