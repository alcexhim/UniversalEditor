//
//  DDIDataFormat.cs - provides a DataFormat for manipulating synthesized audio voicebank index in Vocaloid DDI format
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

using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
using UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankIndex;

namespace UniversalEditor.Plugins.Vocaloid.DataFormats.Multimedia.Audio.VoicebankIndex.Vocaloid
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio voicebank index in Vocaloid DDI format.
	/// </summary>
	public class VocaloidIndexDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(VoicebankIndexObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private byte[] ReadChunk(Reader br, out string chunkName, out uint chunkSize)
		{
			chunkName = br.ReadFixedLengthString(4);
			chunkSize = br.ReadUInt32();

			uint dataSize = chunkSize;

			byte[] chunkData = br.ReadBytes(dataSize);
			uint unknown = br.ReadUInt32();

			return chunkData;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VoicebankIndexObjectModel dbse = (objectModel as VoicebankIndexObjectModel);
			if (dbse == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;


			// Bruno	Clara
			uint unknown1 = reader.ReadUInt32();        // 0
			uint unknown2 = reader.ReadUInt32();

			while (!reader.EndOfStream)
			{
				string chunkName = null;
				uint chunkSize = 0;
				byte[] chunk = ReadChunk(reader, out chunkName, out chunkSize);

				Reader r = new Reader(new MemoryAccessor(chunk));
				switch (chunkName)
				{
					case "ARR ":
					{
						uint unknownA1 = r.ReadUInt32();
						uint unknownA2 = r.ReadUInt32();

						while (!r.EndOfStream)
						{
							string ck1name = null;
							uint ck1size = 0;
							byte[] ck = ReadChunk(r, out ck1name, out ck1size);
							Reader r2 = new Reader(new MemoryAccessor(ck));

							switch (ck1name)
							{
								case "PHDC":
								{
									uint unknownB1 = r2.ReadUInt32();
									uint count = r2.ReadUInt32();
									for (uint i = 0; i < count; i++)
									{
										string phnm = r2.ReadFixedLengthString(30);
										phnm = phnm.TrimNull();

										byte phnmData = r2.ReadByte();

										Console.WriteLine("found phoneme '{0}'", phnm);

										Phoneme p = new Phoneme();
										p.Title = phnm;
										dbse.Phonemes.Add(p);
									}

									string phg2Name = null;
									uint phg2Size = 0;
									byte[] phg2 = ReadChunk(r2, out phg2Name, out phg2Size);
									Reader r3 = new Reader(new MemoryAccessor(phg2));

									uint groupCount = r3.ReadUInt32();
									for (uint i = 0; i < groupCount; i++)
									{
										uint groupNameLength = r3.ReadUInt32();
										string groupName = r3.ReadFixedLengthString(groupNameLength);
										uint groupPhonemeCount = r3.ReadUInt32();

										PhonemeGroup pg = new PhonemeGroup();
										pg.Title = groupName;

										for (uint j = 0; j < groupPhonemeCount; j++)
										{
											uint unknown8 = r3.ReadUInt32();    // unknown (possibly index into PHDC table?)
											uint groupPhonemeNameLength = r3.ReadUInt32();
											string groupPhonemeName = r3.ReadFixedLengthString(groupPhonemeNameLength);

											Phoneme p = new Phoneme();
											p.Title = groupPhonemeName;
											pg.Phonemes.Add(p);
										}
										uint unknown9 = r3.ReadUInt32();

										dbse.Groups.Add(pg);
									}
									break;
								}
							}
						}
						break;
					}
					case "ART ":
					{
						break;
					}
					case "DBV ":
					{
						break;
					}
					default:
					{
						Console.WriteLine("skipping unknown chunk type '{0}'", chunkName);
						break;
					}
				}
			}

			/*
			string chunkName = reader.ReadFixedLengthString(4);		// DBSe, 'ARR '
			uint unknown3 = reader.ReadUInt32();		// 5202 in miku
			uint unknown4 = reader.ReadUInt32();		// 1
			uint unknown5 = reader.ReadUInt32();
			uint unknown6 = reader.ReadUInt32();		// 3, subchunk count?

			long offsetToPHDC = reader.Accessor.Position;
			string chunkNamePHDC = reader.ReadFixedLengthString(4);
			uint chunkSizePHDC = reader.ReadUInt32();	// total size of PHDC chunk (3927 in Bruno)

			uint offsetToGuid = (uint)(chunkSizePHDC + offsetToPHDC);


			uint unknown7 = reader.ReadUInt32();		// 4

			uint phonemeCount = reader.ReadUInt32();	// 32
			for (uint i = 0; i < phonemeCount; i++)
			{
				string phonemeName = reader.ReadFixedLengthString(30).TrimNull();
				byte phonemeData = reader.ReadByte();

				Phoneme p = new Phoneme();
				p.Title = phonemeName;
				dbse.Phonemes.Add(p);
			}

			string chunkNamePHG2 = reader.ReadFixedLengthString(4);
			uint chunkSizePHG2 = reader.ReadUInt32();	// total size of PHG2 chunk (515 in Bruno)

			uint groupCount = reader.ReadUInt32();		// 9
			for (uint i = 0; i < groupCount; i++)
			{
				uint groupNameLength = reader.ReadUInt32();
				string groupName = reader.ReadFixedLengthString(groupNameLength);
				uint groupPhonemeCount = reader.ReadUInt32();

				PhonemeGroup pg = new PhonemeGroup();
				pg.Title = groupName;

				for (uint j = 0; j < groupPhonemeCount; j++)
				{
					uint unknown8 = reader.ReadUInt32();	// unknown (possibly index into PHDC table?)
					uint groupPhonemeNameLength = reader.ReadUInt32();
					string groupPhonemeName = reader.ReadFixedLengthString(groupPhonemeNameLength);

					Phoneme p = new Phoneme();
					p.Title = groupPhonemeName;
					pg.Phonemes.Add(p);
				}
				uint unknown9 = reader.ReadUInt32();

				dbse.Groups.Add(pg);
			}

			// I do not know how to read the rest of the PHDC chunk, but I do know how to skip it...
			base.Accessor.Seek(offsetToGuid, SeekOrigin.Begin);

			dbse.Title = reader.ReadFixedLengthString(260).TrimNull();

			uint unknown10 = reader.ReadUInt32();
			uint unknown11 = reader.ReadUInt32();
			uint unknown12 = reader.ReadUInt32();

			string chunkNameTDB = reader.ReadFixedLengthString(4);
			uint unknown13 = reader.ReadUInt32();	// 0
			uint unknown14 = reader.ReadUInt32();	// 1
			uint unknown15 = reader.ReadUInt32();	// 0

			uint chunkCountTMM = reader.ReadUInt32();
			uint unknown16 = reader.ReadUInt32();
			uint unknown17 = reader.ReadUInt32();
			for (uint i = 0; i < chunkCountTMM; i++)
			{
				string chunkNameTMM = reader.ReadFixedLengthString(4);
				uint unknown18 = reader.ReadUInt32();	// 0
				uint unknown19 = reader.ReadUInt32();	// 1
				uint unknown20 = reader.ReadUInt32();	// 0
				uint phonemeIndex = reader.ReadUInt32();	// is this right?
				uint chunkCountARR = reader.ReadUInt32();
				uint unknown21 = reader.ReadUInt32();
				uint unknown22 = reader.ReadUInt32();

				for (uint j = 0; j < chunkCountARR; j++)
				{
					DDIParameter param = new DDIParameter();
					string chunkNameARR = reader.ReadFixedLengthString(4);
					uint unknown23 = reader.ReadUInt32();	// 0
					uint unknown24 = reader.ReadUInt32();	// 1
					uint unknown25 = reader.ReadUInt32();	// 0
					uint unknown26 = reader.ReadUInt32();
					uint paramNameLength = reader.ReadUInt32();
					param.ParameterName = reader.ReadFixedLengthString(paramNameLength);

					int phonemeNameLength = reader.ReadInt32();
					string phonemeName = null;
					if (phonemeNameLength > -1)
					{
						phonemeName = reader.ReadFixedLengthString(phonemeNameLength);
					}
					param.PhonemeName = phonemeName;

					uint unknown28 = reader.ReadUInt32();
				}

				uint unknown29 = reader.ReadUInt32();
			}
			*/
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			VoicebankIndexObjectModel dbse = (objectModel as VoicebankIndexObjectModel);
			if (dbse == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteUInt32(0);
			writer.WriteUInt32(0);
			writer.WriteFixedLengthString("DBSe");

			writer.Flush();
		}
	}
}
