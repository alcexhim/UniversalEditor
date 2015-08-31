﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.Chunked.RIFF;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.ExtendedMIDI
{
    public class XMIDataFormat : RIFFDataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(this.GetType());
                _dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
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
            SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);


        }
        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);
            
            SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);
            ChunkedObjectModel chunked = new ChunkedObjectModel();



            objectModels.Push(chunked);
        }
    }
}
