using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Rendering
{
    public class StackUnderflowException : ArgumentOutOfRangeException
    {
        public StackUnderflowException()
            : base("An attempt has been made to perform an operation that would cause an internal stack to underflow.")
        {
        }
        public StackUnderflowException(string message)
            : base(message)
        {
        }
        public StackUnderflowException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public StackUnderflowException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
