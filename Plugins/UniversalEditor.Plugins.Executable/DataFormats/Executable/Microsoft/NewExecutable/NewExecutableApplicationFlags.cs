using System;
namespace UniversalEditor.DataFormats.Executable.Microsoft.NewExecutable
{
	[Flags()]
	public enum NewExecutableApplicationFlags : byte
	{
		/// <summary>
		/// Not aware of Windows/Presentation Manager API
		/// </summary>
		FullScreen = 0x01,
		/// <summary>
		/// Compatible with Windows/Presentation Manager API
		/// </summary>
		PMCompatible = 0x02,
		/// <summary>
		/// Uses Windows/Presentation Manager API
		/// </summary>
		PMRequired = 0x03,

		/// <summary>
		/// Dual-mode (OS/2 and DOS) Family Application Program Interface (FAPI)
		/// </summary>
		FamilyApplication = 0x04,
		/// <summary>
		/// Executable image contains errors.
		/// </summary>
		ImageErrors = 0x10,
		/// <summary>
		/// non-conforming program (valid stack is not maintained)
		/// </summary>
		NonConforming = 0x20,
		/// <summary>
		/// DLL or driver rather than application
		/// (SS:SP info invalid, CS:IP points at FAR init routine called with AX = module handle which returns AX = 0000h on failure,
		/// AX nonzero on successful initialization)
		/// </summary>
		Library = 0x40
	}
}
