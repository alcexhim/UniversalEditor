using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Rendering
{
    public class InvalidFrameBufferException : InvalidOperationException
    {
        public InvalidFrameBufferException()
            : base("The framebuffer object is not complete.")
        {
        }
        public InvalidFrameBufferException(string message)
            : base(message)
        {
        }
        public InvalidFrameBufferException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public InvalidFrameBufferException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
