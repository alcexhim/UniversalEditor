using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SMD
{
    public class SMDObjectModel : ObjectModel
    {
        private SMDSection.SMDSectionCollection mvarSections = new SMDSection.SMDSectionCollection();
        public SMDSection.SMDSectionCollection Sections { get { return mvarSections; } }

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
                _omr.Title = "SMD";
            }
            return _omr;
        }
    }
}
