using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NWCSceneLayout.SceneObjects
{
    public class SceneObjectLabel : SceneObject
    {
        private string mvarText = String.Empty;
        public string Text { get { return mvarText; } set { mvarText = value; } }

        private string mvarFontFileName = String.Empty;
        public string FontFileName { get { return mvarFontFileName; } set { mvarFontFileName = value; } }
    }
}
