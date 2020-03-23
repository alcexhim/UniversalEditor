using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.PreferredExecutable
{
	public enum PEFSharingOption : byte
	{
		/// <summary>
		/// Indicates that the section is shared within a process, but a fresh copy is created for different processes.
		/// </summary>
		Process = 1,
		/// <summary>
		/// Indicates that the section is shared between all processes in the system.
		/// </summary>
		Global = 4,
		/// <summary>
		/// Indicates that the section is shared between all processes, but is protected. Protected sections are read/write in privileged mode and read-only in user mode. This option is not
		/// available in System 7.
		/// </summary>
		Protected = 5
	}
}
