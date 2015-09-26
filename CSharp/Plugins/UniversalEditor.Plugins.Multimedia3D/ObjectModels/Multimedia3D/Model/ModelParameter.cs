using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
    public abstract class ModelParameter : ICloneable
    {
        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private ModelParameterAttachment.ModelParameterAttachmentCollection mvarAttachments = new ModelParameterAttachment.ModelParameterAttachmentCollection();
        public ModelParameterAttachment.ModelParameterAttachmentCollection Attachments { get { return mvarAttachments; } }

        public abstract object Clone();
    }
}
