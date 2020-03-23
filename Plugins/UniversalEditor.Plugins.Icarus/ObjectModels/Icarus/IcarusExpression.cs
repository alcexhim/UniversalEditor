using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public class IcarusExpression : ICloneable
    {
        public class IcarusExpressionCollection
            : System.Collections.ObjectModel.Collection<IcarusExpression>
        {
        }

        private IcarusExpressionType mvarType = IcarusExpressionType.None;
        public IcarusExpressionType Type { get { return mvarType; } set { mvarType = value; } }

        private object mvarValue = null;
        public object Value { get { return mvarValue; } set { mvarValue = value; } }

        public IcarusExpression(IcarusExpressionType type, object value)
        {
            mvarType = type;
            mvarValue = value;
        }
        public IcarusExpression(float value) : this(IcarusExpressionType.Float, value) { }
        public IcarusExpression(string value) : this(IcarusExpressionType.String, value) { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (mvarType == IcarusExpressionType.String) sb.Append("\"");
            sb.Append(mvarValue.ToString());
            if (mvarType == IcarusExpressionType.String) sb.Append("\"");
            return sb.ToString();
        }

        public object Clone()
        {
            object value = mvarValue;
            if (mvarValue is ICloneable) value = (mvarValue as ICloneable).Clone();
            
            IcarusExpression clone = new IcarusExpression(mvarType, value);
            return clone;
        }
    }
}
