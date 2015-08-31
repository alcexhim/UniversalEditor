using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.WebService.Description
{
    public class Input
    {
        public class InputCollection
            : System.Collections.ObjectModel.Collection<Input>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private Message mvarMessage = null;
        public Message Message { get { return mvarMessage; } set { mvarMessage = value; } }
    }
}
