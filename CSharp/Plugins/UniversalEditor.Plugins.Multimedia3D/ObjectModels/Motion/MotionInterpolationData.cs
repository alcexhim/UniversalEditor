using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Motion
{
    public class MotionInterpolationData
    {
        public MotionInterpolationData()
        {
            mvarXAxis.Name = "X-axis interpolation bezier curve";
            mvarYAxis.Name = "Y-axis interpolation bezier curve";
            mvarZAxis.Name = "Z-axis interpolation bezier curve";
            mvarRotation.Name = "Rotation interpolation bezier curve";
        }

        private Neo.BezierCurve mvarXAxis = new Neo.BezierCurve();
        public Neo.BezierCurve XAxis { get { return mvarXAxis; } }

        private Neo.BezierCurve mvarYAxis = new Neo.BezierCurve();
        public Neo.BezierCurve YAxis { get { return mvarYAxis; } }

        private Neo.BezierCurve mvarZAxis = new Neo.BezierCurve();
        public Neo.BezierCurve ZAxis { get { return mvarZAxis; } }

        private Neo.BezierCurve mvarRotation = new Neo.BezierCurve();
        public Neo.BezierCurve Rotation { get { return mvarRotation; } }

    }
}
