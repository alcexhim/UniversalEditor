//
//  DPKDataFormat.cs - provides a DataFormat for manipulating archives in Mech Commander DPK format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.MechCommander
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Mech Commander DPK format.
	/// </summary>
	public class MechCommanderDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Title = "MechCommander DPK/FST archive";
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private bool compress = false;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;

			int nEntriesInTOC = br.ReadInt32();

			int[] nSizeCompressed = new int[nEntriesInTOC];

			for (int i = 0; i < nEntriesInTOC; i++)
			{
				int nFileOffset = br.ReadInt32();
				nSizeCompressed[i] = br.ReadInt32();
				int nSizeUncompressed = br.ReadInt32();
				string path = br.ReadNullTerminatedString(250);

				File f = new File();
				f.Name = path;
				// f.SetData(new byte[nSizeUncompressed]); // idk why I did this in the original
				fsom.Files.Add(f);
			}
			for (int i = 0; i < nEntriesInTOC; i++)
			{
				byte[] fileData = br.ReadBytes(nSizeCompressed[i]);
				fsom.Files[i].SetData(fileData);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;
			bw.WriteInt32(fsom.Files.Count);

			byte[] compressedData = new byte[] { };
			int nFileOffset = 4 + 262;
			for (int i = 0; i < fsom.Files.Count; i++)
			{
				bw.WriteInt32(nFileOffset);

				if (compress)
				{
					bw.WriteInt32(compressedData.Length);
				}
				else
				{
					bw.WriteInt32((int)fsom.Files[i].Size);
				}
				bw.WriteInt32((int)fsom.Files[i].Size);
				bw.WriteFixedLengthString(fsom.Files[i].Name, 250);

				if (compress)
				{
					nFileOffset += 262 + compressedData.Length;
				}
				else
				{
					nFileOffset += 262 + (int)fsom.Files[i].Size;
				}
			}
			for (int i = 0; i < fsom.Files.Count; i++)
			{
				if (compress)
				{
					bw.WriteBytes(compressedData);
				}
				else
				{
					bw.WriteBytes(fsom.Files[i].GetData());
				}
			}
		}
	}
}
