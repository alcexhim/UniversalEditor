//
//  PictureCollectionEditor.cs - provides a UWT-based Editor for PictureCollectionObjectModel
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker
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
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.Bitmap;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.ObjectModels.Multimedia.SpriteAnimationCollection;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.PictureCollection
{
	/// <summary>
	/// Provides a UWT-based Editor for <see cref="PictureCollectionObjectModel"/>.
	/// </summary>
	[ContainerLayout("~/Editors/Multimedia/PictureCollection/PictureCollectionEditor.glade")]
	public class PictureCollectionEditor : Editor
	{
		private Button cmdSave;
		private Button cmdSaveAll;
		private NumericTextBox txtFrameIndex;

		private NumericTextBox txtQuickAnimateStartFrame;
		private NumericTextBox txtQuickAnimateEndFrame;
		private NumericTextBox txtQuickAnimateFrameDuration;

		private Button cmdQuickAnimatePlay;
		private Button cmdQuickAnimateSave;

		private Toolbar tbAnimations;

		private ListViewControl tvAnimations;
		private DefaultTreeModel tmAnimations;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(PictureCollectionObjectModel));
			}
			return _er;
		}

		[EventHandler(nameof(txtFrameIndex), "Changed")]
		private void txtFrameIndex_Changed(object sender, EventArgs e)
		{
			PictureCollectionObjectModel coll = (ObjectModel as PictureCollectionObjectModel);
			if (coll == null) return;

			cmdSave.Enabled = (SelectedFrameIndex >= 0 && SelectedFrameIndex < coll.Pictures.Count);

			if (txtFrameIndex.Value >= 0 && txtFrameIndex.Value < coll.Pictures.Count)
				picFrame.Image = coll.Pictures[(int)txtFrameIndex.Value].ToImage();
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			(tbAnimations.Items["tsbAnimationAdd"] as ToolbarItemButton).Click += tsbAnimationAdd_Click;
			(tbAnimations.Items["tsbAnimationEdit"] as ToolbarItemButton).Click += tsbAnimationEdit_Click;
			(tbAnimations.Items["tsbAnimationRemove"] as ToolbarItemButton).Click += tsbAnimationRemove_Click;

			(tbAnimations.Items["tsbAnimationLoad"] as ToolbarItemButton).Click += tsbAnimationOpen_Click;
			(tbAnimations.Items["tsbAnimationSave"] as ToolbarItemButton).Click += tsbAnimationSave_Click;
			OnObjectModelChanged(e);
		}

		private void tsbAnimationAdd_Click(object sender, EventArgs e)
		{
			Dialogs.AnimationPropertiesDialog dlg = new Dialogs.AnimationPropertiesDialog();
			dlg.PictureCollection = (ObjectModel as PictureCollectionObjectModel);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				SpriteAnimation anim = new SpriteAnimation();
				anim.Name = dlg.AnimationName;
				for (int i = 0; i < dlg.AnimationFrames.Count; i++)
				{
					anim.Frames.Add(dlg.AnimationFrames[i]);
				}

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmAnimations.Columns[0], anim.Name),
					new TreeModelRowColumn(tmAnimations.Columns[1], anim.Frames.Count)
				});
				row.SetExtraData<SpriteAnimation>("anim", anim);

				tmAnimations.Rows.Add(row);
			}
		}
		private void tsbAnimationEdit_Click(object sender, EventArgs e)
		{
			if (tvAnimations.SelectedRows.Count != 1)
			{
				MessageDialog.ShowDialog("Please select EXACTLY ONE animation to edit.", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			TreeModelRow row = tvAnimations.SelectedRows[0];
			SpriteAnimation anim = row.GetExtraData<SpriteAnimation>("anim");

			Dialogs.AnimationPropertiesDialog dlg = new Dialogs.AnimationPropertiesDialog();
			dlg.AnimationName = anim.Name;
			dlg.PictureCollection = (ObjectModel as PictureCollectionObjectModel);
			for (int i = 0; i < anim.Frames.Count; i++)
			{
				dlg.AnimationFrames.Add(anim.Frames[i]);
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				anim.Name = dlg.AnimationName;

				anim.Frames.Clear();
				for (int i = 0; i < dlg.AnimationFrames.Count; i++)
				{
					anim.Frames.Add(dlg.AnimationFrames[i]);
				}

				row.RowColumns[0].Value = anim.Name;
				row.RowColumns[1].Value = anim.Frames.Count.ToString();
			}
		}
		private void tsbAnimationRemove_Click(object sender, EventArgs e)
		{
			if (tvAnimations.SelectedRows.Count < 1)
			{
				MessageDialog.ShowDialog("Please select AT LEAST ONE animation to remove.", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			if (MessageDialog.ShowDialog(String.Format("Are you sure you want to remove the {0}?", tvAnimations.SelectedRows.Count == 1 ? "selected animation" : String.Format("{0} selected animations", tvAnimations.SelectedRows.Count)), "Remove Animations", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) == DialogResult.Yes)
			{
				// FIXME: GTK doesn't remove the same row from the SelectedRows even when the row gets removed from the model...
				// probably because the GtkTreeView automatically selects the other row. this is convenient for the user but not for the developer!
				for (int i = 0; i < tvAnimations.SelectedRows.Count; i++)
				{
					tmAnimations.Rows.Remove(tvAnimations.SelectedRows[i]);
				}
			}
		}

		private void tsbAnimationOpen_Click(object sender, EventArgs e)
		{
			FileDialog fd = new FileDialog();
			fd.Mode = FileDialogMode.Open;

			SpriteAnimationCollectionObjectModel wow = new SpriteAnimationCollectionObjectModel();
			if (fd.ShowDialog() == DialogResult.OK)
			{
				FileAccessor fa = new FileAccessor(fd.SelectedFileName);
				DataFormatReference[] dfrs = Common.Reflection.GetAvailableDataFormats(fa);

				for (int i = 0; i < dfrs.Length; i++)
				{
					DataFormat wdf = dfrs[i].Create();

					try
					{
						Document.Load(wow, wdf, fa);

						foreach (SpriteAnimation anim in wow.Animations)
						{
							TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
							{
								new TreeModelRowColumn(tmAnimations.Columns[0], anim.Name),
								new TreeModelRowColumn(tmAnimations.Columns[1], anim.Frames.Count.ToString())
							});
							row.SetExtraData<SpriteAnimation>("anim", anim);
							tmAnimations.Rows.Add(row);
						}

						break;
					}
					catch (ObjectModelNotSupportedException ex)
					{
						continue;
					}
					catch (InvalidDataFormatException ex)
					{
						continue;
					}
				}
			}
		}
		private void tsbAnimationSave_Click(object sender, EventArgs e)
		{

		}

		[EventHandler(nameof(tvAnimations), "RowActivated")]
		private void tvAnimations_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			if (e.Row == null) return;

			SpriteAnimation anim = e.Row.GetExtraData<SpriteAnimation>("anim");
			if (anim == null) return;

			if (anim.Frames.Count == 0) return;

			_QuickAnimateAnimation = anim;

			PlayAnimation();
		}

		private SpriteAnimation _QuickAnimateAnimation = null;

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			cmdSave.Enabled = false;
			cmdSaveAll.Enabled = false;

			DocumentExplorer.Nodes.Clear();

			PictureCollectionObjectModel coll = (ObjectModel as PictureCollectionObjectModel);
			if (coll == null) return;

			EditorDocumentExplorerNode nodeFrames = DocumentExplorer.Nodes.Add("Frames");
			for (int i = 0; i < coll.Pictures.Count; i++)
			{
				nodeFrames.Nodes.Add(String.Format("Frame {0}", (i + 1).ToString()));
				nodeFrames.Nodes[i].SetExtraData("index", i);
			}

			txtFrameIndex.Maximum = coll.Pictures.Count - 1;
			cmdSave.Enabled = coll.Pictures.Count > 0;
			cmdSaveAll.Enabled = coll.Pictures.Count > 0;

			if (coll.Pictures.Count > 0)
				picFrame.Image = coll.Pictures[0].ToImage();
		}

		protected override void OnDocumentExplorerSelectionChanged(EditorDocumentExplorerSelectionChangedEventArgs e)
		{
			base.OnDocumentExplorerSelectionChanged(e);

			if (e.Node == null) return;

			int index = e.Node.GetExtraData<int>("index");
			SelectedFrameIndex = index;
		}

		public int SelectedFrameIndex
		{
			get { return (int)txtFrameIndex.Value; }
			set { txtFrameIndex.Value = value; Refresh(); }
		}

		private PictureFrame picFrame = null;

		[EventHandler(nameof(cmdSave), "Click")]
		void CmdSave_Click(object sender, EventArgs e)
		{
			PictureCollectionObjectModel coll = ObjectModel as PictureCollectionObjectModel;
			if (SelectedFrameIndex < 0 || SelectedFrameIndex >= coll.Pictures.Count)
				return;

			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Save;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				PictureObjectModel pic = coll.Pictures[SelectedFrameIndex];
				BitmapDataFormat bmp = new BitmapDataFormat();

				FileAccessor fa = new FileAccessor(dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1]);
				fa.AllowWrite = true;
				fa.ForceOverwrite = true;
				fa.Open();
				Document.Save(pic, bmp, fa, true);
			}
		}
		[EventHandler(nameof(cmdSaveAll), "Click")]
		void CmdSaveAll_Click(object sender, EventArgs e)
		{
			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.SelectFolder;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				PictureCollectionObjectModel coll = ObjectModel as PictureCollectionObjectModel;

				string directoryName = dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1];
				if (!System.IO.Directory.Exists(directoryName))
				{
					System.IO.Directory.CreateDirectory(directoryName);
				}

				for (int i = 0; i < coll.Pictures.Count; i++)
				{
					PictureObjectModel pic = coll.Pictures[i];
					BitmapDataFormat bmp = new BitmapDataFormat();

					FileAccessor fa = new FileAccessor(directoryName + System.IO.Path.DirectorySeparatorChar.ToString() + i.ToString().PadLeft(8, '0') + ".bmp");
					fa.AllowWrite = true;
					fa.ForceOverwrite = true;
					fa.Open();
					Document.Save(pic, bmp, fa, true);
				}
			}
		}

		private Timer tmr = new Timer();
		public PictureCollectionEditor()
		{
			tmr.Tick += Timer_Tick;
		}

		private void PlayAnimation()
		{
			if (tmr.Enabled) return;
			cmdQuickAnimatePlay.Text = "_Pause";

			if (_QuickAnimateAnimation != null)
			{
				txtQuickAnimateStartFrame.Enabled = false;
				txtQuickAnimateEndFrame.Enabled = false;

			}
			if ((int)txtQuickAnimateFrameDuration.Value == 0)
			{
				txtQuickAnimateFrameDuration.Value = 150;  // anim.FrameDuration;
			}

			tmr.Duration = (int)txtQuickAnimateFrameDuration.Value;
			tmr.Enabled = true;
		}
		private void StopAnimation()
		{
			if (!tmr.Enabled) return;

			tmr.Enabled = false;

			txtQuickAnimateStartFrame.Enabled = true;
			txtQuickAnimateEndFrame.Enabled = true;

			cmdQuickAnimatePlay.Text = "_Play";
		}

		[EventHandler(nameof(cmdQuickAnimatePlay), "Click")]
		private void cmdQuickAnimatePlay_Click(object sender, EventArgs e)
		{
			if (!tmr.Enabled)
			{
				if ((int)(txtQuickAnimateEndFrame.Value - txtQuickAnimateStartFrame.Value) <= 0)
				{
					MessageDialog.ShowDialog("Please select a range of frames to animate.", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
					return;
				}

				PlayAnimation();
			}
			else
			{
				StopAnimation();

				_QuickAnimateAnimation = null;
			}
		}

		private int _c_frame = 0, _c_frameIndex = 0;
		private void Timer_Tick(object sender, EventArgs e)
		{
			txtFrameIndex.Value = _c_frame;

			_c_frame++;
			_c_frameIndex++;

			if (_QuickAnimateAnimation != null)
			{
				if (_c_frameIndex >= _QuickAnimateAnimation.Frames.Count)
				{
					_c_frameIndex = 0;
				}
				_c_frame = _QuickAnimateAnimation.Frames[_c_frameIndex];
			}
			else
			{
				if (_c_frame > (int)txtQuickAnimateEndFrame.Value)
				{
					_c_frame = (int)txtQuickAnimateStartFrame.Value;
				}
			}
		}

		[EventHandler(nameof(cmdQuickAnimateSave), "Click")]
		private void cmdQuickAnimateSave_Click(object sender, EventArgs e)
		{

		}


		public override void UpdateSelections()
		{
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			return null;
		}


	}
}
