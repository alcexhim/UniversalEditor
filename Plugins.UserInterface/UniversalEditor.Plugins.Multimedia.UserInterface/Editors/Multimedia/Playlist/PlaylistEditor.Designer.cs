//
//  PlaylistEditor.Designer.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Playlist
{
	[ContainerLayout("~/Editors/Multimedia/Playlist/PlaylistEditor.glade", "GtkWindow")]
	partial class PlaylistEditor : Editor
	{
		private ListView lvPlaylist = null;
		private DefaultTreeModel tmPlaylist = null;

		protected override void OnCreating(EventArgs e)
		{
			base.OnCreating(e);

			lvPlaylist.Model = tmPlaylist;

			// fuckkkk
			for (int i = 0; i < tmPlaylist.Columns.Count; i++)
			{
				lvPlaylist.Columns[i].Column = tmPlaylist.Columns[i];
			}
		}
	}
}
