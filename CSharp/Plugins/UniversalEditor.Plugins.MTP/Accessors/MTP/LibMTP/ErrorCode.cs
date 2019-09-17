/***************************************************************************
 *  MtpDevice.cs
 *
 *  Copyright (C) 2006-2007 Alan McGovern
 *  Authors:
 *  Alan McGovern (alan.mcgovern@gmail.com)
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),
 *  to deal in the Software without restriction, including without limitation
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,
 *  and/or sell copies of the Software, and to permit persons to whom the
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 *  DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mtp
{
	public enum ErrorCode
	{
		/*LIBMTP_ERROR_NONE,
        LIBMTP_ERROR_GENERAL,
        LIBMTP_ERROR_PTP_LAYER,
        LIBMTP_ERROR_USB_LAYER,
        LIBMTP_ERROR_MEMORY_ALLOCATION,
        LIBMTP_ERROR_NO_DEVICE_ATTACHED,
        LIBMTP_ERROR_STORAGE_FULL,
        LIBMTP_ERROR_CONNECTING,
        LIBMTP_ERROR_CANCELLED*/
		None,
		General,
		PtpLayer,
		UsbLayer,
		MemoryAllocation,
		NoDeviceAttached,
		StorageFull,
		Connecting,
		Cancelled
	}
}
