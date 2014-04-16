using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.PropertyList.UniversalPropertyList
{
    public enum VariantType : byte
    {
        Null            = 0,
        Array           = 1,
        Boolean         = 2,
        Byte            = 3,
        Char            = 4,
        DateTime        = 5,
        Decimal         = 6,
        Double          = 7,
        Guid            = 8,
        Int16           = 9,
        Int32           = 10,
        Int64           = 11,
        Object          = 12,
        SByte           = 13,
        Single          = 14,
        String          = 15,
        UInt16          = 16,
        UInt32          = 17,
        UInt64          = 18
    }
}
