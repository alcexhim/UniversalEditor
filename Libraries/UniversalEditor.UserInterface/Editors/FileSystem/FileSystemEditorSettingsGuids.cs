//
//  FileSystemEditorSettingsGuids.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.Editors.FileSystem
{
	public class FileSystemEditorSettingsGuids
	{
		public static Guid SingleClickToOpenItems { get; } = new Guid("{409C4308-BA99-489F-BD33-4122E430709D}");
		public static Guid InvalidPathChars { get; } = new Guid("{2fd5348a-2a74-4cdf-9f07-43011b109bde}");
		public static Guid InvalidFileNames { get; } = new Guid("{bfc2323b-a628-419c-827b-fed169ce176e}");
	}
}
