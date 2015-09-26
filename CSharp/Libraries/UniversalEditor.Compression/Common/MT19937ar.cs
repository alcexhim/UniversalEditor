using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Common
{
	public class MT19937ar
	{
		private static short N = 624;
		private static short M = 397;
		private static uint MATRIX_A = 2567483615u;
		private static uint UPPER_MASK = 2147483648u;
		private static uint LOWER_MASK = 2147483647u;
		private static uint[] mag01 = new uint[]
		{
			0u, 
			MT19937ar.MATRIX_A
		};
		private static uint[] mt = new uint[(int)MT19937ar.N];
		private static short mti = (short)(MT19937ar.N + 1);
		public void init_genrand(uint s)
		{
			MT19937ar.mt[0] = (s & 4294967295u);
			MT19937ar.mti = 1;
			while (MT19937ar.mti < MT19937ar.N)
			{
				MT19937ar.mt[(int)MT19937ar.mti] = 1812433253u * (MT19937ar.mt[(int)(MT19937ar.mti - 1)] ^ MT19937ar.mt[(int)(MT19937ar.mti - 1)] >> 30) + (uint)MT19937ar.mti;
				MT19937ar.mt[(int)MT19937ar.mti] &= 4294967295u;
				MT19937ar.mti += 1;
			}
		}
		public void init_by_array(uint[] init_key, short key_length)
		{
			this.init_genrand(19650218u);
			uint i = 1u;
			uint j = 0u;
			for (uint k = (uint)((MT19937ar.N > key_length) ? ((uint)MT19937ar.N) : ((uint)key_length)); k > 0u; k -= 1u)
			{
				MT19937ar.mt[(int)((UIntPtr)i)] = (MT19937ar.mt[(int)((UIntPtr)i)] ^ (MT19937ar.mt[(int)((UIntPtr)(i - 1u))] ^ MT19937ar.mt[(int)((UIntPtr)(i - 1u))] >> 30) * 1664525u) + init_key[(int)((UIntPtr)j)] + j;
				MT19937ar.mt[(int)((UIntPtr)i)] &= 4294967295u;
				i += 1u;
				j += 1u;
				if ((ulong)i >= (ulong)((long)MT19937ar.N))
				{
					MT19937ar.mt[0] = MT19937ar.mt[(int)(MT19937ar.N - 1)];
					i = 1u;
				}
				if ((ulong)j >= (ulong)((long)key_length))
				{
					j = 0u;
				}
			}
			for (uint k = (uint)(MT19937ar.N - 1); k > 0u; k -= 1u)
			{
				MT19937ar.mt[(int)((UIntPtr)i)] = (MT19937ar.mt[(int)((UIntPtr)i)] ^ (MT19937ar.mt[(int)((UIntPtr)(i - 1u))] ^ MT19937ar.mt[(int)((UIntPtr)(i - 1u))] >> 30) * 1566083941u) - i;
				MT19937ar.mt[(int)((UIntPtr)i)] &= 4294967295u;
				i += 1u;
				if ((ulong)i >= (ulong)((long)MT19937ar.N))
				{
					MT19937ar.mt[0] = MT19937ar.mt[(int)(MT19937ar.N - 1)];
					i = 1u;
				}
			}
			MT19937ar.mt[0] = 2147483648u;
		}
		public uint genrand_int32()
		{
			uint y;
			if (MT19937ar.mti >= MT19937ar.N)
			{
				if (MT19937ar.mti == MT19937ar.N + 1)
				{
					this.init_genrand(5489u);
				}
				short kk;
				for (kk = 0; kk < MT19937ar.N - MT19937ar.M; kk += 1)
				{
					y = ((MT19937ar.mt[(int)kk] & MT19937ar.UPPER_MASK) | (MT19937ar.mt[(int)(kk + 1)] & MT19937ar.LOWER_MASK));
					MT19937ar.mt[(int)kk] = (MT19937ar.mt[(int)(kk + MT19937ar.M)] ^ y >> 1 ^ MT19937ar.mag01[(int)((UIntPtr)(y & 1u))]);
				}
				while (kk < MT19937ar.N - 1)
				{
					y = ((MT19937ar.mt[(int)kk] & MT19937ar.UPPER_MASK) | (MT19937ar.mt[(int)(kk + 1)] & MT19937ar.LOWER_MASK));
					MT19937ar.mt[(int)kk] = (MT19937ar.mt[(int)(kk + (MT19937ar.M - MT19937ar.N))] ^ y >> 1 ^ MT19937ar.mag01[(int)((UIntPtr)(y & 1u))]);
					kk += 1;
				}
				y = ((MT19937ar.mt[(int)(MT19937ar.N - 1)] & MT19937ar.UPPER_MASK) | (MT19937ar.mt[0] & MT19937ar.LOWER_MASK));
				MT19937ar.mt[(int)(MT19937ar.N - 1)] = (MT19937ar.mt[(int)(MT19937ar.M - 1)] ^ y >> 1 ^ MT19937ar.mag01[(int)((UIntPtr)(y & 1u))]);
				MT19937ar.mti = 0;
			}
			uint[] arg_15A_0 = MT19937ar.mt;
			short expr_151 = MT19937ar.mti;
			MT19937ar.mti = (short)(expr_151 + 1);
			y = arg_15A_0[(int)expr_151];
			y ^= y >> 11;
			y ^= (y << 7 & 2636928640u);
			y ^= (y << 15 & 4022730752u);
			return y ^ y >> 18;
		}
	}
}
