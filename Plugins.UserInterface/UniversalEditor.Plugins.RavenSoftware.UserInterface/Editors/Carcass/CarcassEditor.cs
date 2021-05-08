//
//  CarcassEditor.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.Plugins.RavenSoftware.ObjectModels.Carcass;
using UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Carcass.Dialogs;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Carcass
{
	[ContainerLayout("~/Editors/RavenSoftware/Carcass/CarcassEditor.glade")]
	public class CarcassEditor : Editor
	{
		private Toolbar tbModelFiles;
		private ListViewControl lvModelFiles;
		private CheckBox chkSkew90;
		private CheckBox chkSmoothAllSurfaces;
		private CheckBox chkRemoveDuplicateVertices;
		private CheckBox chkMakeSkinFile;
		private CheckBox chkKeepMotionBone;
		private NumericTextBox txtFramestepValue;
		private NumericTextBox txtOriginX;
		private NumericTextBox txtOriginY;
		private NumericTextBox txtOriginZ;
		private CheckBox chkMakeSkeleton;
		private CheckBox chkUseLegacyCompression;
		private FileChooserButton fcbSkeletonPath;
		private FileChooserButton fcbSkeletonGLAFile;
		private NumericTextBox txtSkeletonScale;
		private Toolbar tbPCJList;
		private ListViewControl lvPCJList;
		private TextBox txtPCJName;

		private Label lblSkeletonPath;
		private Label lblSkeletonGLAFile;
		private Label lblSkeletonScale;
		private Container fraPCJList;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(CarcassObjectModel));
			}
			return _er;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			fcbSkeletonPath.RequireExistingFile = false;
			fcbSkeletonGLAFile.RequireExistingFile = false;

			chkMakeSkinFile.Changed += chk_Changed;
			chkKeepMotionBone.Changed += chk_Changed;
			chkSmoothAllSurfaces.Changed += chk_Changed;
			chkUseLegacyCompression.Changed += chk_Changed;
			chkRemoveDuplicateVertices.Changed += chk_Changed;

			txtOriginX.Changed += txtOrigin_Changed;
			txtOriginY.Changed += txtOrigin_Changed;
			txtOriginZ.Changed += txtOrigin_Changed;

			(tbModelFiles.Items["tsbModelFileAdd"] as ToolbarItemButton).Click += tsbModelFileAdd_Click;
			(tbModelFiles.Items["tsbModelFileEdit"] as ToolbarItemButton).Click += tsbModelFileEdit_Click;
			(tbModelFiles.Items["tsbModelFileRemove"] as ToolbarItemButton).Click += tsbModelFileRemove_Click;
			(tbModelFiles.Items["tsbModelFileMoveUp"] as ToolbarItemButton).Click += tsbModelFileMoveUp_Click;
			(tbModelFiles.Items["tsbModelFileMoveDown"] as ToolbarItemButton).Click += tsbModelFileMoveDown_Click;

			(tbPCJList.Items["tsbPCJListAdd"] as ToolbarItemButton).Click += tsbPCJListAdd_Click;
			(tbPCJList.Items["tsbPCJListRemove"] as ToolbarItemButton).Click += tsbPCJListRemove_Click;
			(tbPCJList.Items["tsbPCJListMoveUp"] as ToolbarItemButton).Click += tsbPCJListMoveUp_Click;
			(tbPCJList.Items["tsbPCJListMoveDown"] as ToolbarItemButton).Click += tsbPCJListMoveDown_Click;

			OnObjectModelChanged(e);
		}

		private void tsbPCJListAdd_Click(object sender, EventArgs e)
		{
			CarcassObjectModel car = (ObjectModel as CarcassObjectModel);
			if (car == null) return;

			if (txtPCJName.Text == String.Empty)
			{
				MessageDialog.ShowDialog("Please enter a name for the PCJ to add it to the list!", "Missing PCJ Name", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			BeginEdit();

			lvPCJList.Model.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(lvPCJList.Model.Columns[0], txtPCJName.Text)
			}));
			car.PCJ.Add(txtPCJName.Text);
			txtPCJName.Text = String.Empty;

			EndEdit();
		}
		private void tsbPCJListRemove_Click(object sender, EventArgs e)
		{
			if (lvPCJList.SelectedRows.Count > 0)
			{
				// FIXME: this CRASHES!!! in GTK native code: "gtk_tree_store_remove: assertion failed (parent != NULL)"
				for (int i = 0; i < lvPCJList.SelectedRows.Count; i++)
				{
					lvPCJList.Model.Rows.Remove(lvPCJList.SelectedRows[i]);
				}
			}
		}
		private void tsbPCJListMoveUp_Click(object sender, EventArgs e)
		{

		}
		private void tsbPCJListMoveDown_Click(object sender, EventArgs e)
		{

		}

		[EventHandler(nameof(chkMakeSkeleton), "Changed")]
		private void chkMakeSkeleton_Changed(object sender, EventArgs e)
		{
			bool enabled = chkMakeSkeleton.Checked;
			chkUseLegacyCompression.Enabled = enabled;
			lblSkeletonPath.Enabled = enabled;
			fcbSkeletonPath.Enabled = enabled;
			lblSkeletonGLAFile.Enabled = enabled;
			fcbSkeletonGLAFile.Enabled = enabled;
			lblSkeletonScale.Enabled = enabled;
			txtSkeletonScale.Enabled = enabled;
			chkKeepMotionBone.Enabled = enabled;
			// fraPCJList.Enabled = enabled;
			tbPCJList.Enabled = enabled;
			lvPCJList.Enabled = enabled;
		}

		private void tsbModelFileAdd_Click(object sender, EventArgs e)
		{
			ModelReferencePropertiesDialog dlg = new ModelReferencePropertiesDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ModelReference ase = new ModelReference();
				ase.FileName = dlg.SelectedFileName;
				BeginEdit();
				EndEdit();
			}
		}
		private void tsbModelFileEdit_Click(object sender, EventArgs e)
		{
			if (lvModelFiles.SelectedRows.Count != 1)
			{
				MessageDialog.ShowDialog("Please select EXACTLY ONE item to edit!", "Invalid Selection", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			ModelReference mr = lvModelFiles.SelectedRows[0].GetExtraData<ModelReference>("model");
			ModelReferencePropertiesDialog dlg = new ModelReferencePropertiesDialog();
			dlg.Item = mr;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();
				EndEdit();
			}
		}
		private void tsbModelFileRemove_Click(object sender, EventArgs e)
		{

		}
		private void tsbModelFileMoveUp_Click(object sender, EventArgs e)
		{

		}
		private void tsbModelFileMoveDown_Click(object sender, EventArgs e)
		{

		}

		[EventHandler(nameof(lvModelFiles), "RowActivated")]
		private void lvModelFiles_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			tsbModelFileEdit_Click(sender, e);
		}

		[EventHandler(nameof(fcbSkeletonPath), "Changed")]
		private void fcbSkeletonPath_Changed(object sender, EventArgs e)
		{
			CarcassObjectModel car = (ObjectModel as CarcassObjectModel);
			if (car == null) return;

			BeginEdit();
			car.SkeletonFileName = fcbSkeletonPath.SelectedFileName;
			EndEdit();
		}

		[EventHandler(nameof(fcbSkeletonGLAFile), "Changed")]
		private void fcbSkeletonGLAFile_Changed(object sender, EventArgs e)
		{
			CarcassObjectModel car = (ObjectModel as CarcassObjectModel);
			if (car == null) return;

			BeginEdit();
			car.GLAFileName = fcbSkeletonGLAFile.SelectedFileName;
			EndEdit();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			lvModelFiles.Model.Rows.Clear();

			CarcassObjectModel car = (ObjectModel as CarcassObjectModel);
			if (car == null) return;

			chkSmoothAllSurfaces.Checked = car.SmoothAllSurfaces;
			chkRemoveDuplicateVertices.Checked = car.RemoveDuplicateVertices;
			chkMakeSkinFile.Checked = car.MakeSkin;
			chkMakeSkeleton.Checked = (car.SkeletonFileName != null);
			chkKeepMotionBone.Checked = car.KeepMotionBone;

			_inhibit_txtOrigin_Changed = true;
			txtOriginX.Value = car.Origin.X;
			txtOriginY.Value = car.Origin.Y;
			txtOriginZ.Value = car.Origin.Z;
			_inhibit_txtOrigin_Changed = false;

			chkUseLegacyCompression.Checked = car.UseLegacyCompression;
			txtSkeletonScale.Value = car.Scale;

			fcbSkeletonPath.SelectedFileName = car.SkeletonFileName;
			fcbSkeletonGLAFile.SelectedFileName = car.GLAFileName;

			int frameStart = 0, frameCount = 250;
			for (int i = 0; i < car.ModelReferences.Count; i++)
			{
				ModelReference mr = car.ModelReferences[i];
				string modelName = System.IO.Path.GetFileNameWithoutExtension(mr.FileName).ToUpper();
				string enumName = mr.EnumName;

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(lvModelFiles.Model.Columns[0], frameStart),
					new TreeModelRowColumn(lvModelFiles.Model.Columns[1], frameCount),
					new TreeModelRowColumn(lvModelFiles.Model.Columns[2], mr.LoopCount),
					new TreeModelRowColumn(lvModelFiles.Model.Columns[3], mr.FrameSpeed),
					new TreeModelRowColumn(lvModelFiles.Model.Columns[4], modelName),
					new TreeModelRowColumn(lvModelFiles.Model.Columns[5], enumName ?? String.Format("( (BAD: {0}) )", modelName))
				});
				row.SetExtraData<ModelReference>("model", mr);
				lvModelFiles.Model.Rows.Add(row);
				frameStart += frameCount;
			}

			for (int i = 0; i < car.PCJ.Count; i++)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(lvPCJList.Model.Columns[0], car.PCJ[i])
				});
				row.SetExtraData<int>("index", i);
				lvPCJList.Model.Rows.Add(row);
			}
		}

		private void chk_Changed(object sender, EventArgs e)
		{
			CarcassObjectModel car = (ObjectModel as CarcassObjectModel);
			if (car == null) return;

			CheckBox chk = (sender as CheckBox);

			BeginEdit();
			if (chk == chkMakeSkinFile)
			{
				car.MakeSkin = chk.Checked;
			}
			else if (chk == chkKeepMotionBone)
			{
				car.KeepMotionBone = chk.Checked;
			}
			else if (chk == chkSmoothAllSurfaces)
			{
				car.SmoothAllSurfaces = chk.Checked;
			}
			else if (chk == chkUseLegacyCompression)
			{
				car.UseLegacyCompression = chk.Checked;
			}
			else if (chk == chkRemoveDuplicateVertices)
			{
				car.RemoveDuplicateVertices = chk.Checked;
			}
			EndEdit();
		}

		private bool _inhibit_txtOrigin_Changed = false;
		private void txtOrigin_Changed(object sender, EventArgs e)
		{
			CarcassObjectModel car = (ObjectModel as CarcassObjectModel);
			if (car == null) return;

			if (_inhibit_txtOrigin_Changed) return;

			BeginEdit();
			car.Origin = new PositionVector3(txtOriginX.Value, txtOriginY.Value, txtOriginZ.Value);
			EndEdit();
		}

		[EventHandler(nameof(txtFramestepValue), "Changed")]
		private void txtFramestepValue_Changed(object sender, EventArgs e)
		{
			CarcassObjectModel car = (ObjectModel as CarcassObjectModel);
			if (car == null) return;

			BeginEdit();
			car.Framestep = (int)txtFramestepValue.Value;
			EndEdit();
		}

		[EventHandler(nameof(txtSkeletonScale), "Changed")]
		private void txtSkeletonScale_Changed(object sender, EventArgs e)
		{
			CarcassObjectModel car = (ObjectModel as CarcassObjectModel);
			if (car == null) return;

			BeginEdit();
			car.Scale = txtSkeletonScale.Value;
			EndEdit();
		}

		public override void UpdateSelections()
		{

		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
	}
}
