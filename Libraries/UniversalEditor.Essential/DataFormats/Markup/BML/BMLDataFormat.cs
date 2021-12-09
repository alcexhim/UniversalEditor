//
//  BMLDataFormat.cs
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
using UniversalEditor.DataFormats.Markup.BML.Internal;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Markup.BML
{
	/// <summary>
	/// Binary Markup Language - (c) MBs
	/// </summary>
	public class BMLDataFormat : DataFormat
	{
		public uint FormatVersion { get; set; } = 1;

		private bool _reading = false;
		private bool _InDocument = false;
		private BMLHeader header;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			MarkupObjectModel mom = (objectModel as MarkupObjectModel);
			if (mom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "BML!")
				throw new InvalidDataFormatException();

			FormatVersion = reader.ReadUInt32();

			BMLFlags flags = (BMLFlags)reader.ReadUInt32();

			header = new BMLHeader();
			header.textOffset = reader.ReadUInt32();
			header.textLength = reader.ReadUInt32();

			header.dataOffset = reader.ReadUInt32();
			header.dataLength = reader.ReadUInt32();

			header.stringTableOffset = reader.ReadUInt32();
			header.stringTableLength = reader.ReadUInt32();

			_reading = true;
			while (_reading)
			{
				BMLOpcode opcode = (BMLOpcode)reader.ReadByte();
				if (!ProcessOpcode(opcode, mom))
				{
					throw new InvalidDataFormatException("opcode {0} not understood by this implementation");
				}
			}
		}

		private byte[] ReadData(uint offset, uint length)
		{
			Accessor.SavePosition();

			Accessor.Reader.Seek(header.dataOffset + offset, SeekOrigin.Begin);
			byte[] data = Accessor.Reader.ReadBytes(length);

			Accessor.LoadPosition();
			return data;
		}
		private string ReadString(uint index)
		{
			Accessor.SavePosition();

			Accessor.Reader.Seek(header.stringTableOffset, SeekOrigin.Begin);
			for (uint i = 0; i < index; i++)
			{
				Accessor.Reader.ReadNullTerminatedString();
			}
			string data = Accessor.Reader.ReadNullTerminatedString();

			Accessor.LoadPosition();
			return data;
		}

		private Stack<MarkupTagElement> _tags = new Stack<MarkupTagElement>();

		protected virtual bool ProcessOpcode(BMLOpcode opcode, MarkupObjectModel objectModel)
		{
			Reader reader = Accessor.Reader;
			switch (opcode)
			{
				case BMLOpcode.DocumentBegin:
				{
					_InDocument = true;
					return true;
				}
				case BMLOpcode.DocumentEnd:
				{
					_InDocument = false;
					_reading = false;
					return true;
				}
				case BMLOpcode.Preprocessor:
				{
					MarkupPreprocessorElement el = new MarkupPreprocessorElement();
					uint index = reader.ReadUInt32();
					el.Value = ReadString(index);
					return true;
				}
				case BMLOpcode.TagBegin:
				{
					MarkupTagElement tag = new MarkupTagElement();
					uint nameIndex = reader.ReadUInt32();
					tag.Name = ReadString(nameIndex);
					_tags.Push(tag);
					return true;
				}
				case BMLOpcode.Attribute:
				case BMLOpcode.Literal:
				case BMLOpcode.Comment:
				case BMLOpcode.CDATA:
				{
					if (_tags.Count > 0)
					{
						MarkupTagElement tag = _tags.Pop();

						if (opcode == BMLOpcode.Attribute)
						{
							MarkupAttribute att = new MarkupAttribute();

							uint nameIndex = reader.ReadUInt32();
							uint valueIndex = reader.ReadUInt32();

							att.Name = ReadString(nameIndex);
							att.Value = ReadString(valueIndex);

							tag.Attributes.Add(att);
						}
						else if (opcode == BMLOpcode.Literal)
						{
							MarkupLiteralElement lit = new MarkupLiteralElement();
							uint contentIndex = reader.ReadUInt32();

							lit.Value = ReadString(contentIndex);
							tag.Elements.Add(lit);
						}
						else if (opcode == BMLOpcode.Comment)
						{
							MarkupCommentElement lit = new MarkupCommentElement();
							uint contentIndex = reader.ReadUInt32();

							lit.Value = ReadString(contentIndex);
							tag.Elements.Add(lit);
						}
						else if (opcode == BMLOpcode.CDATA)
						{
							MarkupStringElement lit = new MarkupStringElement();
							uint contentIndex = reader.ReadUInt32();

							lit.Value = ReadString(contentIndex);
							tag.Elements.Add(lit);
						}
						_tags.Push(tag);
					}
					else
					{
						throw new InvalidDataFormatException(String.Format("illegal opcode '{0}' found without opening tag '{1}'", opcode, BMLOpcode.TagBegin));
					}
					return true;
				}
				case BMLOpcode.TagEnd:
				{
					MarkupTagElement tag = _tags.Pop();
					if (_tags.Count > 0)
					{
						MarkupTagElement tagParent = _tags.Pop();
						tagParent.Elements.Add(tag);
						_tags.Push(tagParent);
					}
					else
					{
						objectModel.Elements.Add(tag);
					}
					return true;
				}
			}
			return false;
		}

		private System.Collections.Specialized.StringCollection _strings = new System.Collections.Specialized.StringCollection();

		protected override void SaveInternal(ObjectModel objectModel)
		{
			MarkupObjectModel mom = (objectModel as MarkupObjectModel);
			if (mom == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.WriteFixedLengthString("BML!");
			writer.WriteUInt32(FormatVersion);

			BMLFlags flags = BMLFlags.None;
			writer.WriteUInt32((uint)flags);

			MemoryAccessor maText = new MemoryAccessor();
			MemoryAccessor maData = new MemoryAccessor();
			MemoryAccessor maStrings = new MemoryAccessor();

			maText.Writer.WriteByte((byte)BMLOpcode.DocumentBegin);
			foreach (MarkupElement el in mom.Elements)
			{
				WriteElement(el, maText.Writer, maData.Writer, maStrings.Writer);
			}
			maText.Writer.WriteByte((byte)BMLOpcode.DocumentEnd);

			uint textOffset = 36;
			writer.WriteUInt32(textOffset);

			byte[] text = maText.ToArray();
			uint textLength = (uint)text.Length;
			writer.WriteUInt32(textLength);

			uint dataOffset = textOffset + textLength;
			writer.WriteUInt32(dataOffset);

			byte[] data = maData.ToArray();
			uint dataLength = (uint)data.Length;
			writer.WriteUInt32(dataLength);

			for (int i = 0; i < _strings.Count; i++)
			{
				maStrings.Writer.WriteNullTerminatedString(_strings[i]);
			}

			uint stringsOffset = dataOffset + dataLength;
			writer.WriteUInt32(stringsOffset);

			byte[] strings = maStrings.ToArray();
			uint stringsLength = (uint)strings.Length;
			writer.WriteUInt32(stringsLength);

			writer.WriteBytes(text);
			writer.WriteBytes(data);
			writer.WriteBytes(strings);
		}

		private uint WriteString(string value, Writer stringWriter)
		{
			if (!_strings.Contains(value))
			{
				_strings.Add(value);
			}
			return (uint) _strings.IndexOf(value);
		}
		private uint WriteData(string value, Writer dataWriter)
		{
			return WriteData(System.Text.Encoding.UTF8.GetBytes(value), dataWriter);
		}
		private uint WriteData(byte[] value, Writer dataWriter)
		{
			uint offset = (uint)dataWriter.Accessor.Position;
			dataWriter.WriteBytes(value);
			return offset;
		}

		private void WriteElement(MarkupElement el, Writer writer, Writer dataWriter, Writer stringsWriter)
		{
			if (el is MarkupPreprocessorElement)
			{
				writer.WriteByte((byte)BMLOpcode.Preprocessor);

				MarkupPreprocessorElement preproc = (el as MarkupPreprocessorElement);
				writer.WriteUInt32(WriteString(preproc.Value, dataWriter));
			}
			else if (el is MarkupTagElement)
			{
				MarkupTagElement tag = (el as MarkupTagElement);
				writer.WriteByte((byte)BMLOpcode.TagBegin);

				writer.WriteUInt32(WriteString(tag.Name, stringsWriter));

				foreach (MarkupAttribute att in tag.Attributes)
				{
					writer.WriteByte((byte)BMLOpcode.Attribute);
					writer.WriteUInt32(WriteString(att.Name, stringsWriter));
					writer.WriteUInt32(WriteString(att.Value, stringsWriter));
				}

				foreach (MarkupElement el1 in tag.Elements)
				{
					WriteElement(el1, writer, dataWriter, stringsWriter);
				}

				writer.WriteByte((byte)BMLOpcode.TagEnd);
			}
			else if (el is MarkupCommentElement)
			{
				writer.WriteByte((byte)BMLOpcode.Comment);
				writer.WriteUInt32(WriteString(el.Value, stringsWriter));
			}
			else if (el is MarkupLiteralElement)
			{
				writer.WriteByte((byte)BMLOpcode.Literal);
				writer.WriteUInt32(WriteString(el.Value, stringsWriter));
			}
			else if (el is MarkupStringElement)
			{
				writer.WriteByte((byte)BMLOpcode.CDATA);
				writer.WriteUInt32(WriteString(el.Value, stringsWriter));
			}
		}
	}
}
