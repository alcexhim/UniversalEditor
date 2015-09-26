using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
    public class ModelAngleLimit
    {
        private bool mvarEnabled = false;
        public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

        private PositionVector3 mvarLower = PositionVector3.Empty;
        public PositionVector3 Lower { get { return mvarLower; } set { mvarLower = value; } }

        private PositionVector3 mvarUpper = PositionVector3.Empty;
        public PositionVector3 Upper { get { return mvarUpper; } set { mvarUpper = value; } }
    }
}
