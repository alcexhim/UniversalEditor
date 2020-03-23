using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.PreferredExecutable
{
	public enum PEFSectionKind : byte
	{
		/// <summary>
		/// Contains read-only executable code in an uncompressed binary format. A container can have any number of code sections. Code sections are always shared.
		/// </summary>
		Code = 0,
		/// <summary>
		/// Contains uncompressed, initialized, read/write data followed by zero-initialized read/write data. A container can have any number of data sections, each with a different sharing
		/// option.
		/// </summary>
		UnpackedData = 1,
		/// <summary>
		/// Contains read/write data initialized by a pattern specification contained in the section's contents. The contents essentially contain a small program that tells the Code Fragment
		/// Manager how to initialize the raw data in memory. A container can have any number of pattern-initialized data sections, each with its own sharing option. See "Pattern-Initialized
		/// Data" (page 8-10) for more information about creating pattern specifications.
		/// </summary>
		PatternInitializedData = 2,
		/// <summary>
		/// Contains uncompressed, initialized, read-only data. A container can have any number of constant sections, and they are implicitly shared.
		/// </summary>
		Constant = 3,
		/// <summary>
		/// Contains information about imports, exports, and entry points. See "The Loader Section" (page 8-15) for more details. A container can have only one loader section.
		/// </summary>
		Loader = 4,
		/// <summary>
		/// Reserved for future use.
		/// </summary>
		Debug = 5,
		/// <summary>
		/// Contains information that is both executable and modifiable. For example, this section can store code that contains embedded data. A container can have any number of executable data
		/// sections, each with a different sharing option.
		/// </summary>
		ExecutableData = 6,
		/// <summary>
		/// Reserved for future use.
		/// </summary>
		Exception = 7,
		/// <summary>
		/// Reserved for future use.
		/// </summary>
		Traceback = 8
	}
}
