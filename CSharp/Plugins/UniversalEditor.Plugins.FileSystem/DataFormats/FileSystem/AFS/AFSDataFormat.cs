//
//  AFSDataFormat.cs
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

namespace UniversalEditor.DataFormats.FileSystem.AFS
{
	public class AFSDataFormat : DataFormat
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
			string afs = reader.ReadFixedLengthString(4);
			if (afs != "AFS\0")
				throw new InvalidDataFormatException("file does not begin with \"AFS\\0\"");

			uint fileCount = reader.ReadUInt32();
			AFSFileInfo[] fileinfos = new AFSFileInfo[fileCount];

			for (int i = 0; i < fileCount; i++)
			{
				fileinfos[i].offset = reader.ReadUInt32();
				fileinfos[i].length = reader.ReadUInt32();
			}

			uint tocOffset = 0u;
			while (Accessor.Position < fileinfos[0].offset && tocOffset == 0)
			{
				tocOffset = reader.ReadUInt32();
				uint num3 = reader.ReadUInt32();
			}
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
					fileinfos[j].maybeChecksum = reader.ReadUInt32();

					File f = fsom.AddFile(fileinfos[j].name);
					f.Properties.Add("fileinfo", fileinfos[j]);
					f.Size = fileinfos[j].length;
					f.ModificationTimestamp = fileinfos[j].datetime;
					f.DataRequest += f_DataRequest;
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


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.WriteFixedLengthString("AFS\0");

			File[] files = fsom.GetAllFiles();

			uint filecount = (uint)files.LongLength;
			writer.WriteUInt32(filecount);

			uint offset = 8;
			offset += (8 * filecount); // offset + size
			offset += 8; // tocoffset + unknown1

			for (int i = 0; i < filecount; i++)
			{
				writer.WriteUInt32(offset);
				writer.WriteUInt32((uint)files[i].Size);
				offset += (uint)files[i].Size;
			}

			uint tocOffset = offset;
			writer.WriteUInt32(tocOffset);
			writer.WriteUInt32(0);

			// now we should be at file data
			for (int i = 0; i < filecount; i++)
			{
				writer.WriteBytes(files[i].GetData());
			}

			// now we should be at the TOC
			for (int j = 0; j < filecount; j++)
			{
				writer.WriteFixedLengthString(files[j].Name, 32);

				writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Year);
				writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Month);
				writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Day);
				writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Hour);
				writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Minute);
				writer.WriteUInt16((ushort)files[j].ModificationTimestamp.Second);
				writer.WriteUInt32(0); // maybe checksum
			}
		}
	}
}
