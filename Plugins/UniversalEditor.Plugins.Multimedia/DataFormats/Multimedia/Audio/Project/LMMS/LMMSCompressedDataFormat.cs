//
//  LMMSCompressedDataFormat.cs - provides a DataFormat for manipulating gzip-compressed Linux MultiMedia Studio (LMMS) project files
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

using System;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Project;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Project.LMMS
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating gzip-compressed Linux MultiMedia Studio (LMMS) project files.
	/// </summary>
	public class LMMSCompressedDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(AudioProjectObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			byte[] input = br.ReadToEnd();
			byte[] output = CompressionModules.Gzip.Decompress(input);
			LMMSProjectDataFormat mmp = new LMMSProjectDataFormat();
			MemoryAccessor file = new MemoryAccessor(output);
			Document doc = new Document(objectModel, mmp, file);

			file.Open();
			doc.Load();
			file.Close();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
