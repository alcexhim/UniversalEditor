using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Designer
{
    public class Library
    {
        public class LibraryCollection
            : System.Collections.ObjectModel.Collection<Library>
        {

        }

        private Guid mvarID = Guid.Empty;
        public Guid ID { get { return mvarID; } set { mvarID = value; } }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private Component.ComponentCollection mvarComponents = new Component.ComponentCollection();
        public Component.ComponentCollection Components { get { return mvarComponents; } set { mvarComponents = value; } }
    }
}
