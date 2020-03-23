using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    public class MapObject
    {
        private MapObjectType mvarObjectType = MapObjectType.Unknown00;
        public MapObjectType ObjectType { get { return mvarObjectType; } set { mvarObjectType = value; } }
    }
}
