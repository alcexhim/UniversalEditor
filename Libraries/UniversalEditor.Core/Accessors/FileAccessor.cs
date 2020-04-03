//
//  FileAccessor.cs - provide an Accessor for reading from/writing to a file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;

namespace UniversalEditor.Accessors
{
	/// <summary>
	/// Provides an <see cref="Accessor"/> for reading from/writing to a file on the local file system.
	/// </summary>
	public class FileAccessor : Accessor
	{
		private static AccessorReference _ar = null;
		protected override AccessorReference MakeReferenceInternal()
		{
			if (_ar == null)
			{
				_ar = base.MakeReferenceInternal();
				_ar.Title = "Local file";

				_ar.ImportOptions.Add(new CustomOptionFile(nameof(FileName), "_File name:"));
				_ar.ImportOptions.Add(new CustomOptionBoolean(nameof(ForceOverwrite), "Force over_write if file exists", false, false, false));
				_ar.ImportOptions.Add(new CustomOptionBoolean(nameof(AllowWrite), "Open file for writing", false, false, false));

				CustomOptionFile cofExportFileName = new CustomOptionFile(nameof(FileName), "_File name:");
				cofExportFileName.DialogMode = CustomOptionFileDialogMode.Save;
				_ar.ExportOptions.Add(cofExportFileName);

				_ar.ExportOptions.Add(new CustomOptionBoolean(nameof(ForceOverwrite), "Force over_write if file exists", true, true));
				_ar.ExportOptions.Add(new CustomOptionBoolean(nameof(AllowWrite), "Open file for writing", true, false, false));
			}
			return _ar;
		}

		public override string GetFileTitle()
		{
			return System.IO.Path.GetFileName(mvarFileName);
		}
		public override string GetFileName()
		{
			return mvarFileName;
		}
		protected override long GetPosition()
		{
			return mvarFileStream.Position;
		}
		public override long Length
		{
			get { return mvarFileStream.Length; }
			set { mvarFileStream.SetLength(value); }
		}

		public override void Seek(long length, SeekOrigin origin)
		{
			mvarFileStream.Seek(length, (System.IO.SeekOrigin)origin);
		}

		protected internal override int ReadInternal(byte[] buffer, int offset, int count)
		{
			int length = mvarFileStream.Read(buffer, offset, count);
			return length;
		}

		protected internal override int WriteInternal(byte[] buffer, int offset, int count)
		{
			mvarFileStream.Write(buffer, offset, count);
			return count;
		}

		protected override void FlushInternal()
		{
			mvarFileStream.Flush();
		}

		private System.IO.FileStream mvarFileStream = null;

		private bool mvarAllowWrite = false;
		public bool AllowWrite { get { return mvarAllowWrite; } set { mvarAllowWrite = value; } }

		private bool mvarForceOverwrite = false;
		public bool ForceOverwrite { get { return mvarForceOverwrite; } set { mvarForceOverwrite = value; } }

		private string mvarFileName = String.Empty;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		public FileAccessor()
		{
		}
		public FileAccessor(string FileName, bool AllowWrite = false, bool ForceOverwrite = false, bool AutoOpen = false)
		{
			mvarFileName = FileName;
			mvarAllowWrite = AllowWrite;
			mvarForceOverwrite = ForceOverwrite;

			if (AutoOpen)
			{
				Open();
			}
		}

		public void Open(string FileName)
		{
			mvarFileName = FileName;
			OpenInternal();
		}
		protected override void OpenInternal()
		{
			string filename = mvarFileName;
			if (filename.StartsWith("file://"))
			{
				Uri uri = new Uri(filename);
				filename = uri.LocalPath;
			}

			System.IO.FileShare share = System.IO.FileShare.Read;
			System.IO.FileMode mode = System.IO.FileMode.OpenOrCreate;
			System.IO.FileAccess access = System.IO.FileAccess.Read;
			if (mvarAllowWrite)
			{
				access = System.IO.FileAccess.ReadWrite;
				if (mvarForceOverwrite)
				{
					mode = System.IO.FileMode.Create;
				}
			}
			mvarFileStream = System.IO.File.Open(filename, mode, access, share);
		}

		protected override void CloseInternal()
		{
			mvarFileStream.Close();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			// sb.Append("file:///");
			sb.Append(mvarFileName);
			return sb.ToString();
		}
	}
}
