using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandCamera : IcarusPredefinedCommand
    {
        public override string Name
        {
            get { return "camera"; }
        }

        private IcarusCameraOperation mvarOperation = IcarusCameraOperation.None;
        public IcarusCameraOperation Operation { get { return mvarOperation; } set { mvarOperation = value; } }

        private IcarusExpression.IcarusExpressionCollection mvarParameters = new IcarusExpression.IcarusExpressionCollection();
        public IcarusExpression.IcarusExpressionCollection Parameters { get { return mvarParameters; } }

        public override object Clone()
        {
            IcarusCommandCamera clone = new IcarusCommandCamera();
            clone.Operation = mvarOperation;
            return clone;
        }
    }
}
