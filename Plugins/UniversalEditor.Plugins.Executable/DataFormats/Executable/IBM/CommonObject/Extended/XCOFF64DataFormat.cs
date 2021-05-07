//
//  XCOFF64DataFormat.cs - provides a DataFormat for manipulating 64-bit Extended Common Object File Format executables
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
	/// Provides a <see cref="DataFormat" /> for manipulating 64-bit Extended Common Object File Format executables.
	/// </summary>
	public class XCOFF64DataFormat : XCOFFDataFormat
	{
		private ushort mvarMagicNumber = 0x01F7;
		/// <value>
		/// Specifies an integer known as the magic number, which specifies the target machine and environment of the object file. For XCOFF32, the only valid value is 0x01DF (0737 Octal). For XCOFF64 on AIX 4.3 and earlier, the only valid value is 0x01EF (0757 Octal). For XCOFF64 on AIX 5.1 and later, the only valid value is 0x01F7 (0767 Octal).
		/// </value>
		public ushort MagicNumber { get { return mvarMagicNumber; } set { mvarMagicNumber = value; } }

		private ulong mvarSymbolicPointer = 0;
		/// <value>
		/// Specifies a file pointer (byte offset from the beginning of the file) to the start of the symbol table. If the value of the f_nsyms field is 0, then this value is undefined.
		/// </value>
		public ulong SymbolicPointer { get { return mvarSymbolicPointer; } set { mvarSymbolicPointer = value; } }

		private XCOFF64AuxilaryHeader mvarAuxilaryHeader = new XCOFF64AuxilaryHeader();
		public XCOFF64AuxilaryHeader AuxilaryHeader { get { return mvarAuxilaryHeader; } }
	}
}
