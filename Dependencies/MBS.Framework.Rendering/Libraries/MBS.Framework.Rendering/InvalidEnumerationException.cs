using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Rendering
{
    public class InvalidEnumerationException : ArgumentOutOfRangeException
    {
        public InvalidEnumerationException()
            : base("An unacceptable value is specified for an enumerated argument.")
        {
        }
        public InvalidEnumerationException(string message)
            : base(message)
        {
        }
        public InvalidEnumerationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public InvalidEnumerationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
