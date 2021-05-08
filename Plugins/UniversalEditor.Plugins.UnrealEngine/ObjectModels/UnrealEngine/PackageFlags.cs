//
//  PackageFlags.cs - indicates attributes for an Unreal Engine package file
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

using System;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
	/// <summary>
	/// Indicates attributes for an Unreal Engine package file.
	/// </summary>
	[Flags()]
	public enum PackageFlags
	{
		None = 0,
		/// <summary>
		/// The package is allowed to be downloaded to clients freely.
		/// </summary>
		AllowDownload = 0x0001,
		/// <summary>
		/// All objects in the package are optional (i.e. skins, textures) and it's up to the client whether he wants
		/// to download them or not. Not yet implemented; currently ignored.
		/// </summary>
		ClientOptional = 0x0002,
		/// <summary>
		/// This package is only needed on the server side, and the client shouldn't be informed of its presence. This
		/// is used with packages like IpDrv so that it can be updated frequently on the server side without requiring
		/// downloading stuff to the client.
		/// </summary>
		ServerSideOnly = 0x0004,
		/// <summary>
		/// Loaded from linker with broken import links
		/// </summary>
		BrokenLinks = 0x0008,
		/// <summary>
		/// Not trusted
		/// </summary>
		Untrusted = 0x0010,
		/// <summary>
		/// Client needs to download this package
		/// </summary>
		Required = 0x8000
	}
}
