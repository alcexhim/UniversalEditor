//
//  ACEDataFormat.cs - provides a DataFormat for manipulating archives in WinACE format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker
//  Copyright (c) 2004 Marcel Lemke <mlemke@winace.com)
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

/*

The public version of UNACE is limited in its functionality:

	*	no v2.0 decompression
	*	no EMS/XMS support

	*	decompression dictionary limited by the target system; this means that the
		16bit version has a maximum of 32k only

	*	no decryption
	*	no wildcard-handling

Here's hoping the community can fix these bugs!

*/

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.WinAce
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in WinACE format.
	/// </summary>
	/// <remarks>
	/// This code has been ported from the unace utility, copyright Marcel Lemke (mlemke@winace.com) and licensed under the GNU GPLv2.
	/// </remarks>
	public class ACEDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Reader br = Accessor.Reader;
			if (br.Accessor.Length < 14) throw new InvalidDataFormatException("File must be at least 14 bytes in length");
			br.Accessor.Position = 0;

			crc_init();

			ushort HEAD_CRC = br.ReadUInt16();          // 37941
			ushort HEAD_SIZE = br.ReadUInt16();         // 49

			#region header
			byte HEAD_TYPE = br.ReadByte();
			ACEHeaderFlags HEAD_FLAGS = (ACEHeaderFlags)br.ReadUInt16();     // 36866 - comment, 36864 - normal

			string ACESIGN = br.ReadFixedLengthString(7);
			if (ACESIGN != "**ACE**") throw new InvalidDataFormatException("File does not contain \"**ACE**\" signature");

			byte VER_MOD = br.ReadByte();           // 20
			byte VER_CR = br.ReadByte();            // 20
			byte HOST_CR = br.ReadByte();           // 2
			byte VOL_NUM = br.ReadByte();           // 0
			uint TIME_CR = br.ReadUInt32();         // 1117062890
			uint TIME_22 = br.ReadUInt32();         // 2017284461

			byte commenLeng = br.ReadByte();
			byte commenLeng1 = br.ReadByte();
			ushort RES2 = br.ReadUInt16();
			byte AV_SIZE = br.ReadByte();
			AuthenticityString = br.ReadFixedLengthString(AV_SIZE);

			if ((HEAD_FLAGS & ACEHeaderFlags.HasComment) == ACEHeaderFlags.HasComment)
			{
				ushort commentSize = br.ReadUInt16();
				byte[] comment = br.ReadBytes(commentSize);
			}

			#endregion

			while (!br.EndOfStream)
			{
				ushort head_crc = br.ReadUInt16();                    // 47000
				ushort head_size = br.ReadUInt16();                    // 47
				byte head_type = br.ReadByte();
				ACEHeaderFlags head_flags = (ACEHeaderFlags)br.ReadUInt16();                    // 257

				uint compressedLength = br.ReadUInt32();
				uint decompressedLength = br.ReadUInt32();

				uint ftime = br.ReadUInt32();                    // 1117044212
				uint attrs = br.ReadUInt32();                    // 32
				uint file_crc = br.ReadUInt32();                    // 2754832349

				// TECH
				byte tech_type = br.ReadByte();         // 2 in norm , max
				byte tech_qual = br.ReadByte();         //			max: 5, norm: 3
				ushort tech_parm = br.ReadUInt16();//			max: 10

				ushort reserved = br.ReadUInt16();                  // 17748
				ushort filenameLength = br.ReadUInt16();
				string filename = br.ReadFixedLengthString(filenameLength);

				if ((head_flags & ACEHeaderFlags.HasComment) == ACEHeaderFlags.HasComment)
				{
					ushort comment_size = br.ReadUInt16();
					string comment = br.ReadFixedLengthString(comment_size);
				}

				File f = fsom.AddFile(filename);
				f.Size = decompressedLength;
				f.Properties.Add("offset", Accessor.Position);
				f.Properties.Add("compressedLength", compressedLength);
				f.Properties.Add("decompressedLength", decompressedLength);
				f.Properties.Add("tech_type", tech_type);
				f.Properties.Add("reader", Accessor.Reader);
				f.DataRequest += f_DataRequest;

				// skip over the compressed data until we need it
				Accessor.Seek(compressedLength, IO.SeekOrigin.Current);
			}
		}

		void f_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (File)sender;
			long offset = (long)file.Properties["offset"];
			uint compressedLength = (uint)file.Properties["compressedLength"];
			uint decompressedLength = (uint)file.Properties["decompressedLength"];
			byte tech_type = (byte)file.Properties["tech_type"];
			IO.Reader reader = (IO.Reader)file.Properties["reader"];

			// in newer version, we might close the file once the initial header read is complete
			// this means subsequent accesses to read the file, the document must be opened again

			reader.Seek(offset, IO.SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(compressedLength);

			byte[] decompressedData = compressedData;
			switch (tech_type)
			{
			case 1:
				{
					decompressedData = Compression.LZW.LZWStream.Decompress(compressedData);
					break;
				}
			}

			e.Data = decompressedData;
		}

		/*
		void comment_out(ACEHeader head, byte[] top)      // outputs comment if present
		{
			int i;
			int comm_cpr_size = 0;
			string comm;

			if ((head.flags & ACEHeaderFlags.HasComment) == ACEHeaderFlags.HasComment)
			{                             // comment present?
				if (head.type == MAIN_BLK)
				{                          // get begin and size of comment data
					comm = MCOMM;
					comm_cpr_size = MCOMM_SIZE;
				}
				else
				{
					comm = FCOMM;
					comm_cpr_size = FCOMM_SIZE;
				}                          // limit comment size if too big
				i = sizeof(head) - (INT)(comm - (CHAR*)&head);
				if (comm_cpr_size > i)
					comm_cpr_size = i;
				dcpr_comm(i);              // decompress comment

# ifdef AMIGA
				{
					char* p = comm;
					while (*p)
					{
						if (*p == 0x0D)
							*p = 0x0A;          // Replace ms-dos line termination
						p++;
					}
				}
#endif

				printf("%s\n\n%s\n\n", top, comm); // output comment
			}
		}

		private void dcpr_comm_init()
		{
			int i = comm_cpr_size > size_rdb * 4 ? size_rdb * 4 : comm_cpr_size;
			if (!f_err)
			{
				// memcpy(buf_rd, comm, i);
			}

			code_rd = buf_rd[0];
			rpos = bits_rd = 0;
		}

		private void dcpr_comm(int comm_size)
		{
			short[] hash = new short[comm_cpr_hf(255 + 255) + 1];
			int dpos = 0, c, pos, len, hs;

			if (comm_cpr_size != 0)
			{
				dcpr_comm_init();
				len = code_rd >> (32 - 15);
				addbits(15);
				if (len >= comm_size)
				{
					len = comm_size - 1;
				}
				if (read_wd(maxwd_mn, dcpr_code_mn, dcpr_wd_mn, max_cd_mn))
				{
					do
					{
						if (dpos > 1)
						{
							pos = hash[hs = comm_cpr_hf(comm[dpos - 1], comm[dpos - 2])];
							hash[hs] = dpos;
						}
						addbits(dcpr_wd_mn[(c = dcpr_code_mn[code_rd >> (32 - maxwd_mn)])]);
						if (rpos == size_rdb - 3)
						{
							rpos = 0;
						}
						if (c > 255)
						{
							c -= 256;
							c += 2;
							while (c--)
							{
								comm[dpos++] = comm[pos++];
							}
						}
						else
						{
							comm[dpos++] = c;
						}
					}
					while (dpos < len);
				}
				comm[len] = 0;
			}
		}
		*/

		public string AuthenticityString { get; set; } = "*UNREGISTERED VERSION*";

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Writer bw = base.Accessor.Writer;

			crc_init();

			byte[] header = new byte[0];
			#region header
			{
				MemoryAccessor mah = new MemoryAccessor();
				IO.Writer bwh = new IO.Writer(mah);

				byte HEAD_TYPE = 0;
				bwh.WriteByte(HEAD_TYPE);

				ACEHeaderFlags HEAD_FLAGS = (ACEHeaderFlags.HasAuthenticityVerification | ACEHeaderFlags.Solid);
				bwh.WriteUInt16((ushort)HEAD_FLAGS);

				bwh.WriteFixedLengthString("**ACE**");

				byte VER_MOD = 20;
				bwh.WriteByte(VER_MOD);

				byte VER_CR = 20;
				bwh.WriteByte(VER_CR);

				byte HOST_CR = 2;
				bwh.WriteByte(HOST_CR);

				byte VOL_NUM = 0;
				bwh.WriteByte(VOL_NUM);

				uint TIME_CR = 1117062890;
				bwh.WriteUInt32(TIME_CR);

				uint TIME_22 = 2017284461;
				bwh.WriteUInt32(TIME_22);

				ushort RES1 = 0;
				bwh.WriteUInt16(RES1);

				ushort RES2 = 0;
				bwh.WriteUInt16(RES2);

				bwh.WriteByte((byte)AuthenticityString.Length);
				bwh.WriteFixedLengthString(AuthenticityString);
				bwh.Flush();
				bwh.Close();

				header = mah.ToArray();
			}
			#endregion

			ushort HEAD_CRC = (ushort)crc_calculate(CRCMASK, header);

			bw.WriteUInt16(HEAD_CRC); // 37941
			bw.WriteUInt16((ushort)header.Length);

			bw.WriteBytes(header);

			foreach (File file in fsom.Files)
			{
				ushort r0 = 47000;
				bw.WriteUInt16(r0);
				ushort r1 = 47;
				bw.WriteUInt16(r1);
				ushort r2 = 257;
				bw.WriteUInt16(r2);

				byte un1 = 128;
				bw.WriteByte(un1);

				byte[] decompressedData = file.GetData();
				byte[] compressedData = decompressedData;

				bw.WriteUInt32((uint)compressedData.Length);
				bw.WriteUInt32((uint)decompressedData.Length);

				uint unknown1 = 1117044212;
				bw.WriteUInt32(unknown1);
				uint unknown2 = 32;
				bw.WriteUInt32(unknown2);

				uint file_crc = 2754832349;
				file_crc = (uint)crc_calculate(CRCMASK, decompressedData);
				bw.WriteUInt32(file_crc);
				uint unknown4 = 656128;
				bw.WriteUInt32(unknown4);
				ushort unknown5 = 17748;
				bw.WriteUInt16(unknown5);

				bw.WriteUInt16((ushort)file.Name.Length);
				bw.WriteFixedLengthString(file.Name);
				bw.WriteBytes(compressedData);
			}
		}

		private const uint CRCMASK = 0xFFFFFFFF;
		private const uint CRCPOLY = 0xEDB88320;
		private uint[] crctable;

		private void crc_init()   // initializes CRC table
		{
			crctable = new uint[256];
			uint r = 0, j = 0;
			for (uint i = 0; i <= 255; i++)
			{
				for (r = i, j = 8; j != 0; j--)
				{
					r = (uint)(((r & 1) != 0) ? (r >> 1) ^ CRCPOLY : (r >> 1));
				}
				crctable[i] = r;
			}
		}

		// Updates crc from addr till addr+len-1
		//
		private uint crc_calculate(uint crc, byte[] addr)
		{
			for (int i = 0; i < addr.Length; i++)
			{
				crc = crctable[(byte)(crc ^ addr[i])] ^ (crc >> 8);
			}
			return crc;
		}
	}
}
