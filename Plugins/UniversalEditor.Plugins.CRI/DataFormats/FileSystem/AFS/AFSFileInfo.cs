//
//  AFSFileInfo.cs - internal structure representing metadata for files in an AFS archive
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.AFS
{
	/// <summary>
	/// Internal structure representing metadata for files in an AFS archive.
	/// </summary>
	internal struct AFSFileInfo
	{
		public string name;
		public uint offset;
		public DateTime datetime;
		public uint length;
		public uint length2;

		public override string ToString()
		{
			return String.Format("{0} : {1} [{2}]", name, offset, length);
		}
	}
}
