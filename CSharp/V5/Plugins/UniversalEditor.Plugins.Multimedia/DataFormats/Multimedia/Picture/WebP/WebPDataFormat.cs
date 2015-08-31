using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.WebP
{
    public class WebPDataFormat : RIFFDataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(this.GetType());
                _dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
                _dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.Bootstrap);
            }
            return _dfr;
        }

        protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeLoadInternal(objectModels);
            objectModels.Push(new ChunkedObjectModel());
        }
        protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);

            ChunkedObjectModel chunked = (objectModels.Pop() as ChunkedObjectModel);
            PictureObjectModel pic = (objectModels.Pop() as PictureObjectModel);

            RIFFGroupChunk WEBP = (chunked.Chunks["WEBP"] as RIFFGroupChunk);
            if (WEBP == null) throw new InvalidDataFormatException("File does not contain a \"WEBP\" chunk");

            throw new NotImplementedException();
        }
    }
}
