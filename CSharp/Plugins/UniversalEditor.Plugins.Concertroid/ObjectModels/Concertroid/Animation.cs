using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid
{
    public class Animation
    {
        public class AnimationCollection
            : System.Collections.ObjectModel.Collection<Animation>
        {
        }

        private string mvarFileName = String.Empty;
        public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }
    }
}
