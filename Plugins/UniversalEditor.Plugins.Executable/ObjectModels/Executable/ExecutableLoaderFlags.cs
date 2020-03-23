using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// Fields relating to debugging support
	/// </summary>
	public enum ExecutableLoaderFlags
	{
		/// <summary>
		/// No loader flags have been defined.
		/// </summary>
		None = 0,
		/// <summary>
		/// Invoke a breakpoint instruction before starting the process.
		/// </summary>
		BreakBeforeStart = 1,
		/// <summary>
		/// Invoke a debugger on the process after it's been loaded.
		/// </summary>
		DebugAfterLoad = 2
	}
}
