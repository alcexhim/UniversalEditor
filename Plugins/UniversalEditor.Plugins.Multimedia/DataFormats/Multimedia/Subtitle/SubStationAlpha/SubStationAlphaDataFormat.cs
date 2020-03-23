using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList;

using UniversalEditor.ObjectModels.Multimedia.Subtitle;
using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Subtitle.SubStationAlpha
{
    public class SubStationAlphaDataFormat : WindowsConfigurationDataFormat
    {
        public SubStationAlphaDataFormat()
        {
            PropertyNameValueSeparator = ": ";
            PropertyValuePrefix = String.Empty;
            PropertyValueSuffix = String.Empty;
        }

        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(GetType());
                _dfr.Capabilities.Add(typeof(SubtitleObjectModel), DataFormatCapabilities.All);
                _dfr.ExportOptions.Add(new CustomOptionText(nameof(Title), "&Title:"));
                _dfr.ExportOptions.Add(new CustomOptionFile(nameof(VideoFileName), "&Video file:", String.Empty, "*.asf;*.avi;*.avs;*.d2v;*.m2ts;*.m4v;*.mkv;*.mov;*.mp4;*.mpeg;*.mpg;*.ogm;*.webm;*.wmv;*.ts;*.y4m;*.yuv"));
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
            SubtitleObjectModel subtitle = (objectModels.Pop() as SubtitleObjectModel);
            if (subtitle == null) throw new ObjectModelNotSupportedException();

            PropertyListObjectModel plom = new PropertyListObjectModel();

            Group grpScriptInfo = plom.Groups.Add("Script Info");
            grpScriptInfo.Properties.Add("Title", mvarTitle);
            grpScriptInfo.Properties.Add("ScriptType", "v4.00+");
            grpScriptInfo.Properties.Add("WrapStyle", "0");
            grpScriptInfo.Properties.Add("ScaledBorderAndShadow", "yes");
            grpScriptInfo.Properties.Add("Collisions", "Normal");
            grpScriptInfo.Properties.Add("PlayResX", "1280");
            grpScriptInfo.Properties.Add("PlayResY", "720");
            grpScriptInfo.Properties.Add("Scroll Position", "0");
            grpScriptInfo.Properties.Add("Active Line", "0");
            grpScriptInfo.Properties.Add("Video Zoom Percent", "0.5");
            grpScriptInfo.Properties.Add("Video File", mvarVideoFileName);
            grpScriptInfo.Properties.Add("Video Aspect Ratio", "c1.77778");
            grpScriptInfo.Properties.Add("Video Position", "0");
            grpScriptInfo.Properties.Add("Last Style Storage", "Default");
            grpScriptInfo.Properties.Add("Export Encoding", "Unicode (UTF-8)");
            grpScriptInfo.Properties.Add("YCbCr Matrix", "TV.601");

            Group grpV4Styles = plom.Groups.Add("V4+ Styles");
            grpV4Styles.Properties.Add("Format", "Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding");
            foreach (Style style in subtitle.Styles)
            {
                grpV4Styles.Properties.Add("Style", style.Name + "," + style.FontName + "," + style.FontSize + "," + style.PrimaryColor.ToHexadecimalVB() + "," + style.SecondaryColor.ToHexadecimalVB() + "," + style.OutlineColor.ToHexadecimalVB() + "," + style.BackgroundColor.ToHexadecimalVB() + "," + (style.Bold ? "1" : "0") + (style.Italic ? "1" : "0") + (style.Underline ? "1" : "0") + (style.Strikethrough ? "1" : "0") + "," + (style.ScaleX * 100).ToString() + ",100,0,0,1,2,2,2,10,10,10,1");
            }

            Group grpEvents = plom.Groups.Add("Events");
            grpEvents.Properties.Add("Format", "Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text");
            foreach (Event evt in subtitle.Events)
            {
                StringBuilder sb = new StringBuilder();
                if (!evt.Position.IsEmpty)
                {
                    sb.Append("{\\pos(" + evt.Position.X + "," + evt.Position.Y + ")}");
                }
                sb.Append(evt.Text);

                string styleName = String.Empty;
                if (evt.Style != null)
                {
                    styleName = evt.Style.Name;
                }
                else if (subtitle.Styles.Count > 0)
                {
                    styleName = subtitle.Styles[0].Name;
                }
                string actorName = String.Empty;
                if (evt.Actor != null) actorName = evt.Actor.Name;

                grpEvents.Properties.Add("Dialogue", "0," + evt.StartTimestamp.ToString("H:MM:SS.TT") + "," + evt.EndTimestamp.ToString("H:MM:SS.TT") + "," + styleName + "," + actorName + "," + "0,0,0,," + sb.ToString());
            }

            objectModels.Push(plom);
        }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private string mvarVideoFileName = String.Empty;
        public string VideoFileName { get { return mvarVideoFileName; } set { mvarVideoFileName = value; } }
    }
}
