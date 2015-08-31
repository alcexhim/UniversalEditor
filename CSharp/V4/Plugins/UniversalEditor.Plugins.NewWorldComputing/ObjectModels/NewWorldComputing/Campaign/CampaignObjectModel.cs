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
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "New World Computing campaign";
			}
            return _omr;
        }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

        public override void Clear()
        {
			mvarTitle = String.Empty;
			mvarDescription = String.Empty;
        }

        public override void CopyTo(ObjectModel where)
        {
			CampaignObjectModel clone = (where as CampaignObjectModel);
			clone.Description = (mvarDescription.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
        }
    }
}
