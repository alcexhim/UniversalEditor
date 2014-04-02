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

namespace UniversalEditor.DataFormats.Concertroid.Concert
{
    public class ConcertBinaryDataFormat : VersatileContainerV1DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(GetType());
                _dfr.Capabilities.Add(typeof(VersatileContainerObjectModel), DataFormatCapabilities.Bootstrap);
                _dfr.Capabilities.Add(typeof(ConcertObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Concertroid compili hed binary", new string[] { "*.cvb" });
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

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                bw.Write(StringTable.Count);
                foreach (KeyValuePair<int, string> kvp in StringTable)
                {
                    bw.WriteNullTerminatedString(kvp.Value);
                }
                bw.Close();
                vcom.Sections.Add("StringTable", ms.ToArray());
            }
            #endregion

            Dictionary<Musician, int> MusicianIndices = new Dictionary<Musician, int>();
            #region Concert
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                bw.Write(StringTable.GetValue2(0));
                bw.Close();
                vcom.Sections.Add("Concert", ms.ToArray());
            }
            #endregion
            #region Musicians
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                int count = 0;
                foreach (Musician musician in concert.BandMusicians)
                {
                    bw.Write(MusicianNameIndices[musician]);
                    bw.Write(MusicianInstrumentIndices[musician]);
                    MusicianIndices[musician] = count;
                    count++;
                }
                foreach (Musician musician in concert.GuestMusicians)
                {
                    bw.Write(MusicianNameIndices[musician]);
                    bw.Write(MusicianInstrumentIndices[musician]);
                    MusicianIndices[musician] = count;
                    count++;
                }
                bw.Close();
                vcom.Sections.Add("Musicians", ms.ToArray());
            }
            #endregion
            #region BandMusicianReferences
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                bw.WriteNullTerminatedString(concert.BandName);
                bw.Write(concert.BandMusicians.Count);
                foreach (Musician musician in concert.BandMusicians)
                {
                    bw.Write(MusicianIndices[musician]);
                }
                bw.Close();
                vcom.Sections.Add("BandMusicianReferences", ms.ToArray());
            }
            #endregion
            #region GuestMusicianReferences
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (Musician musician in concert.GuestMusicians)
                {
                    bw.Write(MusicianIndices[musician]);
                }
                bw.Close();
                vcom.Sections.Add("GuestMusicianReferences", ms.ToArray());
            }
            #endregion

            objectModels.Push(vcom);
        }

    }
}
