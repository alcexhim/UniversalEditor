using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.AIN
{
	public class AINDataFormat : DataFormat
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
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			byte signature = reader.ReadByte();
			if (signature != 0x21)
				throw new InvalidDataFormatException();

			AINCompressionType compressionType = (AINCompressionType)reader.ReadByte();

			int unknown1 = reader.ReadInt32();
			if (unknown1 != 0)
				throw new InvalidDataFormatException();

			ushort unknown2 = reader.ReadUInt16();
			ushort filecount = reader.ReadUInt16();
			for (ushort i = 0; i < filecount; i++)
			{
				uint maybeChecksum = reader.ReadUInt32();
				uint fileLength = reader.ReadUInt32();
				uint unknown3 = reader.ReadUInt32();
				ushort unknown4 = reader.ReadUInt16();

				File file = fsom.AddFile(i.ToString());
				file.Properties.Add("offset", base.Accessor.Position);
				file.Properties.Add("length", fileLength);
				file.DataRequest += File_DataRequest;
				file.Size = fileLength;

				base.Accessor.Seek(fileLength, SeekOrigin.Current);
			}
		}

		void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			long offset = (long)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];

			Accessor.Seek(offset, SeekOrigin.Begin);
			byte[] data = Accessor.Reader.ReadBytes(length);

			e.Data = data;
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
