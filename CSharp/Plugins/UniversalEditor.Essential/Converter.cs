using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public struct ConverterCapability
	{
		public Type from;
		public Type to;
		public ConverterCapability(Type from, Type to)
		{
			this.from = from;
			this.to = to;
		}
	}
	public class ConverterCapabilityCollection
	{
        private List<ConverterCapability> caps = new List<ConverterCapability>();

        public void Add(Type from, Type to)
        {
            ConverterCapability cap = new ConverterCapability(from, to);
            caps.Add(cap);
        }
		public bool Contains(Type from, Type to)
		{
            ConverterCapability cap = new ConverterCapability(from, to);
            return caps.Contains(cap);
		}
		public bool Remove(Type from, Type to)
		{
            ConverterCapability cap = new ConverterCapability(from, to);
            if (caps.Contains(cap))
            {
                caps.Remove(cap);
                return true;
            }
            return false;
		}
	}
	public class ConverterReference
	{
		private Type mvarConverterType = null;
		public Type ConverterType { get { return mvarConverterType; } set { mvarConverterType = value; } }

		private ConverterCapabilityCollection mvarCapabilities = new ConverterCapabilityCollection();
		public ConverterCapabilityCollection Capabilities { get { return mvarCapabilities; } }

		public ConverterReference(Type type)
		{
			if (!type.IsSubclassOf(typeof(Converter)))
			{
				throw new InvalidCastException("Cannot create a converter reference to a non-Converter type");
			}
			else if (type.IsAbstract)
			{
				throw new InvalidOperationException("Cannot create a converter reference to an abstract type");
			}

			mvarConverterType = type;
		}
	}
	public abstract class Converter
	{
		public abstract void Convert(ObjectModel from, ObjectModel to);
		public virtual ConverterReference MakeReference()
		{
			ConverterReference _cr = new ConverterReference(this.GetType());
			return _cr;
		}
	}
}
