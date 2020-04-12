//
//  DERDataFormat.cs - provides a DataFormat for manipulating AbstractSyntaxObjectModel data in Distinguished Encoding Rules (DER) format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.AbstractSyntax;

namespace UniversalEditor.DataFormats.AbstractSyntax.DER
{
	/// <summary>
	/// Provides a DataFormat for manipulating <see cref="AbstractSyntaxObjectModel" /> data in Distinguished Encoding Rules (DER) format.
	/// </summary>
	public class DERDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		/// <summary>
		/// Creates a new <see cref="DataFormatReference" /> containing the appropriate metadata for this <see cref="DERDataFormat" /> and caches it, returning the cached instance.
		/// </summary>
		/// <returns>The <see cref="DataFormatReference" /> that provides metadata and other information about this <see cref="DERDataFormat" />.</returns>
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(AbstractSyntaxObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Reads the data from the <see cref="Accessor" /> into the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> into which to load data.</param>
		/// <exception cref="ObjectModelNotSupportedException">The given <see cref="ObjectModel" /> is not supported by this <see cref="DataFormat" />.</exception>
		/// <exception cref="InvalidDataFormatException">The data being loaded from the <see cref="Accessor" /> is invalid for this <see cref="DataFormat" />.</exception>
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			AbstractSyntaxObjectModel asn = (objectModel as AbstractSyntaxObjectModel);
			if (asn == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			object val = ReadValue(reader);

			List<object> seq = (val as List<object>);
			if (seq != null)
			{
			}
		}

		/// <summary>
		/// Reads a DER-encoded ASN.1 value and returns it.
		/// </summary>
		/// <returns>The ASN.1 value decoded from the <see cref="Reader" />.</returns>
		/// <param name="reader">The <see cref="Reader" /> used to read from the current <see cref="Accessor" />.</param>
		private object ReadValue(Reader reader)
		{
			DERTypeTag identifier = (DERTypeTag)reader.ReadByte();
			byte valueLen = reader.ReadByte();
			long end = reader.Accessor.Position - valueLen;

			switch (identifier)
			{
				case DERTypeTag.Sequence0x10:
				{
					break;
				}
				case DERTypeTag.Sequence0x30:
				{
					List<object> list = new List<object>();
					while (reader.Accessor.Position < end)
					{
						object seqVal = ReadValue(reader);
						list.Add(seqVal);
					}
					return list;
				}
				case DERTypeTag.Integer:
				{
					switch (valueLen)
					{
					case 1: return reader.ReadByte();
					case 2: return reader.ReadInt16();
					case 3: return reader.ReadInt24();
					case 4: return reader.ReadInt32();
					case 8: return reader.ReadInt64();
					default: throw new NotSupportedException(String.Format("DER: cannot read Integer with length {0}", valueLen));
					}
					break;
				}
				case DERTypeTag.IA5String:
				{
					return reader.ReadFixedLengthString(valueLen);
					break;
				}
			}
			return null;
		}

		/// <summary>
		/// Writes the contents of the specified <see cref="ObjectModel" /> to the <see cref="Accessor" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> from which to save data.</param>
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
