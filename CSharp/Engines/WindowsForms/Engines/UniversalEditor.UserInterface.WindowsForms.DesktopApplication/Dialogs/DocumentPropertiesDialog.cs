using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	public partial class DocumentPropertiesDialogBase : Form
	{
		public DocumentPropertiesDialogBase()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private DocumentPropertiesDialogMode mvarMode = DocumentPropertiesDialogMode.Open;
		public DocumentPropertiesDialogMode Mode { get { return mvarMode; } set { mvarMode = value; } }

		private ObjectModel mvarInitialObjectModel = null;
		
		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; mvarInitialObjectModel = value; } }

		private DataFormat mvarInitialDataFormat = null;

		private DataFormat mvarDataFormat = null;
		public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; mvarInitialDataFormat = value; } }

		private Accessor mvarInitialAccesor = null;

		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; mvarInitialAccesor = value; } }

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			RefreshButtons();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (mvarObjectModel == null)
			{
				MessageBox.Show("Please choose an object model for the document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (mvarDataFormat == null)
			{
				MessageBox.Show("Please choose an object model for the document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (mvarAccessor == null)
			{
				MessageBox.Show("Please choose an object model for the document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void cmdObjectModel_Click(object sender, EventArgs e)
		{
			GenericBrowserPopup<ObjectModel, ObjectModelReference> dlg = new GenericBrowserPopup<ObjectModel, ObjectModelReference>();
			dlg.SelectedObject = mvarObjectModel;

			Point loc = PointToScreen(cmdObjectModel.Location);
			dlg.Location = new Point(loc.X, loc.Y + cmdObjectModel.Height);
			dlg.Size = new Size(Width, 200);

			ObjectModelReference[] omrs = new ObjectModelReference[0];

			if (mvarMode == DocumentPropertiesDialogMode.Save)
			{
				// show all dataformats for the current object model
				omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(mvarDataFormat.MakeReference());
			}
			else if (mvarMode == DocumentPropertiesDialogMode.Open)
			{
				if (mvarAccessor != null)
				{
					// show all dataformats for the current accessor
					omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(mvarAccessor);
				}
				else
				{
					omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
				}
			}
			foreach (ObjectModelReference dfr in omrs)
			{
				dlg.AvailableObjects.Add(dfr);
			}
			dlg.SelectionChanged += dlgObjectModel_SelectionChanged;
			dlg.Show();
		}

		private void cmdDataFormat_Click(object sender, EventArgs e)
		{
			GenericBrowserPopup<DataFormat, DataFormatReference> dlg = new GenericBrowserPopup<DataFormat, DataFormatReference>();
			dlg.SelectedObject = mvarDataFormat;

			Point loc = PointToScreen(cmdDataFormat.Location);
			dlg.Location = new Point(loc.X, loc.Y + cmdDataFormat.Height);
			dlg.Size = new Size(Width, 200);

			DataFormatReference[] dfrs = new DataFormatReference[0];
			if (mvarMode == DocumentPropertiesDialogMode.Save)
			{
				// show all dataformats for the current object model
				Association[] assocs = Association.FromCriteria(new AssociationCriteria() { ObjectModel = mvarObjectModel.MakeReference() });
				List<DataFormatReference> dfrlist = new List<DataFormatReference>();
				foreach (Association assoc in assocs)
				{
					foreach(DataFormatReference dfr in assoc.DataFormats)
					{
						dlg.AvailableObjects.Add(dfr);
					}
				}
				dlg.SelectionChanged += dlgDataFormat_SelectionChanged;
				dlg.Show();
			}
			else if (mvarMode == DocumentPropertiesDialogMode.Open)
			{
				if (mvarAccessor != null)
				{
					// TODO: This desperately needs to be fixed; GetAvailableDataFormats should take
					// an accessor, not a file name, as parameter to be cross-accessor compatible!!!
					if (mvarAccessor is FileAccessor)
					{
						// show all dataformats for the current accessor
						dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(mvarAccessor);
					}
					else
					{
						dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();
					}
				}
				else if (mvarObjectModel != null)
				{
					dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(mvarObjectModel.MakeReference());
				}
				else
				{
					dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();
				}
			}
			foreach (DataFormatReference dfr in dfrs)
			{
				dlg.AvailableObjects.Add(dfr);
			}
			dlg.SelectionChanged += dlgDataFormat_SelectionChanged;
			dlg.Show();
		}

		private void dlgObjectModel_SelectionChanged(object sender, EventArgs e)
		{
			GenericBrowserPopup<ObjectModel, ObjectModelReference> dlg = (sender as GenericBrowserPopup<ObjectModel, ObjectModelReference>);
			mvarObjectModel = dlg.SelectedObject;
			RefreshButtons();
		}
		private void dlgDataFormat_SelectionChanged(object sender, EventArgs e)
		{
			GenericBrowserPopup<DataFormat, DataFormatReference> dlg = (sender as GenericBrowserPopup<DataFormat, DataFormatReference>);
			mvarDataFormat = dlg.SelectedObject;
			RefreshButtons();
		}
		private void dlgAccessor_SelectionChanged(object sender, EventArgs e)
		{
			GenericBrowserPopup<Accessor, AccessorReference> dlg = (sender as GenericBrowserPopup<Accessor, AccessorReference>);
			mvarAccessor = dlg.SelectedObject;
			dlg.AutoClose = false;

			switch (mvarMode)
			{
				case DocumentPropertiesDialogMode.Open:
				{
					if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref mvarAccessor, CustomOptionDialogType.Import))
					{
						return;
					}
					break;
				}
				case DocumentPropertiesDialogMode.Save:
				{
					if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref mvarAccessor, CustomOptionDialogType.Export))
					{
						return;
					}
					break;
				}
			}

			RefreshButtons();

			dlg.AutoClose = true;
		}

		private void cmdAccessor_Click(object sender, EventArgs e)
		{
			GenericBrowserPopup<Accessor, AccessorReference> dlg = new GenericBrowserPopup<Accessor, AccessorReference>();
			dlg.SelectedObject = mvarAccessor;

			Point loc = PointToScreen(cmdAccessor.Location);
			dlg.Location = new Point(loc.X, loc.Y + cmdAccessor.Height);
			dlg.Size = new Size(Width, 200);

			AccessorReference[] ars = UniversalEditor.Common.Reflection.GetAvailableAccessors();
			foreach (AccessorReference ar in ars)
			{
				if (!ar.Visible) continue;
				dlg.AvailableObjects.Add(ar);
			}
			dlg.SelectionChanged += dlgAccessor_SelectionChanged;
			dlg.Show();
		}

		private void ShowContextMenuBelow(ContextMenuStrip menu, Control parent)
		{
			Point pt = PointToScreen(parent.Location);
			menu.Show(pt.X, pt.Y + parent.Height);
		}

		private void mnuObjectModelSelect_Click(object sender, EventArgs e)
		{

		}

		private void mnuObjectModelClear_Click(object sender, EventArgs e)
		{
			mvarObjectModel = null;
			txtObjectModel.Text = String.Empty;
			RefreshButtons();
		}

		private void RefreshButtons()
		{
			if (mvarMode == DocumentPropertiesDialogMode.Save)
			{
				if (mvarAccessor != null)
				{
					string filename = (mvarAccessor as FileAccessor).FileName;
					if (!System.IO.File.Exists(filename))
					{
						if (mvarInitialAccesor != null)
						{
							filename = (mvarInitialAccesor as FileAccessor).FileName;
						}
					}
				}
			}
			else if (mvarMode == DocumentPropertiesDialogMode.Open)
			{
				if (mvarAccessor is FileAccessor)
				{
					Association[] assocs = Association.FromCriteria(new AssociationCriteria() { Accessor = mvarAccessor });
					List<DataFormatReference> dfrs = new List<DataFormatReference>();
					foreach (Association assoc in assocs)
					{
						foreach (DataFormatReference dfr in assoc.DataFormats)
						{
							dfrs.Add(dfr);
						}
					}
					if (mvarDataFormat == null)
					{
						if (dfrs.Count > 0)
						{
							mvarDataFormat = dfrs[0].Create();
						}
					}
					else
					{
					}
				}
				if (mvarObjectModel == null)
				{
					if (mvarDataFormat != null)
					{
						ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(mvarDataFormat.MakeReference());
						if (omrs.Length > 0)
						{
							mvarObjectModel = omrs[0].Create();
						}
					}
					else if (mvarAccessor is FileAccessor)
					{
						string filename = (mvarAccessor as FileAccessor).FileName;
						ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(mvarAccessor);
						if (omrs.Length == 1)
						{
							mvarObjectModel = omrs[0].Create();
						}
					}
				}
			}

			if (mvarAccessor != null)
			{
				txtAccessor.Text = mvarAccessor.ToString();
			}
			if (mvarDataFormat != null)
			{
				DataFormatReference dfr = mvarDataFormat.MakeReference();
				txtDataFormat.Text = DataFormatReferenceToString(dfr);
			}
			else
			{
				txtDataFormat.Text = String.Empty;
			}
			if (mvarObjectModel != null)
			{
				ObjectModelReference omr = mvarObjectModel.MakeReference();
				txtObjectModel.Text = omr.Title;
			}
			else
			{
				txtObjectModel.Text = String.Empty;
			}

			cmdOK.Enabled = (mvarObjectModel != null && mvarDataFormat != null && mvarAccessor != null);
		}

		void tsmiObjectModel_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			mvarObjectModel = (tsmi.Tag as ObjectModelReference).Create();
			RefreshButtons();
		}

		private void tsmi_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			mvarDataFormat = (tsmi.Tag as DataFormatReference).Create();
			RefreshButtons();
		}

		private string DataFormatReferenceToString(DataFormatReference dfr)
		{
			Association[] assocs = Association.FromCriteria(new AssociationCriteria() { DataFormat = dfr });
			return dfr.Title + " (" + DataFormatFilterCollectionToString(assocs) + ")";
		}

		private string DataFormatFilterCollectionToString(Association[] associations)
		{
			StringBuilder sb = new StringBuilder();
			foreach (Association assoc in associations)
			{
				foreach (DataFormatFilter filter in assoc.Filters)
				{
					sb.Append(StringArrayToString(filter.FileNameFilters));
					if (assoc.Filters.IndexOf(filter) < assoc.Filters.Count - 1)
					{
						sb.Append("; ");
					}
				}
			}
			return sb.ToString();
		}

		private string StringArrayToString(System.Collections.Specialized.StringCollection collection)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in collection)
			{
				sb.Append(s);
				if (collection.IndexOf(s) < collection.Count - 1)
				{
					sb.Append(", ");
				}
			}
			return sb.ToString();
		}

		private void mnuDataFormatSelect_Click(object sender, EventArgs e)
		{

		}

		private void mnuDataFormatClear_Click(object sender, EventArgs e)
		{
			mvarDataFormat = null;
			RefreshButtons();
		}

		private void mnuAccessorLocalFile_Click(object sender, EventArgs e)
		{
			switch (mvarMode)
			{
				case DocumentPropertiesDialogMode.Open:
				{
					OpenFileDialog ofd = new OpenFileDialog();
					if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						mvarAccessor = new FileAccessor(ofd.FileName);
						txtAccessor.Text = "File: " + ofd.FileName;
					}
					break;
				}
				case DocumentPropertiesDialogMode.Save:
				{
					SaveFileDialog sfd = new SaveFileDialog();
					if (mvarDataFormat != null)
					{
						Association[] assocs = Association.FromCriteria(new AssociationCriteria() { DataFormat = mvarDataFormat.MakeReference() });
						sfd.Filter = UniversalEditor.UserInterface.WindowsForms.CommonDialog.GetCommonDialogFilter(assocs);
					}
					if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						mvarAccessor = new FileAccessor(sfd.FileName, true, true);
						txtAccessor.Text = "File: " + sfd.FileName;
					}
					break;
				}
			}
			RefreshButtons();
		}
	}

	public enum DocumentPropertiesDialogMode
	{
		Open,
		Save
	}
	public class DocumentPropertiesDialog
	{
		private DocumentPropertiesDialogBase dlg = null;

		private DocumentPropertiesDialogMode mvarMode = DocumentPropertiesDialogMode.Open;
		public DocumentPropertiesDialogMode Mode { get { return mvarMode; } set { mvarMode = value; } }

		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }

		private DataFormat mvarDataFormat = null;
		public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }

		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		public DialogResult ShowDialog()
		{
			if (dlg == null) dlg = new DocumentPropertiesDialogBase();
			if (dlg.IsDisposed) dlg = new DocumentPropertiesDialogBase();

			dlg.Mode = mvarMode;
			dlg.ObjectModel = mvarObjectModel;
			dlg.DataFormat = mvarDataFormat;
			dlg.Accessor = mvarAccessor;
			dlg.ShowInTaskbar = false;

			DialogResult result = dlg.ShowDialog();

			if (result == DialogResult.OK)
			{
				mvarObjectModel = dlg.ObjectModel;
				mvarDataFormat = dlg.DataFormat;
				mvarAccessor = dlg.Accessor;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
