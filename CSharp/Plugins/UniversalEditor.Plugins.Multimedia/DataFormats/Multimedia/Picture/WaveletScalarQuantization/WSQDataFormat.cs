using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.WaveletScalarQuantization
{
	public class WSQDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Wavelet Scalar Quantization image", new byte?[][] { new byte?[] { 0xA0, 0xFF } }, new string[] { "*.wsq" });
			}
			return _dfr;
		}

		private class WSQData
		{
			public static TableDHT[] tableDHT;
			public static TableDTT tableDTT;
			public static Table_DQT tableDQT;

			public static WavletTree[] wtree;
			public static QuantTree[] qtree;

			public static void Initialize()
			{
				tableDTT = new TableDTT();
				tableDQT = new Table_DQT();

				// Initialize DHT tables to 0
				tableDHT = new TableDHT[Internal.Constants.MAX_DHT_TABLES];
				for (int i = 0; i < Internal.Constants.MAX_DHT_TABLES; i++)
				{
					tableDHT[i] = new TableDHT();
					tableDHT[i].tabdef = 0;
				}
			}
		}


		private int intSign(int power)
		{
			/* "sign" power */
			int cnt;        /* counter */
			int num = -1;   /* sign return value */

			if (power == 0)
			{
				return 1;
			}
			for (cnt = 1; cnt < power; cnt++)
			{
				num *= -1;
			}
			return num;
		}

		private HuffmanTable getCHuffmanTable(IO.Reader br, int maxHuffcounts, int bytesLeft, bool readTableLen)
		{

			HuffmanTable huffmanTable = new HuffmanTable();

			/* table_len */
			if (readTableLen)
			{
				huffmanTable.tableLen = br.ReadInt16();
				huffmanTable.bytesLeft = huffmanTable.tableLen - 2;
				bytesLeft = huffmanTable.bytesLeft;
			}
			else
			{
				huffmanTable.bytesLeft = bytesLeft;
			}

			/* If no bytes left ... */
			if (bytesLeft <= 0)
			{
				throw new SystemException("ERROR : getCHuffmanTable : no huffman table bytes remaining");
			}

			/* Table ID */
			huffmanTable.tableId = br.ReadByte();
			huffmanTable.bytesLeft--;


			huffmanTable.huffbits = new int[Internal.Constants.MAX_HUFFBITS];
			int numHufvals = 0;
			/* L1 ... L16 */
			for (int i = 0; i < Internal.Constants.MAX_HUFFBITS; i++)
			{
				huffmanTable.huffbits[i] = br.ReadByte();
				numHufvals += huffmanTable.huffbits[i];
			}
			huffmanTable.bytesLeft -= Internal.Constants.MAX_HUFFBITS;

			if (numHufvals > maxHuffcounts + 1)
			{
				throw new SystemException("ERROR : getCHuffmanTable : numHufvals is larger than MAX_HUFFCOUNTS");
			}

			// Could allocate only the amount needed ... then we wouldn't
			// need to pass MAX_HUFFCOUNTS
			huffmanTable.huffvalues = new int[maxHuffcounts + 1];

			// V1,1 ... V16,16
			for (int i = 0; i < numHufvals; i++)
			{
				huffmanTable.huffvalues[i] = br.ReadByte();
			}
			huffmanTable.bytesLeft -= numHufvals;

			return huffmanTable;
		}

		private void wtree4(int start1, int start2, int lenx, int leny, int x, int y, int stop1)
		{
			int evenx, eveny;   /* Check length of subband for even or odd */
			int p1, p2;         /* w_tree locations for storing subband sizes and locations */

			p1 = start1;
			p2 = start2;

			evenx = lenx % 2;
			eveny = leny % 2;

			WSQData.wtree[p1].x = x;
			WSQData.wtree[p1].y = y;
			WSQData.wtree[p1].lenx = lenx;
			WSQData.wtree[p1].leny = leny;

			WSQData.wtree[p2].x = x;
			WSQData.wtree[p2 + 2].x = x;
			WSQData.wtree[p2].y = y;
			WSQData.wtree[p2 + 1].y = y;

			if (evenx == 0)
			{
				WSQData.wtree[p2].lenx = lenx / 2;
				WSQData.wtree[p2 + 1].lenx = WSQData.wtree[p2].lenx;
			}
			else
			{
				if (p1 == 4)
				{
					WSQData.wtree[p2].lenx = (lenx - 1) / 2;
					WSQData.wtree[p2 + 1].lenx = WSQData.wtree[p2].lenx + 1;
				}
				else
				{
					WSQData.wtree[p2].lenx = (lenx + 1) / 2;
					WSQData.wtree[p2 + 1].lenx = WSQData.wtree[p2].lenx - 1;
				}
			}
			WSQData.wtree[p2 + 1].x = WSQData.wtree[p2].lenx + x;
			if (stop1 == 0)
			{
				WSQData.wtree[p2 + 3].lenx = WSQData.wtree[p2 + 1].lenx;
				WSQData.wtree[p2 + 3].x = WSQData.wtree[p2 + 1].x;
			}
			WSQData.wtree[p2 + 2].lenx = WSQData.wtree[p2].lenx;


			if (eveny == 0)
			{
				WSQData.wtree[p2].leny = leny / 2;
				WSQData.wtree[p2 + 2].leny = WSQData.wtree[p2].leny;
			}
			else
			{
				if (p1 == 5)
				{
					WSQData.wtree[p2].leny = (leny - 1) / 2;
					WSQData.wtree[p2 + 2].leny = WSQData.wtree[p2].leny + 1;
				}
				else
				{
					WSQData.wtree[p2].leny = (leny + 1) / 2;
					WSQData.wtree[p2 + 2].leny = WSQData.wtree[p2].leny - 1;
				}
			}
			WSQData.wtree[p2 + 2].y = WSQData.wtree[p2].leny + y;
			if (stop1 == 0)
			{
				WSQData.wtree[p2 + 3].leny = WSQData.wtree[p2 + 2].leny;
				WSQData.wtree[p2 + 3].y = WSQData.wtree[p2 + 2].y;
			}
			WSQData.wtree[p2 + 1].leny = WSQData.wtree[p2].leny;
		}
		private void qtree16(int start, int lenx, int leny, int x, int y, int rw, int cl)
		{
			int tempx, temp2x;   /* temporary x values */
			int tempy, temp2y;   /* temporary y values */
			int evenx, eveny;    /* Check length of subband for even or odd */
			int p;               /* indicates subband information being stored */

			p = start;
			evenx = lenx % 2;
			eveny = leny % 2;

			if (evenx == 0)
			{
				tempx = lenx / 2;
				temp2x = tempx;
			}
			else
			{
				if (cl != 0)
				{
					temp2x = (lenx + 1) / 2;
					tempx = temp2x - 1;
				}
				else
				{
					tempx = (lenx + 1) / 2;
					temp2x = tempx - 1;
				}
			}

			if (eveny == 0)
			{
				tempy = leny / 2;
				temp2y = tempy;
			}
			else
			{
				if (rw != 0)
				{
					temp2y = (leny + 1) / 2;
					tempy = temp2y - 1;
				}
				else
				{
					tempy = (leny + 1) / 2;
					temp2y = tempy - 1;
				}
			}

			evenx = tempx % 2;
			eveny = tempy % 2;

			WSQData.qtree[p].x = x;
			WSQData.qtree[p + 2].x = x;
			WSQData.qtree[p].y = y;
			WSQData.qtree[p + 1].y = y;
			if (evenx == 0)
			{
				WSQData.qtree[p].lenx = tempx / 2;
				WSQData.qtree[p + 1].lenx = WSQData.qtree[p].lenx;
				WSQData.qtree[p + 2].lenx = WSQData.qtree[p].lenx;
				WSQData.qtree[p + 3].lenx = WSQData.qtree[p].lenx;
			}
			else
			{
				WSQData.qtree[p].lenx = (tempx + 1) / 2;
				WSQData.qtree[p + 1].lenx = WSQData.qtree[p].lenx - 1;
				WSQData.qtree[p + 2].lenx = WSQData.qtree[p].lenx;
				WSQData.qtree[p + 3].lenx = WSQData.qtree[p + 1].lenx;
			}
			WSQData.qtree[p + 1].x = x + WSQData.qtree[p].lenx;
			WSQData.qtree[p + 3].x = WSQData.qtree[p + 1].x;
			if (eveny == 0)
			{
				WSQData.qtree[p].leny = tempy / 2;
				WSQData.qtree[p + 1].leny = WSQData.qtree[p].leny;
				WSQData.qtree[p + 2].leny = WSQData.qtree[p].leny;
				WSQData.qtree[p + 3].leny = WSQData.qtree[p].leny;
			}
			else
			{
				WSQData.qtree[p].leny = (tempy + 1) / 2;
				WSQData.qtree[p + 1].leny = WSQData.qtree[p].leny;
				WSQData.qtree[p + 2].leny = WSQData.qtree[p].leny - 1;
				WSQData.qtree[p + 3].leny = WSQData.qtree[p + 2].leny;
			}
			WSQData.qtree[p + 2].y = y + WSQData.qtree[p].leny;
			WSQData.qtree[p + 3].y = WSQData.qtree[p + 2].y;


			evenx = temp2x % 2;

			WSQData.qtree[p + 4].x = x + tempx;
			WSQData.qtree[p + 6].x = WSQData.qtree[p + 4].x;
			WSQData.qtree[p + 4].y = y;
			WSQData.qtree[p + 5].y = y;
			WSQData.qtree[p + 6].y = WSQData.qtree[p + 2].y;
			WSQData.qtree[p + 7].y = WSQData.qtree[p + 2].y;
			WSQData.qtree[p + 4].leny = WSQData.qtree[p].leny;
			WSQData.qtree[p + 5].leny = WSQData.qtree[p].leny;
			WSQData.qtree[p + 6].leny = WSQData.qtree[p + 2].leny;
			WSQData.qtree[p + 7].leny = WSQData.qtree[p + 2].leny;
			if (evenx == 0)
			{
				WSQData.qtree[p + 4].lenx = temp2x / 2;
				WSQData.qtree[p + 5].lenx = WSQData.qtree[p + 4].lenx;
				WSQData.qtree[p + 6].lenx = WSQData.qtree[p + 4].lenx;
				WSQData.qtree[p + 7].lenx = WSQData.qtree[p + 4].lenx;
			}
			else
			{
				WSQData.qtree[p + 5].lenx = (temp2x + 1) / 2;
				WSQData.qtree[p + 4].lenx = WSQData.qtree[p + 5].lenx - 1;
				WSQData.qtree[p + 6].lenx = WSQData.qtree[p + 4].lenx;
				WSQData.qtree[p + 7].lenx = WSQData.qtree[p + 5].lenx;
			}
			WSQData.qtree[p + 5].x = WSQData.qtree[p + 4].x + WSQData.qtree[p + 4].lenx;
			WSQData.qtree[p + 7].x = WSQData.qtree[p + 5].x;


			eveny = temp2y % 2;

			WSQData.qtree[p + 8].x = x;
			WSQData.qtree[p + 9].x = WSQData.qtree[p + 1].x;
			WSQData.qtree[p + 10].x = x;
			WSQData.qtree[p + 11].x = WSQData.qtree[p + 1].x;
			WSQData.qtree[p + 8].y = y + tempy;
			WSQData.qtree[p + 9].y = WSQData.qtree[p + 8].y;
			WSQData.qtree[p + 8].lenx = WSQData.qtree[p].lenx;
			WSQData.qtree[p + 9].lenx = WSQData.qtree[p + 1].lenx;
			WSQData.qtree[p + 10].lenx = WSQData.qtree[p].lenx;
			WSQData.qtree[p + 11].lenx = WSQData.qtree[p + 1].lenx;
			if (eveny == 0)
			{
				WSQData.qtree[p + 8].leny = temp2y / 2;
				WSQData.qtree[p + 9].leny = WSQData.qtree[p + 8].leny;
				WSQData.qtree[p + 10].leny = WSQData.qtree[p + 8].leny;
				WSQData.qtree[p + 11].leny = WSQData.qtree[p + 8].leny;
			}
			else
			{
				WSQData.qtree[p + 10].leny = (temp2y + 1) / 2;
				WSQData.qtree[p + 11].leny = WSQData.qtree[p + 10].leny;
				WSQData.qtree[p + 8].leny = WSQData.qtree[p + 10].leny - 1;
				WSQData.qtree[p + 9].leny = WSQData.qtree[p + 8].leny;
			}
			WSQData.qtree[p + 10].y = WSQData.qtree[p + 8].y + WSQData.qtree[p + 8].leny;
			WSQData.qtree[p + 11].y = WSQData.qtree[p + 10].y;


			WSQData.qtree[p + 12].x = WSQData.qtree[p + 4].x;
			WSQData.qtree[p + 13].x = WSQData.qtree[p + 5].x;
			WSQData.qtree[p + 14].x = WSQData.qtree[p + 4].x;
			WSQData.qtree[p + 15].x = WSQData.qtree[p + 5].x;
			WSQData.qtree[p + 12].y = WSQData.qtree[p + 8].y;
			WSQData.qtree[p + 13].y = WSQData.qtree[p + 8].y;
			WSQData.qtree[p + 14].y = WSQData.qtree[p + 10].y;
			WSQData.qtree[p + 15].y = WSQData.qtree[p + 10].y;
			WSQData.qtree[p + 12].lenx = WSQData.qtree[p + 4].lenx;
			WSQData.qtree[p + 13].lenx = WSQData.qtree[p + 5].lenx;
			WSQData.qtree[p + 14].lenx = WSQData.qtree[p + 4].lenx;
			WSQData.qtree[p + 15].lenx = WSQData.qtree[p + 5].lenx;
			WSQData.qtree[p + 12].leny = WSQData.qtree[p + 8].leny;
			WSQData.qtree[p + 13].leny = WSQData.qtree[p + 8].leny;
			WSQData.qtree[p + 14].leny = WSQData.qtree[p + 10].leny;
			WSQData.qtree[p + 15].leny = WSQData.qtree[p + 10].leny;
		}
		private void qtree4(int start, int lenx, int leny, int x, int y)
		{
			int evenx, eveny;    /* Check length of subband for even or odd */
			int p;               /* indicates subband information being stored */

			p = start;
			evenx = lenx % 2;
			eveny = leny % 2;


			WSQData.qtree[p].x = x;
			WSQData.qtree[p + 2].x = x;
			WSQData.qtree[p].y = y;
			WSQData.qtree[p + 1].y = y;
			if (evenx == 0)
			{
				WSQData.qtree[p].lenx = lenx / 2;
				WSQData.qtree[p + 1].lenx = WSQData.qtree[p].lenx;
				WSQData.qtree[p + 2].lenx = WSQData.qtree[p].lenx;
				WSQData.qtree[p + 3].lenx = WSQData.qtree[p].lenx;
			}
			else
			{
				WSQData.qtree[p].lenx = (lenx + 1) / 2;
				WSQData.qtree[p + 1].lenx = WSQData.qtree[p].lenx - 1;
				WSQData.qtree[p + 2].lenx = WSQData.qtree[p].lenx;
				WSQData.qtree[p + 3].lenx = WSQData.qtree[p + 1].lenx;
			}
			WSQData.qtree[p + 1].x = x + WSQData.qtree[p].lenx;
			WSQData.qtree[p + 3].x = WSQData.qtree[p + 1].x;
			if (eveny == 0)
			{
				WSQData.qtree[p].leny = leny / 2;
				WSQData.qtree[p + 1].leny = WSQData.qtree[p].leny;
				WSQData.qtree[p + 2].leny = WSQData.qtree[p].leny;
				WSQData.qtree[p + 3].leny = WSQData.qtree[p].leny;
			}
			else
			{
				WSQData.qtree[p].leny = (leny + 1) / 2;
				WSQData.qtree[p + 1].leny = WSQData.qtree[p].leny;
				WSQData.qtree[p + 2].leny = WSQData.qtree[p].leny - 1;
				WSQData.qtree[p + 3].leny = WSQData.qtree[p + 2].leny;
			}
			WSQData.qtree[p + 2].y = y + WSQData.qtree[p].leny;
			WSQData.qtree[p + 3].y = WSQData.qtree[p + 2].y;
		}

		private byte[] ImageToByteArray(float[] img, int width, int height, float mShift, float rScale)
		{
			byte[] data = new byte[width * height];

			int idx = 0;
			for (int r = 0; r < height; r++)
			{
				for (int c = 0; c < width; c++)
				{
					float pixel = (img[idx] * rScale) + mShift;
					pixel += 0.5F;

					if (pixel < 0.0)
					{
						data[idx] = 0; // neg pix poss after quantization
					}
					else if (pixel > 255.0)
					{
						data[idx] = (byte)255;
					}
					else
					{
						data[idx] = (byte)pixel;
					}
					idx++;
				}
			}

			return data;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="newdata"></param>
		/// <param name="olddata"></param>
		/// <param name="newIndex"></param>
		/// <param name="oldIndex"></param>
		/// <param name="len1">temporary length parameters</param>
		/// <param name="len2">temporary length parameters</param>
		/// <param name="pitch">The next row_col to filter</param>
		/// <param name="stride">The next pixel to filter</param>
		/// <param name="hi"></param>
		/// <param name="hsz">NEW</param>
		/// <param name="lo">Filter coefficients</param>
		/// <param name="lsz">NEW</param>
		/// <param name="inv">Spectral inversion</param>
		private void JoinLets(float[] newdata, float[] olddata, int newIndex, int oldIndex, int len1, int len2, int pitch, int stride, float[] hi, int hsz, float[] lo, int lsz, int inv)
		{
			int lp0, lp1;
			int hp0, hp1;
			int lopass, hipass;   // lo/hi pass image pointers
			int limg, himg;
			int pix, cl_rw;      // pixel counter and column/row counter
			int i, da_ev;         // if "scanline" is even or odd and
			int loc, hoc;
			int hlen, llen;
			int nstr, pstr;
			int tap;
			int fi_ev;
			int olle, ohle, olre, ohre;
			int lle, lle2, lre, lre2;
			int hle, hle2, hre, hre2;
			int lpx, lspx;
			int lpxstr, lspxstr;
			int lstap, lotap;
			int hpx, hspx;
			int hpxstr, hspxstr;
			int hstap, hotap;
			int asym, fhre = 0, ofhre;
			float ssfac, osfac, sfac;

			da_ev = len2 % 2;
			fi_ev = lsz % 2;
			pstr = stride;
			nstr = -pstr;
			if (da_ev != 0)
			{
				llen = (len2 + 1) / 2;
				hlen = llen - 1;
			}
			else
			{
				llen = len2 / 2;
				hlen = llen;
			}

			if (fi_ev != 0)
			{
				asym = 0;
				ssfac = 1.0f;
				ofhre = 0;
				loc = (lsz - 1) / 4;
				hoc = (hsz + 1) / 4 - 1;
				lotap = ((lsz - 1) / 2) % 2;
				hotap = ((hsz + 1) / 2) % 2;
				if (da_ev != 0)
				{
					olle = 0;
					olre = 0;
					ohle = 1;
					ohre = 1;
				}
				else
				{
					olle = 0;
					olre = 1;
					ohle = 1;
					ohre = 0;
				}
			}
			else
			{
				asym = 1;
				ssfac = -1.0f;
				ofhre = 2;
				loc = lsz / 4 - 1;
				hoc = hsz / 4 - 1;
				lotap = (lsz / 2) % 2;
				hotap = (hsz / 2) % 2;
				if (da_ev != 0)
				{
					olle = 1;
					olre = 0;
					ohle = 1;
					ohre = 1;
				}
				else
				{
					olle = 1;
					olre = 1;
					ohle = 1;
					ohre = 1;
				}

				if (loc == -1)
				{
					loc = 0;
					olle = 0;
				}
				if (hoc == -1)
				{
					hoc = 0;
					ohle = 0;
				}

				for (i = 0; i < hsz; i++)
				{
					hi[i] *= -1.0F;
				}
			}


			for (cl_rw = 0; cl_rw < len1; cl_rw++)
			{
				limg = newIndex + cl_rw * pitch;
				himg = limg;
				newdata[himg] = 0.0f;
				newdata[himg + stride] = 0.0f;
				if (inv != 0)
				{
					hipass = oldIndex + cl_rw * pitch;
					lopass = hipass + stride * hlen;
				}
				else
				{
					lopass = oldIndex + cl_rw * pitch;
					hipass = lopass + stride * llen;
				}


				lp0 = lopass;
				lp1 = lp0 + (llen - 1) * stride;
				lspx = lp0 + (loc * stride);
				lspxstr = nstr;
				lstap = lotap;
				lle2 = olle;
				lre2 = olre;

				hp0 = hipass;
				hp1 = hp0 + (hlen - 1) * stride;
				hspx = hp0 + (hoc * stride);
				hspxstr = nstr;
				hstap = hotap;
				hle2 = ohle;
				hre2 = ohre;
				osfac = ssfac;

				for (pix = 0; pix < hlen; pix++)
				{
					for (tap = lstap; tap >= 0; tap--)
					{
						lle = lle2;
						lre = lre2;
						lpx = lspx;
						lpxstr = lspxstr;

						newdata[limg] = olddata[lpx] * lo[tap];
						for (i = tap + 2; i < lsz; i += 2)
						{
							if (lpx == lp0)
							{
								if (lle != 0)
								{
									lpxstr = 0;
									lle = 0;
								}
								else
								{
									lpxstr = pstr;
								}
							}
							if (lpx == lp1)
							{
								if (lre != 0)
								{
									lpxstr = 0;
									lre = 0;
								}
								else
								{
									lpxstr = nstr;
								}
							}
							lpx += lpxstr;
							newdata[limg] += olddata[lpx] * lo[i];
						}
						limg += stride;
					}
					if (lspx == lp0)
					{
						if (lle2 != 0)
						{
							lspxstr = 0;
							lle2 = 0;
						}
						else
						{
							lspxstr = pstr;
						}
					}
					lspx += lspxstr;
					lstap = 1;

					for (tap = hstap; tap >= 0; tap--)
					{
						hle = hle2;
						hre = hre2;
						hpx = hspx;
						hpxstr = hspxstr;
						fhre = ofhre;
						sfac = osfac;

						for (i = tap; i < hsz; i += 2)
						{
							if (hpx == hp0)
							{
								if (hle != 0)
								{
									hpxstr = 0;
									hle = 0;
								}
								else
								{
									hpxstr = pstr;
									sfac = 1.0f;
								}
							}
							if (hpx == hp1)
							{
								if (hre != 0)
								{
									hpxstr = 0;
									hre = 0;
									if (asym != 0 && da_ev != 0)
									{
										hre = 1;
										fhre--;
										sfac = (float)fhre;
										if (sfac == 0.0)
										{
											hre = 0;
										}
									}
								}
								else
								{
									hpxstr = nstr;
									if (asym != 0)
									{
										sfac = -1.0f;
									}
								}
							}
							newdata[himg] += olddata[hpx] * hi[i] * sfac;
							hpx += hpxstr;
						}
						himg += stride;
					}
					if (hspx == hp0)
					{
						if (hle2 != 0)
						{
							hspxstr = 0;
							hle2 = 0;
						}
						else
						{
							hspxstr = pstr;
							osfac = 1.0f;
						}
					}
					hspx += hspxstr;
					hstap = 1;
				}


				if (da_ev != 0)
				{
					if (lotap != 0)
					{
						lstap = 1;
					}
					else
					{
						lstap = 0;
					}
				}
				else if (lotap != 0)
				{
					lstap = 2;
				}
				else
				{
					lstap = 1;
				}
				for (tap = 1; tap >= lstap; tap--)
				{
					lle = lle2;
					lre = lre2;
					lpx = lspx;
					lpxstr = lspxstr;

					newdata[limg] = olddata[lpx] * lo[tap];
					for (i = tap + 2; i < lsz; i += 2)
					{
						if (lpx == lp0)
						{
							if (lle != 0)
							{
								lpxstr = 0;
								lle = 0;
							}
							else
							{
								lpxstr = pstr;
							}
						}
						if (lpx == lp1)
						{
							if (lre != 0)
							{
								lpxstr = 0;
								lre = 0;
							}
							else
							{
								lpxstr = nstr;
							}
						}
						lpx += lpxstr;
						newdata[limg] += olddata[lpx] * lo[i];
					}
					limg += stride;
				}


				if (da_ev != 0)
				{
					if (hotap != 0)
					{
						hstap = 1;
					}
					else
					{
						hstap = 0;
					}
					if (hsz == 2)
					{
						hspx -= hspxstr;
						fhre = 1;
					}
				}
				else if (hotap != 0)
				{
					hstap = 2;
				}
				else
				{
					hstap = 1;
				}

				for (tap = 1; tap >= hstap; tap--)
				{
					hle = hle2;
					hre = hre2;
					hpx = hspx;
					hpxstr = hspxstr;
					sfac = osfac;
					if (hsz != 2)
					{
						fhre = ofhre;
					}
					for (i = tap; i < hsz; i += 2)
					{
						if (hpx == hp0)
						{
							if (hle != 0)
							{
								hpxstr = 0;
								hle = 0;
							}
							else
							{
								hpxstr = pstr;
								sfac = 1.0f;
							}
						}
						if (hpx == hp1)
						{
							if (hre != 0)
							{
								hpxstr = 0;
								hre = 0;
								if (asym != 0 && da_ev != 0)
								{
									hre = 1;
									fhre--;
									sfac = (float)fhre;
									if (sfac == 0.0)
									{
										hre = 0;
									}
								}
							}
							else
							{
								hpxstr = nstr;
								if (asym != 0)
									sfac = -1.0f;
							}
						}
						newdata[himg] += olddata[hpx] * hi[i] * sfac;
						hpx += hpxstr;
					}
					himg += stride;
				}
			}

			if (fi_ev == 0)
			{
				for (i = 0; i < hsz; i++)
				{
					hi[i] *= -1.0F;
				}
			}
		}
		private void ReconstructWSQ(float[] fdata, int width, int height)
		{
			if (WSQData.tableDTT.lodef != 1)
			{
				throw new SystemException("ERROR: ReconstructWSQ : Lopass filter coefficients not defined");
			}

			if (WSQData.tableDTT.hidef != 1)
			{
				throw new SystemException("ERROR: ReconstructWSQ : Hipass filter coefficients not defined");
			}

			int numPix = width * height;
			// Allocate temporary floating point pixmap
			float[] fdataTemp = new float[numPix];

			// Reconstruct floating point pixmap from wavelet subband buffer
			for (int node = Internal.Constants.W_TREELEN - 1; node >= 0; node--)
			{
				int fdataBse = (WSQData.wtree[node].y * width) + WSQData.wtree[node].x;
				JoinLets(fdataTemp, fdata, 0, fdataBse, WSQData.wtree[node].lenx, WSQData.wtree[node].leny, 1, width, WSQData.tableDTT.hifilt, WSQData.tableDTT.hisz, WSQData.tableDTT.lofilt, WSQData.tableDTT.losz, WSQData.wtree[node].invcl);
				JoinLets(fdata, fdataTemp, fdataBse, 0, WSQData.wtree[node].leny, WSQData.wtree[node].lenx, width, 1, WSQData.tableDTT.hifilt, WSQData.tableDTT.hisz, WSQData.tableDTT.lofilt, WSQData.tableDTT.losz, WSQData.wtree[node].invrw);
			}
		}
		
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) return;

			br.Endianness = IO.Endianness.BigEndian;

			// Initialize WSQ data structures
			WSQData.Initialize();

			// Read the SOI marker
			ushort signature = br.ReadUInt16();
			if (signature != Internal.Constants.SOI_WSQ) throw new InvalidDataFormatException("Invalid Wavelet Scalar Quantization image");

			#region Supporting Tables
			// Read in supporting tables up to the SOF marker
			bool done = false;
			do
			{
				signature = br.ReadUInt16();
				switch (signature)
				{
					case Internal.Constants.SOF_WSQ:
					{
						done = true;
						break;
					}
					case Internal.Constants.DTT_WSQ:
					{
						ProcessTransformTable(br);
						break;
					}
					case Internal.Constants.DQT_WSQ:
					{
						ProcessQuantizationTable(br);
						break;
					}
					case Internal.Constants.DHT_WSQ:
					{
						ProcessHuffmanTable(br);
						break;
					}
					case Internal.Constants.COM_WSQ:
					{
						ProcessComment(br);
						break;
					}
					default:
					{
						throw new SystemException("ERROR: getCTableWSQ : Invalid table defined : " + signature.ToString());
					}
				}
			}
			while ((signature != Internal.Constants.SOF_WSQ) || (!done));
			#endregion
			#region Frame Header
			// Read in the Frame Header
			short hdrSize = br.ReadInt16(); /* header size */

			byte black = br.ReadByte();
			byte white = br.ReadByte();
			short height = br.ReadInt16();
			short width = br.ReadInt16();
			byte hScale = br.ReadByte(); // exponent scaling parameter
			short hShrtDat = br.ReadInt16(); // buffer pointer
			float mShift = (float)hShrtDat;
			while (hScale > 0)
			{
				mShift /= 10.0F;
				hScale--;
			}

			hScale = br.ReadByte();
			hShrtDat = br.ReadInt16();
			float rScale = (float)hShrtDat;
			while (hScale > 0)
			{
				rScale /= 10.0F;
				hScale--;
			}

			byte wsqEncoder = br.ReadByte();
			short software = br.ReadInt16();

			#endregion
			
			int ppi = -1;
			#region WSQ Decomposition Trees
			// Build a W-TREE structure for the image
			BuildWTree(width, height);
			BuildQTree();
			#endregion

			// Decode the Huffman encoded buffer blocks
			int[] qdata = DecodeHuffmanTable(br, width * height);

			// Decode the quantize wavelet subband buffer
			float[] fdata = Unquantize(qdata, width, height);

			// Done with quantized wavelet subband buffer
			qdata = null;

			ReconstructWSQ(fdata, width, height);

			// Convert floating point pixels to unsigned char pixels
			byte[] cdata = ImageToByteArray(fdata, width, height, mShift, rScale);
			fdata = null;

			pic.Width = width;
			pic.Height = height;
			int pix = 0;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					pic.SetPixel(Color.FromRGBA(cdata[pix], cdata[pix], cdata[pix]), j, i);
					pix++;
				}
			}
		}

		private void ProcessTable(IO.Reader br, int marker)
		{
			switch (marker)
			{
				case Internal.Constants.DTT_WSQ:
				{
					ProcessTransformTable(br);
					return;
				}
				case Internal.Constants.DQT_WSQ:
				{
					ProcessQuantizationTable(br);
					return;
				}
				case Internal.Constants.DHT_WSQ:
				{
					ProcessHuffmanTable(br);
					return;
				}
				case Internal.Constants.COM_WSQ:
				{
					ProcessComment(br);
					return;
				}
				default:
				{
					throw new SystemException("ERROR: getCTableWSQ : Invalid table defined : " + marker);
				}
			}
		}
		private void ProcessComment(IO.Reader br)
		{
			int size = br.ReadInt16() - 2;
			string commentValue = br.ReadFixedLengthString(size);
		}
		private void ProcessHuffmanTable(IO.Reader br)
		{
			// First time, read table length
			HuffmanTable firstHuffmanTable = getCHuffmanTable(br, Internal.Constants.MAX_HUFFCOUNTS_WSQ, 0, true);

			// Store table into global structure list
			int tableId = firstHuffmanTable.tableId;
			WSQData.tableDHT[tableId].huffbits = (int[])firstHuffmanTable.huffbits.Clone();
			WSQData.tableDHT[tableId].huffvalues = (int[])firstHuffmanTable.huffvalues.Clone();
			WSQData.tableDHT[tableId].tabdef = 1;

			int bytesLeft = firstHuffmanTable.bytesLeft;
			while (bytesLeft != 0)
			{
				// Read next table without reading table length
				HuffmanTable huffmantable = getCHuffmanTable(br, Internal.Constants.MAX_HUFFCOUNTS_WSQ, bytesLeft, false);

				// If table is already defined ...
				tableId = huffmantable.tableId;
				if (WSQData.tableDHT[tableId].tabdef != 0)
				{
					throw new SystemException("ERROR : ProcessHuffmanTable : huffman table already defined.");
				}

				/* Store table into global structure list. */
				WSQData.tableDHT[tableId].huffbits = (int[])huffmantable.huffbits.Clone();
				WSQData.tableDHT[tableId].huffvalues = (int[])huffmantable.huffvalues.Clone();
				WSQData.tableDHT[tableId].tabdef = 1;
				bytesLeft = huffmantable.bytesLeft;
			}
		}
		private void ProcessQuantizationTable(IO.Reader br)
		{
			br.ReadInt16(); /* header size */
			int scale = br.ReadByte(); /* scaling parameter */
			int shrtDat = br.ReadInt16(); /* counter and temp short buffer */

			WSQData.tableDQT.binCenter = (float)shrtDat;
			while (scale > 0)
			{
				WSQData.tableDQT.binCenter /= 10.0F;
				scale--;
			}

			for (int cnt = 0; cnt < Table_DQT.MAX_SUBBANDS; cnt++)
			{
				scale = br.ReadByte();
				shrtDat = br.ReadInt16();
				WSQData.tableDQT.qBin[cnt] = (float)shrtDat;
				while (scale > 0)
				{
					WSQData.tableDQT.qBin[cnt] /= 10.0F;
					scale--;
				}

				scale = br.ReadByte();
				shrtDat = br.ReadInt16();
				WSQData.tableDQT.zBin[cnt] = (float)shrtDat;
				while (scale > 0)
				{
					WSQData.tableDQT.zBin[cnt] /= 10.0F;
					scale--;
				}
			}

			WSQData.tableDQT.dqtDef = 1;
		}
		private void ProcessTransformTable(IO.Reader br)
		{
			short temp = br.ReadInt16();

			WSQData.tableDTT.hisz = br.ReadByte(); // has to be &'d with 0xFF?
			WSQData.tableDTT.losz = br.ReadByte();

			WSQData.tableDTT.hifilt = new float[WSQData.tableDTT.hisz];
			WSQData.tableDTT.lofilt = new float[WSQData.tableDTT.losz];

			int aSize;
			if (WSQData.tableDTT.hisz % 2 != 0)
			{
				aSize = (WSQData.tableDTT.hisz + 1) / 2;
			}
			else
			{
				aSize = WSQData.tableDTT.hisz / 2;
			}

			float[] aLofilt = new float[aSize];


			aSize--;
			for (int cnt = 0; cnt <= aSize; cnt++)
			{
				int sign = br.ReadByte();
				int scale = br.ReadByte();
				long shrtDat = br.ReadInt32();
				aLofilt[cnt] = (float)shrtDat;

				while (scale > 0)
				{
					aLofilt[cnt] /= 10.0F;
					scale--;
				}

				if (sign != 0)
				{
					aLofilt[cnt] *= -1.0F;
				}

				if (WSQData.tableDTT.hisz % 2 != 0)
				{
					WSQData.tableDTT.hifilt[cnt + aSize] = intSign(cnt) * aLofilt[cnt];
					if (cnt > 0)
					{
						WSQData.tableDTT.hifilt[aSize - cnt] = WSQData.tableDTT.hifilt[cnt + aSize];
					}
				}
				else
				{
					WSQData.tableDTT.hifilt[cnt + aSize + 1] = intSign(cnt) * aLofilt[cnt];
					WSQData.tableDTT.hifilt[aSize - cnt] = -1 * WSQData.tableDTT.hifilt[cnt + aSize + 1];
				}
			}

			if (WSQData.tableDTT.losz % 2 != 0)
			{
				aSize = (WSQData.tableDTT.losz + 1) / 2;
			}
			else
			{
				aSize = WSQData.tableDTT.losz / 2;
			}

			float[] aHifilt = new float[aSize];

			aSize--;
			for (int cnt = 0; cnt <= aSize; cnt++)
			{
				int sign = br.ReadByte();
				int scale = br.ReadByte();
				long shrtDat = br.ReadInt32();

				aHifilt[cnt] = (float)shrtDat;

				while (scale > 0)
				{
					aHifilt[cnt] /= 10.0F;
					scale--;
				}

				if (sign != 0)
				{
					aHifilt[cnt] *= -1.0F;
				}

				if (WSQData.tableDTT.losz % 2 != 0)
				{
					WSQData.tableDTT.lofilt[cnt + aSize] = intSign(cnt) * aHifilt[cnt];
					if (cnt > 0)
					{
						WSQData.tableDTT.lofilt[aSize - cnt] = WSQData.tableDTT.lofilt[cnt + aSize];
					}
				}
				else
				{
					WSQData.tableDTT.lofilt[cnt + aSize + 1] = intSign(cnt + 1) * aHifilt[cnt];
					WSQData.tableDTT.lofilt[aSize - cnt] = WSQData.tableDTT.lofilt[cnt + aSize + 1];
				}
			}

			WSQData.tableDTT.lodef = 1;
			WSQData.tableDTT.hidef = 1;
		}

		private void BuildWTree(short width, short height)
		{
			// starting lengths of sections of the image being split into subbands
			int lenx, lenx2, leny, leny2;
			WSQData.wtree = new WavletTree[Internal.Constants.W_TREELEN];
			for (int i = 0; i < Internal.Constants.W_TREELEN; i++)
			{
				WSQData.wtree[i] = new WavletTree();
				WSQData.wtree[i].invrw = 0;
				WSQData.wtree[i].invcl = 0;
			}

			WSQData.wtree[2].invrw = 1;
			WSQData.wtree[4].invrw = 1;
			WSQData.wtree[7].invrw = 1;
			WSQData.wtree[9].invrw = 1;
			WSQData.wtree[11].invrw = 1;
			WSQData.wtree[13].invrw = 1;
			WSQData.wtree[16].invrw = 1;
			WSQData.wtree[18].invrw = 1;
			WSQData.wtree[3].invcl = 1;
			WSQData.wtree[5].invcl = 1;
			WSQData.wtree[8].invcl = 1;
			WSQData.wtree[9].invcl = 1;
			WSQData.wtree[12].invcl = 1;
			WSQData.wtree[13].invcl = 1;
			WSQData.wtree[17].invcl = 1;
			WSQData.wtree[18].invcl = 1;

			wtree4(0, 1, width, height, 0, 0, 1);

			if ((WSQData.wtree[1].lenx % 2) == 0)
			{
				lenx = WSQData.wtree[1].lenx / 2;
				lenx2 = lenx;
			}
			else
			{
				lenx = (WSQData.wtree[1].lenx + 1) / 2;
				lenx2 = lenx - 1;
			}

			if ((WSQData.wtree[1].leny % 2) == 0)
			{
				leny = WSQData.wtree[1].leny / 2;
				leny2 = leny;
			}
			else
			{
				leny = (WSQData.wtree[1].leny + 1) / 2;
				leny2 = leny - 1;
			}

			wtree4(4, 6, lenx2, leny, lenx, 0, 0);
			wtree4(5, 10, lenx, leny2, 0, leny, 0);
			wtree4(14, 15, lenx, leny, 0, 0, 0);

			WSQData.wtree[19].x = 0;
			WSQData.wtree[19].y = 0;
			if ((WSQData.wtree[15].lenx % 2) == 0)
			{
				WSQData.wtree[19].lenx = WSQData.wtree[15].lenx / 2;
			}
			else
			{
				WSQData.wtree[19].lenx = (WSQData.wtree[15].lenx + 1) / 2;
			}
			if ((WSQData.wtree[15].leny % 2) == 0)
			{
				WSQData.wtree[19].leny = WSQData.wtree[15].leny / 2;
			}
			else
			{
				WSQData.wtree[19].leny = (WSQData.wtree[15].leny + 1) / 2;
			}
		}
		private void BuildQTree()
		{
			WSQData.qtree = new QuantTree[Internal.Constants.Q_TREELEN];
			for (int i = 0; i < WSQData.qtree.Length; i++)
			{
				WSQData.qtree[i] = new QuantTree();
			}

			qtree16(3, WSQData.wtree[14].lenx, WSQData.wtree[14].leny, WSQData.wtree[14].x, WSQData.wtree[14].y, 0, 0);
			qtree16(19, WSQData.wtree[4].lenx, WSQData.wtree[4].leny, WSQData.wtree[4].x, WSQData.wtree[4].y, 0, 1);
			qtree16(48, WSQData.wtree[0].lenx, WSQData.wtree[0].leny, WSQData.wtree[0].x, WSQData.wtree[0].y, 0, 0);
			qtree16(35, WSQData.wtree[5].lenx, WSQData.wtree[5].leny, WSQData.wtree[5].x, WSQData.wtree[5].y, 1, 0);
			qtree4(0, WSQData.wtree[19].lenx, WSQData.wtree[19].leny, WSQData.wtree[19].x, WSQData.wtree[19].y);
		}
		private int AssertNextMarker(IO.Reader br, ushort ExpectedMarker)
		{
			ushort nextMarker = br.ReadUInt16();
			switch (ExpectedMarker)
			{
				case Internal.Constants.SOI_WSQ:
				{
					if (nextMarker != Internal.Constants.SOI_WSQ)
					{
						throw new SystemException("ERROR : AssertNextMarker : No SOI marker : " + nextMarker);
					}
					return nextMarker;
				}
				case Internal.Constants.TBLS_N_SOF:
				{
					if (nextMarker != Internal.Constants.DTT_WSQ
						&& nextMarker != Internal.Constants.DQT_WSQ
						&& nextMarker != Internal.Constants.DHT_WSQ
						&& nextMarker != Internal.Constants.SOB_WSQ
						&& nextMarker != Internal.Constants.COM_WSQ)
					{
						throw new SystemException("ERROR : AssertNextMarker : No SOB, Table, or comment markers : " + nextMarker);
					}
					return nextMarker;
				}
				case Internal.Constants.TBLS_N_SOB:
				{
					if (nextMarker != Internal.Constants.DTT_WSQ
						&& nextMarker != Internal.Constants.DQT_WSQ
						&& nextMarker != Internal.Constants.DHT_WSQ
						&& nextMarker != Internal.Constants.SOB_WSQ
						&& nextMarker != Internal.Constants.COM_WSQ)
					{
						throw new SystemException("ERROR : AssertNextMarker : No SOB, Table, or comment markers : " +
								nextMarker);
					}
					return nextMarker;
				}
				case Internal.Constants.ANY_WSQ:
				{
					if ((nextMarker & 0xff00) != 0xff00)
					{
						throw new SystemException("ERROR : AssertNextMarker : no marker found : " + nextMarker);
					}
					if ((nextMarker < Internal.Constants.SOI_WSQ) || (nextMarker > Internal.Constants.COM_WSQ))
					{
						throw new SystemException("ERROR : AssertNextMarker : not a valid marker : " + nextMarker);
					}
					return nextMarker;
				}
			}
			throw new SystemException("ERROR : AssertNextMarker : Invalid marker : " + nextMarker);
		}

		private int getCNextbitsWSQ(IO.Reader br, ref int marker, ref int bitCount, int bitsReq, ref int nextByte)
		{
			if (bitCount == 0)
			{
				nextByte = br.ReadByte();

				bitCount = 8;
				if (nextByte == 0xFF)
				{
					int code2 = br.ReadByte();  // stuffed byte of buffer

					if (code2 != 0x00 && bitsReq == 1)
					{
						marker = (nextByte << 8) | code2;
						return 1;
					}
					if (code2 != 0x00)
					{
						throw new SystemException("ERROR: getCNextbitsWSQ : No stuffed zeros.");
					}
				}
			}

			int bits, tbits;  // bits of current buffer byte requested
			int bitsNeeded; // additional bits required to finish request

			if (bitsReq <= bitCount)
			{
				bits = (nextByte >> (bitCount - bitsReq)) & (Internal.Constants.BITMASK[bitsReq]);
				bitCount -= bitsReq;
				nextByte &= Internal.Constants.BITMASK[bitCount];
			}
			else
			{
				bitsNeeded = bitsReq - bitCount; // additional bits required to finish request
				bits = nextByte << bitsNeeded;
				bitCount = 0;
				tbits = getCNextbitsWSQ(br, ref marker, ref bitCount, bitsNeeded, ref nextByte);
				bits |= tbits;
			}

			return bits;
		}

		private float[] Unquantize(int[] sip, int width, int height)
		{
			float[] fip = new float[width * height];  // floating point image

			if (WSQData.tableDQT.dqtDef != 1)
			{
				throw new SystemException("ERROR: unquantize : quantization table parameters not defined!");
			}

			float binCenter = WSQData.tableDQT.binCenter; // quantizer bin center

			int sptr = 0;
			for (int cnt = 0; cnt < Internal.Constants.NUM_SUBBANDS; cnt++)
			{
				if (WSQData.tableDQT.qBin[cnt] == 0.0)
				{
					continue;
				}

				int fptr = (WSQData.qtree[cnt].y * width) + WSQData.qtree[cnt].x;

				for (int row = 0; row < WSQData.qtree[cnt].leny; row++, fptr += width - WSQData.qtree[cnt].lenx)
				{
					for (int col = 0; col < WSQData.qtree[cnt].lenx; col++)
					{
						if (sip[sptr] == 0)
						{
							fip[fptr] = 0.0f;
						}
						else if (sip[sptr] > 0)
						{
							fip[fptr] = (WSQData.tableDQT.qBin[cnt] * (sip[sptr] - binCenter)) + (WSQData.tableDQT.zBin[cnt] / 2.0f);
						}
						else if (sip[sptr] < 0)
						{
							fip[fptr] = (WSQData.tableDQT.qBin[cnt] * (sip[sptr] + binCenter)) - (WSQData.tableDQT.zBin[cnt] / 2.0f);
						}
						else
						{
							throw new SystemException("ERROR : unquantize : invalid quantization pixel value");
						}
						fptr++;
						sptr++;
					}
				}
			}

			return fip;
		}

		#region Huffman Table
		private void BuildHuffmanCodes(HuffCode[] huffcodeTable)
		{
			short tempCode = 0;  /*used to construct code word*/
			int pointer = 0;     /*pointer to code word information*/

			int tempSize = huffcodeTable[0].size;
			if (huffcodeTable[pointer].size == 0)
			{
				return;
			}

			do
			{
				do
				{
					huffcodeTable[pointer].code = tempCode;
					tempCode++;
					pointer++;
				}
				while (huffcodeTable[pointer].size == tempSize);

				if (huffcodeTable[pointer].size == 0) return;

				do
				{
					tempCode <<= 1;
					tempSize++;
				}
				while (huffcodeTable[pointer].size != tempSize);
			}
			while (huffcodeTable[pointer].size == tempSize);
		}
		private void GenerateHuffmanDecodeTable(HuffCode[] huffcodeTable, int[] maxcode, int[] mincode, int[] valptr, int[] huffbits)
		{
			for (int i = 0; i <= Internal.Constants.MAX_HUFFBITS; i++)
			{
				maxcode[i] = 0;
				mincode[i] = 0;
				valptr[i] = 0;
			}

			int i2 = 0;
			for (int i = 1; i <= Internal.Constants.MAX_HUFFBITS; i++)
			{
				if (huffbits[i - 1] == 0)
				{
					maxcode[i] = -1;
					continue;
				}
				valptr[i] = i2;
				mincode[i] = huffcodeTable[i2].code;
				i2 = i2 + huffbits[i - 1] - 1;
				maxcode[i] = huffcodeTable[i2].code;
				i2++;
			}
		}

		private int DecodeHuffmanDataMemory(IO.Reader br, int[] mincode, int[] maxcode, int[] valptr, int[] huffvalues, ref int bitCount, ref int marker, ref int nextByte)
		{
			short code = (short)getCNextbitsWSQ(br, ref marker, ref bitCount, 1, ref nextByte); // becomes a huffman code word  (one bit at a time)
			if (marker != 0) return -1;

			int inx;

			for (inx = 1; code > maxcode[inx]; inx++)
			{
				int tbits = getCNextbitsWSQ(br, ref marker, ref bitCount, 1, ref nextByte);  // becomes a huffman code word  (one bit at a time)
				code = (short)((code << 1) + tbits);

				if (marker != 0) return -1;
			}

			int inx2 = valptr[inx] + code - mincode[inx];  /*increment variables*/
			return huffvalues[inx2];
		}
		private HuffCode[] BuildHuffmanSizes(int[] huffbits, int maxHuffcounts)
		{
			HuffCode[] huffcodeTable;	// table of huffman codes and sizes
			int numberOfCodes = 1;		// the number codes for a given code size

			huffcodeTable = new HuffCode[maxHuffcounts + 1];

			int tempSize = 0;
			for (int codeSize = 1; codeSize <= Internal.Constants.MAX_HUFFBITS; codeSize++)
			{
				while (numberOfCodes <= huffbits[codeSize - 1])
				{
					huffcodeTable[tempSize] = new HuffCode();
					huffcodeTable[tempSize].size = codeSize;
					tempSize++;
					numberOfCodes++;
				}
				numberOfCodes = 1;
			}

			huffcodeTable[tempSize] = new HuffCode();
			huffcodeTable[tempSize].size = 0;

			return huffcodeTable;
		}
		private int[] DecodeHuffmanTable(IO.Reader br, int size)
		{
			int[] qdata = new int[size];

			int[] maxcode = new int[Internal.Constants.MAX_HUFFBITS + 1];
			int[] mincode = new int[Internal.Constants.MAX_HUFFBITS + 1];
			int[] valptr = new int[Internal.Constants.MAX_HUFFBITS + 1];

			int marker = AssertNextMarker(br, Internal.Constants.TBLS_N_SOB);

			int bitCount = 0; // bit count for getc_nextbits_wsq routine
			int nextByte = 0; // next byte of buffer
			int hufftableId = 0; // huffman table number
			int ip = 0;

			while (marker != Internal.Constants.EOI_WSQ)
			{
				if (marker != 0)
				{
					while (marker != Internal.Constants.SOB_WSQ)
					{
						ProcessTable(br, marker);
						marker = AssertNextMarker(br, Internal.Constants.TBLS_N_SOB);
					}

					short u0 = br.ReadInt16(); // block header size
					hufftableId = br.ReadByte(); // huffman table number

					if (WSQData.tableDHT[hufftableId].tabdef != 1)
					{
						throw new SystemException("ERROR : huffmanDecodeDataMem : huffman table undefined.");
					}

					// the next two routines reconstruct the huffman tables
					HuffCode[] hufftable = BuildHuffmanSizes(WSQData.tableDHT[hufftableId].huffbits, Internal.Constants.MAX_HUFFCOUNTS_WSQ);
					BuildHuffmanCodes(hufftable);

					// build a set of three tables used in decoding the compressed buffer
					GenerateHuffmanDecodeTable(hufftable, maxcode, mincode, valptr, WSQData.tableDHT[hufftableId].huffbits);

					bitCount = 0;
					marker = 0;
				}

				// get next huffman category code from compressed input buffer stream
				int nodeptr = DecodeHuffmanDataMemory(br, mincode, maxcode, valptr, WSQData.tableDHT[hufftableId].huffvalues, ref bitCount, ref marker, ref nextByte);
				// nodeptr  pointers for decoding

				if (nodeptr == -1)
				{
					continue;
				}

				if (nodeptr > 0 && nodeptr <= 100)
				{
					for (int n = 0; n < nodeptr; n++)
					{
						qdata[ip++] = 0; /* z run */
					}
				}
				else if (nodeptr > 106 && nodeptr < 0xff)
				{
					qdata[ip++] = nodeptr - 180;
				}
				else if (nodeptr == 101)
				{
					qdata[ip++] = getCNextbitsWSQ(br, ref marker, ref bitCount, 8, ref nextByte);

				}
				else if (nodeptr == 102)
				{
					qdata[ip++] = -getCNextbitsWSQ(br, ref marker, ref bitCount, 8, ref nextByte);
				}
				else if (nodeptr == 103)
				{
					qdata[ip++] = getCNextbitsWSQ(br, ref marker, ref bitCount, 16, ref nextByte);
				}
				else if (nodeptr == 104)
				{
					qdata[ip++] = -getCNextbitsWSQ(br, ref marker, ref bitCount, 16, ref nextByte);
				}
				else if (nodeptr == 105)
				{
					int n = getCNextbitsWSQ(br, ref marker, ref bitCount, 8, ref nextByte);
					while (n-- > 0)
					{
						qdata[ip++] = 0;
					}
				}
				else if (nodeptr == 106)
				{
					int n = getCNextbitsWSQ(br, ref marker, ref bitCount, 16, ref nextByte);
					while (n-- > 0)
					{
						qdata[ip++] = 0;
					}
				}
				else
				{
					throw new SystemException("ERROR: huffman_decode_data_mem : Invalid code (" + nodeptr + ")");
				}
			}

			return qdata;
		}
		#endregion
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
