//
//  PMAXAdvancedTextureBlockChunk.cs - represents an advanced texture block chunk in a PMAX patch file
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

using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.PMAXPatch.Chunks
{
	/// <summary>
	/// Represents an advanced texture block chunk in a PMAX patch file.
	/// </summary>
	public class PMAXAdvancedTextureBlockChunk : PMAXPatchChunk
	{
		private string mvarName = "TEXA";
		public override string Name
		{
			get { return mvarName; }
		}

		public PMAXAdvancedTextureBlock.PMAXAdvancedTextureBlockCollection AdvancedTextureBlocks { get; } = new PMAXAdvancedTextureBlock.PMAXAdvancedTextureBlockCollection();

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
				AdvancedTextureBlocks.Add(block);
			}
		}
		public override void SaveInternal(Accessor accessor)
		{
			Writer writer = new Writer(accessor);
			writer.WriteInt32(AdvancedTextureBlocks.Count);
			foreach (PMAXAdvancedTextureBlock block in AdvancedTextureBlocks)
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
			foreach (PMAXAdvancedTextureBlock block in AdvancedTextureBlocks)
			{
				clone.AdvancedTextureBlocks.Add(block.Clone() as PMAXAdvancedTextureBlock);
			}
			return clone;
		}
	}
}
