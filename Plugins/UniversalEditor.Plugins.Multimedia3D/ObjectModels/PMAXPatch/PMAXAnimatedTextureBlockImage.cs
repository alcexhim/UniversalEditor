using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
    public class PMAXAnimatedTextureBlockImage : ICloneable
    {
        public class PMAXAnimatedTextureBlockImageCollection
            : System.Collections.ObjectModel.Collection<PMAXAnimatedTextureBlockImage>
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
            PMAXAnimatedTextureBlockImage clone = new PMAXAnimatedTextureBlockImage();
            clone.FileName = mvarFileName;
            clone.Timestamp = mvarTimestamp;
            clone.TextureFlags = mvarTextureFlags;
            return clone;
        }
    }
}
