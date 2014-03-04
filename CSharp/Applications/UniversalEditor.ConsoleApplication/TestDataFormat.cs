using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ConsoleApplication
{
    public class TestDataFormat : DataFormat
    {
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            TestObjectModel test = (objectModel as TestObjectModel);
            string DISTAL00 = Accessor.Reader.ReadFixedLengthString(8);
            if (DISTAL00 != "DISTAL00") throw new InvalidDataFormatException();

            test.Count = Accessor.Reader.ReadInt64();
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            TestObjectModel test = (objectModel as TestObjectModel);
            Accessor.Writer.WriteFixedLengthString("DISTAL00");
            Accessor.Writer.WriteInt64(test.Count);
        }
    }
}
