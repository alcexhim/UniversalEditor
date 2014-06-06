using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.VersatileContainer.Sections
{
    public class VersatileContainerBlankSection : VersatileContainerSection
    {
        public override object Clone()
        {
            VersatileContainerBlankSection clone = new VersatileContainerBlankSection();
            return clone;
        }
    }
}
