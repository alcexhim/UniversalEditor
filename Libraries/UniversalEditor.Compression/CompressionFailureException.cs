//
//  CompressionFailureException.cs - the exception that is thrown when a compression or decompression operation results in an error
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
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

namespace UniversalEditor.Compression
{
	/// <summary>
	/// The exception that is thrown when a compression or decompression operation results in an error.
	/// </summary>
	[Serializable]
	public class CompressionFailureException : Exception
	{
		public CompressionFailureException() { }
		public CompressionFailureException(string message) : base(message) { }
		public CompressionFailureException(string message, Exception inner) : base(message, inner) { }
		protected CompressionFailureException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

		private CompressionMethod mvarMethod = CompressionMethod.Unknown;
		/// <summary>
		/// The method used during the compression/decompression operation.
		/// </summary>
		public CompressionMethod Method { get { return mvarMethod; } }

		private CompressionMode mvarMode = CompressionMode.Compress;
		/// <summary>
		/// Whether the operation was a compression or a decompression.
		/// </summary>
		public CompressionMode Mode { get { return mvarMode; } }

		public CompressionFailureException(CompressionMethod method)
		{
			mvarMethod = method;
		}
		public CompressionFailureException(string message, CompressionMethod method) : base(message)
		{
			mvarMethod = method;
		}
		public CompressionFailureException(string message, Exception inner, CompressionMethod method) : base(message, inner)
		{
			mvarMethod = method;
		}
		public CompressionFailureException(CompressionMode mode)
		{
			mvarMode = mode;
		}
		public CompressionFailureException(string message, CompressionMode mode) : base(message)
		{
			mvarMode = mode;
		}
		public CompressionFailureException(string message, Exception inner, CompressionMode mode) : base(message, inner)
		{
			mvarMode = mode;
		}
		public CompressionFailureException(CompressionMethod method, CompressionMode mode)
		{
			mvarMethod = method;
			mvarMode = mode;
		}
		public CompressionFailureException(string message, CompressionMethod method, CompressionMode mode) : base(message)
		{
			mvarMethod = method;
			mvarMode = mode;
		}
		public CompressionFailureException(string message, Exception inner, CompressionMethod method, CompressionMode mode) : base(message, inner)
		{
			mvarMethod = method;
			mvarMode = mode;
		}
	}
}
