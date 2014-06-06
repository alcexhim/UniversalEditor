using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.VersatileContainer.Sections
{
    public class VersatileContainerReferenceSection : VersatileContainerSection
    {
        private VersatileContainerSection mvarTarget = null;
        public VersatileContainerSection Target { get { return mvarTarget; } set { mvarTarget = value; } }

        public override object Clone()
        {
            VersatileContainerReferenceSection clone = new VersatileContainerReferenceSection();
            clone.Target = mvarTarget;
            return clone;
        }
    }
}
