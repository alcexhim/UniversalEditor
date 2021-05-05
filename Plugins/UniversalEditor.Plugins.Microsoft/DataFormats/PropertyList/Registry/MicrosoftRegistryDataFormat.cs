//
//  MicrosoftRegistryDataFormat.cs - provides a DataFormat to manipulate Microsoft registry files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using MBS.Framework;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.UserInterface;

namespace UniversalEditor.DataFormats.PropertyList.Registry
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Microsoft registry files.
	/// </summary>
	public class MicrosoftRegistryDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public int RootCellOffset { get; set; }
		public int HiveBinsDataSize { get; set; }
		public int ClusteringFactor { get; set; }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			Reader reader = base.Accessor.Reader;

			// from https://github.com/msuhanov/regf/blob/master/Windows%20registry%20file%20format%20specification.md

			string signature = reader.ReadFixedLengthString(4); // should be 'regf'
			if (signature != "regf") throw new InvalidDataFormatException("File does not begin with 'regf'");

			// This number is incremented by 1 in the beginning of a write operation on the primary file
			int primarySequenceNumber = reader.ReadInt32();
			// This number is incremented by 1 at the end of a write operation on the primary file
			int secondarySequenceNumber = reader.ReadInt32();

			// A primary sequence number and a secondary sequence number should be equal after a successful write operation
			if (primarySequenceNumber != secondarySequenceNumber)
			{
				// do we wnnt to handle this as a fatal error?
				(Application.Instance as IHostApplication).Messages.Add(HostApplicationMessageSeverity.Warning, "Primary and secondary sequence number mismatch", base.Accessor.GetFileName());
			}

			DateTime lastModifiedTimestamp = reader.ReadDateTime64(); // FILETIME

			// Major version of a hive writer
			// Should be 1
			int majorVersion = reader.ReadInt32();

			// Minor version of a hive writer
			// Should be 3, 4, 5, or 6
			int minorVersion = reader.ReadInt32();

			if (majorVersion != 1 && (minorVersion < 3 || minorVersion > 6))
			{
				// ?
			}

			// 0 means 'primary file'
			int fileType = reader.ReadInt32();
			// 1 means 'direct memory load'
			int fileFormat = reader.ReadInt32();

			// Offset of a root cell in bytes, relative from the start of the hive bins data
			RootCellOffset = reader.ReadInt32();
			// Size of the hive bins data in bytes
			HiveBinsDataSize = reader.ReadInt32();
			// Logical sector size of the underlying disk in bytes divided by 512
			ClusteringFactor = reader.ReadInt32();

			// UTF-16LE string (contains a partial file path to the primary file, or a file name of the primary file), used for debugging purposes
			string fileName = reader.ReadFixedLengthString(64, UniversalEditor.IO.Encoding.UTF16LittleEndian);

			// OFFSET 112: In Windows 10, the following fields are found to be allocated in the previously reserved areas
			Guid RmId = reader.ReadGuid();
			Guid logId = reader.ReadGuid();
			MicrosoftRegistryReservedFlags flags = (MicrosoftRegistryReservedFlags) reader.ReadInt32();
			Guid TmId = reader.ReadGuid();

			string guidSignature = reader.ReadFixedLengthString(4); // "rmtm"

			// OFFSET 168: could be either OfRg or a last reorganized timestamp depending on...
			string offregSignature168 = reader.ReadFixedLengthString(4);
			if (offregSignature168 == "OfRg")
			{
				MicrosoftRegistryOfflineRegistryInfo ofrg = ReadOfRg(reader);
			}
			else
			{
				// back up four bytes...
				reader.Seek(-4, SeekOrigin.Current);
				// ... and read the last reorganized timestamp
				DateTime ftLastReorganizedTimestamp = reader.ReadDateTime64();

				// OFFSET 176: The Offline Registry Library (offreg.dll) is writing the following additional fields to the base
				// block when the hive is serialized (saved): These fields begin at offset 176, 180, 512 in current versions of
				// the library (versions 6.2, 6.3, 10.0). The fields defined below begin at offset 168, 172, 512 in legacy versions
				// of the library (version 6.1):
				string offregSignature176 = reader.ReadFixedLengthString(4);
				if (offregSignature176 == "OfRg")
				{
					MicrosoftRegistryOfflineRegistryInfo ofrg = ReadOfRg(reader);
				}
			}

			// at offset 4040: Guid ThawTmId
			// at offset 4056: Guid ThawRmId
			// at offset 4072: Guid ThawLogId
			reader.Seek(4096, SeekOrigin.Begin);

			// Hive bins are variable in size and consist of a header and cells. A hive
			// bin's header is 32 bytes in length
			MicrosoftRegistryHiveBinHeader hbin = ReadHiveBinHeader(reader);

			System.Collections.Generic.List<MicrosoftRegistryKeyNode> keyNodes = new System.Collections.Generic.List<MicrosoftRegistryKeyNode>();

			long hiveBinDataOffset = base.Accessor.Position;
			long end = base.Accessor.Position + hbin.Size;

			while (base.Accessor.Position < end)
			{
				long thisCellOffset = base.Accessor.Position;
				long thisCellLocalOffset = thisCellOffset - hiveBinDataOffset;

				// Cells fill the remaining space of a hive bin (without gaps between them),
				// each cell is variable in size and has the following structure:
				int cellSize = reader.ReadInt32();
				int realCellSize = Math.Abs(cellSize);
				MicrosoftRegistryHiveCellType cellType = (MicrosoftRegistryHiveCellType)reader.ReadInt16();
				switch (cellType)
				{
					case MicrosoftRegistryHiveCellType.IndexLeaf:
					{
						short elementCount = reader.ReadInt16();
						for (short i = 0; i < elementCount; i++)
						{
							// in bytes, relative from the start of the hive bins data
							int keyNodeOffset = reader.ReadInt32();
						}

						// All list elements are required to be sorted ascending by an
						// uppercase version of a key name string (comparison should be
						// based on character codes).
						break;
					}
					case MicrosoftRegistryHiveCellType.FastLeaf:
					{
						short elementCount = reader.ReadInt16();
						for (short i = 0; i < elementCount; i++)
						{
							// in bytes, relative from the start of the hive bins data
							int keyNodeOffset = reader.ReadInt32();
							// the first 4 ASCII characters of a key name string (used to speed up lookups)
							int nameHint = reader.ReadInt32();
						}

						// If a key name string is less than 4 characters in length, it is
						// stored in the beginning of the Name hint field (hereinafter, the
						// beginning of the field means the byte at the lowest address or the
						// first few bytes at lower addresses in the field), unused bytes of
						// this field are set to null. UTF-16LE characters are converted to
						// ASCII (extended), if possible (if it isn't, the first byte of the
						// Name hint field is null).

						// Hereinafter, an extended ASCII string means a string made from
						// UTF-16LE characters with codes less than 256 by removing the null
						// byte (at the highest address) from each character.

						// All list elements are required to be sorted (as described above).
						break;
					}
					case MicrosoftRegistryHiveCellType.HashLeaf:
					{
						short elementCount = reader.ReadInt16();
						for (short i = 0; i < elementCount; i++)
						{
							// in bytes, relative from the start of the hive bins data
							int keyNodeOffset = reader.ReadInt32();
							// Hash of a key name string, see below (used to speed up lookups)
							int nameHash = reader.ReadInt32();
						}

						// All list elements are required to be sorted (as described above).
						// The Hash leaf is used when the Minor version field of the base
						// block is greater than 4.

						// The hash is calculated using the following algorithm:
						// * let H be a 32-bit value, initial value is zero;
						// * let N be an uppercase name of a key;
						// * split N into characters, and
						// * for each character (exactly two bytes in the case of UTF-16LE,
						//                      also known as a wide character),
						//   treated as a number (character code), C[i], do the following:

						//      H = 37 * H + C[i];

						// H is the hash value.
						break;
					}
					case MicrosoftRegistryHiveCellType.IndexRoot:
					{
						short elementCount = reader.ReadInt16();
						for (short i = 0; i < elementCount; i++)
						{
							// in bytes, relative from the start of the hive bins data
							int subkeysListOffset = reader.ReadInt32();
						}

						// 1. An Index root can't point to another Index root.
						// 2. A Subkeys list can't point to an Index root.
						// 3. List elements within subkeys lists referenced by a single Index
						//    root must be sorted as a whole (i.e.the first list element of
						//    the second subkeys list must be greater than the last list
						//    element of the first subkeys list).
						break;
					}
					case MicrosoftRegistryHiveCellType.KeyNode:
					{
						MicrosoftRegistryKeyNode kn = ReadKeyNode(reader);
							kn.LocalOffset = thisCellLocalOffset;

							if (kn.KeyValueListOffset > -1)
							{
								long ofssave = reader.Accessor.Position;

								long localKeyValueListOffset = kn.KeyValueListOffset + hiveBinDataOffset;
								reader.Accessor.Position = localKeyValueListOffset; // - 28 ?
								System.Collections.Generic.List<MicrosoftRegistryKeyValue> values = new System.Collections.Generic.List<MicrosoftRegistryKeyValue>();
								for (int i = 0; i < kn.KeyValueCount; i++)
								{
									// In bytes, relative from the start of the hive bins data
									int keyValueOffset = reader.ReadInt32();
									keyValueOffset += (int)hiveBinDataOffset;
									// keyValueOffset -= 20;
									keyValueOffset -= 28;

									long offsave2 = reader.Accessor.Position;
									reader.Accessor.Position = keyValueOffset;
									MicrosoftRegistryKeyValue kv = ReadKeyValue(reader);
									values.Add(kv);
									reader.Accessor.Position = offsave2;
								}
								reader.Accessor.Position = ofssave;
							}
							keyNodes.Add(kn);
						break;
					}
					default:
					{
						break;
					}
				}

				long alreadyRead = reader.Accessor.Position - thisCellOffset;
				long remaining = realCellSize - alreadyRead;
				reader.Seek(remaining, SeekOrigin.Current);
			}
		}

		private MicrosoftRegistryKeyValue ReadKeyValue(Reader reader)
		{
			MicrosoftRegistryKeyValue kv = new MicrosoftRegistryKeyValue();
			short signature = reader.ReadInt16();
			if (signature != 27510) throw new InvalidOperationException("Invalid signature for key value 'vk'");

			kv.NameLength = reader.ReadInt16();
			kv.DataSize = reader.ReadInt32();
			kv.DataOffset = reader.ReadInt32();
			kv.DataType = (MicrosoftRegistryKeyValueDataType)reader.ReadInt32();
			kv.Flags = (MicrosoftRegistryKeyValueFlags)reader.ReadInt16();
			kv.Spare = reader.ReadInt16();
			kv.Name = reader.ReadFixedLengthString(kv.NameLength);
			return kv;
		}

		private MicrosoftRegistryKeyNode ReadKeyNode(Reader reader)
		{
			MicrosoftRegistryKeyNode kn = new MicrosoftRegistryKeyNode();
			kn.Flags = (MicrosoftRegistryKeyNodeFlags)reader.ReadInt16();

			// It is plausible that both a registry key virtualization (when registry writes to sensitive locations are redirected to per-user locations in order to protect a Windows registry against corruption) and a registry key reflection (when registry changes are synchronized between keys in 32-bit and 64-bit views; this feature was removed in Windows 7 and Windows Server 2008 R2) required more space than this field can provide, that is why the Largest subkey name length field was split and the new fields were introduced.
			// Starting from Windows Vista, user flags were moved away from the first 4 bits of the Flags field to the new User flags bit field (see above). These user flags in the new location are also called Wow64 flags. In Windows XP and Windows Server 2003, user flags are stored in the old location anyway.
			// It should be noted that, in Windows Vista and Windows 7, the 4th bit (counting from the most significant bit) of the Flags field is set to 1 in many key nodes belonging to different hives; this bit, however, can't be read through the NtQueryKey() call. Such key nodes are present in initial primary files within an installation image (install.wim), and their number doesn't increase after the installation. A possible explanation for this oddity is that initial primary files were generated on Windows XP or Windows Server 2003 using the Wow64 subsystem (see below).

			kn.LastModifiedTimestamp = reader.ReadDateTime64();
			// This field is used as of Windows 8 and Windows Server 2012; in
			// previous versions of Windows, this field is reserved and called
			// Spare)
			kn.Access = (MicrosoftRegistryKeyNodeAccess)reader.ReadInt32();
			// Offset of a parent key node in bytes, relative from the start of
			// the hive bins data (this field has no meaning on a disk for a root
			// key node)
			kn.ParentKeyNodeOffset = reader.ReadInt32();

			kn.SubkeyCount = reader.ReadInt32();
			kn.VolatileSubkeyCount = reader.ReadInt32();
			kn.SubkeysListOffset = reader.ReadInt32();
			kn.VolatileSubkeysListOffset = reader.ReadInt32();
			kn.KeyValueCount = reader.ReadInt32();
			kn.KeyValueListOffset = reader.ReadInt32();
			kn.KeySecurityOffset = reader.ReadInt32();
			kn.ClassNameOffset = reader.ReadInt32();
			kn.LargestSubkeyNameLength = reader.ReadInt32();
			kn.LargestSubkeyClassNameLength = reader.ReadInt32();
			kn.LargestValueNameLength = reader.ReadInt32();
			kn.LargestValueDataSize = reader.ReadInt32();
			kn.Workvar = reader.ReadInt32();
			short keyNameLength = reader.ReadInt16();
			kn.ClassNameLength = reader.ReadInt16();
			kn.KeyName = reader.ReadFixedLengthString(keyNameLength);
			return kn;
		}

		private MicrosoftRegistryHiveBinHeader ReadHiveBinHeader(Reader reader)
		{
			long pos = reader.Accessor.Position;
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "hbin")
				throw new InvalidOperationException("Invalid hive bin");

			MicrosoftRegistryHiveBinHeader hbin = new MicrosoftRegistryHiveBinHeader();
			hbin.Offset = reader.ReadInt32();
			hbin.Size = reader.ReadInt32();
			hbin.Reserved1 = reader.ReadInt64();
			hbin.Timestamp = reader.ReadDateTime64();
			hbin.Reserved2 = reader.ReadInt32();

			if (reader.Accessor.Position - pos != 32)
			{
				throw new InvalidDataFormatException("Sanity check failed");
			}
			return hbin;
		}

		private long GetRootCellFileOffset()
		{
			return 4096 + RootCellOffset;
		}

		private MicrosoftRegistryOfflineRegistryInfo ReadOfRg(Reader reader)
		{
			MicrosoftRegistryOfflineRegistryInfo ofrg = new MicrosoftRegistryOfflineRegistryInfo();
			ofrg.Flags = reader.ReadInt32();
			ofrg.SerializationTimestamp = reader.ReadDateTime64();
			return ofrg;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new System.NotImplementedException();
		}
	}
}
