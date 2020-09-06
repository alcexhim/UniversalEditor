//
//  BoxDataFormat.cs - provides a DataFormat for manipulating archives in BOX format
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

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.Box
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in BOX format.
	/// </summary>
	public class BoxDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice(nameof(NumberSize), "_Number size", true,
					new CustomOptionFieldChoice("8-bit, 1 byte per number", 1),
					new CustomOptionFieldChoice("16-bit, 2 bytes per number", 2),
					new CustomOptionFieldChoice("24-bit, 3 bytes per number", 3),
					new CustomOptionFieldChoice("32-bit, 4 bytes per number", 4, true),
					new CustomOptionFieldChoice("64-bit, 8 bytes per number", 8)));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(AllocationSize), "_Allocation size", 512));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Comment), "_Comment"));
			}
			return _dfr;
		}

		/// <summary>
		/// The size of an SNUM or a UNUM field.
		/// </summary>
		public byte NumberSize { get; set; } = 4;

		public ulong ReadUNum(IO.Reader br)
		{
			switch (NumberSize)
			{
				case 1: return br.ReadByte();
				case 2: return br.ReadUInt16();
				case 3: return br.ReadUInt24();
				case 4: return br.ReadUInt32();
				case 8: return br.ReadUInt64();
			}
			throw new InvalidOperationException("Invalid number size (" + NumberSize.ToString() + ")");
		}
		public long ReadSNum(IO.Reader br)
		{
			switch (NumberSize)
			{
				case 1: return br.ReadSByte();
				case 2: return br.ReadInt16();
				case 3: return br.ReadInt24();
				case 4: return br.ReadInt32();
				case 8: return br.ReadInt64();
			}
			throw new InvalidOperationException("Invalid number size (" + NumberSize.ToString() + ")");
		}

		public void WriteUNum(IO.Writer bw, ulong value)
		{
			switch (NumberSize)
			{
				case 1: bw.WriteByte((byte)value); return;
				case 2: bw.WriteUInt16((ushort)value); return;
				case 3: bw.WriteUInt24((uint)value); return;
				case 4: bw.WriteUInt32((uint)value); return;
				case 8: bw.WriteUInt64((ulong)value); return;
			}
			throw new InvalidOperationException("Invalid number size (" + NumberSize.ToString() + ")");
		}
		public void WriteSNum(IO.Writer bw, long value)
		{
			switch (NumberSize)
			{
				case 1: bw.WriteSByte((sbyte)value); return;
				case 2: bw.WriteInt16((short)value); return;
				case 3: bw.WriteInt24((int)value); return;
				case 4: bw.WriteInt32((int)value); return;
				case 8: bw.WriteInt64((long)value); return;
			}
			throw new InvalidOperationException("Invalid number size (" + NumberSize.ToString() + ")");
		}

		public ulong AllocationSize { get; set; } = 512;
		public string Comment { get; set; } = String.Empty;
		public bool External { get; set; } = false;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader brf = base.Accessor.Reader;
			IO.Reader br = brf;

			string BOX_FILE = brf.ReadFixedLengthString(8);
			if (BOX_FILE != "BOX FILE") throw new InvalidDataFormatException("File does not begin with \"BOX FILE\"");
			// BOX FILE is a HUGE file format. it's designed to hold a plethora of data, and be fast
			// and efficient on any type of system. therefore we use NUMBER everywhere.

			byte numsize = brf.ReadByte();
			External = brf.ReadBoolean();
			if (External)
			{
				string FileName = null;
				if (Accessor is FileAccessor) FileName = (Accessor as FileAccessor).FileName;
				if (FileName == null) throw new InvalidOperationException("External BOX FILE not mapped to a section list");

				FileName = System.IO.Path.ChangeExtension(FileName, "lst");
				if (!System.IO.File.Exists(FileName)) throw new InvalidOperationException("External BOX FILE not mapped to a section list");

				IO.Reader brl = new IO.Reader(new FileAccessor(FileName));
				string BOX_LIST = brl.ReadFixedLengthString(8);
				if (BOX_LIST != "BOX LIST") throw new InvalidDataFormatException("File does not begin with \"BOX LIST\"");

				br = brl;
			}

			// its primary purpose is not to store files, but to store sections. this is similar to
			// the Versatile Container file format.
			ulong sectionCount = ReadUNum(br);
			AllocationSize = ReadUNum(br);

			// comment can store arbitrary text data relating to this box
			Comment = br.ReadFixedLengthString(64);

			// sections are named with a 64-byte name, followed by an offset and virtual size. this
			// means that theoretically sections can be located anywhere in the file.
			for (ulong i = 0; i < sectionCount; i++)
			{
				string sectionName = br.ReadFixedLengthString(64);
				ulong sectionOffset = ReadUNum(br);
				ulong sectionVirtualSize = ReadUNum(br);

				File file = new File();
				file.Name = sectionName;
				file.Source = new EmbeddedFileSource(brf, (long)sectionOffset, (long)sectionVirtualSize);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;

			bw.WriteFixedLengthString("BOX FILE");
			bw.WriteByte(NumberSize);
			bw.WriteBoolean(External);
			if (External)
			{
			}

			WriteUNum(bw, (ulong)fsom.Files.Count);
			WriteUNum(bw, AllocationSize);
			bw.WriteFixedLengthString(Comment, 64);

			ulong sectionOffset = (ulong)(bw.Accessor.Position + (fsom.Files.Count * (64 + (2 * NumberSize))));
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 64);
				WriteUNum(bw, sectionOffset);
				WriteUNum(bw, (ulong)file.Size);

				sectionOffset += (ulong)file.Size;
			}
			foreach (File file in fsom.Files)
			{
				file.WriteTo(bw);

				ulong allocationPadding = ((ulong)file.Size / AllocationSize);
				ulong rem = allocationPadding * AllocationSize;
				ulong r = (ulong)file.Size - rem;
				bw.WriteBytes(new byte[r]);
			}
			bw.Flush();
		}
	}
}
