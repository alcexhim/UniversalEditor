using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.ObjectModels.AniMiku.PMDExtension
{
    public class PMDExtensionTextureGroup : ICloneable
    {
        public class PMDExtensionArchiveFileCollection
            : System.Collections.ObjectModel.Collection<PMDExtensionTextureGroup>
        {
        }

        private string mvarName = String.Empty;
        public string ArchiveFileName { get { return mvarName; } set { mvarName = value; } }

        private System.Collections.Specialized.StringCollection mvarTextureImageFileNames = new System.Collections.Specialized.StringCollection();
        public System.Collections.Specialized.StringCollection TextureImageFileNames { get { return mvarTextureImageFileNames; } }

        public object Clone()
        {
            PMDExtensionTextureGroup clone = new PMDExtensionTextureGroup();
            clone.ArchiveFileName = (mvarName.Clone() as string);
            foreach (string s in mvarTextureImageFileNames)
            {
                clone.TextureImageFileNames.Add(s.Clone() as string);
            }
            return clone;
        }

        private ModelMaterial mvarMaterial = null;
        public ModelMaterial Material { get { return mvarMaterial; } set { mvarMaterial = value; } }
    }
}
