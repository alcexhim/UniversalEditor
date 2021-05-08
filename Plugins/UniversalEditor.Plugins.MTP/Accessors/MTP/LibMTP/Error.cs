//  **************************************************************************
//    Error.cs
//
//    Copyright (C) 2006-2007 Alan McGovern
//    Authors:
//    Alan McGovern (alan.mcgovern@gmail.com)
//  ***************************************************************************/
//
//  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//  DEALINGS IN THE SOFTWARE.


using System;
using System.Runtime.InteropServices;

namespace Mtp
{
	public class LibMtpException : Exception
	{
		public LibMtpException(ErrorCode error) : this(error, error.ToString(), null)
		{
		}

		public LibMtpException(ErrorCode error, string message) : this(error, message, null)
		{

		}

		public LibMtpException(ErrorCode error, string message, Exception innerException)
			: base(message, innerException)
		{
		}

		internal static void CheckErrorStack(MtpDeviceHandle handle)
		{
			IntPtr ptr = MtpDevice.GetErrorStack(handle);
			if (ptr == IntPtr.Zero)
				return;

			LibMtpException ex = null;
			while (ptr != IntPtr.Zero)
			{
				Error e = (Error)Marshal.PtrToStructure(ptr, typeof(Error));
				ex = new LibMtpException(e.errornumber, e.error_text, ex);
				ptr = e.next;
			}

			// Once we throw the exception, clear the error stack
			MtpDevice.ClearErrorStack(handle);
			throw ex;
		}
	}

	public struct Error
	{
		public ErrorCode errornumber;
		[MarshalAs(UnmanagedType.LPStr)] public string error_text;
		public IntPtr next; // LIBMTP_error_t*

		public static void CheckError(ErrorCode errorCode)
		{
			if (errorCode != ErrorCode.None)
			{
				throw new LibMtpException(errorCode, errorCode.ToString());
			}
		}
	}
}
