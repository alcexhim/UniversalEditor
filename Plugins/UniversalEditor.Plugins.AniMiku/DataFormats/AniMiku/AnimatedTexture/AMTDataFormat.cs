//
//  AMTDataFormat.cs - implementation of AniMiku Texture Package data format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.FileSystem.AniMiku.TexturePackage
{
	/// <summary>
	/// Implements the AniMiku Texture Package data format.
	/// </summary>
	public class AMTDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
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
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			int unknown = br.ReadInt32();
			int count = br.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				int dataSize = br.ReadInt32();
				byte[] data = br.ReadBytes(dataSize);
				fsom.Files.Add(i.ToString().PadLeft(8, '0'), data);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			int unknown = 150;
			bw.WriteInt32(unknown);

			bw.WriteInt32(fsom.Files.Count);
			foreach (File file in fsom.Files)
			{
				byte[] data = file.GetData();
				bw.WriteInt32(data.Length);
				bw.WriteBytes(data);
			}
		}
	}
}
