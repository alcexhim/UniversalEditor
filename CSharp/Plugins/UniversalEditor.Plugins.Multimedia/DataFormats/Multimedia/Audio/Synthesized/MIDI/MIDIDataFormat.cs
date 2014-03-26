using System;
using System.Collections.Generic;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MIDI
{
	public class MIDIDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Music Instrument Digital Interface sequence", new byte?[][] { new byte?[] { (byte)'M', (byte)'T', (byte)'h', (byte)'d' } }, new string[] { "*.mid", "*.midi", "*.rmi" });
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;
			SynthesizedAudioObjectModel syn = objectModel as SynthesizedAudioObjectModel;
			string MThd = br.ReadFixedLengthString(4);
			int headerSize = br.ReadInt32();
			short fileFormat = br.ReadInt16();
			short trackCount = br.ReadInt16();
			short ticksPerQuarterNote = br.ReadInt16();
			for (short i = 0; i < trackCount; i += 1)
			{
				string MTrk = br.ReadFixedLengthString(4);
				int trackLength = br.ReadInt32();
				SynthesizedAudioTrack track = new SynthesizedAudioTrack();
				long position = br.Accessor.Position;
				while (br.Accessor.Position - position < (long)trackLength)
                {
                    try
                    {
                        int deltaTime = br.ReadVariableLengthInt32();
                        byte command = br.ReadByte();
                        if (command == 255)
                        {
                            byte metaEventType = br.ReadByte();
                            byte b = metaEventType;
                            if (b <= 47)
                            {
                                switch (b)
                                {
                                    case 1:
                                    {
                                        byte length = br.ReadByte();
                                        string text = br.ReadFixedLengthString(length);
                                        track.Commands.Add(new SynthesizedAudioCommandText(text));
                                        break;
                                    }
                                    case 2:
                                    {
                                        break;
                                    }
                                    case 3:
                                    {
                                        byte length = br.ReadByte();
                                        string text = br.ReadFixedLengthString(length);
                                        track.Name = text;
                                        break;
                                    }
                                    default:
                                    {
                                        if (b == 47)
                                        {
                                            byte endOfTrackMarker = br.ReadByte();
                                            syn.Tracks.Add(track);
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (b != 81)
                                {
                                    if (b == 88)
                                    {
                                        byte zero4 = br.ReadByte();
                                        byte numerator = br.ReadByte();
                                        byte denominator = (byte)Math.Pow(2.0, (double)br.ReadByte());
                                        byte ticksPerMetronomeClick = br.ReadByte();
                                        byte numberOf32ndNotesPerQuarterNote = br.ReadByte();
                                        track.Commands.Add(new SynthesizedAudioCommandTimeSignature(numerator, denominator, ticksPerMetronomeClick, numberOf32ndNotesPerQuarterNote));
                                    }
                                }
                                else
                                {
                                    byte zero5 = br.ReadByte();
                                    int tempo = (int)br.ReadInt16();
                                    int tempo2 = (int)br.ReadByte();
                                    int tempo3 = tempo + tempo2;
                                    track.Commands.Add(new SynthesizedAudioCommandTempo((double)tempo3));
                                }
                            }
                        }
                    }
                    catch (System.IO.EndOfStreamException ex)
                    {
                        continue;
                    }
                }
			}
		}
        
        /*
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			SynthesizedAudioObjectModel au = objectModels.Pop() as SynthesizedAudioObjectModel;
			SynthesizedAudioTrack trkControl = au.Tracks["Control"];
			if (trkControl != null)
			{
				bool inSettingsBlock = false;
				foreach (SynthesizedAudioCommand cmd in trkControl.Commands)
				{
					if (!(cmd is SynthesizedAudioCommandTempo))
					{
						if (!(cmd is SynthesizedAudioCommandTimeSignature))
						{
							if (cmd is SynthesizedAudioCommandText)
							{
								SynthesizedAudioCommandText text = cmd as SynthesizedAudioCommandText;
								if (text.Text == "Settings")
								{
									inSettingsBlock = true;
								}
								else
								{
									if (inSettingsBlock && text.Text.StartsWith("@rem project="))
									{
										au.Information.SongTitle = text.Text.Substring(13);
									}
									else
									{
										if (inSettingsBlock && text.Text.StartsWith("@set tempo="))
										{
											double tempo = double.Parse(text.Text.Substring(11));
										}
									}
								}
							}
						}
					}
				}
			}
		}
        */

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
