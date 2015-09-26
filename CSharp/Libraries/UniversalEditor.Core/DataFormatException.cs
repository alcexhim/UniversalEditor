using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor
{
	public class DataFormatException : Exception
	{
		public DataFormatException() : base() { }
		public DataFormatException(string message) : base(message) { }
		public DataFormatException(string message, Exception innerException) : base(message, innerException) { }
		public DataFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
