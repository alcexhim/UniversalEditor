using System;
using System.Collections.Generic;

using UniversalEditor;
using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Synthesized;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Synthesized.UTAU
{
	public class USTDataFormat : WindowsConfigurationDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();

			dfr.Filters.Add("UTAU voice sequence", new string[] { "*.ust" });
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			PropertyListObjectModel plom = objectModels.Pop() as PropertyListObjectModel;
			SynthesizedAudioObjectModel sa = objectModels.Pop() as SynthesizedAudioObjectModel;
			sa.Tempo = double.Parse(plom.Groups["#SETTING"].Properties["Tempo"].Value.ToString());
			sa.Name = plom.Groups["#SETTING"].Properties["ProjectName"].Value.ToString();
			int trackCount = int.Parse(plom.Groups["#SETTING"].Properties["Tracks"].Value.ToString());
			int groupStart = 1;
			for (int i = 0; i < trackCount; i++)
			{
				SynthesizedAudioTrack track = new SynthesizedAudioTrack();
				Group grp = plom.Groups[groupStart];
				while (grp.Name != "#TRACKEND")
				{
					SynthesizedAudioCommandNote note = null;
					if (grp.Properties["Lyric"].Value.ToString() == "R")
					{
						note = new SynthesizedAudioCommandRest();
					}
					else
					{
						note = new SynthesizedAudioCommandNote();
					}
					note.Length = int.Parse(grp.Properties["Length"].Value.ToString());
					note.Lyric = grp.Properties["Lyric"].Value.ToString();
					note.Frequency = int.Parse(grp.Properties["NoteNum"].Value.ToString());
					if (grp.Properties["PreUtterance"] != null && grp.Properties["PreUtterance"].Value != null && !string.IsNullOrEmpty(grp.Properties["PreUtterance"].Value.ToString()))
					{
						note.PreUtterance = int.Parse(grp.Properties["PreUtterance"].Value.ToString());
					}
					if (grp.Properties["VoiceOverlap"] != null && grp.Properties["VoiceOverlap"].Value != null && !string.IsNullOrEmpty(grp.Properties["VoiceOverlap"].Value.ToString()))
					{
						note.VoiceOverlap = int.Parse(grp.Properties["VoiceOverlap"].Value.ToString());
					}
					if (grp.Properties["Intensity"] != null && grp.Properties["Intensity"].Value != null && !string.IsNullOrEmpty(grp.Properties["Intensity"].Value.ToString()))
					{
						note.Intensity = int.Parse(grp.Properties["Intensity"].Value.ToString());
					}
					if (grp.Properties["Moduration"] != null && grp.Properties["Moduration"].Value != null && !string.IsNullOrEmpty(grp.Properties["Moduration"].Value.ToString()))
					{
						note.Modulation = int.Parse(grp.Properties["Moduration"].Value.ToString());
					}
					if (grp.Properties["PBType"] != null && grp.Properties["PBType"].Value != null && !string.IsNullOrEmpty(grp.Properties["PBType"].Value.ToString()))
					{
						note.PBType = int.Parse(grp.Properties["PBType"].Value.ToString());
					}
					if (grp.Properties["Piches"] != null && grp.Properties["Piches"].Value != null && !string.IsNullOrEmpty(grp.Properties["Piches"].Value.ToString()))
					{
						note.Pitches = grp.Properties["Piches"].Value.ToString().Split<double>(new char[] { ',' });
					}
					if (grp.Properties["Envelope"] != null && grp.Properties["Envelope"].Value != null && !string.IsNullOrEmpty(grp.Properties["Envelope"].Value.ToString()))
					{
						note.Envelope = grp.Properties["Envelope"].Value.ToString().Split(new char[]
						{
							','
						});
					}
					if (grp.Properties["VBR"] != null && grp.Properties["VBR"].Value != null && !string.IsNullOrEmpty(grp.Properties["VBR"].Value.ToString()))
					{
						note.VBR = grp.Properties["VBR"].Value.ToString().Split<double>(new char[] { ',' });
					}
					track.Commands.Add(note);
					groupStart++;
					grp = plom.Groups[groupStart];
				}
				sa.Tracks.Add(track);
			}
		}
	}
}
