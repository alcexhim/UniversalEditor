//
//  LGPDataFormat.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2019 Mike Becker
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
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.SquareSoft
{
	public class LGPDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText("Creator", "_Creator"));
				_dfr.ExportOptions.Add(new CustomOptionText("Description", "_Description"));
				_dfr.Title = "SquareSoft LGP archive";
			}
			return _dfr;
		}

		public string Creator { get; set; } = String.Empty;
		public string Description { get; set; } = String.Empty;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			int i;
			string fileName;
			byte[] ww;
			Reader br = base.Accessor.Reader;
			short twonulls = br.ReadInt16();
			Creator = br.ReadFixedLengthString(10);
			int u1 = br.ReadInt32();
			for (i = 0; i < u1; i++)
			{
				fileName = br.ReadNullTerminatedString(20);
				short u = br.ReadInt16();
				byte b = br.ReadByte();
				ww = br.ReadBytes((uint)4);
				fsom.Files.Add(fileName, new byte[0]);
			}

			br.Seek((uint)0xE12, SeekOrigin.Current);

			for (i = 0; i < u1; i++)
			{
				fileName = br.ReadNullTerminatedString(20);
				int dataLength = br.ReadInt32();

				fsom.Files[i].Source = new EmbeddedFileSource(br, base.Accessor.Position, dataLength);
				br.Seek(dataLength, SeekOrigin.Current);
			}
			Description = br.ReadFixedLengthString(14);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
