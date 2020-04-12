//
//  MIDIDataFormat.cs - provides a DataFormat for manipulating synthesized audio files in MIDI format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MIDI
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio files in MIDI format.
	/// </summary>
	public class MIDIDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;
			SynthesizedAudioObjectModel syn = objectModel as SynthesizedAudioObjectModel;
			string MThd = br.ReadFixedLengthString(4);
			if (MThd != "MThd") throw new InvalidDataFormatException("File does not begin with \"MThd\"");

			int headerSize = br.ReadInt32();
			short fileFormat = br.ReadInt16();
			short trackCount = br.ReadInt16();
			short ticksPerQuarterNote = br.ReadInt16();
			for (short i = 0; i < trackCount; i += 1)
			{
				string MTrk = br.ReadFixedLengthString(4);
				if (MTrk != "MTrk") throw new InvalidDataFormatException("Could not read track " + (trackCount + 1).ToString() + " - does not begin with \"MTrk\"");

				int trackLength = br.ReadInt32();
				SynthesizedAudioTrack track = new SynthesizedAudioTrack();
				long position = br.Accessor.Position;
				while (br.Accessor.Position - position < (long)trackLength)
				{
					int deltaTime = br.ReadVariableLengthInt32();
					MIDIEventType command = (MIDIEventType)br.ReadByte();
					if (command == MIDIEventType.Meta)
					{
						MIDIMetaEventType metaEventType = (MIDIMetaEventType)br.ReadByte();
						byte length = br.ReadByte();
						switch (metaEventType)
						{
							case MIDIMetaEventType.Text:
							{
								string text = br.ReadFixedLengthString(length);
								track.Commands.Add(new SynthesizedAudioCommandText(text));
								break;
							}
							case MIDIMetaEventType.CopyrightNotice:
							{
								break;
							}
							case MIDIMetaEventType.SequenceName:
							{
								string text = br.ReadFixedLengthString(length);
								track.Name = text;
								break;
							}
							case MIDIMetaEventType.EndOfTrack:
							{
								syn.Tracks.Add(track);
								break;
							}
							case MIDIMetaEventType.TimeSignature:
							{
								byte zero4 = br.ReadByte();
								byte numerator = br.ReadByte();
								byte denominator = (byte)Math.Pow(2.0, (double)br.ReadByte());
								byte ticksPerMetronomeClick = br.ReadByte();
								byte numberOf32ndNotesPerQuarterNote = br.ReadByte();
								track.Commands.Add(new SynthesizedAudioCommandTimeSignature(numerator, denominator, ticksPerMetronomeClick, numberOf32ndNotesPerQuarterNote));
								break;
							}
							case MIDIMetaEventType.SetTempo:
							{
								byte zero5 = br.ReadByte();
								int tempo = (int)br.ReadInt16();
								int tempo2 = (int)br.ReadByte();
								int tempo3 = tempo + tempo2;
								track.Commands.Add(new SynthesizedAudioCommandTempo((double)tempo3));
								break;
							}
							default:
							{
								Console.WriteLine("ue: MIDI: warning: meta event type {0} ({1}) [{2} bytes] unhandled", metaEventType, (byte)metaEventType, length);
								break;
							}
						}
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
			Writer bw = Accessor.Writer;
			bw.Endianness = Endianness.BigEndian;

			SynthesizedAudioObjectModel syn = objectModel as SynthesizedAudioObjectModel;
			bw.WriteFixedLengthString("MThd");

			int headerSize = 6;
			bw.WriteInt32(headerSize);

			MIDIFileFormatType fileFormat = MIDIFileFormatType.SimultaneousMultitrack;
			bw.WriteInt16((short)fileFormat);

			bw.WriteInt16((short)syn.Tracks.Count);

			short ticksPerQuarterNote = 0;      // if MSB is 1, bits 0-7 are ticks / frame and bits 8-14 are frames / second
			bw.WriteInt16(ticksPerQuarterNote);

			for (short i = 0; i < syn.Tracks.Count; i += 1)
			{
				bw.WriteFixedLengthString("MTrk");

				int trackLength = 0;
				bw.WriteInt32(trackLength);

				SynthesizedAudioTrack track = syn.Tracks[i];

				// track name
				WriteMIDIEvent(bw, 0, MIDIMetaEventType.SequenceName, track.Name);

				for (int j = 0; j < syn.Tracks[i].Commands.Count; j++)
				{
					int deltaTime = 0;

					if (syn.Tracks[i].Commands[j] is SynthesizedAudioCommandText)
					{
						SynthesizedAudioCommandText cmd = (syn.Tracks[i].Commands[j] as SynthesizedAudioCommandText);
						WriteMIDIEvent(bw, deltaTime, MIDIMetaEventType.Text, cmd.Text);
					}
					else if (syn.Tracks[i].Commands[j] is SynthesizedAudioCommandTimeSignature)
					{
						SynthesizedAudioCommandTimeSignature cmd = (syn.Tracks[i].Commands[j] as SynthesizedAudioCommandTimeSignature);

						byte numerator = 4;
						byte denom = 4;
						byte denominator = (byte)(Math.Sqrt(denom) / Math.Sqrt(2));
						byte ticksPerMetronomeClick = 0;
						byte numberOf32ndNotesPerQuarterNote = 0;
						WriteMIDIEvent(bw, deltaTime, MIDIMetaEventType.TimeSignature, new byte[] { numerator, denominator, ticksPerMetronomeClick, numberOf32ndNotesPerQuarterNote });
					}
					else if (syn.Tracks[i].Commands[j] is SynthesizedAudioCommandTempo)
					{
						SynthesizedAudioCommandTempo cmd = (syn.Tracks[i].Commands[j] as SynthesizedAudioCommandTempo);
						MemoryAccessor ma = new MemoryAccessor();
						ma.Writer.WriteInt24((int)cmd.Tempo);
						WriteMIDIEvent(bw, deltaTime, MIDIMetaEventType.SetTempo, ma.ToArray());
					}
				}

				WriteMIDIEvent(bw, 0, MIDIMetaEventType.EndOfTrack, new byte[0]);
			}
		}

		private void WriteMIDIEvent(Writer bw, int deltaTime, MIDIMetaEventType midiEventType, string data)
		{
			WriteMIDIEvent(bw, deltaTime, midiEventType, System.Text.Encoding.UTF8.GetBytes(data));
		}
		private void WriteMIDIEvent(Writer bw, int deltaTime, MIDIMetaEventType midiEventType, byte[] data)
		{
			bw.WriteVariableLengthInt32(deltaTime);
			bw.WriteByte((byte)MIDIEventType.Meta);
			bw.WriteByte((byte)midiEventType);
			bw.WriteVariableLengthInt32(data.Length);
			bw.WriteBytes(data);
		}

		private void WriteMIDIEvent(Writer bw, int deltaTime, MIDIEventType midiEventType, string data)
		{
			WriteMIDIEvent(bw, deltaTime, midiEventType, System.Text.Encoding.UTF8.GetBytes(data));
		}
		private void WriteMIDIEvent(Writer bw, int deltaTime, MIDIEventType midiEventType, byte[] data)
		{
			bw.WriteVariableLengthInt32(deltaTime);
			bw.WriteByte((byte)midiEventType);
			bw.WriteVariableLengthInt32(data.Length);
			bw.WriteBytes(data);
		}
	}
}
