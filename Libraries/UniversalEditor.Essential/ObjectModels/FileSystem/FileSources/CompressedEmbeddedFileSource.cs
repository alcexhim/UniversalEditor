//
//  CompressedEmbeddedFileSource.cs - provides a FileSource for retrieving compressed embedded file data
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
using System.Diagnostics.Contracts;

using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	/// <summary>
	/// Provides a <see cref="FileSource" /> for retrieving compressed embedded file data.
	/// </summary>
	public class CompressedEmbeddedFileSource : FileSource
	{
		public Reader Reader { get; private set; }
		public long Offset { get; private set; }
		public long DecompressedLength { get; private set; }
		public long CompressedLength { get; private set; }

		public override byte[] GetDataInternal(long offset, long length)
		{
			Reader.Seek(Offset, SeekOrigin.Begin);
			byte[] sourceData = Reader.ReadBytes(DecompressedLength);

			long realLength = Math.Min(length, sourceData.Length);
			byte[] data = new byte[realLength];
			Array.Copy(sourceData, 0, data, 0, realLength);
			return data;
		}

		public override long GetLength()
		{
			return DecompressedLength;
		}

		public CompressedEmbeddedFileSource(Reader reader, long offset, long decompressedLength, long compressedLength, FileSourceTransformation[] transformations)
		{
			Reader = reader;
			Offset = offset;
			DecompressedLength = decompressedLength;
			CompressedLength = compressedLength;

			Contract.Assert(!(transformations.Length < 0 && decompressedLength != compressedLength), "Must provide a decompression transformation");

			foreach (FileSourceTransformation transformation in transformations)
			{
				base.Transformations.Add(transformation);
			}
		}
	}
}
