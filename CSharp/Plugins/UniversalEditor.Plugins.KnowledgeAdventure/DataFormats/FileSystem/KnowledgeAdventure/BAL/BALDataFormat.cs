using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.KnowledgeAdventure.BAL
{
    public class BALDataFormat : DataFormat
    {
		private static DataFormatReference _dfr = null;
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
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", entry.Offset);
				file.Properties.Add("length", entry.Length);
				file.DataRequest += file_DataRequest;
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

		void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			int offset = (int)file.Properties["offset"];
			int length = (int)file.Properties["length"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] data = reader.ReadBytes(length);
			e.Data = data;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
		}
	}
}
