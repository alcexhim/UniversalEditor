//
//  DocumentPropertiesDialog.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using System.Collections.Generic;
using System.Text;
using UniversalEditor.Accessors;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Drawing;

using MBS.Framework.Drawing;
using System.ComponentModel;
using MBS.Framework.UserInterface.Dialogs;

namespace UniversalEditor.UserInterface.Dialogs
{
	[ContainerLayout("~/Dialogs/DocumentPropertiesDialog.glade", "GtkDialog")]
	public class DocumentPropertiesDialog : CustomDialog
	{
		// **********************************************************
		// THESE FIELDS ARE FILLED IN BY UWT CONTAINER LAYOUT LOADER
		// DO NOT REMOVE!!!
		private Button cmdObjectModel = null;
		private Button cmdDataFormat = null;
		private Button cmdAccessor = null;
		private TextBox txtObjectModel = null;
		private TextBox txtDataFormat = null;
		private TextBox txtAccessor = null;
		private Button cmdDataFormatOptions = null;
		private Button cmdAccessorOptions = null;
		// **********************************************************

		protected override void OnCreating(EventArgs e)
		{
			base.OnCreating(e);

			this.Buttons[0].ResponseValue = (int)DialogResult.OK;
			this.Buttons[1].ResponseValue = (int)DialogResult.Cancel;

			this.Buttons[0].Enabled = false;

			switch (Mode)
			{
				case DocumentPropertiesDialogMode.Open:
				{
					this.Text = "Open Document";
					this.Buttons[0].StockType = StockType.Open;
					break;
				}
				case DocumentPropertiesDialogMode.Save:
				{
					this.Text = "Save Document";
					this.Buttons[0].StockType = StockType.Save;
					break;
				}
			}

			this.txtObjectModel.Text = ObjectModel?.MakeReference().Title;
			this.txtDataFormat.Text = DataFormat?.MakeReference().Title;
			this.txtAccessor.Text = Accessor?.GetFileName();
		}
		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			RefreshButtons();
		}

		public DocumentPropertiesDialogMode Mode { get; set; } = DocumentPropertiesDialogMode.Open;

