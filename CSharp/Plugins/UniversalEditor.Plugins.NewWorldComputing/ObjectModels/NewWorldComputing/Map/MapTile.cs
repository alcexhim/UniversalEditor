using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    public class MapTile
    {
        public class MapTileCollection
            : System.Collections.ObjectModel.Collection<MapTile>
        {
        }

        private MapGroundType mvarGroundType = MapGroundType.Unknown;
        public MapGroundType GroundType { get { return mvarGroundType; } set { mvarGroundType = value; } }
    }
}
