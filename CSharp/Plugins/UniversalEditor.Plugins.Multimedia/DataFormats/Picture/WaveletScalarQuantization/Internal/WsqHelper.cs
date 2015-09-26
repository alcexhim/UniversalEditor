using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.WaveletScalarQuantization
{
	public struct WavletTree
	{
		public int x;
		public int y;
		public int lenx;
		public int leny;
		public int invrw;
		public int invcl;
	}

	public struct TableDTT
	{
		public float[] lofilt;
		public float[] hifilt;
		public int losz;
		public int hisz;
		public int lodef;
		public int hidef;
	}

	public struct HuffCode
	{
		public int size;
		public int code;
	}

	public struct HeaderFrm
	{
		public int black;
		public int white;
		public int width;
		public int height;
		public float mShift;
		public float rScale;
		public int wsqEncoder;
		public int software;
	}

	public struct HuffmanTable
	{
		public int tableLen;
		public int bytesLeft;
		public int tableId;
		public int[] huffbits;
		public int[] huffvalues;
	}

	public class TableDHT
	{
		private const int MAX_HUFFBITS = 16; /*DO NOT CHANGE THIS CONSTANT!! */
		private const int MAX_HUFFCOUNTS_WSQ = 256; /* Length of code table: change as needed */

		public byte tabdef;
		public int[] huffbits = new int[MAX_HUFFBITS];
		public int[] huffvalues = new int[MAX_HUFFCOUNTS_WSQ + 1];
	}

	public class Table_DQT
	{
		public const int MAX_SUBBANDS = 64;
		public float binCenter;
		public float[] qBin = new float[MAX_SUBBANDS];
		public float[] zBin = new float[MAX_SUBBANDS];
		public int dqtDef;
	}

	public class QuantTree
	{
		public int x;    /* UL corner of block */
		public int y;
		public int lenx;  /* block size */
		public int leny;  /* block size */
	}

	public class IntRef
	{
		public int value;


		public IntRef()
		{
		}

		public IntRef(int value)
		{
			this.value = value;
		}
	}

	internal class Token
	{
		public TableDHT[] tableDHT;
		public TableDTT tableDTT;
		public Table_DQT tableDQT;

		public WavletTree[] wtree;
		public QuantTree[] qtree;


		public byte[] buffer;
		public int pointer;

		public Token(byte[] buffer)
		{
			this.buffer = buffer;
			this.pointer = 0;
		}

		public void initialize()
		{
			tableDTT = new TableDTT();
			tableDQT = new Table_DQT();

			/* Init DHT Tables to 0. */
			tableDHT = new TableDHT[Internal.Constants.MAX_DHT_TABLES];
			for (int i = 0; i < Internal.Constants.MAX_DHT_TABLES; i++)
			{
				tableDHT[i] = new TableDHT();
				tableDHT[i].tabdef = 0;
			}
		}

		public long readInt()
		{
			byte byte1 = buffer[pointer++];
			byte byte2 = buffer[pointer++];
			byte byte3 = buffer[pointer++];
			byte byte4 = buffer[pointer++];

			return (0xffL & byte1) << 24 | (0xffL & byte2) << 16 | (0xffL & byte3) << 8 | (0xffL & byte4);
		}

		public int readShort()
		{
			int byte1 = buffer[pointer++];
			int byte2 = buffer[pointer++];

			return (0xff & byte1) << 8 | (0xff & byte2);
		}

		public int readByte()
		{
			byte byte1 = buffer[pointer++];

			return 0xff & byte1;
		}

		public byte[] readBytes(int size)
		{
			byte[] bytes = new byte[size];

			for (int i = 0; i < size; i++)
			{
				bytes[i] = buffer[pointer++];
			}

			return bytes;
		}
	}

	//public class Bitmap
	//{
	//    private int width;
	//    private int height;
	//    private int ppi;
	//    private int depth;
	//    private int lossyflag;

	//    private byte[] pixels;
	//    private int length;

	//    public Bitmap(byte[] pixels, int width, int height, int ppi, int depth, int lossyflag) 
	//    {
	//        this.pixels = pixels;
	//        this.length = pixels != null ? pixels.Length : 0;

	//        this.width = width;
	//        this.height = height;
	//        this.ppi = ppi;
	//        this.depth = depth;
	//        this.lossyflag = lossyflag;
	//    }


	//    public int getWidth() 
	//    {
	//        return width;
	//    }

	//    public int getHeight() 
	//    {
	//        return height;
	//    }

	//    public int getPpi() 
	//    {
	//        return ppi;
	//    }

	//    public byte[] getPixels() 
	//    {
	//        return pixels;
	//    }

	//    public int getLength() 
	//    {
	//        return length;
	//    }

	//    public int getDepth() 
	//    {
	//        return depth;
	//    }

	//    public int getLossyflag() 
	//    {
	//        return lossyflag;
	//    }
	//}
}
