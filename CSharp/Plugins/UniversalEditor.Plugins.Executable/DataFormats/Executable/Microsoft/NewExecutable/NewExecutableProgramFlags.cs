using System;
namespace UniversalEditor.DataFormats.Executable.Microsoft.NewExecutable
{
	[Flags()]
	public enum NewExecutableProgramFlags : byte
	{
		None = 0x00,
		/// <summary>
		/// DGROUP type: single shared
		/// </summary>
		SingleSharedDGROUP = 0x01,
		/// <summary>
		/// DGROUP type: multiple (unshared)
		/// </summary>
		MultipleUnsharedDGROUP = 0x02,
		/// <summary>
		/// Global initialization.
		/// </summary>
		GlobalInitialization = 0x04,
		/// <summary>
		/// Protected mode only.
		/// </summary>
		ProtectedModeOnly = 0x08,
		/// <summary>
		/// 8086 instructions
		/// </summary>
		Instructions8086 = 0x10,
		/// <summary>
		/// 80286 instructions
		/// </summary>
		Instructions80286 = 0x20,
		/// <summary>
		/// 80386 instructions
		/// </summary>
		Instructions80386 = 0x40,
		/// <summary>
		/// 80x87 instructions
		/// </summary>
		Instructions80x87 = 0x80
	}
}
