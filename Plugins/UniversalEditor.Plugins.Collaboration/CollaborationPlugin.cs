//
//  MyClass.cs
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
using System.Collections.Generic;
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Collaboration
{
	public class CollaborationPlugin : UserInterfacePlugin
	{
		private Dictionary<Document, CollaborationSettings> _CollaborationSettings = new Dictionary<Document, CollaborationSettings>();
		public CollaborationSettings GetCollaborationSettings(Document document = null)
		{
			if (document == null)
			{
				document = HostApplication.CurrentWindow?.GetCurrentEditor()?.Document;
			}

			if (!_CollaborationSettings.ContainsKey(document))
			{
				_CollaborationSettings[document] = new CollaborationSettings();
			}
			return _CollaborationSettings[document];
		}

		public CollaborationPlugin()
		{
			ID = new Guid("{981d54ae-dee6-47c7-bea6-20890b3baa23}");
			Context = new UIContext(ID, "Collaboration Plugin");
			Context.AttachCommandEventHandler("Collaboration_Tracking_Track", delegate (object sender, EventArgs e)
			{
				Editor editor = HostApplication.CurrentWindow.GetCurrentEditor();
				Document d = editor?.Document;
				if (d != null)
				{
					CollaborationSettings collab = GetCollaborationSettings(d);
					collab.TrackChangesEnabled = !collab.TrackChangesEnabled;

					MessageDialog.ShowDialog(String.Format("Track Changes enabled / disabled for document {0}: {1}", d.Title, collab.TrackChangesEnabled), "Information", MessageDialogButtons.OK, MessageDialogIcon.Information);
				}
			});
		}
	}
}
