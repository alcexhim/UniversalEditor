// one line to give the program's name and an idea of what it does.
// Copyright (C) yyyy  name of author
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.ObjectModels.FileSystem;

#if EXECUTABLE_LOAD_RESOURCES
using UniversalEditor.DataFormats.Resource.Microsoft;
using UniversalEditor.ObjectModels.Resource;
using UniversalEditor.ObjectModels.Resource.Blocks;
#endif

namespace UniversalEditor.DataFormats.Executable.Microsoft
{
	/// <summary>
	/// Description of DOSExecutable.
	/// </summary>
	public class MicrosoftExecutableDataFormat : DataFormat
	{
		private DOSExecutableHeader mvarDOSHeader = new DOSExecutableHeader();
		public DOSExecutableHeader DOSHeader { get { return mvarDOSHeader; } }

		private NameValuePair<int>.NameValuePairCollection mvarImportTable = new NameValuePair<int>.NameValuePairCollection();
		public NameValuePair<int>.NameValuePairCollection ImportTable { get { return mvarImportTable; } }
		
		private System.Reflection.Assembly mvarCLRAssembly = null;
		public System.Reflection.Assembly CLRAssembly { get { return mvarCLRAssembly; } }

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
				// _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void AfterLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			FileAccessor fa = (base.Accessor as FileAccessor);
			if (fa != null)
			{
				try
				{
					mvarCLRAssembly = System.Reflection.Assembly.LoadFile(fa.FileName);
				}
				catch (BadImageFormatException)
				{
					mvarCLRAssembly = null;
				}
			}
		}

		/*
			C++ Name Mangling
Compiler						void h(int)				void h(int, char)		void h(void)
Intel C++ 8.0 for Linux 		_Z1hi 					_Z1hic 					_Z1hv
HP aC++ A.05.55 IA-64 			_Z1hi 					_Z1hic 					_Z1hv
IAR EWARM C++ 5.4 ARM 			_Z1hi 					_Z1hic 					_Z1hv
GCC 3.x and 4.x 				_Z1hi 					_Z1hic 					_Z1hv
GCC 2.9x						h__Fi 					h__Fic 					h__Fv
HP aC++ A.03.45 PA-RISC			h__Fi 					h__Fic 					h__Fv
Microsoft Visual C++ v6-v10		?h@@YAXH@Z				?h@@YAXHD@Z				?h@@YAXXZ
Digital Mars C++				?h@@YAXH@Z				?h@@YAXHD@Z				?h@@YAXXZ
Borland C++ v3.1				@h$qi					@h$qizc					@h$qv
OpenVMS C++ V6.5 (ARM mode)		H__XI					H__XIC					H__XV
OpenVMS C++ V6.5 (ANSI mode)	CXX$__7H__FI0ARG51T		CXX$__7H__FIC26CDH77	CXX$__7H__FV2CB06E8
OpenVMS C++ X7.1 IA-64			CXX$_Z1HI2DSQ26A		CXX$_Z1HIC2NP3LI4		CXX$_Z1HV0BCA19V
SunPro CC						__1cBh6Fi_v_			__1cBh6Fic_v_			__1cBh6F_v_
Tru64 C++ V6.5 (ARM mode)		h__Xi					h__Xic					h__Xv
Tru64 C++ V6.5 (ANSI mode)		__7h__Fi				__7h__Fic				__7h__Fv
Watcom C++ 10.6					W?h$n(i)v				W?h$n(ia)v				W?h$n()v

		*/

