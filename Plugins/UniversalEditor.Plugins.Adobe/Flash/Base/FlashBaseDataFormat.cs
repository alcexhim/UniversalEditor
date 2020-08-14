//
//  FlashBaseDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using MBS.Framework.Drawing;
using UniversalEditor.IO;

namespace UniversalEditor.Plugins.Adobe.Flash.Base
{
	public class FlashBaseDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FlashBaseObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public bool Compressed { get; set; } = false;
		public byte FormatVersion { get; set; } = 5;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FlashBaseObjectModel swf = (objectModel as FlashBaseObjectModel);
			if (swf == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			string signature = reader.ReadFixedLengthString(3);
			if (signature == "CWS")
			{
				Compressed = true;
			}
			else if (signature == "FWS")
			{
				Compressed = false;
			}
			else
			{
				throw new InvalidDataFormatException("file does not begin with 'CWS' (compressed) or 'FWS' (uncompressed)");
			}

			FormatVersion = reader.ReadByte();

			uint totalFileLength = reader.ReadUInt32();
			Rectangle frameSize = ReadRECT(reader);

			ushort frameRate = reader.ReadUInt16(); // 8.8
			ushort frameCount = reader.ReadUInt16();
		}

		private Rectangle ReadRECT(Reader reader)
		{
			// Bit values are stored by using the minimum number of bits possible for the range needed.
			// Most bit value fields use a fixed number of bits.Some use a variable number of bits, but in all
			// such cases, the number of bits to be used is explicitly stated in another field in the same
			// structure.In these variable - length cases, applications that generate SWF files must determine
			// the minimum number of bits necessary to represent the actual values that will be specified.
			// For signed-bit values, if the number to be encoded is positive, an extra bit is necessary to
			// preserve the leading 0; otherwise sign extension changes the bit value into a negative number.
			int nbits_ = reader.ReadBitsAsInt32(5);

			int xmin = 0, ymin = 0, xmax = 0, ymax = 0;
			return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
