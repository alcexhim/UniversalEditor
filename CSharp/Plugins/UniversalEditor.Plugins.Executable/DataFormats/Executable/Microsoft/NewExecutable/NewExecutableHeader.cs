using System;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.Executable.Microsoft.NewExecutable
{
	public struct NewExecutableHeader
	{
		/// <summary>
		/// The major linker version
		/// </summary>
		public byte MajorLinkerVersion;
		/// <summary>
		/// The minor linker version
		/// </summary>
		public byte MinorLinkerVersion;
		/// <summary>
		/// Offset of entry table, see below
		/// </summary>
		public ushort EntryTableOffset;
		/// <summary>
		/// Length of entry table in bytes
		/// </summary>
		public ushort EntryTableLength;
		public uint FileLoadCRC;
		/// <summary>
		/// Program flags
		/// </summary>
		public NewExecutableProgramFlags ProgramFlags;
		/// <summary>
		/// Application flags
		/// </summary>
		public NewExecutableApplicationFlags ApplicationFlags;
		/// <summary>
		/// The index of the automatic data segment.
		/// </summary>
		public byte AutomaticDataSegmentIndex;
		/// <summary>
		/// The initial size of the local heap.
		/// </summary>
		public ushort InitialLocalHeapSize;
		/// <summary>
		/// The initial size of the stack.
		/// </summary>
		public ushort InitialStackSize;
		/// <summary>
		/// CS:IP entry point, CS is index into segment table
		/// </summary>
		public uint EntryPoint;
		/// <summary>
		/// SS:SP initial stack pointer, SS is index into segment table
		/// </summary>
		public uint InitialStackPointer;
		/// <summary>
		/// Number of segments in segment table
		/// </summary>
		public ushort SegmentCount;
		/// <summary>
		/// Number of module references (DLLs)
		/// </summary>
		public ushort ModuleReferenceCount;
		/// <summary>
		/// Size of non-resident names table, in bytes (Please clarify non-resident names table)
		/// </summary>
		public ushort NonResidentNamesTableSize;
		/// <summary>
		/// The segment table offset.
		/// </summary>
		public ushort SegmentTableOffset;
		/// <summary>
		/// The resources table offset.
		/// </summary>
		public ushort ResourcesTableOffset;
		/// <summary>
		/// The resident names table offset.
		/// </summary>
		public ushort ResidentNamesTableOffset;
		/// <summary>
		/// The module reference table offset.
		/// </summary>
		public ushort ModuleReferenceTableOffset;
		/// <summary>
		/// The imported names table offset (array of counted strings, terminated with string of length 0)
		/// </summary>
		public ushort ImportedNamesTableOffset;
		/// <summary>
		/// Offset from start of file to non-resident names table
		/// </summary>
		public uint NonResidentNamesTableOffset;
		/// <summary>
		/// Count of moveable entry point listed in entry table
		/// </summary>
		public ushort MovableEntryPointCount;
		/// <summary>
		/// File alignment size shift count (0=9(default 512 byte pages))
		/// </summary>
		public ushort FileAlignmentSizeShiftCount;
		/// <summary>
		/// Number of resources table entries
		/// </summary>
		public ushort ResourcesTableEntryCount;
		/// <summary>
		/// Target operating system
		/// </summary>
		public NewExecutableTargetOperatingSystem TargetOperatingSystem;
		/// <summary>
		/// other EXE flags
		/// </summary>
		public NewExecutableOS2Flags OS2EXEFlags;
		/// <summary>
		/// Offset to return thunks or start of gangload area - what is gangload?
		/// </summary>
		public ushort ReturnThunksOrGangloadAreaOffset;
		/// <summary>
		/// The segment reference thunks offset or size of the gangload area.
		/// </summary>
		public ushort SegmentReferenceThunksOffsetOrGangloadAreaSize;
		/// <summary>
		/// The minimum size of the code swap area.
		/// </summary>
		public ushort MinimumCodeSwapAreaSize;
		/// <summary>
		/// The expected minor Windows version.
		/// </summary>
		public byte ExpectedMinorWindowsVersion;
		/// <summary>
		/// The expected major Windows version.
		/// </summary>
		public byte ExpectedMajorWindowsVersion;

		public static NewExecutableHeader Read(Reader reader)
		{
			NewExecutableHeader neh = new NewExecutableHeader();
			neh.MajorLinkerVersion = reader.ReadByte();
			neh.MinorLinkerVersion = reader.ReadByte();
			neh.EntryTableOffset = reader.ReadUInt16();
			neh.EntryTableLength = reader.ReadUInt16();
			neh.FileLoadCRC = reader.ReadUInt32();
			neh.ProgramFlags = (NewExecutableProgramFlags)reader.ReadByte();
			neh.ApplicationFlags = (NewExecutableApplicationFlags)reader.ReadByte();
			neh.AutomaticDataSegmentIndex = reader.ReadByte();
			neh.InitialLocalHeapSize = reader.ReadUInt16();
			neh.InitialStackSize = reader.ReadUInt16();
			neh.EntryPoint = reader.ReadUInt32();
			neh.InitialStackPointer = reader.ReadUInt32();
			neh.SegmentCount = reader.ReadUInt16();
			neh.ModuleReferenceCount = reader.ReadUInt16();
			neh.NonResidentNamesTableSize = reader.ReadUInt16();
			neh.SegmentTableOffset = reader.ReadUInt16();
			neh.ResourcesTableOffset = reader.ReadUInt16();
			neh.ResidentNamesTableOffset = reader.ReadUInt16();
			neh.ModuleReferenceTableOffset = reader.ReadUInt16();
			neh.ImportedNamesTableOffset = reader.ReadUInt16();
			neh.NonResidentNamesTableOffset = reader.ReadUInt32();
			neh.MovableEntryPointCount = reader.ReadUInt16();
			neh.FileAlignmentSizeShiftCount = reader.ReadUInt16();
			neh.ResourcesTableEntryCount = reader.ReadUInt16();
			neh.TargetOperatingSystem = (NewExecutableTargetOperatingSystem)reader.ReadByte();
			neh.OS2EXEFlags = (NewExecutableOS2Flags)reader.ReadByte();
			neh.ReturnThunksOrGangloadAreaOffset = reader.ReadUInt16();
			neh.SegmentReferenceThunksOffsetOrGangloadAreaSize = reader.ReadUInt16();
			neh.MinimumCodeSwapAreaSize = reader.ReadUInt16();
			neh.ExpectedMinorWindowsVersion = reader.ReadByte();
			neh.ExpectedMajorWindowsVersion = reader.ReadByte();
			return neh;
		}
	}
}
