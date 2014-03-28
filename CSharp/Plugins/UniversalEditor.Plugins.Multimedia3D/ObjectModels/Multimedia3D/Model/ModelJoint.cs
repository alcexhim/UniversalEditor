using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
    public class ModelJoint : ICloneable
    {
        public class ModelJointCollection
            : System.Collections.ObjectModel.Collection<ModelJoint>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private PositionVector3 mvarPosition = new PositionVector3(0, 0, 0);
        public PositionVector3 Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private PositionVector3 mvarRotation = new PositionVector3(0, 0, 0);
        public PositionVector3 Rotation { get { return mvarRotation; } set { mvarRotation = value; } }

        private PositionVector3 mvarLimitMoveLow = new PositionVector3(0, 0, 0);
        public PositionVector3 LimitMoveLow { get { return mvarLimitMoveLow; } set { mvarLimitMoveLow = value; } }

        private PositionVector3 mvarLimitMoveHigh = new PositionVector3(0, 0, 0);
        public PositionVector3 LimitMoveHigh { get { return mvarLimitMoveHigh; } set { mvarLimitMoveHigh = value; } }
        
        private PositionVector3 mvarLimitAngleLow = new PositionVector3(0, 0, 0);
        public PositionVector3 LimitAngleLow { get { return mvarLimitAngleLow; } set { mvarLimitAngleLow = value; } }

        private PositionVector3 mvarLimitAngleHigh = new PositionVector3(0, 0, 0);
        public PositionVector3 LimitAngleHigh { get { return mvarLimitAngleHigh; } set { mvarLimitAngleHigh = value; } }
        
        private PositionVector3 mvarSpringConstraintMovementStiffness = new PositionVector3(0, 0, 0);
        /// <summary>
        /// Movement stiffness for spring constraint.
        /// </summary>
        public PositionVector3 SpringConstraintMovementStiffness { get { return mvarSpringConstraintMovementStiffness; } set { mvarSpringConstraintMovementStiffness = value; } }

        private PositionVector3 mvarSpringConstraintRotationStiffness = new PositionVector3(0, 0, 0);
        /// <summary>
        /// Rotation stiffness for spring constraint.
        /// </summary>
        public PositionVector3 SpringConstraintRotationStiffness { get { return mvarSpringConstraintRotationStiffness; } set { mvarSpringConstraintRotationStiffness = value; } }

        public object Clone()
        {
            ModelJoint clone = new ModelJoint();
            clone.LimitAngleHigh = (PositionVector3)(mvarLimitAngleHigh.Clone());
            clone.LimitAngleLow = (PositionVector3)(mvarLimitAngleLow.Clone());
            clone.LimitMoveHigh = (PositionVector3)(mvarLimitMoveHigh.Clone());
            clone.LimitMoveLow = (PositionVector3)(mvarLimitMoveLow.Clone());
            clone.Name = mvarName.Clone() as string;
            clone.Position = ((PositionVector3)mvarPosition.Clone());
            clone.Rotation = ((PositionVector3)mvarRotation.Clone());
            clone.SpringConstraintMovementStiffness = ((PositionVector3)mvarSpringConstraintMovementStiffness.Clone());
            clone.SpringConstraintRotationStiffness = ((PositionVector3)mvarSpringConstraintRotationStiffness.Clone());
            return clone;
        }
    }
}
