//
//  QuickSubTextDataFormat.cs - provides a DataFormat for manipulating subtitle files in QuickSub text format
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

using UniversalEditor.DataFormats.PropertyList.Text;
using UniversalEditor.ObjectModels.Multimedia.Subtitle;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.Multimedia.Subtitle.QuickSub
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating subtitle files in QuickSub text format.
	/// </summary>
	public class QuickSubTextDataFormat : TextPropertyListDataFormat
	{
		public QuickSubTextDataFormat()
		{
			Settings.PropertyNameValueSeparators = new string[] { "\t" };
		}

		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(this.GetType());
				_dfr.Capabilities.Add(typeof(SubtitleObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.None);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			SubtitleObjectModel subtitle = (objectModels.Pop() as SubtitleObjectModel);
			if (subtitle == null) throw new ObjectModelNotSupportedException();

			Group grpEvents = plom.Items.OfType<Group>("Events");
			foreach (PropertyListItem pli in grpEvents.Items)
			{
				Property property = pli as Property;
				if (property == null) continue;

				if (property.Value == null || property.Name == "-")
				{
					// get the previous event
					if (subtitle.Events.Count < 1) continue;

					Event evt = subtitle.Events[subtitle.Events.Count - 1];
					if (property.Value == null)
					{
						evt.Text += "\r\n" + property.Name;
					}
					else
					{
						evt.Text += "\r\n" + property.Value.ToString();
					}
				}
				else
				{
					string[] values = property.Value.ToString().Split(new char[] { '\t' }, 5, StringSplitOptions.RemoveEmptyEntries);

					string[] startTime = property.Name.Split(new char[] { ':' });
					string[] endTime = values[0].Trim().Split(new char[] { ':' });

					int sh = 0, sm = 0, ss = 0;
					if (startTime.Length >= 1)
					{
						ss = Int32.Parse(startTime[startTime.Length - 1]);
						if (startTime.Length >= 2)
						{
							sm = Int32.Parse(startTime[startTime.Length - 2]);
							if (startTime.Length >= 3)
							{
								sh = Int32.Parse(startTime[startTime.Length - 3]);
							}
						}
					}

					string language = values[1].Trim();
					string actor = values[2].Trim();
					string text = values[3].Trim();

					Event evt = new Event();
					evt.StartTimestamp = DateTime.Today.Add(new TimeSpan(sh, sm, ss));
					if (values[0].Trim() != "-")
					{
						int eh = 0, em = 0, es = 0;
						if (endTime.Length >= 1)
						{
							es = Int32.Parse(endTime[endTime.Length - 1]);
							if (endTime.Length >= 2)
							{
								em = Int32.Parse(endTime[endTime.Length - 2]);
								if (endTime.Length >= 3)
								{
									eh = Int32.Parse(endTime[endTime.Length - 3]);
								}
							}
						}
						evt.EndTimestamp = DateTime.Today.Add(new TimeSpan(eh, em, es));
					}
					else
					{
						evt.EndTimestamp = evt.StartTimestamp.AddSeconds(5);
					}

					evt.Actor = new Actor();
					evt.Actor.Name = actor;
					evt.Text = text;
					subtitle.Events.Add(evt);
				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			SubtitleObjectModel subtitle = (objectModels.Pop() as SubtitleObjectModel);
			PropertyListObjectModel plom = new PropertyListObjectModel();

			plom.Title = "QuickSub Text File Format";

			objectModels.Push(plom);
		}
	}
}
