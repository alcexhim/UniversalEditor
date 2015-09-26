using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.BPlus.Internal
{
	internal struct DIRECTORYINDEXENTRY
	{
		/// <summary>
		/// Varying-length NUL-terminated string.
		/// </summary>
		public string FileName;
		/// <summary>
		/// Page number of page dealing with FileName and above.
		/// </summary>
		public short PageNumber;
	}
}
