//
//  SilentHillARCDataFormat.cs - provides a DataFormat for manipulating archives in Silent Hill ARC format
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

namespace UniversalEditor.DataFormats.FileSystem.SilentHill.ARC
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Silent Hill ARC format.
	/// </summary>
	public class SilentHillARCDataFormat
	{
		private static string[] mvarFileNameList = new string[0];
		public static int GetHashFromFileName(string FileName)
		{
			int hash = 0;
			FileName = FileName.ToLower();
			for (int i = 0; i < FileName.Length; i++)
			{
				hash *= 33;
				hash ^= (int)(FileName[i]);
			}
			return hash;
		}
		public static string GetFileNameFromHash(int HashValue)
		{
			foreach (string fileName in mvarFileNameList)
			{
				if (GetHashFromFileName(fileName) == HashValue) return fileName;
			}
			return null;
		}

	}
}
