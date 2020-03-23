//
//  AFSFormatVersion.cs
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
namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.AFS
{
	/// <summary>
	/// The version of AFS archive being handled by a <see cref="AFSDataFormat" /> instance.
	/// </summary>
	public enum AFSFormatVersion
	{
		/// <summary>
		/// Older version of AFS, which stores file data and TOC information in the same AFS file.
		/// </summary>
		AFS0,
		/// <summary>
		/// Newer version of AFS, which stores file data in an AWB file and writes the TOC to a separate ACB file.
		/// </summary>
		AFS2
	}
}
