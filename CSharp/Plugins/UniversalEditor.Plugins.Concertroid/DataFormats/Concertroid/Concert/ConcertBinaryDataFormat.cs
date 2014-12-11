using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Concertroid;
using UniversalEditor.ObjectModels.Concertroid.Concert;

using UniversalEditor.ObjectModels.VersatileContainer;
using UniversalEditor.ObjectModels.VersatileContainer.Sections;
using UniversalEditor.DataFormats.VersatileContainer;
using UniversalEditor.Collections.Generic;
using UniversalEditor.Accessors;

namespace UniversalEditor.DataFormats.Concertroid.Concert
{
    public class ConcertBinaryDataFormat : VersatileContainerV1DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(GetType());
                _dfr.Capabilities.Add(typeof(VersatileContainerObjectModel), DataFormatCapabilities.Bootstrap);
                _dfr.Capabilities.Add(typeof(ConcertObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Concertroid compilied binary", new string[] { "*.cvb" });
            }
            return _dfr;
        }

        protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeLoadInternal(objectModels);

            objectModels.Push(new VersatileContainerObjectModel());
        }
        protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);

            VersatileContainerObjectModel vcom = (objectModels.Pop() as VersatileContainerObjectModel);
            ConcertObjectModel concert = (objectModels.Pop() as ConcertObjectModel);


        }

        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);

            ConcertObjectModel concert = (objectModels.Pop() as ConcertObjectModel);
            VersatileContainerObjectModel vcom = new VersatileContainerObjectModel();
            vcom.Title = "Copyright (c) 2013 CVE ; Concertroid compiled assets file";

            Dictionary<Musician, int> MusicianNameIndices = new Dictionary<Musician, int>();
            Dictionary<Musician, int> MusicianInstrumentIndices = new Dictionary<Musician, int>();
            BidirectionalDictionary<int, string> StringTable = new BidirectionalDictionary<int, string>();

            #region StringTable
            {
                StringTable.Add(StringTable.Count, concert.Title);

                foreach (Musician musician in concert.BandMusicians)
                {
                    if (!StringTable.ContainsValue2(musician.FullName)) StringTable.Add(StringTable.Count, musician.FullName);
                    MusicianNameIndices.Add(musician, StringTable.GetValue1(musician.FullName));
                }
                foreach (Musician musician in concert.BandMusicians)
                {
                    if (!StringTable.ContainsValue2(musician.Instrument)) StringTable.Add(StringTable.Count, musician.Instrument);
                    MusicianInstrumentIndices.Add(musician, StringTable.GetValue1(musician.Instrument));
                }

                MemoryAccessor ma = new MemoryAccessor();
                IO.Writer bw = new IO.Writer(ma);
                bw.WriteInt32(StringTable.Count);
                foreach (KeyValuePair<int, string> kvp in StringTable)
                {
                    bw.WriteNullTerminatedString(kvp.Value);
                }
                bw.Close();
                vcom.Sections.Add("StringTable", ma.ToArray());
            }
            #endregion

            Dictionary<Musician, int> MusicianIndices = new Dictionary<Musician, int>();
            #region Concert
            {
                MemoryAccessor ma = new MemoryAccessor();
                IO.Writer bw = new IO.Writer(ma);
                bw.Write(StringTable.GetValue2(0));
                bw.Close();
                vcom.Sections.Add("Concert", ma.ToArray());
            }
            #endregion
            #region Musicians
            {
                MemoryAccessor ma = new MemoryAccessor();
                IO.Writer bw = new IO.Writer(ma);
                int count = 0;
                foreach (Musician musician in concert.BandMusicians)
                {
                    bw.WriteInt32(MusicianNameIndices[musician]);
                    bw.WriteInt32(MusicianInstrumentIndices[musician]);
                    MusicianIndices[musician] = count;
                    count++;
                }
                foreach (Musician musician in concert.GuestMusicians)
                {
                    bw.WriteInt32(MusicianNameIndices[musician]);
                    bw.WriteInt32(MusicianInstrumentIndices[musician]);
                    MusicianIndices[musician] = count;
                    count++;
                }
                bw.Close();
                vcom.Sections.Add("Musicians", ma.ToArray());
            }
            #endregion
            #region BandMusicianReferences
            {
                MemoryAccessor ma = new MemoryAccessor();
                IO.Writer bw = new IO.Writer(ma);
                bw.WriteNullTerminatedString(concert.BandName);
                bw.WriteInt32(concert.BandMusicians.Count);
                foreach (Musician musician in concert.BandMusicians)
                {
                    bw.WriteInt32(MusicianIndices[musician]);
                }
                bw.Close();
                vcom.Sections.Add("BandMusicianReferences", ma.ToArray());
            }
            #endregion
            #region GuestMusicianReferences
            {
                MemoryAccessor ma = new MemoryAccessor();
                IO.Writer bw = new IO.Writer(ma);
                foreach (Musician musician in concert.GuestMusicians)
                {
                    bw.WriteInt32(MusicianIndices[musician]);
                }
                bw.Close();
                vcom.Sections.Add("GuestMusicianReferences", ma.ToArray());
            }
            #endregion

            objectModels.Push(vcom);
        }

    }
}