		private ObjectModel mvarInitialObjectModel = null;

		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; mvarInitialObjectModel = value; } }

		private DataFormat mvarInitialDataFormat = null;

		private DataFormat mvarDataFormat = null;
		public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; mvarInitialDataFormat = value; } }

		private Accessor mvarInitialAccesor = null;

		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; mvarInitialAccesor = value; } }

		[EventHandler(nameof(cmdObjectModel), "Click")]
		private void cmdObjectModel_Click(object sender, EventArgs e)
		{
			GenericBrowserPopup<ObjectModel, ObjectModelReference> popup = new GenericBrowserPopup<ObjectModel, ObjectModelReference>();
			popup.ResetList += delegate(object s, EventArgs ee)
			{
				popup.AvailableObjects.Clear();
				ObjectModelReference[] _omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
				for (int i = 0; i < _omrs.Length; i++)
				{
					popup.AvailableObjects.Add(_omrs[i]);
				}
			};
			popup.SelectedObject = ObjectModel;

			Vector2D loc = ClientToScreenCoordinates(cmdObjectModel.Location);
			popup.Location = new Vector2D(loc.X, loc.Y + cmdObjectModel.Size.Height);
			popup.Size = new Dimension2D(this.Size.Width, 200);

			ObjectModelReference[] omrs = new ObjectModelReference[0];

			if (Mode == DocumentPropertiesDialogMode.Save)
			{
				// show all dataformats for the current object model
				if (DataFormat == null)
				{
					omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
				}
				else
				{
					omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(DataFormat.MakeReference());
				}
			}
			else if (Mode == DocumentPropertiesDialogMode.Open)
			{
				if (Accessor != null)
				{
					// show all dataformats for the current accessor
					omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(Accessor);
				}
				else
				{
					omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
				}
			}
			foreach (ObjectModelReference omr in omrs)
			{
				popup.AvailableObjects.Add(omr);
			}
			popup.SelectionChanged += popupObjectModel_SelectionChanged;

			popup.ShowDialog();
		}

		private void RefreshButtons()
		{
			if (Accessor != null)
			{
				txtAccessor.Text = Accessor.ToString();
			}
			else
			{
				txtAccessor.Text = String.Empty;
			}
			if (DataFormat != null)
			{
				DataFormatReference dfr = DataFormat.MakeReference();
				txtDataFormat.Text = DataFormat?.MakeReference().Title;
			}
			else
			{
				txtDataFormat.Text = String.Empty;
			}
			if (ObjectModel != null)
			{
				ObjectModelReference omr = ObjectModel.MakeReference();
				txtObjectModel.Text = omr.Title;
			}
			else
			{
				txtObjectModel.Text = String.Empty;
			}

			Buttons[0].Enabled = (Accessor != null && (Mode == DocumentPropertiesDialogMode.Open || (Mode == DocumentPropertiesDialogMode.Save && ObjectModel != null && DataFormat != null)));
			
			switch (Mode)
			{
				case DocumentPropertiesDialogMode.Open:
				{
					cmdDataFormatOptions.Enabled = (DataFormat?.MakeReference().ImportOptions.Count > 0);
					cmdAccessorOptions.Enabled = (Accessor?.MakeReference().ImportOptions.Count > 0);
					break;
				}
				case DocumentPropertiesDialogMode.Save:
				{
					cmdDataFormatOptions.Enabled = (DataFormat?.MakeReference().ExportOptions.Count > 0);
					cmdAccessorOptions.Enabled = (Accessor?.MakeReference().ExportOptions.Count > 0);
					break;
				}
			}
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


		private void popupObjectModel_SelectionChanged(object sender, EventArgs e)
		{
			GenericBrowserPopup<ObjectModel, ObjectModelReference> dlg = (sender as GenericBrowserPopup<ObjectModel, ObjectModelReference>);
			ObjectModel = dlg.SelectedObject;
			RefreshButtons();
		}

		[EventHandler(nameof(cmdDataFormat), "Click")]
		private void cmdDataFormat_Click(object sender, EventArgs e)
		{
			GenericBrowserPopup<DataFormat, DataFormatReference> popup = new GenericBrowserPopup<DataFormat, DataFormatReference>();
			popup.ResetList += delegate(object s, EventArgs ee)
			{
				popup.AvailableObjects.Clear();
				DataFormatReference[] _dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();
				for (int i = 0; i < _dfrs.Length; i++)
				{
					popup.AvailableObjects.Add(_dfrs[i]);
				}
			};
			popup.SelectedObject = mvarDataFormat;

			Vector2D loc = ClientToScreenCoordinates(cmdDataFormat.Location);
			popup.Location = new Vector2D(loc.X, loc.Y + cmdDataFormat.Size.Height);
			popup.Size = new Dimension2D(this.Size.Width, 200);

			DataFormatReference[] dfrs = new DataFormatReference[0];
			if (Mode == DocumentPropertiesDialogMode.Save)
			{
				// show all dataformats for the current object model
				Association[] assocs = Association.FromCriteria(new AssociationCriteria() { ObjectModel = mvarObjectModel?.MakeReference() });
				List<DataFormatReference> dfrlist = new List<DataFormatReference>();
				foreach (Association assoc in assocs)
				{
					foreach (DataFormatReference dfr in assoc.DataFormats)
					{
						dfrlist.Add(dfr);
					}
				}
				dfrs = dfrlist.ToArray();
			}
			else if (Mode == DocumentPropertiesDialogMode.Open)
			{
				if (mvarAccessor != null)
				{
					/*
					// TODO: This desperately needs to be fixed; GetAvailableDataFormats should take
					// an accessor, not a file name, as parameter to be cross-accessor compatible!!!

					// so... have we fixed this now?

					if (mvarAccessor is FileAccessor)
					{
					*/
					// show all dataformats for the current accessor
					dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(mvarAccessor);
					/*
					}
					else
					{
						dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();
					}
					*/
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
				popup.AvailableObjects.Add(dfr);
			}
			popup.SelectionChanged += dlgDataFormat_SelectionChanged;
			popup.ShowDialog();
		}

		[EventHandler(nameof(cmdAccessor), "Click")]
		private void cmdAccessor_Click(object sender, EventArgs e)
		{
			GenericBrowserPopup<Accessor, AccessorReference> dlg = new GenericBrowserPopup<Accessor, AccessorReference>();
			dlg.SelectedObject = mvarAccessor;

			Vector2D loc = ClientToScreenCoordinates(cmdAccessor.Location);
			dlg.Location = new Vector2D(loc.X, loc.Y + cmdAccessor.Size.Height);
			dlg.Size = new Dimension2D(this.Size.Width, 200);

			AccessorReference[] ars = UniversalEditor.Common.Reflection.GetAvailableAccessors();
			foreach (AccessorReference ar in ars)
			{
				if (!ar.Visible) continue;
				dlg.AvailableObjects.Add(ar);
			}
			dlg.SelectionChanged += dlgAccessor_SelectionChanged;
			dlg.ShowDialog();
		}

		private void dlgDataFormat_SelectionChanged(object sender, EventArgs e)
		{
			GenericBrowserPopup<DataFormat, DataFormatReference> dlg = (sender as GenericBrowserPopup<DataFormat, DataFormatReference>);

			DataFormat df = dlg.SelectedObject;
			if (df != null)
			{
				switch (Mode)
				{
					case DocumentPropertiesDialogMode.Open:
					{
						if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref df, CustomOptionDialogType.Import))
						{
							return;
						}
						cmdDataFormatOptions.Enabled = (df.MakeReference().ImportOptions.Count > 0);
						break;
					}
					case DocumentPropertiesDialogMode.Save:
					{
						if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref df, CustomOptionDialogType.Export))
						{
							return;
						}
						cmdDataFormatOptions.Enabled = (df.MakeReference().ExportOptions.Count > 0);
						break;
					}
				}
			}
			DataFormat = df;

			RefreshButtons();
		}
		private void dlgAccessor_SelectionChanged(object sender, EventArgs e)
		{
			GenericBrowserPopup<Accessor, AccessorReference> dlg = (sender as GenericBrowserPopup<Accessor, AccessorReference>);
			
			Accessor acc = dlg.SelectedObject;
			dlg.AutoClose = false;

			if (DataFormat != null)
			{
				if (acc is FileAccessor)
				{
					AccessorReference accref = acc.MakeReference();
					string[] details = DataFormat.MakeReference().GetDetails();
					if (details.Length > 1)
					{
						(accref.ImportOptions[0] as CustomOptionFile).Filter = details[0] + "|" + details[1];
						(accref.ExportOptions[0] as CustomOptionFile).Filter = details[0] + "|" + details[1];
					}
				}
			}
			else
			{
				if (acc is FileAccessor)
				{
					AccessorReference accref = acc.MakeReference();
					(accref.ImportOptions[0] as CustomOptionFile).Filter = "All files|*";
					(accref.ExportOptions[0] as CustomOptionFile).Filter = "All files|*";
				}
			}

			switch (Mode)
			{
				case DocumentPropertiesDialogMode.Open:
				{
					if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref acc, CustomOptionDialogType.Import))
					{
						return;
					}
					cmdAccessorOptions.Enabled = (acc.MakeReference().ImportOptions.Count > 0);
					break;
				}
				case DocumentPropertiesDialogMode.Save:
				{
					if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref acc, CustomOptionDialogType.Export))
					{
						return;
					}
					cmdAccessorOptions.Enabled = (acc.MakeReference().ExportOptions.Count > 0);
					break;
				}
			}

			Accessor = acc;

			DataFormat df = null;
			// pro feature: if we find a better OM/DF, maybe ask the user "do you wish to change object model to ___ ?" before doing so
			if (Mode == DocumentPropertiesDialogMode.Open)
			{
				if (Accessor is FileAccessor)
				{
					if (!System.IO.File.Exists(Accessor.GetFileName()))
					{
						// prevents the FileAccessor from "helpfully" creating an empty file with the given name if it doesn't exist
						MessageDialog.ShowDialog("The file you selected does not exist.", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
						return;
					}

					Association[] assocs = Association.FromCriteria(new AssociationCriteria() { Accessor = Accessor });
					List<DataFormatReference> dfrs = new List<DataFormatReference>();
					foreach (Association assoc in assocs)
					{
						foreach (DataFormatReference dfr in assoc.DataFormats)
						{
							dfrs.Add(dfr);
						}
					}
					if (DataFormat == null)
					{
						if (dfrs.Count > 0)
						{
							df = dfrs[0].Create();
						}
					}
					else
					{
					}
				}
				if (ObjectModel == null)
				{
					if (df != null)
					{
						switch (Mode)
						{
							case DocumentPropertiesDialogMode.Open:
							{
								if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref df, CustomOptionDialogType.Import))
								{
									return;
								}
								break;
							}
							case DocumentPropertiesDialogMode.Save:
							{
								if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref df, CustomOptionDialogType.Export))
								{
									return;
								}
								break;
							}
						}
						DataFormat = df;

						ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(DataFormat.MakeReference());
						if (omrs.Length > 0)
						{
							ObjectModel = omrs[0].Create();
						}
					}
					else if (Accessor is FileAccessor)
					{
						string filename = (Accessor as FileAccessor).FileName;
						if (String.IsNullOrEmpty(filename)) return;

						ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(Accessor);
						if (omrs.Length == 1)
						{
							ObjectModel = omrs[0].Create();
						}
					}
				}
			}

			RefreshButtons();

			dlg.AutoClose = true;
		}

		[EventHandler(nameof(cmdDataFormatOptions), "Click")]
		private void cmdDataFormatOptions_Click(object sender, EventArgs e)
		{
			DataFormat df = DataFormat;
			if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref df, Mode == DocumentPropertiesDialogMode.Open ? CustomOptionDialogType.Import : CustomOptionDialogType.Export))
			{
				return;
			}
			DataFormat = df;
		}

		[EventHandler(nameof(cmdAccessorOptions), "Click")]
		private void cmdAccessorOptions_Click(object sender, EventArgs e)
		{
			Accessor acc = Accessor;
			if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref acc, Mode == DocumentPropertiesDialogMode.Open ? CustomOptionDialogType.Import : CustomOptionDialogType.Export))
			{
				return;
			}
			Accessor = acc;
			this.txtAccessor.Text = Accessor?.GetFileName();
		}
	}
}
