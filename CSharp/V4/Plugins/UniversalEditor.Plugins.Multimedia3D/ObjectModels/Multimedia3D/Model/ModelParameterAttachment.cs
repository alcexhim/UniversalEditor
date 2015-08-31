using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
    public class ModelParameterAttachment : ICloneable
    {
        public class ModelParameterAttachmentCollection
            : System.Collections.ObjectModel.Collection<ModelParameterAttachment>
        {
        }

        private IModelObject mvarAttachedObject = null;
        public IModelObject AttachedObject { get { return mvarAttachedObject; } set { mvarAttachedObject = value; } }

        private string mvarPropertyName = String.Empty;
        public string PropertyName { get { return mvarPropertyName; } set { mvarPropertyName = value; } }

        public object Clone()
        {
            ModelParameterAttachment clone = new ModelParameterAttachment();
            clone.AttachedObject = mvarAttachedObject;
            clone.PropertyName = (mvarPropertyName.Clone() as string);
            return clone;
        }
    }
}
