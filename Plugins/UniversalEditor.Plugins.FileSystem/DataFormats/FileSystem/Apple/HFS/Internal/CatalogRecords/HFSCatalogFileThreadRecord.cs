using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal.CatalogRecords
{
	internal class HFSCatalogFileThreadRecord : HFSCatalogRecord
	{
		public int[/*2*/] reserved2;
		/// <summary>
		/// Parent ID for this file.
		/// </summary>
		public int parentID;
		/// <summary>
		/// File name of this file.
		/// </summary>
		public string fileName;
	}
}
