using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.PropertyList.Text;
using UniversalEditor.ObjectModels.Multimedia.Subtitle;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.Multimedia.Subtitle.QuickSub
{
	public class QuickSubTextDataFormat : TextPropertyListDataFormat
	{
		public QuickSubTextDataFormat()
		{
			Settings.PropertyNameValueSeparators = new string[] { "\t" };
		}

		private static DataFormatReference _dfr = null;
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

			Group grpEvents = plom.Groups["Events"];
			foreach (Property property in grpEvents.Properties)
			{
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
