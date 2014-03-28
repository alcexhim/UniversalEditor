using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
    public class PMAXPatchObjectModel : ObjectModel
    {
        public override void Clear()
        {
            mvarPatches.Clear();
        }
        public override void CopyTo(ObjectModel where)
        {
            PMAXPatchObjectModel clone = (where as PMAXPatchObjectModel);
            if (clone == null) return;

            foreach (PMAXPatch patch in mvarPatches)
            {
                clone.Patches.Add(patch.Clone() as PMAXPatch);
            }
        }

        public override ObjectModelReference MakeReference()
        {
            ObjectModelReference omr = base.MakeReference();
            omr.Title = "PMAX patch";
            omr.Path = new string[] { "Multimedia", "3D Multimedia", "Concertroid Extensions" };
            omr.Description = "Extensions that enhance the 3D model rendered with Concertroid";
            return omr;
        }

        private PMAXPatch.PMAXPatchCollection mvarPatches = new PMAXPatch.PMAXPatchCollection();
        public PMAXPatch.PMAXPatchCollection Patches { get { return mvarPatches; } }

    }
}
