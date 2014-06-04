using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NWCSceneLayout
{
    public abstract class SceneObject : IComparable<SceneObject>
    {
        public class SceneObjectCollection
            : System.Collections.ObjectModel.Collection<SceneObject>
        {
        }

        private ushort mvarDisplayIndex = 0;
        public ushort DisplayIndex { get { return mvarDisplayIndex; } set { mvarDisplayIndex = value; } }

        private ushort mvarLeft = 0;
        public ushort Left { get { return mvarLeft; } set { mvarLeft = value; } }

        private ushort mvarTop = 0;
        public ushort Top { get { return mvarTop; } set { mvarTop = value; } }

        private ushort mvarWidth = 0;
        public ushort Width { get { return mvarWidth; } set { mvarWidth = value; } }

        private ushort mvarHeight = 0;
        public ushort Height { get { return mvarHeight; } set { mvarHeight = value; } }

        public int CompareTo(SceneObject other)
        {
            return mvarDisplayIndex.CompareTo(other.DisplayIndex);
        }
    }
}
