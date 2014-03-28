using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.AniMiku.PMDExtension
{
    public class PMDExtensionArchiveFile : ICloneable
    {
        public class PMDExtensionArchiveFileCollection
            : System.Collections.ObjectModel.Collection<PMDExtensionArchiveFile>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private System.Collections.Specialized.StringCollection mvarTextureImageFileNames = new System.Collections.Specialized.StringCollection();
        public System.Collections.Specialized.StringCollection TextureImageFileNames { get { return mvarTextureImageFileNames; } }

        public object Clone()
        {
            PMDExtensionArchiveFile clone = new PMDExtensionArchiveFile();
            clone.Name = (mvarName.Clone() as string);
            foreach (string s in mvarTextureImageFileNames)
            {
                clone.TextureImageFileNames.Add(s.Clone() as string);
            }
            return clone;
        }
    }
}
