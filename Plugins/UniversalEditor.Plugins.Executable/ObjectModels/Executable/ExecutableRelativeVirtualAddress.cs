//
//  ExecutableRelativeVirtualAddress.cs - represents a Relative Virtual Address (RVA) in an executable
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

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// Represents a Relative Virtual Address (RVA) in an executable.
	/// </summary>
	public class ExecutableRelativeVirtualAddress : ICloneable
	{
		public class ExecutableRelativeVirtualAddressCollection
			: System.Collections.ObjectModel.Collection<ExecutableRelativeVirtualAddress>
		{
		}

		private ushort mvarDataDirectoryVirtualAddress = 0;
		public ushort DataDirectoryVirtualAddress { get { return mvarDataDirectoryVirtualAddress; } set { mvarDataDirectoryVirtualAddress = value; } }

		private ushort mvarDataDirectorySize = 0;
		public ushort DataDirectorySize { get { return mvarDataDirectorySize; } set { mvarDataDirectorySize = value; } }

		public object Clone()
		{
			ExecutableRelativeVirtualAddress rva = new ExecutableRelativeVirtualAddress();
			rva.DataDirectorySize = mvarDataDirectorySize;
			rva.DataDirectoryVirtualAddress = mvarDataDirectoryVirtualAddress;
			return rva;
		}
	}
}
