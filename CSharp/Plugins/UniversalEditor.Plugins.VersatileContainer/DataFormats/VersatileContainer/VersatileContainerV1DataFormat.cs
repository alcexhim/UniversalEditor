using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.VersatileContainer;
using UniversalEditor.ObjectModels.VersatileContainer.Sections;

namespace UniversalEditor.DataFormats.VersatileContainer
{
	public class VersatileContainerV1DataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			_dfr = base.MakeReference();
			_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			_dfr.Filters.Add("Versatile Container binary file", new byte?[][] { new byte?[] { (byte)'V', (byte)'e', (byte)'r', (byte)'s', (byte)'a', (byte)'t', (byte)'i', (byte)'l', (byte)'e', (byte)' ', (byte)'C', (byte)'o', (byte)'n', (byte)'t', (byte)'a', (byte)'i', (byte)'n', (byte)'e', (byte)'r', (byte)' ', (byte)'f', (byte)'i', (byte)'l', (byte)'e', (byte)' ', (byte)'0', (byte)'0', (byte)'0', (byte)'1' } }, new string[] { "*.vcb" });
			return _dfr;
		}

		private VersatileContainerProperty.VersatileContainerPropertyCollection mvarProperties = new VersatileContainerProperty.VersatileContainerPropertyCollection();
		public VersatileContainerProperty.VersatileContainerPropertyCollection Properties { get { return mvarProperties; } }

		private Version mvarFormatVersion = new Version(2, 0);
		public Version FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		private uint mvarBytesPerNUMBER = 4;
		public uint BytesPerNUMBER { get { return mvarBytesPerNUMBER; } set { mvarBytesPerNUMBER = value; } }

		private uint mvarSectionAlignment = 512;
		public uint SectionAlignment { get { return mvarSectionAlignment; } set { mvarSectionAlignment = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			VersatileContainerObjectModel vcom = (objectModel as VersatileContainerObjectModel);

			string signature = br.ReadFixedLengthString(30);
			if (signature != "Versatile Container file 0001\0")
			{
                throw new InvalidDataFormatException();
			}

			mvarFormatVersion = br.ReadVersion();
			mvarSectionAlignment = br.ReadUInt32();
			mvarBytesPerNUMBER = br.ReadUInt32();

			string title = br.ReadNullTerminatedString(64);

			if (mvarFormatVersion.Major > 1)
			{
				long oldPosition = br.Accessor.Position;
				long numberOfPropertiesInFile = (long)ReadUnum(br);

				long[] propertyNameLengths = new long[numberOfPropertiesInFile];
				long[] propertyValueLengths = new long[numberOfPropertiesInFile];

				for (long i = 0; i < numberOfPropertiesInFile; i++)
				{
					long propertyNameLength = (long)ReadUnum(br);
					long propertyValueLength = (long)ReadUnum(br);
					propertyNameLengths.SetValue(propertyNameLength, i);
					propertyValueLengths.SetValue(propertyValueLength, i);
				}
				for (long i = 0; i < numberOfPropertiesInFile; i++)
				{
					long propertyNameLength = (long)propertyNameLengths.GetValue(i);
					long propertyValueLength = (long)propertyValueLengths.GetValue(i);

					string propertyName = br.ReadFixedLengthString(propertyNameLength);
					string propertyValue = br.ReadFixedLengthString(propertyValueLength);
					mvarProperties.Add(propertyName, propertyValue);
				}
				long newPosition = br.Accessor.Position;

				long totalSizeOfBlock = (newPosition - oldPosition);
				if ((mvarSectionAlignment - totalSizeOfBlock) > 0)
				{
					byte[] padding = br.ReadBytes((ulong)(mvarSectionAlignment - totalSizeOfBlock));
				}
			}

			long numberOfSectionsInFile = (long)ReadUnum(br);

			string[] sectionNames = new string[numberOfSectionsInFile];
			ulong[] sectionOffsets = new ulong[numberOfSectionsInFile];
			ulong[] sectionVirtualSizes = new ulong[numberOfSectionsInFile];
			ulong[] sectionPhysicalSizes = new ulong[numberOfSectionsInFile];
			VersatileContainerSectionType[] sectionContentTypes = new VersatileContainerSectionType[numberOfSectionsInFile];

			Compression.CompressionMethod[] sectionCompressions = new Compression.CompressionMethod[numberOfSectionsInFile];

			for (long i = 0; i < numberOfSectionsInFile; i++)
			{
				string sectionName = br.ReadFixedLengthString(32);
				if (sectionName.Contains("\0")) sectionName = sectionName.Substring(0, sectionName.IndexOf('\0'));

				ulong sectionOffset = ReadUnum(br);
				ulong sectionVirtualSize = ReadUnum(br);
				ulong sectionPhysicalSize = ReadUnum(br);

				sectionCompressions.SetValue((Compression.CompressionMethod)br.ReadInt32(), i);
				sectionNames.SetValue(sectionName, i);
				sectionOffsets.SetValue(sectionOffset, i);
				sectionPhysicalSizes.SetValue(sectionPhysicalSize, i);
				sectionVirtualSizes.SetValue(sectionVirtualSize, i);
			}

			for (long i = 0; i < numberOfSectionsInFile; i++)
			{
				ulong sectionOffset = (ulong)sectionOffsets.GetValue(i);
				ulong sectionVirtualSize = (ulong)sectionVirtualSizes.GetValue(i);
				ulong sectionPhysicalSize = (ulong)sectionPhysicalSizes.GetValue(i);
				string sectionName = (string)sectionNames.GetValue(i);
				ulong sectionRemainder = sectionPhysicalSize - sectionVirtualSize;

				byte[] sectionData = br.ReadBytes(sectionVirtualSize);
				br.Accessor.Seek((long)sectionRemainder, SeekOrigin.Current);

				Compression.CompressionMethod sectionCompression = (Compression.CompressionMethod)sectionCompressions.GetValue(i);

				if (fsom != null)
				{
					if (sectionCompression != Compression.CompressionMethod.None)
					{
						File section = new File();
						section.Name = sectionName;
						byte[] data = CompressionModule.FromKnownCompressionMethod(sectionCompression).Decompress(sectionData);
						section.SetDataAsByteArray(data);
						fsom.Files.Add(section);
					}
					else
					{
						File section = new File();
						section.Name = sectionName;
						section.SetDataAsByteArray(sectionData);
						fsom.Files.Add(section);
					}
				}
				else if (vcom != null)
				{
					if (sectionCompression != Compression.CompressionMethod.None)
					{
						VersatileContainerContentSection section = new VersatileContainerContentSection();
						// section.CompressionMethod = sectionCompression;
						section.Name = sectionName;
						byte[] data = CompressionModule.FromKnownCompressionMethod(sectionCompression).Decompress(sectionData);
						section.Data = data;
						vcom.Sections.Add(section);
					}
					else
					{
						VersatileContainerContentSection section = new VersatileContainerContentSection();
						section.Name = sectionName;
						section.Data = sectionData;
						vcom.Sections.Add(section);
					}
				}
			}

			if (fsom != null)
			{
				fsom.Title = title;
			}
			else if (vcom != null)
			{
				vcom.Title = title;
			}
		}

		private ulong ReadUnum(IO.Reader br)
		{
			switch (mvarBytesPerNUMBER)
			{
				case 1: return br.ReadByte();
				case 2: return br.ReadUInt16();
				case 4: return br.ReadUInt32();
				case 8: return br.ReadUInt64();
			}
			throw new ArgumentOutOfRangeException();
		}
		private long ReadNum(IO.Reader br)
		{
			switch (mvarBytesPerNUMBER)
			{
				case 1: return br.ReadByte();
				case 2: return br.ReadInt16();
				case 4: return br.ReadInt32();
				case 8: return br.ReadInt64();
			}
			throw new ArgumentOutOfRangeException();
		}

		private void WriteUnum(IO.Writer bw, ulong value)
		{
			switch (mvarBytesPerNUMBER)
			{
				case 1: bw.WriteByte((byte)value); return;
				case 2: bw.WriteUInt16((ushort)value); return;
				case 4: bw.WriteUInt32((uint)value); return;
				case 8: bw.WriteUInt64((ulong)value); return;
			}
			throw new ArgumentOutOfRangeException();
		}
		private void WriteNum(IO.Writer bw, long value)
		{
			switch (mvarBytesPerNUMBER)
			{
				case 1: bw.WriteByte((byte)value); return;
				case 2: bw.WriteInt16((short)value); return;
				case 4: bw.WriteInt32((int)value); return;
				case 8: bw.WriteInt64((long)value); return;
			}
			throw new ArgumentOutOfRangeException();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.Writer bw = base.Accessor.Writer;
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			VersatileContainerObjectModel vcom = (objectModel as VersatileContainerObjectModel);

			bw.WriteFixedLengthString("Versatile Container file 0001\0");
			bw.WriteVersion(mvarFormatVersion);
			bw.WriteUInt32(mvarSectionAlignment);
			bw.WriteUInt32(mvarBytesPerNUMBER);
			if (fsom != null)
			{
				bw.WriteFixedLengthString(fsom.Title, 64);
			}
			else if (vcom != null)
			{
				bw.WriteFixedLengthString(vcom.Title, 64);
			}

			#region Properties/Metadata (Versatile Container V2)
			if (mvarFormatVersion.Major > 1)
			{
				long oldPosition = bw.Accessor.Position;
				WriteUnum(bw, (ulong)mvarProperties.Count);
				foreach (VersatileContainerProperty property in mvarProperties)
				{
					WriteUnum(bw, (ulong)property.Name.Length);
					WriteUnum(bw, (ulong)property.Value.Length);
				}
				foreach (VersatileContainerProperty property in mvarProperties)
				{
					bw.WriteFixedLengthString(property.Name);
					bw.WriteFixedLengthString(property.Value);
				}
				long newPosition = bw.Accessor.Position;

				long totalSizeOfBlock = (newPosition - oldPosition);
				if ((mvarSectionAlignment - totalSizeOfBlock) > 0)
				{
					byte[] padding = new byte[mvarSectionAlignment - totalSizeOfBlock];
					bw.WriteBytes(padding);
				}
			}
			#endregion

			if (fsom != null)
			{
				WriteUnum(bw, (ulong)fsom.Files.Count);
			}
			else if (vcom != null)
			{
				WriteUnum(bw, (ulong)vcom.Sections.Count);
			}

			ulong sectionOffset = 0;
			if (fsom != null)
			{
				sectionOffset = (ulong)(bw.Accessor.Position + ((32 + (mvarBytesPerNUMBER * 3)) * fsom.Files.Count));
			}
			else if (vcom != null)
			{
				sectionOffset = (ulong)(bw.Accessor.Position + ((32 + (mvarBytesPerNUMBER * 3)) * vcom.Sections.Count));
			}

			byte[][] realDatas = null;
			if (fsom != null)
			{
				realDatas = new byte[fsom.Files.Count][];
			}
			else if (vcom != null)
			{
				realDatas = new byte[vcom.Sections.Count][];
			}

			if (fsom != null)
			{
				foreach (File section in fsom.Files)
				{
					byte[] data = section.GetDataAsByteArray();
                    /*
					if (section is File)
					{
						data = Compression.CompressionStream.Compress((section as File).CompressionMethod, data);
					}
                    */
					ulong sectionVirtualSize = (ulong)data.Length;
					realDatas[fsom.Files.IndexOf(section)] = data;

					bw.WriteFixedLengthString(section.Name, 32);

					ulong sectionPhysicalSize = sectionVirtualSize;

					ulong remainder = (sectionPhysicalSize % mvarSectionAlignment);
					if (remainder != 0)
					{
						sectionPhysicalSize += (mvarSectionAlignment - remainder);
					}

					WriteUnum(bw, sectionOffset);
					WriteUnum(bw, sectionVirtualSize);
					WriteUnum(bw, sectionPhysicalSize);

                    /*
					if (section is File)
					{
						bw.WriteInt32((int)((section as File).CompressionMethod));
					}
					else
					{
                    */
					bw.WriteInt32((int)Compression.CompressionMethod.None);
                    // }

					sectionOffset += sectionPhysicalSize;
				}

				foreach (File section in fsom.Files)
				{
					byte[] realData = realDatas[fsom.Files.IndexOf(section)];
					ulong sectionVirtualSize = (ulong)realData.Length;
					ulong sectionPhysicalSize = sectionVirtualSize;

					ulong remainder = (sectionPhysicalSize % mvarSectionAlignment);
					if (remainder != 0)
					{
						sectionPhysicalSize += (mvarSectionAlignment - remainder);
					}

					byte[] sectionData = new byte[sectionPhysicalSize];
					Array.Copy(realData, 0, sectionData, 0, (long)sectionVirtualSize);

					bw.WriteBytes(sectionData);
				}
			}
			else if (vcom != null)
			{
				foreach (VersatileContainerSection section in vcom.Sections)
				{
					if (section is VersatileContainerContentSection)
					{
						VersatileContainerContentSection content = (section as VersatileContainerContentSection);

						byte[] data = content.Data;

						ulong sectionVirtualSize = (ulong)data.Length;
						realDatas[vcom.Sections.IndexOf(section)] = data;

						bw.WriteFixedLengthString(section.Name, 32);

						ulong sectionPhysicalSize = sectionVirtualSize;

						ulong remainder = (sectionPhysicalSize % mvarSectionAlignment);
						if (remainder != 0)
						{
							sectionPhysicalSize += (mvarSectionAlignment - remainder);
						}

						WriteUnum(bw, sectionOffset);
						WriteUnum(bw, sectionVirtualSize);
						WriteUnum(bw, sectionPhysicalSize);

						bw.WriteInt32((int)Compression.CompressionMethod.None);

						sectionOffset += sectionPhysicalSize;
					}
				}
				foreach (VersatileContainerSection section in vcom.Sections)
				{
					byte[] realData = realDatas[vcom.Sections.IndexOf(section)];
					ulong sectionVirtualSize = (ulong)realData.Length;
					ulong sectionPhysicalSize = sectionVirtualSize;

					ulong remainder = (sectionPhysicalSize % mvarSectionAlignment);
					if (remainder != 0)
					{
						sectionPhysicalSize += (mvarSectionAlignment - remainder);
					}

					byte[] sectionData = new byte[sectionPhysicalSize];
					Array.Copy(realData, 0, sectionData, 0, (long)sectionVirtualSize);

					bw.WriteBytes(sectionData);
				}
			}
			bw.Flush();
		}
	}
}
