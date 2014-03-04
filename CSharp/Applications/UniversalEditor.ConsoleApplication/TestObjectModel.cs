using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ConsoleApplication
{
    public class TestObjectModel : ObjectModel
    {
        private long mvarCount = 0;
        public long Count { get { return mvarCount; } set { mvarCount = value; } }

        public override void Clear()
        {
            mvarCount = 0;
        }

        public override void CopyTo(ObjectModel where)
        {
            TestObjectModel clone = (where as TestObjectModel);
            clone.Count = mvarCount;
        }
    }
}
