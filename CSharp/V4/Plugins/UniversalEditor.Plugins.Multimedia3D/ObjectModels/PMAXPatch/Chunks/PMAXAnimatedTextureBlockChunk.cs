using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.PMAXPatch.Chunks
{
    public class PMAXAnimatedTextureBlockChunk : PMAXPatchChunk
    {
        private string mvarName = "TEXA";
        public override string Name
        {
            get { return mvarName; }
        }

        private PMAXAnimatedTextureBlock.PMAXAnimatedTextureBlockCollection mvarAnimatedTextureBlocks = new PMAXAnimatedTextureBlock.PMAXAnimatedTextureBlockCollection();
        public PMAXAnimatedTextureBlock.PMAXAnimatedTextureBlockCollection AnimatedTextureBlocks { get { return mvarAnimatedTextureBlocks; } }


        public override void LoadInternal(System.IO.Stream stream)
        {
            UniversalEditor.IO.BinaryReader br = new UniversalEditor.IO.BinaryReader(stream);

            int animatedTextureBlocksCount = br.ReadInt32();
            for (int i = 0; i < animatedTextureBlocksCount; i++)
            {
                PMAXAnimatedTextureBlock block = new PMAXAnimatedTextureBlock();
                block.MaterialID = br.ReadInt32();
                int flags = br.ReadInt32();
                if (flags == 1)
                {
                    block.AlwaysLight = true;
                }
                else
                {
                    block.AlwaysLight = false;
                }

                int imageCount = br.ReadInt32();
                for (int j = 0; j < imageCount; j++)
                {
                    PMAXAnimatedTextureBlockImage image = new PMAXAnimatedTextureBlockImage();
                    image.Timestamp = br.ReadInt64();
                    image.FileName = br.ReadNullTerminatedString();
                    image.TextureFlags = (UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model.ModelTextureFlags)br.ReadInt32();
                    block.Images.Add(image);
                }
                mvarAnimatedTextureBlocks.Add(block);
            }
        }
        public override void SaveInternal(System.IO.Stream stream)
        {
            UniversalEditor.IO.BinaryWriter bw = new UniversalEditor.IO.BinaryWriter(stream);

            bw.Write(mvarAnimatedTextureBlocks.Count);
            foreach (PMAXAnimatedTextureBlock block in mvarAnimatedTextureBlocks)
            {
                bw.Write(block.MaterialID);

                int flags = 1; // always light the texture
                if (block.AlwaysLight)
                {
                    flags = 1;
                }
                else
                {
                    flags = 0;
                }
                bw.Write(flags);

                bw.Write(block.Images.Count);
                foreach (PMAXAnimatedTextureBlockImage image in block.Images)
                {
                    bw.Write(image.Timestamp);
                    bw.WriteNullTerminatedString(image.FileName);
                    bw.Write((int)image.TextureFlags);
                }
            }

            bw.Flush();
        }

        public override object Clone()
        {
            PMAXAnimatedTextureBlockChunk clone = new PMAXAnimatedTextureBlockChunk();
            foreach (PMAXAnimatedTextureBlock block in mvarAnimatedTextureBlocks)
            {
                clone.AnimatedTextureBlocks.Add(block.Clone() as PMAXAnimatedTextureBlock);
            }
            return clone;
        }
    }
}
