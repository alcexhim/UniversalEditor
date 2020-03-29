using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public abstract class IcarusExpression : ICloneable
    {
        public class IcarusExpressionCollection
            : System.Collections.ObjectModel.Collection<IcarusExpression>
        {
        }

		public abstract object Clone();

		protected abstract bool GetValueInternal(ref object value);
		public T GetValue<T>(T defaultValue = default(T))
		{
			object val = defaultValue;
			bool ret = GetValueInternal(ref val);
			if (ret) return (T)val;
			return defaultValue;
		}
	}
}
