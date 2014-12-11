using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MIDI;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.Accessors;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.Vocaloid
{
	public class VSQDataFormat : MIDIDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();
			dfr.Filters.Add("VOCALOID2 voice sequence", new string[] { "*.vsq" });
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			SynthesizedAudioObjectModel audio = objectModels.Pop() as SynthesizedAudioObjectModel;
			for (int i = 1; i < audio.Tracks.Count; i++)
			{
				SynthesizedAudioTrack track = audio.Tracks[i];
				string text = string.Empty;
				foreach (SynthesizedAudioCommand cmd in track.Commands)
				{
					if (cmd is SynthesizedAudioCommandText)
					{
						string textt = (cmd as SynthesizedAudioCommandText).Text;
						if (textt.StartsWith("DM:"))
						{
							textt = textt.Substring(8);
						}
						text += textt;
					}
				}
				PropertyListObjectModel plom = new PropertyListObjectModel();
				ObjectModel om = plom;
                
                WindowsConfigurationDataFormat ini = new WindowsConfigurationDataFormat();
                ini.CommentSignals = new string[0];

				Document.Load(om, ini, new StringAccessor(text), true);

				foreach (Group grp in plom.Groups)
				{
					string text2 = grp.Name;
					switch (text2)
					{
						case "Common":
						case "Master":
						case "Mixer":
						{
							break;
						}
						case "EventList":
						{
							foreach (Property prop in grp.Properties)
							{
								string eventName = prop.Name;
								string eventValue = prop.Value.ToString();
								int thisNotePosition = int.Parse(eventName);
								if (eventValue.StartsWith("ID#"))
								{
									Group eventGroup = plom.Groups[eventValue];
									text2 = eventGroup.Properties["Type"].Value.ToString();
									switch (text2)
									{
										case "Anote":
										{
											SynthesizedAudioCommandNote note = new SynthesizedAudioCommandNote();
											Group lyricInfo = plom.Groups[eventGroup.Properties["LyricHandle"].Value.ToString()];
											string phonemeInfo = lyricInfo.Properties["L0"].Value.ToString();
											int length = int.Parse(eventGroup.Properties["Length"].Value.ToString());
											int noteNumber = int.Parse(eventGroup.Properties["Note#"].Value.ToString());
											string[] phonemeInfos = phonemeInfo.Split(new char[]
											{
												','
											}, "\"");
											note.Lyric = phonemeInfos[0];
											note.Phoneme = phonemeInfos[1];
											note.Length = length;
											note.Frequency = 128 - noteNumber;
											track.Commands.Add(note);
											if (grp.Properties.IndexOf(prop) < grp.Properties.Count - 1)
											{
												int nextNotePosition = int.Parse(grp.Properties[grp.Properties.IndexOf(prop) + 1].Name);
												if (thisNotePosition + note.Length < nextNotePosition)
												{
													int restLength = nextNotePosition - thisNotePosition;
													SynthesizedAudioCommandRest rest = new SynthesizedAudioCommandRest();
													rest.Length = restLength;
													track.Commands.Add(rest);
												}
											}
											break;
										}
										case "Singer":
										{
											Group singerInfo = plom.Groups[eventGroup.Properties["IconHandle"].Value.ToString()];
											string singerName = singerInfo.Properties["IDS"].Value.ToString();
											int programChange = int.Parse(singerInfo.Properties["Program"].Value.ToString());
											break;
										}
									}
								}
							}
							break;
						}
					}
				}
			}
		}
	}
}
