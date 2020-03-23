using System;
using System.Collections.Generic;
using System.Linq;

using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.PMAXPatch.Chunks
{
	public class PMAXAdvancedTextureBlockChunk : PMAXPatchChunk
	{
		private string mvarName = "TEXA";
		public override string Name
		{
			get { return mvarName; }
		}

		private PMAXAdvancedTextureBlock.PMAXAdvancedTextureBlockCollection mvarAdvancedTextureBlocks = new PMAXAdvancedTextureBlock.PMAXAdvancedTextureBlockCollection();
		public PMAXAdvancedTextureBlock.PMAXAdvancedTextureBlockCollection AdvancedTextureBlocks { get { return mvarAdvancedTextureBlocks; } }


		public override void LoadInternal(Accessor accessor)
		{
			Reader reader = new Reader(accessor);
			int animatedTextureBlocksCount = reader.ReadInt32();
			for (int i = 0; i < animatedTextureBlocksCount; i++)
			{
				PMAXAdvancedTextureBlock block = new PMAXAdvancedTextureBlock();
				block.MaterialID = reader.ReadInt32();
				int flags = reader.ReadInt32();
				if (flags == 1)
				{
					block.AlwaysLight = true;
				}
				else
				{
					block.AlwaysLight = false;
				}

				int imageCount = reader.ReadInt32();
				for (int j = 0; j < imageCount; j++)
				{
					PMAXAdvancedTextureBlockImage image = new PMAXAdvancedTextureBlockImage();
					image.Timestamp = reader.ReadInt64();
					image.FileName = reader.ReadNullTerminatedString();
					image.TextureFlags = (UniversalEditor.ObjectModels.Multimedia3D.Model.ModelTextureFlags)reader.ReadInt32();
					block.Images.Add(image);
				}
				mvarAdvancedTextureBlocks.Add(block);
			}
		}
		public override void SaveInternal(Accessor accessor)
		{
			Writer writer = new Writer(accessor);
			writer.WriteInt32(mvarAdvancedTextureBlocks.Count);
			foreach (PMAXAdvancedTextureBlock block in mvarAdvancedTextureBlocks)
			{
				writer.WriteInt32(block.MaterialID);

				int flags = 1; // always light the texture
				if (block.AlwaysLight)
				{
					flags = 1;
				}
				else
				{
					flags = 0;
				}
				writer.WriteInt32(flags);

				writer.WriteInt32(block.Images.Count);
				foreach (PMAXAdvancedTextureBlockImage image in block.Images)
				{
					writer.WriteInt64(image.Timestamp);
					writer.WriteNullTerminatedString(image.FileName);
					writer.WriteInt32((int)image.TextureFlags);
				}
			}

			writer.Flush();
		}

		public override object Clone()
		{
			PMAXAdvancedTextureBlockChunk clone = new PMAXAdvancedTextureBlockChunk();
			foreach (PMAXAdvancedTextureBlock block in mvarAdvancedTextureBlocks)
			{
				clone.AdvancedTextureBlocks.Add(block.Clone() as PMAXAdvancedTextureBlock);
			}
			return clone;
		}
	}
}
