
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
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
