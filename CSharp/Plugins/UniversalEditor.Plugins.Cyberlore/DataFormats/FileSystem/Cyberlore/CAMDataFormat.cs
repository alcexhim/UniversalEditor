using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.Cyberlore
{
	public class CAMDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://forum.xentax.com/viewtopic.php?p=9801#9801");
			}
			return _dfr;
		}

		private Version mvarFormatVersion = new Version(2, 1);
		public Version FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(8);
			if (signature != "CYLBPC  ") throw new InvalidDataFormatException("File does not start with 'CYLBPC  '");

			short versionMajor = reader.ReadInt16();
			short versionMinor = reader.ReadInt16();
			mvarFormatVersion = new Version(versionMajor, versionMinor);

			uint fileTypeCount = reader.ReadUInt32();
			for (uint i = 0; i < fileTypeCount; i++)
			{
				// for each file type

				// Size of the directory for this file type [+8] (including these 4 fields)
				uint directorySize = reader.ReadUInt32();

				// File Type Description (WAVE)
				string fileTypeID = reader.ReadFixedLengthString(4);

				// Size of each file entry (28) (OLD!)
				uint fileEntrySize = reader.ReadUInt32();

				// Number Of Files of this type
				uint fileCount = reader.ReadUInt32();

				uint unknown1 = reader.ReadUInt32();

				for (uint j = 0; j < fileCount; j++)
				{
					// for each file of this type
					File file = new File();

					// Filename? File ID?
					string fileID = reader.ReadFixedLengthString(4);

					// File Offset
					uint offset = reader.ReadUInt32();

					// File Size
					uint length = reader.ReadUInt32();

					file.Name = fileID + "." + fileTypeID;
					file.Size = length;

					file.Source = new EmbeddedFileSource(reader, offset, length);

					uint unknown2 = reader.ReadUInt32();

					fsom.Files.Add(file);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
