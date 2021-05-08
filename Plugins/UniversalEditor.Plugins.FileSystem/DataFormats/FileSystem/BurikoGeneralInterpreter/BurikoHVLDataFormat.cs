//
//  BurikoHVLDataFormat.cs - provides a DataFormat for manipulating archives in Buriko General Interpreter HVL format
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

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.BurikoGeneralInterpreter
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Buriko General Interpreter HVL format.
	/// </summary>
	public class BurikoHVLDataFormat : DataFormat
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

			IO.Reader br = base.Accessor.Reader;
			string BHV_____0000 = br.ReadFixedLengthString(12);
			if (BHV_____0000 != "BHV_____\0\0\0\0")
			{
				throw new InvalidDataFormatException("File does not begin with 'BHV_____', 0x00, 0x00, 0x00, 0x00");
			}

			int FileCount = br.ReadInt32();
			for (int i = 0; i < FileCount; i++)
			{
				string FileName = br.ReadFixedLengthString(56);
				if (FileName.Contains("\0")) FileName = FileName.Substring(0, FileName.IndexOf('\0'));

				int u1 = br.ReadInt32();
				int u2 = br.ReadInt32();

				MemoryAccessor ma = new MemoryAccessor();
				IO.Writer bw = new IO.Writer(ma);
				bw.Close();

				fsom.Files.Add(FileName, ma.ToArray());
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
