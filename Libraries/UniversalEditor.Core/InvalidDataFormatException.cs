//
//  InvalidDataFormatException.cs - raised when the DataFormat has problems loading accessor
//
//  Author:
//	   Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

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
		public InvalidDataFormatException() : base(Localization.StringTable.ErrorDataFormatInvalid) { }
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
