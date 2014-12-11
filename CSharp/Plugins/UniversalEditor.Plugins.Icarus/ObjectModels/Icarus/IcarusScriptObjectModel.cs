using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public class IcarusScriptObjectModel : ObjectModel
    {
        private IcarusCommand.IcarusCommandCollection mvarCommands = new IcarusCommand.IcarusCommandCollection();
        public IcarusCommand.IcarusCommandCollection Commands { get { return mvarCommands; } }

        public override void Clear()
        {
            mvarCommands.Clear();
        }
        public override void CopyTo(ObjectModel where)
        {
            IcarusScriptObjectModel clone = (where as IcarusScriptObjectModel);
            if (clone == null) return;

            foreach (IcarusCommand cmd in mvarCommands)
            {
                clone.Commands.Add(cmd.Clone() as IcarusCommand);
            }
        }
        protected override ObjectModelReference MakeReferenceInternal()
        {
            ObjectModelReference omr = base.MakeReferenceInternal();
            omr.Title = "ICARUS engine script";
            omr.Path = new string[] { "Game development", "ICARUS engine script" };
            return omr;
        }
    }
}
