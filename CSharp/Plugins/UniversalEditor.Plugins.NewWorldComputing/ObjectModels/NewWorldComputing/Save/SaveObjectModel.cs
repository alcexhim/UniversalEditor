using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Save
{
    public class SaveObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        public override ObjectModelReference MakeReference()
        {
            if (_omr == null) _omr = base.MakeReference();
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
