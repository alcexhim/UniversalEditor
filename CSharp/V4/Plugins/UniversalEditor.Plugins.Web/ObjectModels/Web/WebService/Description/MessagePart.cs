using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.WebService.Description
{
    public class MessagePart
    {
        public class MessagePartCollection
            : System.Collections.ObjectModel.Collection<MessagePart>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarElement = String.Empty;
        public string Element { get { return mvarElement; } set { mvarElement = value; } }
    }
}
