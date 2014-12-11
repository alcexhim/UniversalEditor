using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PMAXPatch;

namespace UniversalEditor.DataFormats.PMAXPatch
{
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
