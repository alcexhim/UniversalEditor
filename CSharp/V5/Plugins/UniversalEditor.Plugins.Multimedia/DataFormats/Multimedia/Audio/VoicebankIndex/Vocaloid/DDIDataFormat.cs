using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
using UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankIndex;

namespace UniversalEditor.DataFormats.Multimedia.Audio.VoicebankIndex.Vocaloid
{
	public class VocaloidIndexDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(VoicebankIndexObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VoicebankIndexObjectModel dbse = (objectModel as VoicebankIndexObjectModel);
			if (dbse == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
														// Bruno	Clara
			uint unknown1 = reader.ReadUInt32();		// 0
			uint unknown2 = reader.ReadUInt32();
			string chunkName = reader.ReadFixedLengthString(4);		// DBSe
			uint unknown3 = reader.ReadUInt32();
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
