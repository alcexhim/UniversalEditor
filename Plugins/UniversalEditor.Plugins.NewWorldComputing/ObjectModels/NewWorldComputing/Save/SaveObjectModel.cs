using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Save
{
    public class SaveObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            if (_omr == null) _omr = base.MakeReferenceInternal();
            _omr.Title = "New World Computing save game";
            return _omr;
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override void CopyTo(ObjectModel where)
        {
            throw new NotImplementedException();
        }
    }
}
