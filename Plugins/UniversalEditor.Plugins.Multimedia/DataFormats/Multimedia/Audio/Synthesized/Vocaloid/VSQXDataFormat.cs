using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
using UniversalEditor.ObjectModels.Markup;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.Vocaloid
{
	public class VSQXDataFormat : XMLDataFormat
	{
        private string mvarProductVendor = String.Empty;
        public string ProductVendor { get { return mvarProductVendor; } set { mvarProductVendor = value; } }

        private Version mvarProductVersion = new Version(1, 0);
        public Version ProductVersion { get { return mvarProductVersion; } set { mvarProductVersion = value; } }

		protected override DataFormatReference MakeReferenceInternal()
		{
            DataFormatReference dfr = new DataFormatReference(this.GetType());
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			MarkupObjectModel mom = objectModels.Pop() as MarkupObjectModel;
			SynthesizedAudioObjectModel au = (objectModels.Pop() as SynthesizedAudioObjectModel);
            if (au == null) throw new ObjectModelNotSupportedException();

			MarkupTagElement tagVSQ3 = mom.Elements["vsq3"] as MarkupTagElement;
			if (tagVSQ3 != null)
			{
				foreach (MarkupElement el in tagVSQ3.Elements)
				{
					MarkupTagElement tag = el as MarkupTagElement;
					if (tag != null)
					{
						string text = tag.FullName;
						if (text != null)
						{
							if (!(text == "vender"))
							{
								if (!(text == "version"))
								{
									if (text == "vsTrack")
									{
										SynthesizedAudioTrack track = new SynthesizedAudioTrack();
										MarkupTagElement tagTrackName = tag.Elements["trackName"] as MarkupTagElement;
										if (tagTrackName != null)
										{
											MarkupStringElement tagTrackNameCDATA = tagTrackName.Elements[0] as MarkupStringElement;
											if (tagTrackNameCDATA != null)
											{
												track.Name = tagTrackNameCDATA.Value;
											}
										}
										MarkupTagElement tagTrackComment = tag.Elements["comment"] as MarkupTagElement;
										if (tagTrackComment != null)
										{
											MarkupStringElement tagTrackNameCDATA = tagTrackComment.Elements[0] as MarkupStringElement;
											if (tagTrackNameCDATA != null)
											{
												track.Comment = tagTrackNameCDATA.Value;
											}
										}
										foreach (MarkupElement el2 in tag.Elements)
										{
											MarkupTagElement tag2 = el2 as MarkupTagElement;
											if (tag2 != null)
											{
												text = tag2.FullName;
												if (text != null)
												{
													if (!(text == "stylePlugin"))
													{
														if (text == "note")
														{
															SynthesizedAudioCommandNote note = new SynthesizedAudioCommandNote();
															MarkupTagElement tagPosTick = tag2.Elements["posTick"] as MarkupTagElement;
															if (tagPosTick != null)
															{
																int posTick = 0;
																if (int.TryParse(tagPosTick.Value, out posTick))
																{
																	note.Position = posTick;
																}
															}
															MarkupTagElement tagDurTick = tag2.Elements["durTick"] as MarkupTagElement;
															if (tagDurTick != null)
															{
																int durTick = 0;
																if (int.TryParse(tagDurTick.Value, out durTick))
																{
																	note.Length = durTick;
																}
															}
															MarkupTagElement tagNoteNum = tag2.Elements["noteNum"] as MarkupTagElement;
															if (tagNoteNum != null)
															{
																int noteNum = 0;
																if (int.TryParse(tagNoteNum.Value, out noteNum))
																{
																	note.Frequency = noteNum;
																}
															}
															MarkupTagElement tagVelocity = tag2.Elements["velocity"] as MarkupTagElement;
															if (tagVelocity != null)
															{
																int velocity = 0;
																if (int.TryParse(tagVelocity.Value, out velocity))
																{
																	note.Intensity = velocity;
																}
															}
															MarkupTagElement tagLyric = tag2.Elements["lyric"] as MarkupTagElement;
															if (tagLyric != null)
															{
																MarkupStringElement tagLyricCDATA = tagLyric.Elements[0] as MarkupStringElement;
																if (tagLyricCDATA != null)
																{
																	note.Lyric = tagLyricCDATA.Value;
																}
															}
															MarkupTagElement tagPhoneme = tag2.Elements["phnms"] as MarkupTagElement;
															if (tagPhoneme != null)
															{
																MarkupStringElement tagPhonemeCDATA = tagPhoneme.Elements[0] as MarkupStringElement;
																if (tagPhonemeCDATA != null)
																{
																	note.Phoneme = tagPhonemeCDATA.Value;
																}
															}
															MarkupTagElement tagNoteStyle = tag2.Elements["noteStyle"] as MarkupTagElement;
															if (tagNoteStyle != null)
															{
																foreach (MarkupElement elAttr in tagNoteStyle.Elements)
																{
																	MarkupTagElement tagAttr = elAttr as MarkupTagElement;
																	if (tagAttr != null)
																	{
																		if (tagAttr.Attributes["id"] != null)
																		{
																			if (tagAttr.Name == "attr")
																			{
																				text = tagAttr.Attributes["id"].Value;
																				switch (text)
																				{
																					case "accent":
																					{
																						string value = tagAttr.Value;
																						int realValue = 50;
																						int.TryParse(value, out realValue);
																						note.Accent = realValue;
																						break;
																					}
																					case "bendDep":
																					{
																						string value = tagAttr.Value;
																						int realValue = 8;
																						int.TryParse(value, out realValue);
																						note.PitchBendDepth = realValue;
																						break;
																					}
																					case "bendLen":
																					{
																						string value = tagAttr.Value;
																						int realValue = 0;
																						int.TryParse(value, out realValue);
																						note.PitchBendLength = realValue;
																						break;
																					}
																					case "decay":
																					{
																						string value = tagAttr.Value;
																						int realValue = 50;
																						int.TryParse(value, out realValue);
																						note.Decay = realValue;
																						break;
																					}
																					case "fallPort":
																					{
																						string value = tagAttr.Value;
																						bool realValue2 = false;
																						bool.TryParse(value, out realValue2);
																						note.PortamentoFalling = realValue2;
																						break;
																					}
																					case "opening":
																					{
																						string value = tagAttr.Value;
																						int realValue = 127;
																						int.TryParse(value, out realValue);
																						note.Opening = realValue;
																						break;
																					}
																					case "risePort":
																					{
																						string value = tagAttr.Value;
																						bool realValue2 = false;
																						bool.TryParse(value, out realValue2);
																						note.PortamentoRising = realValue2;
																						break;
																					}
																					case "vibLen":
																					{
																						string value = tagAttr.Value;
																						int realValue = 66;
																						int.TryParse(value, out realValue);
																						note.VibratoLength = realValue;
																						break;
																					}
																					case "vibType":
																					{
																						string value = tagAttr.Value;
																						SynthesizedAudioVibratoType realValue3 = SynthesizedAudioVibratoType.None;
																						try
																						{
																							realValue3 = (SynthesizedAudioVibratoType)Enum.Parse(typeof(SynthesizedAudioVibratoType), value);
																						}
																						catch
																						{
																						}
																						note.VibratoType = realValue3;
																						break;
																					}
																				}
																			}
																			else
																			{
																				if (tagAttr.FullName == "seqAttr")
																				{
																					if (tagAttr.Attributes["id"] != null)
																					{
																						text = tagAttr.Attributes["id"].Value;
																						if (text != null)
																						{
																							if (!(text == "vibDep"))
																							{
																								if (text == "vibRate")
																								{
																									MarkupTagElement tagPosNrm = tagAttr.FindElement(new string[]
																									{
																										"elem", 
																										"posNrm"
																									}) as MarkupTagElement;
																									MarkupTagElement tagElv = tagAttr.FindElement(new string[]
																									{
																										"elem", 
																										"elv"
																									}) as MarkupTagElement;
																									if (tagPosNrm != null)
																									{
																										int posNrm = 0;
																										int.TryParse(tagPosNrm.Value, out posNrm);
																									}
																									if (tagElv != null)
																									{
																										int elv = 0;
																										int.TryParse(tagElv.Value, out elv);
																									}
																								}
																							}
																							else
																							{
																								MarkupTagElement tagPosNrm = tagAttr.FindElement(new string[]
																								{
																									"elem", 
																									"posNrm"
																								}) as MarkupTagElement;
																								MarkupTagElement tagElv = tagAttr.FindElement(new string[]
																								{
																									"elem", 
																									"elv"
																								}) as MarkupTagElement;
																								if (tagPosNrm != null)
																								{
																									int posNrm = 0;
																									int.TryParse(tagPosNrm.Value, out posNrm);
																								}
																								if (tagElv != null)
																								{
																									int elv = 0;
																									int.TryParse(tagElv.Value, out elv);
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
															track.Commands.Add(note);
														}
													}
													else
													{
														Guid StylePluginID = Guid.Empty;
														string StylePluginName = string.Empty;
														Version StylePluginVersion = new Version(1, 0, 0, 0);
														MarkupTagElement tagStylePluginID = tag2.Elements["stylePluginID"] as MarkupTagElement;
														if (tagStylePluginID != null)
														{
															MarkupStringElement tagStylePluginIDCDATA = tagStylePluginID.Elements[0] as MarkupStringElement;
															if (tagStylePluginIDCDATA != null)
															{
																try
																{
																	StylePluginID = new Guid(tagStylePluginIDCDATA.Value);
																}
																catch
																{
																}
															}
														}
														MarkupTagElement tagStylePluginName = tag2.Elements["stylePluginName"] as MarkupTagElement;
														if (tagStylePluginName != null)
														{
															MarkupStringElement tagStylePluginNameCDATA = tagStylePluginName.Elements[0] as MarkupStringElement;
															if (tagStylePluginNameCDATA != null)
															{
																StylePluginName = tagStylePluginNameCDATA.Value;
															}
														}
														MarkupTagElement tagStylePluginVersion = tag2.Elements["version"] as MarkupTagElement;
														if (tagStylePluginVersion != null)
														{
															MarkupStringElement tagStylePluginVersionCDATA = tagStylePluginVersion.Elements[0] as MarkupStringElement;
															if (tagStylePluginVersionCDATA != null)
															{
																StylePluginVersion = new Version(tagStylePluginVersionCDATA.Value);
															}
														}
														SynthesizedAudioStylePlugin plugin = new SynthesizedAudioStylePlugin();
														plugin.ID = StylePluginID;
														plugin.Name = StylePluginName;
														plugin.Version = StylePluginVersion;
													}
												}
											}
										}
										au.Tracks.Add(track);
									}
								}
								else
								{
									MarkupStringElement tagCDATA = tag.Elements[0] as MarkupStringElement;
									if (tagCDATA != null)
									{
										mvarProductVersion = new Version(tagCDATA.Value);
									}
								}
							}
							else
							{
								MarkupStringElement tagCDATA = tag.Elements[0] as MarkupStringElement;
								if (tagCDATA != null)
								{
									mvarProductVendor = tagCDATA.Value;
								}
							}
						}
					}
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			SynthesizedAudioObjectModel au = objectModels.Pop() as SynthesizedAudioObjectModel;
			MarkupObjectModel mom = new MarkupObjectModel();
			MarkupPreprocessorElement xml = new MarkupPreprocessorElement();
			xml.Name = "xml";
			xml.Value = "version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"";
			mom.Elements.Add(xml);
			MarkupTagElement tagVSQ3 = new MarkupTagElement();
			tagVSQ3.Attributes.Add("xmlns", "http://www.yamaha.co.jp/vocaloid/schema/vsq3/");
			tagVSQ3.Attributes.Add("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
			tagVSQ3.Attributes.Add("xsi:schemaLocation", "http://www.yamaha.co.jp/vocaloid/schema/vsq3/vsq3.xsd");
			MarkupTagElement tagVender = new MarkupTagElement();
			tagVender.FullName = "vender";
			MarkupStringElement tagVenderCDATA = new MarkupStringElement();
			tagVenderCDATA.Name = "CDATA";
            tagVenderCDATA.Value = mvarProductVendor;
			tagVender.Elements.Add(tagVenderCDATA);
			tagVSQ3.Elements.Add(tagVender);
			MarkupTagElement tagVersion = new MarkupTagElement();
			tagVersion.FullName = "version";
			MarkupStringElement tagVersionCDATA = new MarkupStringElement();
			tagVersionCDATA.Name = "CDATA";
			tagVersionCDATA.Value = mvarProductVersion.ToString();
			tagVersion.Elements.Add(tagVersionCDATA);
			tagVSQ3.Elements.Add(tagVersion);
			MarkupTagElement tagvVoiceTable = new MarkupTagElement();
			tagvVoiceTable.FullName = "vVoiceTable";
			foreach (VoicebankObjectModel voice in au.Voices)
			{
				MarkupTagElement tagvVoice = new MarkupTagElement();
				tagvVoice.FullName = "vVoice";
				MarkupTagElement tagvBS = new MarkupTagElement();
				tagvBS.FullName = "vBS";
				MarkupElement arg_1AB_0 = tagvBS;
				int num = voice.BankSelect;
				arg_1AB_0.Value = num.ToString();
				tagvVoice.Elements.Add(tagvBS);
				MarkupTagElement tagvPC = new MarkupTagElement();
				tagvPC.FullName = "vPC";
				MarkupElement arg_1E6_0 = tagvPC;
				num = voice.ProgramChange;
				arg_1E6_0.Value = num.ToString();
				tagvVoice.Elements.Add(tagvPC);
				MarkupTagElement tagcompID = new MarkupTagElement();
				tagcompID.FullName = "compID";
				MarkupStringElement tagcompIDCDATA = new MarkupStringElement();
				tagcompIDCDATA.Name = "CDATA";
				tagcompIDCDATA.Value = voice.ID;
				tagcompID.Elements.Add(tagcompIDCDATA);
				tagvVoice.Elements.Add(tagcompID);
				MarkupTagElement tagvVoiceName = new MarkupTagElement();
				tagvVoiceName.FullName = "vVoiceName";
				MarkupStringElement tagvVoiceNameCDATA = new MarkupStringElement();
				tagvVoiceNameCDATA.Name = "CDATA";
				tagvVoiceNameCDATA.Value = voice.ID;
				tagvVoiceName.Elements.Add(tagvVoiceNameCDATA);
				tagvVoice.Elements.Add(tagvVoiceName);
				MarkupTagElement tagvVoiceParam = new MarkupTagElement();
				tagvVoiceParam.FullName = "vVoiceParam";
				MarkupTagElement tagvVoiceParamBRE = new MarkupTagElement();
				tagvVoiceParamBRE.FullName = "bre";
				MarkupElement arg_2E9_0 = tagvVoiceParamBRE;
				byte b = voice.SynthesisParameters.Breathiness;
				arg_2E9_0.Value = b.ToString();
				tagvVoiceParam.Elements.Add(tagvVoiceParamBRE);
				MarkupTagElement tagvVoiceParamBRI = new MarkupTagElement();
				tagvVoiceParamBRI.FullName = "bri";
				MarkupElement arg_329_0 = tagvVoiceParamBRI;
				b = voice.SynthesisParameters.Brightness;
				arg_329_0.Value = b.ToString();
				tagvVoiceParam.Elements.Add(tagvVoiceParamBRI);
				MarkupTagElement tagvVoiceParamCLE = new MarkupTagElement();
				tagvVoiceParamCLE.FullName = "cle";
				MarkupElement arg_369_0 = tagvVoiceParamCLE;
				b = voice.SynthesisParameters.Clearness;
				arg_369_0.Value = b.ToString();
				tagvVoiceParam.Elements.Add(tagvVoiceParamCLE);
				MarkupTagElement tagvVoiceParamGEN = new MarkupTagElement();
				tagvVoiceParamGEN.FullName = "gen";
				MarkupElement arg_3A9_0 = tagvVoiceParamGEN;
				b = voice.SynthesisParameters.GenderFactor;
				arg_3A9_0.Value = b.ToString();
				tagvVoiceParam.Elements.Add(tagvVoiceParamGEN);
				MarkupTagElement tagvVoiceParamOPE = new MarkupTagElement();
				tagvVoiceParamOPE.FullName = "ope";
				MarkupElement arg_3E9_0 = tagvVoiceParamOPE;
				b = voice.SynthesisParameters.Openness;
				arg_3E9_0.Value = b.ToString();
				tagvVoiceParam.Elements.Add(tagvVoiceParamOPE);
				tagvVoice.Elements.Add(tagvVoiceParam);
				tagvVoiceTable.Elements.Add(tagvVoice);
			}
			tagVSQ3.Elements.Add(tagvVoiceTable);
			MarkupTagElement tagMixer = new MarkupTagElement();
			tagMixer.FullName = "mixer";
			MarkupTagElement tagMasterUnit = new MarkupTagElement();
			tagMasterUnit.FullName = "masterUnit";
			MarkupTagElement tagOutDev = new MarkupTagElement();
			tagOutDev.FullName = "outDev";
			tagOutDev.Value = "0";
			tagMasterUnit.Elements.Add(tagOutDev);
			MarkupTagElement tagRetLevel = new MarkupTagElement();
			tagRetLevel.FullName = "retLevel";
			tagRetLevel.Value = "0";
			tagMasterUnit.Elements.Add(tagRetLevel);
			MarkupTagElement tagVolume = new MarkupTagElement();
			tagVolume.FullName = "vol";
			tagVolume.Value = "0";
			tagMasterUnit.Elements.Add(tagVolume);
			tagMixer.Elements.Add(tagMasterUnit);
			MarkupTagElement tagInGain;
			MarkupTagElement tagSendLevel;
			MarkupTagElement tagSendEnable;
			MarkupTagElement tagMute;
			MarkupTagElement tagSolo;
			MarkupTagElement tagPan;
			MarkupTagElement tagVol;
			foreach (SynthesizedAudioTrack track in au.Tracks)
			{
				MarkupTagElement tagVSUnit = new MarkupTagElement();
				tagVSUnit.FullName = "vsUnit";
				MarkupTagElement tagVSTrackNo = new MarkupTagElement();
				tagVSTrackNo.FullName = "vsTrackNo";
				MarkupElement arg_57B_0 = tagVSTrackNo;
				int num = au.Tracks.IndexOf(track);
				arg_57B_0.Value = num.ToString();
				tagVSUnit.Elements.Add(tagVSTrackNo);
				tagInGain = new MarkupTagElement();
				tagInGain.FullName = "inGain";
				tagInGain.Value = "0";
				tagVSUnit.Elements.Add(tagInGain);
				tagSendLevel = new MarkupTagElement();
				tagSendLevel.FullName = "sendLevel";
				tagSendLevel.Value = "0";
				tagVSUnit.Elements.Add(tagSendLevel);
				tagSendEnable = new MarkupTagElement();
				tagSendEnable.FullName = "sendEnable";
				tagSendEnable.Value = "0";
				tagVSUnit.Elements.Add(tagSendEnable);
				tagMute = new MarkupTagElement();
				tagMute.FullName = "mute";
				tagMute.Value = (track.IsMuted ? "1" : "0");
				tagVSUnit.Elements.Add(tagMute);
				tagSolo = new MarkupTagElement();
				tagSolo.FullName = "solo";
				tagSolo.Value = (track.IsSolo ? "1" : "0");
				tagVSUnit.Elements.Add(tagSolo);
				tagPan = new MarkupTagElement();
				tagPan.FullName = "pan";
				MarkupElement arg_6C6_0 = tagPan;
				byte b = track.Panpot;
				arg_6C6_0.Value = b.ToString();
				tagVSUnit.Elements.Add(tagPan);
				tagVol = new MarkupTagElement();
				tagVol.FullName = "vol";
				MarkupElement arg_701_0 = tagVol;
				b = track.Volume;
				arg_701_0.Value = b.ToString();
				tagVSUnit.Elements.Add(tagVol);
				tagMixer.Elements.Add(tagVSUnit);
			}
			MarkupTagElement tagSEUnit = new MarkupTagElement();
			tagSEUnit.FullName = "seUnit";
			tagInGain = new MarkupTagElement();
			tagInGain.FullName = "inGain";
			tagInGain.Value = "0";
			tagSEUnit.Elements.Add(tagInGain);
			tagSendLevel = new MarkupTagElement();
			tagSendLevel.FullName = "sendLevel";
			tagSendLevel.Value = "0";
			tagSEUnit.Elements.Add(tagSendLevel);
			tagSendEnable = new MarkupTagElement();
			tagSendEnable.FullName = "sendEnable";
			tagSendEnable.Value = "0";
			tagSEUnit.Elements.Add(tagSendEnable);
			tagMute = new MarkupTagElement();
			tagMute.FullName = "mute";
			tagMute.Value = "0";
			tagSEUnit.Elements.Add(tagMute);
			tagSolo = new MarkupTagElement();
			tagSolo.FullName = "solo";
			tagSolo.Value = "0";
			tagSEUnit.Elements.Add(tagSolo);
			tagPan = new MarkupTagElement();
			tagPan.FullName = "pan";
			tagPan.Value = "64";
			tagSEUnit.Elements.Add(tagPan);
			tagVol = new MarkupTagElement();
			tagVol.FullName = "vol";
			tagVol.Value = "0";
			tagSEUnit.Elements.Add(tagVol);
			tagMixer.Elements.Add(tagSEUnit);
			MarkupTagElement tagKaraokeUnit = new MarkupTagElement();
			tagKaraokeUnit.FullName = "karaokeUnit";
			tagInGain = new MarkupTagElement();
			tagInGain.FullName = "inGain";
			tagInGain.Value = "0";
			tagKaraokeUnit.Elements.Add(tagInGain);
			tagMute = new MarkupTagElement();
			tagMute.FullName = "mute";
			tagMute.Value = "0";
			tagKaraokeUnit.Elements.Add(tagMute);
			tagSolo = new MarkupTagElement();
			tagSolo.FullName = "solo";
			tagSolo.Value = "0";
			tagKaraokeUnit.Elements.Add(tagSolo);
			tagVol = new MarkupTagElement();
			tagVol.FullName = "vol";
			tagVol.Value = "0";
			tagKaraokeUnit.Elements.Add(tagVol);
			tagMixer.Elements.Add(tagKaraokeUnit);
			tagVSQ3.Elements.Add(tagMixer);
			MarkupTagElement tagMasterTrack = new MarkupTagElement();
			tagMasterTrack.FullName = "masterTrack";
			MarkupTagElement tagSeqName = new MarkupTagElement();
			tagSeqName.FullName = "seqName";
			MarkupStringElement tagSeqNameCDATA = new MarkupStringElement();
			tagSeqNameCDATA.Name = "CDATA";
			tagSeqNameCDATA.Value = "Voice1";
			tagSeqName.Elements.Add(tagSeqNameCDATA);
			tagMasterTrack.Elements.Add(tagSeqName);
			MarkupTagElement tagComment = new MarkupTagElement();
			tagComment.FullName = "comment";
			MarkupStringElement tagCommentCDATA = new MarkupStringElement();
			tagCommentCDATA.Name = "CDATA";
			tagCommentCDATA.Value = "Voice1";
			tagComment.Elements.Add(tagCommentCDATA);
			tagMasterTrack.Elements.Add(tagComment);
			MarkupTagElement tagResolution = new MarkupTagElement();
			tagResolution.FullName = "resolution";
			tagResolution.Value = "480";
			tagMasterTrack.Elements.Add(tagResolution);
			MarkupTagElement tagPreMeasure = new MarkupTagElement();
			tagPreMeasure.FullName = "preMeasure";
			tagPreMeasure.Value = "480";
			tagMasterTrack.Elements.Add(tagPreMeasure);
			MarkupTagElement tagTimeSig = new MarkupTagElement();
			tagTimeSig.FullName = "timeSig";
			MarkupTagElement tagTimeSigPosMes = new MarkupTagElement();
			tagTimeSigPosMes.FullName = "posMes";
			tagTimeSigPosMes.Value = "0";
			tagTimeSig.Elements.Add(tagTimeSigPosMes);
			MarkupTagElement tagTimeSigNume = new MarkupTagElement();
			tagTimeSigNume.FullName = "nume";
			tagTimeSigNume.Value = "4";
			tagTimeSig.Elements.Add(tagTimeSigNume);
			MarkupTagElement tagTimeSigDenomi = new MarkupTagElement();
			tagTimeSigDenomi.FullName = "denomi";
			tagTimeSigDenomi.Value = "4";
			tagTimeSig.Elements.Add(tagTimeSigDenomi);
			tagMasterTrack.Elements.Add(tagTimeSig);
			MarkupTagElement tagTempo = new MarkupTagElement();
			tagTempo.FullName = "tempo";
			MarkupTagElement tagTempoPosTick = new MarkupTagElement();
			tagTempoPosTick.FullName = "posTick";
			tagTempoPosTick.Value = "0";
			tagTempo.Elements.Add(tagTempoPosTick);
			MarkupTagElement tagTempoBPM = new MarkupTagElement();
			tagTempoBPM.FullName = "bpm";
			tagTempoBPM.Value = "20500";
			tagTempo.Elements.Add(tagTempoBPM);
			tagTempo.Elements.Add(tagTempoPosTick);
			tagMasterTrack.Elements.Add(tagTempo);
			tagVSQ3.Elements.Add(tagMasterTrack);
			foreach (SynthesizedAudioTrack track in au.Tracks)
			{
				MarkupTagElement tagVSTrack = new MarkupTagElement();
				tagVSTrack.FullName = "vsTrack";
				MarkupTagElement tagVSTrackNo = new MarkupTagElement();
				tagVSTrackNo.FullName = "vsTrackNo";
				MarkupElement arg_C87_0 = tagVSTrackNo;
				int num = au.Tracks.IndexOf(track);
				arg_C87_0.Value = num.ToString();
				tagVSTrack.Elements.Add(tagVSTrackNo);
				MarkupTagElement tagTrackName = new MarkupTagElement();
				tagTrackName.FullName = "trackName";
				MarkupStringElement tagTrackNameCDATA = new MarkupStringElement();
				tagTrackNameCDATA.Name = "CDATA";
				tagTrackNameCDATA.Value = track.Name;
				tagTrackName.Elements.Add(tagTrackNameCDATA);
				tagVSTrack.Elements.Add(tagTrackName);
				tagComment = new MarkupTagElement();
				tagComment.FullName = "comment";
				tagCommentCDATA = new MarkupStringElement();
				tagCommentCDATA.Name = "CDATA";
				if (!string.IsNullOrEmpty(track.Comment))
				{
					tagCommentCDATA.Value = track.Comment;
				}
				else
				{
					tagCommentCDATA.Value = track.Name;
				}
				tagComment.Elements.Add(tagCommentCDATA);
				tagVSTrack.Elements.Add(tagComment);
				MarkupTagElement tagMusicalPart = new MarkupTagElement();
				tagMusicalPart.FullName = "musicalPart";
				foreach (SynthesizedAudioCommand cmd in track.Commands)
				{
					if (cmd is SynthesizedAudioCommandNote)
					{
						SynthesizedAudioCommandNote note = cmd as SynthesizedAudioCommandNote;
						MarkupTagElement tagNote = new MarkupTagElement();
						tagNote.FullName = "note";
						MarkupTagElement tagNotePosTick = new MarkupTagElement();
						tagNotePosTick.FullName = "posTick";
						MarkupElement arg_E00_0 = tagNotePosTick;
						num = note.Position;
						arg_E00_0.Value = num.ToString();
						tagNote.Elements.Add(tagNotePosTick);
						MarkupTagElement tagNoteDurTick = new MarkupTagElement();
						tagNoteDurTick.FullName = "durTick";
						MarkupElement arg_E3D_0 = tagNoteDurTick;
						num = (int)note.Length;
						arg_E3D_0.Value = num.ToString();
						tagNote.Elements.Add(tagNoteDurTick);
						MarkupTagElement tagNoteNum = new MarkupTagElement();
						tagNoteNum.FullName = "noteNum";
						MarkupElement arg_E7A_0 = tagNoteNum;
						arg_E7A_0.Value = note.Frequency.ToString();
						tagNote.Elements.Add(tagNoteNum);
						MarkupTagElement tagVelocity = new MarkupTagElement();
						tagVelocity.FullName = "velocity";
						MarkupElement arg_EB7_0 = tagVelocity;
						arg_EB7_0.Value = note.Frequency.ToString();
						tagNote.Elements.Add(tagVelocity);
						MarkupTagElement tagLyric = new MarkupTagElement();
						tagLyric.FullName = "lyric";
						MarkupStringElement tagLyricCDATA = new MarkupStringElement();
						tagLyricCDATA.Name = "CDATA";
						tagLyricCDATA.Value = note.Lyric.ToString();
						tagLyric.Elements.Add(tagLyricCDATA);
						tagNote.Elements.Add(tagLyric);
						MarkupTagElement tagPhoneme = new MarkupTagElement();
						tagPhoneme.FullName = "phnms";
						MarkupStringElement tagPhonemeCDATA = new MarkupStringElement();
						tagPhonemeCDATA.Name = "CDATA";
						tagPhonemeCDATA.Value = note.Phoneme.ToString();
						tagPhoneme.Elements.Add(tagPhonemeCDATA);
						tagNote.Elements.Add(tagPhoneme);
						MarkupTagElement tagNoteStyle = new MarkupTagElement();
						tagNoteStyle.FullName = "noteStyle";
						MarkupTagElement tagNoteStyleAccent = new MarkupTagElement();
						tagNoteStyleAccent.FullName = "attr";
						tagNoteStyleAccent.Attributes.Add("id", "accent");
						MarkupElement arg_FD7_0 = tagNoteStyleAccent;
						num = note.Accent;
						arg_FD7_0.Value = num.ToString();
						tagNoteStyle.Elements.Add(tagNoteStyleAccent);
						MarkupTagElement tagNoteStyleBendDepth = new MarkupTagElement();
						tagNoteStyleBendDepth.FullName = "attr";
						tagNoteStyleBendDepth.Attributes.Add("id", "bendDep");
						MarkupElement arg_1029_0 = tagNoteStyleBendDepth;
						num = note.PitchBendDepth;
						arg_1029_0.Value = num.ToString();
						tagNoteStyle.Elements.Add(tagNoteStyleBendDepth);
						MarkupTagElement tagNoteStyleBendLength = new MarkupTagElement();
						tagNoteStyleBendLength.FullName = "attr";
						tagNoteStyleBendLength.Attributes.Add("id", "bendLen");
						MarkupElement arg_107B_0 = tagNoteStyleBendLength;
						num = note.PitchBendLength;
						arg_107B_0.Value = num.ToString();
						tagNoteStyle.Elements.Add(tagNoteStyleBendLength);
						MarkupTagElement tagNoteStyleDecay = new MarkupTagElement();
						tagNoteStyleDecay.FullName = "attr";
						tagNoteStyleDecay.Attributes.Add("id", "decay");
						tagNoteStyleDecay.Value = "50";
						tagNoteStyle.Elements.Add(tagNoteStyleDecay);
						MarkupTagElement tagNoteStyleFallPort = new MarkupTagElement();
						tagNoteStyleFallPort.FullName = "attr";
						tagNoteStyleFallPort.Attributes.Add("id", "fallPort");
						tagNoteStyleFallPort.Value = (note.PortamentoFalling ? "1" : "0");
						tagNoteStyle.Elements.Add(tagNoteStyleFallPort);
						MarkupTagElement tagNoteStyleOpening = new MarkupTagElement();
						tagNoteStyleOpening.FullName = "attr";
						tagNoteStyleOpening.Attributes.Add("id", "opening");
						tagNoteStyleOpening.Value = "127";
						tagNoteStyle.Elements.Add(tagNoteStyleOpening);
						MarkupTagElement tagNoteStyleRisePort = new MarkupTagElement();
						tagNoteStyleRisePort.FullName = "attr";
						tagNoteStyleRisePort.Attributes.Add("id", "risePort");
						tagNoteStyleRisePort.Value = (note.PortamentoRising ? "1" : "0");
						tagNoteStyle.Elements.Add(tagNoteStyleRisePort);
						MarkupTagElement tagNoteStyleVibLen = new MarkupTagElement();
						tagNoteStyleVibLen.FullName = "attr";
						tagNoteStyleVibLen.Attributes.Add("id", "vibLen");
						tagNoteStyleVibLen.Value = "0";
						tagNoteStyle.Elements.Add(tagNoteStyleVibLen);
						MarkupTagElement tagNoteStyleVibType = new MarkupTagElement();
						tagNoteStyleVibType.FullName = "attr";
						tagNoteStyleVibType.Attributes.Add("id", "vibType");
						tagNoteStyleVibType.Value = "0";
						tagNoteStyle.Elements.Add(tagNoteStyleVibType);
						if (note.VibratoType != SynthesizedAudioVibratoType.None)
						{
							MarkupTagElement tagSeqAttr = new MarkupTagElement();
							tagSeqAttr.FullName = "seqAttr";
							tagSeqAttr.Attributes.Add("id", "vibDep");
							MarkupTagElement tagSeqAttrElem = new MarkupTagElement();
							tagSeqAttr.FullName = "elem";
							MarkupTagElement tagSeqAttrElemPosNrm = new MarkupTagElement();
							tagSeqAttrElemPosNrm.FullName = "posNrm";
							tagSeqAttrElemPosNrm.Value = "27670";
							tagSeqAttrElem.Elements.Add(tagSeqAttrElemPosNrm);
							MarkupTagElement tagSeqAttrElemElv = new MarkupTagElement();
							tagSeqAttrElemElv.FullName = "elv";
							tagSeqAttrElemElv.Value = "64";
							tagSeqAttrElem.Elements.Add(tagSeqAttrElemElv);
							tagSeqAttr.Elements.Add(tagSeqAttrElem);
							tagSeqAttr = new MarkupTagElement();
							tagSeqAttr.FullName = "seqAttr";
							tagSeqAttr.Attributes.Add("id", "vibRate");
							tagSeqAttrElem = new MarkupTagElement();
							tagSeqAttr.FullName = "elem";
							tagSeqAttrElemPosNrm = new MarkupTagElement();
							tagSeqAttrElemPosNrm.FullName = "posNrm";
							tagSeqAttrElemPosNrm.Value = "27670";
							tagSeqAttrElem.Elements.Add(tagSeqAttrElemPosNrm);
							tagSeqAttrElemElv = new MarkupTagElement();
							tagSeqAttrElemElv.FullName = "elv";
							tagSeqAttrElemElv.Value = "27670";
							tagSeqAttrElem.Elements.Add(tagSeqAttrElemElv);
							tagSeqAttr.Elements.Add(tagSeqAttrElem);
						}
						tagNote.Elements.Add(tagNoteStyle);
						tagMusicalPart.Elements.Add(tagNote);
					}
				}
				tagVSTrack.Elements.Add(tagMusicalPart);
				tagVSQ3.Elements.Add(tagVSTrack);
			}
			MarkupTagElement tagSETrack = new MarkupTagElement();
			tagSETrack.FullName = "seTrack";
			tagVSQ3.Elements.Add(tagSETrack);
			MarkupTagElement tagKaraokeTrack = new MarkupTagElement();
			tagKaraokeTrack.FullName = "karaokeTrack";
			tagVSQ3.Elements.Add(tagKaraokeTrack);
			MarkupTagElement tagAux = new MarkupTagElement();
			tagAux.FullName = "aux";
			MarkupTagElement tagAuxID = new MarkupTagElement();
			tagAuxID.FullName = "auxID";
			MarkupStringElement tagAuxIDCDATA = new MarkupStringElement();
			tagAuxIDCDATA.Name = "CDATA";
			tagAuxIDCDATA.Value = "AUX_VST_HOST_CHUNK_INFO";
			tagAuxID.Elements.Add(tagAuxIDCDATA);
			tagAux.Elements.Add(tagAuxID);
			MarkupTagElement tagAuxContent = new MarkupTagElement();
			tagAuxContent.FullName = "content";
			MarkupStringElement tagAuxContentCDATA = new MarkupStringElement();
			tagAuxContentCDATA.Name = "CDATA";
			tagAuxContentCDATA.Value = "VlNDSwAAAAADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=";
			tagAuxContent.Elements.Add(tagAuxContentCDATA);
			tagAux.Elements.Add(tagAuxContent);
			tagVSQ3.Elements.Add(tagAux);
			mom.Elements.Add(tagVSQ3);
			objectModels.Push(mom);
		}
	}
}
