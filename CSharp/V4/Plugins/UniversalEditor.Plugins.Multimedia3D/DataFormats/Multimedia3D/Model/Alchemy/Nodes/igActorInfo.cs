using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
    public class igActorInfo : igBase
    {
        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private uint mvarResolveState = 0;
        public uint ResolveState { get { return mvarResolveState; } set { mvarResolveState = value; } }

        private igActor mvarActor = null;
        public igActor Actor { get { return mvarActor; } set { mvarActor = value; } }

        private igActorList mvarActorList = null;
        public igActorList ActorList { get { return mvarActorList; } set { mvarActorList = value; } }

        private igAnimationDatabase mvarAnimationDatabase = null;
        public igAnimationDatabase AnimationDatabase { get { return mvarAnimationDatabase; } set { mvarAnimationDatabase = value; } }
    }
}
