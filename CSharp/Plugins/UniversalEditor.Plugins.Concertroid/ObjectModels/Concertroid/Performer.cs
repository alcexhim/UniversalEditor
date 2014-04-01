using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid
{
    public class Performer
    {
        public class PerformerCollection
            : System.Collections.ObjectModel.Collection<Performer>
        {
        }

        private Guid mvarID = Guid.Empty;
        public Guid ID { get { return mvarID; } set { mvarID = value; } }

        private Character mvarCharacter = null;
        public Character Character { get { return mvarCharacter; } set { mvarCharacter = value; } }

        private Costume mvarCostume = null;
        public Costume Costume { get { return mvarCostume; } set { mvarCostume = value; } }

        private Animation mvarAnimation = null;
        public Animation Animation { get { return mvarAnimation; } set { mvarAnimation = value; } }

        private PositionVector3 mvarOffset = new PositionVector3();
        public PositionVector3 Offset { get { return mvarOffset; } set { mvarOffset = value; } }
    }
}
