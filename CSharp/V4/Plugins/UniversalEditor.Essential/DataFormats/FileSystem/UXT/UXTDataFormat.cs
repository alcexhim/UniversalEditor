using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.UXT
{
	public class UXTDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText("Comment", "Comment: "));
			}
			return _dfr;
		}

		public const int MAX_VERSION = 1;



		private int mvarVersion = MAX_VERSION;
		public int Version { get { return mvarVersion; } set { mvarVersion = value; } }

		private string mvarComment = String.Empty;
		public string Comment { get { return mvarComment; } set { mvarComment = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;

			string signature = br.ReadFixedLengthString(4);
			if (signature != "UXt!") throw new InvalidDataFormatException("File does not begin with \"UxT!\"");

			mvarVersion = br.ReadInt32();
			if (mvarVersion > MAX_VERSION) throw new InvalidDataFormatException("Cannot read version (" + mvarVersion.ToString() + ", expected <= " + MAX_VERSION.ToString() + ")");

			mvarComment = br.ReadNullTerminatedString();

			long entryCount = br.ReadInt64();
			for (long i = 0; i < entryCount; i++)
			{
				string filename = br.ReadFixedLengthString(240);
				filename = filename.TrimNull();
				long offset = br.ReadInt64();
				ulong length = br.ReadUInt64();

				File file = new File();
				file.Name = filename;
				file.Size = (long)length;
				file.Source = new EmbeddedFileSource(br, offset, (long)length);
				fsom.Files.Add(file);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("UXt!");
			bw.WriteInt32(mvarVersion);
			bw.WriteNullTerminatedString(mvarComment);
			bw.WriteInt64(fsom.Files.LongCount<File>());

			long offset = bw.Accessor.Position + (256 * fsom.Files.Count);
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 240);
				bw.WriteInt64(offset);
				bw.WriteUInt64((ulong)file.Size);
				offset += (long)file.Size;
			}
			foreach (File file in fsom.Files)
			{
				file.WriteTo(bw);
			}

			bw.Flush();
		}
	}
}
