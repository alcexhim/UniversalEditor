using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
    public class PMAXAnimatedTextureBlock : ICloneable
    {
        public class PMAXAnimatedTextureBlockCollection
            : System.Collections.ObjectModel.Collection<PMAXAnimatedTextureBlock>
        {
        }

        private int mvarMaterialID = 0;
        public int MaterialID { get { return mvarMaterialID; } set { mvarMaterialID = value; } }

        private PMAXAnimatedTextureBlockImage.PMAXAnimatedTextureBlockImageCollection mvarImages = new PMAXAnimatedTextureBlockImage.PMAXAnimatedTextureBlockImageCollection();
        public PMAXAnimatedTextureBlockImage.PMAXAnimatedTextureBlockImageCollection Images
        {
            get { return mvarImages; }
        }

        public object Clone()
        {
            PMAXAnimatedTextureBlock clone = new PMAXAnimatedTextureBlock();
            foreach (PMAXAnimatedTextureBlockImage image in mvarImages)
            {
                clone.Images.Add(image.Clone() as PMAXAnimatedTextureBlockImage);
            }
            clone.MaterialID = mvarMaterialID;
            return clone;
        }

        private bool mvarAlwaysLight = false;
        public bool AlwaysLight { get { return mvarAlwaysLight; } set { mvarAlwaysLight = value; } }
    }
}
