using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NWCSceneLayout
{
    public class NWCSceneLayoutObjectModel : ObjectModel
    {
        public override void Clear()
        {
            mvarObjects.Clear();
        }

        public override void CopyTo(ObjectModel where)
        {
            throw new NotImplementedException();
        }

        private ushort mvarWidth = 0;
        public ushort Width { get { return mvarWidth; } set { mvarWidth = value; } }
        private ushort mvarHeight = 0;
        public ushort Height { get { return mvarHeight; } set { mvarHeight = value; } }

        private SceneObject.SceneObjectCollection mvarObjects = new SceneObject.SceneObjectCollection();
        public SceneObject.SceneObjectCollection Objects { get { return mvarObjects; } set { mvarObjects = value; } }

        private string mvarBackgroundImageFileName = String.Empty;
        public string BackgroundImageFileName { get { return mvarBackgroundImageFileName; } set { mvarBackgroundImageFileName = value; } }

        private int mvarBackgroundImageIndex = 0;
        public int BackgroundImageIndex { get { return mvarBackgroundImageIndex; } set { mvarBackgroundImageIndex = value; } }
    }
}
