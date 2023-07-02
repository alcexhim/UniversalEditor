//
//  NTFSFileNameAttribute.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.FileSystem.Microsoft.NTFS.Attributes
{
	public class NTFSFileNameAttribute : NTFSAttribute
	{
		public DateTime CreationDateTime { get; set; }
		public DateTime ModificationDateTime { get; set; }
		public DateTime AccessDateTime { get; set; }

		public long AllocatedSize { get; set; }
		public long ActualSize { get; set; }

		public NTFSFileNameNamespace FileNameNamespace { get; set; } = NTFSFileNameNamespace.Windows;
		public string FileName { get; set; } = null;
	}
}
