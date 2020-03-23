//
//  MicrosoftRegistryHiveCellType.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
namespace UniversalEditor.DataFormats.PropertyList.Registry
{
	public enum MicrosoftRegistryHiveCellType : short
	{
		IndexLeaf = 26988,
		FastLeaf = 26220,
		HashLeaf = 26732,
		IndexRoot = 26994,
		KeyNode = 27502,
		KeyValue = 27510,
		KeySecurity = 27507,
		BigData = 25188
	}
}
