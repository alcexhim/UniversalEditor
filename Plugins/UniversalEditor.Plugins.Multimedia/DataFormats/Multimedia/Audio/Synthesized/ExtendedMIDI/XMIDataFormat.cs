//
//  XMIDataFormat.cs - provides a DataFormat for manipulating synthesized audio files in Extended MIDI (XMI) format
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
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Chunked.RIFF;
using UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MIDI;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.ExtendedMIDI
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio files in Extended MIDI (XMI) format.
	/// </summary>
	public class XMIDataFormat : RIFFDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(this.GetType());
				_dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.Bootstrap);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel chunked = (objectModels.Pop() as ChunkedObjectModel);
			SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);

			if (chunked.Chunks.Count < 2)
				throw new InvalidDataFormatException("must contain TWO top-level RIFF group chunks, one 'FORM:XDIR' and one 'CAT :XMID'");

			RIFFGroupChunk grpFORM = (chunked.Chunks[0] as RIFFGroupChunk);
			if (!(grpFORM.TypeID == "FORM" && grpFORM.ID == "XDIR")) throw new InvalidDataFormatException("does not contain a top-level RIFF group chunk with type 'FORM' and ID 'XDIR'");

			if (grpFORM.Chunks.Count < 0)
				throw new InvalidDataFormatException("'FORM:XDIR' group chunk must contain an 'INFO' data chunk");

			RIFFDataChunk datINFO = (grpFORM.Chunks[0] as RIFFDataChunk);
			MemoryAccessor maINFO = new MemoryAccessor(datINFO.Data);

			short seqCount = maINFO.Reader.ReadInt16();

			RIFFGroupChunk grpCAT = (chunked.Chunks[1] as RIFFGroupChunk);
			if (!(grpCAT.TypeID == "CAT " && grpCAT.ID == "XMID")) throw new InvalidDataFormatException("does not contain a top-level RIFF group chunk with type 'CAT ' and ID 'XMID'");

			for (short seqIndex = 0; seqIndex < seqCount; seqIndex++)
			{
				RIFFGroupChunk grpSong = (grpCAT.Chunks[seqIndex] as RIFFGroupChunk);

				if (grpSong.Chunks.Count > 1)
				{
					SynthesizedAudioTrack track = new SynthesizedAudioTrack();

					RIFFDataChunk datEVNT = (grpSong.Chunks[1] as RIFFDataChunk);
					byte[] data = datEVNT.Data;

					MemoryAccessor maData = new MemoryAccessor(data);
					System.Collections.Generic.Dictionary<int, SynthesizedAudioCommandNote> notesForNoteNumber = new System.Collections.Generic.Dictionary<int, SynthesizedAudioCommandNote>();
					int delay = 0;

					while (!maData.Reader.EndOfStream)
					{
						byte code = maData.Reader.ReadByte();
						MIDIEventType command = MIDIEventType.None;
						if ((code & 0x80) == 0x80)
						{
							// has high-bit set so this is a midi event
							// byte realcode = (byte)(code & ~0x80);
							command = (MIDIEventType)code;

							byte channel = (byte)((byte)command & (byte)MIDIEventType.MIDIChannelMask);
							MIDIEventType commandWithoutChannel = (MIDIEventType)((byte)command >> 4);

							if (command == MIDIEventType.Meta)
							{
								MIDIMetaEventType metaEventType = (MIDIMetaEventType)maData.Reader.ReadByte();
								byte length = maData.Reader.ReadByte();
								switch (metaEventType)
								{
									case MIDIMetaEventType.Text:
									{
										string text = maData.Reader.ReadFixedLengthString(length).TrimNull();
										track.Commands.Add(new SynthesizedAudioCommandText(text));
										break;
									}
									case MIDIMetaEventType.CopyrightNotice:
									{
										break;
									}
									case MIDIMetaEventType.SequenceName:
									{
										string text = maData.Reader.ReadFixedLengthString(length).TrimNull();
										track.Name = text;
										break;
									}
									case MIDIMetaEventType.ProgramName:
									{
										string text = maData.Reader.ReadFixedLengthString(length).TrimNull();
										break;
									}
									case MIDIMetaEventType.DeviceName:
									{
										string text = maData.Reader.ReadFixedLengthString(length).TrimNull();
										break;
									}
									case MIDIMetaEventType.EndOfTrack:
									{
										audio.Tracks.Add(track);
										break;
									}
									case MIDIMetaEventType.TimeSignature:
									{
										byte numerator = maData.Reader.ReadByte();
										byte denominatorPower = maData.Reader.ReadByte();
										byte denominator = (byte)Math.Pow(2.0, denominatorPower);
										byte ticksPerMetronomeClick = maData.Reader.ReadByte();
										byte numberOf32ndNotesPerQuarterNote = maData.Reader.ReadByte();
										track.Commands.Add(new SynthesizedAudioCommandTimeSignature(numerator, denominator, ticksPerMetronomeClick, numberOf32ndNotesPerQuarterNote));
										break;
									}
									case MIDIMetaEventType.SetTempo:
									{
										int tempo = (int)maData.Reader.ReadInt16();
										int tempo2 = (int)maData.Reader.ReadByte();
										int tempo3 = tempo + tempo2;
										track.Commands.Add(new SynthesizedAudioCommandTempo((double)tempo3));
										break;
									}
									case MIDIMetaEventType.KeySignature:
									{
										byte sf = maData.Reader.ReadByte(); // -7 = 7 flats, -1 = 1 flat, 0 = none (key of C), 1 =
										bool minorKey = maData.Reader.ReadBoolean();
										break;
									}
									default:
									{
										Console.WriteLine("ue: MIDI: warning: meta event type {0} ({1}) [{2} bytes] unhandled", metaEventType, (byte)metaEventType, length);
										Accessor.Seek(length, SeekOrigin.Current);
										break;
									}
								}
							}
							else
							{
								switch (commandWithoutChannel)
								{
									case MIDIEventType.ProgramChange:
									{
										byte programNumber = maData.Reader.ReadByte();
										break;
									}
									case MIDIEventType.NoteOn:
									{
										byte noteNumber = maData.Reader.ReadByte();
										byte velocity = maData.Reader.ReadByte();
										int length = maData.Reader.Read7BitEncodedInt();

										if (!notesForNoteNumber.ContainsKey(noteNumber))
										{
											notesForNoteNumber[noteNumber] = new SynthesizedAudioCommandNote();
										}
										SynthesizedAudioCommandNote note = notesForNoteNumber[noteNumber];
										note.Frequency = noteNumber;
										note.Length = length;
										note.Position = delay;
										track.Commands.Add(note);
										break;
									}
									case MIDIEventType.NoteOff:
									{
										byte noteNumber = maData.Reader.ReadByte();
										byte velocity = maData.Reader.ReadByte();

										if (notesForNoteNumber.ContainsKey(noteNumber))
										{
											notesForNoteNumber[noteNumber].Length = 0;
											notesForNoteNumber.Remove(noteNumber);
										}
										break;
									}
									case MIDIEventType.ControlChange:
									{
										byte controllerNumber = maData.Reader.ReadByte();
										byte value = maData.Reader.ReadByte();
										break;
									}
									case MIDIEventType.PolyphonicKeyPressureAftertouch:
									{
										byte noteNumber = maData.Reader.ReadByte();
										byte pressure = maData.Reader.ReadByte();
										break;
									}
									case MIDIEventType.ChannelPressureAftertouch:
									{
										byte pressure = maData.Reader.ReadByte();
										break;
									}
									case MIDIEventType.PitchWheelChange:
									{
										short value = maData.Reader.ReadInt16();
										break;
									}
								}
							}
						}
						else
						{
							// this is a delay
							maData.Reader.Seek(-1, SeekOrigin.Current);
							int r = maData.Reader.ReadVariableLengthInt32();
							delay += r;
						}
					}
					audio.Tracks.Add(track);
				}
				else
				{

				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);
			ChunkedObjectModel chunked = new ChunkedObjectModel();



			objectModels.Push(chunked);
		}
	}
}
