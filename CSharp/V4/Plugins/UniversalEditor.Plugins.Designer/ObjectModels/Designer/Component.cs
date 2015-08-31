using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Designer
{
    public class Component
    {
        public class ComponentCollection
            : System.Collections.ObjectModel.Collection<Component>
        {

        }

        private Guid mvarID = Guid.Empty;
        public Guid ID { get { return mvarID; } set { mvarID = value; } }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private Property.PropertyCollection mvarProperties = new Property.PropertyCollection();
        public Property.PropertyCollection Properties { get { return mvarProperties; } }
    }
}