		private string DecodeMangledName(string name, PECompiler compiler)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			switch (compiler)
			{
				case PECompiler.IntelCPP80Linux:
				{
					// _Z1hi 					_Z1hic 					_Z1hv
					// void h(int)				void h(int, char)		void h(void)

					string _Z1 = name.Substring(0, 2);

					break;
				}
				case PECompiler.MsVCpp:
				case PECompiler.DigitalMars:
				{
					// ?h@@YAXH@Z				?h@@YAXHD@Z				?h@@YAXXZ
					// void h(int)				void h(int, char)		void h(void)

					string prefix = name.Substring(0, 1); // ?
					string funcName = name.Substring(1, name.IndexOf('@', 1) - 2); // h
					string suffix = name.Substring(1 + funcName.Length, 4); // @@YA
					//XH@Z
					break;
				}
			}
			return sb.ToString();
		}
		
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;

			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			ExecutableObjectModel exec = (objectModel as ExecutableObjectModel);
			if (fsom == null && exec == null) throw new ObjectModelNotSupportedException("Object model must be a FileSystem or an Executable");

			ExecutableObjectModel exe = new ExecutableObjectModel();
			uint e_lfanew = 0;
			#region DOS part
			{
				string signature = br.ReadFixedLengthString(2); // 0x4d, 0x5a. This is the "magic number" of an EXE file. The first byte of the file is 0x4d and the second is 0x5a.
				if (signature != "MZ") throw new InvalidDataFormatException("File does not begin with \"MZ\"");
				mvarDOSHeader.LastBlockLength = br.ReadUInt16(); // The number of bytes in the last block of the program that are actually used. If this value is zero, that means the entire last block is used (i.e. the effective value is 512).
				mvarDOSHeader.NumBlocksInEXE = br.ReadUInt16(); // Number of blocks in the file that are part of the EXE file. If mvarDOSHeader.LastBlockLength is non-zero, only that much of the last block is used.
				mvarDOSHeader.NumRelocEntriesAfterHeader = br.ReadUInt16(); // Number of relocation entries stored after the header. May be zero.
				mvarDOSHeader.NumParagraphsInHeader = br.ReadUInt16(); // Number of paragraphs in the header. The program's data begins just after the header, and this field can be used to calculate the appropriate file offset. The header includes the relocation entries. Note that some OSs and/or programs may fail if the header is not a multiple of 512 bytes.
				mvarDOSHeader.NumParagraphsAdditionalMemory = br.ReadUInt16(); // Number of paragraphs of additional memory that the program will need. This is the equivalent of the BSS size in a Unix program. The program can't be loaded if there isn't at least this much memory available to it.
				mvarDOSHeader.NumMaxParagraphsAdditionalMemory = br.ReadUInt16(); // Maximum number of paragraphs of additional memory. Normally, the OS reserves all the remaining conventional memory for your program, but you can limit it with this field.
				mvarDOSHeader.RelativeStackSegmentValue = br.ReadUInt16(); // Relative value of the stack segment. This value is added to the segment the program was loaded at, and the result is used to initialize the SS register.
				mvarDOSHeader.InitialValueRegisterSP = br.ReadUInt16(); // Initial value of the SP register.
				mvarDOSHeader.WordChecksum = br.ReadUInt16(); // Word checksum. If set properly, the 16-bit sum of all words in the file should be zero. Usually, this isn't filled in.
				mvarDOSHeader.InitialValueRegisterIP = br.ReadUInt16(); // Initial value of the IP register.
				mvarDOSHeader.InitialValueRegisterCS = br.ReadUInt16(); // Initial value of the CS register, relative to the segment the program was loaded at.
				mvarDOSHeader.FirstRelocationItemOffset = br.ReadUInt16(); // Offset of the first relocation item in the file.
				mvarDOSHeader.OverlayNumber = br.ReadUInt16(); // Overlay number. Normally zero, meaning that it's the main program.

				ushort[] e_res = br.ReadUInt16Array(4);        // Reserved words
				ushort e_oemid = br.ReadUInt16();         // OEM identifier (for e_oeminfo)
				ushort e_oeminfo = br.ReadUInt16();       // OEM information; e_oemid specific
				ushort[] e_res2 = br.ReadUInt16Array(10);      // Reserved words
				e_lfanew = br.ReadUInt32();        // File address of new exe header
			}
			#endregion

			#region Portable Executable
			{
				if (e_lfanew != 0)
				{
					br.Accessor.Position = e_lfanew;

					#region PE header
					PEHeader pe = new PEHeader();
					pe.signature = br.ReadFixedLengthString(4);
					if (pe.signature == "PE\0\0")
					{
						pe.enabled = true;
						pe.machine = (PEMachineType)br.ReadUInt16();
						pe.sectionCount = br.ReadInt16();
						pe.unknown1 = br.ReadInt16();		// date/time stamp
						pe.unknown2 = br.ReadInt16();		// symbolTableOffset
						pe.unknown3 = br.ReadInt16();		// symbolCount
						pe.unknown4 = br.ReadInt16();		
						pe.unknown5 = br.ReadInt16();		
						pe.unknown6 = br.ReadInt16();
						pe.sizeOfOptionalHeader = br.ReadInt16(); // relative offset to sectiontable
						pe.characteristics = (PECharacteristics)br.ReadUInt16();

						exe.TargetMachineType = (ExecutableMachine)pe.machine;
						exe.Characteristics = (ExecutableCharacteristics)pe.characteristics;
					}
					else
					{
						pe.enabled = false;
						br.Accessor.Position -= 4;
					}
					#endregion
					#region Optional Header
					long peohOffset = br.Accessor.Position;
					PEOptionalHeader peoh = new PEOptionalHeader();
					if (pe.sizeOfOptionalHeader > 0)
					{
						// offset: 0x58
						peoh.enabled = true;
						peoh.magic = br.ReadUInt16();
						peoh.unknown1 = br.ReadUInt16();
						peoh.unknown2 = br.ReadUInt32();
						peoh.unknown3 = br.ReadUInt32();
						peoh.unknown4 = br.ReadUInt32();
						peoh.entryPointAddr = br.ReadUInt32();
						peoh.unknown5 = br.ReadUInt32();
						peoh.unknown6 = br.ReadUInt32();
						peoh.imageBase = br.ReadUInt32();
						peoh.sectionAlignment = br.ReadUInt32();
						peoh.fileAlignment = br.ReadUInt32();
						peoh.unknown7 = br.ReadUInt32();
						peoh.unknown8 = br.ReadUInt32();
						peoh.majorSubsystemVersion = br.ReadUInt16(); // 4 = NT 4 or later
						peoh.unknown9 = br.ReadUInt16();
						peoh.unknown10 = br.ReadUInt32();
						peoh.imageSize = br.ReadUInt32();
						peoh.headerSize = br.ReadUInt32();
						peoh.unknown11 = br.ReadUInt32();
						peoh.subsystem = br.ReadUInt16();
						peoh.unknown12 = br.ReadUInt16();
						peoh.unknown13 = br.ReadUInt32();
						peoh.unknown14 = br.ReadUInt32();
						peoh.unknown15 = br.ReadUInt32();
						peoh.unknown16 = br.ReadUInt32();
						peoh.unknown17 = br.ReadUInt32();
						peoh.rvaCount = br.ReadUInt32();
					}
					else
					{
						peoh.enabled = false;
					}
					#endregion
					#region Data Directories
					{
						for (int i = 0; i < peoh.rvaCount; i++)
						{
							uint rva = br.ReadUInt32();
						}
					}
					#endregion
					#region Sections Table
					{
						// offset: 0x138
						if (peoh.enabled)
						{
							br.Accessor.Seek(peohOffset + pe.sizeOfOptionalHeader, SeekOrigin.Begin);
						}
						
						uint lastRawDataPtr = 0;
						for (short i = 0; i < pe.sectionCount; i++)
						{
							PESectionHeader pesh = new PESectionHeader();
							pesh.name = br.ReadFixedLengthString(8).TrimNull();
							pesh.virtualSize = br.ReadUInt32();
							pesh.virtualAddress = br.ReadUInt32();
							pesh.rawDataSize = br.ReadUInt32();
							pesh.rawDataPtr = br.ReadUInt32();
							pesh.unknown1 = br.ReadUInt32();
							pesh.unknown2 = br.ReadUInt32();
							pesh.unknown3 = br.ReadUInt32();
							pesh.characteristics = (PESectionCharacteristics)br.ReadUInt32();

							if (fsom != null)
							{
								File file = new File();
								file.Name = pesh.name;
								file.Properties.Add("reader", br);
								file.Properties.Add("offset", pesh.rawDataPtr);
								file.Properties.Add("length", pesh.rawDataSize);

								file.Size = pesh.rawDataSize;
								file.DataRequest += file_DataRequest;
								fsom.Files.Add(file);
							}

							ExecutableSection sect = new ExecutableSection();
							sect.Name = pesh.name;
							// sect.DataRequest += sect_DataRequest;
							sect.VirtualSize = pesh.virtualSize;
							sect.VirtualAddress = pesh.virtualAddress;
							sect.PhysicalAddress = pesh.rawDataPtr;
							sect.Characteristics = (ExecutableSectionCharacteristics)pesh.characteristics;

							long ofs = br.Accessor.Position;
							br.Accessor.Position = pesh.rawDataPtr;
							sect.Data = br.ReadBytes(pesh.rawDataSize);
							br.Accessor.Position = ofs;

							exe.Sections.Add(sect);
						}
					}
					#endregion
				}
				else
				{
					#region New Executable
					// not a PE file, try NE?
					br.Accessor.Position = 128;
					string NE = br.ReadFixedLengthString(2);
					if (NE == "NE")
					{
						byte MajLinkerVersion = br.ReadByte(); ;    //The major linker version
						byte MinLinkerVersion = br.ReadByte();    //The minor linker version
						ushort EntryTableOffset = br.ReadUInt16();   //Offset of entry table, see below
						ushort EntryTableLength = br.ReadUInt16();   //Length of entry table in bytes
						uint FileLoadCRC = br.ReadUInt32();        //UNKNOWN - PLEASE ADD INFO
						byte ProgFlags = br.ReadByte();           //Program flags, bitmapped
						byte ApplFlags = br.ReadByte();           //Application flags, bitmapped
						byte AutoDataSegIndex = br.ReadByte();    //The automatic data segment index
						ushort InitHeapSize = br.ReadUInt16();       //The intial local heap size
						ushort InitStackSize = br.ReadUInt16();      //The inital stack size
						uint EntryPoint = br.ReadUInt32();         //CS:IP entry point, CS is index into segment table
						uint InitStack = br.ReadUInt32();          //SS:SP inital stack pointer, SS is index into segment table
						ushort SegCount = br.ReadUInt16();           //Number of segments in segment table
						ushort ModRefs = br.ReadUInt16();            //Number of module references (DLLs)
						ushort NoResNamesTabSiz = br.ReadUInt16();   //Size of non-resident names table, in bytes (Please clarify non-resident names table)
						ushort SegTableOffset = br.ReadUInt16();     //Offset of Segment table
						ushort ResTableOffset = br.ReadUInt16();     //Offset of resources table
						ushort ResidNamTable = br.ReadUInt16();      //Offset of resident names table
						ushort ModRefTable = br.ReadUInt16();        //Offset of module reference table
						ushort ImportNameTable = br.ReadUInt16();    //Offset of imported names table (array of counted strings, terminated with string of length 00h)
						uint OffStartNonResTab = br.ReadUInt32();  //Offset from start of file to non-resident names table
						ushort MovEntryCount = br.ReadUInt16();      //Count of moveable entry point listed in entry table
						ushort FileAlnSzShftCnt = br.ReadUInt16();   //File alligbment size shift count (0=9(default 512 byte pages))
						ushort nResTabEntries = br.ReadUInt16();     //Number of resource table entries
						byte targOS = br.ReadByte();              //Target OS
						byte OS2EXEFlags = br.ReadByte();         //Other OS/2 flags
						ushort retThunkOffset = br.ReadUInt16();     //Offset to return thunks or start of gangload area - what is gangload?
						ushort segrefthunksoff = br.ReadUInt16();    //Offset to segment reference thunks or size of gangload area
						ushort mincodeswap = br.ReadUInt16();        //Minimum code swap area size
						byte expctwinver_min = br.ReadByte();      //Expected windows version (minor)
						byte expctwinver_maj = br.ReadByte();      //Expected windows version (major)
					}
					#endregion
				}
			}
			#endregion

