//
//  BALDataFormat.cs - implements the Knowledge Adventure BAL archive format
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.KnowledgeAdventure.BAL
{
	/// <summary>
	/// Implements the Knowledge Adventure BAL archive format.
	/// </summary>
	public class BALDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			// Everything is loaded using this recursive function, so start it off by loading the root entry
			LoadEntry(reader, null, fsom);
		}

		private void LoadEntry(Reader reader, BALDirectoryEntry entry, IFileSystemContainer parent)
		{
			if (entry == null || (entry.Attributes & BALDirectoryEntryAttributes.Directory) == BALDirectoryEntryAttributes.Directory)
			{
				List<BALDirectoryEntry> entries = new List<BALDirectoryEntry>();

				int count = reader.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					BALDirectoryEntry entry1 = ReadDirectoryEntry(reader);
					entries.Add(entry1);
				}

				int nameTableLength = reader.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					BALDirectoryEntry entry1 = entries[i];
					entry1.Name = reader.ReadNullTerminatedString();
					entries[i] = entry1;
				}

				if (entry == null)
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].Name == "." || entries[i].Name == "..") continue;

						reader.Seek(entries[i].Offset, SeekOrigin.Begin);
						LoadEntry(reader, entries[i], parent);
					}
				}
				else
				{
					Folder folder = new Folder();
					folder.Name = entry.Name;
					for (int i = 0; i < count; i++)
					{
						if (entries[i].Name == "." || entries[i].Name == "..") continue;

						reader.Seek(entries[i].Offset, SeekOrigin.Begin);
						LoadEntry(reader, entries[i], folder);
					}
					parent.Folders.Add(folder);
				}
			}
			else
			{
				File file = new File();
				file.Name = entry.Name;
				file.Size = entry.Length;
				file.Source = new EmbeddedFileSource(reader, entry.Offset, entry.Length);
				parent.Files.Add(file);
			}
		}

		private BALDirectoryEntry ReadDirectoryEntry(Reader reader)
		{
			BALDirectoryEntry entry = new BALDirectoryEntry();
			entry.UnknownA1 = reader.ReadInt32();
			entry.Offset = reader.ReadInt32();
			entry.Length = reader.ReadInt32();
			entry.Attributes = (BALDirectoryEntryAttributes)reader.ReadInt32();
			entry.UnknownA3 = reader.ReadInt32();
			return entry;
		}
		private void WriteDirectoryEntry(Writer writer, BALDirectoryEntry entry)
		{
			writer.WriteInt32(entry.UnknownA1);
			writer.WriteInt32(entry.Offset);
			writer.WriteInt32(entry.Length);
			writer.WriteInt32((int)entry.Attributes);
			writer.WriteInt32(entry.UnknownA3);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			int totalCount = fsom.Folders.Count + fsom.Files.Count;
			writer.WriteInt32(totalCount);

			IFileSystemObject[] allObjects = fsom.GetAllObjects();
			IFileSystemObject[] allObjectsToplevel = fsom.GetAllObjects(null, System.IO.SearchOption.TopDirectoryOnly);
			IFileSystemObject[] allFolders = fsom.GetAllObjects(null, System.IO.SearchOption.AllDirectories, IFileSystemObjectType.Folder);

			int totalFolderCountIncludingRoot = allFolders.Length + 1;

			// each folder including the root has a 20-byte directory entry
			int totalSize = (20 * totalFolderCountIncludingRoot);

			// each actual folder including the root also has 4 bytes for the count of files and 4 bytes for the name table size
			totalSize += (8 * totalFolderCountIncludingRoot);

			for (int i = 0; i < allObjects.Length; i++)
			{
				// add the length of the file name including the null byte
				totalSize += allObjects[i].Name.Length + 1;

				// add the total size of the file data
				if (allObjects[i] is File)
				{
					totalSize += (int)((allObjects[i] as File).Size);
				}
				else if (allObjects[i] is Folder)
				{
					totalSize += (int)(allObjects[i] as Folder).GetSize();
				}
			}

			BALDirectoryEntry entryRoot = new BALDirectoryEntry();
			entryRoot.Attributes = BALDirectoryEntryAttributes.Directory;
			entryRoot.Offset = 0;
			entryRoot.Length = totalSize;
			WriteDirectoryEntry(writer, entryRoot);

			for (int i = 0; i < fsom.Folders.Count; i++)
			{
				BALDirectoryEntry entry = new BALDirectoryEntry();
				entry.Attributes = BALDirectoryEntryAttributes.Directory;
				entry.Offset = 0;
				entry.Length = 0;
				WriteDirectoryEntry(writer, entry);
			}

			// I have no idea what this does but it's at the end of the SOUND.BAL file
			writer.WriteInt32(-1172438784);
		}
	}
}
