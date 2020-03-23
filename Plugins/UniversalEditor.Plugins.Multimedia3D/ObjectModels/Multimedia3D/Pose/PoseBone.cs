using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Pose
{
    public class PoseBone
    {
        public class PoseBoneCollection
            : System.Collections.ObjectModel.Collection<PoseBone>
        {
        }

        private string mvarBoneID = String.Empty;
        public string BoneID { get { return mvarBoneID; } set { mvarBoneID = value; } }

        private string mvarBoneName = String.Empty;
        public string BoneName { get { return mvarBoneName; } set { mvarBoneName = value; } }

        private PositionVector3 mvarPosition = new PositionVector3(0, 0, 0);
        public PositionVector3 Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private PositionVector4 mvarQuaternion = new PositionVector4(0, 0, 0, 0);
        public PositionVector4 Quaternion { get { return mvarQuaternion; } set { mvarQuaternion = value; } }
    }
}
