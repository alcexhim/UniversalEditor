using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.WebService.Description
{
    public class Port
    {
        public class PortCollection
            : System.Collections.ObjectModel.Collection<Port>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarDescription = String.Empty;
        public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

        private Operation.OperationCollection mvarOperations = new Operation.OperationCollection();
        public Operation.OperationCollection Operations { get { return mvarOperations; } }
    }
}
