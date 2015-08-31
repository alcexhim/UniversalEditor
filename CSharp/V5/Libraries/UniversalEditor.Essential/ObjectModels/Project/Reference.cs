using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Project
{
    public class Reference
    {
        public class ReferenceCollection
            : System.Collections.ObjectModel.Collection<Reference>
        {
            public Reference Add(string title, string fileName, Version version, Guid id)
            {
                Reference refer = new Reference();
                refer.Title = title;
                refer.FileName = fileName;
                refer.Version = version;
                refer.ID = id;
                base.Add(refer);
                return refer;
            }
        }

        private Guid mvarID = Guid.Empty;
        public Guid ID { get { return mvarID; } set { mvarID = value; } }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private string mvarFileName = String.Empty;
        public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

        private Version mvarVersion = new Version();
        public Version Version { get { return mvarVersion; } set { mvarVersion = value; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(mvarTitle);
            sb.Append(", ");
            sb.Append(mvarVersion.ToString());
            sb.Append(", ");
            sb.Append(mvarID.ToString("B"));
            sb.Append(", ");
            sb.Append(mvarFileName);
            return sb.ToString();
        }
    }
}
