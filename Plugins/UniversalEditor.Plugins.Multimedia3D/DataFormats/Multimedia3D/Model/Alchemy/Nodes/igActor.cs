using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
    public class igActor : igBase
    {
        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private uint mvarFlags = 0;
        public uint Flags { get { return mvarFlags; } set { mvarFlags = value; } }

        private float[] mvarTransform = new float[16];
        public float[] Transform { get { return mvarTransform; } set { mvarTransform = value; } }
    }
}
