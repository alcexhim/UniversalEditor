using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
    public class PMAXAdvancedTextureBlockImage : ICloneable
    {
        public class PMAXAdvancedTextureBlockImageCollection
            : System.Collections.ObjectModel.Collection<PMAXAdvancedTextureBlockImage>
        {
        }

        private long mvarTimestamp = 0;
        public long Timestamp { get { return mvarTimestamp; } set { mvarTimestamp = value; } }

        private string mvarFileName = String.Empty;
        public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

        private ModelTextureFlags mvarTextureFlags = ModelTextureFlags.None;
        public ModelTextureFlags TextureFlags { get { return mvarTextureFlags; } set { mvarTextureFlags = value; } }

        public object Clone()
        {
            PMAXAdvancedTextureBlockImage clone = new PMAXAdvancedTextureBlockImage();
            clone.FileName = mvarFileName;
            clone.Timestamp = mvarTimestamp;
            clone.TextureFlags = mvarTextureFlags;
            return clone;
        }
    }
}
