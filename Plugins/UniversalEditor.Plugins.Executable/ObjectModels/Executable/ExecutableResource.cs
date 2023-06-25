//
//  ExecutableResource.cs
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
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.ObjectModels.Executable
{
	public class ExecutableResource : ICloneable
	{
		public class ExecutableResourceCollection
			: System.Collections.ObjectModel.Collection<ExecutableResource>
		{

		}

		public long VirtualAddress { get; set; }
		public long Length { get; set; }
		public ExecutableResourceType ResourceType { get; set; }
		public ExecutableResourceIdentifier Identifier { get; set; }

		public FileSource Source { get; set; } = null;

		public object Clone()
		{
			ExecutableResource clone = new ExecutableResource();
			clone.VirtualAddress = VirtualAddress;
			clone.Length = Length;
			clone.ResourceType = ResourceType;
			clone.Identifier = Identifier;
			return clone;
		}
	}
}
