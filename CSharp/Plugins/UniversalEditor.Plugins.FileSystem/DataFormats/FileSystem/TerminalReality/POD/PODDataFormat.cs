using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.TerminalReality.POD
{
	public class PODDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice(nameof(FormatVersion), "Format &version: ", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("POD1", PODVersion.POD1, true),
					new CustomOptionFieldChoice("POD2", PODVersion.POD2),
					new CustomOptionFieldChoice("POD3", PODVersion.POD3)
				}));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Comment), "&Comment: "));
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=PODArchive1");
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=PODArchive2");
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=PODArchive3");
			}
			return _dfr;
		}

		private PODVersion mvarFormatVersion = PODVersion.POD1;
		public PODVersion FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		private string mvarComment = String.Empty;
		public string Comment { get { return mvarComment; } set { mvarComment = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signatureV2 = reader.ReadFixedLengthString(4);
			if (signatureV2 == "POD2")
			{
				mvarFormatVersion = PODVersion.POD2;
			}
			else if (signatureV2 == "POD3")
			{
				mvarFormatVersion = PODVersion.POD3;
			}
			else
			{
				base.Accessor.Seek(-4, SeekOrigin.Current);
			}

			if (mvarFormatVersion >= PODVersion.POD2)
			{
				uint checksum = reader.ReadUInt32();
			}

			mvarComment = reader.ReadFixedLengthString(80);
			mvarComment = mvarComment.TrimNull();
			uint fileCount = reader.ReadUInt32();
			uint trailCount = 0;

			if (mvarFormatVersion >= PODVersion.POD2)
			{
				trailCount = reader.ReadUInt32();
			}

			switch (mvarFormatVersion)
			{
				case PODVersion.POD1:
				{
					for (uint i = 0; i < fileCount; i++)
					{
						string fileName = reader.ReadFixedLengthString(32);
						fileName = fileName.TrimNull();

						uint length = reader.ReadUInt32();
						uint offset = reader.ReadUInt32();

						File file = fsom.AddFile(fileName);
						file.Size = length;
						file.Source = new EmbeddedFileSource(reader, offset, length);
					}
					break;
				}
				case PODVersion.POD2:
				{
					uint[] lengths = new uint[fileCount];
					uint[] offsets = new uint[fileCount];

					for (uint i = 0; i < fileCount; i++)
					{
						// from start of filename directory
						uint filenameOffset = reader.ReadUInt32();

						lengths[i] = reader.ReadUInt32();
						offsets[i] = reader.ReadUInt32();
						ulong unknown = reader.ReadUInt64();
					}
					for (uint i = 0; i < fileCount; i++)
					{
						string fileName = reader.ReadNullTerminatedString();
						
						File file = fsom.AddFile(fileName);
						file.Size = lengths[i];
						file.Source = new EmbeddedFileSource(reader, offsets[i], lengths[i]);
					}
					break;
				}
				case PODVersion.POD3:
				{
					uint unknown1 = reader.ReadUInt32();
					uint unknown2 = reader.ReadUInt32();
					byte[] unknown3 = reader.ReadBytes(160);
					uint directoryOffset = reader.ReadUInt32();
					uint unknown4 = reader.ReadUInt32();
					uint filenameDirectoryLength = reader.ReadUInt32();
					uint unknown5 = reader.ReadUInt32();
					int padding = reader.ReadInt32(); // -1
					uint unknown6 = reader.ReadUInt32();

					base.Accessor.Seek(directoryOffset, SeekOrigin.Begin);

					uint[] lengths = new uint[fileCount];
					uint[] offsets = new uint[fileCount];
					for (uint i = 0; i < fileCount; i++)
					{
						// relative to the start of the filename directory
						uint filenameOffset = reader.ReadUInt32();
						lengths[i] = reader.ReadUInt32();
						offsets[i] = reader.ReadUInt32();
						ulong checksum = reader.ReadUInt64();
					}
					for (uint i = 0; i < fileCount; i++)
					{
						string filename = reader.ReadNullTerminatedString();

						File file = fsom.AddFile(filename);
						file.Size = lengths[i];
						file.Source = new EmbeddedFileSource(reader, offsets[i], lengths[i]);
					}

					// russellm directory
					for (uint i = 0; i < fileCount; i++)
					{
						string russellm = reader.ReadFixedLengthString(32).TrimNull();
						byte[] data = reader.ReadBytes(4); // (byte)20 + "RdA"
						uint unknown7 = reader.ReadUInt32();
						string fileName = reader.ReadFixedLengthString(256).TrimNull();
						ulong checksum = reader.ReadUInt64();
						ulong unknown8 = reader.ReadUInt64();
					}
					break;
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			if (mvarFormatVersion == PODVersion.POD2)
			{
				writer.WriteFixedLengthString("POD2");
				writer.WriteUInt32(0);
			}
			else if (mvarFormatVersion == PODVersion.POD3)
			{
				writer.WriteFixedLengthString("POD3");
				writer.WriteUInt32(0);
			}
			writer.WriteFixedLengthString(mvarComment, 80);

			File[] files = fsom.GetAllFiles();
			writer.WriteUInt32((uint)files.Length);

			if (mvarFormatVersion >= PODVersion.POD2)
			{
				writer.WriteUInt32(0); // trail count
			}

			switch (mvarFormatVersion)
			{
				case PODVersion.POD1:
				{
					uint offset = (uint)(base.Accessor.Position + (40 * files.Length));

					foreach (File file in files)
					{
						writer.WriteFixedLengthString(file.Name, 32);
						uint length = (uint)file.Size;
						writer.WriteUInt32(length);
						writer.WriteUInt32(offset);
						offset += length;
					}

					foreach (File file in files)
					{
						writer.WriteBytes(file.GetData());
					}
					break;
				}
				case PODVersion.POD2:
				{
					uint fileNameDirectoryStart = (uint)base.Accessor.Position;
					fileNameDirectoryStart += (uint)(20 * files.Length);

					uint fileNameOffset = fileNameDirectoryStart;
					uint offset = fileNameDirectoryStart;
					for (uint i = 0; i < files.Length; i++)
					{
						offset += (uint)(files[i].Name.Length + 1);
					}
					for (uint i = 0; i < files.Length; i++)
					{
						// from start of filename directory
						writer.WriteUInt32(fileNameOffset);
						uint length = (uint)(files[i].Size);
						writer.WriteUInt32(length);
						writer.WriteUInt32(offset);
						writer.WriteUInt64(0); // unknown
						fileNameOffset += (uint)(files[i].Name.Length + 1);
						offset += length;
					}
					for (uint i = 0; i < files.Length; i++)
					{
						writer.WriteNullTerminatedString(files[i].Name);
					}
					for (uint i = 0; i < files.Length; i++)
					{
						writer.WriteBytes(files[i].GetData());
					}
					writer.Flush();
					break;
				}
				case PODVersion.POD3:
				{
					writer.WriteUInt32(1000);
					writer.WriteUInt32(1000);
					byte[] unknown3 = new byte[160];
					writer.WriteBytes(unknown3);

					uint directoryOffset = (uint)(base.Accessor.Position + 24);
					for (uint i = 0; i < files.Length; i++)
					{
						directoryOffset += (uint)files[i].Size;
					}
					writer.WriteUInt32(directoryOffset);

					uint unknown4 = 0;
					writer.WriteUInt32(unknown4);

					uint filenameDirectoryLength = 0;
					for (uint i = 0; i < files.Length; i++)
					{
						filenameDirectoryLength += (uint)(files[i].Name.Length + 1);
					}
					writer.WriteUInt32(filenameDirectoryLength);

					uint unknown5 = 0;
					writer.WriteUInt32(unknown5);

					int padding = -1;
					writer.WriteInt32(padding);

					uint unknown6 = 0;
					writer.WriteUInt32(unknown6);

					uint offset = (uint)base.Accessor.Position;
					for (uint i = 0; i < files.Length; i++)
					{
						writer.WriteBytes(files[i].GetData());
					}

					uint filenameOffset = 0;
					for (uint i = 0; i < files.Length; i++)
					{
						// relative to the start of the filename directory
						writer.WriteUInt32(filenameOffset);
						writer.WriteUInt32((uint)files[i].Size);
						writer.WriteUInt32(offset);
						ulong checksum = 0;
						writer.WriteUInt64(checksum);
						filenameOffset += (uint)files[i].Name.Length;
						offset += (uint)files[i].Size;
					}

					// filename directory
					for (uint i = 0; i < files.Length; i++)
					{
						writer.WriteNullTerminatedString(files[i].Name);
					}

					// russellm directory
					for (uint i = 0; i < files.Length; i++)
					{
						writer.WriteFixedLengthString("russellm", 32);
						byte[] data = new byte[] { 20, (byte)'R', (byte)'d', (byte)'A' };
						writer.WriteBytes(data);

						uint unknown7 = 0;
						writer.WriteUInt32(unknown7);

						writer.WriteFixedLengthString(files[i].Name, 256);

						ulong checksum = 0;
						writer.WriteUInt64(checksum);
						ulong unknown8 = 0;
						writer.WriteUInt64(unknown8);
					}
					break;
				}
			}
		}
	}
}
