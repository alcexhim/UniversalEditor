using System;
using System.Collections.Generic;

using UniversalEditor;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList;

using UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankPhonemeDictionary;
using UniversalEditor.DataFormats.Multimedia.Audio.VoicebankPhonemeDictionary;

using UniversalEditor.Accessors;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.UTAU
{
	public class USTDataFormat : WindowsConfigurationDataFormat
	{
		#region Phoneme Dictionary
		private static PhonemeDictionaryObjectModel mvarPhonemeDictionary = null;
		public static void InitializePhonemeDictionary()
		{
			string BasePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			string FileName = BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "PhonemeDictionary.xml";
			InitializePhonemeDictionary(FileName);
		}
		public static void InitializePhonemeDictionary(string FileName)
		{
			if (mvarPhonemeDictionary != null) return;

			mvarPhonemeDictionary = new PhonemeDictionaryObjectModel();
			ObjectModel om = mvarPhonemeDictionary;

			PhonemeDictionaryXMLDataFormat xdf = new PhonemeDictionaryXMLDataFormat();
			Document.Load(om, xdf, new FileAccessor(FileName, false, false, false), true);
		}
		#endregion

		public USTDataFormat()
		{
			InitializePhonemeDictionary();
		}

		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();

			dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
            
            // base.Accessor.DefaultEncoding = IO.Encoding.GetRuntimeEncoding(System.Text.Encoding.GetEncoding("shift-jis"));
            throw new NotImplementedException();

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
					note.Phoneme = LyricToPhoneme(note.Lyric);
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

		private string LyricToPhoneme(string lyric)
		{
			// Use PhonemeDictionary.xml lookup table to find the phoneme that corresponds to the
			// lyric
			Phoneme p = mvarPhonemeDictionary.PhonemeLists[0].GetPhonemeFromMapping(lyric);
			if (p == null) return null;
			return p.Value;
		}
	}
}
