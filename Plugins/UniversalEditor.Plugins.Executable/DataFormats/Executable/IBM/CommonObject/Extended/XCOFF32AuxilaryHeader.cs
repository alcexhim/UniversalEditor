//
//  XCOFF32AuxilaryHeader.cs - describes the auxiliary header in a 32-bit Extended Common Object File Format executable
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

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	/// <summary>
	/// Describes the auxiliary header in a 32-bit Extended Common Object File Format executable.
	/// </summary>
	public class XCOFF32AuxilaryHeader : XCOFFAuxilaryHeaderBase
	{
		private uint mvarTextSize = 0;
		public uint TextSize { get { return mvarTextSize; } set { mvarTextSize = value; } }

		private uint mvarInitializedDataSize = 0;
		public uint InitializedDataSize { get { return mvarInitializedDataSize; } set { mvarInitializedDataSize = value; } }

		private uint mvarUninitializedDataSize = 0;
		public uint UninitializedDataSize { get { return mvarUninitializedDataSize; } set { mvarUninitializedDataSize = value; } }

		private uint mvarEntryPointDescriptor = 0;
		public uint EntryPointDescriptor { get { return mvarEntryPointDescriptor; } set { mvarEntryPointDescriptor = value; } }

		private uint mvarBaseAddressText = 0;
		public uint BaseAddressText { get { return mvarBaseAddressText; } set { mvarBaseAddressText = value; } }

		private uint mvarBaseAddressData = 0;
		public uint BaseAddressData { get { return mvarBaseAddressData; } set { mvarBaseAddressData = value; } }

		private uint mvarTOCAnchorAddress = 0;
		public uint TOCAnchorAddress { get { return mvarTOCAnchorAddress; } set { mvarTOCAnchorAddress = value; } }

		private ulong mvarReserved2 = 0;
		public ulong Reserved2 { get { return mvarReserved2; } set { mvarReserved2 = value; } }
	}
}
