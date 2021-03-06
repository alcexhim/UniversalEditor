//
//  MicrosoftRegistryKeyValueDataType.cs - indicates the data type of a registry key value in a Microsoft registry file
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

namespace UniversalEditor.DataFormats.PropertyList.Registry
{
	/// <summary>
	/// Indicates the data type of a registry key value in a Microsoft registry file.
	/// </summary>
	public enum MicrosoftRegistryKeyValueDataType : int
	{
		None = 0x00000000,
		String = 0x00000001,
		ExpandString = 0x00000002,
		Binary = 0x00000003,
		DoubleWord = 0x00000004,
		DoubleWordBigEndian = 0x00000005,
		Link = 0x00000006,
		MultiString = 0x00000007,
		ResourceList = 0x00000008,
		FullResourceDescriptor = 0x00000009,
		ResourceRequirementsList = 0x0000000A,
		QuadWord = 0x0000000B
	}
}
