using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	/// <summary>
	/// The exception that is thrown when a <see cref="DataFormat" /> cannot process a file because the file is
	/// corrupted or unreadable or the file format is not supported by the given <see cref="DataFormat" />.
	/// </summary>
    public class InvalidDataFormatException : DataFormatException
    {
		/// <summary>
		/// Creates a new instance of a <see cref="InvalidDataFormatException" /> with the default message.
		/// </summary>
        public InvalidDataFormatException() : base("The data format is invalid") { }
		/// <summary>
		/// Creates a new instance of a <see cref="InvalidDataFormatException" /> with the specified message.
		/// </summary>
		/// <param name="message">The message to display in the exception window.</param>
		public InvalidDataFormatException(string message) : base(message) { }
		/// <summary>
		/// Creates a new instance of a <see cref="InvalidDataFormatException" /> with the specified message and
		/// inner exception.
		/// </summary>
		/// <param name="message">The message to display in the exception window.</param>
		/// <param name="innerException">The <see cref="Exception" /> that is the cause of this <see cref="InvalidDataFormatException" />.</param>
        public InvalidDataFormatException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidDataFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
