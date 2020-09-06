//
//  Ultra3DTBVDataFormat.cs - provides a DataFormat for manipulating archives in Ultra 3D TBV format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.FileSystem.Ultra3D
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Ultra 3D TBV format.
	/// </summary>
	public class Ultra3DTBVDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Description), "_Description", "RichRayl@CUC"));
			}
			return _dfr;
		}

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string header = br.ReadFixedLengthString(8);
			if (header != "TBVolume") throw new InvalidDataFormatException();

			byte nul = br.ReadByte();                       // 0
			ushort unknown1 = br.ReadUInt16();              // 2000
			uint fileCount = br.ReadUInt32();
			ushort unknown2 = br.ReadUInt16();              // 0

			mvarDescription = br.ReadFixedLengthString(24).TrimNull();
			uint unknown3 = br.ReadUInt32();                // 825294546
			uint firstFileOffset = br.ReadUInt32();

			br.Accessor.Seek(firstFileOffset, SeekOrigin.Begin);
			for (uint i = 0; i < fileCount; i++)
			{
				string filename = br.ReadFixedLengthString(24).TrimNull();
				if (filename == String.Empty)
				{
					Console.WriteLine("Ultra3DTBV: encountered empty file name, assuming end of file list");
					break;
				}

				uint length = br.ReadUInt32();
				File file = fsom.AddFile(filename);
				file.Properties["reader"] = br;
				file.Properties["length"] = length;
				file.Properties["offset"] = br.Accessor.Position;
				file.DataRequest += File_DataRequest;
				file.Size = length;

				br.Seek(length, SeekOrigin.Current);
			}
		}

		void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (File)sender;
			uint length = (uint)file.Properties["length"];
			long offset = (long)file.Properties["offset"];
			Reader br = (Reader)file.Properties["reader"];

			br.Seek(offset, SeekOrigin.Begin);
			byte[] data = br.ReadBytes(length);
			e.Data = data;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("TBVolume");

			bw.WriteByte((byte)0);
			bw.WriteUInt16((ushort)2000);
			bw.WriteUInt32((uint)fsom.Files.Count);
			bw.WriteUInt16((ushort)0);

			bw.WriteFixedLengthString(mvarDescription, 24);

			bw.WriteUInt32((uint)825294546);

			bw.WriteUInt32((uint)(600 + bw.Accessor.Position + 4));
			bw.WriteBytes(new byte[600]);

			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 24);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteBytes(file.GetData());
			}
		}
	}
}
