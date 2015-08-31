using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs.FileSystem.Internal
{
	internal partial class FilePropertiesDialogImpl : Form
	{
		public FilePropertiesDialogImpl()
		{
			InitializeComponent();
			tv.SelectedNode = tv.Nodes[0];

			Font = SystemFonts.MenuFont;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			if (mvarSelectedObjects.Count == 0)
			{
				sc.Visible = false;
				sc.Enabled = false;
				pnlNoObjectsSelected.Enabled = true;
				pnlNoObjectsSelected.Visible = true;
			}
			else if (mvarSelectedObjects.Count == 1)
			{
				pnlNoObjectsSelected.Visible = false;
				pnlNoObjectsSelected.Enabled = false;
				sc.Enabled = true;
				sc.Visible = true;

				cmdGeneralInformationDataFormatChange.Enabled = false;
				IFileSystemObject fso = mvarSelectedObjects[0];
				if (fso is File)
				{
					File file = (fso as File);

					string fileTitle = System.IO.Path.GetFileName(file.Name);
					string fileLocation = System.IO.Path.GetDirectoryName(file.Name);

					txtFileName.Text = fileTitle;
					if (String.IsNullOrEmpty(txtGeneralInformationLocation.Text))
					{
						// only set the General - Location value if it has not already been set
						txtGeneralInformationLocation.Text = fileLocation;
					}

					chkGeneralAttributesArchive.Checked = ((file.Attributes & FileAttributes.Archive) == FileAttributes.Archive);
					chkGeneralAttributesDeleted.Checked = ((file.Attributes & FileAttributes.Deleted) == FileAttributes.Deleted);
					chkGeneralAttributesHidden.Checked = ((file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden);
					chkGeneralAttributesReadOnly.Checked = ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);

					Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor(file.GetData(), file.Name);
					Association[] assocs = Association.FromCriteria(new AssociationCriteria() { Accessor = ma });
					if (assocs.Length > 0)
					{
						DataFormatReference dfr = assocs[0].DataFormats[0];
						if (assocs[0].Filters.Count > 0)
						{
							txtGeneralInformationDataFormat.Text = assocs[0].Filters[0].Title;
						}
						else
						{
							txtGeneralInformationDataFormat.Text = dfr.Title;
						}

						ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(dfr);
						if (omrs.Length > 0)
						{
							txtGeneralInformationObjectModel.Text = omrs[0].Title;
							cmdGeneralInformationDataFormatChange.Enabled = true;
						}
					}
					txtGeneralInformationSize.Text = PrettyPrintFileSize(file.Size);
				}
				else if (fso is Folder)
				{

				}
			}
			else if (mvarSelectedObjects.Count > 1)
			{
				pnlNoObjectsSelected.Visible = false;
				pnlNoObjectsSelected.Enabled = false;
				sc.Enabled = true;
				sc.Visible = true;
			}
		}

		private string PrettyPrintFileSize(long size)
		{
			long KB = 1024;
			long MB = KB * 1024;
			long GB = MB * 1024;
			long TB = GB * 1024;

			StringBuilder sb = new StringBuilder();
			if (size >= TB)
			{
				sb.Append(Math.Round(((decimal)size / TB), 2).ToString() + " TB");
			}
			else if (size >= GB)
			{
				sb.Append(Math.Round(((decimal)size / TB), 2).ToString() + " GB");
			}
			else if (size >= MB)
			{
				sb.Append(Math.Round(((decimal)size / MB), 2).ToString() + " MB");
			}
			else if (size >= KB)
			{
				sb.Append(Math.Round(((decimal)size / KB), 2).ToString() + " KB");
			}
			else
			{
				sb.Append(Math.Round((decimal)size, 2).ToString() + " bytes");
			}
			return sb.ToString();
		}

		private void cmdGeneralInformationDataFormatChange_Click(object sender, EventArgs e)
		{

		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (mvarSelectedObjects.Count == 1)
			{
				IFileSystemObject fso = mvarSelectedObjects[0];
				if (fso is File)
				{
					File file = (fso as File);
					file.Name = txtFileName.Text;


					if (chkGeneralAttributesArchive.Checked)
					{
						file.Attributes |= FileAttributes.Archive;
					}
					else
					{
						file.Attributes &= ~FileAttributes.Archive;
					}

					if (chkGeneralAttributesDeleted.Checked)
					{
						file.Attributes |= FileAttributes.Deleted;
					}
					else
					{
						file.Attributes &= ~FileAttributes.Deleted;
					}

					if (chkGeneralAttributesHidden.Checked)
					{
						file.Attributes |= FileAttributes.Hidden;
					}
					else
					{
						file.Attributes &= ~FileAttributes.Hidden;
					}

					if (chkGeneralAttributesReadOnly.Checked)
					{
						file.Attributes |= FileAttributes.ReadOnly;
					}
					else
					{
						file.Attributes &= ~FileAttributes.ReadOnly;
					}
				}
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private IFileSystemObjectCollection mvarSelectedObjects = new IFileSystemObjectCollection();
		public IFileSystemObjectCollection SelectedObjects
		{
			get { return mvarSelectedObjects; }
			set
			{
				if (value != null) mvarSelectedObjects = value;
			}
		}

		private void cmdGeneralInformationLocationBrowse_Click(object sender, EventArgs e)
		{
			// Check if location is a directory so we don't inadvertently launch a program
			if (System.IO.Directory.Exists(txtGeneralInformationLocation.Text))
			{
				System.Diagnostics.Process.Start(txtGeneralInformationLocation.Text);
			}
		}

		private void cmdGeneralInformationLocationChange_Click(object sender, EventArgs e)
		{
			// Check if location is a directory so we don't inadvertently move a program
			if (System.IO.Directory.Exists(txtGeneralInformationLocation.Text))
			{
				string oldFilePath = txtGeneralInformationLocation.Text + System.IO.Path.DirectorySeparatorChar.ToString() + txtFileName.Text;

				// use AC NativeDialog for FolderBrowserDialog because it looks much sexier than the built-in WinForms one
				AwesomeControls.NativeDialogs.FolderBrowserDialog dlg = new AwesomeControls.NativeDialogs.FolderBrowserDialog();
				if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string newFilePath = dlg.SelectedPath + System.IO.Path.DirectorySeparatorChar.ToString() + txtFileName.Text;

					// move the specified file from the old location to the new location
					System.IO.File.Move(oldFilePath, newFilePath);
				}
			}
		}
	}
}
