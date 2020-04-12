//
//  PMAXPatchBinaryDataFormat.cs - provides a DataFormat for manipulating Concertroid PMAX patch files in binary format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PMAXPatch;

namespace UniversalEditor.DataFormats.PMAXPatch
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Concertroid PMAX patch files in binary format.
	/// </summary>
	/// <remarks>
	/// This is a DataFormat I created for Concertroid and really should be refactored into a separate library.
	/// </remarks>
	public class PMAXPatchBinaryDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(PMAXPatchObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PMAXPatchObjectModel patches = (objectModel as PMAXPatchObjectModel);
			if (patches == null) return;

			UniversalEditor.ObjectModels.PMAXPatch.PMAXPatch patch = new UniversalEditor.ObjectModels.PMAXPatch.PMAXPatch();

			Reader br = base.Accessor.Reader;
			string PMAX = br.ReadFixedLengthString(4);
			if (PMAX != "PMAX") return;

			short versionMajor = br.ReadInt16();
			short versionMinor = br.ReadInt16();

			int numChunksInFile = br.ReadInt32();
			for (int i = 0; i < numChunksInFile; i++)
			{
				string chunkName = br.ReadFixedLengthString(4);
				int chunkSize = br.ReadInt32();

				switch (chunkName)
				{
					case "MTLN":
					{
						UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXMaterialNamesChunk chunk = new UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXMaterialNamesChunk();
						chunk.Size = chunkSize;
						chunk.LoadInternal(base.Accessor);
						patch.Chunks.Add(chunk);
						break;
					}
					case "TEXA":
					{
						UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXAdvancedTextureBlockChunk chunk = new UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXAdvancedTextureBlockChunk();
						chunk.Size = chunkSize;
						chunk.LoadInternal(base.Accessor);
						patch.Chunks.Add(chunk);
						break;
					}
					case "EFXS":
					{
						UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXEffectsScriptChunk chunk = new UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXEffectsScriptChunk();
						chunk.Size = chunkSize;
						chunk.LoadInternal(base.Accessor);
						patch.Chunks.Add(chunk);
						break;
					}
				}
			}

			patches.Patches.Add(patch);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PMAXPatchObjectModel patches = (objectModel as PMAXPatchObjectModel);
			if (patches == null) return;
			if (patches.Patches.Count < 1) return;

			UniversalEditor.ObjectModels.PMAXPatch.PMAXPatch patch = patches.Patches[0];

			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("PMAX");

			short versionMajor = 1, versionMinor = 0;
			bw.WriteInt16(versionMajor);
			bw.WriteInt16(versionMinor);

			// number of PMAX chunks in this file
			bw.WriteInt32(patch.Chunks.Count);

			// the chunk data
			foreach (PMAXPatchChunk chunk in patch.Chunks)
			{
				bw.WriteFixedLengthString(chunk.Name, 4);

				MemoryAccessor mem = new MemoryAccessor();
				chunk.SaveInternal(mem);

				byte[] data = mem.ToArray();
				bw.WriteInt32(data.Length);
				bw.WriteBytes(data);
			}

			bw.Flush();
		}
	}
}
