using System;
using System.Collections.Generic;

using UniversalEditor.Accessors;
using UniversalEditor.Common;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MusicXML
{
	public class MusicXMLDataFormat : XMLDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();
			dfr.Filters.Add("MusicXML markup", new string[] { "*.mxl" });
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
				foreach (MarkupElement el in score_partwise.Elements)
				{
					MarkupTagElement tag = el as MarkupTagElement;
					if (tag != null)
					{
						SynthesizedAudioTrack currentTrack = null;
						string text = tag.Name;
						if (text != null)
						{
							if (!(text == "part-list"))
							{
								if (text == "part")
								{
									if (tag.Attributes["id"] != null)
									{
										currentTrack = au.Tracks[tag.Attributes["id"].Value];
										if (currentTrack != null)
										{
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
															text = tagMeasureItem.Name;
															if (text != null)
															{
																if (!(text == "attributes"))
																{
																	if (text == "note")
																	{
																		SynthesizedAudioCommandNote note = new SynthesizedAudioCommandNote();
																		foreach (MarkupElement elNoteItem in tagMeasureItem.Elements)
																		{
																			MarkupTagElement tagNoteItem = elNoteItem as MarkupTagElement;
																			if (tagNoteItem != null)
																			{
																				MarkupElement elPhoneme = tagNoteItem.FindElement("sing", "phoneme");
																				MarkupElement elLyric = tagNoteItem.FindElement("sing", "lyric");
																				if (elPhoneme != null)
																				{
																					note.Phoneme = elPhoneme.Value;
																				}
																				if (elLyric != null)
																				{
																					note.Lyric = elLyric.Value;
																				}
																			}
																		}
																		currentTrack.Commands.Add(note);
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
							else
							{
								foreach (MarkupElement elScorePart in tag.Elements)
								{
									if (elScorePart is MarkupTagElement)
									{
										MarkupTagElement tagScorePart = elScorePart as MarkupTagElement;
										SynthesizedAudioTrack track = new SynthesizedAudioTrack();
										if (tagScorePart.Attributes["id"] != null)
										{
											track.ID = tagScorePart.Attributes["id"].Value;
										}
										foreach (MarkupElement elTagScorePartChild in tagScorePart.Elements)
										{
											if (elTagScorePartChild is MarkupTagElement)
											{
												MarkupTagElement tag_elTagScorePartChild = elTagScorePartChild as MarkupTagElement;
												text = tag_elTagScorePartChild.Name;
												if (text != null)
												{
													if (!(text == "part-name"))
													{
														if (text == "link")
														{
															if (tag_elTagScorePartChild.Attributes["rel"] != null)
															{
																text = tag_elTagScorePartChild.Attributes["rel"].Value;
																if (text != null)
																{
																	if (text == "synthesizer")
																	{
                                                                        if (Accessor is FileAccessor)
                                                                        {
                                                                            FileAccessor file = (Accessor as FileAccessor);
                                                                            if (file.FileName != null)
                                                                            {
                                                                                track.Synthesizer = Reflection.GetAvailableObjectModel<VoicebankObjectModel>(Path.MakeAbsolutePath(tag_elTagScorePartChild.Attributes["href"].Value, System.IO.Path.GetDirectoryName(file.FileName)));
                                                                            }
                                                                        }
																	}
																}
															}
														}
													}
													else
													{
														track.Name = tag_elTagScorePartChild.Value;
													}
												}
											}
										}
										au.Tracks.Add(track);
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
