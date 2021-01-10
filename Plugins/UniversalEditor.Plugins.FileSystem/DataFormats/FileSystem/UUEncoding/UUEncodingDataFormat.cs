//
//  UUEncodingDataFormat.cs - provides a DataFormat for manipulating archives in UUEncode format
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
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.UUEncoding
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in UUEncode format.
	/// </summary>
	public class UUEncodingDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private static byte[] UUDecode(byte[] input)
		{
			System.IO.MemoryStream msInput = new System.IO.MemoryStream(input);
			System.IO.MemoryStream msOutput = new System.IO.MemoryStream();

			if (msInput == null)
				throw new ArgumentNullException("input");

			if (msOutput == null)
				throw new ArgumentNullException("output");

			long len = msInput.Length;
			if (len == 0) throw new InvalidOperationException();

			long didx = 0;
			int nextByte = msInput.ReadByte();
			while (nextByte >= 0)
			{
				// get line length (in number of encoded octets)
				int line_len = (nextByte - 0x20) & 0x3F;

				// ascii printable to 0-63 and 4-byte to 3-byte conversion
				long end = didx + line_len;
				byte A, B, C, D;
				if (end > 2)
				{
					while (didx < end - 2)
					{
						A = (byte)((msInput.ReadByte() - 0x20) & 0x3F);
						B = (byte)((msInput.ReadByte() - 0x20) & 0x3F);
						C = (byte)((msInput.ReadByte() - 0x20) & 0x3F);
						D = (byte)((msInput.ReadByte() - 0x20) & 0x3F);

						msOutput.WriteByte((byte)(((A << 2) & 255) | ((B >> 4) & 3)));
						msOutput.WriteByte((byte)(((B << 4) & 255) | ((C >> 2) & 15)));
						msOutput.WriteByte((byte)(((C << 6) & 255) | (D & 63)));
						didx += 3;
					}
				}

				if (didx < end)
				{
					A = (byte)((msInput.ReadByte() - 0x20) & 0x3F);
					B = (byte)((msInput.ReadByte() - 0x20) & 0x3F);
					msOutput.WriteByte((byte)(((A << 2) & 255) | ((B >> 4) & 3)));
					didx++;
				}

				if (didx < end)
				{
					B = (byte)((msInput.ReadByte() - 0x20) & 0x3F);
					C = (byte)((msInput.ReadByte() - 0x20) & 0x3F);
					msOutput.WriteByte((byte)(((B << 4) & 255) | ((C >> 2) & 15)));
					didx++;
				}

				// skip padding
				do
				{
					nextByte = msInput.ReadByte();
				}
				while (nextByte >= 0 && nextByte != '\n' && nextByte != '\r');

				// skip end of line
				do
				{
					nextByte = msInput.ReadByte();
				}
				while (nextByte >= 0 && (nextByte == '\n' || nextByte == '\r'));
			}
			return msOutput.ToArray();
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader tr = base.Accessor.Reader;

			string infoline = tr.ReadLine();

			string[] info = infoline.Split(new char[] { ' ' }, 3);
			if (info[0] != "begin") throw new InvalidDataFormatException();
			int permissions = Int32.Parse(info[1]);
			string fileName = info[2];

			StringBuilder sb = new StringBuilder();
			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				if (line == "end") break;

				sb.Append(line);
			}

			string datastr = sb.ToString();
			byte[] data = System.Text.Encoding.ASCII.GetBytes(datastr);
			data = UUDecode(data);

			File file = new File();
			file.Name = fileName;
			file.SetData(data);
			fsom.Files.Add(file);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
