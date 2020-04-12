//
//  MicrosoftRegistryKeyAccess.cs - access bits used to track when key nodes are being accessed
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
	/// Access bits used to track when key nodes are being accessed (e.g. via the
	/// RegCreateKeyEx() and RegOpenKeyEx() calls)
	/// </summary>
	public enum MicrosoftRegistryKeyNodeAccess : byte
	{
		None = 0x0,
		/// <summary>
		/// This key was accessed before a Windows registry was initialized with the
		/// NtInitializeRegistry() routine during the boot
		/// </summary>
		PreInitialize = 0x1,
		/// <summary>
		/// This key was accessed after a Windows registry was initialized with the
		/// NtInitializeRegistry() routine during the boot
		/// </summary>
		PostInitialize = 0x2
	}
}
