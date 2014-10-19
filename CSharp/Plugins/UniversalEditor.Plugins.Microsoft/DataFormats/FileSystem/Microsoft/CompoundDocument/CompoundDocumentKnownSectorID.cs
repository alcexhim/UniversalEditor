using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument
{
	public enum CompoundDocumentKnownSectorID
	{
		/// <summary>
		/// Free sector; may exist in the file, but is not part of any stream.
		/// </summary>
		Free = -1,
		/// <summary>
		/// Trailing section ID in a section ID chain.
		/// </summary>
		EndOfChain = -2,
		/// <summary>
		/// Sector is used by the Sector Allocation Table (SAT)
		/// </summary>
		SectorAllocationTable = -3,
		/// <summary>
		/// Sector is used by the Master Sector Allocation Table (MSAT)
		/// </summary>
		MasterSectionAllocationTable = -4
	}
}
