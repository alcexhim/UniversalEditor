//
//  Array.cs - methods to resize / copy arrays
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.IO
{
    public class Array
    {
        public static void Resize(ref byte[] array, int newSize)
        {
            byte[] newArray = new byte[newSize];
            Array.Copy(array, 0, newArray, 0, Math.Min(array.Length, newArray.Length));
            array = newArray;
        }
        public static void Copy(System.Array sourceArray, long sourceIndex, System.Array destinationArray, long destinationIndex, long length)
        {
            System.Array.Copy(sourceArray, 0, destinationArray, 0, length);
        }
    }
}
