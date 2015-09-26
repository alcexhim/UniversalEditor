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
