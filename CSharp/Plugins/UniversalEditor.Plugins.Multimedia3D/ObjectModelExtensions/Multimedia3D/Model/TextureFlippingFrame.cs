using System;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModelExtensions.Multimedia3D.Model
{
    public class TextureFlippingFrame : ICloneable
    {
        public class TextureFlippingFrameCollection : System.Collections.ObjectModel.Collection<TextureFlippingFrame>
        {
        }

        private ulong mvarTimestamp = 0uL;
        public ulong Timestamp { get { return mvarTimestamp; } set { mvarTimestamp = value; } }

        private string mvarFileName = string.Empty;
        public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

        public object Clone()
        {
            TextureFlippingFrame clone = new TextureFlippingFrame();
            clone.Timestamp = mvarTimestamp;
            clone.FileName = (mvarFileName.Clone() as string);
            return clone;
        }
    }
}
