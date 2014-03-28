using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Motion
{
    public abstract class MotionAction : ICloneable
    {
        public class MotionActionCollection
            : System.Collections.ObjectModel.Collection<MotionAction>
        {
        }

        public abstract object Clone();
    }
    /// <summary>
    /// Repositions the specified face on the current model.
    /// </summary>
    public class MotionFaceRepositionAction : MotionAction
    {
        private string mvarFaceName = String.Empty;
        /// <summary>
        /// The name of the face to which this action is applied.
        /// </summary>
        public string FaceName { get { return mvarFaceName; } set { mvarFaceName = value; } }

        private float mvarWeight = 0.0f;
        /// <summary>
        /// The weight of the face.
        /// </summary>
        public float Weight { get { return mvarWeight; } set { mvarWeight = value; } }

        public override object Clone()
        {
            MotionFaceRepositionAction clone = new MotionFaceRepositionAction();
            clone.FaceName = (mvarFaceName.Clone() as string);
            clone.Weight = mvarWeight;
            return clone;
        }
    }
    /// <summary>
    /// Repositions the specified bone on the current model.
    /// </summary>
    public class MotionBoneRepositionAction : MotionAction
    {
        private string mvarBoneName = String.Empty;
        /// <summary>
        /// The name of the bone to which this action is applied.
        /// </summary>
        public string BoneName { get { return mvarBoneName; } set { mvarBoneName = value; } }

        private PositionVector3 mvarPosition = new PositionVector3();
        public PositionVector3 Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private PositionVector4 mvarRotation = new PositionVector4();
        public PositionVector4 Rotation { get { return mvarRotation; } set { mvarRotation = value; } }

        private MotionInterpolationData mvarInterpolation = new MotionInterpolationData();
        public MotionInterpolationData Interpolation { get { return mvarInterpolation; } }

        private MotionInterpolationData mvarInterpolation2 = new MotionInterpolationData();
        public MotionInterpolationData Interpolation2 { get { return mvarInterpolation2; } }

        private MotionInterpolationData mvarInterpolation3 = new MotionInterpolationData();
        public MotionInterpolationData Interpolation3 { get { return mvarInterpolation3; } }

        private MotionInterpolationData mvarInterpolation4 = new MotionInterpolationData();
        public MotionInterpolationData Interpolation4 { get { return mvarInterpolation4; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Reposition: ");
            sb.Append(" bone \"");
            sb.Append(mvarBoneName);
            sb.Append("\" @ pos ");
            sb.Append(mvarPosition.ToString());
            sb.Append("; rot ");
            sb.Append(mvarRotation.ToString());
            return sb.ToString();
        }

        public override object Clone()
        {
            MotionBoneRepositionAction clone = new MotionBoneRepositionAction();
            clone.BoneName = (mvarBoneName.Clone() as string);

			clone.Interpolation.Rotation.X1 = mvarInterpolation.Rotation.X1;
			clone.Interpolation.Rotation.X2 = mvarInterpolation.Rotation.X2;
			clone.Interpolation.Rotation.Y1 = mvarInterpolation.Rotation.Y1;
			clone.Interpolation.Rotation.Y2 = mvarInterpolation.Rotation.Y2;
			clone.Interpolation.XAxis.X1 = mvarInterpolation.XAxis.X1;
			clone.Interpolation.XAxis.X2 = mvarInterpolation.XAxis.X2;
			clone.Interpolation.XAxis.Y1 = mvarInterpolation.XAxis.Y1;
			clone.Interpolation.XAxis.Y2 = mvarInterpolation.XAxis.Y2;
			clone.Interpolation.YAxis.X1 = mvarInterpolation.YAxis.X1;
			clone.Interpolation.YAxis.X2 = mvarInterpolation.YAxis.X2;
			clone.Interpolation.YAxis.Y1 = mvarInterpolation.YAxis.Y1;
			clone.Interpolation.YAxis.Y2 = mvarInterpolation.YAxis.Y2;
			clone.Interpolation.ZAxis.X1 = mvarInterpolation.ZAxis.X1;
			clone.Interpolation.ZAxis.X2 = mvarInterpolation.ZAxis.X2;
			clone.Interpolation.ZAxis.Y1 = mvarInterpolation.ZAxis.Y1;
			clone.Interpolation.ZAxis.Y2 = mvarInterpolation.ZAxis.Y2;

			clone.Interpolation2.Rotation.X1 = mvarInterpolation2.Rotation.X1;
			clone.Interpolation2.Rotation.X2 = mvarInterpolation2.Rotation.X2;
			clone.Interpolation2.Rotation.Y1 = mvarInterpolation2.Rotation.Y1;
			clone.Interpolation2.Rotation.Y2 = mvarInterpolation2.Rotation.Y2;
			clone.Interpolation2.XAxis.X1 = mvarInterpolation2.XAxis.X1;
			clone.Interpolation2.XAxis.X2 = mvarInterpolation2.XAxis.X2;
			clone.Interpolation2.XAxis.Y1 = mvarInterpolation2.XAxis.Y1;
			clone.Interpolation2.XAxis.Y2 = mvarInterpolation2.XAxis.Y2;
			clone.Interpolation2.YAxis.X1 = mvarInterpolation2.YAxis.X1;
			clone.Interpolation2.YAxis.X2 = mvarInterpolation2.YAxis.X2;
			clone.Interpolation2.YAxis.Y1 = mvarInterpolation2.YAxis.Y1;
			clone.Interpolation2.YAxis.Y2 = mvarInterpolation2.YAxis.Y2;
			clone.Interpolation2.ZAxis.X1 = mvarInterpolation2.ZAxis.X1;
			clone.Interpolation2.ZAxis.X2 = mvarInterpolation2.ZAxis.X2;
			clone.Interpolation2.ZAxis.Y1 = mvarInterpolation2.ZAxis.Y1;
			clone.Interpolation2.ZAxis.Y2 = mvarInterpolation2.ZAxis.Y2;

			clone.Interpolation3.Rotation.X1 = mvarInterpolation3.Rotation.X1;
			clone.Interpolation3.Rotation.X2 = mvarInterpolation3.Rotation.X2;
			clone.Interpolation3.Rotation.Y1 = mvarInterpolation3.Rotation.Y1;
			clone.Interpolation3.Rotation.Y2 = mvarInterpolation3.Rotation.Y2;
			clone.Interpolation3.XAxis.X1 = mvarInterpolation3.XAxis.X1;
			clone.Interpolation3.XAxis.X2 = mvarInterpolation3.XAxis.X2;
			clone.Interpolation3.XAxis.Y1 = mvarInterpolation3.XAxis.Y1;
			clone.Interpolation3.XAxis.Y2 = mvarInterpolation3.XAxis.Y2;
			clone.Interpolation3.YAxis.X1 = mvarInterpolation3.YAxis.X1;
			clone.Interpolation3.YAxis.X2 = mvarInterpolation3.YAxis.X2;
			clone.Interpolation3.YAxis.Y1 = mvarInterpolation3.YAxis.Y1;
			clone.Interpolation3.YAxis.Y2 = mvarInterpolation3.YAxis.Y2;
			clone.Interpolation3.ZAxis.X1 = mvarInterpolation3.ZAxis.X1;
			clone.Interpolation3.ZAxis.X2 = mvarInterpolation3.ZAxis.X2;
			clone.Interpolation3.ZAxis.Y1 = mvarInterpolation3.ZAxis.Y1;
			clone.Interpolation3.ZAxis.Y2 = mvarInterpolation3.ZAxis.Y2;

			clone.Interpolation4.Rotation.X1 = mvarInterpolation4.Rotation.X1;
			clone.Interpolation4.Rotation.X2 = mvarInterpolation4.Rotation.X2;
			clone.Interpolation4.Rotation.Y1 = mvarInterpolation4.Rotation.Y1;
			clone.Interpolation4.Rotation.Y2 = mvarInterpolation4.Rotation.Y2;
			clone.Interpolation4.XAxis.X1 = mvarInterpolation4.XAxis.X1;
			clone.Interpolation4.XAxis.X2 = mvarInterpolation4.XAxis.X2;
			clone.Interpolation4.XAxis.Y1 = mvarInterpolation4.XAxis.Y1;
			clone.Interpolation4.XAxis.Y2 = mvarInterpolation4.XAxis.Y2;
			clone.Interpolation4.YAxis.X1 = mvarInterpolation4.YAxis.X1;
			clone.Interpolation4.YAxis.X2 = mvarInterpolation4.YAxis.X2;
			clone.Interpolation4.YAxis.Y1 = mvarInterpolation4.YAxis.Y1;
			clone.Interpolation4.YAxis.Y2 = mvarInterpolation4.YAxis.Y2;
			clone.Interpolation4.ZAxis.X1 = mvarInterpolation4.ZAxis.X1;
			clone.Interpolation4.ZAxis.X2 = mvarInterpolation4.ZAxis.X2;
			clone.Interpolation4.ZAxis.Y1 = mvarInterpolation4.ZAxis.Y1;
			clone.Interpolation4.ZAxis.Y2 = mvarInterpolation4.ZAxis.Y2;

            clone.Position = ((PositionVector3)mvarPosition.Clone());
            clone.Rotation = ((PositionVector4)mvarRotation.Clone());
            return clone;
        }
    }
    /// <summary>
    /// Updates the texture on the specified material.
    /// </summary>
    public class MotionTextureUpdateAction : MotionAction
    {
        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
