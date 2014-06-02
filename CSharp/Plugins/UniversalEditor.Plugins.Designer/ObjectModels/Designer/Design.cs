using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Designer
{
    public class Design
    {
        public class DesignCollection
            : System.Collections.ObjectModel.Collection<Design>
        {

        }

        private ComponentInstance.ComponentInstanceCollection mvarComponentInstances = new ComponentInstance.ComponentInstanceCollection();
        public ComponentInstance.ComponentInstanceCollection ComponentInstances { get { return mvarComponentInstances; } }

    }
}
