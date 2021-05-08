using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UniversalEditor.Compression.Modules.Deflate.Internal;
using UniversalEditor.Compression.Common;

namespace UniversalEditor.Compression.Modules.Gzip.Internal
{
	internal class GzipDecoder
	{
		private const int CommentFlag = 16;
		private const int CRCFlag = 2;
		private const int ExtraFieldsFlag = 4;
		private const int FileNameFlag = 8;
		private const int FileText = 1;
		private int gzip_header_flag;
		private int gzip_header_xlen;
		private uint gzipCrc32;
		private GzipHeaderState gzipFooterSubstate;
		private GzipHeaderState gzipHeaderSubstate;
		private uint gzipOutputStreamSize;
		private InputBuffer input;
		private int loopCounter;
		public uint Crc32
		{
			get
			{
				return this.gzipCrc32;
			}
		}
		public uint StreamSize
		{
			get
			{
				return this.gzipOutputStreamSize;
			}
		}
		public GzipDecoder(InputBuffer input)
		{
			this.input = input;
			this.Reset();
		}
		public bool ReadGzipFooter()
		{
			this.input.SkipToByteBoundary();
			bool result;
			if (this.gzipFooterSubstate == GzipHeaderState.ReadingCRC)
			{
				while (this.loopCounter < 4)
				{
					int bits = this.input.GetBits(8);
					if (bits < 0)
					{
						result = false;
						return result;
					}
					this.gzipCrc32 |= (uint)((uint)bits << 8 * this.loopCounter);
					this.loopCounter++;
				}
				this.gzipFooterSubstate = GzipHeaderState.ReadingFileSize;
				this.loopCounter = 0;
			}
			if (this.gzipFooterSubstate == GzipHeaderState.ReadingFileSize)
			{
				if (this.loopCounter == 0)
				{
					this.gzipOutputStreamSize = 0u;
				}
				while (this.loopCounter < 4)
				{
					int num2 = this.input.GetBits(8);
					if (num2 < 0)
					{
						result = false;
						return result;
					}
					this.gzipOutputStreamSize |= (uint)((uint)num2 << 8 * this.loopCounter);
					this.loopCounter++;
				}
			}
			result = true;
			return result;
		}
		public bool ReadGzipHeader()
		{
			int bits;
			bool result;
			switch (this.gzipHeaderSubstate)
			{
				case GzipHeaderState.ReadingID1:
					{
						bits = this.input.GetBits(8);
						if (bits < 0)
						{
							result = false;
							return result;
						}
						if (bits != 31)
						{
							throw new InvalidDataException("CorruptedGZipHeader");
						}
						this.gzipHeaderSubstate = GzipHeaderState.ReadingID2;
						break;
					}
				case GzipHeaderState.ReadingID2:
					{
						break;
					}
				case GzipHeaderState.ReadingCM:
					{
						goto IL_114;
					}
				case GzipHeaderState.ReadingFLG:
					{
						goto IL_14F;
					}
				case GzipHeaderState.ReadingMMTime:
					{
						goto IL_184;
					}
				case GzipHeaderState.ReadingXFL:
					{
						goto IL_1D1;
					}
				case GzipHeaderState.ReadingOS:
					{
						goto IL_1F6;
					}
				case GzipHeaderState.ReadingXLen1:
					{
						goto IL_21B;
					}
				case GzipHeaderState.ReadingXLen2:
					{
						goto IL_261;
					}
				case GzipHeaderState.ReadingXLenData:
					{
						goto IL_2A0;
					}
				case GzipHeaderState.ReadingFileName:
					{
						goto IL_2F3;
					}
				case GzipHeaderState.ReadingComment:
					{
						goto IL_348;
					}
				case GzipHeaderState.ReadingCRC16Part1:
					{
						goto IL_39B;
					}
				case GzipHeaderState.ReadingCRC16Part2:
					{
						goto IL_3DB;
					}
				case GzipHeaderState.Done:
					{
						goto IL_3FE;
					}
				default:
					{
						throw new InvalidDataException("UnknownState");
					}
			}
			bits = this.input.GetBits(8);
			if (bits < 0)
			{
				result = false;
				return result;
			}
			if (bits != 139)
			{
				throw new InvalidDataException("CorruptedGZipHeader");
			}
			this.gzipHeaderSubstate = GzipHeaderState.ReadingCM;
		IL_114:
			bits = this.input.GetBits(8);
			if (bits < 0)
			{
				result = false;
				return result;
			}
			if (bits != 8)
			{
				throw new InvalidDataException("UnknownCompressionMode");
			}
			this.gzipHeaderSubstate = GzipHeaderState.ReadingFLG;
		IL_14F:
			bits = this.input.GetBits(8);
			if (bits < 0)
			{
				result = false;
				return result;
			}
			this.gzip_header_flag = bits;
			this.gzipHeaderSubstate = GzipHeaderState.ReadingMMTime;
			this.loopCounter = 0;
		IL_184:
			while (this.loopCounter < 4)
			{
				if (this.input.GetBits(8) < 0)
				{
					result = false;
					return result;
				}
				this.loopCounter++;
			}
			this.gzipHeaderSubstate = GzipHeaderState.ReadingXFL;
			this.loopCounter = 0;
		IL_1D1:
			if (this.input.GetBits(8) < 0)
			{
				result = false;
				return result;
			}
			this.gzipHeaderSubstate = GzipHeaderState.ReadingOS;
		IL_1F6:
			if (this.input.GetBits(8) < 0)
			{
				result = false;
				return result;
			}
			this.gzipHeaderSubstate = GzipHeaderState.ReadingXLen1;
		IL_21B:
			if ((this.gzip_header_flag & 4) == 0)
			{
				goto IL_2F3;
			}
			bits = this.input.GetBits(8);
			if (bits < 0)
			{
				result = false;
				return result;
			}
			this.gzip_header_xlen = bits;
			this.gzipHeaderSubstate = GzipHeaderState.ReadingXLen2;
		IL_261:
			bits = this.input.GetBits(8);
			if (bits < 0)
			{
				result = false;
				return result;
			}
			this.gzip_header_xlen |= bits << 8;
			this.gzipHeaderSubstate = GzipHeaderState.ReadingXLenData;
			this.loopCounter = 0;
		IL_2A0:
			while (this.loopCounter < this.gzip_header_xlen)
			{
				if (this.input.GetBits(8) < 0)
				{
					result = false;
					return result;
				}
				this.loopCounter++;
			}
			this.gzipHeaderSubstate = GzipHeaderState.ReadingFileName;
			this.loopCounter = 0;
		IL_2F3:
			if ((this.gzip_header_flag & 8) == 0)
			{
				this.gzipHeaderSubstate = GzipHeaderState.ReadingComment;
			}
			else
			{
				while (true)
				{
					bits = this.input.GetBits(8);
					if (bits < 0)
					{
						break;
					}
					if (bits == 0)
					{
						goto Block_20;
					}
				}
				result = false;
				return result;
			Block_20:
				this.gzipHeaderSubstate = GzipHeaderState.ReadingComment;
			}
		IL_348:
			if ((this.gzip_header_flag & 16) == 0)
			{
				this.gzipHeaderSubstate = GzipHeaderState.ReadingCRC16Part1;
			}
			else
			{
				while (true)
				{
					bits = this.input.GetBits(8);
					if (bits < 0)
					{
						break;
					}
					if (bits == 0)
					{
						goto Block_23;
					}
				}
				result = false;
				return result;
			Block_23:
				this.gzipHeaderSubstate = GzipHeaderState.ReadingCRC16Part1;
			}
		IL_39B:
			if ((this.gzip_header_flag & 2) == 0)
			{
				this.gzipHeaderSubstate = GzipHeaderState.Done;
				goto IL_3FE;
			}
			if (this.input.GetBits(8) < 0)
			{
				result = false;
				return result;
			}
			this.gzipHeaderSubstate = GzipHeaderState.ReadingCRC16Part2;
		IL_3DB:
			if (this.input.GetBits(8) < 0)
			{
				result = false;
				return result;
			}
			this.gzipHeaderSubstate = GzipHeaderState.Done;
		IL_3FE:
			result = true;
			return result;
		}
		public void Reset()
		{
			this.gzipHeaderSubstate = GzipHeaderState.ReadingID1;
			this.gzipFooterSubstate = GzipHeaderState.ReadingCRC;
			this.gzipCrc32 = 0u;
			this.gzipOutputStreamSize = 0u;
		}
	}
}