#if EXECUTABLE_LOAD_RESOURCES
			#region Resources
			{
				ExecutableSection sectRSRC = exe.Sections[".rsrc"];
				if (sectRSRC != null)
				{
					byte[] rsrc_data = sectRSRC.Data;

					ResourceObjectModel resources = new ResourceObjectModel();
					Win32EmbeddedResourceDataFormat rsrc = new Win32EmbeddedResourceDataFormat();
					rsrc.AddressOffset = sectRSRC.VirtualAddress;

					FileAccessor fa = new FileAccessor(resources, rsrc);
					fa.Open(ref rsrc_data);
					fa.Load();
					fa.Close();

					if (fsom != null)
					{
						Folder fldrResources = fsom.Folders.Add("Resources");
						foreach (ResourceBlock block in resources.Blocks)
						{
							RecursiveLoadResourceBlock(block, fldrResources);
						}
					}

					#region Version Information
					{
						// find the VS_VERSION_INFO resource
						DirectoryResourceBlock drbVersion = (resources.Blocks["Version"] as DirectoryResourceBlock);
						if (drbVersion != null)
						{
							DirectoryResourceBlock drb1 = (drbVersion.Blocks[0] as DirectoryResourceBlock);

							ContentResourceBlock crb = (drb1.Blocks[0] as ContentResourceBlock);

							ObjectModels.Version.VersionObjectModel ver = new ObjectModels.Version.VersionObjectModel();
							Version.Microsoft.Win32VersionDataFormat vs_version_info = new Version.Microsoft.Win32VersionDataFormat();
							FileAccessor fa_ver = new FileAccessor(ver, vs_version_info);
							byte[] ver_data = crb.Data;
							fa_ver.Open(ref ver_data);
							fa_ver.Load();
							fa_ver.Close();
						}
					}
					#endregion
				}
			}
			#endregion
