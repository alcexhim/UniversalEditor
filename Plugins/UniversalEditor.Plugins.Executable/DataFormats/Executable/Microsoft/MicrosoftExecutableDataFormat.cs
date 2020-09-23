//
//  MicrosoftExecutableDataFormat.cs - provides a DataFormat to manipulate Microsoft executable files (e.g. DOS, MZ, LE, NE, PE)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.ObjectModels.FileSystem;

using UniversalEditor.DataFormats.Executable.Microsoft.PortableExecutable;

#if EXECUTABLE_LOAD_RESOURCES
using UniversalEditor.DataFormats.Resource.Microsoft;
using UniversalEditor.ObjectModels.Resource;
using UniversalEditor.ObjectModels.Resource.Blocks;
#endif

namespace UniversalEditor.DataFormats.Executable.Microsoft
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Microsoft executable files (e.g. DOS, MZ, LE, NE, PE).
	/// </summary>
	public class MicrosoftExecutableDataFormat : DataFormat
	{
		public NameValuePair<int>.NameValuePairCollection ImportTable { get; } = new NameValuePair<int>.NameValuePairCollection();
		public System.Reflection.Assembly CLRAssembly { get; private set; } = null;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://www.csn.ul.ie/~caolan/publink/winresdump/winresdump/doc/pefile.html");
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
					CLRAssembly = System.Reflection.Assembly.LoadFile(fa.FileName);
				}
				catch (BadImageFormatException)
				{
					CLRAssembly = null;
				}
			}
		}

		public byte[] StubProgram { get; set; } = new byte[] { 0x0E, 0x1F, 0xBA, 0x0E, 0x00, 0xB4, 0x09, 0xCD, 0x21, 0xB8, 0x01, 0x4C, 0xCD, 0x21, 0x54, 0x68, 0x69, 0x73, 0x20, 0x70, 0x72, 0x6F, 0x67, 0x72, 0x61, 0x6D, 0x20, 0x63, 0x61, 0x6E, 0x6E, 0x6F, 0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x75, 0x6E, 0x20, 0x69, 0x6E, 0x20, 0x44, 0x4F, 0x53, 0x20, 0x6D, 0x6F, 0x64, 0x65, 0x2E, 0x0D, 0x0D, 0x0A, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

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
				case PECompiler.HPaCppIA64:
				case PECompiler.IAREwarmCPP54ARM:
				case PECompiler.GCC3or4x:
				{
					// _Z{name.length}{name}i 					_Z1hic 					_Z1hv
					// void h(int)								void h(int, char)		void h(void)

					string _Z = name.Substring(0, 2);

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
			Reader reader = base.Accessor.Reader;

			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			ExecutableObjectModel exec = (objectModel as ExecutableObjectModel);
			if (fsom == null && exec == null) throw new ObjectModelNotSupportedException("Object model must be a FileSystem or an Executable");

			if (exec == null)
				exec = new ExecutableObjectModel();

			// try loading as a .NET assembly
			if (base.Accessor is FileAccessor)
			{
				System.Reflection.Assembly asm = null;
				try
				{
					asm = System.Reflection.Assembly.LoadFile(base.Accessor.GetFileName());
				}
				catch
				{
				}

				exec.ManagedAssembly = asm;
			}

			DOSExecutableHeader mvarDOSHeader = ReadDOSHeader(reader);
			exec.SetCustomProperty<DOSExecutableHeader>(MakeReference(), "DOSExecutableHeader", mvarDOSHeader);

			byte[] stubProgram = reader.ReadBytes(64);
			exec.SetCustomProperty<byte[]>(MakeReference(), "StubProgram", stubProgram);
			StubProgram = stubProgram;

			byte[] richHeaderDataEncrypted = null;
			byte[] richHeaderDataDecrypted = null;
			byte[] richHeaderDecryptionKey = null;
			bool richHeaderEnabled = ReadRichHeader(reader, out richHeaderDataEncrypted, out richHeaderDataDecrypted, out richHeaderDecryptionKey);
			RichHeader rh = DecodeRichHeader(richHeaderDataDecrypted);
			exec.SetCustomProperty<RichHeader>(MakeReference(), "RichHeader", rh);

			exec.SetCustomProperty<byte[]>(MakeReference(), "RichHeaderDataEncrypted", richHeaderDataEncrypted);
			exec.SetCustomProperty<byte[]>(MakeReference(), "RichHeaderDataDecrypted", richHeaderDataDecrypted);
			exec.SetCustomProperty<byte[]>(MakeReference(), "RichHeaderDecryptionKey", richHeaderDecryptionKey);
			
			#region Portable Executable
			{
				if (mvarDOSHeader.NewEXEHeaderOffset != 0)
				{
					reader.Accessor.Position = mvarDOSHeader.NewEXEHeaderOffset;

					// PE header
					PEHeader pe = ReadPEHeader(reader);
					if (pe.enabled)
					{
						exec.TargetMachineType = (ExecutableMachine)pe.machine;
						exec.Characteristics = (ExecutableCharacteristics)pe.characteristics;
					}

					// Optional Header
					long peohOffset = reader.Accessor.Position;
					PEOptionalHeader peoh = new PEOptionalHeader();
					peoh = ReadPEOptionalHeader(reader, pe);

					#region Data Directories
					{
						for (int i = 0; i < peoh.rvaCount; i++)
						{
							uint dataDirectoryOffset = reader.ReadUInt32();
							uint dataDirectoryLength = reader.ReadUInt32();
						}
					}
					#endregion
					#region Sections Table
					{
						// offset: 0x138
						if (peoh.enabled)
						{
							reader.Accessor.Seek(peohOffset + pe.sizeOfOptionalHeader, SeekOrigin.Begin);
						}

						uint lastRawDataPtr = 0;
						for (short i = 0; i < pe.sectionCount; i++)
						{
							PESectionHeader pesh = ReadPESectionHeader(reader);

							if (fsom != null)
							{
								File file = new File();
								file.Name = pesh.name;
								file.Properties.Add("reader", reader);
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

							long ofs = reader.Accessor.Position;
							reader.Accessor.Position = pesh.rawDataPtr;
							sect.Data = reader.ReadBytes(pesh.rawDataSize);
							reader.Accessor.Position = ofs;

							exec.Sections.Add(sect);
						}
					}
					#endregion
				}
				else
				{
					#region New Executable
					// not a PE file, try NE?
					reader.Accessor.Position = 128;
					string NE = reader.ReadFixedLengthString(2);
					if (NE == "NE")
					{
						NewExecutable.NewExecutableHeader ne_header = NewExecutable.NewExecutableHeader.Read(reader);
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
				if (fsom != null)
				{
					// don't do this again
					for (int i = 0;  i < exec.Sections.Count;  i++)
					{
						// i can't remember the purpose of the ominous warning "don't do this again"
						// so maybe using DataRequest will be less of a burden on memory
						// (I really need to benchmark this... especially replacing foreach with for)

						File f = fsom.AddFile(exec.Sections[i].Name);
						f.Properties.Add("section", exec.Sections[i]);
						f.DataRequest += f_DataRequest;
					}
				}
			}
			#endregion
		}

		private void f_DataRequest(object sender, DataRequestEventArgs e)
		{
			// copy the data on-demand from the ExecutableSection
			e.Data = ((ExecutableSection)((File)sender).Properties["section"]).Data;
		}

		private RichHeader DecodeRichHeader(byte[] richHeader)
		{
			if (richHeader == null) return null;

			RichHeader rich = new RichHeader();
			Reader reader = new Reader(new MemoryAccessor(richHeader));
			
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "DanS") throw new InvalidDataFormatException("Rich header does not begin with 'DanS'");

			for (int i = 0; i < 3; i++)
			{
				// three times the mask value, which when "decrypted" leaves three 4-byte zeros
				int unknown1 = reader.ReadInt32();
			}

			while (!reader.EndOfStream)
			{
				int dword = reader.ReadInt32();
				int id = (dword >> 16);
				int minver = (dword & 0xFFFF);
				int vnum = reader.ReadInt32();

				RichHeaderEntry entry = new RichHeaderEntry();
				entry.Id = id;
				entry.Version = minver;
				entry.Count = vnum;
				rich.Entries.Add(entry);
			}

			reader.Close();
			return rich;
		}

		private const int MAX_RICH_HEADER_DECRYPTION_ATTEMPTS = 100;

		/// <summary>
		/// Decrypts and returns the Rich header, or null if no Rich header was included with the executable.
		/// </summary>
		/// <param name="reader">The <see cref="Reader" /> used to access the file.</param>
		/// <returns>The data of the Rich header, or null if no Rich header was included with the executable.</returns>
		private bool ReadRichHeader(Reader reader, out byte[] encryptedData, out byte[] decryptedData, out byte[] decryptionKey)
		{
			// store the original position in case we run into an error later
			long originalPosition = reader.Accessor.Position;

			// http://ntcore.com/files/richsign.htm
			byte[] richBlock = System.Text.Encoding.ASCII.GetBytes("Rich");
			byte[] nextBlock = reader.ReadBytes(4);
			System.Collections.Generic.List<byte[]> RichBlocks = new System.Collections.Generic.List<byte[]>();

			int nRichHeaderDecryptionAttempt = 0;
			while (!nextBlock.Match(richBlock))
			{
				// see if we time out before hitting the Rich header
				if (nRichHeaderDecryptionAttempt == MAX_RICH_HEADER_DECRYPTION_ATTEMPTS)
				{
					// reset the reader to original position
					reader.Accessor.Position = originalPosition;

					encryptedData = null;
					decryptedData = null;
					decryptionKey = null;
					return false;
				}
				RichBlocks.Add(nextBlock);
				nextBlock = reader.ReadBytes(4);
				nRichHeaderDecryptionAttempt++;
			}
			byte[] decKey = reader.ReadBytes(4);

			byte[] encData = new byte[RichBlocks.Count * 4];
			byte[] decData = new byte[encData.Length];
			int x = 0;
			for (int i = 0; i < RichBlocks.Count; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					encData[x] = RichBlocks[i][j];
					decData[x] = (byte)(RichBlocks[i][j] ^ decKey[j]);
					x++;
				}
			}

			encryptedData = encData;
			decryptedData = decData;
			decryptionKey = decKey;
			return true;
		}

		private static PEHeader ReadPEHeader(Reader reader)
		{
			PEHeader pe = new PEHeader();
			pe.signature = reader.ReadFixedLengthString(4);
			if (pe.signature == "PE\0\0")
			{
				pe.enabled = true;
				pe.machine = (PEMachineType)reader.ReadUInt16();
				pe.sectionCount = reader.ReadInt16();
				pe.timestamp = reader.ReadInt16();		// date/time stamp
				pe.symbolTableOffset = reader.ReadInt16();		// symbolTableOffset
				pe.symbolCount = reader.ReadInt16();		// symbolCount
				pe.unknown4 = reader.ReadInt16();
				pe.unknown5 = reader.ReadInt16();
				pe.unknown6 = reader.ReadInt16();
				pe.sizeOfOptionalHeader = reader.ReadInt16(); // relative offset to sectiontable
				pe.characteristics = (PECharacteristics)reader.ReadUInt16();
			}
			else
			{
				pe.enabled = false;
				reader.Accessor.Position -= 4;
			}
			return pe;
		}
		private static PESectionHeader ReadPESectionHeader(Reader reader)
		{
			PESectionHeader pesh = new PESectionHeader();
			pesh.name = reader.ReadFixedLengthString(8).TrimNull();
			pesh.virtualSize = reader.ReadUInt32();
			pesh.virtualAddress = reader.ReadUInt32();
			pesh.rawDataSize = reader.ReadUInt32();
			pesh.rawDataPtr = reader.ReadUInt32();
			pesh.unknown1 = reader.ReadUInt32();
			pesh.unknown2 = reader.ReadUInt32();
			pesh.unknown3 = reader.ReadUInt32();
			pesh.characteristics = (PESectionCharacteristics)reader.ReadUInt32();
			return pesh;
		}
		private static PEOptionalHeader ReadPEOptionalHeader(Reader reader, PEHeader pe)
		{
			PEOptionalHeader peoh = new PEOptionalHeader();
			if (pe.sizeOfOptionalHeader > 0)
			{
				// offset: 0x58
				peoh.enabled = true;
				peoh.magic = reader.ReadUInt16();
				peoh.unknown1 = reader.ReadUInt16();
				peoh.unknown2 = reader.ReadUInt32();
				peoh.unknown3 = reader.ReadUInt32();
				peoh.unknown4 = reader.ReadUInt32();
				peoh.entryPointAddr = reader.ReadUInt32();
				peoh.unknown5 = reader.ReadUInt32();
				peoh.unknown6 = reader.ReadUInt32();
				peoh.imageBase = reader.ReadUInt32();
				peoh.sectionAlignment = reader.ReadUInt32();
				peoh.fileAlignment = reader.ReadUInt32();
				peoh.unknown7 = reader.ReadUInt32();
				peoh.unknown8 = reader.ReadUInt32();
				peoh.majorSubsystemVersion = reader.ReadUInt16(); // 4 = NT 4 or later
				peoh.unknown9 = reader.ReadUInt16();
				peoh.unknown10 = reader.ReadUInt32();
				peoh.imageSize = reader.ReadUInt32();
				peoh.headerSize = reader.ReadUInt32();
				peoh.unknown11 = reader.ReadUInt32();
				peoh.subsystem = reader.ReadUInt16();
				peoh.unknown12 = reader.ReadUInt16();
				peoh.unknown13 = reader.ReadUInt32();
				peoh.unknown14 = reader.ReadUInt32();
				peoh.unknown15 = reader.ReadUInt32();
				peoh.unknown16 = reader.ReadUInt32();
				peoh.unknown17 = reader.ReadUInt32();
				peoh.rvaCount = reader.ReadUInt32();
			}
			else
			{
				peoh.enabled = false;
			}
			return peoh;
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

		private static DOSExecutableHeader ReadDOSHeader(Reader reader)	
		{
			DOSExecutableHeader value = new DOSExecutableHeader();

			// 0x4d, 0x5a. This is the "magic number" of an EXE file. The first byte of the file is 0x4d and the second is 0x5a.
			string signature = reader.ReadFixedLengthString(2);
			if (signature != "MZ") throw new InvalidDataFormatException("File does not begin with \"MZ\"");

			value.LastBlockLength = reader.ReadUInt16();
			value.NumBlocksInEXE = reader.ReadUInt16();
			value.NumRelocEntriesAfterHeader = reader.ReadUInt16();
			value.NumParagraphsInHeader = reader.ReadUInt16();
			value.NumParagraphsAdditionalMemory = reader.ReadUInt16();
			value.NumMaxParagraphsAdditionalMemory = reader.ReadUInt16();
			value.RelativeStackSegmentValue = reader.ReadUInt16();
			value.InitialValueRegisterSP = reader.ReadUInt16();
			value.WordChecksum = reader.ReadUInt16();
			value.InitialValueRegisterIP = reader.ReadUInt16();
			value.InitialValueRegisterCS = reader.ReadUInt16();
			value.FirstRelocationItemOffset = reader.ReadUInt16();
			value.OverlayNumber = reader.ReadUInt16();

			ushort[] e_res = reader.ReadUInt16Array(4);        // Reserved words
			ushort e_oemid = reader.ReadUInt16();         // OEM identifier (for e_oeminfo)
			ushort e_oeminfo = reader.ReadUInt16();       // OEM information; e_oemid specific
			ushort[] e_res2 = reader.ReadUInt16Array(10);      // Reserved words

			value.NewEXEHeaderOffset = reader.ReadUInt32();
			return value;
		}
		private static void WriteDOSHeader(Writer writer, DOSExecutableHeader value)
		{
			writer.WriteFixedLengthString("MZ");
			writer.WriteUInt16(value.LastBlockLength); // The number of bytes in the last block of the program that are actually used. If this value is zero, that means the entire last block is used (i.e. the effective value is 512).
			writer.WriteUInt16(value.NumBlocksInEXE); // Number of blocks in the file that are part of the EXE file. If mvarDOSHeader.LastBlockLength is non-zero, only that much of the last block is used.
			writer.WriteUInt16(value.NumRelocEntriesAfterHeader); // Number of relocation entries stored after the header. May be zero.
			writer.WriteUInt16(value.NumParagraphsInHeader); // Number of paragraphs in the header. The program's data begins just after the header, and this field can be used to calculate the appropriate file offset. The header includes the relocation entries. Note that some OSs and/or programs may fail if the header is not a multiple of 512 bytes.
			writer.WriteUInt16(value.NumParagraphsAdditionalMemory); // Number of paragraphs of additional memory that the program will need. This is the equivalent of the BSS size in a Unix program. The program can't be loaded if there isn't at least this much memory available to it.
			writer.WriteUInt16(value.NumMaxParagraphsAdditionalMemory); // Maximum number of paragraphs of additional memory. Normally, the OS reserves all the remaining conventional memory for your program, but you can limit it with this field.
			writer.WriteUInt16(value.RelativeStackSegmentValue); // Relative value of the stack segment. This value is added to the segment the program was loaded at, and the result is used to initialize the SS register.
			writer.WriteUInt16(value.InitialValueRegisterSP); // Initial value of the SP register.
			writer.WriteUInt16(value.WordChecksum); // Word checksum. If set properly, the 16-bit sum of all words in the file should be zero. Usually, this isn't filled in.
			writer.WriteUInt16(value.InitialValueRegisterIP); // Initial value of the IP register.
			writer.WriteUInt16(value.InitialValueRegisterCS); // Initial value of the CS register, relative to the segment the program was loaded at.
			writer.WriteUInt16(value.FirstRelocationItemOffset); // Offset of the first relocation item in the file.
			writer.WriteUInt16(value.OverlayNumber); // Overlay number. Normally zero, meaning that it's the main program.


			ushort[] e_res = new ushort[4];
			writer.WriteUInt16Array(e_res);        // Reserved words
			ushort e_oemid = 0;
			writer.WriteUInt16(e_oemid);         // OEM identifier (for e_oeminfo)
			ushort e_oeminfo = 0;       // OEM information; e_oemid specific
			writer.WriteUInt16(e_oeminfo);
			ushort[] e_res2 = new ushort[10];      // Reserved words
			writer.WriteUInt16Array(e_res2);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (fsom == null && exe == null) throw new ObjectModelNotSupportedException("Object model must be a FileSystem or an Executable");

			// DOS part
			DOSExecutableHeader mvarDOSHeader = exe.GetCustomProperty<DOSExecutableHeader>(MakeReference(), "DOSExecutableHeader", new DOSExecutableHeader());
			WriteDOSHeader(bw, mvarDOSHeader);

			#region Portable Executable
			int e_lfanew = (int)(bw.Accessor.Position + 4);
			bw.WriteInt32(e_lfanew);

			byte[] stubProgram = exe.GetCustomProperty<byte[]>(MakeReference(), "StubProgram", StubProgram);
			bw.WriteBytes(stubProgram);

			int sizeOfRich = 0;

			RichHeader rh = exe.GetCustomProperty<RichHeader>(MakeReference(), "RichHeader");
			byte[] richHeaderData = exe.GetCustomProperty<byte[]>(MakeReference(), "RichHeaderDataEncrypted");
			if (richHeaderData != null)
			{
				byte[] richHeaderDecryptionKey = exe.GetCustomProperty<byte[]>(MakeReference(), "RichHeaderDecryptionKey");
				sizeOfRich = richHeaderData.Length;

				byte[] DanS = new byte[] { (byte)'D', (byte)'a', (byte)'n', (byte)'S' };
				bw.WriteBytes(XorByteArray(DanS, richHeaderDecryptionKey));
				for (int i = 0; i < 3; i++)
				{
					bw.WriteBytes(richHeaderDecryptionKey);
				}
				foreach (RichHeaderEntry entry in rh.Entries)
				{
					int dw = 0;
					dw = (entry.Id << 16);
					dw |= entry.Version;

					byte[] dword1 = BitConverter.GetBytes(dw);
					dword1 = XorByteArray(dword1, richHeaderDecryptionKey);
					byte[] dword2 = BitConverter.GetBytes(entry.Count);
					dword2 = XorByteArray(dword2, richHeaderDecryptionKey);

					bw.WriteBytes(dword1);
					bw.WriteBytes(dword2);
				}
				// bw.WriteBytes(richHeaderData);

				bw.WriteFixedLengthString("Rich");
				bw.WriteBytes(richHeaderDecryptionKey);
			}

			if (sizeOfRich > 0) // HACK : not sure if this is correct or not
			{
				int sizeOfPadding = 96 - sizeOfRich;

				byte[] unknown = new byte[sizeOfPadding];
				bw.WriteBytes(unknown);
			}

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
			// pe.characteristics = ExecutableCharacteristicsToPECharacteristics(exe.Characteristics);
			// pe.sizeOfOptionalHeader = 240;
			pe.sizeOfOptionalHeader = 224;      // 240???
			pe.machine = (PEMachineType)exe.TargetMachineType;
			pe.characteristics = (PECharacteristics)exe.Characteristics;

			WritePEHeader(bw, pe);

			#endregion
			#region PE Optional Header
			long peohOffset = bw.Accessor.Position;
			// peohOffset += 8;
			bw.Accessor.Seek(peohOffset, SeekOrigin.Begin);

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
			peoh.rvas1 = new uint[16];
			peoh.rvas2 = new uint[16];
			
			if (peoh.enabled)
			{
				WritePEOptionalHeader(bw, peoh);
			}
			#endregion
			#endregion

			bw.Accessor.Seek(peohOffset + pe.sizeOfOptionalHeader, SeekOrigin.Begin);

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
						pesh.virtualSize = (uint) section.VirtualSize;
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

				bw.Align(1024);

				foreach (ExecutableSection section in exe.Sections)
				{
					bw.WriteBytes(section.Data);
				}
			}
			#endregion
		}

		private void WritePEHeader(Writer bw, PEHeader pe)
		{
			bw.WriteFixedLengthString(pe.signature);
			bw.WriteUInt16((ushort)pe.machine);
			bw.WriteUInt16((ushort)pe.sectionCount);
			bw.WriteUInt16((ushort)pe.timestamp);					// ®9
			bw.WriteUInt16((ushort)pe.symbolTableOffset);			// PE
			bw.WriteUInt16((ushort)pe.symbolCount);					// CO
			bw.WriteUInt16((ushort)pe.unknown4);					// \0\0
			bw.WriteUInt16((ushort)pe.unknown5);					// \0\0
			bw.WriteUInt16((ushort)pe.unknown6);					// à\0
			bw.WriteUInt16((ushort)pe.sizeOfOptionalHeader); // relative offset to sectiontable
			bw.WriteUInt16((ushort)pe.characteristics);
		}

		private void WritePEOptionalHeader(Writer bw, PEOptionalHeader peoh)
		{
			bw.WriteUInt16((ushort)peoh.magic);
			bw.WriteUInt16((ushort)peoh.unknown1);		// major/minor linker version
			bw.WriteUInt32((uint)peoh.unknown2);		// size of code
			bw.WriteUInt32((uint)peoh.unknown3);		// size of initialized data
			bw.WriteUInt32((uint)peoh.unknown4);		// size of uninitialized data
			bw.WriteUInt32((uint)peoh.entryPointAddr);
			bw.WriteUInt32((uint)peoh.unknown5);		// base of code
			bw.WriteUInt32((uint)peoh.unknown6);		// base of data
			bw.WriteUInt32((uint)peoh.imageBase);
			bw.WriteUInt32((uint)peoh.sectionAlignment);
			bw.WriteUInt32((uint)peoh.fileAlignment);
			bw.WriteUInt32((uint)peoh.unknown7);		// major/minor OS version
			bw.WriteUInt32((uint)peoh.unknown8);		// major/minor Image version
			bw.WriteUInt16((ushort)peoh.majorSubsystemVersion); // major subsystem version (4 = NT 4 or later)
			bw.WriteUInt16((ushort)peoh.unknown9);		// minor subsystem version
			bw.WriteUInt32((uint)peoh.unknown10);		// reserved1
			bw.WriteUInt32((uint)peoh.imageSize);		// image size
			bw.WriteUInt32((uint)peoh.headerSize);		// header size
			bw.WriteUInt32((uint)peoh.unknown11);		// checksum
			bw.WriteUInt16((ushort)peoh.subsystem);
			bw.WriteUInt16((ushort)peoh.unknown12);		// DLL characteristics
			bw.WriteUInt32((uint)peoh.unknown13);		// size of stack reserve
			bw.WriteUInt32((uint)peoh.unknown14);		// size of stack commit
			bw.WriteUInt32((uint)peoh.unknown15);		// size of heap reserve
			bw.WriteUInt32((uint)peoh.unknown16);		// size of heap commit
			bw.WriteUInt32((uint)peoh.unknown17);		// loader flags
			bw.WriteUInt32((uint)peoh.rvaCount);
			// 96 + 128 = 224
			
			// write the RVA values
			for (uint i = 0; i < peoh.rvaCount; i++)
			{
				bw.WriteUInt32(peoh.rvas1[(int)i]);
				bw.WriteUInt32(peoh.rvas2[(int)i]);
			}
		}

		private byte[] XorByteArray(byte[] value, byte[] key)
		{
			byte[] resultValue = new byte[value.Length];
			int j = 0;
			for (int i = 0; i < resultValue.Length; i++)
			{
				resultValue[i] = (byte)(value[i] ^ key[j]);
				j++;
				if (j == key.Length) j = 0;
			}
			return resultValue;
		}
	}
}
