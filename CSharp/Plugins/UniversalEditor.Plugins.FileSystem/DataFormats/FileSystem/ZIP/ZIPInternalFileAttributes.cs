using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	public enum ZIPInternalFileAttributes : short
	{
		None = 0x00,
		ApparentTextFile = 0x01,
		/// <summary>
		/// Control field records precede logical records
		/// </summary>
		ControlFieldRecordsPrecedeLogicalRecords = 0x04
	}
}
