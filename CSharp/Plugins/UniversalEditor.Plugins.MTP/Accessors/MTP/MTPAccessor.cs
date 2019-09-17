//
//  MyClass.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UniversalEditor.IO;

namespace UniversalEditor.Accessors.MTP
{
	public class MTPAccessor : Accessor
	{
		private static AccessorReference _ar = null;

		protected override void InitializeInternal()
		{
			base.InitializeInternal();
		}

		public override long Length { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public override void Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		protected override void CloseInternal()
		{
			throw new NotImplementedException();
		}

		protected override long GetPosition()
		{
			return 0;
		}

		protected override AccessorReference MakeReferenceInternal()
		{
			if (_ar == null)
			{
				_ar = base.MakeReferenceInternal();
				_ar.Title = "Media Transfer Protocol (MTP)";
				_ar.Schemas.Add("mtp-disabled");
				_ar.ImportOptions.Add(new CustomOptionText("FileName", "File _name"));
			}
			return _ar;
		}

		private static IntPtr Offset(IntPtr ptr, int offset)
		{
			if (IntPtr.Size == 8)
			{
				return (IntPtr)(ptr.ToInt64() + offset);
			}
			else
			{
				return (IntPtr)(ptr.ToInt32() + offset);
			}
		}

		protected override void OpenInternal()
		{
			/*
			List<Mtp.RawMtpDevice> list = Mtp.MtpDevice.Detect();
			Mtp.RawMtpDevice rawdev = list[0];
			Mtp.MtpDevice dev = Mtp.MtpDevice.Connect(rawdev);

			List<Mtp.Folder> listFolders = dev.GetRootFolders();
			*/
			// int w = Internal.Methods.Get_File_To_File(hRawDevice, 0, OriginalUri.LocalPath, null, IntPtr.Zero);

			// IntPtr hFile = Internal.Linux.Methods.g_file_new_for_uri(OriginalUri.OriginalString);
		}

		protected override int ReadInternal(byte[] buffer, int start, int count)
		{
			throw new NotImplementedException();
		}

		protected override int WriteInternal(byte[] buffer, int start, int count)
		{
			throw new NotImplementedException();
		}
	}
}
