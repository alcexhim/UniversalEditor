using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TerminalReality.POD
{
	public class PODDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice("FormatVersion", "Format &version: ", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("POD1", PODVersion.POD1, true),
					new CustomOptionFieldChoice("POD2", PODVersion.POD2)
				}));
				_dfr.ExportOptions.Add(new CustomOptionText("ArchiveName", "Archive &name: "));
				_dfr.Filters.Add("Terminal Reality POD archive", new string[] { "*.pod" });
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=PODArchive1");
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=PODArchive2");
			}
			return _dfr;
		}

		private PODVersion mvarFormatVersion = PODVersion.POD1;
		public PODVersion FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		private string mvarArchiveName = String.Empty;
		public string ArchiveName { get { return mvarArchiveName; } set { mvarArchiveName = value; } }

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
			else
			{
				base.Accessor.Seek(-4, SeekOrigin.Current);
			}

			if (mvarFormatVersion == PODVersion.POD2)
			{
				uint unknown1 = reader.ReadUInt32();
			}

			mvarArchiveName = reader.ReadFixedLengthString(80);
			mvarArchiveName = mvarArchiveName.TrimNull();
			uint fileCount = reader.ReadUInt32();
			uint trailCount = 0;

			if (mvarFormatVersion == PODVersion.POD2)
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
						file.Properties.Add("reader", reader);
						file.Properties.Add("length", length);
						file.Properties.Add("offset", offset);
						file.DataRequest += file_DataRequest;
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
						file.Properties.Add("reader", reader);
						file.Properties.Add("length", lengths[i]);
						file.Properties.Add("offset", offsets[i]);
						file.DataRequest += file_DataRequest;
					}
					break;
				}
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint length = (uint)file.Properties["length"];
			uint offset = (uint)file.Properties["offset"];

			reader.Accessor.Seek(offset, SeekOrigin.Begin);
			byte[] data = reader.ReadBytes(length);
			e.Data = data;
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
			writer.WriteFixedLengthString(mvarArchiveName, 80);

			File[] files = fsom.GetAllFiles();
			writer.WriteUInt32((uint)files.Length);

			if (mvarFormatVersion == PODVersion.POD2)
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
						writer.WriteBytes(file.GetDataAsByteArray());
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
						writer.WriteBytes(files[i].GetDataAsByteArray());
					}
					writer.Flush();
					break;
				}
			}
		}
	}
}
