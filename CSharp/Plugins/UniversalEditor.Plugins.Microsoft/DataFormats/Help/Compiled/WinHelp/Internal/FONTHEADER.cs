using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	public struct FONTHEADER
	{
		/// <summary>
		/// Number of face names
		/// </summary>
		public ushort NumFacenames;
		/// <summary>
		/// Number of font descriptors
		/// </summary>
		public ushort NumDescriptors;
		/// <summary>
		/// Start of array of face names relative to &NumFacenames
		/// </summary>
		public ushort FacenamesOffset;
		/// <summary>
		/// Start of array of font descriptors relative to &NumFacenames
		/// </summary>
		public ushort DescriptorsOffset;

		// only if FacenamesOffset >= 12
		/// <summary>
		/// Number of style descriptors
		/// </summary>
		public ushort NumStyles;
		/// <summary>
		/// Start of array of style descriptors relative to &NumFacenames
		/// </summary>
		public ushort StyleOffset;

		// only if FacenamesOffset >= 16
		/// <summary>
		/// Number of character mapping tables
		/// </summary>
		public ushort NumCharMapTables;
		/// <summary>
		/// Start of array of character mapping table names relative to &NumFacenames
		/// </summary>
		public ushort CharMapTableOffset;
	}
}
