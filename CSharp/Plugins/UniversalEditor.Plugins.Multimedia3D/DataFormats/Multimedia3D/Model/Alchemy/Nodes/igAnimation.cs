using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
    public class igAnimation : igBase
    {
        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private uint mvarPriority = 0;
        public uint Priority { get { return mvarPriority; } set { mvarPriority = value; } }

        private igAnimationBindingList mvarBindingList = null;
        public igAnimationBindingList BindingList { get { return mvarBindingList; } set { mvarBindingList = value; } }

        private igAnimationTrackList mvarTrackList = null;
        public igAnimationTrackList TrackList { get { return mvarTrackList; } set { mvarTrackList = value; } }

        private igAnimationTransitionDefinitionList mvarTransitionDefinitionList = null;
        public igAnimationTransitionDefinitionList TransitionDefinitionList { get { return mvarTransitionDefinitionList; } set { mvarTransitionDefinitionList = value; } }

        private ulong mvarKeyFrameTimeOffset = 0;
        public ulong KeyFrameTimeOffset { get { return mvarKeyFrameTimeOffset; } set { mvarKeyFrameTimeOffset = value; } }

        private ulong mvarStartTime = 0;
        public ulong StartTime { get { return mvarStartTime; } set { mvarStartTime = value; } }

        private ulong mvarDuration = 0;
        public ulong Duration { get { return mvarDuration; } set { mvarDuration = value; } }

        private uint mvarUseAnimationTransBoolArray = 0;
        public uint UseAnimationTransBoolArray { get { return mvarUseAnimationTransBoolArray; } set { mvarUseAnimationTransBoolArray = value; } }
    }
}
