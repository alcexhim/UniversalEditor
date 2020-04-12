//
//  HTMLPlaylistDataFormat.cs - provides a DataFormat for manipulating playlists in VLC-generated HTML format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

using System.Collections.Generic;

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating playlists in VLC-generated HTML format.
	/// </summary>
	public class HTMLPlaylistDataFormat : XMLDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		protected override bool IsObjectModelSupported(UniversalEditor.ObjectModel omb)
		{
			MarkupObjectModel mom = (omb as MarkupObjectModel);
			if (mom == null) return false;

			return (mom.Elements.Count == 1 && mom.Elements[0].FullName == "html");
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			MarkupObjectModel mom = objectModels.Pop() as MarkupObjectModel;
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PlaylistObjectModel pom = (objectModels.Pop() as PlaylistObjectModel);
			if (pom == null) throw new ObjectModelNotSupportedException();

			MarkupObjectModel mom = new MarkupObjectModel();
			objectModels.Push(mom);
		}
	}
}
