//
//  PlaylistObjectModel.cs - provides an ObjectModel for manipulating a playlist of media files
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

using System;
using System.Collections.Generic;
namespace UniversalEditor.ObjectModels.Multimedia.Playlist
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating a playlist of media files.
	/// </summary>
	public class PlaylistObjectModel : ObjectModel
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Title = "Multimedia playlist";
			omr.Path = new string[] { "Multimedia", "Playlist" };
			return omr;
		}

		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets a dictionary of key-value pairs representing custom metadata tags associated with this <see cref="PlaylistObjectModel" />.
		/// </summary>
		/// <value>The custom metadata tags associated with this <see cref="PlaylistObjectModel" />.</value>
		public Dictionary<string, string> CustomInformation { get; } = new Dictionary<string, string>();
		/// <summary>
		/// Gets or sets the title of this <see cref="PlaylistObjectModel" />.
		/// </summary>
		/// <value>The title of this <see cref="PlaylistObjectModel" />.</value>
		public string Title { get; set; } = string.Empty;
		/// <summary>
		/// Gets or sets the author of this <see cref="PlaylistObjectModel" />.
		/// </summary>
		/// <value>The author of this <see cref="PlaylistObjectModel" />.</value>
		public string Author { get; set; } = string.Empty;
		/// <summary>
		/// Gets or sets the abstract.
		/// </summary>
		/// <value>The abstract.</value>
		public string Abstract { get; set; } = string.Empty;
		/// <summary>
		/// Gets or sets the value for the copyright field.
		/// </summary>
		/// <value>The copyright notice included with this <see cref="PlaylistObjectModel" />.</value>
		public string Copyright { get; set; } = string.Empty;
		/// <summary>
		/// Gets a collection of <see cref="PlaylistEntry" /> instances representing the entries in this <see cref="PlaylistObjectModel" />.
		/// </summary>
		/// <value>The entries in this playlist.</value>
		public PlaylistEntry.PlaylistEntryCollection Entries { get; } = new PlaylistEntry.PlaylistEntryCollection();

		public override void Clear()
		{
			this.Abstract = string.Empty;
			this.Author = string.Empty;
			this.Copyright = string.Empty;
			this.CustomInformation.Clear();
			this.Entries.Clear();
			this.ID = Guid.Empty;
			this.Title = string.Empty;
		}
		public override void CopyTo(ObjectModel destination)
		{
			PlaylistObjectModel clone = destination as PlaylistObjectModel;
			clone.Abstract = (this.Abstract.Clone() as string);
			clone.Author = (this.Author.Clone() as string);
			clone.Copyright = (this.Copyright.Clone() as string);
			foreach (KeyValuePair<string, string> kvp in this.CustomInformation)
			{
				clone.CustomInformation.Add(kvp.Key, kvp.Value);
			}
			foreach (PlaylistEntry entry in this.Entries)
			{
				clone.Entries.Add(entry.Clone() as PlaylistEntry);
			}
			clone.ID = this.ID;
			clone.Title = this.Title;
		}
	}
}
