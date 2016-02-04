using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;

namespace UniversalEditor.DataFormats.Executable.Apple.PreferredExecutable
{
	public class PEFDataFormat : DataFormat
	{
		private static Internal.PEFContainerHeader ReadPEFContainerHeader(Reader reader)
		{
			Internal.PEFContainerHeader item = new Internal.PEFContainerHeader();
			item.tag1 = reader.ReadFixedLengthString(4);
			if (item.tag1 != "Joy!") throw new InvalidDataFormatException("PEF container header does not have 'Joy!' in 'tag1' field");

			item.tag2 = reader.ReadFixedLengthString(4);
			if (item.tag2 != "peff") throw new InvalidDataFormatException("PEF container header does not have 'peff' in 'tag2' field");

			item.architecture = reader.ReadFixedLengthString(4);
			item.formatVersion = reader.ReadUInt32();
			item.dateTimeStamp = reader.ReadUInt32();
			item.oldDefVersion = reader.ReadUInt32();
			item.oldImpVersion = reader.ReadUInt32();
			item.currentVersion = reader.ReadUInt32();
			item.sectionCount = reader.ReadUInt16();
			item.instSectionCount = reader.ReadUInt16();
			item.reservedA = reader.ReadUInt32();
			return item;
		}

		private static Internal.PEFSectionHeader ReadPEFSectionHeader(Reader reader)
		{
			Internal.PEFSectionHeader item = new Internal.PEFSectionHeader();
			item.nameOffset = reader.ReadInt32();
			item.defaultAddress = reader.ReadUInt32();
			item.totalSize = reader.ReadUInt32();
			item.unpackedSize = reader.ReadUInt32();
			item.packedSize = reader.ReadUInt32();
			item.containerOffset = reader.ReadUInt32();
			item.sectionKind = (PEFSectionKind)reader.ReadByte();
			item.shareKind = (PEFSharingOption)reader.ReadByte();
			item.alignment = reader.ReadByte();
			item.reservedA = reader.ReadByte();
			return item;
		}

		private PEFArchitecture mvarArchitecture = PEFArchitecture.PowerPC;
		/// <summary>
		/// Indicates the architecture for which this container was generated.
		/// </summary>
		public PEFArchitecture Architecture { get { return mvarArchitecture; } set { mvarArchitecture = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			Reader reader = base.Accessor.Reader;

			reader.Endianness = Endianness.BigEndian;

			Internal.PEFContainerHeader header = ReadPEFContainerHeader(reader);
			switch (header.architecture)
			{
				case "pwpc":
				{
					mvarArchitecture = PEFArchitecture.PowerPC;
					break;
				}
				case "m68k":
				{
					mvarArchitecture = PEFArchitecture.Motorola68000;
					break;
				}
			}

			List<Internal.PEFSectionHeader> sectHeads = new List<Internal.PEFSectionHeader>();
			for (ushort i = 0; i < header.sectionCount; i++)
			{
				Internal.PEFSectionHeader sect = ReadPEFSectionHeader(reader);
				sectHeads.Add(sect);
			}
			foreach (Internal.PEFSectionHeader sectHead in sectHeads)
			{
				ExecutableSection section = new ExecutableSection();
				section.PhysicalAddress = sectHead.containerOffset;
				section.VirtualAddress = sectHead.containerOffset;
				section.VirtualSize = sectHead.packedSize;

				reader.Seek(sectHead.containerOffset, SeekOrigin.Begin);
				byte[] sectionData = reader.ReadBytes(sectHead.packedSize);
				section.Data = sectionData;

				exe.Sections.Add(section);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
