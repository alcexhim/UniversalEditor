using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HostileWaters
{
	public class DATDataFormat : DataFormat
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

		private string mvarMBXFileName = null;
		public string MBXFileName { get { return mvarMBXFileName; } set { mvarMBXFileName = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			if (!(base.Accessor is FileAccessor) && mvarMBXFileName == null) throw new InvalidOperationException("Requires a file reference or known MBX file path");

			IO.Reader br = base.Accessor.Reader;
			uint fileCount = br.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = br.ReadFixedLengthString(12);
				if (String.IsNullOrEmpty(fileName)) fileName = (i + 1).ToString().PadLeft(8, '0');

				uint fileLength = br.ReadUInt32();
				uint fileOffset = br.ReadUInt32();

				if ((fileOffset + fileLength) >= br.Accessor.Length) throw new InvalidDataFormatException("File offset + length is too large");

				File file = new File();
				file.Name = fileName;
				file.Size = fileLength;
				file.Source = new MBXFileSource(mvarMBXFileName, fileOffset, fileLength);

				fsom.Files.Add(file);
			}
		}

		private Writer mbxWriter = null;

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteUInt32((uint)fsom.Files.Count);
			uint offset = 0;
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 12);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteUInt32(offset);
				offset += (uint)file.Size;
			}

			if (mbxWriter == null)
			{
				if (!(base.Accessor is FileAccessor) && mvarMBXFileName == null) throw new InvalidOperationException("Requires a file reference");

				string MBXFileName = null;
				if (mvarMBXFileName != null)
				{
					MBXFileName = mvarMBXFileName;
				}
				else
				{
					FileAccessor acc = (base.Accessor as FileAccessor);
					MBXFileName = System.IO.Path.ChangeExtension(acc.FileName, "mbx");
				}

				mbxWriter = new Writer(new FileAccessor(MBXFileName, true, true));
			}
			mbxWriter.Accessor.Seek(0, SeekOrigin.Begin);
			foreach (File file in fsom.Files)
			{
				file.WriteTo(mbxWriter);
			}
			mbxWriter.Flush();
			mbxWriter.Close();
			mbxWriter = null;
		}
	}
}
