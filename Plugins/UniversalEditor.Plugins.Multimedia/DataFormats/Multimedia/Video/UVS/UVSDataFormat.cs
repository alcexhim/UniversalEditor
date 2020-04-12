//
//  UVSDataFormat.cs - provides a DataFormat for manipulating video files encoded in Ogg/UVS format
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

namespace UniversalEditor.DataFormats.Multimedia.Video.UVS
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating video files encoded in Ogg/UVS format.
	/// </summary>
	public class UVSDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Sources.Add("http://wiki.xiph.org/OggUVS");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			#region Main Header Packet
			string codecIdentifierWord1 = br.ReadFixedLengthString(4);
			string codecIdentifierWord2 = br.ReadFixedLengthString(4);
			if (codecIdentifierWord1 != "UVS " || codecIdentifierWord2 != "    ") throw new InvalidDataFormatException("File does not begin with \"UVS     \"");

			// Version Major (breaks backwards compatability to increment)
			ushort versionMajor = br.ReadUInt16();
			// Version Minor (backwards compatable, ie, more supported format id's)
			ushort versionMinor = br.ReadUInt16();

			ushort displayWidth = br.ReadUInt16();
			ushort displayHeight = br.ReadUInt16();

			ushort pixelAspectRatioNumerator = br.ReadUInt16();
			ushort pixelAspectRatioDenominator = br.ReadUInt16();
			double pixelAspectRatio = ((double)pixelAspectRatioNumerator / (double)pixelAspectRatioDenominator);

			ushort fieldRateNumerator = br.ReadUInt16();
			ushort fieldRateDenominator = br.ReadUInt16();
			double fieldRate = ((double)fieldRateNumerator / (double)fieldRateDenominator);

			uint timebase = br.ReadUInt32(); // in hertz
			uint fieldImageSize = br.ReadUInt32(); // in bytes
			uint extraHeaderCount = br.ReadUInt32();
			UVSColorspace colorspace = (UVSColorspace)(br.ReadUInt32());

			UVSFlags flags = (UVSFlags)(br.ReadUInt32());
			UVSLayoutID layoutID = new UVSLayoutID(br.ReadUInt32());
			#endregion
			#region Comment packet
			#endregion
			if (layoutID == UVSLayoutID.Custom)
			{

			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
