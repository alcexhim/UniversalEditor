using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandPrint : IcarusPredefinedCommand
    {
        public override string Name { get { return "print"; } }

        private string mvarText = String.Empty;
        public string Text { get { return mvarText; } set { mvarText = value; } }

        public override object Clone()
        {
            IcarusCommandPrint clone = new IcarusCommandPrint();
            clone.Text = (mvarText.Clone() as string);
            return clone;
        }
    }
}
