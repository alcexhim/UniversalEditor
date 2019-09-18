
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
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
