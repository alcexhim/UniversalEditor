//
//  SubRipDataFormat.cs - provides a DataFormat for manipulating subtitles in SubRip (SRT) format
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
using System.Text;

using UniversalEditor.ObjectModels.Multimedia.Subtitle;

namespace UniversalEditor.DataFormats.Multimedia.Subtitle.SubRip
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating subtitles in SubRip (SRT) format.
	/// </summary>
	public class SubRipDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SubtitleObjectModel), DataFormatCapabilities.All);
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
