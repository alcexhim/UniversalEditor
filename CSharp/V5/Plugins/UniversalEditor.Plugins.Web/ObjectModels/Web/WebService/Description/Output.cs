﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.WebService.Description
{
    public class Output
    {
        public class OutputCollection
            : System.Collections.ObjectModel.Collection<Output>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private Message mvarMessage = null;
        public Message Message { get { return mvarMessage; } set { mvarMessage = value; } }
    }
}
