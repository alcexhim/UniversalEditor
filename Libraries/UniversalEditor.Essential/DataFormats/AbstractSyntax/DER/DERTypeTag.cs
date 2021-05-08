//
//  DERTypeTag.cs - indicates the type of tag in a DER file
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

namespace UniversalEditor.DataFormats.AbstractSyntax.DER
{
	/// <summary>
	/// Indicates the type of tag in a DER file.
	/// </summary>
	public enum DERTypeTag
	{
		EndOfContent = 0x00,
		Boolean = 0x01,
		Integer = 0x02,
		BitString = 0x03,
		OctetString = 0x04,
		Null = 0x05,
		ObjectIdentifier = 0x06,
		ObjectDescriptor = 0x07,
		External = 0x08,
		Real = 0x09,
		Enumerated = 0x0A,
		EmbeddedPDV = 0x0B,
		UTF8String = 0x0C,
		RelativeOID = 0x0D,
		Time = 0x0E,
		Reserved0x0F = 0x0F,
		Sequence0x10 = 0x10,
		Set = 0x11,
		NumericString = 0x12,
		PrintableString = 0x13,
		T61String = 0x14,
		VideotexString = 0x15,
		IA5String = 0x16,
		UTCTime = 0x17,
		GeneralizedTime = 0x18,
		GraphicString = 0x19,
		VisibleString = 0x1A,
		GeneralString = 0x1B,
		UniversalString = 0x1C,
		CharacterString = 0x1D,
		BMPString = 0x1E,
		Date = 0x1F,
		TimeOfDay = 0x20,
		DateTime = 0x21,
		Duration = 0x22,
		OIDIRI = 0x23,
		RelativeOIDIRI = 0x24,
		Sequence0x30 = 0x30
	}
}
