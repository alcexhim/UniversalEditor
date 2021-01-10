//
//  PSFDataFormat.cs - provides a DataFormat for manipulating synthesized audio in Sony PlayStation PSF format
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

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.PSF
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio in Sony PlayStation PSF format.
	/// </summary>
	public class PSFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Indicates the platform and format for the PSF file.
		/// </summary>
		/// <remarks>
		/// The platform byte is used to determine the type of PSF file. It does NOT affect the basic
		/// structure of the file in any way. Depending on the platform byte, the reserved and program
		/// sections are interpreted differently. Some tags may also be interpreted differently. Refer
		/// to the linked sections above for more information.
		/// </remarks>
		public PSFPlatform Platform { get; set; } = PSFPlatform.Playstation;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SynthesizedAudioObjectModel audio = (objectModel as SynthesizedAudioObjectModel);
			if (audio == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(3);
			if (signature != "PSF") throw new InvalidDataFormatException("File does not begin with \"PSF\"");
			Platform = (PSFPlatform)br.ReadByte();

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
			bw.WriteByte((byte)Platform);
		}
	}
}
