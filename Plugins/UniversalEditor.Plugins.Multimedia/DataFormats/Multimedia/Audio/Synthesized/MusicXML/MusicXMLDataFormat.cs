//
//  MusicXMLDataFormat.cs - provides a DataFormat for manipulating synthesized audio in MusicXML format
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

using System.Collections.Generic;

using UniversalEditor.Accessors;
using UniversalEditor.Common;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
using System.Linq;
using System;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MusicXML
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio in MusicXML format.
	/// </summary>
	public class MusicXMLDataFormat : XMLDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			MarkupObjectModel mom = new MarkupObjectModel();
			objectModels.Push(mom);
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			MarkupObjectModel mom = objectModels.Pop() as MarkupObjectModel;
			SynthesizedAudioObjectModel au = objectModels.Pop() as SynthesizedAudioObjectModel;
			MarkupTagElement score_partwise = (mom.FindElement("score-partwise") as MarkupTagElement);
			if (score_partwise != null)
			{
				MarkupTagElement tagPartList = score_partwise.Elements["part-list"] as MarkupTagElement;
				if (tagPartList == null)
				{
					throw new InvalidDataFormatException("MusicXML score-partwise has no part-list tag");
				}

				foreach (MarkupTagElement tagScorePart in tagPartList.Elements.OfType<MarkupTagElement>())
				{
					SynthesizedAudioTrack track = new SynthesizedAudioTrack();
					if (tagScorePart.Attributes["id"] != null)
					{
						track.ID = tagScorePart.Attributes["id"].Value;
					}
					foreach (MarkupTagElement tagScorePartChild in tagScorePart.Elements.OfType<MarkupTagElement>())
					{
						switch (tagScorePartChild.Name)
						{
							case "link":
							{
								string rel = tagScorePartChild.Attributes["rel"]?.Value;
								switch (rel)
								{
									case "synthesizer":
									{
										if (Accessor is FileAccessor)
										{
											FileAccessor file = (Accessor as FileAccessor);
											if (file.FileName != null)
											{
												string fileName = Path.MakeAbsolutePath(tagScorePartChild.Attributes["href"].Value, System.IO.Path.GetDirectoryName(file.FileName));
												if (Reflection.GetAvailableObjectModel<VoicebankObjectModel>(new FileAccessor(fileName), out VoicebankObjectModel synth))
												{
													track.Synthesizer = synth;
												}
											}
										}
										break;
									}
								}
								break;
							}
							case "part-name":
							{
								track.Name = tagScorePartChild.Value;
								break;
							}
							case "score-instrument":
							{
								// instrument-name
								break;
							}
							case "midi-instrument":
							{
								// midi-channel, midi-program, volume, pan
								break;
							}
						}
					}
					au.Tracks.Add(track);
				}

				MarkupTagElement[] tagParts = score_partwise.GetElementsByTagName("part");
				foreach (MarkupTagElement tag in tagParts)
				{
					if (tag.Attributes["id"] != null)
					{
						SynthesizedAudioTrack currentTrack = au.Tracks[tag.Attributes["id"].Value];
						if (currentTrack != null)
						{
							double ntpos = 0;
							foreach (MarkupElement el2 in tag.Elements)
							{
								MarkupTagElement tag2 = el2 as MarkupTagElement;
								if (tag2.Name == "measure")
								{
									foreach (MarkupElement elMeasureItem in tag2.Elements)
									{
										MarkupTagElement tagMeasureItem = elMeasureItem as MarkupTagElement;
										if (tagMeasureItem != null)
										{
											switch (tagMeasureItem.Name)
											{
												case "note":
												{
													MarkupTagElement tagRest = tagMeasureItem.Elements["rest"] as MarkupTagElement;
													SynthesizedAudioCommand note = null;
													if (tagRest != null)
													{
														note = new SynthesizedAudioCommandRest();
													}
													else
													{
														note = new SynthesizedAudioCommandNote();

														MarkupTagElement tagPitch = tagMeasureItem.Elements["pitch"] as MarkupTagElement;
														if (tagPitch != null)
														{
															MarkupTagElement tagStep = tagPitch.Elements["step"] as MarkupTagElement;
															MarkupTagElement tagOctave = tagPitch.Elements["octave"] as MarkupTagElement;

															(note as SynthesizedAudioCommandNote).Frequency = PitchToFrequency(tagStep.Value, int.Parse(tagOctave.Value));
														}

														foreach (MarkupElement elNoteItem in tagMeasureItem.Elements)
														{
															MarkupTagElement tagNoteItem = elNoteItem as MarkupTagElement;
															if (tagNoteItem != null)
															{
																MarkupElement elPhoneme = tagNoteItem.FindElement("sing", "phoneme");
																MarkupElement elLyric = tagNoteItem.FindElement("sing", "lyric");
																if (elPhoneme != null)
																{
																	(note as SynthesizedAudioCommandNote).Phoneme = elPhoneme.Value;
																}
																if (elLyric != null)
																{
																	(note as SynthesizedAudioCommandNote).Lyric = elLyric.Value;
																}
																else
																{
																	elLyric = tagNoteItem.FindElement("lyric");
																	if (elLyric != null)
																	{
																		MarkupTagElement tagLyric = (elLyric as MarkupTagElement);

																		MarkupTagElement tagSyllabic = tagLyric.Elements["syllabic"] as MarkupTagElement;
																		MarkupTagElement tagText = tagLyric.Elements["text"] as MarkupTagElement;
																		(note as SynthesizedAudioCommandNote).Lyric = tagText.Value;
																	}
																}
															}
														}
													}

													MarkupTagElement tagDuration = tagMeasureItem.Elements["duration"] as MarkupTagElement;
													MarkupTagElement tagVoice = tagMeasureItem.Elements["voice"] as MarkupTagElement;
													if (tagDuration != null)
													{
														// FIXME: 100 is BPM of a quarter note?
														(note as SynthesizedAudioCommandNote).Length = double.Parse(tagDuration.Value) * 100;
													}

													(note as SynthesizedAudioCommandNote).Position = (int)ntpos;
													ntpos += (note as SynthesizedAudioCommandNote).Length;

													if (note != null)
													{
														currentTrack.Commands.Add(note);
													}
													break;
												}
												case "attributes":
												{
													break;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// thanks https://stackoverflow.com/questions/38838145
		static int FastBinarySearch<T>(T[] arr, T i) where T : IComparable
		{
			int low = 0, high = arr.Length - 1, mid;

			while (low <= high)
			{
				mid = (low + high) / 2;

				if (i.CompareTo(arr[mid]) < 0)
					high = mid - 1;

				else if (i.CompareTo(arr[mid]) > 0)
					low = mid + 1;

				else
					return mid;
			}
			return -1;
		}

		private double PitchToFrequency(string note, int octave)
		{
			string[] noteNames = new string[]
			{
				"A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#"
			};
			int noteNameIndex = FastBinarySearch<string>(noteNames, note);

			double factor = 1.059463094;
			double initialFrequency = 55.0;
			double freqOctave = initialFrequency * Math.Pow(2, octave - 1);
			double freqNote = freqOctave * Math.Pow(2, ((double)noteNameIndex / 12));

			return freqNote;
		}
	}
}
