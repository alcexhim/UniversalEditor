//
//  SubStationAlphaDataFormat.cs - provides a DataFormat for manipulating subtitles in SubStation Alpha (SSA) format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList;

using UniversalEditor.ObjectModels.Multimedia.Subtitle;

namespace UniversalEditor.DataFormats.Multimedia.Subtitle.SubStationAlpha
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating subtitles in SubStation Alpha (SSA) format.
	/// </summary>
	public class SubStationAlphaDataFormat : WindowsConfigurationDataFormat
	{
		public SubStationAlphaDataFormat()
		{
			PropertyNameValueSeparator = ": ";
			PropertyValuePrefix = String.Empty;
			PropertyValueSuffix = String.Empty;
		}

		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(SubtitleObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Title), "_Title"));
				_dfr.ExportOptions.Add(new CustomOptionFile(nameof(VideoFileName), "_Video file name", String.Empty, "*.asf;*.avi;*.avs;*.d2v;*.m2ts;*.m4v;*.mkv;*.mov;*.mp4;*.mpeg;*.mpg;*.ogm;*.webm;*.wmv;*.ts;*.y4m;*.yuv"));
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

			Group grpScriptInfo = plom.Items.AddGroup("Script Info", new PropertyListItem[]
			{
				new Property("Title", Title),
				new Property("ScriptType", "v4.00+"),
				new Property("WrapStyle", "0"),
				new Property("ScaledBorderAndShadow", "yes"),
				new Property("Collisions", "Normal"),
				new Property("PlayResX", "1280"),
				new Property("PlayResY", "720"),
				new Property("Scroll Position", "0"),
				new Property("Active Line", "0"),
				new Property("Video Zoom Percent", "0.5"),
				new Property("Video File", VideoFileName),
				new Property("Video Aspect Ratio", "c1.77778"),
				new Property("Video Position", "0"),
				new Property("Last Style Storage", "Default"),
				new Property("Export Encoding", "Unicode (UTF-8)"),
				new Property("YCbCr Matrix", "TV.601")
			});
			Group grpV4Styles = plom.Items.AddGroup("V4+ Styles", new PropertyListItem[]
			{
				new Property("Format", "Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding")
			});
			foreach (Style style in subtitle.Styles)
			{
				grpV4Styles.Items.AddProperty("Style", style.Name + "," + style.FontName + "," + style.FontSize + "," + style.PrimaryColor.ToHexadecimalVB() + "," + style.SecondaryColor.ToHexadecimalVB() + "," + style.OutlineColor.ToHexadecimalVB() + "," + style.BackgroundColor.ToHexadecimalVB() + "," + (style.Bold ? "1" : "0") + (style.Italic ? "1" : "0") + (style.Underline ? "1" : "0") + (style.Strikethrough ? "1" : "0") + "," + (style.ScaleX * 100).ToString() + ",100,0,0,1,2,2,2,10,10,10,1");
			}

			Group grpEvents = plom.Items.AddGroup("Events", new PropertyListItem[]
			{
				new Property("Format", "Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text")
			});
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

				grpEvents.Items.AddProperty("Dialogue", "0," + evt.StartTimestamp.ToString("H:MM:SS.TT") + "," + evt.EndTimestamp.ToString("H:MM:SS.TT") + "," + styleName + "," + actorName + "," + "0,0,0,," + sb.ToString());
			}

			objectModels.Push(plom);
		}

		/// <summary>
		/// Gets or sets the title of this subtitle file.
		/// </summary>
		/// <value>The title of this subtitle file.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the name of the video file associated with this subtitle file.
		/// </summary>
		/// <value>The name of the video file associated with this subtitle file.</value>
		public string VideoFileName { get; set; } = String.Empty;
	}
}
