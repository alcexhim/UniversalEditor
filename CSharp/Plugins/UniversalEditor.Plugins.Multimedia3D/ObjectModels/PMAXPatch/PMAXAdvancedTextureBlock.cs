using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
    public class PMAXAdvancedTextureBlock : ICloneable
    {
        public class PMAXAdvancedTextureBlockCollection
            : System.Collections.ObjectModel.Collection<PMAXAdvancedTextureBlock>
        {
        }

        private int mvarMaterialID = 0;
        public int MaterialID { get { return mvarMaterialID; } set { mvarMaterialID = value; } }

        private PMAXAdvancedTextureBlockImage.PMAXAdvancedTextureBlockImageCollection mvarImages = new PMAXAdvancedTextureBlockImage.PMAXAdvancedTextureBlockImageCollection();
        public PMAXAdvancedTextureBlockImage.PMAXAdvancedTextureBlockImageCollection Images
        {
            get { return mvarImages; }
        }

        public object Clone()
        {
            PMAXAdvancedTextureBlock clone = new PMAXAdvancedTextureBlock();
            foreach (PMAXAdvancedTextureBlockImage image in mvarImages)
            {
                clone.Images.Add(image.Clone() as PMAXAdvancedTextureBlockImage);
            }
            clone.MaterialID = mvarMaterialID;
            return clone;
        }

        private bool mvarAlwaysLight = false;
        public bool AlwaysLight { get { return mvarAlwaysLight; } set { mvarAlwaysLight = value; } }
    }
}
