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
					txtFileName.Text = file.Name;

					Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor(file.GetDataAsByteArray());
					DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(ma);
					if (dfrs.Length > 0)
					{
						DataFormatReference dfr = dfrs[0];
						txtGeneralInformationDataFormat.Text = dfr.Title;

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
	}
}
