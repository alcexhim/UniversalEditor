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

		[EventHandler(nameof(tvExports), "RowActivated")]
		void tvExports_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			ExportTableEntry item = e.Row.GetExtraData<ExportTableEntry>("item");

			UnrealPackageEntryPropertiesDialog dlg = new UnrealPackageEntryPropertiesDialog();
			dlg.ExportTableEntry = item;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
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
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			DefaultTreeModel tmExports = (tvExports.Model as DefaultTreeModel);
			DefaultTreeModel tmImports = (tvImports.Model as DefaultTreeModel);

			tmExports.Rows.Clear();
			tmImports.Rows.Clear();

			UnrealPackageObjectModel package = (ObjectModel as UnrealPackageObjectModel);
			if (package == null) return;

			for (int i = 0; i < package.ExportTableEntries.Count; i++)
			{
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
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmExports.Columns[0], package.ExportTableEntries[i].Group?.Name),
					new TreeModelRowColumn(tmExports.Columns[1], package.ExportTableEntries[i].ObjectClass),
					new TreeModelRowColumn(tmExports.Columns[2], package.ExportTableEntries[i].ObjectParent),
					new TreeModelRowColumn(tmExports.Columns[3], package.ExportTableEntries[i].Name?.Name),
					new TreeModelRowColumn(tmExports.Columns[4], package.ExportTableEntries[i].Flags),
					new TreeModelRowColumn(tmExports.Columns[5], package.ExportTableEntries[i].Size),
					new TreeModelRowColumn(tmExports.Columns[6], package.ExportTableEntries[i].Offset)
				});
				row.SetExtraData<ExportTableEntry>("item", package.ExportTableEntries[i]);
				// rowParent2.Rows.Add(row);
				tmExports.Rows.Add(row);
			}
			for (int i = 0; i < package.ImportTableEntries.Count; i++)
			{
				string rowName = package.ImportTableEntries[i].Package?.Name?.ToString() ?? String.Empty;
				TreeModelRow rowParent = tmImports.Rows[rowName];
				if (rowParent == null)
				{
					rowParent = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmImports.Columns[0], rowName)
					});
					rowParent.Name = rowName;
					tmImports.Rows.Add(rowParent);
				}

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmImports.Columns[0], package.ImportTableEntries[i].Package),
					new TreeModelRowColumn(tmImports.Columns[1], package.ImportTableEntries[i].ClassName),
					new TreeModelRowColumn(tmImports.Columns[2], package.ImportTableEntries[i].PackageName),
					new TreeModelRowColumn(tmImports.Columns[3], package.ImportTableEntries[i].ObjectName)
				});
				row.SetExtraData<ImportTableEntry>("item", package.ImportTableEntries[i]);
				rowParent.Rows.Add(row);
			}
		}
	}
}
