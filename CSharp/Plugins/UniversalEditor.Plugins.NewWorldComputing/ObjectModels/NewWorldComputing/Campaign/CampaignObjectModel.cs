using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Campaign
{
    public class CampaignObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            _omr = base.MakeReferenceInternal();
            _omr.Title = "New World Computing campaign";
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
