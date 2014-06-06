using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.VersatileContainer.Sections
{
    public class VersatileContainerDirectorySection : VersatileContainerSection
    {
        public override object Clone()
        {
            VersatileContainerDirectorySection clone = new VersatileContainerDirectorySection();
            return clone;
        }
    }
}
