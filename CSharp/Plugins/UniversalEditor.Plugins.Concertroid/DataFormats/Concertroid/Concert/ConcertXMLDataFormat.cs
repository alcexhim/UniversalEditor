using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Concertroid;
using UniversalEditor.ObjectModels.Concertroid.Concert;

namespace UniversalEditor.DataFormats.Concertroid.Concert
{
    public class ConcertXMLDataFormat : XMLDataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(GetType());
                _dfr.Capabilities.Add(typeof(ConcertObjectModel), DataFormatCapabilities.All);
                _dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
                _dfr.Filters.Add("Concertroid XML concert", new string[] { "*.concert" });
            }
            return _dfr;
        }

        protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeLoadInternal(objectModels);
            objectModels.Push(new MarkupObjectModel());
        }
        protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);

            MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
            ConcertObjectModel concert = (objectModels.Pop() as ConcertObjectModel);


        }
        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);

            ConcertObjectModel concert = (objectModels.Pop() as ConcertObjectModel);
            MarkupObjectModel mom = new MarkupObjectModel();

            MarkupTagElement tagConcertroid = new MarkupTagElement();
            tagConcertroid.FullName = "Concertroid";
            tagConcertroid.Attributes.Add("Version", "1.0");



            mom.Elements.Add(tagConcertroid);
            objectModels.Push(mom);
        }
    }
}
