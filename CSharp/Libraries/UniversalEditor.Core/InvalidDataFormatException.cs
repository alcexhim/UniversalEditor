using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
    public class InvalidDataFormatException : DataFormatException
    {
        public InvalidDataFormatException() : base("The data format is invalid") { }
        public InvalidDataFormatException(string message) : base(message) { }
        public InvalidDataFormatException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidDataFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
