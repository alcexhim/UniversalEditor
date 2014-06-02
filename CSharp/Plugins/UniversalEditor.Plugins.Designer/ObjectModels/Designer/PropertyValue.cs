using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Designer
{
    public class PropertyValue
    {
        public class PropertyValueCollection
            : System.Collections.ObjectModel.Collection<PropertyValue>
        {

        }

        private Property mvarProperty = null;
        public Property Property { get { return mvarProperty; } set { mvarProperty = value; } }

        private bool mvarIsSet = false;
        public bool IsSet { get { return mvarIsSet; } set { mvarIsSet = value; } }

        private object mvarValue = null;
        public object Value { get { return mvarValue; } set { mvarValue = value; } }
    }
}
