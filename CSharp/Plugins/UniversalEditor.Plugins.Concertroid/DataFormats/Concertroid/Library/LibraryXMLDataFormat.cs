using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Concertroid.Library;
using UniversalEditor.ObjectModels.Concertroid;

namespace UniversalEditor.DataFormats.Concertroid.Library
{
    public class LibraryXMLDataFormat : XMLDataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(GetType());
                _dfr.Capabilities.Add(typeof(LibraryObjectModel), DataFormatCapabilities.All);
                _dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
                _dfr.Filters.Add("Concertroid XML library", new string[] { "*.library" });
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
            LibraryObjectModel library = (objectModels.Pop() as LibraryObjectModel);
            if (library == null) throw new ObjectModelNotSupportedException();

            MarkupTagElement tagConcertroidLibrary = (mom.Elements["ConcertroidLibrary"] as MarkupTagElement);
            if (tagConcertroidLibrary == null) throw new InvalidDataFormatException("File does not contain top-level tag \"ConcertroidLibrary\"");

            MarkupTagElement tagProducers = (tagConcertroidLibrary.Elements["Producers"] as MarkupTagElement);
            if (tagProducers != null)
            {
                foreach (MarkupElement elProducer in tagProducers.Elements)
                {
                    MarkupTagElement tagProducer = (elProducer as MarkupTagElement);
                    if (tagProducer == null) continue;
                    if (tagProducer.FullName != "Producer") continue;

                    MarkupAttribute attProducerID = tagProducer.Attributes["ID"];
                    if (attProducerID == null) continue;

                    Producer p = new Producer();
                    p.ID = new Guid(attProducerID.Value);

                    MarkupTagElement tagInformation = (tagProducer.Elements["Information"] as MarkupTagElement);
                    if (tagInformation != null)
                    {
                        MarkupTagElement tagGivenName = (tagInformation.Elements["GivenName"] as MarkupTagElement);
                        if (tagGivenName != null) p.FirstName = tagGivenName.Value;

                        MarkupTagElement tagFamilyName = (tagInformation.Elements["FamilyName"] as MarkupTagElement);
                        if (tagFamilyName != null) p.LastName = tagFamilyName.Value;
                    }
                    library.Producers.Add(p);
                }
            }
        }
        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);

            LibraryObjectModel library = (objectModels.Pop() as LibraryObjectModel);
            MarkupObjectModel mom = new MarkupObjectModel();

            MarkupTagElement tagConcertroid = new MarkupTagElement();
            tagConcertroid.FullName = "Concertroid";
            tagConcertroid.Attributes.Add("Version", "1.0");



            mom.Elements.Add(tagConcertroid);
            objectModels.Push(mom);
        }
    }
}
