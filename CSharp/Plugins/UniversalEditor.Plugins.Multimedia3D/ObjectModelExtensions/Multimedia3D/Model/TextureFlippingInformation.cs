using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModelExtensions.Multimedia3D.Model
{
    public class TextureFlippingInformation : ICloneable
    {
        private bool mvarEnabled = false;
        public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

        private TextureFlippingBlock.TextureFlippingBlockCollection mvarBlocks = new TextureFlippingBlock.TextureFlippingBlockCollection();
        public TextureFlippingBlock.TextureFlippingBlockCollection Blocks { get { return mvarBlocks; } }

        public object Clone()
        {
            TextureFlippingInformation clone = new TextureFlippingInformation();
            clone.Enabled = mvarEnabled;
            foreach (TextureFlippingBlock block in mvarBlocks)
            {
                clone.Blocks.Add(block.Clone() as TextureFlippingBlock);
            }
            return clone;
        }
    }
}
