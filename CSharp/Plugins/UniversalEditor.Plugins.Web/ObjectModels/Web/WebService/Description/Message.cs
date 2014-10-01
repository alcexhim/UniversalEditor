using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.WebService.Description
{
    public class Message
    {
        public class MessageCollection
            : System.Collections.ObjectModel.Collection<Message>
        {
            public Message this[string name]
            {
                get
                {
                    foreach (Message message in this)
                    {
                        if (message.Name == name) return message;
                    }
                    return null;
                }
            }
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private MessagePart.MessagePartCollection mvarParts = new MessagePart.MessagePartCollection();
        public MessagePart.MessagePartCollection Parts { get { return mvarParts; } }
    }
}
