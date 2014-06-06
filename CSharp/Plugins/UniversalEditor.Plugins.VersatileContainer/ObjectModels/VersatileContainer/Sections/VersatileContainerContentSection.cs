using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.VersatileContainer.Sections
{
	public class VersatileContainerContentSection : VersatileContainerSection
    {
        private string mvarClassName = String.Empty;
        public string ClassName { get { return mvarClassName; } set { mvarClassName = value; } }

        private byte[] mvarData = new byte[0];
        public byte[] Data { get { return mvarData; } set { mvarData = value; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"" + this.Name + "\"");
            if (!String.IsNullOrEmpty(mvarClassName))
            {
                sb.Append(" : ");
                sb.Append(mvarClassName);
            }
            return sb.ToString();
        }
        public override object Clone()
        {
            VersatileContainerContentSection clone = new VersatileContainerContentSection();
            clone.Name = (this.Name.Clone() as string);
            clone.ClassName = (mvarClassName.Clone() as string);
            clone.Data = (mvarData.Clone() as byte[]);
            return clone;
        }

        private Compression.CompressionMethod mvarCompressionMethod = Compression.CompressionMethod.None;
        public Compression.CompressionMethod CompressionMethod { get { return mvarCompressionMethod; } set { mvarCompressionMethod = value; } }
    }
}
