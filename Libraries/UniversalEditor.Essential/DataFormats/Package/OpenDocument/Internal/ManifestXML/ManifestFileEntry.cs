//
//  ManifestFileEntry.cs
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
namespace UniversalEditor.DataFormats.Package.OpenDocument.Internal.ManifestXML
{
	public class ManifestFileEntry : ICloneable
	{
		public class ManifestFileEntryCollection
			: System.Collections.ObjectModel.Collection<ManifestFileEntry>
		{

		}

		public string FullPath { get; set; } = null;
		public string Version { get; set; } = null;
		public string MediaType { get; set; } = null;

		public ManifestFileEntry()
		{
		}
		public ManifestFileEntry(string fullPath, string mediaType, string version = null)
		{
			FullPath = fullPath;
			MediaType = mediaType;
			Version = version;
		}

		public object Clone()
		{
			ManifestFileEntry clone = new ManifestFileEntry();
			clone.FullPath = (FullPath?.Clone() as string);
			clone.Version = (Version?.Clone() as string);
			clone.MediaType = (MediaType?.Clone() as string);
			return clone;
		}
	}
}
