using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

namespace UniversalEditor.DataFormats.Designer
{
    public class DesignerXMLDataFormat : XMLDataFormat
    {
        protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeLoadInternal(objectModels);
            objectModels.Push(new MarkupObjectModel());
        }
        protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);

            MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
            DesignerObjectModel dsn = (objectModels.Pop() as DesignerObjectModel);
        }

        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);
        }
    }
}
