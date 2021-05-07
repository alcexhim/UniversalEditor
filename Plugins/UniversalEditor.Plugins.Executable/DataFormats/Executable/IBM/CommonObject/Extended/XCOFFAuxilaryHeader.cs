//
//  XCOFFAuxilaryHeader.cs - describes the common attributes of an auxiliary header for both 32-bit and 64-bit Extended Common Object File Format executables
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
	/// Describes the common attributes of an auxiliary header for both 32-bit and 64-bit Extended Common Object File Format executables.
	/// </summary>
	public abstract class XCOFFAuxilaryHeaderBase
	{
		private bool mvarEnabled = false;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private uint mvarSize = 0;
		/// <value>
		/// Specifies the length, in bytes, of the auxiliary header. For an XCOFF file to be executable, the auxiliary header must exist and be _AOUTHSZ_EXEC bytes long. (_AOUTHSZ_EXEC is defined in aouthdr.h.)
		/// </value>
		public uint Size { get { return mvarSize; } set { mvarSize = value; } }

		private ushort mvarFlags = 0x010B;
		public ushort Flags { get { return mvarFlags; } set { mvarFlags = value; } }

		private ushort mvarVersion = 1;
		public ushort Version { get { return mvarVersion; } set { mvarVersion = value; } }

		private ushort mvarSectionNumberEntryPoint = 0;
		public ushort SectionNumberEntryPoint { get { return mvarSectionNumberEntryPoint; } set { mvarSectionNumberEntryPoint = value; } }

		private ushort mvarSectionNumberText = 0;
		public ushort SectionNumberText { get { return mvarSectionNumberText; } set { mvarSectionNumberText = value; } }

		private ushort mvarSectionNumberData = 0;
		public ushort SectionNumberData { get { return mvarSectionNumberData; } set { mvarSectionNumberData = value; } }

		private ushort mvarSectionNumberTOC = 0;
		public ushort SectionNumberTOC { get { return mvarSectionNumberTOC; } set { mvarSectionNumberTOC = value; } }

		private ushort mvarSectionNumberLoader = 0;
		public ushort SectionNumberLoader { get { return mvarSectionNumberLoader; } set { mvarSectionNumberLoader = value; } }

		private ushort mvarSectionNumberBSS = 0;
		public ushort SectionNumberBSS { get { return mvarSectionNumberBSS; } set { mvarSectionNumberBSS = value; } }

		private ushort mvarMaximumAlignmentText = 0;
		public ushort MaximumAlignmentText { get { return mvarMaximumAlignmentText; } set { mvarMaximumAlignmentText = value; } }

		private ushort mvarMaximumAlignmentData = 0;
		public ushort MaximumAlignmentData { get { return mvarMaximumAlignmentData; } set { mvarMaximumAlignmentData = value; } }

		private ushort mvarModuleType = 0;
		public ushort ModuleType { get { return mvarModuleType; } set { mvarModuleType = value; } }

		private byte mvarCPUFlag = 0;
		public byte CPUFlag { get { return mvarCPUFlag; } set { mvarCPUFlag = value; } }

		private byte mvarCPUType = 0;
		public byte CPUType { get { return mvarCPUType; } set { mvarCPUType = value; } }

		private uint mvarDebugger = 0;
		public uint Debugger { get { return mvarDebugger; } set { mvarDebugger = value; } }

	}
}
