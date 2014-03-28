using System;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.ObjectModelExtensions.Multimedia3D.Model
{
    public class TextureFlippingBlock : ICloneable
    {
        public class TextureFlippingBlockCollection : System.Collections.ObjectModel.Collection<TextureFlippingBlock>
        {
        }

        private ModelMaterial mvarMaterial = null;
        public ModelMaterial Material { get { return mvarMaterial; } set { mvarMaterial = value; } }

        private TextureFlippingFrame.TextureFlippingFrameCollection mvarFrames = new TextureFlippingFrame.TextureFlippingFrameCollection();
        public TextureFlippingFrame.TextureFlippingFrameCollection Frames { get { return mvarFrames; } }

        public object Clone()
        {
            TextureFlippingBlock clone = new TextureFlippingBlock();
            clone.Material = mvarMaterial;
            foreach (TextureFlippingFrame frame in mvarFrames)
            {
                clone.Frames.Add(frame.Clone() as TextureFlippingFrame);
            }
            return clone;
        }
    }
}
