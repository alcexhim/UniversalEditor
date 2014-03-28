using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
    public class PMAXPatch : ICloneable
    {
        public class PMAXPatchCollection
            : System.Collections.ObjectModel.Collection<PMAXPatch>
        {
        }

        private string mvarInputFileName = String.Empty;
        public string InputFileName { get { return mvarInputFileName; } set { mvarInputFileName = value; } }

        private string mvarOutputFileName = String.Empty;
        public string OutputFileName { get { return mvarOutputFileName; } set { mvarOutputFileName = value; } }

        private PMAXPatchChunk.PMAXPatchChunkCollection mvarChunks = new PMAXPatchChunk.PMAXPatchChunkCollection();
        public PMAXPatchChunk.PMAXPatchChunkCollection Chunks { get { return mvarChunks; } }

        public object Clone()
        {
            PMAXPatch clone = new PMAXPatch();
            foreach (PMAXPatchChunk chunk in mvarChunks)
            {
                clone.Chunks.Add(chunk.Clone() as PMAXPatchChunk);
            }
            clone.InputFileName = mvarInputFileName;
            clone.OutputFileName = mvarOutputFileName;
            return clone;
        }
    }
}
