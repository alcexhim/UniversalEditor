using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public abstract class IcarusCommand : ICloneable
    {
        public class IcarusCommandCollection
            : System.Collections.ObjectModel.Collection<IcarusCommand>
        {
        }

        public abstract object Clone();
    }
}
