//
//  USTDataFormat.cs - provides a DataFormat for manipulating synthesized audio in Utau UST format
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

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList;

using UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankPhonemeDictionary;
using UniversalEditor.DataFormats.Multimedia.Audio.VoicebankPhonemeDictionary;

using UniversalEditor.Accessors;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.UTAU
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio in Utau UST format.
	/// </summary>
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
			if (mvarPhonemeDictionary != null)
			{
				return;
			}

			mvarPhonemeDictionary = new PhonemeDictionaryObjectModel();
			ObjectModel om = mvarPhonemeDictionary;

			PhonemeDictionaryXMLDataFormat xdf = new PhonemeDictionaryXMLDataFormat();

			try
			{
				Document.Load(om, xdf, new FileAccessor(FileName, false, false, false), true);
			}
			catch (InvalidDataFormatException ex)
			{
				// data format invalid
			}
		}
		#endregion

		public USTDataFormat()
		{
			InitializePhonemeDictionary();
		}

		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			base.Accessor.DefaultEncoding = IO.Encoding.ShiftJIS;

			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PropertyListObjectModel plom = objectModels.Pop() as PropertyListObjectModel;
			SynthesizedAudioObjectModel sa = objectModels.Pop() as SynthesizedAudioObjectModel;

			Group gSetting = plom.Items.OfType<Group>("#SETTING");
			sa.Tempo = double.Parse(gSetting.Items.OfType<Property>("Tempo")?.Value?.ToString());
			sa.Name = gSetting.Items.OfType<Property>("ProjectName")?.Value?.ToString();
			int trackCount = int.Parse(gSetting.Items.OfType<Property>("Tracks")?.Value?.ToString());
			int groupStart = 1;
			for (int i = 0; i < trackCount; i++)
			{
				SynthesizedAudioTrack track = new SynthesizedAudioTrack();
				Group grp = plom.Items[groupStart] as Group;

				int position = 0;
				while (grp.Name != "#TRACKEND")
				{
					SynthesizedAudioCommandNote note = null;
					if (grp.Items.OfType<Property>("Lyric").Value.ToString() == "R")
					{
						note = new SynthesizedAudioCommandRest();
					}
					else
					{
						note = new SynthesizedAudioCommandNote();
					}
					note.Position = position;
					note.Length = int.Parse(grp.Items.OfType<Property>("Length").Value.ToString());
					note.Lyric = grp.Items.OfType<Property>("Lyric").Value.ToString();
					note.Phoneme = LyricToPhoneme(note.Lyric);
					note.Frequency = int.Parse(grp.Items.OfType<Property>("NoteNum").Value.ToString());

					note.PreUtterance = grp.GetPropertyValue<int>("PreUtterance");
					note.VoiceOverlap = grp.GetPropertyValue<int>("VoiceOverlap");
					note.Intensity = grp.GetPropertyValue<int>("Intensity");
					note.Modulation = grp.GetPropertyValue<int>("Moduration");
					note.PBType = grp.GetPropertyValue<int>("PBType");
					note.Pitches = grp.GetPropertyValue<string>("Pitches", String.Empty).Split<double>(new char[] { ',' });
					note.Envelope = grp.GetPropertyValue<string>("Envelope", String.Empty).Split(new char[] { ',' });
					note.VBR = grp.GetPropertyValue<string>("VBR", String.Empty).Split<double>(new char[] { ',' });

					track.Commands.Add(note);
					groupStart++;
					grp = plom.Items[groupStart] as Group;

					position += (int)note.Length;
				}
				sa.Tracks.Add(track);
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			SynthesizedAudioObjectModel sa = objectModels.Pop() as SynthesizedAudioObjectModel;
			PropertyListObjectModel plom = new PropertyListObjectModel();

			plom.Items.Add(new Group("#SETTING", new PropertyListItem[]
			{
				new Property("Tempo", sa.Tempo),
				new Property("Tracks", sa.Tracks.Count.ToString()),
				new Property("ProjectName", sa.Name),
				new Property("VoiceDir", "%VOICE%"),
				new Property("OutFile", System.IO.Path.ChangeExtension(System.IO.Path.GetFileName(Accessor.GetFileName()), ".wav")),
				new Property("CacheDir", ".cache"),
				new Property("Tool1", "wavtool.exe"),
				new Property("Tool2", "resampler.exe"),
				new Property("Mode2", true),
				new Property("Flags", "g8BRE10H10")
			}));

			for (int i = 0; i < sa.Tracks.Count; i++)
			{
				for (int k = 0; k < sa.Tracks[i].Commands.Count; k++)
				{
					if (sa.Tracks[i].Commands[k] is SynthesizedAudioCommandNote)
					{
						SynthesizedAudioCommandNote note = sa.Tracks[i].Commands[k] as SynthesizedAudioCommandNote;

						Group gNote = new Group(String.Format("#{0}", i.ToString().PadLeft(4, '0')), new PropertyListItem[]
						{
							new Property("Length", note.Length),
							new Property("Lyric", note.Lyric),
							new Property("NoteNum", note.Frequency),
							new Property("PreUtterance", note.PreUtterance),
							new Property("VoiceOverlap", note.VoiceOverlap),
							new Property("Intensity", note.Intensity),
							new Property("Moduration", note.Modulation),
							new Property("Envelope", String.Join(",", note.Envelope))
						});
						plom.Items.Add(gNote);
					}
				}
				plom.Items.Add(new Group("#TRACKEND"));
			}

			objectModels.Push(plom);
		}

		private string LyricToPhoneme(string lyric)
		{
			// Use PhonemeDictionary.xml lookup table to find the phoneme that corresponds to the
			// lyric
			if (mvarPhonemeDictionary.PhonemeLists.Count > 0)
			{
				for (int i = 0; i < mvarPhonemeDictionary.PhonemeLists.Count; i++)
				{
					Phoneme p = mvarPhonemeDictionary.PhonemeLists[i].GetPhonemeFromMapping(lyric);
					if (p != null) return p.Value;
				}
			}
			return null;
		}
	}
}
