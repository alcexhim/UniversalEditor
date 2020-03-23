using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.PSF
{
	public class PSFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private PSFPlatform mvarPlatform = PSFPlatform.Playstation;
		/// <summary>
		/// The platform byte is used to determine the type of PSF file. It does NOT affect the basic
		/// structure of the file in any way. Depending on the platform byte, the reserved and program
		/// sections are interpreted differently. Some tags may also be interpreted differently. Refer
		/// to the linked sections above for more information.
		/// </summary>
		public PSFPlatform Platform { get { return mvarPlatform; } set { mvarPlatform = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SynthesizedAudioObjectModel audio = (objectModel as SynthesizedAudioObjectModel);
			if (audio == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(3);
			if (signature != "PSF") throw new InvalidDataFormatException("File does not begin with \"PSF\"");
			mvarPlatform = (PSFPlatform)br.ReadByte();

			// Size of reserved area (R)
			uint reservedAreaLength = br.ReadUInt32();
			// This is the length of the program data _after_ compression.
			uint compressedProgramLength = br.ReadUInt32();
			// This is the CRC-32 of the program data _after_ compression. Filling in this value is
			// mandatory, as a PSF file may be regarded as corrupt if it does not match.
			uint compressedProgramCRC32 = br.ReadUInt32();
			byte[] reservedAreaData = br.ReadBytes(reservedAreaLength);
			byte[] compressedProgramData = br.ReadBytes(compressedProgramLength);

			if (br.Remaining > 5)
			{
				string tag = br.ReadFixedLengthString(5);
				if (tag != "[TAG]") return;

				while (!br.EndOfStream)
				{
					string propValuePair = br.ReadStringUntil("\n");
					string[] propNameValue = propValuePair.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			SynthesizedAudioObjectModel audio = (objectModel as SynthesizedAudioObjectModel);
			if (audio == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("PSF");
			bw.WriteByte((byte)mvarPlatform);
		}
	}
}
