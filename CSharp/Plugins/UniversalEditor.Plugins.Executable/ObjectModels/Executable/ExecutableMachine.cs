using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// The target CPU for this executable.
	/// </summary>
	public enum ExecutableMachine : ushort
	{
		/// <summary>
		/// Machine type unknown
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// Intel 386 (i386) 32-bit machine
		/// </summary>
		Intel386 = 0x014C,
		/// <summary>
		/// Intel 860
		/// </summary>
		Intel860 = 0x014D,
		/// <summary>
		/// MIPS little-endian, 0540 big-endian
		/// </summary>
		R3000 = 0x0162,
		/// <summary>
		/// MIPS little-endian
		/// </summary>
		R4000 = 0x0166,
		/// <summary>
		/// MIPS little-endian
		/// </summary>
		R10000 = 0x0168,
		/// <summary>
		/// Alpha_AXP 32-bit
		/// </summary>
		AlphaAXP32 = 0x0184,
		/// <summary>
		/// IBM PowerPC Little-Endian
		/// </summary>
		PowerPCLittleEndian = 0x01F0,
		/// <summary>
		/// IBM PowerPC Big-Endian
		/// </summary>
		PowerPCBigEndian = 0x01F2,
		/// <summary>
		/// SH3 little-endian
		/// </summary>
		SH3 = 0x01A2,
		/// <summary>
		/// SH3E little-endian
		/// </summary>
		SH3E = 0x01A4,
		/// <summary>
		/// SH4 little-endian
		/// </summary>
		SH4 = 0x01A6,
		/// <summary>
		/// ARM little-endian
		/// </summary>
		ARM = 0x01C0,
		/// <summary>
		/// Thumb
		/// </summary>
		Thumb = 0x01C2,
		/// <summary>
		/// Intel 64 (ia64) Itanium 64-bit machine
		/// </summary>
		Intel64Bit = 0x0200,
		/// <summary>
		/// MIPS
		/// </summary>
		MIPS16 = 0x0266,
		/// <summary>
		/// Alpha_AXP 64-bit
		/// </summary>
		AlphaAXP64 = 0x0284,
		/// <summary>
		/// MIPS
		/// </summary>
		MIPSFPU = 0x0366,
		/// <summary>
		/// MIPS
		/// </summary>
		MIPSFPU16 = 0x0466,
		/// <summary>
		/// AMD64 64-bit machine
		/// </summary>
		AMD64 = 0x8664,
		/// <summary>
		/// CEF
		/// </summary>
		CEF = 0xC0EF
	}
}
