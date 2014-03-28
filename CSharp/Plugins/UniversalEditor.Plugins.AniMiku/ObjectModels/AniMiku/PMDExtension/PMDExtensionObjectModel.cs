using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.AniMiku.PMDExtension
{
    internal class PMDExtensionObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        public override ObjectModelReference MakeReference()
        {
            if (_omr == null)
            {
                _omr = base.MakeReference();
                _omr.Title = "AniMiku PMD model extension";
            }
            return _omr;
        }
        public override void Clear()
        {
            mvarArchiveFiles.Clear();
        }
        public override void CopyTo(ObjectModel where)
        {
            PMDExtensionObjectModel clone = (where as PMDExtensionObjectModel);
            if (clone == null) return;

            foreach (PMDExtensionTextureGroup file in mvarArchiveFiles)
            {
                clone.ArchiveFiles.Add(file.Clone() as PMDExtensionTextureGroup);
            }
        }

        private PMDExtensionTextureGroup.PMDExtensionArchiveFileCollection mvarArchiveFiles = new PMDExtensionTextureGroup.PMDExtensionArchiveFileCollection();
        public PMDExtensionTextureGroup.PMDExtensionArchiveFileCollection ArchiveFiles
        {
            get { return mvarArchiveFiles; }
        }
    }
}
