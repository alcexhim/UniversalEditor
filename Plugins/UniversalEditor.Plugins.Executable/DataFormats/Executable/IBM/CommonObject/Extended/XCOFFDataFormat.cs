
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	public abstract class XCOFFDataFormat : DataFormat
	{
		private ushort mvarSectionHeaderCount = 0;
		/// <value>
		/// Specifies the number of section headers contained in the file. The first section header is section header number one; all references to a section are one-based.
		/// </value>
		public ushort SectionHeaderCount { get { return mvarSectionHeaderCount; } set { mvarSectionHeaderCount = value; } }
		
		private DateTime mvarCreationDate = DateTime.Now;
		/// <value>
		///	Specifies when the file was created (number of elapsed seconds since 00:00:00 Universal Coordinated Time (UCT), January 1, 1970). This field should specify either the actual time or be set to a value of 0.
		/// </value>
		public DateTime CreationDate { get { return mvarCreationDate; } set { mvarCreationDate = value; } }
		
		private uint mvarSymbolicEntryCount = 0;
		/// <value>
		/// Specifies the number of entries in the symbol table. Each symbol table entry is 18 bytes long. 
		/// </value>
		public uint SymbolicEntryCount { get { return mvarSymbolicEntryCount; } set { mvarSymbolicEntryCount = value; } }
		
		private XCOFFDocumentFlags mvarFlags = XCOFFDocumentFlags.F_NONE;
		public XCOFFDocumentFlags Flags { get { return mvarFlags; } set { mvarFlags = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
