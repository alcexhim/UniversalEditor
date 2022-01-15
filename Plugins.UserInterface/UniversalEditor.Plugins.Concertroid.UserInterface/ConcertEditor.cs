//
//  MyClass.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.ObjectModels.Concertroid;
using UniversalEditor.ObjectModels.Concertroid.Concert;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Concertroid.UserInterface
{
	[ContainerLayout("~/Extensions/Concertroid/Editors/ConcertEditor.glade")]
	public class ConcertEditor : Editor
	{
		private ListViewControl tvSongs;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(ConcertObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated)
				return;

			ConcertObjectModel concert = (ObjectModel as ConcertObjectModel);
			if (concert == null) return;

			foreach (Performance item in concert.Performances)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvSongs.Model.Columns[0], item.Title)
				});
				foreach (Performer item2 in item.Performers)
				{
					row.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tvSongs.Model.Columns[1], item2.Character.FullName),
						new TreeModelRowColumn(tvSongs.Model.Columns[2], item2.Costume.Title),
						new TreeModelRowColumn(tvSongs.Model.Columns[3], item2.Animation.FileName)
					}));
				}
				row.SetExtraData<Performance>("item", item);
				tvSongs.Model.Rows.Add(row);
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			OnObjectModelChanged(e);
		}
	}
}
