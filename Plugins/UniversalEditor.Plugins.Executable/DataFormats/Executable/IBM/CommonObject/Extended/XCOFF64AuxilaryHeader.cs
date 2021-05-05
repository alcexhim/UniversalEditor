//
//  XCOFF64AuxilaryHeader.cs - describes the auxiliary header in a 64-bit Extended Common Object File Format executable
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
	public class XCOFF64AuxilaryHeader : XCOFFAuxilaryHeaderBase
	{
		private ulong mvarTextSize = 0;
		public ulong TextSize { get { return mvarTextSize; } set { mvarTextSize = value; } }

		private ulong mvarInitializedDataSize = 0;
		public ulong InitializedDataSize { get { return mvarInitializedDataSize; } set { mvarInitializedDataSize = value; } }

		private ulong mvarUninitializedDataSize = 0;
		public ulong UninitializedDataSize { get { return mvarUninitializedDataSize; } set { mvarUninitializedDataSize = value; } }

		private ulong mvarEntryPointDescriptor = 0;
		public ulong EntryPointDescriptor { get { return mvarEntryPointDescriptor; } set { mvarEntryPointDescriptor = value; } }

		private ulong mvarBaseAddressText = 0;
		public ulong BaseAddressText { get { return mvarBaseAddressText; } set { mvarBaseAddressText = value; } }

		private ulong mvarBaseAddressData = 0;
		public ulong BaseAddressData { get { return mvarBaseAddressData; } set { mvarBaseAddressData = value; } }

		private ulong mvarTOCAnchorAddress = 0;
		public ulong TOCAnchorAddress { get { return mvarTOCAnchorAddress; } set { mvarTOCAnchorAddress = value; } }

		private uint mvarReserved2 = 0;
		public uint Reserved2 { get { return mvarReserved2; } set { mvarReserved2 = value; } }

		private byte[] mvarReserved3 = new byte[116];
		public byte[] Reserved3 { get { return mvarReserved3; } set { mvarReserved3 = value; } }
	}
}
