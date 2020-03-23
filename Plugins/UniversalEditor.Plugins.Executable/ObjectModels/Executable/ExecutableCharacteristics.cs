using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// A set of bit flags indicating attributes of the file.
	/// </summary>
	[Flags()]
	public enum ExecutableCharacteristics : ushort
	{
		/// <summary>
		/// No characteristics defined.
		/// </summary>
		None = 0,
		/// <summary>
		/// Relocation information stripped from a file.
		/// </summary>
		RelocationInformationStripped = 0x0001,
		/// <summary>
		/// The file is executable.
		/// </summary>
		ExecutableImage = 0x0002,
		/// <summary>
		/// Line numbers stripped from file.
		/// </summary>
		LineNumbersStripped = 0x0004,
		/// <summary>
		/// Local symbols stripped from file.
		/// </summary>
		LocalSymbolsStripped = 0x0008,
		/// <summary>
		/// Lets the OS aggressively trim the working set.
		/// </summary>
		AggressiveWorkingSetTrim = 0x0010,
		/// <summary>
		/// Lets the OS aggressively trim the working set.
		/// </summary>
		MinimalObject = 0x0010,
		/// <summary>
		/// The application can handle addresses greater than two gigabytes.
		/// </summary>
		UpdateObject = 0x0020,
		/// <summary>
		/// The application can handle addresses greater than two gigabytes.
		/// </summary>
		LargeAddressAware = 0x0020,
		/// <summary>
		/// This requires a 32-bit word machine.
		/// </summary>
		Require32BitWord = 0x0100,
		/// <summary>
		/// Debug information is stripped to a .DBG file.
		/// </summary>
		DebugStripped = 0x0200,
		/// <summary>
		/// If the image is on removable media, copy to and run from the swap file.
		/// </summary>
		RemovableRunFromSwap = 0x0400,
		/// <summary>
		/// If the image is on a network, copy to and run from the swap file.
		/// </summary>
		NetworkRunFromSwap = 0x0800,
		/// <summary>
		/// File is a system file.
		/// </summary>
		IsSystemFile = 0x1000,
		/// <summary>
		/// File is a DLL.
		/// </summary>
		IsDynamicLinkLibrary = 0x2000,
		/// <summary>
		/// The file should only be run on single-processor machines.
		/// </summary>
		UniprocessorOnly = 0x4000,
		/// <summary>
		/// Bytes of machine word are reversed
		/// </summary>
		ReverseByteOrder = 0x8000
	}
}
