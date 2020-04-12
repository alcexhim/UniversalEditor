//
//  PlaylistEditor.cs - provides a UWT-based Editor for PlaylistObjectModel
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
using UniversalEditor.ObjectModels.Multimedia.Playlist;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Playlist
{
	/// <summary>
	/// Provides a UWT-based Editor for <see cref="PlaylistObjectModel" />.
	/// </summary>
	public partial class PlaylistEditor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(PlaylistObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tmPlaylist.Rows.Clear();

			PlaylistObjectModel playlist = (ObjectModel as PlaylistObjectModel);
			if (playlist == null) return;

			for (int i = 0; i < playlist.Entries.Count; i++)
			{
				TreeModelRow row = new TreeModelRow();
				row.RowColumns.Add(new TreeModelRowColumn(tmPlaylist.Columns[0], playlist.Entries[i].FileName));
				row.SetExtraData<PlaylistEntry>("entry", playlist.Entries[i]);

				tmPlaylist.Rows.Add(row);
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			lvPlaylist.Model = tmPlaylist;
		}
	}
}
