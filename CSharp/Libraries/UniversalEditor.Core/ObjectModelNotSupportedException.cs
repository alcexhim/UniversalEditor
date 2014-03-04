using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
    public class ObjectModelNotSupportedException : NotSupportedException
    {
        public ObjectModelNotSupportedException()
            : base("Object model not supported")
        {
        }
        public ObjectModelNotSupportedException(Exception innerException)
            : base("Object model not supported", innerException)
        {
        }
        public ObjectModelNotSupportedException(string message)
            : base(message)
        {
        }
        public ObjectModelNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
