//
//  AFSDataFormat.cs - COMPLETED - implementation of CRI Middleware AFS archive
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using System;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.AFS
{
	/// <summary>
	/// A <see cref="DataFormat" /> for loading and saving <see cref="FileSystemObjectModel" /> archives in CRI Middleware AFS/AWB/ACB format.
	/// </summary>
	public class AFSDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		/// <summary>
		/// Creates a <see cref="DataFormatReference" /> containing metadata about the <see cref="AFSDataFormat" />.
		/// </summary>
		/// <returns>The <see cref="DataFormatReference" /> which contains metadata about the <see cref="AFSDataFormat" />.</returns>
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Gets or sets the version of AFS archive to read or write. Defaults to <see cref="AFSFormatVersion.AFS0" /> ('AFS\0').
		/// </summary>
		/// <value>The version of AFS archive to read or write.</value>
		public AFSFormatVersion FormatVersion { get; set; } = AFSFormatVersion.AFS0;

		/// <summary>
		/// Reads an AFS format 0 archive (AFS).
		/// </summary>
		/// <param name="reader">The <see cref="Reader" /> which reads the data.</param>
		/// <param name="fsom">The <see cref="FileSystemObjectModel" /> into which to populate archive content.</param>
		private void ReadAFS0(IO.Reader reader, FileSystemObjectModel fsom)
		{
			uint fileCount = reader.ReadUInt32();
			AFSFileInfo[] fileinfos = new AFSFileInfo[fileCount];

			for (int i = 0; i < fileCount; i++)
			{
				fileinfos[i].offset = reader.ReadUInt32();
				fileinfos[i].length = reader.ReadUInt32();
			}

			uint tocOffset = reader.ReadUInt32();
			uint tocLength = reader.ReadUInt32();

			if (tocOffset == 0)
			{
				throw new InvalidDataFormatException("table of contents not found");
			}
			else
			{
				reader.Seek(tocOffset, SeekOrigin.Begin);
				for (int j = 0; j < fileCount; j++)
				{
					fileinfos[j].name = reader.ReadFixedLengthString(32).TrimNull();

					ushort year = reader.ReadUInt16();
					ushort month = reader.ReadUInt16();
					ushort day = reader.ReadUInt16();
					ushort hour = reader.ReadUInt16();
					ushort minute = reader.ReadUInt16();
					ushort second = reader.ReadUInt16();
					fileinfos[j].datetime = new DateTime(year, month, day, hour, minute, second);
					fileinfos[j].length2 = reader.ReadUInt32();

					File f = fsom.AddFile(fileinfos[j].name);
					f.Properties.Add("fileinfo", fileinfos[j]);
					f.Size = fileinfos[j].length;
					f.ModificationTimestamp = fileinfos[j].datetime;
					f.DataRequest += f_DataRequest;
				}
			}
		}

		/// <summary>
		/// Reads an AFS format 2 archive (AWB/ACB).
		/// </summary>
		/// <param name="reader">The <see cref="Reader" /> which reads the data.</param>
		/// <param name="fsom">The <see cref="FileSystemObjectModel" /> into which to populate archive content.</param>
		private void ReadAFS2(IO.Reader reader, FileSystemObjectModel fsom)
		{
			uint unknown1 = reader.ReadUInt32();

			uint fileCount = reader.ReadUInt32();
			AFSFileInfo[] fileinfos = new AFSFileInfo[fileCount];

			uint unknown2 = reader.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				ushort index = reader.ReadUInt16();
			}
			for (uint i = 0; i < fileCount; i++)
			{
				fileinfos[i].offset = reader.ReadUInt32();
				fileinfos[i].offset = fileinfos[i].offset.RoundUp(0x10); // does not affect 6 and 1 in v_etc_streamfiles.awb; idk why
				if (i > 0)
				{
					fileinfos[i - 1].length = fileinfos[i].offset - fileinfos[i - 1].offset;
				}
			}

			uint totalArchiveSize = reader.ReadUInt32();
			fileinfos[fileinfos.Length - 1].length = totalArchiveSize - fileinfos[fileinfos.Length - 1].offset;

			ushort unknown4 = reader.ReadUInt16();

			for (uint i = 0; i < fileinfos.Length; i++)
			{
				File f = fsom.AddFile(i.ToString().PadLeft(8, '0'));
				f.Properties.Add("fileinfo", fileinfos[i]);
				f.DataRequest += f_DataRequest;
				f.Size = fileinfos[i].length;
			}
		}

		/// <summary>
		/// Loads the <see cref="ObjectModel" /> data from the input <see cref="Accessor" />.
		/// </summary>
		/// <param name="objectModel">A <see cref="FileSystemObjectModel" /> into which to load archive content.</param>
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			string afs = reader.ReadFixedLengthString(4);

			switch (afs)
			{
				case "AFS\0":
				{
					FormatVersion = AFSFormatVersion.AFS0;
					ReadAFS0(reader, fsom);
					break;
				}
				case "AFS2":
				{
					FormatVersion = AFSFormatVersion.AFS2;
					ReadAFS2(reader, fsom);
					break;
				}
				default:
				{
					throw new InvalidDataFormatException("file does not begin with \"AFS\\0\"");
				}
			}
		}

		void f_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			AFSFileInfo fileinfo = (AFSFileInfo)f.Properties["fileinfo"];

			Accessor.Seek(fileinfo.offset, SeekOrigin.Begin);
			e.Data = Accessor.Reader.ReadBytes(fileinfo.length);
		}

		/// <summary>
		/// Writes the <see cref="ObjectModel" /> data to the output <see cref="Accessor" />.
		/// </summary>
		/// <param name="objectModel">A <see cref="FileSystemObjectModel" /> containing the archive content to write.</param>
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			if ((Accessor.GetFileName()?.ToLower()?.EndsWith(".awb")).GetValueOrDefault())
			{
				FormatVersion = AFSFormatVersion.AFS2;
			}

			Writer writer = Accessor.Writer;
			File[] files = fsom.GetAllFiles();

			if (FormatVersion == AFSFormatVersion.AFS0)
			{
				writer.WriteFixedLengthString("AFS\0");

				uint filecount = (uint)files.LongLength;
				writer.WriteUInt32(filecount);

				uint offset = 8;
				offset += (8 * filecount); // offset + size
				offset += 8; // tocoffset + unknown1

				uint[] offsets = new uint[(filecount * 2) + 1];
				offsets[0] = filecount;

				for (int i = 0; i < filecount; i++)
				{
					offset += ((2048 - (offset % 2048)) % 2048); // align to 2048 byte boundary

					offsets[(i * 2) + 1] = offset;
					offsets[(i * 2) + 2] = (uint)files[i].Size;

					writer.WriteUInt32(offset);
					writer.WriteUInt32((uint)files[i].Size);

					offset += (uint)files[i].Size;
				}

				offset += ((2048 - (offset % 2048)) % 2048); // align to 2048 byte boundary
				uint tocOffset = offset;
				uint tocLength = (uint)(48 * files.Length);
				writer.WriteUInt32(tocOffset);
				writer.WriteUInt32(tocLength);

				// now we should be at file data
				for (int i = 0; i < filecount; i++)
				{
					writer.Align(2048);
					writer.WriteBytes(files[i].GetData());
				}

				// now we should be at the TOC
				writer.Align(2048);
				for (int j = 0; j < filecount; j++)
				{
					writer.WriteFixedLengthString(files[j].Name, 32);

					writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Year);
					writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Month);
					writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Day);
					writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Hour);
					writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Minute);
					writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Second);
					writer.WriteUInt32((uint)offsets[j]);
				}

				writer.Align(2048);
			}
			else if (FormatVersion == AFSFormatVersion.AFS2)
			{
				writer.WriteFixedLengthString("AFS2");

				writer.WriteUInt32(0); //unknown1
				writer.WriteUInt32((uint)files.Length);
				writer.WriteUInt32(32); // unknown2
				for (uint i = 0; i < files.Length; i++)
				{
					writer.WriteUInt16((ushort)i);
				}

				uint offset = (uint)(20 + (files.Length * 6));
				for (uint i = 0; i < files.Length; i++)
				{
					writer.WriteUInt32(offset);
					offset += (uint) files[i].Size;
				}

				writer.WriteUInt32(offset); // total archive size

				for (uint i = 0; i < files.Length; i++)
				{
					writer.Align(16);
					writer.WriteBytes(files[i].GetData());
				}
			}
		}
	}
}
