//
//  ArrayExtensions.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using System;
namespace MBS.Framework
{
	public static class ArrayExtensions
	{
		public static void Bisect<T>(this T[] array, int index, out T[] left, out T[] right)
		{
			left = new T[index];
			right = new T[array.Length - index];

			Array.Copy(array, 0, left, 0, left.Length);
			Array.Copy(array, index, right, 0, right.Length);
		}
		public static void Array_RemoveAt<T>(ref T[] array, int index, int count = 1)
		{
			T[] old = (T[])array.Clone();

			int start = index;
			int length = count;
			if (count < 0)
			{
				start = index + count;
				length = Math.Abs(count);
			}
			Array.Resize<T>(ref array, old.Length - length);

			Array.Copy(old, 0, array, 0, start);
			if (array.Length - (start + length) > -1)
				Array.Copy(old, start + length, array, start, array.Length - (start + length));
		}
	}
}
