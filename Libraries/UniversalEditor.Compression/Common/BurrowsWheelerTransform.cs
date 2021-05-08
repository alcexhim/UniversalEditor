using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Common
{
	public static class BurrowsWheelerTransform
	{
		private static char[][] getRotations(char[] array)
		{
			char[][] cz = new char[array.Length][];
			for (int i = 0; i < cz.Length; i++)
			{
				cz[i] = BurrowsWheelerTransform.getRotation(array, i);
			}
			return cz;
		}
		private static char[] getRotation(char[] array, int rotation)
		{
			char[] oldArray = new char[array.Length];
			char[] newArray = new char[array.Length];
			array.CopyTo(oldArray, 0);
			array.CopyTo(newArray, 0);
			for (int i = 0; i < newArray.Length; i++)
			{
				int rot = 0;
				if (i + rotation < newArray.Length)
				{
					rot = i + rotation;
				}
				else
				{
					rot = i + rotation - newArray.Length;
				}
				newArray[i] = oldArray[rot];
			}
			return newArray;
		}
		public static void Test()
		{
			char[] sorted = Sorting.AlphabeticSort("FAAOD".ToCharArray());
		}
	}
}
