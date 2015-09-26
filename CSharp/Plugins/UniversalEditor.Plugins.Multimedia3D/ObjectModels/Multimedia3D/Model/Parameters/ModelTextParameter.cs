using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Parameters
{
    public class ModelTextParameter : ModelParameter
    {
        private string mvarDefaultValue = String.Empty;
        public string DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

        private bool mvarRequireChoice = false;
        public bool RequireChoice { get { return mvarRequireChoice; } set { mvarRequireChoice = value; } }

        private System.Collections.Specialized.StringCollection mvarAvailableValues = new System.Collections.Specialized.StringCollection();
        public System.Collections.Specialized.StringCollection AvailableValues { get { return mvarAvailableValues; } }

        public override object Clone()
        {
            ModelTextParameter clone = new ModelTextParameter();
            clone.DefaultValue = (mvarDefaultValue.Clone() as string);
            clone.RequireChoice = mvarRequireChoice;
            foreach (string availableValue in mvarAvailableValues)
            {
                clone.AvailableValues.Add(availableValue.Clone() as string);
            }
            return clone;
        }
    }
}
