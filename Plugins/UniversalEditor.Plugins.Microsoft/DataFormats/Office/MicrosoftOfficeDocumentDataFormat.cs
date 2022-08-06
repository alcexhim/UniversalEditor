//
//  MicrosoftOfficeDocumentDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using UniversalEditor.DataFormats.CompoundDocument;
using UniversalEditor.ObjectModels.CompoundDocument;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.Office
{
	public abstract class MicrosoftOfficeDocumentDataFormat : CompoundDocumentDataFormat
	{
		protected abstract string MainDocumentStreamName { get; }
		protected abstract ushort MainDocumentIdentifier { get; }

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			CompoundDocumentObjectModel fsom = new CompoundDocumentObjectModel();

			Folder rootEntry = fsom.Folders.Add("Root Entry");

			MemoryAccessor ma = new MemoryAccessor();
			ma.Writer.WriteUInt16(MainDocumentIdentifier);

			ushort fib_nFib = 0;
			ma.Writer.WriteUInt16(fib_nFib);

			ushort fib_unused1 = 0;
			ma.Writer.WriteUInt16(fib_unused1);

			ushort fib_lid = 0;
			ma.Writer.WriteUInt16(fib_lid);

			ushort fib_pnNext = 0;
			ma.Writer.WriteUInt16(fib_pnNext);

			ushort fib_flags = 0;
			ma.Writer.WriteUInt16(fib_flags);

			ushort fib_nFibBack = 0;
			ma.Writer.WriteUInt16(fib_nFibBack);

			uint fib_lKey = 0;
			ma.Writer.WriteUInt32(fib_lKey);

			byte fib_envr = 0;
			ma.Writer.WriteByte(fib_envr);

			byte fib_flags2 = 0;
			ma.Writer.WriteByte(fib_flags2);

			ushort fib_reserved3 = 0;
			ma.Writer.WriteUInt16(fib_reserved3);

			ushort fib_reserved4 = 0;
			ma.Writer.WriteUInt16(fib_reserved4);

			uint fib_reserved5 = 0;
			ma.Writer.WriteUInt32(fib_reserved5);

			uint fib_reserved6 = 0;
			ma.Writer.WriteUInt32(fib_reserved6);

			ma.Close();

			File mainDocument = rootEntry.Files.Add(MainDocumentStreamName, ma.ToArray());

			objectModels.Push(fsom);

			base.BeforeSaveInternal(objectModels);
		}

		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			CompoundDocumentObjectModel fsom = objectModels.Pop() as CompoundDocumentObjectModel;

			File mainDocument = fsom.Folders["Root Entry"].Files[MainDocumentStreamName];
			if (mainDocument == null)
			{
				throw new InvalidDataFormatException(String.Format("compound document file does not contain a '{0}' stream", MainDocumentStreamName));
			}

			MemoryAccessor ma = new MemoryAccessor(mainDocument.GetData());

			ushort fib_wIdent = ma.Reader.ReadUInt16();
			if (fib_wIdent != MainDocumentIdentifier)
			{
				throw new InvalidDataFormatException(String.Format("file information block does not contain document signature 0x{0}", MainDocumentIdentifier.ToString("x")));
			}
			ushort fib_nFib = ma.Reader.ReadUInt16();
			ushort fib_unused1 = ma.Reader.ReadUInt16();
			ushort fib_lid = ma.Reader.ReadUInt16();
			ushort fib_pnNext = ma.Reader.ReadUInt16();
			ushort fib_flags = ma.Reader.ReadUInt16();
			ushort fib_nFibBack = ma.Reader.ReadUInt16();
			uint fib_lKey = ma.Reader.ReadUInt32();
			byte fib_envr = ma.Reader.ReadByte();
			byte fib_flags2 = ma.Reader.ReadByte();
			ushort fib_reserved3 = ma.Reader.ReadUInt16();
			ushort fib_reserved4 = ma.Reader.ReadUInt16();
			uint fib_reserved5 = ma.Reader.ReadUInt32();
			uint fib_reserved6 = ma.Reader.ReadUInt32();

			objectModels.Push(fsom);
		}
	}
}
