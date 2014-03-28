using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
    public class igAnimationDatabase : igBase
    {
        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private uint mvarResolveState = 0;
        public uint ResolveState { get { return mvarResolveState; } set { mvarResolveState = value; } }
    }
}
