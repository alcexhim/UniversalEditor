using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Parameters
{
    public class ModelRangeParameter : ModelParameter
    {
        private double mvarDefaultValue = 0;
        public double DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

        private double mvarMinimumValue = 0;
        public double MinimumValue { get { return mvarMinimumValue; } set { mvarMinimumValue = value; } }

        private double mvarMaximumValue = 0;
        public double MaximumValue { get { return mvarMaximumValue; } set { mvarMaximumValue = value; } }

        private double mvarSmallChange = 0;
        public double SmallChange { get { return mvarSmallChange; } set { mvarSmallChange = value; } }

        private double mvarLargeChange = 0;
        public double LargeChange { get { return mvarLargeChange; } set { mvarLargeChange = value; } }

        public override object Clone()
        {
            ModelRangeParameter clone = new ModelRangeParameter();
            clone.DefaultValue = mvarDefaultValue;
            clone.MinimumValue = mvarMinimumValue;
            clone.MaximumValue = mvarMaximumValue;
            clone.SmallChange = mvarSmallChange;
            clone.LargeChange = mvarLargeChange;
            return clone;
        }
    }
}
