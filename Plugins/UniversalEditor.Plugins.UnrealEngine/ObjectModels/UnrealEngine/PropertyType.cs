using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    public enum PropertyType
    {
        Byte = 1,
        Integer = 2,
        Boolean = 3,
        Float = 4,
        Object = 5,
        Name = 6,
        String = 7,
        Class = 8,
        Array = 9,
        Struct = 10,
        Vector = 11,
        Rotator = 12,
        Str = 13, 
        Map = 14,
        FixedArray = 15
    }

    /*
     * Property size values:
0 = 1 byte
1 = 2 bytes
2 = 4 bytes
3 = 12 bytes
4 = 16 bytes
5 = a byte follows with real size
6 = a word follows with real size
7 = an integer follows with real size
     * 
     * 
     * Property value layout:
     * 0x01 (ByteProperty) Value Format
0x02 (IntegerProperty) BYTE
0x03 (BooleanProperty) DWORD
0x04 (FloatProperty) DWORD
0x05 (ObjectProperty) INDEX
0x06 (NameProperty) INDEX
0x07 (StringProperty) Unknown
0x08 (ClassProperty) 
0x09 (ArrayProperty) Unknown
0x0A (StructProperty) 
0x0B (VectorProperty) Unknown
0x0C (RotatorProperty) Unknown
0x0D (StrProperty) INDEX length
                  ASCIIZ text
                 Unknown
                Unknown
0x0E (MapProperty)
0x0F (FixedArrayProperty)
Comments
The real value is in bit 7 of
the info byte.
A 4-byte float.
Object Reference value.
See “Object References”.
Name Reference value.
Index in to the Name
Table.
See below for some known
classes.
See below for some known
structs.
Length field includes null
terminator.

    */
}
