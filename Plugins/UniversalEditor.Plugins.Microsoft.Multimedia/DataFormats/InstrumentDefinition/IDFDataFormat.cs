//
//  IDFDataFormat.cs - implements the Microsoft MIDI Instrument Definition (IDF) file format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.Multimedia.InstrumentDefinition;

namespace UniversalEditor.Plugins.Microsoft.Multimedia.DataFormats.InstrumentDefinition
{
	/// <summary>
	/// Implements the Microsoft MIDI Instrument Definition (IDF) file format.
	/// </summary>
	public class IDFDataFormat : RIFFDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(InstrumentDefinitionObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Method called BEFORE the <see cref="M:UniversalEditor.DataFormat.LoadInternal(UniversalEditor.ObjectModel@)" /> method is called on the original <see cref="T:UniversalEditor.DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// When inheriting from a
		/// <see cref="T:UniversalEditor.DataFormat" /> subclass (e.g. XMLDataFormat), you need to create a new instance of the appropriate <see cref="T:UniversalEditor.ObjectModel" /> that the
		/// subclass expects, and push that onto the <paramref name="objectModels"/> stack, i.e. <code>objectModels.Push(new MarkupObjectModel());</code> This is
		/// usually the only line of code in the overridden <see cref="M:UniversalEditor.DataFormat.BeforeLoadInternal(System.Collections.Generic.Stack{UniversalEditor.ObjectModel})" /> method's body.
		/// </remarks>
		/// <example>
		/// objectModels.Push(new BaseObjectModel()); // this is all we need to do
		/// </example>
		/// <param name="objectModels">The stack of <see cref="T:UniversalEditor.ObjectModel"/>s used by this <see cref="T:UniversalEditor.DataFormat" />.</param>
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}

		/// <summary>
		/// Method called AFTER the <see cref="M:UniversalEditor.DataFormat.LoadInternal(UniversalEditor.ObjectModel@)"/> method is called on the original <see cref="T:UniversalEditor.DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// When inheriting from a <see cref="T:UniversalEditor.DataFormat" /> subclass (e.g. XMLDataFormat), you need to first pop the <see cref="T:UniversalEditor.ObjectModel" /> that you pushed
		/// onto the <paramref name="objectModels"/> stack in your <see cref="M:UniversalEditor.DataFormat.BeforeLoadInternal(System.Collections.Generic.Stack{UniversalEditor.ObjectModel})" /> implementation, then pop the <see cref="T:UniversalEditor.ObjectModel" /> that
		/// your class expects to get passed. Now you can read data from the original <see cref="T:UniversalEditor.ObjectModel" /> and modify the second <see cref="T:UniversalEditor.ObjectModel" />.
		/// Because these objects are passed by reference, you do not need to push them back onto the stack for them to get properly loaded.
		/// </remarks>
		/// <example>
		/// BaseObjectModel bom = (objectModels.Pop() as BaseObjectModel); // base object model comes first
		/// MyVerySpecificObjectModel myOM = (objectModels.Pop() as MyVerySpecificObjectModel);
		///
		/// // populate MyVerySpecificObjectModel... and we're done. nothing else needs to be pushed back onto the stack.
		/// </example>
		/// <param name="objectModels">The stack of <see cref="T:UniversalEditor.ObjectModel"/>s used by this <see cref="T:UniversalEditor.DataFormat" />.</param>
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel chunked = objectModels.Pop() as ChunkedObjectModel;
			InstrumentDefinitionObjectModel idf = objectModels.Pop() as InstrumentDefinitionObjectModel;

			if (chunked.Chunks.Count != 1)
			{
				throw new InvalidDataFormatException("RIFF container does not contain exactly one chunk of type 'RIFF'");
			}

			RIFFGroupChunk chunkRIFF = chunked.Chunks[0] as RIFFGroupChunk;
			if (chunkRIFF?.TypeID != "RIFF" || chunkRIFF?.ID != "IDF ")
			{
				throw new InvalidDataFormatException("RIFF container does not contain exactly one group chunk of type 'RIFF' with ID 'IDF '");
			}

			RIFFDataChunk data1 = (chunkRIFF.Chunks[0] as RIFFDataChunk);
			MemoryAccessor ma = new MemoryAccessor(data1.Data);

			int hdr_len = ma.Reader.ReadInt32();
			if (hdr_len != data1.Size)
				throw new InvalidDataFormatException("recorded chunk length does not match actual chunk length");

			uint hdr_u1 = ma.Reader.ReadUInt32();
			uint hdr_u2 = ma.Reader.ReadUInt32();
			uint name_len = ma.Reader.ReadUInt32();
			string name = ma.Reader.ReadFixedLengthString(name_len).TrimNull();

		}
	}
}
