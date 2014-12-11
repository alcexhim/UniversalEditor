using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Designer
{
    public class DesignerObjectModel : ObjectModel
    {

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override void CopyTo(ObjectModel where)
        {
            throw new NotImplementedException();
        }

        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            if (_omr == null)
            {
                _omr = base.MakeReferenceInternal();
                _omr.Title = "Designer";
            }
            return _omr;
        }

        private Design.DesignCollection mvarDesigns = new Design.DesignCollection();
        public Design.DesignCollection Designs { get { return mvarDesigns; } }

        private Library.LibraryCollection mvarLibraries = new Library.LibraryCollection();
        public Library.LibraryCollection Libraries { get { return mvarLibraries; } }
    }
}
