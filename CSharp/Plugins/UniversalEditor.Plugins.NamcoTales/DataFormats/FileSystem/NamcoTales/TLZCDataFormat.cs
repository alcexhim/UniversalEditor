using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NamcoTales
{
	public class TLZCDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Namco Tales Studio TLZC archive", new byte?[][] { new byte?[] { (byte)'T', (byte)'L', (byte)'Z', (byte)'C' } }, new string[] { "*.dat" });
			}
			return _dfr;
		}

		private uint mvarBlockSize = 10000;
		public uint BlockSize { get { return mvarBlockSize; } set { mvarBlockSize = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			IO.Reader reader = base.Accessor.Reader;
			string TLZC = reader.ReadFixedLengthString(4);
			if (TLZC != "TLZC") throw new InvalidDataFormatException();

			string filename = "FILENAME.FPS4";
			if (base.Accessor is FileAccessor)
			{
				filename = System.IO.Path.GetFileName((base.Accessor as FileAccessor).FileName);
				filename = System.IO.Path.ChangeExtension(filename, ".fps4");
			}

			// comtype MSF
			uint version = reader.ReadUInt32();
			uint compressedSize = reader.ReadUInt32();
			uint decompressedSize = reader.ReadUInt32();
			byte[] filedata = new byte[decompressedSize];

			reader.Seek(0x1D, IO.SeekOrigin.Begin);
			uint blockcount = decompressedSize;
			blockcount += 0xFFFF;
			blockcount /= mvarBlockSize;
			short[] zsizes = new short[blockcount];
			for (uint i = 0; i < blockcount; i++)
			{
				short zsize = reader.ReadInt16();
				zsizes[i] = zsize;
			}
			for (uint i = 0; i < blockcount; i++)
			{
				long offset = base.Accessor.Position;
				short zsize = zsizes[i];
				if (decompressedSize >= mvarBlockSize)
				{
					byte[] block = reader.ReadBytes(mvarBlockSize);
					// block = decompress(block);
					Array.Copy(block, 0, filedata, i * mvarBlockSize, block.Length);
				}
				else
				{
					// append
					if (zsize == 0)
					{
						// log MEMORY_FILE OFFSET tsize
					}
					else
					{
						// clog MEMORY_FILE OFFSET ZSIZE tsize
					}
					// append
				}
				decompressedSize -= 0x10000;
				offset += zsize;
				base.Accessor.Seek(offset, IO.SeekOrigin.Begin);
			}

			fsom.Files.Add(filename, filedata);

			// get SIZE asize MEMORY_FILE
			// log name 0 SIZE MEMORY_FILE
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
