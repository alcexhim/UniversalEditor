using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.WebService.Description
{
    public class WebServiceDescriptionObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            if (_omr == null)
            {
                _omr = base.MakeReferenceInternal();
                _omr.Title = "Web Service Description";
                _omr.Path = new string[] { "Web", "Services", "Web Service Description" };
            }
            return _omr;
        }

        public override void Clear()
        {
        }

        public override void CopyTo(ObjectModel where)
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarDescription = String.Empty;
        public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

        private Message.MessageCollection mvarMessages = new Message.MessageCollection();
        public Message.MessageCollection Messages { get { return mvarMessages; } }

        private Port.PortCollection mvarPorts = new Port.PortCollection();
        public Port.PortCollection Ports { get { return mvarPorts; } }
    }
}
