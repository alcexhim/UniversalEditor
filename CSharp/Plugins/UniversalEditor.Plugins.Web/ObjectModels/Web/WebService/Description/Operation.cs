using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.WebService.Description
{
    public class Operation
    {
        public class OperationCollection
            : System.Collections.ObjectModel.Collection<Operation>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarDescription = String.Empty;
        public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

        private Input.InputCollection mvarInputs = new Input.InputCollection();
        public Input.InputCollection Inputs { get { return mvarInputs; } }

        private Output.OutputCollection mvarOutputs = new Output.OutputCollection();
        public Output.OutputCollection Outputs { get { return mvarOutputs; } }

        private Fault.FaultCollection mvarFaults = new Fault.FaultCollection();
        public Fault.FaultCollection Faults { get { return mvarFaults; } }
    }
}
