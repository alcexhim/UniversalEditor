using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors;
using UniversalEditor.IO;

using UniversalEditor.DataFormats.FileSystem.BPlus;

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Help.Compiled;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp
{
	/// <summary>
	/// The Windows Help (WinHelp) HLP data format. Much of the framework for reading these files is contained in BPlusFileSystemDataFormat, since the container file format
	/// itself is remarkably generic (albeit unused, as far as I can tell, apart from WinHelp files), but WinHelp-specific stuff is here.
	/// </summary>
	public class HLPDataFormat : BPlusFileSystemDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(CompiledHelpObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new FileSystemObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			
			FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
			CompiledHelpObjectModel help = (objectModels.Pop() as CompiledHelpObjectModel);

			bool compressed = false;
			int topicBlockSize = 0;

			#region System Header
			{
				File fileSystem = fsom.Files["|SYSTEM"];
				byte[] dataSystem = fileSystem.GetData();

				MemoryAccessor maSystem = new MemoryAccessor(dataSystem);
				Reader readerSystem = new Reader(maSystem);

				Internal.SYSTEMHEADER systemHeader = ReadSYSTEMHEADER(readerSystem);

				if (systemHeader.Version.Minor <= 16)
				{
					// not compressed
					compressed = false;
					topicBlockSize = 2048;
				}
				else if (systemHeader.Version.Minor > 16)
				{
					if (systemHeader.Flags == Internal.SystemHeaderFlags.None)
					{
						compressed = false;
						topicBlockSize = 4096;
					}
					else if (systemHeader.Flags == Internal.SystemHeaderFlags.CompressedLZ77TopicBlockSize4k)
					{
						compressed = true;
						topicBlockSize = 4096;
					}
					else if (systemHeader.Flags == Internal.SystemHeaderFlags.CompressedLZ77TopicBlockSize2k)
					{
						compressed = true;
						topicBlockSize = 2048;
					}
				}

				if (systemHeader.Version.Minor <= 16)
				{
					// help file title follows SYSTEMHEADER
					string helpFileTitle = readerSystem.ReadNullTerminatedString();
				}
				else
				{
					// If Minor is above 16, one or more SYSTEMREC records follow instead up to the internal end of the |SYSTEM file:
					while (!readerSystem.EndOfStream)
					{
						Internal.SYSTEMRECORD systemRecord = ReadSYSTEMRECORD(readerSystem);
						switch (systemRecord.RecordType)
						{
							case Internal.SystemRecordType.Title:
							{
								help.Title = System.Text.Encoding.Default.GetString(systemRecord.Data).TrimNull();
								break;
							}
							case Internal.SystemRecordType.Copyright:
							{
								help.Copyright = System.Text.Encoding.Default.GetString(systemRecord.Data).TrimNull();
								break;
							}
							case Internal.SystemRecordType.Contents:
							{
								int topicOffset = BitConverter.ToInt32(systemRecord.Data, 0);
								break;
							}
							case Internal.SystemRecordType.Config:
							{
								string macro = System.Text.Encoding.Default.GetString(systemRecord.Data).TrimNull();
								// help.Macros.Add(macro);
								break;
							}
							case Internal.SystemRecordType.Icon:
							{
								break;
							}
							case Internal.SystemRecordType.Window:
							{
								break;
							}
							case Internal.SystemRecordType.Citation:
							{
								break;
							}
							case Internal.SystemRecordType.LanguageID:
							{
								MemoryAccessor maSystemRecord = new MemoryAccessor(systemRecord.Data);
								Reader readerSystemRecord = new Reader(maSystemRecord);
								ushort[] langaugeIDs = readerSystemRecord.ReadUInt16Array((int)((double)systemRecord.Data.Length / 2));
								break;
							}
							case Internal.SystemRecordType.TableOfContents:
							{
								break;
							}
							case Internal.SystemRecordType.CharacterSet:
							{
								break;
							}
						}
					}
				}
			}
			#endregion
			#region Font File
			{
				File file = fsom.Files["|FONT"];
				byte[] data = file.GetData();

				MemoryAccessor ma = new MemoryAccessor(data);
				Reader reader = new Reader(ma);

				Internal.FONTHEADER fontHeader = ReadFONTHEADER(reader);
				List<string> faceNames = new List<string>();
				reader.Seek(fontHeader.FacenamesOffset, SeekOrigin.Begin);
				int faceNameLength = (int)((double)(fontHeader.DescriptorsOffset - fontHeader.FacenamesOffset) / fontHeader.NumFacenames);
				for (int i = 0; i < fontHeader.NumFacenames; i++)
				{
					string faceName = reader.ReadFixedLengthString(faceNameLength).TrimNull();
					faceNames.Add(faceName);
				}

				if (fontHeader.FacenamesOffset >= 12)
				{
					// Multimedia MVB files use different structures to store font descriptors. Assume this structure for descriptors if FacenamesOffset is at least 12.
					// If this kind of descriptor is used, any metric is given in twips.
				}
				else
				{
					List<Internal.OLDFONT> listFonts = new List<Internal.OLDFONT>();

					// At DescriptorsOffset is an array located describing all fonts used in the help file.
					// If this kind of descriptor appears in a help file, any metric value is given in HalfPoints.
					reader.Seek(fontHeader.DescriptorsOffset, SeekOrigin.Begin);
					for (int i = 0; i < fontHeader.NumDescriptors; i++)
					{
						Internal.OLDFONT font = ReadOLDFONT(reader);
						listFonts.Add(font);
					}
				}
			}
			#endregion
			#region Topic File
			{
				File file = fsom.Files["|TOPIC"];
				byte[] data = file.GetData();

				MemoryAccessor ma = new MemoryAccessor(data);
				Reader reader = new Reader(ma);

				Internal.TOPICBLOCKHEADER topicBlockHeader = ReadTOPICBLOCKHEADER(reader);

				while (!reader.EndOfStream)
				{
					Internal.TOPICLINK topicLink = ReadTOPICLINK(reader);

					switch (topicLink.RecordType)
					{
						case Internal.TopicLinkRecordType.TopicHeader:
						{
							Internal.TOPICHEADER topicHeader = ReadTOPICHEADER(reader);

							// The LinkData2 of Topic RecordType 2 contains NUL terminated strings. The first string is the topic title, the next strings contain all macros to be
							// executed on opening this topic (specified using the ! footnote).
							string datastr = reader.ReadFixedLengthString(topicLink.DataLen2);
							string[] datastrs = datastr.Split(new char[] { '\0' });

							string topicTitle = datastrs[0];
							List<string> macros = new List<string>();
							for (int i = 1; i < datastrs.Length; i++)
							{
								macros.Add(datastrs[i]);
							}
							break;
						}
						case Internal.TopicLinkRecordType.Display31:
						{
							// TODO: test with putty.hlp
							byte unknown1 = reader.ReadByte();
							byte unknown2 = reader.ReadByte();
							ushort id = reader.ReadUInt16();
							ushort flags = reader.ReadUInt16(); // unknownfollows, spacingabovefollows, rightindentfollows, etc.
							byte[] blahblah = reader.ReadBytes(7);

							ushort sanityCheck0xFF = reader.ReadUInt16();
							if (sanityCheck0xFF == 0xFF)
							{
								// end of character formatting. proceed with next ParagraphInfo if RecordType is 0x23, else you are done
								string value = reader.ReadNullTerminatedString();
							}
							break;
						}
					}
				}
			}
			#endregion
		}

		private Internal.TOPICHEADER ReadTOPICHEADER(Reader reader)
		{
			Internal.TOPICHEADER item = new Internal.TOPICHEADER();
			item.BlockSize = reader.ReadInt32();
			item.BrowseSequencePreviousTopic = reader.ReadInt32();
			item.BrowseSequenceNextTopic = reader.ReadInt32();
			item.TopicNum = reader.ReadInt32();
			item.NonScrollingRegionOffset = reader.ReadInt32();
			item.ScrollingRegionOffset = reader.ReadInt32();
			item.NextTopic = reader.ReadInt32();
			return item;
		}

		private Internal.TOPICLINK ReadTOPICLINK(Reader reader)
		{
			Internal.TOPICLINK item = new Internal.TOPICLINK();
			item.BlockSize = reader.ReadInt32();
			item.DataLen2 = reader.ReadInt32();
			item.PrevBlock = reader.ReadInt32();
			item.NextBlock = reader.ReadInt32();
			item.DataLen1 = reader.ReadInt32();
			item.RecordType = (Internal.TopicLinkRecordType)reader.ReadByte();
			return item;
		}

		private Internal.TOPICBLOCKHEADER ReadTOPICBLOCKHEADER(Reader reader)
		{
			Internal.TOPICBLOCKHEADER item = new Internal.TOPICBLOCKHEADER();
			item.LastTopicLink = reader.ReadInt32();
			item.FirstTopicLink = reader.ReadInt32();
			item.LastTopicHeader = reader.ReadInt32();
			return item;
		}

		private Internal.OLDFONT ReadOLDFONT(Reader reader)
		{
			Internal.OLDFONT item = new Internal.OLDFONT();
			item.Attributes = (Internal.FontAttributes)reader.ReadByte();
			item.HalfPoints = reader.ReadByte(); // PointSize * 2
			item.FontFamily = (Internal.FontFamily)reader.ReadByte();
			item.FacenameIndex = reader.ReadUInt16();
			item.ForegroundColor = reader.ReadBytes(3);
			item.BackgroundColor = reader.ReadBytes(3);
			return item;
		}

		private Internal.FONTHEADER ReadFONTHEADER(Reader reader)
		{
			Internal.FONTHEADER item = new Internal.FONTHEADER();
			item.NumFacenames = reader.ReadUInt16();
			item.NumDescriptors = reader.ReadUInt16();
			item.FacenamesOffset = reader.ReadUInt16();
			item.DescriptorsOffset = reader.ReadUInt16();
			if (item.FacenamesOffset >= 12)
			{
				item.NumStyles = reader.ReadUInt16();
				item.StyleOffset = reader.ReadUInt16();
				if (item.FacenamesOffset >= 16)
				{
					item.NumCharMapTables = reader.ReadUInt16();
					item.CharMapTableOffset = reader.ReadUInt16();
				}
			}
			return item;
		}

		private Internal.SYSTEMHEADER ReadSYSTEMHEADER(Reader reader)
		{
			Internal.SYSTEMHEADER item = new Internal.SYSTEMHEADER();
			
			short magic = reader.ReadInt16();
			if (magic != 0x036C) throw new InvalidDataFormatException("|SYSTEM header does not begin with { 0x6C, 0x03 }");

			short minorVersion = reader.ReadInt16();
			short majorVersion = reader.ReadInt16();
			item.Version = new Version(majorVersion, minorVersion);

			int GenDate = reader.ReadInt32();
			item.GenDate = time_tToDateTime(GenDate);
			item.Flags = (Internal.SystemHeaderFlags)reader.ReadUInt16();

			return item;
		}
		private Internal.SYSTEMRECORD ReadSYSTEMRECORD(Reader reader)
		{
			Internal.SYSTEMRECORD item = new Internal.SYSTEMRECORD();
			item.RecordType = (Internal.SystemRecordType)reader.ReadUInt16();
			item.DataSize = reader.ReadUInt16();
			item.Data = reader.ReadBytes(item.DataSize);
			return item;
		}

		private DateTime time_tToDateTime(int time_t)
		{
			// hory shet
			// http://blogs.msdn.com/b/brada/archive/2003/07/30/50205.aspx
			long win32FileTime = 10000000 * (long)time_t + 116444736000000000;
			return DateTime.FromFileTimeUtc(win32FileTime);
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			CompiledHelpObjectModel help = (objectModels.Pop() as CompiledHelpObjectModel);
			FileSystemObjectModel fsom = new FileSystemObjectModel();

			objectModels.Push(fsom);
		}
	}
}
