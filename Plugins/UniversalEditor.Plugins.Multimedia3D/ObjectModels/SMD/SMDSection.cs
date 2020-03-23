using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SMD
{
    public class SMDSection : ICloneable
    {
        public class SMDSectionCollection
            : System.Collections.ObjectModel.Collection<SMDSection>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private System.Collections.Specialized.StringCollection mvarLines = new System.Collections.Specialized.StringCollection();
        public System.Collections.Specialized.StringCollection Lines { get { return mvarLines; } }

        public override string ToString()
        {
            return mvarName + " [" + mvarLines.Count.ToString() + " lines]";
        }
        public object Clone()
        {
            SMDSection clone = new SMDSection();
            clone.Name = (mvarName.Clone() as string);
            foreach (string line in mvarLines)
            {
                clone.Lines.Add(line.Clone() as string);
            }
            return clone;
        }
    }
}
