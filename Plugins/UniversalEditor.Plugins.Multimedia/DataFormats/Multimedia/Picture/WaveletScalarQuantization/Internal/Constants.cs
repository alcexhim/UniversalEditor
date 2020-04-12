//
//  Constants.cs - internal constants for Wavelet Scalar Quantization (WSQ)
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.WaveletScalarQuantization.Internal
{
	/// <summary>
	/// Internal constants for Wavelet Scalar Quantization (WSQ).
	/// </summary>
	public static class Constants
	{
		/*used to "mask out" n number of bits from data stream*/
		public static int[] BITMASK = { 0x00, 0x01, 0x03, 0x07, 0x0f, 0x1f, 0x3f, 0x7f, 0xff };

		public const int MAX_DHT_TABLES = 8;
		public const int MAX_HUFFBITS = 16;
		public const int MAX_HUFFCOUNTS_WSQ = 256;

		public const int W_TREELEN = 20;
		public const int Q_TREELEN = 64;

		/* WSQ Marker Definitions */
		public const int SOI_WSQ = 0xffa0;
		public const int EOI_WSQ = 0xffa1;
		public const int SOF_WSQ = 0xffa2;
		public const int SOB_WSQ = 0xffa3;
		public const int DTT_WSQ = 0xffa4;
		public const int DQT_WSQ = 0xffa5;
		public const int DHT_WSQ = 0xffa6;
		public const int DRT_WSQ = 0xffa7;
		public const int COM_WSQ = 0xffa8;

		public const int STRT_SUBBAND_2 = 19;
		public const int STRT_SUBBAND_3 = 52;
		public const int MAX_SUBBANDS = 64;
		public const int NUM_SUBBANDS = 60;
		public const int STRT_SUBBAND_DEL = NUM_SUBBANDS;
		public const int STRT_SIZE_REGION_2 = 4;
		public const int STRT_SIZE_REGION_3 = 51;

		/* Case for getting ANY marker. */
		public const int ANY_WSQ = 0xffff;
		public const int TBLS_N_SOF = 2;
		public const int TBLS_N_SOB = TBLS_N_SOF + 2;
	}
}
