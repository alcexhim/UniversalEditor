//
//  ExecutableResourceTypes.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
namespace UniversalEditor.ObjectModels.Executable
{
	public static class ExecutableResourceTypes
	{
		public static ExecutableResourceType Accelerator { get; } = new ExecutableResourceType("Accelerator");
		public static ExecutableResourceType AnimatedCursor { get; } = new ExecutableResourceType("Animated Cursor");
		public static ExecutableResourceType AnimatedIcon { get; } = new ExecutableResourceType("Animated Icon");
		public static ExecutableResourceType Bitmap { get; } = new ExecutableResourceType("Bitmap");
		public static ExecutableResourceType Cursor { get; } = new ExecutableResourceType("Cursor");
		public static ExecutableResourceType Dialog { get; } = new ExecutableResourceType("Dialog");
		public static ExecutableResourceType DialogInclude { get; } = new ExecutableResourceType("Dialog Include");
		public static ExecutableResourceType DialogInit { get; } = new ExecutableResourceType("Dialog Init");
		public static ExecutableResourceType Font { get; } = new ExecutableResourceType("Font");
		public static ExecutableResourceType FontDir { get; } = new ExecutableResourceType("Font Dir");
		public static ExecutableResourceType GroupCursor { get; } = new ExecutableResourceType("Group Cursor");
		public static ExecutableResourceType GroupIcon { get; } = new ExecutableResourceType("Group Icon");
		public static ExecutableResourceType HTML { get; } = new ExecutableResourceType("HTML");
		public static ExecutableResourceType Icon { get; } = new ExecutableResourceType("Icon");
		public static ExecutableResourceType Manifest { get; } = new ExecutableResourceType("Manifest");
		public static ExecutableResourceType Menu { get; } = new ExecutableResourceType("Menu");
		public static ExecutableResourceType MessageTable { get; } = new ExecutableResourceType("Message Table");
		public static ExecutableResourceType PlugAndPlay { get; } = new ExecutableResourceType("Plug and Play");
		public static ExecutableResourceType RCData { get; } = new ExecutableResourceType("RC Data");
		public static ExecutableResourceType String { get; } = new ExecutableResourceType("String");
		public static ExecutableResourceType Version { get; } = new ExecutableResourceType("Version");
		public static ExecutableResourceType VxD { get; } = new ExecutableResourceType("VxD");
	}
}
