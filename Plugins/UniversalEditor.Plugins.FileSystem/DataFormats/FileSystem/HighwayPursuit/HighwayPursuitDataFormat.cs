//
//  HighwayPursuitDataFormat.cs - provides a DataFormat for manipulating archives in Highway Pursuit format
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

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HighwayPursuit
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Highway Pursuit format.
	/// </summary>
	public class HighwayPursuitDataFormat : DataFormat
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

		private struct FILEINFO
		{
			public uint filesize;
			public uint paddingsize;

			public FILEINFO(uint fileSize, uint paddingSize)
			{
				this.filesize = fileSize;
				this.paddingsize = paddingSize;
			}
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			IO.Reader br = base.Accessor.Reader;
			string HPDT = br.ReadFixedLengthString(4);
			if (HPDT != "HPDT") throw new InvalidDataFormatException("File does not begin with HPDT");

			uint fileCount = br.ReadUInt32();

			FILEINFO[] fis = new FILEINFO[fileCount];
			for (uint i = 0; i < fileCount; i++)
			{
				uint fileSize = br.ReadUInt32();
				uint paddingSize = br.ReadUInt32();
				fis[i] = new FILEINFO(fileSize, paddingSize);
			}
			for (uint i = 0; i < fileCount; i++)
			{
				byte[] filedata = br.ReadBytes(fis[i].filesize);
				byte[] paddingdata = br.ReadBytes(fis[i].paddingsize);

				File file = new File();
				file.Name = i.ToString().PadLeft(8, '0');
				file.SetData(filedata);
				file.Properties.Add("padding", paddingdata);
				fsom.Files.Add(file);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Writer bw = base.Accessor.Writer;

			bw.WriteFixedLengthString("HPDT");
			bw.WriteUInt32((uint)fsom.Files.Count);

			foreach (File file in fsom.Files)
			{
				byte[] filedata = file.GetData();
				byte[] padding = new byte[0];
				if (file.Properties.ContainsKey("padding"))
				{
					padding = (byte[])file.Properties["padding"];
				}
				bw.WriteUInt32((uint)filedata.Length);
				bw.WriteUInt32((uint)padding.Length);
			}
			foreach (File file in fsom.Files)
			{
				byte[] filedata = file.GetData();
				byte[] padding = new byte[0];
				if (file.Properties.ContainsKey("padding"))
				{
					padding = (byte[])file.Properties["padding"];
				}
				bw.WriteBytes(filedata);
				bw.WriteBytes(padding);
			}
		}
	}
}
