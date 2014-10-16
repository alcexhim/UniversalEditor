using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Voicebank.Vocaloid
{
	public class VocaloidIndexDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader reader = base.Accessor.Reader;
														// Bruno	Clara
			uint unknown1 = reader.ReadUInt32();		// 0
			uint unknown2 = reader.ReadUInt32();
			string chunkName = reader.ReadFixedLengthString(4);		// DBSe
			uint unknown3 = reader.ReadUInt32();
			uint unknown4 = reader.ReadUInt32();		// 1
			uint unknown5 = reader.ReadUInt32();
			uint unknown6 = reader.ReadUInt32();		// 3, subchunk count?

			string chunkNamePHDC = reader.ReadFixedLengthString(4);
			uint chunkSizePHDC = reader.ReadUInt32();	// total size of PHDC chunk (3927 in Bruno)
			uint unknown7 = reader.ReadUInt32();		// 4
			
			uint phonemeCount = reader.ReadUInt32();	// 32
			for (uint i = 0; i < phonemeCount; i++)
			{
				string phonemeName = reader.ReadFixedLengthString(30);
				byte phonemeData = reader.ReadByte();
			}

			string chunkNamePHG2 = reader.ReadFixedLengthString(4);
			uint chunkSizePHG2 = reader.ReadUInt32();	// total size of PHG2 chunk (515 in Bruno)

			uint groupCount = reader.ReadUInt32();		// 9
			for (uint i = 0; i < groupCount; i++)
			{
				uint groupNameLength = reader.ReadUInt32();
				string groupName = reader.ReadFixedLengthString(groupNameLength);
				uint groupPhonemeCount = reader.ReadUInt32();
				for (uint j = 0; j < phonemeCount; j++)
				{
					uint unknown8 = reader.ReadUInt32();	// unknown (possibly index into PHDC table?)
					uint groupPhonemeNameLength = reader.ReadUInt32();
					string groupPhonemeName = reader.ReadFixedLengthString(groupPhonemeNameLength);
				}
				uint unknown9 = reader.ReadUInt32();
			}



			// whenever we get to "TDB " chunk


		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
