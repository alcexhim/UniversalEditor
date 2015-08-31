using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Common
{
	public static class Sorting
	{
		public static char[] AlphabeticSort(char[] input)
		{
			char[] output = new char[input.Length];
			input.CopyTo(output, 0);
			int previ = 0;
			int swapi = 0;
			for (int i = 0; i < output.Length; i++)
			{
				if (output[i] < output[previ])
				{
					char swap = output[i];
					output[i] = output[swapi];
					output[swapi] = swap;
					swapi = i;
					previ = i;
				}
				else
				{
					if (output[i] > output[previ])
					{
						swapi = i - 1;
					}
				}
			}
			return output;
		}
	}
}
