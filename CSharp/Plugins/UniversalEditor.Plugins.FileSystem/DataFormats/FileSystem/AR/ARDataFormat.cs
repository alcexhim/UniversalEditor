using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.AR
{
	public class ARDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("AR archive", new byte?[][] { new byte?[] { (byte)'!', (byte)'<', (byte)'a', (byte)'r', (byte)'c', (byte)'h', (byte)'>', 0x0A } }, new string[] { "*.ar", "*.deb" });
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			string sig_arch = br.ReadFixedLengthString(7);
			byte sig_mustBe0x0A = br.ReadByte();

            if (sig_arch != "!<arch>" || sig_mustBe0x0A != 0x0A) throw new InvalidDataFormatException("File does not begin with !<arch>, 0x1A");

			while (!br.EndOfStream)
			{
				string szFileName = br.ReadFixedLengthString(0x10).Trim();
                if (szFileName == "\0") break;

                if (szFileName.EndsWith("/"))
                {
                    // GNU ar uses a '/' to mark the end of the filename; this allows for the use of spaces without
                    // the use of an extended filename.
                    szFileName = szFileName.Substring(0, szFileName.Length - 1);
                }

                if (String.IsNullOrEmpty(szFileName))
                {
                    // for some reason, this happens in .a archives
                    // 7-Zip File Manager gives the blank file the archive name without the .a extension,
                    // so that's what we'll do.
                    szFileName = System.IO.Path.GetFileNameWithoutExtension((base.Accessor as FileAccessor).FileName); //; (fsom.Files.Count + 1).ToString().PadLeft(8, '0');
                }

				string fileModTimestamp = br.ReadFixedLengthString(12).Trim();
				string ownerID = br.ReadFixedLengthString(6).Trim();
                string groupID = br.ReadFixedLengthString(6).Trim();
				string fileMode = br.ReadFixedLengthString(8).Trim();
				
                string szFileSize = br.ReadFixedLengthString(10).Trim();
                int fileSize = Int32.Parse(szFileSize);

				string fileMagic = br.ReadFixedLengthString(2);

                byte[] fileData = br.ReadBytes(fileSize);

				fsom.Files.Add(szFileName, fileData);

                if ((br.Accessor.Position % 2) != 0)
                {
                    // fixed 2013-05-20 for certain .a files
                    // The data section is 2 byte aligned. If it would end on an odd offset, a '\n' is used as filler.
                    char xA = br.ReadChar();
                }
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("!<arch>");
			bw.WriteByte((byte)0x0A);
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name.PadRight(0x10, ' '));

                bw.WriteFixedLengthString(new string(' ', 12));          // file modification timestamp
                bw.WriteFixedLengthString(new string(' ', 6));           // owner ID
                bw.WriteFixedLengthString(new string(' ', 6));           // group ID
                bw.WriteFixedLengthString(new string(' ', 8));           // file mode

                // file size
                bw.WriteFixedLengthString(file.Size.ToString().PadLeft(10, ' '), 10);

                bw.WriteByte((byte)0x60);
				bw.WriteByte((byte)10);

                bw.WriteBytes(file.GetDataAsByteArray());
			}
		}
	}
}
