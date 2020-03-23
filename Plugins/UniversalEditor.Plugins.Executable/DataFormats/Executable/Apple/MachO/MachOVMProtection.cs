using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	[Flags()]
	public enum MachOVMProtection : uint
	{
		None = 0x00,
		/// <summary>
		/// Read permission
		/// </summary>
		Read = 0x01,
		/// <summary>
		/// Write permission
		/// </summary>
		Write = 0x02,
		/// <summary>
		/// Execute permission
		/// </summary>
		Execute = 0x04,

		/// <summary>
		/// The default protection for newly created virtual memory
		/// </summary>
		Default = (Read | Write | Execute),
		
		/// <summary>
		/// Maximum privileges possible, for parameter checking.
		/// </summary>
		All = (Read | Write | Execute)
	}
}
