using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Homeworld
{
	public class HomeworldDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice("Version", "Format &version:", true,
					new CustomOptionFieldChoice("Version \"VCE0\"", (uint)0),
					new CustomOptionFieldChoice("Version \"WXD1\"", (uint)1)
				));
			}
			return _dfr;
		}

		private uint mvarVersion = 0;
		public uint Version { get { return mvarVersion; } set { mvarVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			string header = br.ReadFixedLengthString(4);
			if (!(header == "VCE0" || header == "WXD1")) throw new InvalidDataFormatException("File does not begin with 'VCE0' or 'WXD1'");

			uint unknown1 = br.ReadUInt32();

			while (!br.EndOfStream)
			{
				string tag = br.ReadFixedLengthString(4);
				switch (tag)
				{
					case "INFO":
					{
						uint unknown2 = br.ReadUInt32();
						break;
					}
					case "DATA":
					{
						uint fileLength = br.ReadUInt32();
						byte[] fileData = br.ReadBytes(fileLength);
						fsom.Files.Add((fsom.Files.Count + 1).ToString().PadLeft(8, '0'), fileData);
						break;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			if (mvarVersion >= 1)
			{
				bw.WriteFixedLengthString("WXD1");
			}
			else
			{
				bw.WriteFixedLengthString("VCE0");
			}

			uint unknown1 = 0;
			bw.WriteUInt32(unknown1);

			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString("DATA");

				byte[] data = file.GetDataAsByteArray();
				bw.WriteUInt32((uint)data.Length);
				bw.WriteBytes(data);
			}
		}
	}
}
