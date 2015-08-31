using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
    public class DataCorruptedException : Exception
    {
        public DataCorruptedException() : base(Localization.StringTable.ErrorDataCorrupted) { }
        public DataCorruptedException(string message) : base(message) { }
        public DataCorruptedException(string message, Exception innerException) : base(message, innerException) { }
        public DataCorruptedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