#endif

			#region Push out Executable to the ObjectModel
			{
				if (exec != null)
				{
					exe.CopyTo(exec);
				}
				else if (fsom != null)
				{
					// don't do this again
					/*
					foreach (ExecutableSection sect in exe.Sections)
					{
						fsom.Files.Add(sect.Name, sect.Data);
					}
					*/
				}
			}
			#endregion
		}

#if EXECUTABLE_LOAD_RESOURCES
		private void RecursiveLoadResourceBlock(ResourceBlock block, Folder parent)
		{
			if (block is UniversalEditor.ObjectModels.Resource.Blocks.DirectoryResourceBlock)
			{
				UniversalEditor.ObjectModels.Resource.Blocks.DirectoryResourceBlock dir = (block as UniversalEditor.ObjectModels.Resource.Blocks.DirectoryResourceBlock);
				Folder fldr1 = parent.Folders.Add(dir.Name);
				foreach (ResourceBlock block1 in dir.Blocks)
				{
					RecursiveLoadResourceBlock(block1, fldr1);
				}
			}
			else if (block is UniversalEditor.ObjectModels.Resource.Blocks.ContentResourceBlock)
			{
				UniversalEditor.ObjectModels.Resource.Blocks.ContentResourceBlock crb = (block as UniversalEditor.ObjectModels.Resource.Blocks.ContentResourceBlock);
				File file = parent.Files.Add(crb.Name, crb.Data);
			}
		}
