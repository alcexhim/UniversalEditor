//
//  RARDataFormat.cs - provides a DataFormat for manipulating archives in RAR format
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

using System.Collections.Generic;
using MBS.Framework;
using UniversalEditor.DataFormats.FileSystem.WinRAR.Blocks;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.UserInterface;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in RAR format.
	/// </summary>
	/// <remarks>
	/// Due to the licensing restrictions of the published unrar source, ALL contributions to this codebase MUST NOT have used ANY part of the encumbered
	/// published unrar source in the development of this <see cref="DataFormat" />. It may end up that this <see cref="DataFormat" /> is only able
	/// to handle uncompressed (i.e. stored) RAR files, and until there exists a better solution to the unrar licensing problem, it will have to do.
	/// </remarks>
	public class RARDataFormat : RARBlockDataFormat
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

		void F_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (File)sender;
			Reader reader = (Reader)f.Properties["reader"];
			long offset = (long)f.Properties["offset"];
			long CompressedLength = (long)f.Properties["CompressedLength"];
			long DecompressedLength = (long)f.Properties["DecompressedLength"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(CompressedLength);
			byte[] decompressedData = compressedData;
			e.Data = decompressedData;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new RARBlockObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			RARBlockObjectModel bom = objectModels.Pop() as RARBlockObjectModel;

			FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;

			bool endOfArchiveReached = false;
			for (int i = 0; i < bom.Blocks.Count; i++)
			{
				RARBlock header = bom.Blocks[i];
				if (header is RARFileBlock fh)
				{
					if (fh.HeaderType == RARBlockType.File)
					{
						File f = fsom.AddFile(fh.fileName);
						f.Properties["reader"] = reader;
						f.Properties["offset"] = fh.dataOffset;
						f.Properties["CompressedLength"] = fh.DataSize;
						f.Properties["DecompressedLength"] = fh.unpackedSize;
						f.Size = fh.unpackedSize;
						f.DataRequest += F_DataRequest;
					}
					else if (fh.HeaderType == RARBlockType.Service)
					{
						if (fh.fileName == "CMT")
						{

						}
						else if (fh.fileName == "QO")
						{

						}
					}
				}
				else if (header is RAREndBlock eh)
				{
					endOfArchiveReached = true;
				}
			}

			if (!endOfArchiveReached)
			{
				(Application.Instance as IHostApplication).Messages.Add(HostApplicationMessageSeverity.Warning, "end of file reached before end of archive marker", Accessor.GetFileName());
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			RARBlockObjectModel bom = new RARBlockObjectModel();
			objectModels.Push(bom);
		}
	}
}
