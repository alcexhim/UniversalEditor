//
//  UnrealPackageImportPropertiesDialog.cs - provides a UWT-based CustomDialog for editing the properties of an import in an Unreal Engine package
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
using UniversalEditor.ObjectModels.UnrealEngine;

namespace UniversalEditor.Plugins.UnrealEngine.UserInterface.Dialogs.Unreal.Package
{
	/// <summary>
	/// Provides a UWT-based <see cref="CustomDialog" /> for editing the properties of an import in an Unreal Engine package.
	/// </summary>
	[ContainerLayout("~/Editors/Unreal/Package/Dialogs/UnrealPackageEntryPropertiesDialog.glade")]
	public class UnrealPackageEntryPropertiesDialog : CustomDialog
	{
		private TextBox txtObjectName;
		private ComboBox cboClassName;
		private ComboBox cboPackageName;
		private TextBox txtSourceData;
		private Button cmdSourceDataBrowse;

		private GroupBox fraFlags;
		private CheckBox chkFlagTransactional, chkFlagUnreachable, chkFlagPublic, chkFlagSourceModified, chkFlagGarbageCollect;
		private CheckBox chkFlagImporting, chkFlagExporting, chkFlagRequireLoad, chkFlagHighlightNameEliminateObject, chkFlagRemappedNameSingularFunction, chkFlagSuppressedStateChanged;

		public ImportTableEntry ImportTableEntry { get; set; } = null;
		public ExportTableEntry ExportTableEntry { get; set; } = null;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			if (ImportTableEntry != null)
			{
				txtObjectName.Text = ImportTableEntry.ObjectName.Name;
				cboClassName.Text = ImportTableEntry.ClassName.Name;
				cboPackageName.Text = ImportTableEntry.PackageName.Name;

				fraFlags.Visible = false;

				Text = "Import Table Entry Properties";
			}
			else if (ExportTableEntry != null)
			{
				txtObjectName.Text = ExportTableEntry.Name?.Name;
				cboClassName.Text = ExportTableEntry.ObjectClass?.Name?.Name;
				cboPackageName.Text = ExportTableEntry.ObjectParent?.Name?.Name;

				fraFlags.Visible = true;
				chkFlagTransactional.Checked = ((ExportTableEntry.Flags & ObjectFlags.Transactional) == ObjectFlags.Transactional);
				chkFlagUnreachable.Checked = ((ExportTableEntry.Flags & ObjectFlags.Unreachable) == ObjectFlags.Unreachable);
				chkFlagPublic.Checked = ((ExportTableEntry.Flags & ObjectFlags.Public) == ObjectFlags.Public);
				chkFlagSourceModified.Checked = ((ExportTableEntry.Flags & ObjectFlags.SourceModified) == ObjectFlags.SourceModified);
				chkFlagGarbageCollect.Checked = ((ExportTableEntry.Flags & ObjectFlags.GarbageCollect) == ObjectFlags.GarbageCollect);
				chkFlagImporting.Checked = ((ExportTableEntry.Flags & ObjectFlags.Importing) == ObjectFlags.Importing);
				chkFlagExporting.Checked = ((ExportTableEntry.Flags & ObjectFlags.Exporting) == ObjectFlags.Exporting);
				chkFlagRequireLoad.Checked = ((ExportTableEntry.Flags & ObjectFlags.RequireLoad) == ObjectFlags.RequireLoad);
				chkFlagHighlightNameEliminateObject.Checked = ((ExportTableEntry.Flags & ObjectFlags.HighlightedName) == ObjectFlags.HighlightedName);
				chkFlagRemappedNameSingularFunction.Checked = ((ExportTableEntry.Flags & ObjectFlags.RemappedName) == ObjectFlags.RemappedName);
				chkFlagSuppressedStateChanged.Checked = ((ExportTableEntry.Flags & ObjectFlags.Suppressed) == ObjectFlags.Suppressed);

				Text = "Export Table Entry Properties";
			}
		}
	}
}
