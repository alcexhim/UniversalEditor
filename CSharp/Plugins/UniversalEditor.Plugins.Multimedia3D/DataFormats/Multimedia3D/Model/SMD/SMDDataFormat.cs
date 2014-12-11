using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.SMD
{
    public class SMDDataFormat : SMDBaseDataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(GetType());
                _dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Half-Life 2 SMD", new byte?[][] { new byte?[] { (byte)'v', (byte)'e', (byte)'r', (byte)'s', (byte)'i', (byte)'o', (byte)'n', (byte)' ', (byte)'1' } }, new string[] { "*.smd" });
            }
            return _dfr;
        }
        protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeLoadInternal(objectModels);
        }
        protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);
        }
        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);
        }
    }
}
