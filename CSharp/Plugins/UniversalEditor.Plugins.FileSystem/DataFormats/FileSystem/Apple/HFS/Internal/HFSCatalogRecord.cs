using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	internal enum HFSCatalogRecordType : short
	{
		/// <summary>
		/// HFS+ folder record
		/// </summary>
		ExtendedDirectory = 0x0001,
		/// <summary>
		/// HFS+ file record
		/// </summary>
		ExtendedFile = 0x0002,
		/// <summary>
		/// HFS+ folder thread record
		/// </summary>
		ExtendedDirectoryThread = 0x0003,
		/// <summary>
		/// HFS+ file thread record
		/// </summary>
		ExtendedFileThread = 0x0004,
		/// <summary>
		/// HFS folder record
		/// </summary>
		Directory = 0x0100,
		/// <summary>
		/// HFS file record
		/// </summary>
		File = 0x0200,
		/// <summary>
		/// HFS folder thread record
		/// </summary>
		DirectoryThread = 0x0300,
		/// <summary>
		/// HFS file thread record
		/// </summary>
		FileThread = 0x0400
	}
	internal class HFSCatalogRecord
	{
		public HFSCatalogRecordType type;
	}
}
