//
//  UniversalEditorFileBrowserControl.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Layouts;
using UniversalWidgetToolkit.Controls.FileBrowser;
using UniversalWidgetToolkit.Controls;
using System.Collections.Generic;

namespace UniversalEditor.UserInterface.Controls
{
	public class UniversalEditorFileBrowserControl : Container
	{
		private FileBrowserControl _Browser = null;
		private Container _Table = null;

		private GenericBrowserButton<ObjectModel, ObjectModelReference> cboObjectModel = null;
		private GenericBrowserButton<DataFormat, DataFormatReference> cboDataFormat = null;

		public FileBrowserMode Mode { get { return _Browser.Mode; } set { _Browser.Mode = value; } }

		public ObjectModel ObjectModel { get; set; } = null;
		public DataFormat DataFormat { get; set; } = null;

		public UniversalEditorFileBrowserControl ()
		{
			this.Layout = new BoxLayout (Orientation.Vertical);

			_Browser = new FileBrowserControl ();
			this.Controls.Add (_Browser, new BoxLayout.Constraints (true, true));

			cboObjectModel = new GenericBrowserButton<ObjectModel, ObjectModelReference> ();
			// cboObjectModel.SelectedObject = ObjectModel;

			cboObjectModel.Text = "Object _model: (not selected)";

			ObjectModelReference[] omrs = new ObjectModelReference[0];

			if (Mode == FileBrowserMode.Save)
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
			else if (Mode == FileBrowserMode.Open)
			{
				/*
				if (Accessor != null)
				{
					// show all dataformats for the current accessor
					omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(Accessor);
				}
				else*/
				{
					omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
				}
			}
			foreach (ObjectModelReference omr in omrs)
			{
				cboObjectModel.AvailableObjects.Add(omr);
			}

			cboDataFormat = new GenericBrowserButton<DataFormat, DataFormatReference> ();
			cboDataFormat.Text = "Data _format: (not selected)";

			DataFormatReference[] dfrs = new DataFormatReference[0];
			if (Mode == FileBrowserMode.Save)
			{
				// show all dataformats for the current object model
				Association[] assocs = Association.FromCriteria(new AssociationCriteria() { ObjectModel = ObjectModel.MakeReference() });
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
			else if (Mode == FileBrowserMode.Open)
			{
				if (false) // (Accessor != null)
				{
					/*
					// TODO: This desperately needs to be fixed; GetAvailableDataFormats should take
					// an accessor, not a file name, as parameter to be cross-accessor compatible!!!

					// so... have we fixed this now?

					if (mvarAccessor is FileAccessor)
					{
						*/
						// show all dataformats for the current accessor
						dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(/*Accessor*/);
						/*
					}
					else
					{
						dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();
					}
					*/
					}
					else if (ObjectModel != null)
					{
						dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(ObjectModel.MakeReference());
					}
					else
					{
						dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();
					}
				}
				foreach (DataFormatReference dfr in dfrs)
				{
					cboDataFormat.AvailableObjects.Add(dfr);
				}


			_Table = new Container ();
			_Table.Layout = new BoxLayout (Orientation.Horizontal);
			_Table.Controls.Add (cboObjectModel, new BoxLayout.Constraints (true, true));
			_Table.Controls.Add (cboDataFormat, new BoxLayout.Constraints (true, true));
			this.Controls.Add (_Table, new BoxLayout.Constraints (false, false));
		}
	}
}

