//
//  TLCDDataFormat.cs - provides a DataFormat for manipulating archives in The Learning Company TLCD format
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
using System.Collections.Generic;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TLCD
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in The Learning Company TLCD format.
	/// </summary>
	public class TLCDDataFormat : DataFormat
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
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			string tagTLCD = br.ReadFixedLengthString(4);
			if (tagTLCD != "TLCD") throw new InvalidDataFormatException("File does not begin with \"TLCD\"");

			uint unknown1 = br.ReadUInt32();
			uint count = br.ReadUInt32();
			uint unknown3 = br.ReadUInt32();
			uint unknown4 = br.ReadUInt32();

			List<Internal.TLCSequence> seqs = new List<Internal.TLCSequence>();

			for (uint i = 0; i < count; i++)
			{
				// filetype can be ASEQ, SSND, LIPS, OTHR
				Internal.TLCSequence seq = new Internal.TLCSequence();
				seq.filetype = br.ReadFixedLengthString(4);
				seq.offset = br.ReadUInt32();
				seq.length = br.ReadUInt32();
				seq.id = br.ReadUInt32();
				seq.unknown4 = br.ReadUInt32();
				seqs.Add(seq);
			}

			foreach (Internal.TLCSequence seq in seqs)
			{
				File file = new File();
				file.Properties.Add("fileinfo", seq);
				file.Name = seq.id.ToString() + "." + seq.filetype;
				file.Size = seq.length;
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Internal.TLCSequence seq = (Internal.TLCSequence)file.Properties["fileinfo"];

			IO.Reader br = base.Accessor.Reader;
			br.Accessor.Position = seq.offset;
			e.Data = br.ReadBytes(seq.length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
