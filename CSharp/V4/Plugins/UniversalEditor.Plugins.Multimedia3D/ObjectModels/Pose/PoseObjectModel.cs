using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Pose
{
    public class PoseObjectModel : ObjectModel
    {
        public override ObjectModelReference MakeReference()
        {
            ObjectModelReference omr = base.MakeReference();
            omr.Title = "Pose";
            omr.Path = new string[] { "Multimedia", "3D Multimedia", "Pose" };
            omr.Description = "A pose that can be applied to a model in 3D space.";
            return omr;
        }

        public override void Clear()
        {
        }
        public override void CopyTo(ObjectModel where)
        {
        }

        private string mvarModelName = String.Empty;
        /// <summary>
        /// The name of the model to which this pose applies. If not null, and this pose is applied to a model
        /// whose name does not match the model name of the pose, a warning will be issued and the bones may not
        /// match correctly.
        /// </summary>
        public string ModelName { get { return mvarModelName; } set { mvarModelName = value; } }

        private PoseBone.PoseBoneCollection mvarBones = new PoseBone.PoseBoneCollection();
        public PoseBone.PoseBoneCollection Bones { get { return mvarBones; } }
    }
}
