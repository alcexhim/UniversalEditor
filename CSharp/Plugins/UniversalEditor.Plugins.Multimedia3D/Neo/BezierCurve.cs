using System;
using System.Collections.Generic;
using System.Text;

namespace Neo
{
    public class BezierCurve
    {
        public class BezierCurveCollection
            : System.Collections.ObjectModel.Collection<BezierCurve>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private byte mvarX1 = 0;
        public byte X1 { get { return mvarX1; } set { mvarX1 = value; } }

        private byte mvarX2 = 0;
        public byte X2 { get { return mvarX2; } set { mvarX2 = value; } }

        private byte mvarY1 = 0;
        public byte Y1 { get { return mvarY1; } set { mvarY1 = value; } }

        private byte mvarY2 = 0;
        public byte Y2 { get { return mvarY2; } set { mvarY2 = value; } }

        public override string ToString()
        {
            return mvarName + ": (" + mvarX1.ToString() + ", " + mvarY1.ToString() + ")-(" + mvarX2.ToString() + ", " + mvarY2.ToString();
        }
    }
}
