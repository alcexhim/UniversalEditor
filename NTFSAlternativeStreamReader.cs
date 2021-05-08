// one line to give the program's name and an idea of what it does.
// Copyright (C) yyyy  name of author
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using System.Runtime.InteropServices;

namespace ExtensibleDataStorage
{
	/// <summary>
	/// Description of NTFSAlternativeStreamReader.
	/// </summary>
	public class NTFSAlternativeStreamReader
	{
		/*
		[DllImport("ntoskrnl.exe")]
		private static extern int NtQueryInformationFile(IntPtr handle, ref IO_STATUS_BLOCK IoStatusBlock, ref FILE_STREAM_INFORMATION[] FileInformation, ulong length, FILE_INFORMATION_CLASS FileInformationClass);
		[DllImport("kernel32.dll")]
		private static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, LPSECURITY_ATTRIBUTES lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);
		*/

		public static string[] EnumerateStreamNames(string path)
		{
			string[] val = new string[] { };
			if (System.Environment.OSVersion.Platform == PlatformID.MacOSX)
			{

			}
			else if (System.Environment.OSVersion.Platform == PlatformID.Unix)
			{

			}
			else if (System.Environment.OSVersion.Platform == PlatformID.Xbox)
			{

			}
			else
			{
				/*
				// Open a file and obtain stream information
				PFILE_STREAM_INFORMATION[] pStreamInfo = new FILE_STREAM_INFORMATION[] { };
				IO_STATUS_BLOCK ioStatus;

				HANDLE hFile = CreateFile(szPath, 0, FILE_SHARE_READ | FILE_SHARE_WRITE,
	                            NULL, OPEN_EXISTING, 0, NULL);
				NtQueryInformationFile(hFile, ref ioStatus, ref pStreamInfo, sizeof(FILE_STREAM_INFORMATION), FILE_INFORMATION_CLASS.FileStreamInformation);
				CloseHandle(hFile);

				while(true)
				{
					// Get null-terminated stream name
					string szStreamName = pStreamInfo.StreamName;

					if (pStreamInfo.NextEntryOffset == 0) break;   // No more stream records
				}
				*/
			}
			return val;
		}
	}
}
