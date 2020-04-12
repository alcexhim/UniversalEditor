//
//  IFileSystemObjectType.cs - indicates the type of IFileSystemObject to return in a GetAllObjects() call
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

namespace UniversalEditor.ObjectModels.FileSystem
{
	/// <summary>
	/// Indicates the type of <see cref="IFileSystemObject"/> to return in a GetAllObjects() call.
	/// </summary>
	public enum IFileSystemObjectType
	{
		/// <summary>
		/// Do not return any <see cref="IFileSystemObject" /> instances.
		/// </summary>
		None = 0,
		/// <summary>
		/// Return only <see cref="IFileSystemObject" /> instances that are of type <see cref="Folder" />.
		/// </summary>
		Folder = 1,
		/// <summary>
		/// Return only <see cref="IFileSystemObject" /> instances that are of type <see cref="File" />.
		/// </summary>
		File = 2,
		/// <summary>
		/// Return <see cref="IFileSystemObject" /> instances that are either of type <see cref="Folder" /> or <see cref="File" />.
		/// </summary>
		All = Folder | File
	}
}