#endif

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader br = (Reader)(file.Properties["reader"]);
			uint offset = (uint)(file.Properties["offset"]);
			uint length = (uint)(file.Properties["length"]);

			br.Accessor.Position = offset;
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (fsom == null && exe == null) throw new ObjectModelNotSupportedException("Object model must be a FileSystem or an Executable");

			#region DOS part
			bw.WriteFixedLengthString("MZ");
			bw.WriteUInt16(mvarDOSHeader.LastBlockLength); // The number of bytes in the last block of the program that are actually used. If this value is zero, that means the entire last block is used (i.e. the effective value is 512).
			bw.WriteUInt16(mvarDOSHeader.NumBlocksInEXE); // Number of blocks in the file that are part of the EXE file. If mvarDOSHeader.LastBlockLength is non-zero, only that much of the last block is used.
			bw.WriteUInt16(mvarDOSHeader.NumRelocEntriesAfterHeader); // Number of relocation entries stored after the header. May be zero.
			bw.WriteUInt16(mvarDOSHeader.NumParagraphsInHeader); // Number of paragraphs in the header. The program's data begins just after the header, and this field can be used to calculate the appropriate file offset. The header includes the relocation entries. Note that some OSs and/or programs may fail if the header is not a multiple of 512 bytes.
			bw.WriteUInt16(mvarDOSHeader.NumParagraphsAdditionalMemory); // Number of paragraphs of additional memory that the program will need. This is the equivalent of the BSS size in a Unix program. The program can't be loaded if there isn't at least this much memory available to it.
			bw.WriteUInt16(mvarDOSHeader.NumMaxParagraphsAdditionalMemory); // Maximum number of paragraphs of additional memory. Normally, the OS reserves all the remaining conventional memory for your program, but you can limit it with this field.
			bw.WriteUInt16(mvarDOSHeader.RelativeStackSegmentValue); // Relative value of the stack segment. This value is added to the segment the program was loaded at, and the result is used to initialize the SS register.
			bw.WriteUInt16(mvarDOSHeader.InitialValueRegisterSP); // Initial value of the SP register.
			bw.WriteUInt16(mvarDOSHeader.WordChecksum); // Word checksum. If set properly, the 16-bit sum of all words in the file should be zero. Usually, this isn't filled in.
			bw.WriteUInt16(mvarDOSHeader.InitialValueRegisterIP); // Initial value of the IP register.
			bw.WriteUInt16(mvarDOSHeader.InitialValueRegisterCS); // Initial value of the CS register, relative to the segment the program was loaded at.
			bw.WriteUInt16(mvarDOSHeader.FirstRelocationItemOffset); // Offset of the first relocation item in the file.
			bw.WriteUInt16(mvarDOSHeader.OverlayNumber); // Overlay number. Normally zero, meaning that it's the main program.
			#endregion
			#region Portable Executable
			bw.Accessor.Position = 0x3C;
			int e_lfanew = (int)(bw.Accessor.Position + 4);
			bw.WriteInt32(e_lfanew);

			#region PE header
			PEHeader pe = new PEHeader();
			pe.signature = "PE\0\0";
			if (fsom != null)
			{
				pe.sectionCount = (short)fsom.Files.Count;
			}
			else if (exe != null)
			{
				pe.sectionCount = (short)exe.Sections.Count;
			}

			bw.WriteFixedLengthString(pe.signature);
			bw.WriteUInt16((ushort)pe.machine);
			bw.WriteUInt16((ushort)pe.sectionCount);
			bw.WriteUInt16((ushort)pe.unknown1);
			bw.WriteUInt16((ushort)pe.unknown2);
			bw.WriteUInt16((ushort)pe.unknown3);
			bw.WriteUInt16((ushort)pe.unknown4);
			bw.WriteUInt16((ushort)pe.unknown5);
			bw.WriteUInt16((ushort)pe.unknown6);
			bw.WriteUInt16((ushort)pe.sizeOfOptionalHeader); // relative offset to sectiontable
			bw.WriteUInt16((ushort)pe.characteristics);
			#endregion
			#region PE Optional Header
			PEOptionalHeader peoh = new PEOptionalHeader();
			peoh.enabled = true;
			peoh.magic = 267;
			peoh.entryPointAddr = 4096;
			peoh.imageBase = 4194304;
			peoh.sectionAlignment = 4096;
			peoh.fileAlignment = 512;
			peoh.majorSubsystemVersion = 4;
			peoh.imageSize = 16384;
			peoh.headerSize = 512;
			peoh.subsystem = 2;
			peoh.rvaCount = 16;

			if (peoh.enabled)
			{
				bw.WriteUInt16((ushort)peoh.magic);
				bw.WriteUInt16((ushort)peoh.unknown1);
				bw.WriteUInt32((uint)peoh.unknown2);
				bw.WriteUInt32((uint)peoh.unknown3);
				bw.WriteUInt32((uint)peoh.unknown4);
				bw.WriteUInt32((uint)peoh.entryPointAddr);
				bw.WriteUInt32((uint)peoh.unknown5);
				bw.WriteUInt32((uint)peoh.unknown6);
				bw.WriteUInt32((uint)peoh.imageBase);
				bw.WriteUInt32((uint)peoh.sectionAlignment);
				bw.WriteUInt32((uint)peoh.fileAlignment);
				bw.WriteUInt32((uint)peoh.unknown7);
				bw.WriteUInt32((uint)peoh.unknown8);
				bw.WriteUInt32((uint)peoh.majorSubsystemVersion); // 4 = NT 4 or later
				bw.WriteUInt32((uint)peoh.unknown9);
				bw.WriteUInt32((uint)peoh.unknown10);
				bw.WriteUInt32((uint)peoh.imageSize);
				bw.WriteUInt32((uint)peoh.headerSize);
				bw.WriteUInt32((uint)peoh.unknown11);
				bw.WriteUInt16((ushort)peoh.subsystem);
				bw.WriteUInt16((ushort)peoh.unknown12);
				bw.WriteUInt32((uint)peoh.unknown13);
				bw.WriteUInt32((uint)peoh.unknown14);
				bw.WriteUInt32((uint)peoh.unknown15);
				bw.WriteUInt32((uint)peoh.unknown16);
				bw.WriteUInt32((uint)peoh.unknown17);
				bw.WriteUInt32((uint)peoh.rvaCount);
			}
			#endregion
			#endregion

			#region Sections
			{
				System.Collections.Generic.List<PESectionHeader> peshes = new System.Collections.Generic.List<PESectionHeader>();

				long offset = base.Accessor.Position;
				if (exe != null)
				{
					foreach (ExecutableSection section in exe.Sections)
					{
						offset += PESectionHeader.HeaderSize;
					}
					foreach (ExecutableSection section in exe.Sections)
					{
						PESectionHeader pesh = new PESectionHeader();
						pesh.name = section.Name;
						pesh.virtualSize = section.VirtualSize;
						pesh.virtualAddress = (uint)offset;
						pesh.rawDataPtr = (uint)offset;
						pesh.rawDataSize = (uint)section.Data.Length;
						peshes.Add(pesh);

						offset += section.Data.Length;
					}
				}

				foreach (PESectionHeader pesh in peshes)
				{
					bw.WriteFixedLengthString(pesh.name, 8);
					bw.WriteUInt32(pesh.virtualSize);
					bw.WriteUInt32(pesh.virtualAddress);
					bw.WriteUInt32(pesh.rawDataSize);
					bw.WriteUInt32(pesh.rawDataPtr);
					bw.WriteUInt32(pesh.unknown1);
					bw.WriteUInt32(pesh.unknown2);
					bw.WriteUInt32(pesh.unknown3);
					bw.WriteUInt32((uint)pesh.characteristics);
				}

				foreach (ExecutableSection section in exe.Sections)
				{
					bw.WriteBytes(section.Data);
				}
			}
			#endregion
		}
	}
}
