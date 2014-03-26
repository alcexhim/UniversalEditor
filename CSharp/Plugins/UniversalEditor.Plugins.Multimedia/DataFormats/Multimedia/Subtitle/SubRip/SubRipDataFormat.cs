using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia.Subtitle;

namespace UniversalEditor.DataFormats.Multimedia.Subtitle.SubRip
{
    public class SubRipDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(SubtitleObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("SubRip subtitles", new string[] { "*.srt" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            SubtitleObjectModel subtitle = (objectModel as SubtitleObjectModel);
            if (subtitle == null) throw new ObjectModelNotSupportedException();

            IO.Reader tr = base.Accessor.Reader;
            while (!tr.EndOfStream)
            {
                string index = tr.ReadLine();
                if (index == String.Empty || tr.EndOfStream) return;

                string duration = tr.ReadLine();
                if (tr.EndOfStream) return;

                string text = tr.ReadLine();

                Event evt = new Event();
                // evt.Index = Int32.Parse(index);
                string[] timingData = duration.Split(new string[] { "-->" });
                if (timingData.Length >= 1)
                {
                    string startTimeS = timingData[0].Trim();
                    string endTimeS = String.Empty;

                    evt.StartTimestamp = DateTime.Parse(startTimeS);
                    if (timingData.Length >= 2)
                    {
                        endTimeS = timingData[1].Trim();
                        evt.EndTimestamp = DateTime.Parse(startTimeS);
                    }
                }
                evt.Text = text;
                subtitle.Events.Add(evt);

                if (tr.EndOfStream) return;
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            SubtitleObjectModel subtitle = (objectModel as SubtitleObjectModel);
            if (subtitle == null) throw new ObjectModelNotSupportedException();

            IO.Writer tw = base.Accessor.Writer;

            int index = 1;
            foreach (Event evt in subtitle.Events)
            {
                tw.WriteLine(index.ToString());

                StringBuilder sb = new StringBuilder();
                sb.Append(evt.StartTimestamp.ToString("HH:MM:SS,TTT"));
                sb.Append(" --> ");
                sb.Append(evt.EndTimestamp.ToString("HH:MM:SS,TTT"));

                if (!evt.Position.IsEmpty)
                {
                    sb.Append(" X1:");
                    sb.Append(evt.Position.X.ToString());
                    sb.Append(" X2:");
                    sb.Append(evt.Position.X.ToString());
                    sb.Append(" Y1:");
                    sb.Append(evt.Position.Y.ToString());
                    sb.Append(" Y2:");
                    sb.Append(evt.Position.Y.ToString());
                }

                tw.WriteLine(sb.ToString());
                tw.WriteLine(evt.Text);
            }
            tw.Flush();
        }
    }
}
