using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.BGM
{
	public class OSLCompressionModule : CompressionModule
	{
		public override string Name
		{
			get { return "oslbgm"; }
		}

		private readonly sbyte[] ima_index_table /*[16]*/ =
		{
		  -1, -1, -1, -1, 2, 4, 7, 12,
		  -1, -1, -1, -1, 2, 4, 7, 12
		};
		private readonly short[] ima_step_table /*[89]*/ =
		{
			  7,    8,    9,   10,   11,   12,   13,   14,   16,   17,
			 19,   21,   23,   25,   28,   31,   34,   37,   41,   45,
			 50,   55,   60,   66,   73,   80,   88,   97,  107,  118,
			130,  143,  157,  173,  190,  209,  230,  253,  279,  307,
			337,  371,  408,  449,  494,  544,  598,  658,  724,  796,
			876,  963, 1060, 1166, 1282, 1411, 1552, 1707, 1878, 2066,
		   2272, 2499, 2749, 3024, 3327, 3660, 4026, 4428, 4871, 5358,
		   5894, 6484, 7132, 7845, 8630, 9493,10442,11487,12635,13899,
		  15289,16818,18500,20350,22385,24623,27086,29794,32767
		};

		private static int ima_get_diff(int step, byte nibble, ref int predictor)
		{
			int diff = step >> 3;
			if ((nibble & 1) != 0) diff += step >> 2;
			if ((nibble & 2) != 0) diff += step >> 1;
			if ((nibble & 4) != 0) diff += step;
			if ((nibble & 7) == 7) diff += step >> 1;
			if ((nibble & 8) != 0) diff = -diff;
			return diff;
		}

		private void ima_process_nibble(byte nibble, ref int step_index, ref int predictor, ref int step)
		{
			if (step_index < 0) step_index = 0;
			if (step_index > ima_step_table.Length - 1) step_index = ima_step_table.Length - 1;

			step = ima_step_table[step_index];
			int diff = ima_get_diff(step, nibble, ref predictor);

			step_index += ima_index_table[nibble & 0x07];

			predictor = predictor + diff;
			if (predictor < Int16.MinValue) predictor = Int16.MinValue;
			if (predictor > Int16.MaxValue) predictor = Int16.MaxValue;
		}

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
		}

		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			StreamAccessor saInput = new StreamAccessor(inputStream);
			StreamAccessor saOutput = new StreamAccessor(outputStream);

			Reader br = new Reader(saInput);
			Writer bw = new Writer(saOutput);

			// each byte represents two IMA nibbles, which each represent a signed 16-bit PCM sample
			// so therefore each byte represents 2 Int16 samples
			int predictor = 0;
			int step_index = 0;
			byte nibble = 0;

			while (!br.EndOfStream)
			{
				byte next = br.ReadByte();
				int step = 0;

				nibble = (byte)(next & 0x0F);
				ima_process_nibble(nibble, ref step_index, ref predictor, ref step);
				bw.WriteInt16((short)predictor);

				
				nibble = (byte)(next >> 4);
				ima_process_nibble(nibble, ref step_index, ref predictor, ref step);
				bw.WriteInt16((short)predictor);
			}
		}
	}
}
