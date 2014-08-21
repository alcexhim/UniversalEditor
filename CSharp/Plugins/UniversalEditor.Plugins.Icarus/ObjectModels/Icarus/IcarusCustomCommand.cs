using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public class IcarusCustomCommand : IcarusCommand
    {
        private string mvarName = String.Empty;
        public new string Name { get { return mvarName; } set { mvarName = value; } }

        private int mvarCommandType = 0;
        public int CommandType { get { return mvarCommandType; } set { mvarCommandType = value; } }

        private IcarusExpression.IcarusExpressionCollection mvarParameters = new IcarusExpression.IcarusExpressionCollection();
        public IcarusExpression.IcarusExpressionCollection Parameters { get { return mvarParameters; } }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
