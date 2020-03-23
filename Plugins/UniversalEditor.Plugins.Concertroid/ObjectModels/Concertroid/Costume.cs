using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid
{
    public enum CostumeBehavior
    {
        Overlay = 0,
        Replace = 1
    }
    public class Costume : ICloneable
    {
        public class CostumeCollection
            : System.Collections.ObjectModel.Collection<Costume>
        {
        }

        private CostumeBehavior mvarBehavior = CostumeBehavior.Replace;
        public CostumeBehavior Behavior { get { return mvarBehavior; } set { mvarBehavior = value; } }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private string mvarModelFileName = String.Empty;
        public string ModelFileName { get { return mvarModelFileName; } set { mvarModelFileName = value; } }

        public object Clone()
        {
            Costume clone = new Costume();
            clone.Title = (mvarTitle.Clone() as string);
            clone.ModelFileName = (mvarModelFileName.Clone() as string);
            clone.Behavior = mvarBehavior;
            return clone;
        }
    }
}
