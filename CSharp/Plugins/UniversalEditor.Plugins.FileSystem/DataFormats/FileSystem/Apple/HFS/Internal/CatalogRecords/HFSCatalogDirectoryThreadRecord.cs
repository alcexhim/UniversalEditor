using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal.CatalogRecords
{
	internal class HFSCatalogDirectoryThreadRecord : HFSCatalogRecord
	{
		public int[/*2*/] reserved2;
		/// <summary>
		/// Parent ID for this directory.
		/// </summary>
		public int parentID;
		/// <summary>
		/// File name of this directory.
		/// </summary>
		public string fileName;
	}
}
