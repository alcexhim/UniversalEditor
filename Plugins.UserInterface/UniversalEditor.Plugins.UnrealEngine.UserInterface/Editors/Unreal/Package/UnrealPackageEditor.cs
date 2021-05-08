//
//  UnrealPackageEditor.cs - provides a UWT-based Editor for an UnrealPackageObjectModel
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.ObjectModels.UnrealEngine;
using UniversalEditor.Plugins.UnrealEngine.UserInterface.Dialogs.Unreal.Package;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.UnrealEngine.UserInterface.Editors.Unreal.Package
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for an <see cref="UnrealPackageObjectModel" />.
	/// </summary>
	[ContainerLayout("~/Editors/Unreal/Package/UnrealPackageEditor.glade")]
	public class UnrealPackageEditor : Editor
	{
		private ListViewControl tvImports;
		private ListViewControl tvExports;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(UnrealPackageObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			DefaultTreeModel tmExports = (tvExports.Model as DefaultTreeModel);
			DefaultTreeModel tmImports = (tvImports.Model as DefaultTreeModel);

			tvExports.ContextMenuCommandID = "UnrealPackageEditor_ExportTreeViewContextMenu";
			tvImports.ContextMenuCommandID = "UnrealPackageEditor_ImportTreeViewContextMenu";

			Context.AttachCommandEventHandler("UnrealPackageEditor_ExportTreeViewContextMenu_Export", UnrealPackageEditor_ExportTreeViewContextMenu_Export);

			Context.AttachCommandEventHandler("UnrealPackageEditor_ImportTreeViewContextMenu_Add_Existing", UnrealPackageEditor_ImportTreeViewContextMenu_Add_Existing);
			Context.AttachCommandEventHandler("UnrealPackageEditor_ExportTreeViewContextMenu_Add_Existing", UnrealPackageEditor_ExportTreeViewContextMenu_Add_Existing);

			OnObjectModelChanged(e);
		}

		private void UnrealPackageEditor_ExportTreeViewContextMenu_Export(object sender, EventArgs e)
		{
			if (tvExports.SelectedRows.Count <= 0)
				return;

			ExportTableEntry item = tvExports.SelectedRows[0].GetExtraData<ExportTableEntry>("item");
			byte[] data = item.GetData();

			FileDialog sfd = new FileDialog
			{
				Mode = FileDialogMode.Save,
				SelectedFileName = String.Format("{0}{1}{2}", item.Group?.Name?.Name, item.Group == null ? String.Empty : ".", item.Name?.Name)
			};
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				System.IO.File.WriteAllBytes(sfd.SelectedFileName, data);
			}
		}
		private void UnrealPackageEditor_ExportTreeViewContextMenu_Add_Existing(object sender, EventArgs e)
		{
			ExportTableEntry item = new ExportTableEntry();

			FileDialog ofd = new FileDialog
			{
				Mode = FileDialogMode.Open
			};
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				item.Name = new NameTableEntry();
				item.Name.Name = System.IO.Path.GetFileNameWithoutExtension(ofd.SelectedFileName);

				item.SetData(System.IO.File.ReadAllBytes(ofd.SelectedFileName));

				UnrealPackageEntryPropertiesDialog dlg = new UnrealPackageEntryPropertiesDialog();
				dlg.ExportTableEntry = item;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					UIAddExport(item);
				}
			}
		}
		private void UnrealPackageEditor_ImportTreeViewContextMenu_Add_Existing(object sender, EventArgs e)
		{
			ImportTableEntry item = new ImportTableEntry();

			UnrealPackageEntryPropertiesDialog dlg = new UnrealPackageEntryPropertiesDialog();
			dlg.ImportTableEntry = item;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				UIAddImport(item);
			}
		}

		[EventHandler(nameof(tvExports), "RowActivated")]
		void tvExports_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			ExportTableEntry item = e.Row.GetExtraData<ExportTableEntry>("item");

			UnrealPackageEntryPropertiesDialog dlg = new UnrealPackageEntryPropertiesDialog();
			dlg.ExportTableEntry = item;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				UIUpdateExport(item, e.Row);
			}
		}

		[EventHandler(nameof(tvImports), "RowActivated")]
		void tvImports_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			ImportTableEntry item = e.Row.GetExtraData<ImportTableEntry>("item");

			UnrealPackageEntryPropertiesDialog dlg = new UnrealPackageEntryPropertiesDialog();
			dlg.ImportTableEntry = item;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				UIUpdateImport(item, e.Row);
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			tvExports.Model.Rows.Clear();
			tvImports.Model.Rows.Clear();

			UnrealPackageObjectModel package = (ObjectModel as UnrealPackageObjectModel);
			if (package == null) return;

			for (int i = 0; i < package.ExportTableEntries.Count; i++)
			{
				UIAddExport(package.ExportTableEntries[i]);
				/*
				string rowName = package.ExportTableEntries[i].Group?.Name?.ToString() ?? String.Empty;
				TreeModelRow rowParent = tmExports.Rows[rowName];
				if (rowParent == null)
				{
					rowParent = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmExports.Columns[0], rowName)
					});
					rowParent.Name = rowName;
					tmExports.Rows.Add(rowParent);
				}

				string rowName2 = package.ExportTableEntries[i].ObjectParent?.Name?.ToString() ?? String.Empty;
				TreeModelRow rowParent2 = rowParent.Rows[rowName2];
				if (rowParent2 == null)
				{
					rowParent2 = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmExports.Columns[0], rowName2)
					});
					rowParent2.Name = rowName2;
					rowParent.Rows.Add(rowParent2);
				}
				*/
			}
			for (int i = 0; i < package.ImportTableEntries.Count; i++)
			{
				UIAddImport(package.ImportTableEntries[i]);
			}
		}

		private void UIAddImport(ImportTableEntry item)
		{
			string rowName = item.Package?.Name?.ToString() ?? String.Empty;
			TreeModelRow rowParent = tvImports.Model.Rows[rowName];
			if (rowParent == null)
			{
				rowParent = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvImports.Model.Columns[0], rowName)
				});
				rowParent.Name = rowName;
				tvImports.Model.Rows.Add(rowParent);
			}

			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tvImports.Model.Columns[0], item.Package),
				new TreeModelRowColumn(tvImports.Model.Columns[1], item.ClassName),
				new TreeModelRowColumn(tvImports.Model.Columns[2], item.PackageName),
				new TreeModelRowColumn(tvImports.Model.Columns[3], item.ObjectName)
			});
			row.SetExtraData<ImportTableEntry>("item", item);
			rowParent.Rows.Add(row);
		}
		private void UIUpdateImport(ImportTableEntry item, TreeModelRow row)
		{
			row.RowColumns[0].Value = item.Package;
			row.RowColumns[1].Value = item.ClassName;
			row.RowColumns[2].Value = item.PackageName;
			row.RowColumns[3].Value = item.ObjectName;
		}
		private void UIAddExport(ExportTableEntry item)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tvExports.Model.Columns[0], item.Group?.Name),
				new TreeModelRowColumn(tvExports.Model.Columns[1], item.ObjectClass),
				new TreeModelRowColumn(tvExports.Model.Columns[2], item.ObjectParent),
				new TreeModelRowColumn(tvExports.Model.Columns[3], item.Name?.Name),
				new TreeModelRowColumn(tvExports.Model.Columns[4], item.Flags),
				new TreeModelRowColumn(tvExports.Model.Columns[5], item.Size),
				new TreeModelRowColumn(tvExports.Model.Columns[6], item.Offset)
			});
			row.SetExtraData<ExportTableEntry>("item", item);
			// rowParent2.Rows.Add(row);
			tvExports.Model.Rows.Add(row);
		}
		private void UIUpdateExport(ExportTableEntry item, TreeModelRow row)
		{
			row.RowColumns[0].Value = item.Group?.Name;
			row.RowColumns[1].Value = item.ObjectClass;
			row.RowColumns[2].Value = item.ObjectParent;
			row.RowColumns[3].Value = item.Name?.Name;
			row.RowColumns[4].Value = item.Flags;
			row.RowColumns[5].Value = item.Size;
			row.RowColumns[6].Value = item.Offset;
		}
	}
}
