using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors;

namespace UniversalEditor.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            FileAccessor fa = new FileAccessor(@"C:\Temp\TEST.DAT", true, false);

            TestObjectModel om = new TestObjectModel();
            // om.Count = 5734213958;

            TestDataFormat df = new TestDataFormat();

            Document doc = new Document(om, df, fa);
            doc.Load();
        }
    }
}
