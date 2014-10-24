﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Markup.EBML
{
	public class EBMLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Extensible Binary Meta Language", new byte?[][] { new byte?[] { 0x1A, 0x45, 0xDF, 0xA3 } }, new string[] { "*.ebml" });
				_dfr.Sources.Add("http://ebml.sourceforge.net/specs/");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			MarkupObjectModel mom = (objectModel as MarkupObjectModel);
			if (mom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			reader.Accessor.Seek(0, SeekOrigin.Begin);

			while (!reader.EndOfStream)
			{
				MarkupElement el = ReadEBMLElement(reader);
				mom.Elements.Add(el);
			}
		}

		private MarkupElement ReadEBMLElement(Reader reader)
		{
			long elementID = ReadEBMLElementID(reader);
			long dataSize = ReadEBMLElementID(reader);
			byte[] data = reader.ReadBytes(dataSize);

			MarkupTagElement tag = new MarkupTagElement();
			tag.FullName = elementID.ToString();

			if (elementID == 0x0A45DFA3)
			{
				Reader rdr = new Reader(new MemoryAccessor(data));
				while (!rdr.EndOfStream)
				{
					MarkupElement el = ReadEBMLElement(rdr);
					tag.Elements.Add(el);
				}
				reader.Close();
			}
			else
			{
				MarkupStringElement mse = new MarkupStringElement();
				mse.FullName = "CDATA";
				mse.Value = Convert.ToBase64String(data);
				tag.Elements.Add(mse);
			}
			return tag;
		}

		private long ReadEBMLElementID(Reader reader)
		{
			byte[] buffer = reader.ReadBytes(8);
			reader.Seek(-8, SeekOrigin.Current);

			if ((buffer[0] & 0x80) == 0x80)
			{
				// one byte
				buffer[0] = (byte)(buffer[0] & ~0x80);
				reader.Seek(1, SeekOrigin.Current);
				return (long)buffer[0];
			}
			else if ((buffer[0] & 0x40) == 0x40)
			{
				// two bytes
				buffer[0] = (byte)(buffer[0] & ~0x40);
				reader.Seek(2, SeekOrigin.Current);

				byte[] _buffer = new byte[2];
				_buffer[0] = buffer[1];
				_buffer[1] = buffer[0];
				return (long)BitConverter.ToInt16(_buffer, 0);
			}
			else if ((buffer[0] & 0x20) == 0x20)
			{
				// three bytes
				buffer[0] = (byte)(buffer[0] & ~0x20);
				reader.Seek(3, SeekOrigin.Current);

				byte[] _buffer = new byte[4];
				_buffer[0] = 0;
				_buffer[1] = buffer[2];
				_buffer[2] = buffer[1];
				_buffer[3] = buffer[0];
				return (long)BitConverter.ToInt32(_buffer, 0);
			}
			else if ((buffer[0] & 0x10) == 0x10)
			{
				// four bytes
				buffer[0] = (byte)(buffer[0] & ~0x10);
				reader.Seek(4, SeekOrigin.Current);

				byte[] _buffer = new byte[4];
				_buffer[0] = buffer[3];
				_buffer[1] = buffer[2];
				_buffer[2] = buffer[1];
				_buffer[3] = buffer[0];
				return (long)BitConverter.ToInt32(_buffer, 0);
			}
			else if ((buffer[0] & 0x08) == 0x08)
			{
				// five bytes
				buffer[0] = (byte)(buffer[0] & ~0x08);
				reader.Seek(5, SeekOrigin.Current);

				byte[] _buffer = new byte[8];
				_buffer[0] = 0;
				_buffer[1] = 0;
				_buffer[2] = 0;
				_buffer[3] = buffer[4];
				_buffer[4] = buffer[3];
				_buffer[5] = buffer[2];
				_buffer[6] = buffer[1];
				_buffer[7] = buffer[0];
				return (long)BitConverter.ToInt64(_buffer, 0);
			}
			else if ((buffer[0] & 0x04) == 0x04)
			{
				// six bytes
				buffer[0] = (byte)(buffer[0] & ~0x04);
				reader.Seek(6, SeekOrigin.Current);

				byte[] _buffer = new byte[8];
				_buffer[0] = 0;
				_buffer[1] = 0;
				_buffer[2] = buffer[5];
				_buffer[3] = buffer[4];
				_buffer[4] = buffer[3];
				_buffer[5] = buffer[2];
				_buffer[6] = buffer[1];
				_buffer[7] = buffer[0];
				return (long)BitConverter.ToInt64(_buffer, 0);
			}
			else if ((buffer[0] & 0x02) == 0x02)
			{
				// seven bytes
				buffer[0] = (byte)(buffer[0] & ~0x02);
				reader.Seek(7, SeekOrigin.Current);

				byte[] _buffer = new byte[8];
				_buffer[0] = 0;
				_buffer[1] = buffer[6];
				_buffer[2] = buffer[5];
				_buffer[3] = buffer[4];
				_buffer[4] = buffer[3];
				_buffer[5] = buffer[2];
				_buffer[6] = buffer[1];
				_buffer[7] = buffer[0];
				return (long)BitConverter.ToInt64(_buffer, 0);
			}
			else if ((buffer[0] & 0x01) == 0x01)
			{
				// eight bytes
				buffer[0] = (byte)(buffer[0] & ~0x01);
				reader.Seek(8, SeekOrigin.Current);

				byte[] _buffer = new byte[8];
				_buffer[0] = buffer[7];
				_buffer[1] = buffer[6];
				_buffer[2] = buffer[5];
				_buffer[3] = buffer[4];
				_buffer[4] = buffer[3];
				_buffer[5] = buffer[2];
				_buffer[6] = buffer[1];
				_buffer[7] = buffer[0];
				return (long)BitConverter.ToInt64(_buffer, 0);
			}

			// Since modern computers do not easily deal with data coded in sizes greater than 64 bits,
			// any larger Element Sizes are left undefined at the moment. Currently, the Element Size
			// coding allows for an Element to grow to 72000 To, i.e. 7x10^16 octets or 72000 terabytes,
			// which will be sufficient for the time being.
			throw new NotImplementedException("Unknown Element Size coding: 0x" + buffer[0].ToString("X"));
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}