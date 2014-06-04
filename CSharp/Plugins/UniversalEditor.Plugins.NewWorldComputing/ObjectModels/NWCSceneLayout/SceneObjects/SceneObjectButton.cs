using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NWCSceneLayout.SceneObjects
{
    public class SceneObjectButton : SceneObject
    {
        private string mvarBackgroundImageFileName = String.Empty;
        public string BackgroundImageFileName { get { return mvarBackgroundImageFileName; } set { mvarBackgroundImageFileName = value; } }

        private uint mvarBackgroundImageIndex = 0;
        public uint BackgroundImageIndex { get { return mvarBackgroundImageIndex; } set { mvarBackgroundImageIndex = value; } }
    }
}
