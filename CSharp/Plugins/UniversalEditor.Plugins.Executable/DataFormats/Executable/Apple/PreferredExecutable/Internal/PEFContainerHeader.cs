using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.PreferredExecutable.Internal
{
	internal struct PEFContainerHeader
	{
		/// <summary>
		/// Designates that the container uses an Apple-defined format. This field must be set to "Joy!" in ASCII.
		/// </summary>
		public string tag1;
		/// <summary>
		/// Identifies the type of container (currently set to "peff" in ASCII).
		/// </summary>
		public string tag2;
		/// <summary>
		/// Indicates the architecture type that the container was generated for. This field holds the ASCII value "pwpc" for the
		/// PowerPC CFM implementation or "m68k" for CFM-68K.
		/// </summary>
		public string architecture;
		/// <summary>
		/// Indicates the version of PEF used in the container. The current version is 1.
		/// </summary>
		public uint formatVersion;
		/// <summary>
		/// Indicates when the PEF container was created. The stamp follows the Macintosh time-measurement scheme (that is, the number
		/// of seconds measured from January 1, 1904).
		/// </summary>
		public uint dateTimeStamp;
		/// <summary>
		/// Contains version information that the Code Fragment Manager uses to check shared library compatibility.
		/// </summary>
		public uint oldDefVersion;
		/// <summary>
		/// Contains version information that the Code Fragment Manager uses to check shared library compatibility.
		/// </summary>
		public uint oldImpVersion;
		/// <summary>
		/// Contains version information that the Code Fragment Manager uses to check shared library compatibility.
		/// </summary>
		public uint currentVersion;
		/// <summary>
		/// Indicates the total number of sections contained in the container.
		/// </summary>
		public ushort sectionCount;
		/// <summary>
		/// Indicates the number of instantiated sections. Instantiated sections contain code or data that are required for execution.
		/// </summary>
		public ushort instSectionCount;
		/// <summary>
		/// Currently reserved and must be set to 0.
		/// </summary>
		public uint reservedA;
	}
}
